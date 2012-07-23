using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 试卷中大题
    {
        #region 变量
        private List<试卷大题中试题> _试卷大题中试题;
        #endregion

        #region 属性
       
        public Guid ID
        {
            get;
            set;
        }

       
        [JsonIgnore]
        public Guid 试卷内容ID
        {
            get;
            set;
        }


       
        public string 名称
        {
            get;
            set;
        }

       
        public string 说明
        {
            get;
            set;
        }

        
        public decimal 多选题评分策略
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


      
        public List<试卷大题中试题> 试卷大题中试题集合
        {
            get
            {
                if (_试卷大题中试题 == null)
                {
                    _试卷大题中试题 = 试卷大题中试题.试卷大题中试题查询.Where(a => a.试卷中大题ID == this.ID).OrderBy(a => a.顺序).ToList();
                    return _试卷大题中试题;
                }
                return _试卷大题中试题;
            }
            set
            {
                _试卷大题中试题 = value;
            }
        }



        public static IQueryable<试卷中大题> 试卷中大题查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试卷中大题表.Select(a => new 试卷中大题
                    {
                        ID = a.ID,
                        多选题评分策略 = a.多选题评分策略,
                        名称 = a.名称,
                        试卷内容ID = a.试卷内容ID,
                        顺序 = a.顺序,
                        说明 = a.说明
                    });
            }
        }

        #endregion
    }
}
