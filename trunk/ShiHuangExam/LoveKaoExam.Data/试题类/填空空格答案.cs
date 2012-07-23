using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 填空空格答案
    {
        #region 属性


        public string 答案内容
        {
            get;
            set;
        }


        [JsonIgnore]
        public Guid ID
        {
            get;
            set;
        }


        [JsonIgnore]
        public Guid 空格ID
        {
            get;
            set;
        }


        [JsonIgnore]
        public byte 顺序
        {
            get;
            set;
        }


        public static IQueryable<填空空格答案> 填空空格答案查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.自由题填空答案表.Select(a => new 填空空格答案
                    {
                        ID = a.ID,
                        答案内容 = a.该空答案,
                        空格ID = a.自由题空格ID,
                        顺序 = a.顺序
                    });
            }
        }

        #endregion
    }
}
