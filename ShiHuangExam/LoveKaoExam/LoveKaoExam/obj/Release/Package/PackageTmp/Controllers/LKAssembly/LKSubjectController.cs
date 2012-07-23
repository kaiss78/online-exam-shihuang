using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKaoExam.Library.CSharp;
using LoveKaoExam.Data;
using LoveKao.Page;

namespace LoveKaoExam.Controllers.LKAssembly
{
    [Authorize(Roles = "考官")]
    public class LKSubjectController : Controller
    {
        #region ActionResult
        /// <summary>
        /// 编辑试题
        /// </summary>
        /// <returns></returns>
        public ActionResult EditSubject()
        {

            return View("~/Views/Examiner/Subject/EditSubject.aspx");
        }

        /// <summary>
        /// 试题编辑器Fckeditor
        /// </summary>
        /// <returns></returns>
        public ActionResult Fckeditor()
        {
            return View("~/Views/LKAssembly/IFrame/Fckeditor.aspx");
        }

        /// <summary>
        /// 试题图片管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SubjectImages()
        {
            return View("~/Views/LKAssembly/IFrame/SubjectImages.aspx");
        }

        /// <summary>
        /// 试题题型
        /// </summary>
        /// <returns></returns>
        public ActionResult QuestionTypes()
        {
            return View("~/Views/LKAssembly/IFrame/QuestionTypes.htm");
        }

        /// <summary>
        /// 复合题的子题型
        /// </summary>
        /// <returns></returns>
        public ActionResult ChildQuestionTypes()
        {
            return View("~/Views/LKAssembly/IFrame/ChildQuestionTypes.htm");
        }

        /// <summary>
        /// 试题智能识别
        /// </summary>
        /// <returns></returns>
        public ActionResult SubjectRecog()
        {
            return View("~/Views/LKAssembly/IFrame/SubjectRecog.htm");
        }

        /// <summary>
        /// 试题预览视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SubjectView()
        {
            return View("~/Views/LKAssembly/IFrame/SubjectView.htm");
        }

        #endregion

        #region JsonResult
        /// <summary>
        /// 新增试题
        /// (1)完整试题
        /// (2)草稿试题
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult NewSubject()
        {
            try
            {
                string sJsonText = LKExamURLQueryKey.GetString("text");
                试题外部信息.保存试题相关信息(sJsonText);
                return LKPageJsonResult.GetData(new { result = true, type = "subject", info = "" });
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 新增完整试题
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult NewNormal()
        {
            return NewSubject();
        }

        /// <summary>
        /// 新增草稿试题
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult NewDraft()
        {
            return NewSubject();
        }

        /// <summary>
        /// 修改完整试题
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateNormal()
        {
            try
            {
                string sJsonText = LKExamURLQueryKey.GetString("text");
                试题外部信息.修改正常试题(sJsonText);
                return LKPageJsonResult.GetData(new { result = true, type = "subject", info = "" });
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 修改草稿试题
        /// (1)修改草稿为完整试题
        /// (2)修改草稿为草稿试题
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateDraft()
        {
            try
            {
                string sJsonText = LKExamURLQueryKey.GetString("text");
                试题外部信息.修改草稿试题(sJsonText);
                return LKPageJsonResult.GetData(new { result = true, type = "subject", info = "" });
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 修改草稿为完整试题
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateDraftToNormal()
        {
            return UpdateDraft();
        }

        /// <summary>
        /// 修改草稿为草稿试题
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateDraftToDraft()
        {
            return UpdateDraft();
        }

        /// <summary>
        /// 返回试题信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSubject()
        {
            try
            {
                Guid g试题外部信息ID = LKExamURLQueryKey.GetGuid("guid");
                试题外部信息 outside = 试题外部信息.得到符合ID的试题(g试题外部信息ID);
                string sJsonText = 试题内容.转化成完整Json字符串(outside.当前试题内容, outside);
                return LKPageJsonResult.Success(sJsonText);
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 返回试题信息
        /// (1)不带答案与解析
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult WithoutAnAndRe(Guid id) {
            try
            {
                试题外部信息 outside = 试题外部信息.得到符合ID的试题(id);
                string sJsonText = 试题内容.转化成预览试题Json字符串不带答案(outside.当前试题内容, outside);
                return LKPageJsonResult.Success(sJsonText);
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }

        #endregion
    }
}
