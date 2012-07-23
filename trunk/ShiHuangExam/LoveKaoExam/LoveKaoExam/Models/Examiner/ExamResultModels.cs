using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoveKaoExam.Data;
using Webdiyer.WebControls.Mvc;

namespace LoveKaoExam.Models.Examiner
{
    public class ExamResultModels
    {
        public PagedList<考生做过的试卷> 考生做过的试卷 { get; set; }

        public 考试设置 考试设置 { get; set; }
    }
}