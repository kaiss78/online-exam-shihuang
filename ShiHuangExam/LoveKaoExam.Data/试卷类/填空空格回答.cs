using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 填空空格回答
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


   
        public string 回答的内容
        {
            get;
            set;
        }



        public static IQueryable<填空空格回答> 填空空格回答查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.考生填空题回答表.Select(a => new 填空空格回答
                    {
                        回答的内容 = a.回答的内容,
                        考生考试回答ID = a.考生考试回答ID,
                        空格ID = a.该空ID
                    });
            }
        }

        #endregion
    }
}
