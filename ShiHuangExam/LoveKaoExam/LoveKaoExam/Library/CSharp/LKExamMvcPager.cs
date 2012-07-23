using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoveKao.Page;
using Webdiyer.WebControls.Mvc;
using System.Web.Mvc;
using Fasterflect;

namespace LoveKaoExam.Library.CSharp
{

    /// <summary>
    /// 页面返回空数据的提示信息
    /// </summary>
    public class LKExamMvcPager空数据
    {
        /// <summary>
        /// 中文数字
        /// <para>(1)用于集合DD项前面的序号</para>
        /// </summary>
        public static List<string> 中文数字 = new List<string>() { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

        /// <summary>
        /// 集合附加序号
        /// <para>(1)为集合DD前面自动中文数字序号</para>
        /// </summary>
        /// <param name="l集合DD">集合DD</param>
        /// <returns></returns>
        public static List<string> 集合附加序号(List<string> l集合DD)
        {
            List<string> dd = new List<string>();
            if (l集合DD == null || l集合DD.Count == 0 || l集合DD.Count > 中文数字.Count)
            {
                return dd;
            }

            for (int i = 0; i < l集合DD.Count; i++)
            {
                dd.Add(中文数字[i] + "、" + l集合DD[i] + (i == l集合DD.Count - 1 ? " 。" : " ；"));
            }
            return dd;
        }

        #region 消息Dl
        public static string 消息Dl(string dt)
        {
            return 消息Dl(dt, new List<string>(), false);
        }
        public static string 消息Dl(string dt, bool isItemIcon)
        {
            return 消息Dl(dt, new List<string>(), isItemIcon);
        }
        public static string 消息Dl(string dt, List<string> dd)
        {
            return 消息Dl(dt, dd, false);
        }
        public static string 消息Dl(string dt, List<string> dd, bool isItemIcon)
        {
            string tagHtml = "";

            //dl-start
            tagHtml += "<dl id=\"EmptyData\">";

            //dt
            tagHtml += "<dt>" + dt + "</dt>";

            //dd
            dd = dd == null ? new List<string>() : dd;
            foreach (string d in dd)
            {
                tagHtml += "<dd class=" + (isItemIcon ? "itemIcon" : "itemText") + ">" + d + "</dd>";
            }

            //dl-end
            tagHtml += "</dl>";

            return tagHtml;
        }
        #endregion

        #region 查询方式_仅部门ID

        /// <summary>
        /// 查询方式_仅部门ID
        /// <para>(1)默认SDT</para>
        /// </summary>
        public static string 查询方式_仅部门ID_SDT
        {
            get
            {
                return "到目前还没有该 '" + LKPageHtml.MvcTextTag_B(LKExamEnvironment.部门名称) + "' 的相关数据。您可以尝试：";
            }
        }

        /// <summary>
        /// 查询方式_仅部门ID
        /// <para>(1)默认LDD</para>
        /// </summary>
        public static List<string> 查询方式_仅部门ID_LDD
        {
            get
            {
                List<string> dd = new List<string>();
                dd.Add("选择" + (LKExamURLQueryKey.包含部门ID() ? "其他" : "相关") + "‘" + LKExamEnvironment.部门名称 + "’");
                return dd;
            }
        }

        /// <summary>
        /// 查询方式_仅部门ID_集合
        /// <para>(1)为l集合DD附加序号</para>
        /// </summary>
        /// <param name="l集合DD">l集合DD</param>
        /// <returns></returns>
        public static string 查询方式_仅部门ID_集合(List<string> l集合DD)
        {
            return 查询方式_仅部门ID_集合(l集合DD, 查询方式_仅部门ID_SDT);
        }

        /// <summary>
        /// 查询方式_仅部门ID_集合
        /// <para>(1)为l集合DD附加序号</para>
        /// (2)替换默认SDT
        /// </summary>
        /// <param name="l集合DD"></param>
        /// <param name="sDT"></param>
        /// <returns></returns>
        public static string 查询方式_仅部门ID_集合(List<string> l集合DD, string sDT)
        {
            return 消息Dl(sDT, 集合附加序号(l集合DD), false);
        }

        /// <summary>
        /// 查询方式_仅部门ID
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加一条消息</para>
        /// </summary>
        /// <param name="s附加消息"></param>
        /// <returns></returns>
        public static string 查询方式_仅部门ID_消息(string s附加消息)
        {
            List<string> l集合DD = 查询方式_仅部门ID_LDD;
            if (!string.IsNullOrEmpty(s附加消息))
            {
                l集合DD.Add(s附加消息);
            }
            return 查询方式_仅部门ID_集合(l集合DD);
        }

        /// <summary>
        /// 查询方式_仅部门ID
        /// <para>(1)只显示默认消息</para>
        /// </summary>
        /// <returns></returns>
        public static string 查询方式_仅部门ID()
        {
            return 查询方式_仅部门ID_消息((string)null);
        }

        /// <summary>
        /// 查询方式_仅部门ID
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加操作消息</para>
        /// </summary>
        /// <param name="sTagAHTML">标签A的HTML</param>
        /// <returns></returns>
        public static string 查询方式_仅部门ID_操作(string sTagAHTML)
        {
            string s附加消息 = null;
            if (!string.IsNullOrEmpty(sTagAHTML))
            {
                s附加消息 = "点击这里，我要 " + sTagAHTML;
            }
            return 查询方式_仅部门ID_消息(s附加消息);
        }

        /// <summary>
        /// 查询方式_仅部门ID
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加操作集合DD</para>
        /// </summary>
        /// <param name="l操作集合DD">l操作集合DD</param>
        /// <returns></returns>
        public static string 查询方式_仅部门ID_操作(List<string> l操作集合DD)
        {
            List<string> l集合DD = 查询方式_仅部门ID_LDD;
            l集合DD.AddRange(l操作集合DD);
            return 查询方式_仅部门ID_集合(l集合DD);
        }

        #endregion

        #region 查询中转_仅部门ID
        public static string 查询中转_仅部门ID_操作(string s操作名称, string sTagAHTML)
        {
            bool b查询方式_部门ID = LKExamURLQueryKey.包含部门ID();
            if (LKExamURLQueryKey.包含部门ID())
            {
                return 查询方式_仅部门ID_操作(sTagAHTML);
            }
            else
            {
                return 查询方式_全部显示_操作或消息(s操作名称, sTagAHTML);
            }
        }
        public static string 查询中转_仅部门ID_消息(string s操作名称, string s附加消息)
        {
            if (LKExamURLQueryKey.包含部门ID())
            {
                return 查询方式_仅部门ID_消息(s附加消息);
            }
            else
            {
                return 查询方式_全部显示_操作或消息(s操作名称, s附加消息);
            }
        }
        #endregion

        #region 查询方式_仅关键字

        /// <summary>
        /// 查询方式_仅关键字
        /// <para>(1)默认SDT</para>
        /// </summary>
        public static string 查询方式_仅关键字_SDT
        {
            get
            {
                return "没有搜索到您想查询的信息。您可以尝试：";
            }
        }

        /// <summary>
        /// 查询方式_仅关键字
        /// <para>(1)默认LDD</para>
        /// </summary>
        public static List<string> 查询方式_仅关键字_LDD
        {
            get
            {
                List<string> dd = new List<string>();
                dd.Add("看看输入的文字是否有错");
                dd.Add("去掉不必要的词，如“的”、“吧”等");
                return dd;
            }
        }

        /// <summary>
        /// 查询方式_仅关键字_集合
        /// <para>(1)为l集合DD附加序号</para>
        /// </summary>
        /// <param name="l集合">l集合DD</param>
        /// <returns></returns>
        public static string 查询方式_仅关键字_集合(List<string> l集合DD)
        {
            return 消息Dl(查询方式_仅关键字_SDT, 集合附加序号(l集合DD), false);
        }

        /// <summary>
        /// 查询方式_仅关键字_消息
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加一条消息</para>
        /// </summary>
        /// <param name="s附加消息">附加消息</param>
        /// <returns></returns>
        public static string 查询方式_仅关键字_消息(string s附加消息)
        {
            List<string> l集合DD = 查询方式_仅关键字_LDD;
            if (!string.IsNullOrEmpty(s附加消息))
            {
                l集合DD.Add(s附加消息);
            }
            return 查询方式_仅关键字_集合(l集合DD);
        }

        /// <summary>
        /// 查询方式_仅关键字
        /// <para>(1)只显示默认消息</para>
        /// </summary>
        /// <returns></returns>
        public static string 查询方式_仅关键字()
        {
            return 查询方式_仅关键字_消息((string)null);
        }

        /// <summary>
        /// 查询方式_仅关键字操作
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加操作消息</para>
        /// </summary>
        /// <param name="sTagAHTML">标签A的HTML</param>
        /// <returns></returns>
        public static string 查询方式_仅关键字_操作(string sTagAHTML)
        {
            string s附加消息 = null;
            if (!string.IsNullOrEmpty(sTagAHTML))
            {
                s附加消息 = "点击这里，我要 " + sTagAHTML;
            }
            return 查询方式_仅关键字_消息(s附加消息);
        }

        /// <summary>
        /// 查询方式_仅关键字_操作
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加操作集合DD</para>
        /// </summary>
        /// <param name="l操作集合DD">l操作集合DD</param>
        /// <returns></returns>
        public static string 查询方式_仅关键字_操作(List<string> l操作集合DD)
        {
            List<string> l集合DD = 查询方式_仅关键字_LDD;
            l集合DD.AddRange(l操作集合DD);
            return 查询方式_仅关键字_集合(l集合DD);
        }
        #endregion

        #region 查询中转_仅关键字
        public static string 查询中转_仅关键字_操作(string s操作名称, string sTagAHTML)
        {

            if (LKExamURLQueryKey.包含关键字())
            {
                return 查询方式_仅关键字_操作(sTagAHTML);
            }
            else
            {
                return 查询方式_全部显示_操作或消息(s操作名称, sTagAHTML);
            }
        }
        public static string 查询中转_仅关键字_消息(string s操作名称, string s附加消息)
        {
            if (LKExamURLQueryKey.包含关键字())
            {
                return 查询方式_仅关键字_消息(s附加消息);
            }
            else
            {
                return 查询方式_全部显示_操作或消息(s操作名称, s附加消息);
            }
        }
        #endregion

        #region 查询方式_部门ID并关键字

        /// <summary>
        /// 查询方式_部门ID并关键
        /// <para>(1)默认SDT</para>
        /// </summary>
        public static string 查询方式_部门ID并关键字_SDT
        {
            get
            {
                return "在该 '" + LKPageHtml.MvcTextTag_B(LKExamEnvironment.部门名称) + "' 中没有搜索到您想查询的信息。您可以尝试：";
            }
        }

        /// <summary>
        /// 查询方式_部门ID并关键字
        /// <para>(1)默认LDD</para>
        /// </summary>
        public static List<string> 查询方式_部门ID并关键字_LDD
        {
            get
            {
                List<string> dd = new List<string>();
                dd.Add("选择" + (LKExamURLQueryKey.包含部门ID() ? "其他" : "相关") + "' " + LKExamEnvironment.部门名称 + " '");
                dd.Add("看看输入的文字是否有错");
                dd.Add("去掉不必要的词，如“的”、“吧”等");
                return dd;
            }
        }

        /// <summary>
        /// 查询方式_部门ID并关键字_集合
        /// <para>(1)为l集合DD附加序号</para>
        /// </summary>
        /// <param name="l集合DD">集合DD</param>
        /// <returns></returns>
        public static string 查询方式_部门ID并关键字_集合(List<string> l集合DD)
        {
            return 消息Dl(查询方式_部门ID并关键字_SDT, 集合附加序号(l集合DD), false);
        }

        /// <summary>
        /// 查询方式_部门ID并关键字_消息
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加一条消息</para>
        /// </summary>
        /// <param name="s附加消息">附加消息</param>
        /// <returns></returns>
        public static string 查询方式_部门ID并关键字_消息(string s附加消息)
        {
            List<string> l集合DD = 查询方式_部门ID并关键字_LDD;
            if (!string.IsNullOrEmpty(s附加消息))
            {
                l集合DD.Add(s附加消息);
            }
            return 查询方式_部门ID并关键字_集合(l集合DD);
        }

        /// <summary>
        /// 查询方式_部门ID并关键字
        /// <para>(1)只显示默认消息</para>
        /// </summary>
        /// <returns></returns>
        public static string 查询方式_部门ID并关键字()
        {
            return 查询方式_部门ID并关键字_消息((string)null);
        }

        /// <summary>
        /// 查询方式_部门ID并关键字_操作
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加操作消息</para>
        /// </summary>
        /// <param name="sTagAHTML">标签A的HTML</param>
        /// <returns></returns>
        public static string 查询方式_部门ID并关键字_操作(string sTagAHTML)
        {
            string s附加消息 = null;
            if (!string.IsNullOrEmpty(sTagAHTML))
            {
                s附加消息 = "点击这里，我要 " + sTagAHTML;
            }
            return 查询方式_部门ID并关键字_消息(s附加消息);
        }

        /// <summary>
        /// 查询方式_部门ID并关键字_操作
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加操作集合DD</para>
        /// </summary>
        /// <param name="l操作集合DD">l操作集合DD</param>
        /// <returns></returns>
        public static string 查询方式_部门ID并关键字_操作(List<string> l操作集合DD)
        {
            List<string> l集合DD = 查询方式_部门ID并关键字_LDD;
            l集合DD.AddRange(l操作集合DD);
            return 查询方式_部门ID并关键字_集合(l集合DD);
        }
        #endregion

        #region 查询中转_部门ID并关键字
        public static string 查询中转_部门ID并关键字_操作(string s操作名称, string sTagAHTML)
        {
            bool b查询方式_部门ID = LKExamURLQueryKey.包含部门ID();
            bool b查询方式_关键字 = LKExamURLQueryKey.包含关键字();
            if (b查询方式_部门ID && b查询方式_关键字)
            {
                return 查询方式_部门ID并关键字_操作(sTagAHTML);
            }
            else if (b查询方式_部门ID)
            {
                return 查询方式_仅部门ID_操作(sTagAHTML);
            }
            else if (b查询方式_关键字)
            {
                return 查询方式_仅关键字_操作(sTagAHTML);
            }
            else
            {
                return 查询方式_全部显示_操作或消息(s操作名称, sTagAHTML);
            }
        }
        public static string 查询中转_部门ID并关键字_消息(string s操作名称, string s附加消息)
        {
            bool b查询方式_部门ID = LKExamURLQueryKey.包含部门ID();
            bool b查询方式_关键字 = LKExamURLQueryKey.包含关键字();

            if (b查询方式_部门ID && b查询方式_关键字)
            {
                return 查询方式_部门ID并关键字_消息(s附加消息);
            }
            else if (b查询方式_部门ID)
            {
                return 查询方式_仅部门ID_消息(s附加消息);
            }
            else if (b查询方式_关键字)
            {
                return 查询方式_仅关键字_消息(s附加消息);
            }
            else
            {
                return 查询方式_全部显示_操作或消息(s操作名称, s附加消息);
            }
        }
        #endregion

        #region 查询方式_下拉静态值项并关键字

        /// <summary>
        /// 查询方式_下拉静态值项并关键字
        /// </summary>
        /// <param name="s操作名称">操作名称</param>
        /// <returns></returns>
        public static string 查询方式_下拉静态值项并关键字_SDT(string s操作名称)
        {
            return "在' " + LKPageHtml.MvcTextTag_B(s操作名称) + " '中没有搜索到您想查询的信息。您可以尝试：";
        }

        /// <summary>
        /// 查询方式_下拉静态值项并关键字
        /// <para>(1)默认LDD</para>
        /// </summary>
        /// <returns></returns>
        public static List<string> 查询方式_下拉静态值项并关键字_LDD
        {
            get
            {
                List<string> dd = new List<string>();
                dd.Add("选择" + (LKPageRetainOrReplace.GetString(LKExamURLQueryKey.包含下拉静态值项(), "其他", "相关") + "' 类型 '"));
                dd.Add("看看输入的文字是否有错");
                dd.Add("去掉不必要的词，如“的”、“吧”等");
                return dd;
            }
        }

        /// <summary>
        /// 查询方式_下拉静态值项并关键字_集合
        /// <para>(1)为l集合DD附加序号</para>
        /// </summary>
        /// <param name="l集合DD">集合DD</param>
        /// <returns></returns>
        public static string 查询方式_下拉静态值项并关键字_集合(string s操作名称, List<string> l集合DD)
        {
            return 消息Dl(查询方式_下拉静态值项并关键字_SDT(s操作名称), 集合附加序号(l集合DD), false);
        }

        /// <summary>
        /// 查询方式_下拉静态值项并关键字_消息
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加一条消息</para>
        /// </summary>
        /// <param name="s附加消息">附加消息</param>
        /// <returns></returns>
        public static string 查询方式_下拉静态值项并关键字_消息(string s操作名称, string s附加消息)
        {
            List<string> l集合DD = 查询方式_下拉静态值项并关键字_LDD;
            if (!string.IsNullOrEmpty(s附加消息))
            {
                l集合DD.Add(s附加消息);
            }
            return 查询方式_下拉静态值项并关键字_集合(s操作名称, l集合DD);
        }

        /// <summary>
        /// 查询方式_下拉静态值项并关键字
        /// <para>(1)只显示默认消息</para>
        /// </summary>
        /// <returns></returns>
        public static string 查询方式_下拉静态值项并关键字()
        {
            return 查询方式_下拉静态值项并关键字_消息((string)null, (string)null);
        }

        /// <summary>
        /// 查询方式_下拉静态值项并关键字_操作
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加操作消息</para>
        /// </summary>
        /// <param name="sTagAHTML">标签A的HTML</param>
        /// <returns></returns>
        public static string 查询方式_下拉静态值项并关键字_操作(string s操作名称, string sTagAHTML)
        {
            string s附加消息 = null;
            if (!string.IsNullOrEmpty(sTagAHTML))
            {
                s附加消息 = "点击这里，我要 " + sTagAHTML;
            }
            return 查询方式_下拉静态值项并关键字_消息(s操作名称, s附加消息);
        }

        /// <summary>
        /// 查询方式_下拉静态值项并关键字_操作
        /// <para>(1)显示默认消息</para>
        /// <para>(2)附加操作集合DD</para>
        /// </summary>
        /// <param name="l操作集合DD">l操作集合DD</param>
        /// <returns></returns>
        public static string 查询方式_下拉静态值项并关键字_操作(string s操作名称, List<string> l操作集合DD)
        {
            List<string> l集合DD = 查询方式_下拉静态值项并关键字_LDD;
            l集合DD.AddRange(l操作集合DD);
            return 查询方式_下拉静态值项并关键字_集合(s操作名称, l集合DD);
        }

        #endregion

        #region 查询中转_下拉静态值项并关键字
        public static string 查询中转_下拉静态值项并关键字_操作(string s操作名称, string sTagAHTML)
        {
            bool b查询方式_下拉静态值项 = LKExamURLQueryKey.包含下拉静态值项();
            bool b查询方式_关键字 = LKExamURLQueryKey.包含关键字();
            if (b查询方式_下拉静态值项 && b查询方式_关键字)
            {
                return 查询方式_下拉静态值项并关键字_操作(s操作名称, sTagAHTML);
            }
            else if (b查询方式_关键字)
            {
                return 查询方式_仅关键字_操作(sTagAHTML);
            }
            else
            {
                return 查询方式_全部显示_操作或消息(s操作名称, sTagAHTML);
            }
        }
        public static string 查询中转_下拉静态值项并关键字_消息(string s操作名称, string s附加消息)
        {
            bool b查询方式_下拉静态值项 = LKExamURLQueryKey.包含下拉静态值项();
            bool b查询方式_关键字 = LKExamURLQueryKey.包含关键字();

            if (b查询方式_下拉静态值项 && b查询方式_关键字)
            {
                return 查询方式_下拉静态值项并关键字_消息(s操作名称, s附加消息);
            }
            else if (b查询方式_关键字)
            {
                return 查询方式_仅关键字_消息(s附加消息);
            }
            else
            {
                return 查询方式_全部显示_操作或消息(s操作名称, s附加消息);
            }
        }
        #endregion

        #region 查询方式_全部显示
        /// <summary>
        /// 查询方式_全部显示
        /// <para>(1)默认SDT</para>
        /// </summary>
        /// <param name="s操作名称">操作名称</param>
        /// <param name="l集合DD">集合DD</param>
        /// <returns></returns>
        public static string 查询方式_全部显示_SDT(string s操作名称, List<string> l集合DD)
        {
            string dt = "到目前还没有 '" + LKPageHtml.MvcTextTag_B(s操作名称) + "' 的相关数据。";
            if (l集合DD != null && l集合DD.Count != 0)
            {
                dt += "您可以继续以下操作：";
            }
            return dt;
        }

        /// <summary>
        /// 查询方式_全部显示_集合
        /// <para>(1)为l集合DD附加序号</para>
        /// </summary>
        /// <param name="s操作名称">操作名称</param>
        /// <param name="l集合DD">集合DD</param>
        /// <returns></returns>
        public static string 查询方式_全部显示_集合(string s操作名称, List<string> l集合DD)
        {
            string dt = 查询方式_全部显示_SDT(s操作名称, l集合DD);


            /* 返回Html的DL标签 */
            return 消息Dl(dt, l集合DD, true);
        }

        /// <summary>
        /// 查询方式_全部显示
        /// <para>(1)显示SDT</para>
        /// <para>(2)附加一条消息</para>
        /// </summary>
        /// <param name="s操作名称">操作名称</param>
        /// <param name="s附加操作或消">附加操作或消</param>
        /// <returns></returns>
        public static string 查询方式_全部显示_操作或消息(string s操作名称, string s附加操作或消息)
        {
            /* dd */
            List<string> l集合DD = new List<string>();

            if (!string.IsNullOrEmpty(s附加操作或消息))
            {
                l集合DD.Add(s附加操作或消息);
            }

            return 查询方式_全部显示_集合(s操作名称, l集合DD);
        }

        /// <summary>
        /// 查询方式_全部显示
        /// <para>(1)显示SDT</para>
        /// <para>(2)附加l操作或消息集合DD</para>
        /// </summary>
        /// <param name="s操作名称"></param>
        /// <param name="l操作或消息集合DD"></param>
        /// <returns></returns>
        public static string 查询方式_全部显示_操作或消息(string s操作名称, List<string> l操作或消息集合DD)
        {
            return 查询方式_全部显示_集合(s操作名称, l操作或消息集合DD);
        }

        /// <summary>
        /// 查询方式_全部显示
        /// <para>(1)只显示SDT</para>
        /// </summary>
        /// <param name="s操作名称">操作名称</param>
        /// <returns></returns>
        public static string 查询方式_全部显示(string s操作名称)
        {
            return 查询方式_全部显示_集合(s操作名称, (List<string>)null);
        }
        #endregion

        #region 查询方式_消息方式/操作方式
        /// <summary>
        /// 查询方式_消息方式
        /// </summary>
        /// <param name="eMvcPager查询方式">MvcPager查询方式</param>
        /// <param name="s操作名称">操作名称</param>
        /// <param name="s附加消息">附加消息</param>
        /// <returns></returns>
        public static string 查询方式_消息方式(MvcPager查询方式 eMvcPager查询方式, string s操作名称, string s附加消息)
        {
            string sMsgHTML = "";

            /* MvcPager查询方式 */
            switch (eMvcPager查询方式)
            {
                /* 全部显示 */
                case MvcPager查询方式.全部显示:
                    sMsgHTML = 查询方式_全部显示_操作或消息(s操作名称, s附加消息);
                    break;

                /* 仅部门ID */
                case MvcPager查询方式.仅部门ID:
                    sMsgHTML = 查询中转_仅部门ID_消息(s操作名称, s附加消息);
                    break;

                /* 仅关键字 */
                case MvcPager查询方式.仅关键字:
                    sMsgHTML = 查询中转_仅关键字_消息(s操作名称, s附加消息);
                    break;

                /* 部门ID并关键字 */
                case MvcPager查询方式.部门ID并关键字:
                    sMsgHTML = 查询中转_部门ID并关键字_消息(s操作名称, s附加消息);
                    break;

                /* 下拉静态值项并关键字 */
                case MvcPager查询方式.下拉静态值项并关键字:
                    sMsgHTML = 查询中转_下拉静态值项并关键字_消息(s操作名称, s附加消息);
                    break;

                /* 其他 */
                default:
                    break;
            }
            return sMsgHTML;
        }

        /// <summary>
        /// 查询方式_操作方式
        /// </summary>
        /// <param name="eMvcPager查询方式">MvcPager查询方式</param>
        /// <param name="s操作名称">操作名称</param>
        /// <param name="sTagAHTML">标签AHTML</param>
        /// <returns></returns>
        public static string 查询方式_操作方式(MvcPager查询方式 eMvcPager查询方式, string s操作名称, string sTagAHTML)
        {
            string sMsgHTML = "";

            /* MvcPager查询方式 */
            switch (eMvcPager查询方式)
            {
                /* 全部显示 */
                case MvcPager查询方式.全部显示:
                    sMsgHTML = 查询方式_全部显示_操作或消息(s操作名称, sTagAHTML);
                    break;

                /* 仅部门ID */
                case MvcPager查询方式.仅部门ID:
                    sMsgHTML = 查询中转_仅部门ID_操作(s操作名称, sTagAHTML);
                    break;

                /* 仅关键字 */
                case MvcPager查询方式.仅关键字:
                    sMsgHTML = 查询中转_仅关键字_操作(s操作名称, sTagAHTML);
                    break;

                /* 部门ID并关键字 */
                case MvcPager查询方式.部门ID并关键字:
                    sMsgHTML = 查询中转_部门ID并关键字_操作(s操作名称, sTagAHTML);
                    break;

                /* 下拉静态值项并关键字 */
                case MvcPager查询方式.下拉静态值项并关键字:
                    sMsgHTML = 查询中转_下拉静态值项并关键字_操作(s操作名称, sTagAHTML);
                    break;

                /* 其他 */
                default:
                    break;
            }
            return sMsgHTML;
        }
        #endregion

        #region 查询方式_操作消息
        /// <summary>
        /// 查询方式_我要操作
        /// </summary>
        /// <param name="eMvcPager查询方式">MvcPager查询方式</param>
        /// <param name="s操作名称">操作名称</param>
        /// <param name="sTagACont">标签A内容</param>
        /// <param name="sTagAClick">标签A点击事件</param>
        /// <returns></returns>
        public static string 查询方式_我要操作(MvcPager查询方式 eMvcPager查询方式, string s操作名称, string sTagACont, string sTagAClick)
        {
            /* A标签 */
            string sTagAHTML = LKPageHtml.MvcTextTag_A(sTagACont, sTagAClick);

            return 查询方式_操作方式(eMvcPager查询方式, s操作名称, sTagAHTML);
        }
        /// <summary>
        /// 查询方式_我要操作
        /// </summary>
        /// <param name="eMvcPager查询方式">MvcPager查询方式</param>
        /// <param name="s操作名称">操作名称</param>
        /// <param name="sTagACont">标签A内容</param>
        /// <param name="sTagAHref">标签A超链接地址</param>
        /// <param name="eATarget">标签A链接目标</param>
        /// <returns></returns>
        public static string 查询方式_我要操作(MvcPager查询方式 eMvcPager查询方式, string s操作名称, string sTagACont, string sTagAHref, ATarget eATarget)
        {
            /* A标签 */
            string sTagAHTML = LKPageHtml.MvcTextTag_A(sTagACont, sTagAHref, eATarget);

            return 查询方式_操作方式(eMvcPager查询方式, s操作名称, sTagAHTML);
        }
        /// <summary>
        /// 查询方式_我要操作
        /// </summary>
        /// <param name="eMvcPager查询方式">MvcPager查询方式</param>
        /// <param name="s操作名称">操作名称</param>
        /// <param name="sTagACont">标签A内容</param>
        /// <param name="sActionName">ActionName方法名/文件名</param>
        /// <param name="sControllerName">ControllerName类名/文件夹名</param>
        /// <param name="eATarget">标签A链接目标</param>
        /// <returns></returns>
        public static string 查询方式_我要操作(MvcPager查询方式 eMvcPager查询方式, string s操作名称, string sTagACont, string sActionName, string sControllerName, ATarget eATarget)
        {
            /* A标签 */
            string sTagAHTML = LKPageHtml.MvcTextTag_A(sTagACont, sActionName, sControllerName, eATarget);

            return 查询方式_操作方式(eMvcPager查询方式, s操作名称, sTagAHTML);
        }
        /// <summary>
        /// 查询方式_我要操作
        /// </summary>
        /// <param name="eMvcPager查询方式">MvcPager查询方式</param>
        /// <param name="s操作名称">操作名称</param>
        /// <param name="sTagACont">标签A内容</param>
        /// <param name="oTagAAttributes">标签A属性</param>
        /// <returns></returns>
        public static string 查询方式_我要操作(MvcPager查询方式 eMvcPager查询方式, string s操作名称, string sTagACont, object oTagAAttributes)
        {
            /* A标签 */
            string sTagAHTML = LKPageHtml.MvcTextTag_A(sTagACont, oTagAAttributes);

            return 查询方式_操作方式(eMvcPager查询方式, s操作名称, sTagAHTML);
        }
        #endregion

    }

    /// <summary>
    /// LKExamMvcPager操作目标
    /// </summary>
    public class LKExamMvcPager操作目标 : LKExamMvcPager空数据
    {

        #region 管理员
        public static string 管理员_考生信息
        {
            get
            {
                string s考生名称 = LKExamEnvironment.考生名称;
                return 查询方式_我要操作(MvcPager查询方式.部门ID并关键字, s考生名称 + "信息", "添加" + s考生名称 + "账户", "Add", "User", ATarget._self);
            }
        }
        public static string 管理员_导出考生Excel
        {
            get
            {
                if (LKExamURLQueryKey.包含部门ID())
                {
                    string s考生名称 = LKExamEnvironment.考生名称;
                    return 查询方式_我要操作(MvcPager查询方式.仅部门ID, "导出" + s考生名称 + "Excel信息", "添加" + s考生名称 + "账户", "Add", "User", ATarget._self);
                }
                else
                {
                    return 查询方式_仅部门ID_集合(查询方式_仅部门ID_LDD, "还没有选择 '" + LKPageHtml.MvcTextTag_B(LKExamEnvironment.部门名称) + "' 吧！您可以尝试：");
                }

            }
        }
        public static string 管理员_考官信息
        {
            get
            {
                string s考官名称 = LKExamEnvironment.考官名称;
                return 查询方式_我要操作(MvcPager查询方式.仅关键字, s考官名称 + "信息", "添加" + s考官名称 + "账户", "/User/Add?rtype=1", ATarget._self);
            }
        }
        public static string 管理员_部门信息
        {
            get
            {
                string s部门名称 = LKExamEnvironment.部门名称;
                return 查询方式_我要操作(MvcPager查询方式.仅关键字, s部门名称 + "信息", "添加" + s部门名称, "Add", "Department", ATarget._self);
            }
        }
        #endregion

        #region 考官
        public static string 考官_我的试卷
        {
            get
            {
                int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项();
                Dictionary<string, string> dictionary = LKExamSelectList静态值项.考官试卷;

                string s操作名称 = "我的试卷";
                if (dictionary.ContainsKey(i下拉静态值项.ToString()))
                {
                    s操作名称 = dictionary[i下拉静态值项.ToString()];
                }

                return 查询方式_我要操作(MvcPager查询方式.下拉静态值项并关键字, s操作名称, "添加试卷", "Add", "Test", ATarget._self);
            }
        }
        public static string 考官_我的试题
        {
            get
            {
                int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项();
                Dictionary<string, string> dictionary = LKExamSelectList静态值项.考官试题;

                string s操作名称 = "我的试题";
                if (dictionary.ContainsKey(i下拉静态值项.ToString()))
                {
                    s操作名称 = dictionary[i下拉静态值项.ToString()];
                }

                return 查询方式_我要操作(MvcPager查询方式.下拉静态值项并关键字, s操作名称, "添加试题", "Add", "Subject", ATarget._self);
            }
        }
        public static string 考官_已组织考试
        {
            get
            {
                return 查询方式_我要操作(MvcPager查询方式.仅关键字, "已组织考试", "查看我的试卷", "MyPapers", "Test", ATarget._self);
            }
        }
        public static string 考官_已组织练习
        {
            get
            {
                return 查询方式_我要操作(MvcPager查询方式.仅关键字, "已组织练习", "查看我的试卷", "MyPapers", "Test", ATarget._self);
            }
        }
        public static string 考官_考试结果列表
        {
            get
            {
                return 查询方式_消息方式(MvcPager查询方式.部门ID并关键字, "考试结果详情", "");
            }
        }
        public static string 考官_组织考试或练习设置考生范围
        {
            get
            {
                string s考生名称 = LKExamEnvironment.考生名称;

                List<string> dd = new List<string>();
                dd.Add("点击这里，我要 " + LKPageHtml.MvcTextTag_A("返回考生范围列表", "Global.MvcPager.Search.retreat();"));
                dd.Add("点击这里，我要 " + LKPageHtml.MvcTextTag_A("添加" + s考生名称 + "账户", "Add", "User", ATarget._blank));

                bool b查询方式_部门ID = LKExamURLQueryKey.包含部门ID();
                bool b查询方式_关键字 = LKExamURLQueryKey.包含关键字();

                /* 查询方式_部门ID并关键字_操作 */
                if (b查询方式_部门ID && b查询方式_关键字)
                {
                    return 查询方式_部门ID并关键字_操作(dd);
                }
                /* 查询方式_仅部门ID_操作 */
                else if (b查询方式_部门ID)
                {
                    return 查询方式_仅部门ID_操作(dd);
                }
                /* 查询方式_仅关键字_操作 */
                else if (b查询方式_关键字)
                {
                    return 查询方式_仅关键字_操作(dd);
                }
                /* 查询方式_我要操作 */
                else
                {
                    return 查询方式_我要操作(MvcPager查询方式.全部显示, "可上传试题", "添加" + s考生名称 + "账户", "Add", "User", ATarget._blank);
                }
            }
        }
        public static string 考官_爱考网资源共享上传试题列表
        {
            get
            {
                if (LKExamURLQueryKey.包含关键字())
                {
                    List<string> dd = new List<string>();
                    dd.Add("点击这里，我要 " + LKPageHtml.MvcTextTag_A("返回上传试题列表", "Global.MvcPager.Search.retreat();"));
                    dd.Add("点击这里，我要 " + LKPageHtml.MvcTextTag_A("添加试题", "Add", "Subject", ATarget._blank));
                    return 查询方式_仅关键字_操作(dd);
                }
                else
                {
                    return 查询方式_我要操作(MvcPager查询方式.全部显示, "可上传试题", "添加试题", "Add", "Subject", ATarget._blank);
                }
            }
        }
        public static string 考官_爱考网资源共享上传试卷列表
        {
            get
            {
                if (LKExamURLQueryKey.包含关键字())
                {
                    List<string> dd = new List<string>();
                    dd.Add("点击这里，我要 " + LKPageHtml.MvcTextTag_A("返回上传试卷列表", "Global.MvcPager.Search.retreat();"));
                    dd.Add("点击这里，我要 " + LKPageHtml.MvcTextTag_A("添加试卷", "Add", "Test", ATarget._blank));
                    return 查询方式_仅关键字_集合(dd);
                }
                else
                {
                    return 查询方式_我要操作(MvcPager查询方式.全部显示, "可上传试卷", "添加试卷", "Add", "Test", ATarget._blank);
                }
            }
        }
        public static string 考官_爱考网资源共享下载试题列表
        {
            get
            {
                if (LKExamURLQueryKey.包含关键字())
                {
                    return 查询方式_我要操作(MvcPager查询方式.仅关键字, "可下载试题", "返回下载试题列表", "Global.MvcPager.Search.retreat();");
                }
                else
                {
                    return 查询方式_全部显示("可下载试题");
                }
            }
        }
        public static string 考官_爱考网资源共享下载试卷列表
        {
            get
            {
                if (LKExamURLQueryKey.包含关键字())
                {
                    return 查询方式_我要操作(MvcPager查询方式.仅关键字, "可下载试卷", "返回下载试卷列表", "Global.MvcPager.Search.retreat();");
                }
                else
                {
                    return 查询方式_全部显示("可下载试卷");
                }
            }
        }
        #endregion

        #region 考生
        public static string 考生_我要考试
        {
            get
            {
                int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项();
                Dictionary<string, string> dictionary = LKExamSelectList静态值项.考生我要考试;

                string s操作名称 = "考试列表";
                if (dictionary.ContainsKey(i下拉静态值项.ToString()))
                {
                    s操作名称 = dictionary[i下拉静态值项.ToString()];
                }

                return 查询方式_消息方式(MvcPager查询方式.下拉静态值项并关键字, s操作名称, "可以让您的" + LKExamEnvironment.考官名称 + "组织考试即可");
            }
        }
        public static string 考生_我要练习
        {
            get
            {
                return 查询方式_消息方式(MvcPager查询方式.仅关键字, "练习列表", "可以让您的" + LKExamEnvironment.考官名称 + "组织练习即可");
            }
        }
        public static string 考生_考试记录
        {
            get
            {
                int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项();
                Dictionary<string, string> dictionary = LKExamSelectList静态值项.考生考试记录;

                string s操作名称 = "我的考试记录";
                if (dictionary.ContainsKey(i下拉静态值项.ToString()))
                {
                    s操作名称 = dictionary[i下拉静态值项.ToString()];
                }

                return 查询方式_我要操作(MvcPager查询方式.下拉静态值项并关键字, s操作名称, "参加考试", "SelectExam", "Exam", ATarget._self);
            }
        }
        public static string 考生_练习记录
        {
            get
            {
                int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项();
                Dictionary<string, string> dictionary = LKExamSelectList静态值项.考生练习记录;

                string s操作名称 = "我的练习记录";
                if (dictionary.ContainsKey(i下拉静态值项.ToString()))
                {
                    s操作名称 = dictionary[i下拉静态值项.ToString()];
                }

                return 查询方式_我要操作(MvcPager查询方式.下拉静态值项并关键字, s操作名称, "参加练习", "SelectPractice", "Exam", ATarget._self);
            }
        }

        public static string 考生_考试排行榜
        {
            get
            {
                return 查询方式_我要操作(MvcPager查询方式.仅关键字, "考试排行榜", "参加考试", "SelectExam", "Exam", ATarget._self);
            }
        }
        #endregion
    }

    /// <summary>
    /// 分页信息视图
    /// </summary>
    /// <typeparam name="T">List</typeparam>
    public class LKExamMvcPagerData<T>
    {
        /// <summary>
        /// 返回页数和总数字符串
        /// </summary>
        /// <param name="totalItemCount">总记录数</param>
        /// <param name="totalPageCount">总页数</param>
        /// <returns></returns>
        public static string Get页数和总数(int totalItemCount, int totalPageCount)
        {
            return "<span id=\"MvcPagerDataInfo\" totalPageCount=\"" + totalPageCount + "\" totalItemCount=\"" + totalItemCount + "\" >&nbsp;<span>共计" + totalPageCount + "页</span>&nbsp;&nbsp;<span>" + totalItemCount + "条</span></span>";
        }

        public static void 数据信息(PagedList<T> pagedList, MvcPager引用目标 eMvcPager引用目标, ViewDataDictionary ViewData)
        {
            数据信息Helper(pagedList, eMvcPager引用目标, ViewData, null, true);
        }
        public static void 数据信息(PagedList<T> pagedList, MvcPager引用目标 eMvcPager引用目标, ViewDataDictionary ViewData, string sPageTitle)
        {
            数据信息Helper(pagedList, eMvcPager引用目标, ViewData, sPageTitle, true);
        }
        public static void 数据信息无分页(PagedList<T> pagedList, MvcPager引用目标 eMvcPager引用目标, ViewDataDictionary ViewData)
        {
            数据信息Helper(pagedList, eMvcPager引用目标, ViewData, null, false);
        }
        public static void 数据信息无分页(PagedList<T> pagedList, MvcPager引用目标 eMvcPager引用目标, ViewDataDictionary ViewData, string sPageTitle)
        {
            数据信息Helper(pagedList, eMvcPager引用目标, ViewData, sPageTitle, false);
        }

        /// <summary>
        /// 分页数据信息
        /// </summary>
        /// <param name="pagedList">PagedList</param>
        /// <param name="eMvcPager引用目标">信息类型</param>
        /// <param name="ViewData">ViewData</param>
        /// <param name="sPageTitle">PageTitle</param>
        /// <param name="bISMvcPager">是否分页</param>
        public static void 数据信息Helper(PagedList<T> pagedList, MvcPager引用目标 eMvcPager引用目标, ViewDataDictionary ViewData, string sPageTitle, bool bISMvcPager)
        {
            string emptyData = "";
            string pageCount = "";
            int pageIndex = pagedList == null ? 0 : pagedList.CurrentPageIndex;
            int totalItemCount = pagedList == null ? 0 : pagedList.TotalItemCount;
            int totalPageCount = pagedList == null ? 0 : pagedList.TotalPageCount;

            //总条数为0时,不显示空数据
            try
            {
                emptyData = totalItemCount != 0 ? "" : typeof(LKExamMvcPager操作目标).GetPropertyValue(eMvcPager引用目标.ToString()).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("数据列表空数据，不存在信息类型：" + eMvcPager引用目标);
            }
            //为分页类型并不超出索引范围
            if (bISMvcPager && pageIndex <= totalPageCount)
            {
                pageCount = Get页数和总数(totalItemCount, totalPageCount);
            }

            ViewData["MvcPagerDataInfo"] = emptyData + pageCount;

            if (!string.IsNullOrEmpty(sPageTitle)) ViewData["PageTitle"] = sPageTitle;
        }
    }
}