using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 选词空格
    {
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



        public Guid 答案ID
        {
            get;
            set;
        }



        public static IQueryable<选词空格> 选词空格查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from space in db.自由题空格表
                       join answer in db.自由题选项空格答案表
                       on space.ID equals answer.自由题空格ID
                       select new 选词空格
                       {
                           ID = space.ID,
                           答案ID = answer.自由题选项ID,
                           试题内容ID = space.试题内容ID,
                           顺序 = space.顺序
                       };
            }
        }
       
        #endregion
    }
}
