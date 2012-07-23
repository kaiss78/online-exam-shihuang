using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using LoveKaoExam.Data;
using System.Web.Mvc;

namespace LoveKaoExam.Library.CSharp
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户名
        /// <para>(1)登录系统的编号</para>
        /// </summary>
        public string 用户名;

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid 用户ID;

        /// <summary>
        /// 用户类型
        /// <para>(1)0代表考生</para>
        /// <para>(2)1代表考官</para>
        /// <para>(3)2代表管理员</para>
        /// </summary>
        public byte 用户类型;

        /// <summary>
        /// 票证用于 Forms 身份验证对用户进行标识
        /// </summary>
        public static FormsAuthenticationTicket FormsAuthTicket
        {
            get
            {
                return FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            }
        }

        /// <summary>
        /// 存储在票证中的用户特定的字符串
        /// </summary>
        /// <param name="formsAuthTicket">FormsAuthenticationTicket</param>
        /// <returns></returns>
        public static string[] FormsAuthUserData(FormsAuthenticationTicket formsAuthTicket)
        {
            return formsAuthTicket.UserData.Split(',');
        }

        /// <summary>
        /// 当前用户信息
        /// <para>(1)用户名</para>
        /// <para>(2)用户ID</para>
        /// <para>(3)用户类型</para>
        /// </summary>
        public static UserInfo CurrentUser
        {
            get
            {
                UserInfo userInfo = new UserInfo();
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    /* 票证用于 Forms 身份验证对用户进行标识 */
                    FormsAuthenticationTicket formsAuthTicket = FormsAuthTicket;
                    
                    /* 用户ID */
                    string userID = formsAuthTicket.Name;

                    /* 存储在票证中的用户特定的字符串 */
                    string[] userData = FormsAuthUserData(formsAuthTicket);

                    userInfo.用户ID = Guid.Parse(userID);
                    userInfo.用户类型 = byte.Parse(userData.First());
                    userInfo.用户名 = userData.Last();
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




