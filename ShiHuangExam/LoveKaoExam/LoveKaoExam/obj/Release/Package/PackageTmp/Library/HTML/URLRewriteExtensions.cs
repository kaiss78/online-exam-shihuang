using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKao.Page;
using LoveKaoExam.Data;
using LoveKaoExam.Library.CSharp;
using LoveKaoExam.Models;

namespace LoveKaoExam.Library.HTML
{
    public static class URLRewriteExtensions
    {
        public static MvcHtmlString 资源共享URL重写分类_详细(this HtmlHelper htmlHelper, List<所属分类> 分类列表, 爱考网资源方式 e爱考网资源方式)
        {
            string html = "";
            if (分类列表 == null || 分类列表.Count == 0)
            {
                html = "无";
            }
            else
            {
                List<string> 分类集合 = new List<string>();
                int count = 0;
                int total = 9;
                string tagAHTML = "";
                foreach (所属分类 item in 分类列表)
                {
                    count += item.分类名.Length + 1;
                    if (e爱考网资源方式 == 爱考网资源方式.下载)
                    {
                        tagAHTML = LKPageHtml.MvcTextTag_A(item.分类名, LKPageURLRewrite.爱考网_分类_详细(item.分类名), ATarget._blank);
                    }
                    else
                    {
                        tagAHTML = item.分类名;
                    }

                    if (count > total)
                    {
                        if (分类集合.Count == 0)
                        {
                            分类集合.Add(tagAHTML);
                        }
                        break;
                    }
                    else
                    {
                        分类集合.Add(tagAHTML);
                    }
                }
                html = string.Join(",", 分类集合.ToArray());
                if (count > total) { html += " ..."; }
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString 爱考网URL重写试题_预览(this HtmlHelper htmlHelper, Guid id)
        {
            string html = LKPageHtml.MvcTextTag_A("预览", new { href = LKPageURLRewrite.爱考网_试题_预览(id), target = ATarget._blank, title = "预览试题" });

            return MvcHtmlString.Create(html);
        }
        public static MvcHtmlString 系统URL重写试题_预览(this HtmlHelper htmlHelper, Guid id)
        {
            string html = LKPageHtml.MvcTextTag_A("预览", new { href = "javascript:void(0)", onclick = "ExaminerEmbedHandle.Subject.view('" + id + "');return false;", title = "预览试题" });

            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString 爱考网URL重写试卷_详细(this HtmlHelper htmlHelper, Guid id)
        {
            string html = LKPageHtml.MvcTextTag_A("详细", new { href = LKPageURLRewrite.爱考网_试卷_详细(id), target = ATarget._blank, title = "查看试卷详细" });
            return MvcHtmlString.Create(html);
        }
        public static MvcHtmlString 系统URL重写试卷_预览(this HtmlHelper htmlHelper, Guid id)
        {
            string html = LKPageHtml.MvcTextTag_A("预览", new { href = LKPageURLRewrite.系统_试卷_预览(id), target = ATarget._blank, title = "预览试卷" });
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString 资源共享URL重写试卷(this HtmlHelper htmlHelper, Guid 外部ID, Guid 内部ID, 爱考网资源方式 e爱考网资源方式, 爱考网资源类型 e爱考网资源类型)
        {
            MvcHtmlString sHtml = null;

            switch (e爱考网资源方式)
            {
                case 爱考网资源方式.上传:
                    switch (e爱考网资源类型)
                    {
                        case 爱考网资源类型.试题:
                            sHtml = 系统URL重写试题_预览(htmlHelper, 外部ID);
                            break;
                        case 爱考网资源类型.试卷:
                            sHtml = 系统URL重写试卷_预览(htmlHelper, 内部ID);
                            break;
                        default:
                            break;
                    }
                    break;
                case 爱考网资源方式.下载:
                    switch (e爱考网资源类型)
                    {
                        case 爱考网资源类型.试题:
                            sHtml = 爱考网URL重写试题_预览(htmlHelper, 外部ID);
                            break;
                        case 爱考网资源类型.试卷:
                            sHtml = 爱考网URL重写试卷_详细(htmlHelper, 外部ID);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            return sHtml;
        }


        public static MvcHtmlString 爱考网URL重写会员_详细(this HtmlHelper htmlHelper, Guid g会员ID, string s会员昵称)
        {
            string html = LKPageHtml.MvcTextTag_A(s会员昵称, new { href = LKPageURLRewrite.爱考网_会员_详细(g会员ID), target = ATarget._blank });
            return MvcHtmlString.Create(html);
        
         
        }

    }
}