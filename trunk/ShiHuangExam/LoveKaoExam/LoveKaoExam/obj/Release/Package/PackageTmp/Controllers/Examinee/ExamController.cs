using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LoveKaoExam.Data;
using LoveKaoExam.Library.CSharp;
using Webdiyer.WebControls.Mvc;
using LoveKaoExam.Models.Examinee;
using LoveKao.Page;

namespace LoveKaoExam.Controllers.Examinee
{
    /// <summary> 
    /// 考生控制器
    /// </summary>
    [Authorize(Roles = "考生")] 
    public  class ExamController : BaseController
    {

        /// <summary>
        /// 在线考试
        /// </summary>
        /// <returns></returns>
        public ActionResult OnlineExam()
        {
            return View("~/Views/Examinee/Exam/OnlineExam.aspx");
        }   
        /// <summary>
        /// 考试记录
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult LogExam(int? id)
        {
            /* 考试记录PagedList */
            PagedList<考生做过的试卷> 考试记录PagedList = 考生考试记录列表(id);

            /* 考试记录信息视图 */
            LKExamMvcPagerData<考生做过的试卷>.数据信息(考试记录PagedList, MvcPager引用目标.考生_考试记录, ViewData);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Examinee/Exam/UCLogExam.ascx", 考试记录PagedList);
            }

            /* 返回考试记录视图 */
            return View("~/Views/Examinee/Exam/LogExam.aspx", 考试记录PagedList);
        }

        /// <summary>
        /// 练习记录
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult LogPractice(int? id)
        {
            /* 练习记录PagedList */
            PagedList<考生做过的试卷> 练习记录PagedList = 考生练习记录列表(id);

            /* 练习记录信息视图 */
            LKExamMvcPagerData<考生做过的试卷>.数据信息(练习记录PagedList, MvcPager引用目标.考生_练习记录, ViewData);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Examinee/Exam/UCLogPractice.ascx", 练习记录PagedList);
            }

            /* 返回练习记录视图 */
            return View("~/Views/Examinee/Exam/LogPractice.aspx", 练习记录PagedList);
        }

        /// <summary>
        /// 我要考试
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult SelectExam(int? id)
        {
            /*
             *	考试列表PagedList
             *--------------------*/
            PagedList<考试设置> 考试列表PagedList = 考生选择考试列表(id);

            /*
             *	考试列表信息视图
             *--------------------*/
            LKExamMvcPagerData<考试设置>.数据信息(考试列表PagedList, MvcPager引用目标.考生_我要考试, ViewData);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Examinee/Exam/UCSelectExam.ascx", 考试列表PagedList);
            }

            /*
             *	返回考试列表视图
             *--------------------*/
            return View("~/Views/Examinee/Exam/SelectExam.aspx", 考试列表PagedList);
        }
        /// <summary>
        /// 我要练习
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult SelectPractice(int? id)
        {
            /* 练习列表PagedList */
            PagedList<练习设置> 练习列表PagedList = 考生选择练习列表(id);

            /* 练习列表信息视图 */
            LKExamMvcPagerData<练习设置>.数据信息(练习列表PagedList, MvcPager引用目标.考生_我要练习, ViewData);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Examinee/Exam/UCSelectPractice.ascx", 练习列表PagedList);
            }

            /* 返回练习列表视图 */
            return View("~/Views/Examinee/Exam/SelectPractice.aspx", 练习列表PagedList);
        }

        /// <summary>
        /// 成绩排行
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult Rank(int? id)
        {
            考试排行Models c考试排行Models = 考生考试排名(id, LKExamURLQueryKey.GetGuid("ExamSetID"));

            /* 练习列表信息视图 */
            LKExamMvcPagerData<考试排名>.数据信息(c考试排行Models.PagedList考试排名, MvcPager引用目标.考生_考试排行榜, ViewData);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Examinee/Exam/UCRank.ascx", c考试排行Models);
            }

            /* 返回考试排名视图 */
            return View("~/Views/Examinee/Exam/Rank.aspx", c考试排行Models);
        }

        /// <summary>
        /// 保存考试
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult SaveExam()
        {
            try
            {
                string _testJson = LKExamURLQueryKey.GetString("textJson");
                if (!string.IsNullOrEmpty(_testJson))
                {
                    考生考试回答.提交保存试卷(_testJson);
                    return LKPageJsonResult.Success();

                }
                else
                {
                    return LKPageJsonResult.Failure();
                }
            }
            catch (Exception ex)
            {

                return LKPageJsonResult.Exception(ex);
            }

        }
        /// <summary>
        /// 得到考试
        /// </summary>
        /// <returns></returns>
        public JsonResult GetExam()
        {
            try
            {
                string strGuid = LKExamURLQueryKey.GetString("guid");
                byte type = Convert.ToByte(LKExamURLQueryKey.GetString("type"));
                if (type != 0 && type != 1)
                {
                    return LKPageJsonResult.Failure();
                }
                if (!string.IsNullOrEmpty(strGuid))
                {
                    string testText = 试卷外部信息.得到在线考试Json(new Guid(strGuid), type, UserInfo.CurrentUser.用户ID);
                    return LKPageJsonResult.Success(testText);
                }
                else
                {
                    return LKPageJsonResult.Failure();
                }
            }
            catch (Exception ex)
            {
               
                return LKPageJsonResult.Exception(ex);
            }

        }
        /// <summary>
        /// 学生的分析试卷
        /// </summary>
        /// <returns></returns>
        public JsonResult GetStuMadeExam()
        {
            return GetMadeExam();
        }
    }
}
