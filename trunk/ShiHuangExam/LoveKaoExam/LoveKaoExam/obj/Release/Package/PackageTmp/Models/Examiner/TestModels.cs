using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoveKaoExam.Models.Examiner
{
    /// <summary>
    /// 查找试题
    /// </summary>
    public class 查找试题
    {
        /// <summary>
        /// 查询的内容
        /// </summary>
        public string subContent { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 考官ID
        /// </summary>
        public Guid userID { get; set; }

    }
    /// <summary>
    /// 预览试卷
    /// </summary>
    public class 预览考官试卷 {
        /// <summary>
        /// 试卷标题
        /// </summary>
        public string TestTitle { get; set; }
        /// <summary>
        /// 试卷总分
        /// </summary>
        public int TestCount { get; set; }
        /// <summary>
        /// 考试时间
        /// </summary>
        public short TestTime { get; set; }
    
    }
}