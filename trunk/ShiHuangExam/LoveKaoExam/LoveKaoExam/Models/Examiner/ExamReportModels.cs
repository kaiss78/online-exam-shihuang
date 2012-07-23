using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoveKaoExam.Data;
using System.Data;
using LoveKaoExam.Library.CSharp;
using System.Web.Mvc;
using LoveKaoExam.Library.HTML;
using LoveKao.Page;

namespace LoveKaoExam.Models.Examiner
{
    /// <summary>
    /// 考试报表选项
    /// </summary>
    public class ExamReportOptions
    {
        public ExamReportOptions()
        {
            ChartType = 0;
            ScoreSection = 10;
            Width = 900;
            Height = 400;
        }
        public ExamReportOptions(ExamReportOptions examReportOptions)
        {
            int chartTypeCount = AnalysisSelect.Dictionary图形类型.Count;
            ChartType = LKPageRetainOrReplace.GetInt32(examReportOptions.ChartType, 0, chartTypeCount);
            ScoreSection = LKPageRetainOrReplace.GetInt32(examReportOptions.ScoreSection, 5, 60);
            Width = LKPageRetainOrReplace.GetInt32(examReportOptions.Width, 600, 1000);
            Height = LKPageRetainOrReplace.GetInt32(examReportOptions.Height, 200, 400);
        }

        /// <summary>
        /// 图形类型
        /// <para>(1)0-柱形图(默认图形)</para>
        /// <para>(2)1-折线图</para>
        /// <para>(2)2-饼状图</para>
        /// <para>(3)3-样条线</para>
        /// </summary>
        public int ChartType { get; set; }

        /// <summary>
        /// 分数段
        /// <para>(1)默认10</para>
        /// <para>(2)最小5</para>
        /// <para>(3)最大60</para>
        /// </summary>
        public int ScoreSection { get; set; }

        /// <summary>
        /// 图形宽度
        /// <para>(1)默认1000</para>
        /// <para>(2)最小600</para>
        /// <para>(3)最大1000</para>
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 图形宽度(WidthWord版)
        /// <para>(1)默认600</para>
        /// </summary>
        public int WidthWord版 { get { return 600; } }

        /// <summary>
        /// 图形高度
        /// <para>(1)默认400</para>
        /// <para>(2)最小200</para>
        /// <para>(3)最大400</para>
        /// </summary>
        public int Height { get; set; }
    }

    /// <summary>
    /// 考试报表
    /// </summary>
    public class ExamReportModels
    {
        /// <summary>
        /// 考试设置
        /// </summary>
        public 考试设置 考试设置 { get; set; }
        
        /// <summary>
        /// List<考试分析>
        /// </summary>
        public List<考试分析> 考试分析 { get; set; }
        
        /// <summary>
        /// DataTable
        /// </summary>
        public DataTable DataTable { get; set; }
        
        /// <summary>
        /// 考试设置ID
        /// </summary>
        public Guid TestID { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// 考试报表选项
        /// </summary>
        public ExamReportOptions ExamReportOptions { get; set; }

        /// <summary>
        /// 是否用于Word
        /// </summary>
        public bool ISWord版 { get; set; }
    }
}