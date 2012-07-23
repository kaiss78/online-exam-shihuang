using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKaoExam.Data;
using Webdiyer.WebControls.Mvc;
using LoveKaoExam.Library.CSharp;
using LoveKao.Page;

namespace LoveKaoExam.Controllers.Admin
{
    [Authorize(Roles = "管理员")]
    public class DepartmentController : BaseController
    {
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            /* 创建部门标题 */
            ViewData["PageTitle"] = "添加" + LKExamEnvironment.部门名称;

            /* 返回创建部门视图 */
            return View("~/Views/Admin/Department/Add.aspx");
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult UCEditor(string id)
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            /* 部门Model */
            部门 部门Model = new 部门();

            #region try/catch(){}
            try
            {
                /* 字符串转Guid */
                Guid 部门ID = LKExamURLQueryKey.SToGuid(id);

                /* 如果部门ID不为空，表示获取该部门信息 */
                if (部门ID != Guid.Empty)
                {
                    部门Model = 部门.得到某部门根据部门ID(部门ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("返回部门信息出现异常,路径为：DepartmentController>UCEditor");
            }
            #endregion
            /* 返回编辑部门信息用户控件视图 */
            return View("~/Views/Admin/Department/UCEditor.ascx", 部门Model);
        }

        /// <summary>
        /// 编辑部门
        /// 返回编辑部门的Json格式字符串
        /// </summary>
        /// <param name="model">部门Model</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UCEditor(部门 model)
        {
            #region try/catch(){}
            try
            {
                /* 创建人ID */
                model.创建人ID = UserInfo.CurrentUser.用户ID;

                /* 编辑部门返回值 */
                int returnValue;

                #region 创建/修改部门Model
                /* 部门ID为空时表示添加 */
                if (model.ID == Guid.Empty)
                {
                    model.ID = Guid.NewGuid();
                    returnValue = 部门.添加部门(model);
                }
                /* 编辑部门 */
                else
                {
                    returnValue = 部门.修改部门(model);
                }
                #endregion

                #region 处理返回值
                /* 如果返回值为1表示 部门已存在 */
                if (returnValue == 1)
                {
                    return LKPageJsonResult.Exists("部门名称", model.名称);
                }

                /* 部门添加/修改成功 */
                else
                {
                    return new JsonResult
                    {
                        Data = new { result = true, info = model }
                    };
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
        /// 删除部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public JsonResult Delete(string id)
        {
            #region try/catch(){}
            try
            {
                Guid 部门ID = LKExamURLQueryKey.SToGuid(id);
                部门.删除部门(部门ID);

                /* 返回执行成功的Json字符串 */
                return LKPageJsonResult.Success();
            }
            catch (Exception ex)
            {
                /* 返回异常后Json格式字符串 */
                return LKPageJsonResult.Exception(ex);
            }
            #endregion
        }

        /// <summary>
        /// 创建/修改部门，嵌入IFrame窗口
        /// 返回/Views/Admin/Department/IFrame.aspx视图
        /// </summary>
        /// <returns></returns>
        public ActionResult IFrame()
        {
            /* 返回部门信息嵌入IFrame窗口视图 */
            return View("~/Views/Admin/Department/IFrame.aspx");
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult List(int? id)
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            /* 部门信息PagedList */
            PagedList<部门> 部门信息PagedList = 部门信息列表(id);

            /* 部门信息信息视图 */
            LKExamMvcPagerData<部门>.数据信息(部门信息PagedList, MvcPager引用目标.管理员_部门信息, ViewData, LKExamEnvironment.部门名称 + "信息");

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Admin/Department/UCList.ascx", 部门信息PagedList);
            }

            /* 返回部门信息视图 */
            return View("~/Views/Admin/Department/List.aspx", 部门信息PagedList);
        }

    }
}
