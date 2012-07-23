using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKaoExam.Library.CSharp;
using Webdiyer.WebControls.Mvc;
using LoveKaoExam.Data;
using System.Data;
using LoveKaoExam.Models.Examiner;
using System.Web.UI.DataVisualization.Charting;
using LoveKao.Page;

namespace LoveKaoExam.Controllers.Examiner
{
    [Authorize(Roles = "考官")]
    public class AnalysisController : BaseController
    {
        /// <summary>
        /// 考试结果
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult ExamResult(int? id)
        {
            /* 操作类型 */
            string 操作类型 = LKExamURLQueryKey.GetString("HandleType");

            bool iSExcel版 = 操作类型 == "2";

            /*
              *操作类型
              *  (1)1表示查询
              *  (2)2表示导出Excel
              */
            if (iSExcel版)
            {
                Guid g考试设置ID = LKExamURLQueryKey.试卷ID();
                Guid g部门ID = LKExamURLQueryKey.部门ID();
                考试设置 c考试设置 = null;
                string s部门名称 = null;
                DataSet dDataSet = 导出考试分析.得到导出考试分析列表(g考试设置ID, g部门ID, out c考试设置, out s部门名称);
                string sFileName = c考试设置.试卷内容.名称 + (string.IsNullOrEmpty(s部门名称) ? "" : "(" + s部门名称 + ")");
                new LKExamOffice().导出考试分析到Excel(dDataSet, sFileName);
            }

            ExamResultModels cExamResult = 考官考试结果列表(id);

            /* 已组织考试信息视图 */
            LKExamMvcPagerData<考生做过的试卷>.数据信息(cExamResult.考生做过的试卷, MvcPager引用目标.考官_考试结果列表, ViewData);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Examiner/Analysis/UCExamResult.ascx", cExamResult);
            }

            return View("~/Views/Examiner/Analysis/ExamResult.aspx", cExamResult);
        }

        /// <summary>
        /// 公布考试结果
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PublicExamResult(公布考试结果Models c公布考试结果Models)
        {
            try
            {
                考试设置.修改公布考试结果(c公布考试结果Models.考试设置ID, c公布考试结果Models.是否公布考试结果);
                return LKPageJsonResult.Success();
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 考试报表
        /// </summary>
        /// <returns></returns>
        public ActionResult ExamReport(ExamReportModels examReportModels, ExamReportOptions examReportOptions)
        {
             /* 操作类型 */
            string 操作类型 = LKExamURLQueryKey.GetString("HandleType");

            bool iSWord版 = 操作类型 == "2";

            #region 考试报表属性
            /* 考试设置ID */
            Guid g考试设置ID = examReportModels.TestID;

            /* 部门ID */
            Guid g部门ID = examReportModels.DepartmentID;
            
            /* 考试报表选项初始化值 */
            examReportOptions = new ExamReportOptions(examReportOptions);
            
            /* 考试报表选项 */
            examReportModels.ExamReportOptions = examReportOptions;

            /* 是否导出Word */
            examReportModels.ISWord版 = iSWord版;

            /* 分数段 */
            int i分数段 = examReportOptions.ScoreSection;
            #endregion

            #region 考试分析数据
            考试设置 c考试设置 = null;
            List<考试分析> list考试分析 = 考试分析.得到某场考试分析数据(g考试设置ID, i分数段, g部门ID, out c考试设置);
            examReportModels.考试分析 = list考试分析;
            examReportModels.考试设置 = c考试设置;
            examReportModels.DataTable = 导出考试分析.得到考试分析信息(g考试设置ID, g部门ID);
            #endregion

            /*
              *操作类型
              *  (1)1表示查询
              *  (2)2表示导出Word
              */
            if (iSWord版)
            {
                string s部门名称 = "部门";
                string sFileName = c考试设置.试卷内容.名称 + (string.IsNullOrEmpty(s部门名称) ? "" : "(" + s部门名称 + ")");
                new LKExamOffice().导出考试报表到Word(examReportModels, sFileName);
            }
            else
            {
                #region 考试报表Models
                if (list考试分析 != null && list考试分析.Count != 0)
                {
                    /* 将考试报表Models存放在Session中 */
                    Session["考试报表Models"] = examReportModels;
                    Session.Timeout = 1;
                }
                #endregion
            }

            return View("~/Views/Examiner/Analysis/ExamReport.aspx", examReportModels);
        }

        /// <summary>
        /// 返回考试分析图
        /// </summary>
        /// <returns></returns>
        public FileResult ExamChart()
        {
            string sKey="考试报表Models";
            Chart chart = null;
            try
            {
                /* 返回存在Session中的考试报表Models */
                ExamReportModels examReportModels = (ExamReportModels)Session[sKey];

                LKExamChart lKChart = new LKExamChart();
                chart = lKChart.Get考试分析图形(examReportModels);
                return lKChart.Get考试分析FileResult(chart);
            }
            catch (Exception)
            {
                return null;
            }
            finally {
                chart.Dispose();
                chart = null;
                Session[sKey] = null;
                Session.Clear();
            }
        }

    }
}
