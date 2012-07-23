using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using LoveKaoExam.Library.CSharp;

namespace LoveKaoExam.HttpModule
{
    public class AuthenticationModule : IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
        }

        void context_AuthenticateRequest(object sender, EventArgs e)
        {
            //判断是否已经登录，即有authTicket
            if (!HttpContext.Current.Request.IsAuthenticated)
            { return; }

            FormsAuthenticationTicket formsAuthTicket = UserInfo.FormsAuthTicket;

            //角色
            string roleValue = UserInfo.FormsAuthUserData(formsAuthTicket).First();
            string[] roles = new string[1];
            switch (roleValue)
            {
                case "0":
                    roles[0] = "考生";
                    break;
                case "1":
                    roles[0] = "考官";
                    break;
                case "2":
                    roles[0] = "管理员";
                    break;
                default:
                    roles[0] = "匿名用户";
                    break;
            }

            // Create an Identity object
            var id = new FormsIdentity(formsAuthTicket);

            // This principal will flow throughout the request.
            var principal = new GenericPrincipal(id, roles);
            // Attach the new principal object to the current HttpContext object
            HttpContext.Current.User = principal;
        }


    }
}