using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoveKaoExam.Models.Examiner
{
    /// <summary>
    /// 发布考试结果
    /// </summary>
    public class 公布考试结果Models
    {
        public Guid 考试设置ID { get; set; }

        public bool 是否公布考试结果 { get; set; }
    }
}