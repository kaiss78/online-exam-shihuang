using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKaoExam.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.IO;
using LoveKaoExam.Models.Examiner;
using LoveKaoExam.Library.HTML;

namespace LoveKaoExam.Library.CSharp
{
    public class LKExamChart
    {
        /// <summary>
        /// 返回考试分析图形
        /// </summary>
        /// <returns></returns>
        public Chart Get考试分析图形(ExamReportModels examReportModels)
        {
            /* 考试报表选项 */
            ExamReportOptions examReportOptions = examReportModels.ExamReportOptions;

            /* 图形类型 */
            int chartType = examReportOptions.ChartType;

            /* list考试分析 */
            List<考试分析> list考试分析 = examReportModels.考试分析;

            Chart chart = null;

            #region 图形类型
            switch (chartType)
            {
                /* 样条线图形 */
                case 1:
                    chart = Get考试分析样条线图形(examReportModels);
                    break;

                /* 折线图形 */
                case 2:
                    chart = Get考试分析折线图形(examReportModels);
                    break;

                /* 圆环图形 */
                case 3:
                    chart = Get考试分析圆环图形(examReportModels);
                    break;

                /* 饼状图形 */
                case 4:
                    chart = Get考试分析饼状图形(examReportModels);
                    break;

                /* 默认图形-柱形图形 */
                default:
                    chart = Get考试分析柱形图形(examReportModels);
                    break;
            }
            #endregion

            return chart;
        }

        /// <summary>
        /// 返回考试分析柱形图
        /// </summary>
        /// <param name="examReportModels">考试报表Model</param>
        /// <returns></returns>
        public Chart Get考试分析柱形图形(ExamReportModels examReportModels)
        {
            return Get考试分析常用图形(examReportModels, SeriesChartType.Column);
        }

        /// <summary>
        /// 返回考试分析样条线图形
        /// </summary>
        /// <param name="examReportModels">考试报表Models</param>
        /// <returns></returns>
        public Chart Get考试分析样条线图形(ExamReportModels examReportModels)
        {
            return Get考试分析常用图形(examReportModels, SeriesChartType.Spline);
        }

        /// <summary>
        /// 返回考试分析折线图
        /// </summary>
        /// <param name="examReportModels">考试报表Models</param>
        /// <returns></returns>
        public Chart Get考试分析折线图形(ExamReportModels examReportModels)
        {
            return Get考试分析常用图形(examReportModels, SeriesChartType.Line);
        }

        /// <summary>
        /// 返回考试分析圆环图形
        /// </summary>
        /// <param name="examReportModels">考试报表Models</param>
        /// <returns></returns>
        public Chart Get考试分析圆环图形(ExamReportModels examReportModels)
        {
            return Get考试分析特殊图形(examReportModels, SeriesChartType.Doughnut);
        }

        /// <summary>
        /// 返回考试分析饼状图形
        /// </summary>
        /// <param name="examReportModels">考试报表Models</param>
        /// <returns></returns>
        public Chart Get考试分析饼状图形(ExamReportModels examReportModels)
        {
            return Get考试分析特殊图形(examReportModels, SeriesChartType.Pie);
        }

        /// <summary>
        /// 返回考试分析常用图形
        /// </summary>
        /// <param name="examReportModels">考试报表Models</param>
        /// <param name="seriesChartType">图形类型</param>
        /// <returns></returns>
        public Chart Get考试分析常用图形(ExamReportModels examReportModels, SeriesChartType seriesChartType)
        {
            /* 考试分析Chart */
            Chart chart = Get考试分析Chart(examReportModels);

            /* list考试分析 */
            List<考试分析> list考试分析 = examReportModels.考试分析;
            List<string> listX = list考试分析[0].间隔值列表;

            ChartArea chartArea = Get考试分析ChartArea();

            chartArea.AxisY.Title = "人数";
            chartArea.AxisY.TextOrientation = TextOrientation.Stacked;
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.LabelStyle.Font = new Font("Trebuchet MS", 8, FontStyle.Bold);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);

            chartArea.AxisX.Title = "分数段";
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LabelStyle.Font = new Font("Trebuchet MS", 8, FontStyle.Bold);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);

            chart.ChartAreas.Add(chartArea);

            //加旁边显示
            Legend legend = new Legend();
            legend.ShadowOffset = 3;
            legend.BackColor = Color.Transparent;
            legend.Font = new Font("Trebuchet MS", 8, FontStyle.Bold);
            chart.Legends.Add(legend);

            for (int i = 0; i < list考试分析.Count; i++)
            {
                Series series = new Series();
                series["PointWidth"] = "0.6";
                series["BarLabelStyle"] = "Center";
                series["DrawingStyle"] = "Cylinder";
                series.BorderWidth = 3;
                series.ShadowOffset = 2;
                series.Name = list考试分析[i].部门名;
                series.ChartType = seriesChartType;
                series.IsValueShownAsLabel = true;
                series.Points.DataBindXY(listX, list考试分析[i].人数列表);
                chart.Series.Add(series);
            }

            return chart;
        }

        /// <summary>
        /// 返回考试分析特殊图形
        /// </summary>
        /// <param name="examReportModels">考试报表Models</param>
        /// <param name="seriesChartType">图形类型</param>
        /// <returns></returns>
        public Chart Get考试分析特殊图形(ExamReportModels examReportModels, SeriesChartType seriesChartType)
        {
            /* 考试分析Chart */
            Chart chart = Get考试分析Chart(examReportModels);

            /* list考试分析 */
            List<考试分析> list考试分析 = examReportModels.考试分析;
            List<string> listX = list考试分析[0].间隔值列表;

            for (int i = 0; i < list考试分析.Count; i++)
            {
                ChartArea chartArea = Get考试分析ChartArea();
                List<int> listY = list考试分析[i].人数列表;
                //chartArea.Area3DStyle.Enable3D = true;
                chart.ChartAreas.Add(chartArea);

                Legend legend = new Legend();
                legend.Title = "分数段";
                legend.BackColor = Color.Transparent;
                legend.Font = new Font("Trebuchet MS", 8, FontStyle.Bold);
                legend.DockedToChartArea = chartArea.Name;
                legend.Enabled = true;
                legend.ShadowOffset = 3;
                chart.Legends.Add(legend);

                Series series = new Series();
                series.BorderWidth = 3;
                series.ShadowOffset = 2;
                series.Legend = legend.Name;
                series.Label = "#PERCENT{P1}";
                series.ChartType = seriesChartType;

                series["PieDrawingStyle"] = "SoftEdge";
                series.ChartArea = chartArea.Name;
                for (int k = 0; k < listX.Count; k++)
                {
                    DataPoint point = new DataPoint();
                    point.LegendText = listX[k];
                    point.YValues[0] = listY[k];
                    series.Points.Add(point);
                }
                chart.Series.Add(series);
            }

            return chart;
        }

        /// <summary>
        /// 返回考试分析Chart
        /// </summary>
        /// <param name="examReportModels">考试报表Models</param>
        /// <returns></returns>
        public Chart Get考试分析Chart(ExamReportModels examReportModels)
        {
            /* 考试报表选项 */
            ExamReportOptions examReportOptions = examReportModels.ExamReportOptions;

            /* 考试设置 */
            考试设置 c考试设置 = examReportModels.考试设置;

            /* list考试分析  */
            List<考试分析> list考试分析 = examReportModels.考试分析;

            /* 标题 */
            string sTitle = c考试设置.试卷内容.名称;
            if (examReportModels.DepartmentID != Guid.Empty)
            {
                sTitle += "(" + list考试分析[0].部门名 + ")";
            }

            /* 考试分析图表 */
            Chart chart = new System.Web.UI.DataVisualization.Charting.Chart();

            Title title = new Title();
            title.Text = sTitle;
            title.ToolTip = sTitle;
            title.Font = new Font("Trebuchet MS", 15, FontStyle.Bold);
            title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            title.ShadowOffset = 3;
            title.ForeColor = Color.FromArgb(26, 59, 105);
            chart.Titles.Add(title);

            chart.Width = examReportModels.ISWord版 ? examReportOptions.WidthWord版 : examReportOptions.Width;
            chart.Height = examReportOptions.Height;
            chart.ImageType = ChartImageType.Png;
            chart.RenderType = RenderType.ImageTag;
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BackColor = Color.FromArgb(211, 223, 240);
            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BorderWidth = 2;
            chart.BorderColor = Color.FromArgb(26, 59, 105);
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            return chart;
        }

        /// <summary>
        /// 返回考试分析ChartArea
        /// </summary>
        /// <returns></returns>
        public ChartArea Get考试分析ChartArea()
        {
            ChartArea chartArea = new ChartArea();
            chartArea.BackGradientStyle = GradientStyle.TopBottom;
            chartArea.Area3DStyle.IsRightAngleAxes = true;
            chartArea.BorderWidth = 1;
            chartArea.BorderColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.BackSecondaryColor = Color.White;
            chartArea.BackColor = Color.FromArgb(64, 165, 191, 228);
            chartArea.ShadowColor = Color.Transparent;
            return chartArea;
        }

        /// <summary>
        /// 返回考试分析FileResul
        /// </summary>
        /// <param name="chart">考试分析Chart</param>
        /// <returns></returns>
        public FileResult Get考试分析FileResult(Chart chart)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                chart.SaveImage(ms, ChartImageFormat.Png);
                ms.Position = 0;
                return new FileStreamResult(ms, "image/png");
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                ms = null;
                chart.Dispose();
                chart = null;
            }
        }

        /// <summary>
        /// 保存考试分析Chart
        /// </summary>
        /// <param name="chart"></param>
        public void Save考试分析Chart(ExamReportModels examReportModels)
        {
            Guid g考试设置ID = examReportModels.考试设置.ID;
            Chart chart = Get考试分析图形(examReportModels);

            #region Directory
            //web请求
            HttpServerUtility httpServerUtility = System.Web.HttpContext.Current.Server;

            string directory = "/Content/File/ExamChart/";

            //新图片路径对应的目录不存在
            if (!Directory.Exists(httpServerUtility.MapPath(directory)))
            {
                //按虚拟路径创建所有目录和子目录
                Directory.CreateDirectory(@httpServerUtility.MapPath(directory));
            }
            #endregion

            string filePath = AnalysisExtensions.ExamReport图形文件虚拟路径(true, g考试设置ID);
            chart.SaveImage(@httpServerUtility.MapPath(filePath), ChartImageFormat.Png);
            chart.Dispose();
            chart = null;
        }

        /// <summary>
        /// 删除考试分析Chart
        /// </summary>
        /// <param name="g考试设置ID"></param>
        public void Delete考试分析Chart(Guid g考试设置ID)
        {
            try
            {
                string filePath = AnalysisExtensions.ExamReport图形文件虚拟路径(true, g考试设置ID);

                //web请求
                HttpServerUtility httpServerUtility = System.Web.HttpContext.Current.Server;
                
                /* 文件已存在 */
                if (File.Exists(httpServerUtility.MapPath(filePath)))
                {
                    File.Delete(@httpServerUtility.MapPath(filePath));
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}