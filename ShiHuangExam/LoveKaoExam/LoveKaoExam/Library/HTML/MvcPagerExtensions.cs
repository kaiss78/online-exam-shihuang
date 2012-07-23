using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using LoveKao.Page;
using LoveKao.Page.HTML;
using System.Web.Routing;
using System.Web.Mvc.Html;
using System.Web.Mvc.Ajax;

namespace LoveKaoExam.Library.HTML
{
    public static class MvcPagerExtensions
    {
        #region MvcPagerSearch

        #region MvcPagerNormalSearch
        /// <summary>
        /// MvcPager搜索
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="文本Label">文本输入框无内容失去焦点时显示的文本</param>
        /// <returns></returns>
        public static MvcHtmlString MvcPagerNormalSearch(this HtmlHelper htmlHelper, string 文本Label)
        {
            return MvcPagerSearchHelper(htmlHelper, new LKPageMvcPager查询配置()
            {
                文本输入框 = new LKPageMvcPager文本输入框(文本Label),
                下拉列表框 = new LKPageMvcPager下拉列表框(false)
            });
        }

        /// <summary>
        /// MvcPager搜索
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="n文本输入框">文本输入框</param>
        /// <returns></returns>
        public static MvcHtmlString MvcPagerNormalSearch(this HtmlHelper htmlHelper, LKPageMvcPager文本输入框 n文本输入框)
        {
            return MvcPagerSearchHelper(htmlHelper, new LKPageMvcPager查询配置(n文本输入框));
        }

        /// <summary>
        /// MvcPager搜索
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="n下拉列表框">下拉列表框</param>
        /// <returns></returns>
        public static MvcHtmlString MvcPagerNormalSearch(this HtmlHelper htmlHelper, LKPageMvcPager下拉列表框 n下拉列表框)
        {
            return MvcPagerSearchHelper(htmlHelper, new LKPageMvcPager查询配置(n下拉列表框));
        }
        
        /// <summary>
        /// MvcPager搜索
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="n文本输入框">文本输入框</param>
        /// <param name="n下拉列表框">下拉列表框</param>
        /// <returns></returns>
        public static MvcHtmlString MvcPagerNormalSearch(this HtmlHelper htmlHelper, LKPageMvcPager文本输入框 n文本输入框, LKPageMvcPager下拉列表框 n下拉列表框)
        {
            return MvcPagerSearchHelper(htmlHelper, new LKPageMvcPager查询配置()
            {
                文本输入框 = n文本输入框,
                下拉列表框 = n下拉列表框
            });
        }
        #endregion

        #region MvcPagerAjaxSearch
        /// <summary>
        /// MvcPagerAjax搜索
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="文本Label">文本输入框无内容失去焦点时显示的文本</param>
        /// <returns></returns>
        public static MvcHtmlString MvcPagerAjaxSearch(this HtmlHelper htmlHelper, string 文本Label)
        {
            return MvcPagerSearchHelper(htmlHelper, new LKPageMvcPager查询配置(true)
            {
                文本输入框 = new LKPageMvcPager文本输入框(文本Label),
                下拉列表框 = new LKPageMvcPager下拉列表框(false)
            });
        }

        /// <summary>
        /// MvcPagerAjax搜索
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="n文本输入框">文本输入框</param>
        /// <returns></returns>
        public static MvcHtmlString MvcPagerAjaxSearch(this HtmlHelper htmlHelper, LKPageMvcPager文本输入框 n文本输入框)
        {
            return MvcPagerSearchHelper(htmlHelper, new LKPageMvcPager查询配置(true, n文本输入框));
        }

        /// <summary>
        /// MvcPagerAjax搜索
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="n下拉列表框">下拉列表框</param>
        /// <returns></returns>
        public static MvcHtmlString MvcPagerAjaxSearch(this HtmlHelper htmlHelper, LKPageMvcPager下拉列表框 n下拉列表框)
        {
            return MvcPagerSearchHelper(htmlHelper, new LKPageMvcPager查询配置(true, n下拉列表框));
        }

        /// <summary>
        /// MvcPagerAjax搜索
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="n文本输入框">文本输入框</param>
        /// <param name="n下拉列表框">下拉列表框</param>
        /// <returns></returns>
        public static MvcHtmlString MvcPagerAjaxSearch(this HtmlHelper htmlHelper, LKPageMvcPager文本输入框 n文本输入框, LKPageMvcPager下拉列表框 n下拉列表框)
        {
            return MvcPagerSearchHelper(htmlHelper, new LKPageMvcPager查询配置(true)
            {
                文本输入框 = n文本输入框,
                下拉列表框 = n下拉列表框
            });
        }
        #endregion

        /// <summary>
        /// MvcPager搜索帮助器
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="MvcPagerSearch属性集合">MvcPagerSearch属性集合</param>
        /// <returns></returns>
        public static MvcHtmlString MvcPagerSearchHelper(this HtmlHelper htmlHelper, LKPageMvcPager查询配置 MvcPagerSearch属性集合)
        {
            /* MvcPager.css */
            string StyleLink = LoveKao.Page.HTML.CssJsExtensions.StyleLink(htmlHelper, "/Content/StyleSheet/MvcPager.css").ToHtmlString();

            string divStart = "<div id=\"MvcPagerSearch\">";
            string divEnd = "</div>";
            string button;

            #region dropDownList
            string dropDownList = "";
            if (MvcPagerSearch属性集合.下拉列表框.是否自动附带)
            {
                dropDownList = DropDownListExtensions.DropDownList(htmlHelper, MvcPagerSearch属性集合.下拉列表框).ToHtmlString();
            }
            #endregion

            #region ISAjaxSearch
            if (MvcPagerSearch属性集合.是否用Ajax搜索)
            {
                #region button
                button = ButtonExtensions.Button(htmlHelper, "btn1", (object)"查询", new { onclick = "Global.MvcPager.Search.submit();" }).ToHtmlString();
                #endregion
            }
            else
            {
                #region form
                RouteValueDictionary routeValueDictionary = htmlHelper.ViewContext.RouteData.Values;
                string controllerName = routeValueDictionary["controller"].ToString();
                string actionName = routeValueDictionary["action"].ToString();
                string formAction = "/" + controllerName + "/" + actionName + "/1";
                TagBuilder formTag = new TagBuilder("form");
                formTag.MergeAttribute("action", formAction);
                formTag.MergeAttribute("method", "get");
                divStart = divStart + formTag.ToString(TagRenderMode.StartTag);
                divEnd = formTag.ToString(TagRenderMode.EndTag) + divEnd;
                #endregion

                #region button
                button = ButtonExtensions.Submit(htmlHelper).ToHtmlString();
                #endregion
            }
            #endregion

            #region keyWords
            string keyWords = "";
            if (MvcPagerSearch属性集合.文本输入框.是否自动附带)
            {
                string s文本Label = MvcPagerSearch属性集合.文本输入框.文本Label;
                keyWords = KeyWordsExtensions.KeyWords(htmlHelper, s文本Label, MvcPagerSearch属性集合.是否用Ajax搜索).ToHtmlString();

            }
            #endregion

            /* 完整的HTML */
            string fullHtml = StyleLink + divStart + dropDownList + keyWords + button + divEnd;

            return MvcHtmlString.Create(fullHtml);
        }
        #endregion

        #region MvcPagerDataInfo
        public static MvcHtmlString MvcPagerDataInfo(this HtmlHelper htmlHelper)
        {
            return MvcPagerDataInfo(htmlHelper, null);
        }
        public static MvcHtmlString MvcPagerDataInfo(this HtmlHelper htmlHelper, string expression)
        {
            object o = null;
            expression = string.IsNullOrEmpty(expression) ? "MvcPagerDataInfo" : expression;
            if (htmlHelper.ViewData != null)
            {
                o = htmlHelper.ViewData.Eval(expression);
            }
            if (o == null)
            {
                throw new ArgumentException("值为null", "MvcPagerDataInfo");
            }

            return MvcHtmlString.Create(o.ToString());
        }
        #endregion

        #region MvcPagerLink
        public static MvcHtmlString MvcPagerNormalLink<T>(this HtmlHelper htmlHelper, PagedList<T> pagedList)
        {
            return PagerHelper.Pager(htmlHelper, pagedList, LKPageMvcPager分页配置.Normal选项);
        }
        public static MvcHtmlString MvcPagerAjaxLink<T>(this HtmlHelper htmlHelper, PagedList<T> pagedList)
        {
            return PagerHelper.AjaxPager(htmlHelper, pagedList, LKPageMvcPager分页配置.Normal选项, LKPageMvcPager分页配置.Ajax选项);
        }

         
        #endregion

        #region MvcPagerSwitch
        public static MvcHtmlString MvcPagerSwitchPager<T>(this HtmlHelper htmlHelper, PagedList<T> pagedList, MvcPager分页模式 eMvcPager分页模式)
        {
            string divStart = "<div id=\"MvcPagerNav\">";
            string divEnd = "</div>";

            string dataInfo = MvcPagerDataInfo(htmlHelper, null).ToHtmlString();
            string link = "";
            switch (eMvcPager分页模式)
            {
                case MvcPager分页模式.Ajax:
                    link = MvcPagerAjaxLink<T>(htmlHelper, pagedList).ToHtmlString();
                    break;
                case MvcPager分页模式.Normal:
                    link = MvcPagerNormalLink<T>(htmlHelper, pagedList).ToHtmlString();
                    break;
            }

            return MvcHtmlString.Create(divStart + link + dataInfo + divEnd);
        }

        public static MvcHtmlString MvcPagerAjaxPager<T>(this HtmlHelper htmlHelper, PagedList<T> pagedList)
        {
            return MvcPagerSwitchPager(htmlHelper, pagedList, MvcPager分页模式.Ajax);
        }

        public static MvcHtmlString MvcPagerNormalPager<T>(this HtmlHelper htmlHelper, PagedList<T> pagedList)
        {
            return MvcPagerSwitchPager(htmlHelper, pagedList, MvcPager分页模式.Normal);
        }
        
        #endregion

        #region MvcPagerAjaxRenderPartial
        /// <summary>
        /// 使用指定MvcPagerAjax的 HMTL 帮助器来呈现指定的分部视图。
        /// </summary>
        /// <param name="htmlHelper">HTML 帮助器。</param>
        /// <param name="partialViewName">分部视图的名称。</param>
        public static void MvcPagerAjaxRenderPartial(this HtmlHelper htmlHelper, string partialViewName)
        {
            MvcPagerAjaxRenderPartial(htmlHelper, partialViewName, (object)null, (ViewDataDictionary)null);
        }

        /// <summary>
        /// 呈现指定MvcPagerAjax的分部视图，并使用指定的 System.Web.Mvc.ViewDataDictionary 对象替换其 ViewData 属性。
        /// </summary>
        /// <param name="htmlHelper">HTML 帮助器。</param>
        /// <param name="partialViewName">分部视图的名称。</param>
        /// <param name="viewData">分部视图的视图数据。</param>
        public static void MvcPagerAjaxRenderPartial(this HtmlHelper htmlHelper, string partialViewName, ViewDataDictionary viewData)
        {
            MvcPagerAjaxRenderPartial(htmlHelper, partialViewName, (object)null, viewData);
        }

        /// <summary>
        /// 呈现指定MvcPagerAjax的分部视图，并向其传递当前 System.Web.Mvc.ViewDataDictionary 对象的副本，但应将 Model 属性设置为指定的模型。
        /// </summary>
        /// <param name="htmlHelper">HTML 帮助器。</param>
        /// <param name="partialViewName">分部视图的名称。</param>
        /// <param name="model">用于分部视图的模型。</param>
        public static void MvcPagerAjaxRenderPartial(this HtmlHelper htmlHelper, string partialViewName, object model)
        {
            MvcPagerAjaxRenderPartial(htmlHelper, partialViewName, model, (ViewDataDictionary)null);
        }

        /// <summary>
        /// 呈现指定MvcPagerAjax的分部视图，使用指定的 System.Web.Mvc.ViewDataDictionary 对象替换分部视图的 ViewData 属性，并将视图数据的Model 属性设置为指定的模型。
        /// </summary>
        /// <param name="htmlHelper">HTML 帮助器。</param>
        /// <param name="partialViewName">分部视图的名称。</param>
        /// <param name="model">用于分部视图的模型。</param>
        /// <param name="viewData">分部视图的视图数据。</param>
        public static void MvcPagerAjaxRenderPartial(this HtmlHelper htmlHelper, string partialViewName, object model, ViewDataDictionary viewData)
        {
            HttpResponse httpResponse = HttpContext.Current.Response;

            AjaxOptions ajaxOptions = LKPageMvcPager分页配置.Ajax选项;
            
            httpResponse.Write("<div id=\"" + ajaxOptions.UpdateTargetId + "\">");
            RenderPartialExtensions.RenderPartial(htmlHelper, partialViewName, model, viewData);
            httpResponse.Write("</div>");
        }
        #endregion
    }
}