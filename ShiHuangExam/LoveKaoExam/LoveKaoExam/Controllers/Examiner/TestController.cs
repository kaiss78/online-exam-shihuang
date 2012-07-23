using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKaoExam.Library.CSharp;
using Webdiyer.WebControls.Mvc;
using LoveKaoExam.Data;
using LoveKaoExam.Controllers.LKAssembly;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using LoveKao.Page; 

namespace LoveKaoExam.Controllers.Examiner
{
    [Authorize(Roles = "考官")]
    public class TestController : BaseController
    {

        /// <summary>
        /// 我要组卷(随机出卷)
        /// </summary>
        /// <returns></returns> 
        public ActionResult Add()
        {
            return View("~/Views/Examiner/Test/Add.aspx");
        }

        /// <summary>
        /// 手工出卷
        /// </summary>
        /// <returns></returns>
        public ActionResult EditExam() {
            return View("~/Views/Examiner/Test/EditExam.aspx");
        }
      
       
        /// <summary>
        /// 预览试卷
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewTest()
        {
            Guid g试卷内容ID = LKExamURLQueryKey.GetGuid("guid");
            int word = LKExamURLQueryKey.GetInt32("word");
            string key = LKExamURLQueryKey.GetString("key");
            试卷内容 c试卷内容 = 试卷内容.得到试卷内容带试题内容Json(g试卷内容ID, UserInfo.CurrentUser.用户ID);
            if (word == 1)
            { 
                string sHtmlTest = LKTestController.GetTestViewHTML(c试卷内容, key,true);
                string wordDiv = LKPageHtml.MvcTextTag_Div(sHtmlTest);
                new LKExamOffice().导出预览试卷到Word(wordDiv, c试卷内容.名称);
                
            }
            return View("~/Views/Examiner/Test/ViewTest.aspx", c试卷内容);
        }

        

        /// <summary>
        /// 我的试卷
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult MyPapers(int? id)
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            /* 考官试卷PagedList */
            PagedList<试卷外部信息> 考官试卷PagedList = 考官试卷列表(id);

            /* 考官试卷信息视图 */
            LKExamMvcPagerData<试卷外部信息>.数据信息(考官试卷PagedList, MvcPager引用目标.考官_我的试卷, ViewData);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
               
                return PartialView("~/Views/Examiner/Test/UCMyPapers.ascx", 考官试卷PagedList);
            }

            /* 返回考官试卷列表视图 */
            return View("~/Views/Examiner/Test/MyPapers.aspx", 考官试卷PagedList);
        }

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="id">试卷外部信息ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            #region try/catch(){}
            try
            {
                试卷外部信息.删除试卷(id);

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
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Mark()
        {
            return View("~/Views/Examinee/Exam/OnlineExam.aspx");
        }
    }
}

