using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using LoveKaoExam.Models.Examiner;
using LoveKaoExam.Library.HTML;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using LoveKaoExam.Data;
using LoveKao.Page;


namespace LoveKaoExam.Library.CSharp
{
    public class LKExamOffice:LKPageOffice
    {
        /// <summary>
        /// DataSetToXLS，将DataSet数据源转为XLS格式
        /// </summary>
        /// <param name="dDataSet">DataSet格式数据源</param>
        /// <returns></returns>
        public string DataSetToXLS(DataSet dDataSet)
        {
            string sCaption = "";
            string s部门名称 = LKExamEnvironment.部门名称;
            string sHead = "", sBody = "";

            DataTable cDataTable = dDataSet.Tables[0];
            DataRow[] cDataRow = cDataTable.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的

            int iColumnsCount = cDataTable.Columns.Count;

            //取得数据表各列标题，各标题之间以\t分割，最后一个列标题后加回车符 
            for (int i = 0; i < iColumnsCount; i++)
            {
                sCaption = cDataTable.Columns[i].Caption.ToString();
                if (sCaption == "班级")
                {
                    sCaption = s部门名称;
                }
                if (sCaption == "性别名称")
                {
                    sCaption = "性别";
                }
                if (i == (iColumnsCount - 1))//最后一列，加\n
                {
                    sHead += sCaption + "\n";
                }
                else
                {
                    sHead += sCaption + "\t";
                }
            }

            //逐行处理数据   
            foreach (DataRow row in cDataRow)
            {
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据     
                for (int i = 0; i < iColumnsCount; i++)
                {
                    if (i == (iColumnsCount - 1))//最后一列，加\n
                    {
                        sBody += row[i].ToString() + "\n";
                    }
                    else
                    {
                        sBody += row[i].ToString() + "\t";
                    }
                }
            }
            return sHead + sBody;
        }
      
        /// <summary>
        /// 导出考生信息到Execl，将DataSet格式的考生信息导出到Execl文件
        /// </summary>
        /// <param name="dDataSet">DataSet格式数据源，考生信息</param>
        /// <param name="sFileName">文件名称，部门名称</param>
        public void 导出考生信息到Execl(DataSet dDataSet, string sFileName)
        {
            //Excel内容
            string sContent = DataSetToXLS(dDataSet);

            //导出Execl
            导出DataSet字符串到Execl(sContent, sFileName);
        }

        /// <summary>
        /// 导出考试分析到Excel，将DataSet格式的考试分析导出到Execl文件
        /// </summary>
        /// <param name="dDataSet">DataSet格式数据源，考试分析</param>
        /// <param name="sFileName">文件名称，考试名称</param>
        public void 导出考试分析到Excel(DataSet dDataSet, string sFileName)
        {
            //Excel内容
            string sContent = DataSetToXLS(dDataSet);

            //导出Execl
            导出DataSet字符串到Execl(sContent, sFileName);
        }

        /// <summary>
        /// 导出考试报表到Word，将ExamReportModels的考试报表导出到Word文件
        /// </summary>
        /// <param name="examReportModels">考试报表Models</param>
        /// <param name="sFileName">文件名称，考试名称</param>
        public void 导出考试报表到Word(ExamReportModels examReportModels, string sFileName)
        {
            LKExamChart lKChart = new LKExamChart();
            lKChart.Save考试分析Chart(examReportModels);

            HttpRequest httpRequest = HttpContext.Current.Request;
            string s内容 = "";
            s内容 += "<link href=\"" + httpRequest.Url.Scheme + "://" + httpRequest.Url.Authority + "/Content/StyleSheet/Global.css\" rel=\"stylesheet\" type=\"text/css\" />";
            s内容 += @"<style>
                        table{width:600px;}
                        tr{height:25px;}
                        td{text-align: center;border: #abc0db 1px solid;}
                        h1{font-size:20px;}
                        h3{font-size:14px;}
                        h1,div.detailsInfo{text-align: center;}
                        div.manage{margin-top:30px;}
                    </style>";
            s内容 += AnalysisExtensions.ExamReport标题(examReportModels.考试设置);
            s内容 += AnalysisExtensions.ExamReport图形WORD版(examReportModels);
            s内容 += AnalysisExtensions.ExamReport表格(examReportModels.考试分析);
            s内容 += AnalysisExtensions.ExamReport情况(examReportModels.DataTable);
            s内容 += AnalysisExtensions.ExamReport管理(examReportModels.考试分析);

            //导出Word
            导出BODY到WORD(s内容, sFileName);
        }

        /// <summary>
        /// 导出预览试卷到Word，将试卷的网页Body导出到Word文件
        /// </summary>
        /// <param name="sBody">内容</param>
        /// <param name="sFileName">文件名称</param>
        public void 导出预览试卷到Word(string sBody, string sFileName)
        {
            /* 客户端Http请求 */
            HttpRequest httpRequest = HttpContext.Current.Request;

            sBody = sBody.Replace("src=\"/UploadFiles", "src=\"" + httpRequest.Url.Scheme + "://" + httpRequest.Url.Authority + "/UploadFiles");
            sBody = Regex.Replace(sBody, @"<a class=""chakanyuanti""[^>]*?>\[原题\]</a>", "", RegexOptions.IgnoreCase);

            //导出Word
            导出BODY到WORD(sBody, sFileName);
        }
    }
}