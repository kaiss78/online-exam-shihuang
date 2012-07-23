using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 选项
    {
        #region 属性

        public Guid ID
        {
            get;
            set;
        }


        public string 选项内容HTML
        {
            get;
            set;
        }


        public string 选项内容文本
        {
            get;
            set;
        }

       
        [JsonIgnore]
        public Guid 选项组ID
        {
            get;
            set;
        }


        [JsonIgnore]
        public short 顺序
        {
            get;
            set;
        }


        public static IQueryable<选项> 选项查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.自由题选项表.Select(a => new 选项
                    {
                        ID = a.ID,
                        顺序 = a.顺序,
                        选项内容HTML = a.选项内容HTML,
                        选项内容文本 = a.选项内容文本,
                        选项组ID = a.自由题选项组ID
                    });
            }
        }

        #endregion
    }
}
