using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 连线题答案
    {
        #region 属性

        [JsonIgnore]
        public Guid ID
        {
            get;
            set;
        }


        [JsonIgnore]
        public Guid 试题内容ID
        {
            get;
            set;
        }


        public Guid 连线题左选项ID
        {
            get;
            set;
        }


        public Guid 连线题右选项ID
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


        public static IQueryable<连线题答案> 连线题答案查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.连线题答案表.Select(a => new 连线题答案
                    {
                        ID = a.ID,
                        连线题右选项ID = a.右边ID,
                        连线题左选项ID = a.左边ID,
                        试题内容ID = a.试题内容ID,
                        顺序 = a.顺序
                    });
            }
        }

        #endregion
    }
}
