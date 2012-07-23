using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 单选空格
    {
        #region 变量
        private 选项组 _选项组;
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


        [JsonIgnore]
        public Guid 选项组ID
        {
            get;
            set;
        }


        public 选项组 选项组
        {
            get
            {
                if (_选项组 == null)
                {
                    _选项组 = 选项组.选项组查询.Where(a => a.ID == this.选项组ID).First();
                    return _选项组;
                }
                return _选项组;
            }
            set
            {
                _选项组 = value;
            }
        }


        public Guid 答案ID
        {
            get;
            set;
        }


        public static IQueryable<单选空格> 单选空格查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from space in db.自由题空格表
                       join answer in db.自由题选项空格答案表
                       on space.ID equals answer.自由题空格ID
                       select new 单选空格
                       {
                           ID = space.ID,
                           答案ID = answer.自由题选项ID,
                           试题内容ID = space.试题内容ID,
                           选项组ID=space.自由题选项组ID.Value,
                           顺序 = space.顺序
                       };
            }
        }

        #endregion
    }
}
