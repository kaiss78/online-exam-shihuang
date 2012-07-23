using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 连线题选项
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


        /// <summary>
        /// 0表示左，1表示右
        /// </summary>
        [JsonIgnore]
        public byte 连线标记
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


        public static IQueryable<连线题选项> 连线题选项查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.连线题选项表.Select(a => new 连线题选项
                    {
                        ID = a.ID,
                        连线标记 = a.连线标记,
                        试题内容ID = a.试题内容ID,
                        顺序 = a.顺序,
                        选项内容HTML = a.选项内容HTML,
                        选项内容文本 = a.选项内容文本
                    });
            }
        }
        #endregion
    }
}
