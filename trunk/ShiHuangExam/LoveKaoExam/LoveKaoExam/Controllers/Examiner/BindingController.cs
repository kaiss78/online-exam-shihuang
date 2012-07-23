using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKaoExam.Data;
using LoveKaoExam.Library.CSharp;
using LoveKaoExam.Models;
using LoveKaoExam.Models.Examiner;
using LoveKao.Page;

namespace LoveKaoExam.Controllers.Examiner
{
    [Authorize(Roles = "考官")]
    public class BindingController : BaseController
    {
        /// <summary>
        /// 绑定账号
        /// </summary>
        /// <returns></returns>
        public ActionResult LKAccount()
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            LKPageException lkPageException = null;
            绑定账号表 绑定账号表Model = 用户.得到用户绑定信息(UserInfo.CurrentUser.用户ID, out  lkPageException);
            绑定账号信息Models c绑定账号信息Models = new 绑定账号信息Models(绑定账号表Model, lkPageException);

            if (Request.IsAjaxRequest())
            {
                return View("~/Views/Examiner/Binding/LKAccount.ascx", c绑定账号信息Models);
            }

            return View("~/Views/Examiner/Binding/LKAccount.aspx", c绑定账号信息Models);
        }

        /// <summary>
        /// 验证账号
        /// </summary>
        /// <returns></returns>
        public ActionResult Validate()
        {
            int 用户操作类型 = LKExamURLQueryKey.GetInt32("uHandleType");
            int 用户绑定类型 = LKExamURLQueryKey.GetInt32("uBindingType");

            用户 用户Model = new 用户();//用户.得到用户基本信息根据ID(UserInfo.CurrentUser.用户ID);

            爱考网绑定账号 model = new 爱考网绑定账号();
            model.用户操作类型 = 用户操作类型;
            model.用户绑定类型 = 用户绑定类型;
            model.账号 = 用户Model.编号;
            model.邮箱 = 用户Model.邮箱;

            return View("~/Views/Examiner/Binding/Validate.aspx", model);
        }

        /// <summary>
        /// 绑定/更改绑定账号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Validate(爱考网绑定账号 model)
        {
            try
            {
                //绑定还未绑定的账号
                if (model.用户操作类型 == 0)
                {
                    //绑定新账号
                    if (model.用户绑定类型 == 0)
                    {
                        用户.绑定新账号(model.账号, model.密码, model.邮箱);
                    }
                    //绑定已有账号
                    else
                    {
                        用户.绑定已有账号(model.账号, model.密码);
                    }
                }
                //修改已绑定的账号
                else
                {
                    //修改绑定新账号
                    if (model.用户绑定类型 == 0)
                    {
                        用户.更改绑定新账号(model.账号, model.密码, model.邮箱);
                    }
                    //修改绑定已有账号
                    else
                    {
                        用户.更改绑定已有账号(model.账号, model.密码);
                    }
                }
                return LKPageJsonResult.Success();
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 删除绑定的账号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete()
        {
            try
            {
                int reval = 用户.解除绑定账号();
                if (reval == 1)
                {
                    return LKPageJsonResult.Failure("删除绑定的账号失败");
                }
                return LKPageJsonResult.Success();
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }
    }
}
