using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoveKao.Page;
using LoveKaoExam.Data;

namespace LoveKaoExam.Models.Examiner
{
    public class 绑定账号信息Models
    {
        
        public 绑定账号信息Models(绑定账号表 c绑定账号表)
        {
            绑定账号表 = c绑定账号表;
            LKPageException = new LKPageException();
        }

        public 绑定账号信息Models(LKPageException lkPageException)
        {
            LKPageException = lkPageException;
        }

        public 绑定账号信息Models(绑定账号表 c绑定账号表, LKPageException lkPageException)
        {
            绑定账号表 = c绑定账号表;
            LKPageException = lkPageException;
        }

        /// <summary>
        /// 绑定账号表
        /// </summary>
        public 绑定账号表 绑定账号表 { get; set; }

        /// <summary>
        /// LoveKaoPage 程序异常
        /// </summary>
        public LKPageException LKPageException { get; set; }
    }
}