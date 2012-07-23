using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 试卷大题中试题
    {
        #region 变量
        private 试题内容 _试题内容;
        private 试卷中大题 _试卷中大题;
        private decimal _每小题分值;
        #endregion


        #region 属性
       
        public Guid ID
        {
            get;
            set;
        }

      
        [JsonIgnore]
        public Guid 试卷中大题ID
        {
            get;
            set;
        }

        
        [JsonIgnore]
        public 试卷中大题 试卷中大题
        {
            get
            {
                if (_试卷中大题 == null)
                {
                    _试卷中大题 = 试卷中大题.试卷中大题查询.Where(a => a.ID == this.试卷中大题ID).First();
                    return _试卷中大题;
                }
                return _试卷中大题;
            }
            set
            {
                _试卷中大题 = value;
            }
        }

       
        public Guid 试题内容ID
        {
            get;
            set;
        }


       
        [JsonIgnore]
        public 试题内容 试题内容
        {            
            get;
            set;
        }



        public string 试题内容Json
        {
            get;
            set;
        }

       
        public decimal 每小题分值
        {
            get
            {
                string score = _每小题分值.ToString("G0");
                _每小题分值 = Convert.ToDecimal(score);
                return _每小题分值;
            }
            set
            {
                _每小题分值 = value;
            }
        }

        
        [JsonIgnore]
        public short 顺序
        {
            get;
            set;
        }


        public 考生考试回答 该题考试回答
        {
            get;
            set;
        }


        /// <summary>
        /// 复合题和多题干共选项题时读此属性
        /// </summary>
        public List<试卷大题中试题> 子小题集合
        {
            get;
            set;
        }



        public static IQueryable<试卷大题中试题> 试卷大题中试题查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试卷大题中试题表.Select(a => new 试卷大题中试题
                    {
                        ID = a.ID,
                        每小题分值 = a.每小题分值,
                        试卷中大题ID = a.试卷中大题ID,
                        试题内容ID = a.试题内容ID,
                        顺序 = a.顺序
                    });
            }
        }
        #endregion
    }
}
