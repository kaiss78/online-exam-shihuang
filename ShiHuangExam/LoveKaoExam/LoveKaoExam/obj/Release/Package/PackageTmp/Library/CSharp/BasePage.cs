using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using LoveKaoExam.Models;
using LoveKao.Page;

namespace LoveKaoExam.Library.CSharp
{
    public class BasePage : LKPageBasePage
    {
        /// <summary>
        /// 考试系统名
        /// </summary>
        public static string LKExamName= "石黄高速考试系统";
        /// <summary>
        /// 考试系统名的扩展分类
        /// </summary>
        public static string LKExamSort = "-石黄高速在线考试系统";
        /// <summary>
        /// 考试系统版本
        /// </summary>
        public static string Version = "v1.0版";
        
        public static string Description = "石黄高速内部在线考试系统";
        public static string KeyWords = "石黄高速,在线考试";
       



        private static string pageTitle;

        /// <summary>
        /// 网页标题，石黄高速考试系统v1.0版本
        /// </summary>
        public static string PageTitle {
            get
            {
                if (pageTitle == null)
                {
                    pageTitle = " - " + LKExamName + LKExamSort + Version;
                }
                return pageTitle;
            }
            set {
                pageTitle = value;
            }
        }
        /// <summary>
        /// 登录页面的标题
        /// </summary>
        public static string LogonTitle {
            get {
                return LKExamName + LKExamSort + Version;
            }
        
        }
    }
}
