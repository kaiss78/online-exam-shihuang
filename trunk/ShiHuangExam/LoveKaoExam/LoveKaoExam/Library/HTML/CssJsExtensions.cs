using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKao.Page;
using LoveKaoExam.Library.CSharp;

namespace LoveKaoExam.Library.HTML
{
    public static class CssJsExtensions
    {
        /// <summary>
        /// 引用母板页CSS/JS文件
        /// <para>(1)Global 包括([Basic.css][Global.css][jquery-1.4.2.js][LKConfig.js][Global.js])</para>
        /// <para>(2)MasterPage 包含([Site.css])</para>
        /// <para>(3)PluginsBoxy 包含([boxy.css][jQuery.boxy.js])</para>
        /// <para>(4)PluginsMsgBox 包含([msgBox.css][jQuery.msgBox.js])</para>
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <returns></returns>
        public static MvcHtmlString CssJsMasterPage(this HtmlHelper htmlHelper)
        {
            return CssJsHelper(htmlHelper, new LKPageCssJs()
            {
                ISMasterPage = true,
                ISPluginsBoxy = true,
                ISPluginsMsgBox = true
            });
        }
        /// <summary>
        /// 引用母板页CSS/JS文件
        /// <para>(1)Global 包括([Basic.css][Global.css][jquery-1.4.2.js][LKConfig.js][Global.js])</para>
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <returns></returns>
        public static MvcHtmlString CssJsGlobal(this HtmlHelper htmlHelper)
        {
            return CssJsHelper(htmlHelper, new LKPageCssJs());
        }
        /// <summary>
        /// 引用基本CSS/JS文件
        /// <para>(1)Global 包括([Basic.css][Global.css][jquery-1.4.2.js][LKConfig.js][Global.js])</para>
        /// <para>(3)PluginsBoxy 包含([boxy.css][jQuery.boxy.js])</para>
        /// <para>(4)PluginsMsgBox 包含([msgBox.css][jQuery.msgBox.js])</para>
        /// <para>(4)CssMvcPager 包含([MvcPager.css])</para>
        /// <para>(5)CssUseBox 包含([UseBox.css])</para>
        /// <para>(6)除MasterPage 不包含([Site.css])</para>
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <returns></returns>
        public static MvcHtmlString CssJsBasic(this HtmlHelper htmlHelper)
        {
            return CssJsHelper(htmlHelper, new LKPageCssJs()
            {
                ISPluginsBoxy = true,
                ISPluginsMsgBox = true,
                ISCssMvcPager = true,
                ISCssUseBox = true
            });
        }
        /// <summary>
        /// 引用基本CSS/JS文件
        /// <para>(1)Global 包括([Basic.css][Global.css][jquery-1.4.2.js][LKConfig.js][Global.js])</para>
        /// <para>(2)PluginsBoxy 包含([boxy.css][jQuery.boxy.js])</para>
        /// <para>(3)PluginsMsgBox 包含([msgBox.css][jQuery.msgBox.js])</para>
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="isBoxyFile">是否引用Boxy对话款css与js</param>
        /// <returns></returns>
        public static MvcHtmlString CssJs_PBoxyMsgBox(this HtmlHelper htmlHelper)
        {
            return CssJsHelper(htmlHelper, new LKPageCssJs()
            {
                ISPluginsBoxy = true,
                ISPluginsMsgBox = true,
            });
        }
        /// <summary>
        /// 引用基本CSS/JS文件
        /// <para>(1)CssMvcPager 包含([MvcPager.css])</para>
        /// <para>(2)CssUseBox 包含([UseBox.css])</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString CssJs_CMP_UB(this HtmlHelper htmlHelper)
        {
            return CssJsHelper(htmlHelper, new LKPageCssJs()
            {
                ISGlobal = false,
                ISCssMvcPager = true,
                ISCssUseBox = true
            });
        }
        /// <summary>
        /// 引用基本CSS/JS文件
        /// <para>(1)CssMvcPager 包含([MvcPager.css])</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString CssJs_CMvcPager(this HtmlHelper htmlHelper)
        {
            return CssJsHelper(htmlHelper, new LKPageCssJs()
            {
                ISGlobal = false,
                ISCssMvcPager = true
            });
        }
        /// <summary>
        /// 引用基本CSS/JS文件
        /// <para>(1)CssUseBox 包含([UseBox.css])</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString CssJs_CUseBox(this HtmlHelper htmlHelper)
        {
            return CssJsHelper(htmlHelper, new LKPageCssJs()
            {
                ISGlobal = false,
                ISCssUseBox = true
            });

        }
        /// <summary>
        /// 引用基本CSS/JS文件 
        /// (1)扩展方法
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="cssJs">CssJs类</param>
        /// <returns></returns>
        public static MvcHtmlString CssJsHelper(this HtmlHelper htmlHelper, LKPageCssJs cssJs)
        {
            #region CssPath
            List<string> cssPath = new List<string>();
            if (cssJs.ISGlobal) cssPath.Add("/Content/StyleSheet/Basic.css");
            if (cssJs.ISMasterPage) cssPath.Add("/Content/StyleSheet/Site.css");
            if (cssJs.ISGlobal) cssPath.Add("/Content/StyleSheet/Global.css");
            if (cssJs.ISPluginsBoxy) cssPath.Add("/Library/Plugins/Boxy-0.1.4/StyleSheets/boxy.css");
            if (cssJs.ISPluginsMsgBox) cssPath.Add("/Library/Plugins/MsgBox/StyleSheets/msgBox.css");

            if (cssJs.ISCssMvcPager) cssPath.Add("/Content/StyleSheet/MvcPager.css");
            if (cssJs.ISCssUseBox) cssPath.Add("/Content/StyleSheet/UseBox.css");

            string cssTag = "";
            foreach (string path in cssPath)
            {
                cssTag += "\r\n<link href=\"" + path + "\" rel=\"stylesheet\" type=\"text/css\" />";
            }
            #endregion

            #region JsPath
            List<string> jsPath = new List<string>();
            if (cssJs.ISGlobal) jsPath.Add("/Scripts/JQuery/jquery-1.4.2.js");
            if (cssJs.ISGlobal) jsPath.Add("/Scripts/Base/LKConfig.js");
            if (cssJs.ISGlobal) jsPath.Add("/Scripts/Base/Global.js?hj_XX=" + LKExamEnvironment.是否为学校);
            if (cssJs.ISGlobal) jsPath.Add("/Scripts/Base/GlobalManage.js");
            if (cssJs.ISPluginsBoxy) jsPath.Add("/Library/Plugins/Boxy-0.1.4/JavaScripts/jQuery.boxy.js");
            if (cssJs.ISPluginsMsgBox) jsPath.Add("/Library/Plugins/MsgBox/JavaScripts/jQuery.msgBox.js");
            string jsTag = "";
            foreach (string path in jsPath)
            {
                jsTag += "\r\n<script src=\"" + path + "\" type=\"text/javascript\"></script>";
            }
            #endregion

            return MvcHtmlString.Create(cssTag + jsTag);
        }
         
    }
}