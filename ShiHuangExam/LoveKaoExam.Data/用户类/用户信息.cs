using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace LoveKaoExam.Data
{
    public class 用户信息
    {      
        public string 用户名;

        public Guid 用户ID;

        public byte 用户类型;

        public static 用户信息 CurrentUser
        {
            get
            {
                用户信息 userInfo = new 用户信息();              
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(
                        HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    string[] userData = ticket.UserData.Split(',');

                    userInfo.用户ID = Guid.Parse(ticket.Name);
                    userInfo.用户类型 = byte.Parse(userData[0]);
                    userInfo.用户名 = userData[1];
                }
                else
                {
                    throw new Exception("该用户还没有登录！");
                }
                return userInfo;
            }
        }

       
    }
}
