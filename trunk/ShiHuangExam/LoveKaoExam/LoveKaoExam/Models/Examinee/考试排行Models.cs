using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoveKaoExam.Data;
using Webdiyer.WebControls.Mvc;
using LoveKao.Page;

namespace LoveKaoExam.Models.Examinee
{
    public class 考试排行Models
    {

        public 考试排行Models(PagedList<考试排名> pagedList考试排名, LKPageException lkPageException)
        {
            PagedList考试排名 = pagedList考试排名;
            LKPageException = lkPageException;
        }

        public PagedList<考试排名> PagedList考试排名 { get; set; }

        public LKPageException LKPageException { get; set; }
    }
}