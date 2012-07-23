using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 连线题回答
    {
        #region 属性
     
        [JsonIgnore]
        public Guid 考生考试回答ID
        {
            get;
            set;
        }


       
        public Guid 回答的左选项ID
        {
            get;
            set;
        }


       
        public Guid 回答的右选项ID
        {
            get;
            set;
        }



        public static IQueryable<连线题回答> 连线题回答查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.考生连线题回答表.Select(a => new 连线题回答
                    {
                        回答的右选项ID = a.回答的选项ID,
                        回答的左选项ID = a.连线题左边小题ID,
                        考生考试回答ID = a.考生考试回答ID
                    });
            }
        }

        #endregion
    }
}
