using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using LoveKaoExam.Data;
using Webdiyer.WebControls.Mvc;
using LoveKaoExam.Library.CSharp;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.UI;
using LoveKao.Page;


namespace LoveKaoExam.Controllers.Admin
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Authorize(Roles = "管理员")]
    public class UserController : BaseController
    {
        /// <summary>
        /// 创建账户
        /// </summary>
        /// <returns>返回网页视图</returns>
        public ActionResult Add()
        {
            return View("~/Views/Admin/User/Add.aspx");
        }

        /// <summary>
        /// 创建/编辑账户视图
        /// </summary>
        /// <param name="id">要编辑用户的ID</param>
        /// <returns>返回控件视图</returns>
        public ActionResult UCEditor(string id)
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            用户 model = new 用户();
            Guid 用户ID = LKExamURLQueryKey.SToGuid(id);


            /* 如果用户ID不为空 则返回用户基本信息 */
            if (用户ID != Guid.Empty)
            {
                model = 用户.得到用户基本信息根据ID(用户ID);
            }
            else if (LKExamURLQueryKey.GetString("rtype") == "1")
            {
                model.角色 = 1;
                model.性别 = 1;
            }
            /* 返回用户空信息 */
            else
            {
                model.角色 = 0;
                model.性别 = 1;
            }

            ViewData["部门ID"] = new SelectList(部门.查询部门(), "ID", "名称", model.部门ID);

            return View("~/Views/Admin/User/UCEditor.ascx", model);
        }

        /// <summary>
        /// 创建/编辑账户
        /// </summary>
        /// <param name="model">用户</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UCEditor(用户 model)
        {
            #region 用户角色
            if (model.角色 == 0)
            {
                model.邮箱 = "";
            }
            else if (model.角色 == 1)
            {
                model.部门ID = null;
                model.邮箱 = model.邮箱 == null ? "" : model.邮箱;
            }
            #endregion

            #region try/catch(){}
            try
            {
                int returnValue;

                #region 创建/修改用户Model
                if (model.ID == Guid.Empty)
                {
                    returnValue = 用户.添加用户(model);
                }
                else
                {
                    returnValue = 用户.修改用户个人信息(model);
                }
                #endregion

                #region 处理返回值
                if (returnValue == 1)
                {
                    string 角色编号 = LKExamEnvironment.角色编号(model.角色);
                    return LKPageJsonResult.Exists(角色编号, model.编号);
                }
                else if (returnValue == 2)
                {
                    return LKPageJsonResult.Exists("邮箱地址", model.邮箱);
                }
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
        /// 创建/修改账户，嵌入IFrame窗口
        /// 返回/Views/Admin/User/IFrame.aspx视图
        /// </summary>
        /// <returns></returns>
        public ActionResult IFrame()
        {
            /*
             *	返回部门信息嵌入IFrame窗口视图
             *--------------------*/
            return View("~/Views/Admin/User/IFrame.aspx");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string id)
        {
            #region try/catch(){}
            try
            {
                Guid 用户ID = LKExamURLQueryKey.SToGuid(id);
                用户.删除用户(用户ID);

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
        /// 考生信息列表
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult examinee(int? id)
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            /* 考生信息视图标题 */
            string pageTitle = LKExamEnvironment.考生名称 + "信息";

            /* 考生PagedList */
            PagedList<考生> 考生PagedList = 考生信息列表(id);

            /* 信息视图 */
            LKExamMvcPagerData<考生>.数据信息(考生PagedList, MvcPager引用目标.管理员_考生信息, ViewData, pageTitle);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Admin/User/UCExaminee.ascx", 考生PagedList);
            }

            /* 返回考生信息视图 */
            return View("~/Views/Admin/User/Examinee.aspx", 考生PagedList);

        }

        /// <summary>
        /// 考官信息列表
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult examiner(int? id)
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            /* 考官信息视图标题 */
            string pageTitle = LKExamEnvironment.考官名称 + "信息";

            /* 考官PagedList */
            PagedList<考官> 考官PagedList = 考官信息列表(id);

            /* 信息视图 */
            LKExamMvcPagerData<考官>.数据信息(考官PagedList, MvcPager引用目标.管理员_考官信息, ViewData, pageTitle);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Admin/User/UCExaminer.ascx", 考官PagedList);
            }

            /* 返回考信息视图 */
            return View("~/Views/Admin/User/Examiner.aspx", 考官PagedList);
        }

        /// <summary>
        /// 导入导出考生Excel信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Imexport()
        {
            /* 操作类型 */
            string 操作类型 = LKExamURLQueryKey.GetString("handleType");

            /*
              *操作类型
              *  (1)1表示导出考生Excel
              *  (2)2表示查询考生信息
              */
            if (操作类型 == "1")
            {
                Guid 部门ID = LKExamURLQueryKey.部门ID();
                string 部门名称;
                DataSet dataSet = 考生.查询导出考生(部门ID, out 部门名称);
                string sFileName = 部门名称 + "(" + LKExamEnvironment.考生名称 + "信息)";
                new LKExamOffice().导出考生信息到Execl(dataSet, sFileName);
            }

            /* 导入导出考生Excel信息视图标题 */
            string pageTitle = "导入导出" + LKExamEnvironment.考生名称 + "Excel信息";

            /* 考生PagedList */
            PagedList<考生> 考生PagedList = 考生Excel列表();

            /* 信息视图 */
            LKExamMvcPagerData<考生>.数据信息(考生PagedList, MvcPager引用目标.管理员_导出考生Excel, ViewData, pageTitle);

            /* 返回考生信息视图 */
            return View("~/Views/Admin/User/Imexport.aspx", 考生PagedList);
        }

        /// <summary>
        /// 导入考生Excel
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void ImportExcel()
        {
            JsonResult jsonResult = null;
            try
            {
                /* 客户端上载文件 */
                HttpPostedFileBase postFile = Request.Files["InputFile"];

                /* 获取文件扩展名 */
                string ext = System.IO.Path.GetExtension(postFile.FileName);

                /* 如果扩展名不符合规定格式 则抛出异常 */
                if (ext != ".xls" && ext != ".xlsx")
                {
                    throw new Exception("Excel文件格式不正确，请上传xls,xlsx格式的Excel");
                }

                /* 导入考生Excel */
                List<string> 已存在学号集合 = 导入Excel.导入excel(postFile.InputStream, LKExamEnvironment.是否为学校 ? 0 : 1);
                object info = new { 已存在编号数组 = 已存在学号集合 };

                /* 成功输出JsonResult */
                jsonResult = LKPageJsonResult.Success(info);
                
                /* 释放流占的资源 */
                postFile.InputStream.Dispose();
                postFile.InputStream.Close();
                postFile = null;
            }
            catch (Exception ex)
            {
                jsonResult = LKPageJsonResult.Exception(ex);
            }
            /* 对象转化为Json格式字符串 */
            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(jsonResult.Data);

            /* 将Json格式字符串输出到页面 */
            Response.Write(jsonText);
        }
    }
}

