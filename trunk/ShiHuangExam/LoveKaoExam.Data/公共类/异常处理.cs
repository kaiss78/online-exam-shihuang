using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace LoveKaoExam.Data
{
    public enum TagType
    {
        A, B, DIV, FONT, SPAN, TD, TR, TABLE, IMG,
        H1, H2, H3, H4, H5, H6
    }

    public class 异常处理
    {

        public static string LKTag(TagType eTagType, string context)
        {
            return LKTag(eTagType, context, (IDictionary<string, object>)null);
        }
        public static string LKTag(TagType eTagType, string context, object htmlattributes)
        {
            return LKTag(eTagType, context, new RouteValueDictionary(htmlattributes));
        }
        public static string LKTag(TagType eTagType, string context, IDictionary<string, object> htmlattributes)
        {
            TagBuilder tag = new TagBuilder(eTagType.ToString().ToLower());
            tag.MergeAttributes(htmlattributes);
            tag.InnerHtml = context;
            string sHtml = "";
            switch (eTagType)
            {
                case TagType.IMG:
                    sHtml = tag.ToString(TagRenderMode.SelfClosing);
                    break;
                default:
                    sHtml = tag.ToString(TagRenderMode.Normal);
                    break;
            }
            return sHtml;
        }



        /// <param name="类型">-1未绑定,1绑定账号被禁用，2禁止绑定任何账号，3没有足够的积分下载</param>
        public static void 抛出异常(int 类型)
        {
            switch (类型)
            {
                case -1:
                    {
                        throw new Exception(得到异常信息(-1) + LKTag(TagType.A, "我要绑定账号", new { href = "/Binding/LKAccount" }));
                    }
                case 1:
                    {
                        throw new Exception(得到异常信息(1));
                    }
                case 2:
                    {
                        throw new Exception(得到异常信息(2) + LKTag(TagType.A,
                            "登录爱考网更改设置", new {target ="_blank",title = "爱考网(lovekao.com)", href = "http://www.lovekao.com" }));
                    }
                case 3:
                    {
                        throw new Exception(得到异常信息(3));
                    }
                case 4:
                    {
                        throw new Exception(得到异常信息(4));
                    }
                case 5:
                    {
                        throw new Exception(得到异常信息(5));
                    }
                case 6:
                    {
                        throw new Exception(得到异常信息(6));
                    }
                case 7:
                    {
                        throw new Exception(得到异常信息(7));
                    }
                case 8:
                    {
                        throw new Exception(得到异常信息(8));
                    }
            }
        }



        public static string 得到异常信息(int 类型)
        {
            string message = string.Empty;
            switch (类型)
            {
                case -1:
                    {
                        message = "你未绑定爱考网账号！";
                        break;
                    }
                case 1:
                    {
                        message="(该绑定账号已被爱考网禁用，请更换其他账号绑定！)";
                        break;
                    }
                case 2:
                    {
                        message = "(该绑定账号在爱考网设置为禁止绑定账号！" + LKTag(TagType.A,
                            "登录爱考网更改设置", new { target = "_blank", title = "爱考网(lovekao.com)",
                                href = "http://www.lovekao.com" })+")";
                        break;
                    }
                case 3:
                    {
                        message = "你没有足够的可下载试题数，你可以先上传试题再下载！";
                        break;
                    }
                case 4:
                    {
                         message ="该账号在爱考网不存在！";
                         break;
                    }
                case 5:
                    {
                        message = "密码错误！";
                        break;
                    }
                case 6:
                    {
                         message ="你已绑定过该账号！";
                         break;
                    }
                case 7:
                    {
                         message ="该用户名已被他人使用，请更换其它用户名！";
                         break;
                    }
                case 8:
                    {
                         message ="该邮箱已被他人使用，请更换其它邮箱！";
                         break;
                    }
                case 9:
                    {
                        message = "考试未结束，暂不能查看排名！";
                        break;
                    }
                case 10:
                    {
                        message = "该场考试未公布考试结果，你不能查看考试排名！";
                        break;
                    }
                case 11:
                    {
                        message = "你未参加过该场考试，不能查看考试排名！";
                        break;
                    }
            }
            return message;
        }



        public static void Catch异常处理(string 异常信息)
        {
            if (异常信息.Contains("绑定") || 异常信息.Contains("试题") || 异常信息.Contains("账号")
                || 异常信息.Contains("密码") || 异常信息.Contains("用户名") || 异常信息.Contains("邮箱"))
            {
                throw new Exception(异常信息);
            }
            else
            {
                throw new Exception("连接爱考网服务器出错，请稍后再试！");
            }
        }



        public static string 得到无法连接爱考网异常信息()
        {
            return "连接爱考网服务器出错，请稍后再试！";
        }



      
        public static void 删除修改权限判断(Guid memberId)
        {
            if (用户信息.CurrentUser.用户名 == "0" || 用户信息.CurrentUser.用户名 == "1"||用户信息.CurrentUser.用户名=="2")
            {
                throw new Exception("试用账号不具有删除，修改权限！");
            }
            if (memberId != 用户信息.CurrentUser.用户ID)
            {
                throw new Exception("你不是创建人，无权删除！");
            }
        }



        public static void 删除修改权限判断()
        {
            if (用户信息.CurrentUser.用户名 == "0" || 用户信息.CurrentUser.用户名 == "1" || 用户信息.CurrentUser.用户名 == "2")
            {
                throw new Exception("试用账号不具有删除，修改权限！");
            }
        }

    }
}
