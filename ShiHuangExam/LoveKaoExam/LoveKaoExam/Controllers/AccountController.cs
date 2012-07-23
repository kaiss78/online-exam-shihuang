using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using LoveKaoExam.Models;
using LoveKaoExam.Data;
using LoveKaoExam.Library.CSharp;
using LoveKao.Page;

namespace LoveKaoExam.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        /// <summary>
        /// 用户登录视图
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">LogOnModel</param>
        /// <param name="returnUrl">登录成功后跳转页面的URL地址</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            //如果用户名与密码都不为空
            if (ModelState.IsValid)
            {
                用户 userInfo = null;

                /* 验证用户登录 */
                int returnValue = MembershipService.ValidateUser(model.UserName, model.Password, out userInfo);

                #region 处理返回值
                /* 用户已登录成功 */
                if (returnValue == 0)
                {
                    //保存cookie
                    FormsService.SignIn(userInfo, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                /* 编号不存在 */
                else if (returnValue == 1)
                {
                    ModelState.AddModelError("", "用户名不存在，请重新输入");
                }
                /* 密码错误 */
                else if (returnValue == 2)
                {
                    ModelState.AddModelError("", "密码错误，请重新输入");
                }
                #endregion
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult LogOnAjax(LogOnModel model)
        {
            //如果用户名与密码都不为空
            if (ModelState.IsValid)
            {
                用户 userInfo = null;

                /* 验证用户登录 */
                int returnValue = MembershipService.ValidateUser(model.UserName, model.Password, out userInfo);

                #region 处理返回值
                /* 编号不存在 */
                if (returnValue == 1)
                {
                    return LKPageJsonResult.Failure("用户名不存在，请重新输入");
                }
                /* 密码错误 */
                else if (returnValue == 2)
                {
                    return LKPageJsonResult.Failure("密码错误，请重新输入");
                }
                else if (returnValue == -1)
                {
                    return LKPageJsonResult.Failure("数据库连接失败，请更改web.config的数据库连接字符串");
                }
                else
                {
                    //保存cookie
                    FormsService.SignIn(userInfo, model.RememberMe);
                    return LKPageJsonResult.Success(new { uname = userInfo.姓名, uemail = userInfo.邮箱, uguid = userInfo.ID });
                }
                #endregion
            }
            else
            {
                return LKPageJsonResult.Failure("请输入您的用户名或密码");
            }
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 用户修改密码视图
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model">ChangePasswordModel</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            #region try/catch(){}
            try
            {
                //修改密码
                int returnValue = 用户.修改用户密码(UserInfo.CurrentUser.用户ID, model.oldPawd, model.newPawd);

                #region 处理返回值
                /* 如果返回0表示原密码错误 */
                if (returnValue == 1)
                {
                    return LKPageJsonResult.Failure("您输入的原密码错误");
                }
                /* 返回1表示修改成功 */
                else
                {
                    return LKPageJsonResult.Success();
                }
                #endregion
            }
            catch (Exception ex)
            {
                /* 返回异常后Json格式字符串 */
                return LKPageJsonResult.Exception(ex);
            }
            #endregion
        }

        /// <summary>
        /// 用户详细资料视图
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Details()
        {
            用户 model = new 用户();

            #region try/catch(){}
            try
            {
                model = 用户.得到用户基本信息根据ID(UserInfo.CurrentUser.用户ID);
            }
            catch (Exception ex)
            {
            }
            #endregion

            return View(model);
        }

        /// <summary>
        /// 修改资料
        /// </summary>
        /// <param name="model">用户</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public JsonResult Details(用户 model)
        {
            #region try/catch(){}
            try
            {
                model.ID = UserInfo.CurrentUser.用户ID;

                var reVal = 用户.修改用户个人信息(model);

                #region 处理返回值
                if (reVal == 1)
                {
                    return LKPageJsonResult.Exists("编号", model.编号);
                }
                if (reVal == 2)
                {
                    return LKPageJsonResult.Exists("邮箱", model.邮箱);
                }
                else
                {
                    return LKPageJsonResult.Success();
                }
                #endregion
            }
            catch (Exception ex)
            {
                /* 返回异常后Json格式字符串 */
                return LKPageJsonResult.Exception(ex);
            }
            #endregion
        }
    }
}