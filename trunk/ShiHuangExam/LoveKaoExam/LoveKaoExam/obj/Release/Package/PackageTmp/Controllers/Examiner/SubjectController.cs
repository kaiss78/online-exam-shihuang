using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using LoveKaoExam.Data;
using LoveKaoExam.Library.CSharp;
using LoveKao.Page;

namespace LoveKaoExam.Controllers.Examiner
{
    [Authorize(Roles = "考官")]
    public class SubjectController : BaseController
    {
        /// <summary>
        /// 我要出题
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            /* 返回我要出题视图 */
            return View("~/Views/Examiner/Subject/Add.aspx");
        }

        /// <summary>
        /// 编辑试题
        /// (1)用于IFrame嵌套页面
        /// </summary>
        /// <returns></returns>
        public ActionResult IFrame()
        {
            /* 返回我要出题视图 */
            return View("~/Views/Examiner/Subject/IFrame.aspx");
        }

        /// <summary>
        /// 我的试题
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult MySubject(int? id)
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            /* 考官试题PagedList */
            PagedList<试题外部信息> 考官试题PagedList = 考官试题列表(id);

            /* 考官试题信息视图 */
            LKExamMvcPagerData<试题外部信息>.数据信息(考官试题PagedList, MvcPager引用目标.考官_我的试题, ViewData);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Examiner/Subject/UCMySubject.ascx", 考官试题PagedList);
            }

            /* 返回考官试题视图 */
            return View("~/Views/Examiner/Subject/MySubject.aspx", 考官试题PagedList);
        }

        /// <summary>
        /// 删除试题
        /// </summary>
        /// <param name="id">试题外部信息ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            #region try/catch(){}
            try
            {
                试题外部信息.删除试题(id);

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
    }
}
