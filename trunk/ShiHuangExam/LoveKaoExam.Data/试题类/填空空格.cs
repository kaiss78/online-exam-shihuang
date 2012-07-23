using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 填空空格
    {
        #region 变量
        private List<填空空格答案> _填空空格答案集合;
        #endregion

        #region 属性

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


        [JsonIgnore]
        public byte 顺序
        {
            get;
            set;
        }


        public List<填空空格答案> 填空空格答案集合
        {
            get
            {
                if (_填空空格答案集合 == null)
                {
                    _填空空格答案集合 = 填空空格答案.填空空格答案查询.Where(a => a.空格ID == this.ID).OrderBy(a => a.顺序).ToList();
                    return _填空空格答案集合;
                }
                return _填空空格答案集合;
            }
            set
            {
                _填空空格答案集合 = value;
            }
        }


        public static IQueryable<填空空格> 填空空格查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.自由题空格表.Select(a => new 填空空格
                    {
                        ID = a.ID,
                        试题内容ID = a.试题内容ID,
                        顺序 = a.顺序
                    });
            }
        }

        #endregion
    }
}
