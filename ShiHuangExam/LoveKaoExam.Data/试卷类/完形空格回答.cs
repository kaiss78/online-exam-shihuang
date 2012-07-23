using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 完形空格回答
    {
        #region 属性
       
        [JsonIgnore]
        public Guid 考生考试回答ID
        {
            get;
            set;
        }

       
        public Guid 空格ID
        {
            get;
            set;
        }



       
        public Guid 回答的选项ID
        {
            get;
            set;
        }



        public static IQueryable<完形空格回答> 完形空格回答查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.考生多空选择题回答表.Select(a => new 完形空格回答
                    {
                        回答的选项ID = a.该空的回答ID,
                        考生考试回答ID = a.考生考试回答ID,
                        空格ID = a.该空ID
                    });
            }
        }
        #endregion
    }
}
