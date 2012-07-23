using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using LoveKaoExam.Data;
using LoveKaoExam.Library.CSharp;


namespace LoveKaoExam.Models
{

    /// <summary>
    /// 登录
    /// </summary>
    public class LogOnModel
    {
        [Required]
        [DisplayName("账号：")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("密码：")]
        public string Password { get; set; }

        [DisplayName("记住登录状态?")]
        public bool RememberMe { get; set; }
    }

    #region Services
    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>登录成功返回true，否则false 并且out 用户权限</returns>
        int ValidateUser(string userName, string password, out 用户 userInfo);

        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public int ValidateUser(string userName, string password, out 用户 userInfo)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("用户名不能为空.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("密码不能为空.", "password");

            int retval;
            try
            {
                //登录用户方法
                retval = 用户.登录(userName, password, out userInfo);
            }
            catch (Exception)
            {
                retval = -1;
                userInfo = null;
            }

            return retval;


        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }

    public interface IFormsAuthenticationService
    {
        void SignIn(用户 userInfo, bool 是否记住密码);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPermissions">用户类型</param>
        /// <param name="是否记住密码">是否记住密码</param>
        public void SignIn(用户 c用户, bool 是否记住密码)
        {
            try
            {
                if (c用户.ID == Guid.Empty)
                {
                    throw new Exception("该用户还没有登录！");
                }

                HttpCookie clientCookie = new HttpCookie("UserInfo");
                clientCookie.Values.Add("UserGuid", c用户.ID.ToString());
                clientCookie.Values.Add("Uname", c用户.编号);
                clientCookie.Values.Add("UType", c用户.角色.ToString());
                clientCookie.HttpOnly = false;

                string userData = c用户.角色 + "," + c用户.编号;
                //票证
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, c用户.ID.ToString(), DateTime.Now, DateTime.Now.AddDays(7), 是否记住密码, userData);

                //用户名创建身份验证Cookie
                var authCookie = FormsAuthentication.GetAuthCookie(FormsAuthentication.FormsCookieName, false);
                authCookie.Value = FormsAuthentication.Encrypt(ticket);

                //记住密码
                if (是否记住密码)
                {
                    clientCookie.Expires = DateTime.Now.AddDays(7);
                    authCookie.Expires = System.DateTime.Now.AddDays(7);
                }
                HttpContext.Current.Response.Cookies.Add(clientCookie);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        public void SignOut()
        {
            //客户端Cookie
            HttpCookie clientCookie = new HttpCookie("UserInfo");
            clientCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(clientCookie);

            FormsAuthentication.SignOut();
        }
    }
    #endregion

    public class ChangePasswordModel
    {
        /// <summary>
        /// 原密码
        /// </summary>
        public string oldPawd { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string newPawd { get; set; }
    }
}
