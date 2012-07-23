using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.Mvc.Html;
using LoveKao.Page;
using LoveKaoExam.Models.Examiner;
using LoveKaoExam.Library.CSharp;
using LoveKaoExam.Data;
using LoveKaoExam.Models;

namespace LoveKaoExam.Library.HTML
{
    public class AnalysisSelect
    {
        #region 图形类型
        /// <summary>
        /// 图形类型
        /// </summary>
        private static Dictionary<string, string> dictionary图形类型;

        /// <summary>
        /// 图形类型
        /// </summary>
        public static Dictionary<string, string> Dictionary图形类型
        {
            get
            {
                if (dictionary图形类型 == null)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("0", "柱形图");
                    dictionary.Add("1", "样条线");
                    dictionary.Add("2", "折线图");
                    dictionary.Add("3", "圆环图");
                    dictionary.Add("4", "饼状图");

                    dictionary图形类型 = dictionary;
                }
                return dictionary图形类型;
            }
        }
        #endregion

        #region 分数段
        /// <summary>
        /// 分数段
        /// </summary>
        private static Dictionary<string, string> dictionary分数段;

        /// <summary>
        /// 分数段
        /// </summary>
        public static Dictionary<string, string> Dictionary分数段
        {
            get
            {
                if (dictionary分数段 == null)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("5", "5");
                    dictionary.Add("10", "10");
                    dictionary.Add("15", "15");
                    dictionary.Add("20", "20");
                    dictionary.Add("25", "25");
                    dictionary.Add("30", "30");
                    dictionary.Add("35", "35");
                    dictionary.Add("40", "40");
                    dictionary.Add("45", "45");
                    dictionary.Add("50", "50");
                    dictionary.Add("55", "55");
                    dictionary.Add("60", "60");
                    dictionary分数段 = dictionary;
                }
                return dictionary分数段;
            }
        }
        #endregion

        /// <summary>
        /// Dictionary转SelectList
        /// </summary>
        /// <param name="dictionary">dictionary</param>
        /// <param name="selectedValue">selectedValue</param>
        /// <returns></returns>
        public static SelectList DictionaryToSelectList(Dictionary<string, string> dictionary, object selectedValue)
        {
            List<SelectListItem> lSelectListItem = new List<SelectListItem>();
            foreach (var item in dictionary)
            {
                lSelectListItem.Add(new SelectListItem() { Value = item.Key, Text = item.Value });
            }
            return new SelectList(lSelectListItem, "Value", "Text", selectedValue);
        }

        /// <summary>
        /// SelectList图形类型
        /// </summary>
        public static SelectList SelectList图形类型
        {
            get
            {
                int selectedValue = LKExamURLQueryKey.GetInt32("ChartType");
                return DictionaryToSelectList(Dictionary图形类型, selectedValue);
            }
        }

        /// <summary>
        /// SelectList分数段
        /// </summary>
        public static SelectList SelectList分数段
        {
            get
            {
                int selectedValue = LKExamURLQueryKey.GetInt32("ScoreSection");
                return DictionaryToSelectList(Dictionary分数段, selectedValue);
            }
        }
    }

    public static class AnalysisExtensions
    {
        #region Get考试分析标题
        public static string Get考试分析标题(考试设置 c考试设置)
        {
            return "<div class=\"usebox examiner-analysis-head\">" +
                            "<h1>" + c考试设置.试卷内容.名称 + "</h1>" +
                            "<div class=\"detailsInfo\">" +
                                "<label>考试时长:</label>" +
                                "<span><font color=\"red\">" + c考试设置.考试时长 + "</font>分钟</span>" +
                                "<label>总分:</label>" +
                                "<span><font color=\"red\">" + c考试设置.试卷内容.总分 + "</font>分</span>" +
                            "</div>" +
                        "</div>";
        }
        #endregion

        #region Get考试分析表格

        /// <summary>
        /// 返回考试分析表格Head
        /// </summary>
        /// <param name="listX">间隔值</param>
        /// <returns></returns>
        public static string Get考试分析表格THead(List<string> listX)
        {
            /* 保留或替换listX */
            listX = LKPageRetainOrReplace.GetList<string>(listX);

            #region 表格头部
            string sTHead = "";
            sTHead += "<thead style=\"background:#f1f1f1;\">";
            sTHead += "<tr>";
            sTHead += "<td colSpan=\"2\">成绩分布</td>";
            for (int i = 0; i < listX.Count; i++)
            {
                sTHead += "<td>" + listX[i] + "</td>";
            }
            sTHead += "</tr>";
            sTHead += "</thead>";
            #endregion

            return sTHead;
        }

        /// <summary>
        /// 返回考试分析表格Body
        /// </summary>
        /// <param name="c考试分析">考试分析</param>
        /// <returns></returns>
        public static string Get考试分析表格TBody(考试分析 c考试分析)
        {
            List<int> listY = c考试分析.人数列表;

            if (listY == null || listY.Count == 0)
            {
                return "";
            }

            /* 考生范围数 */
            int Total = 0;
            string sTBody = "";

            #region 人数
            sTBody += "<tr>";
            sTBody += "<td rowSpan=\"2\">" + c考试分析.部门名 + "</td>";
            sTBody += "<td>人</td>";
            for (int i = 0; i < listY.Count; i++)
            {
                sTBody += "<td>" + listY[i] + "</td>";
                Total += listY[i];
            }
            sTBody += "</tr>";
            #endregion

            #region 百分比
            sTBody += "<tr style=\"background:#f1f1f1;\">";
            sTBody += "<td>%</td>";
            for (int i = 0; i < listY.Count; i++)
            {
                sTBody += "<td><font>" + (((double)listY[i] / Total) * 100) + "</font></td>";
            }
            sTBody += "</tr>";
            #endregion

            return sTBody;
        }

        /// <summary>
        /// 返回考试分析表格Table
        /// </summary>
        /// <param name="l考试分析">List<考试分析></param>
        /// <returns></returns>
        public static string Get考试分析表格Table(List<考试分析> l考试分析)
        {
            if (l考试分析 == null || l考试分析.Count == 0)
            {
                return "";
            }
            else
            {
                string sHead = Get考试分析表格THead(l考试分析[0].间隔值列表);
                string sBody = "";

                sBody += "<tbody>";
                /* 遍历考试分析 */
                for (int i = 0; i < l考试分析.Count; i++)
                {
                    sBody += Get考试分析表格TBody(l考试分析[i]);
                }
                sBody += "</tbody>";

                return "<table>" + sHead + sBody + "</table>";
            }
        }
        #endregion

        #region Get考试分析情况

        /// <summary>
        /// 返回考试分析情况集合
        /// </summary>
        private static List<List<string>> get考试分析情况集合;

        /// <summary>
        /// 返回考试分析情况集合
        /// </summary>
        public static List<List<string>> Get考试分析情况集合
        {
            get
            {
                if (get考试分析情况集合 == null)
                {
                    List<List<string>> list集合 = new List<List<string>>();
                    list集合.Add(new List<string>() { "规定考试日期：", "考试日期", "最高分数：", "最高分" });
                    list集合.Add(new List<string>() { "规定考试时间：", "考试时间", "最低分数：", "最低分" });
                    list集合.Add(new List<string>() { "应参考人数：", "应参考人数", "平均分数：", "平均分" });
                    list集合.Add(new List<string>() { "实参考人数：", "实参考人数", "平均用时：", "平均用时" });
                    get考试分析情况集合 = list集合;
                }
                return get考试分析情况集合;
            }
        }

        /// <summary>
        /// 返回考试分析情况TD
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="sMark">标识名称</param>
        /// <param name="sField">字段名称</param>
        /// <returns></returns>
        public static string Get考试分析情况TD(DataTable dataTable, string sMark, string sField)
        {
            return "<td class=\"mark\">" + sMark + "</td>" +
                       "<td>" +
                            "<div class=\"InputSpan\">" + dataTable.Rows[0][sField] + "</div>" +
                       "</td>";
        }

        /// <summary>
        /// 返回考试分析情况TR
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="list子集合">list子集合</param>
        /// <returns></returns>
        public static string Get考试分析情况TR(DataTable dataTable, List<string> list子集合, int index)
        {
            list子集合 = LKPageRetainOrReplace.GetList<string>(list子集合);
            if (list子集合.Count != 4)
            {
                return "";
            }
            string sTD = "";
            for (int i = 0; i < list子集合.Count; i += 2)
            {
                sTD += Get考试分析情况TD(dataTable, list子集合[i], list子集合[i + 1]);
            }
            string style = "";
            if (index % 2 == 1)
            {
                style = "style=\"background:#f1f1f1;\"";
            }
            return "<tr " + style + ">" + sTD + "</tr>";
        }

        /// <summary>
        /// 返回考试分析情况TBody
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <returns></returns>
        public static string Get考试分析情况TBody(DataTable dataTable)
        {
            List<List<string>> list集合 = LKPageRetainOrReplace.GetList<List<string>>(Get考试分析情况集合);
            string sTR = "";
            for (int i = 0; i < list集合.Count; i++)
            {
                sTR += Get考试分析情况TR(dataTable, list集合[i], i);
            }
            return "<tbody>" + sTR + "</tbody>";
        }

        /// <summary>
        /// 返回试分析情况Table
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <returns></returns>
        public static string Get考试分析情况Table(DataTable dataTable)
        {
            string sHead = "";
            string sBody = Get考试分析情况TBody(dataTable);
            return "<table>" + sHead + sBody + "</table>";
        }
        #endregion

        #region Get考试分析管理
        public static string Get考试分析管理()
        {
            //bool b环境为学校 = LKExamEnvironment.是否为学校;
            //string sInput = "<div class=\"InputSpan\"> </div>";
            //string sContent = "";
            //if (b环境为学校)
            //{
            //    sContent = "<label>任课教师：</label>" +
            //                                sInput +
            //                                "<label class=\"pl10\">教研室主任：</label>" +
            //                                sInput +
            //                                "<label class=\"pl10\">系主任：</label>" +
            //                                sInput;
            //}
            //else
            //{
            //    sContent = "<label>部门主管：</label>" +
            //                                sInput +
            //                                "<label class=\"pl10\">部门经理：</label>" +
            //                                sInput;
            //}
            //return sContent;
            return "";
        }
        #endregion

        #region ExamReport标题

        /// <summary>
        /// ExamReport标题
        /// </summary>
        /// <param name="c考试设置">考试设置</param>
        /// <returns></returns>
        public static string ExamReport标题(考试设置 c考试设置)
        {
            string sContent = "";
            if (c考试设置 != null)
            {
                sContent = Get考试分析标题(c考试设置); ;
            }
            return sContent;
        }

        /// <summary>
        /// ExamReport标题
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="c考试设置">考试设置</param>
        /// <returns></returns>
        public static MvcHtmlString ExamReport标题(this HtmlHelper htmlHelper, 考试设置 c考试设置)
        {
            string sContent = ExamReport标题(c考试设置);
            return MvcHtmlString.Create(sContent);
        }
        #endregion

        #region ExamReport图形
        /// <summary>
        /// ExamReport图形文件路径
        /// </summary>
        /// <param name="iSWord版">是否用于Word</param>
        /// <returns></returns>
        public static string ExamReport图形文件虚拟路径(bool iSWord版, Guid g考试设置ID)
        {
            string sFileName = "";
            if (iSWord版)
            {
                sFileName = "/Content/File/ExamChart/" + UserInfo.CurrentUser.用户名 + "-" + g考试设置ID + ".png";
                //sFileName = HttpContext.Current.Server.MapPath("/Content/File/ExamChart/" + UserInfo.CurrentUser.用户名 + "ExamChart.Png");
            }
            else
            {
                sFileName = "/Analysis/ExamChart/";
            }
            return sFileName;
        }

        /// <summary>
        /// ExamReport图形
        /// </summary>
        /// <param name="examReportModels">考试报表Models</param>
        /// <returns></returns>
        public static string ExamReport图形(ExamReportModels examReportModels)
        {
            string sContent = LKPageHtml.MvcTextTag(TagName.H3, LKExamEnvironment.考生名称 + "考试分析图表：");

            List<考试分析> list考试分析 = examReportModels.考试分析;
            if (list考试分析 != null && list考试分析.Count != 0)
            {
                /* 图表标题 */
                string sTitle = examReportModels.考试设置.试卷内容.名称;
                if (examReportModels.DepartmentID != Guid.Empty)
                {
                    sTitle += "(" + list考试分析[0].部门名 + ")";
                }

                Guid g考试设置ID = examReportModels.考试设置.ID;
                HttpRequest httpRequest = HttpContext.Current.Request;
                string sSrc = httpRequest.Url.Scheme + "://" + httpRequest.Url.Authority + ExamReport图形文件虚拟路径(examReportModels.ISWord版, g考试设置ID);

                /* 考试图表 */
                sContent += LKPageHtml.MvcTextTag(TagName.IMG, null, new { src = sSrc, alt = sTitle, title = sTitle });
            }
            else
            {
                /* 暂无考试分析 */
                sContent += LKPageHtml.MvcTextTag_Div("暂无分析图表", new { @class = "nodata" });
            }
            return LKPageHtml.MvcTextTag_Div(sContent);
        }

        /// <summary>
        /// ExamReport图形HTML版
        /// </summary>
        /// <param name="examReportModels">考试报表Models</param>
        /// <returns></returns>
        public static string ExamReport图形HTML版(ExamReportModels examReportModels)
        {
            examReportModels.ISWord版 = false;
            return ExamReport图形(examReportModels);
        }

        /// <summary>
        /// ExamReport图形WORD版
        /// </summary>
        /// <param name="examReportModels">考试报表Models</param>
        /// <returns></returns>
        public static string ExamReport图形WORD版(ExamReportModels examReportModels)
        {
            examReportModels.ISWord版 = true;
            return ExamReport图形(examReportModels);
        }

        /// <summary>
        /// ExamReport图形HTML版
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="examReportModels">考试报表Models</param>
        /// <returns></returns>
        public static MvcHtmlString ExamReport图形HTML版(this HtmlHelper htmlHelper, ExamReportModels examReportModels)
        {
            string sContent = ExamReport图形HTML版(examReportModels);
            return MvcHtmlString.Create(sContent);
        }
        #endregion

        #region ExamReport表格
        /// <summary>
        /// 考试报表表格
        /// </summary>
        /// <param name="list考试分析">list考试分析</param>
        /// <returns></returns>
        public static string ExamReport表格(List<考试分析> list考试分析)
        {
            string sContent = LKPageHtml.MvcTextTag(TagName.H3, LKExamEnvironment.考生名称 + "考试分析数据：");
            if (list考试分析 != null && list考试分析.Count != 0)
            {
                sContent += LKPageHtml.MvcTextTag_Div(Get考试分析表格Table(list考试分析), new { @class = "ladder" });
            }
            else
            {
                /* 暂无考试分析 */
                sContent += LKPageHtml.MvcTextTag_Div("暂无分析数据", new { @class = "nodata" });
            }
            return LKPageHtml.MvcTextTag_Div(sContent);
        }

        /// <summary>
        /// 考试报表表格
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="list考试分析">List<考试分析></param>
        /// <returns></returns>
        public static MvcHtmlString ExamReport表格(this HtmlHelper htmlHelper, List<考试分析> list考试分析)
        {
            string sContent = ExamReport表格(list考试分析);

            return MvcHtmlString.Create(sContent);
        }
        #endregion

        #region ExamReport情况
        /// <summary>
        /// 考试报表情况
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <returns></returns>
        public static string ExamReport情况(DataTable dataTable)
        {
            string sContent = LKPageHtml.MvcTextTag(TagName.H3, LKExamEnvironment.考生名称 + "考试情况：");
            if (dataTable != null && dataTable.Rows.Count != 0)
            {
                sContent += LKPageHtml.MvcTextTag_Div(Get考试分析情况Table(dataTable), new { @class = "intro" });
            }
            else
            {
                /* 暂无考试分析 */
                sContent += LKPageHtml.MvcTextTag_Div("暂无考试情况", new { @class = "nodata" });
            }

            return LKPageHtml.MvcTextTag_Div(sContent);
        }

        /// <summary>
        /// 考试报表情况
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="dataTable">DataTable</param>
        /// <returns></returns>
        public static MvcHtmlString ExamReport情况(this HtmlHelper htmlHelper, DataTable dataTable)
        {
            string sContent = ExamReport情况(dataTable);

            return MvcHtmlString.Create(sContent);
        }
        #endregion

        #region ExamReport管理
        /// <summary>
        /// ExamReport管理
        /// </summary>
        /// <param name="list考试分析">list考试分析</param>
        /// <returns></returns>
        public static string ExamReport管理(List<考试分析> list考试分析)
        {
            string sContent = "";
            if (list考试分析 != null && list考试分析.Count != 0)
            {
                sContent += LKPageHtml.MvcTextTag_Div(Get考试分析管理(), new { @class = "manage" });
            }

            return LKPageHtml.MvcTextTag_Div(sContent); ;
        }

        /// <summary>
        /// ExamReport管理
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <returns></returns>
        public static MvcHtmlString ExamReport管理(this HtmlHelper htmlHelper, List<考试分析> list考试分析)
        {
            string sContent = ExamReport管理(list考试分析);

            return MvcHtmlString.Create(sContent);
        }
        #endregion

        #region 考试分析Select
        /// <summary>
        /// ExamReportSelect图形类型
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <returns></returns>
        public static MvcHtmlString ExamReportSelect图形类型(this HtmlHelper htmlHelper)
        {
            return SelectExtensions.DropDownList(htmlHelper, "ChartType", AnalysisSelect.SelectList图形类型, "--请选择图形类型--");
        }

        /// <summary>
        /// ExamReportSelect分数段
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <returns></returns>
        public static MvcHtmlString ExamReportSelect分数段(this HtmlHelper htmlHelper)
        {
            return SelectExtensions.DropDownList(htmlHelper, "ScoreSection", AnalysisSelect.SelectList分数段, "--请选择分数段--");
        }
        #endregion
    }

    public static class ShareExtensions
    {
        #region 爱考网资源共享
        public static string 爱考网资源共享可下载试题总数(上传下载信息 c上传下载信息)
        {

            string font = LKPageHtml.MvcTextTag_Font(c上传下载信息.可下载试题数量, new { color = "red" });

            string b = LKPageHtml.MvcTextTag_B(font);

            return LKPageHtml.MvcTextTag_Span("可下载" + b + "道试题", new { style = "padding-left:10px;" });

        }
        public static string 爱考网资源共享已选择试题总数(爱考网资源类型 e爱考网资源类型)
        {
            if (e爱考网资源类型 == 爱考网资源类型.试题)
            {
                string font = LKPageHtml.MvcTextTag_Font(0, new { id = "资源中选择交互份数", color = "red" });

                string b = LKPageHtml.MvcTextTag_B(font);

                return LKPageHtml.MvcTextTag_Span("已选择" + b + "道试题", new { style = "padding-left:5px;" });
            }
            else
            {
                return "";
            }
        }
        public static string 爱考网资源共享已选择试卷总数(爱考网资源方式 e爱考网资源方式, 爱考网资源类型 e爱考网资源类型)
        {
            if (e爱考网资源类型 == 爱考网资源类型.试卷)
            {
                string 试卷Font = LKPageHtml.MvcTextTag_Font(0, new { id = "资源中选择交互份数", color = "red" });
                string 试卷B = LKPageHtml.MvcTextTag_B(试卷Font);

                string 试题Font = LKPageHtml.MvcTextTag_Font(0, new { id = "试卷中所有试题总数", color = "green" });
                string 试题B = LKPageHtml.MvcTextTag_B(试题Font);

                string 需资源Font = LKPageHtml.MvcTextTag_Font(0, new { id = "试卷中非有试题总数", color = "red" });
                string 需资源B = LKPageHtml.MvcTextTag_B(需资源Font);

                return LKPageHtml.MvcTextTag_Span("已选择" + 试卷B + "份试卷(共" + 试题B + "道试题，需" + e爱考网资源方式 + 需资源B + "道试题)", new { style = "padding-left:5px;" });
            }
            else
            {
                return "";
            }
        }
        public static string 爱考网资源共享已下载试题总数(上传下载信息 c上传下载信息, 爱考网资源方式 e爱考网资源方式, 爱考网资源类型 e爱考网资源类型)
        {
            if (e爱考网资源方式 == 爱考网资源方式.下载 && e爱考网资源类型 == 爱考网资源类型.试题)
            {
                string font = LKPageHtml.MvcTextTag_Font(c上传下载信息.已下载试题数量, new { color = "green" });

                string b = LKPageHtml.MvcTextTag_B(font);

                return LKPageHtml.MvcTextTag_Span("已下载" + b + "道试题", new { style = "padding-left:5px;" });
            }
            else
            {
                return "";
            }


        }
        public static string 爱考网资源共享已下载试卷总数(上传下载信息 c上传下载信息, 爱考网资源方式 e爱考网资源方式, 爱考网资源类型 e爱考网资源类型)
        {
            if (e爱考网资源方式 == 爱考网资源方式.下载 && e爱考网资源类型 == 爱考网资源类型.试卷)
            {
                string 试题Font = LKPageHtml.MvcTextTag_Font(c上传下载信息.已下载试题数量, new { color = "green" });
                string 试题B = LKPageHtml.MvcTextTag_B(试题Font);

                string 试卷Font = LKPageHtml.MvcTextTag_Font(c上传下载信息.已下载试卷数量, new { color = "green" });
                string 试卷B = LKPageHtml.MvcTextTag_B(试卷Font);
                string 试卷HTML = c上传下载信息.已下载试卷数量 == 0 ? "" : "(包含" + 试卷B + "份试卷)";
                return LKPageHtml.MvcTextTag_Span("已下载" + 试题B + "道试题" + 试卷HTML, new { style = "padding-left:5px;" });
            }
            else
            {
                return "";
            }
        }
        public static string 爱考网资源共享已上传试题总数(上传下载信息 c上传下载信息, 爱考网资源方式 e爱考网资源方式, 爱考网资源类型 e爱考网资源类型)
        {
            if (e爱考网资源方式 == 爱考网资源方式.上传 && e爱考网资源类型 == 爱考网资源类型.试题)
            {
                string font = LKPageHtml.MvcTextTag_Font(c上传下载信息.已上传试题数量, new { color = "green" });

                string b = LKPageHtml.MvcTextTag_B(font);

                return LKPageHtml.MvcTextTag_Span("已上传" + b + "道试题", new { style = "padding-left:5px;" });
            }
            else
            {
                return "";
            }
        }
        public static string 爱考网资源共享已上传试卷总数(上传下载信息 c上传下载信息, 爱考网资源方式 e爱考网资源方式, 爱考网资源类型 e爱考网资源类型)
        {
            if (e爱考网资源方式 == 爱考网资源方式.上传 && e爱考网资源类型 == 爱考网资源类型.试卷)
            {

                string 试题Font = LKPageHtml.MvcTextTag_Font(c上传下载信息.已上传试题数量, new { color = "green" });
                string 试题B = LKPageHtml.MvcTextTag_B(试题Font);

                string 试卷Font = LKPageHtml.MvcTextTag_Font(c上传下载信息.已上传试卷数量, new { color = "green" });
                string 试卷B = LKPageHtml.MvcTextTag_B(试卷Font);
                string 试卷HTML = c上传下载信息.已上传试卷数量 == 0 ? "" : "(包含" + 试卷B + "份试卷)";

                return LKPageHtml.MvcTextTag_Span("已上传" + 试题B + "道试题" + 试卷HTML, new { style = "padding-left:5px;" });
            }
            else
            {
                return "";
            }
        }
        public static MvcHtmlString 爱考网资源共享上传下载信息(this HtmlHelper htmlHelper, 上传下载信息 c上传下载信息, 爱考网资源方式 e爱考网资源方式, 爱考网资源类型 e爱考网资源类型)
        {
            string s已选择试题总数 = 爱考网资源共享已选择试题总数(e爱考网资源类型),
                   s已选择试卷总数 = 爱考网资源共享已选择试卷总数(e爱考网资源方式, e爱考网资源类型),
                   s已下载试题总数 = 爱考网资源共享已下载试题总数(c上传下载信息, e爱考网资源方式, e爱考网资源类型),
                   s已下载试卷总数 = 爱考网资源共享已下载试卷总数(c上传下载信息, e爱考网资源方式, e爱考网资源类型),
                   s已上传试题总数 = 爱考网资源共享已上传试题总数(c上传下载信息, e爱考网资源方式, e爱考网资源类型),
                   s已上传试卷总数 = 爱考网资源共享已上传试卷总数(c上传下载信息, e爱考网资源方式, e爱考网资源类型),
                   s可下载试题总数 = 爱考网资源共享可下载试题总数(c上传下载信息);

            string html = s已选择试题总数 + s已选择试卷总数 + s已下载试题总数 + s已下载试卷总数 + s已上传试题总数 + s已上传试卷总数 + s可下载试题总数;
            return MvcHtmlString.Create(html);
        }
        #endregion
    }

    public static class TestSetExtensions
    {
        public static string 试卷设置考生范围已选择考生总数()
        {
            string font = LKPageHtml.MvcTextTag_Font(0, new { id = "考生范围中选择交互总数", color = "red" });

            string b = LKPageHtml.MvcTextTag_B(font);

            return LKPageHtml.MvcTextTag_Span("已选择" + b + "位考生", new { style = "padding-left:5px;" });
        }
        public static MvcHtmlString 试卷设置考生范围总数(this HtmlHelper htmlHelper)
        {
            string s已选择考生总数 = 试卷设置考生范围已选择考生总数();
            return MvcHtmlString.Create(s已选择考生总数);
        }
    }
}