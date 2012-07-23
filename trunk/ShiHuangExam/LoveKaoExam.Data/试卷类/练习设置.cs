using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ExpressionMapper;

namespace LoveKaoExam.Data
{
    public class 练习设置
    {
        #region 变量

        private 试卷内容 _试卷内容;
        private 试卷外部信息 _试卷外部信息;
        private 用户 _设置人;
        private List<Guid> _考生ID集合;

        #endregion



        #region 属性

        public Guid 试卷内容ID
        {
            get;
            set;
        }


        public 试卷内容 试卷内容
        {
            get
            {
                if (_试卷内容 == null)
                {
                    _试卷内容 = 试卷内容.试卷内容查询.Where(a => a.ID == this.试卷内容ID).First();
                    return _试卷内容;
                }
                else
                {
                    return _试卷内容;
                }
            }
            set
            {
                _试卷内容 = value;
            }
        }


        [JsonIgnore]
        public 试卷外部信息 试卷外部信息
        {
            get
            {
                if (_试卷外部信息 == null)
                {
                    Guid testOutsideId = 试卷内容.试卷内容查询.Where(a => a.ID == this.试卷内容ID)
                        .Select(a => a.试卷外部信息ID).First();
                    _试卷外部信息 = 试卷外部信息.试卷外部信息查询.Where(a => a.ID == testOutsideId).First();
                    return _试卷外部信息;
                }
                else
                {
                    return _试卷外部信息;
                }
            }
            set
            {
                _试卷外部信息 = value;
            }
        }


        public short 考试时长
        {
            get;
            set;
        }


        public int 及格条件
        {
            get;
            set;
        }


        public Guid 设置人ID
        {
            get;
            set;
        }


        [JsonIgnore]
        public 用户 设置人
        {
            get
            {
                if (_设置人 == null)
                {
                    _设置人 = 用户.用户查询.Where(a => a.ID == this.设置人ID).First();
                    return _设置人;
                }
                else
                {
                    return _设置人;
                }
            }
            set
            {
                _设置人 = value;
            }
        }


        public DateTime 设置时间
        {
            get;
            set;
        }


        [JsonIgnore]
        public List<Guid> 考生ID集合
        {
            get
            {
                if (_考生ID集合 == null)
                {
                    LoveKaoExamEntities db = new LoveKaoExamEntities();
                    _考生ID集合 = db.考生范围表.Where(a => a.相关ID == this.试卷内容ID).Select(a => a.考生ID).ToList();
                    return _考生ID集合;
                }
                else
                {
                    return _考生ID集合;
                }
            }
            set
            {
                _考生ID集合 = value;
            }
        }


        public bool 是否删除
        {
            get;
            set;
        }


        public static IQueryable<练习设置> 练习设置查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.练习设置表.Select(a => new 练习设置
                    {
                        及格条件 = a.及格条件,
                        考试时长 = a.考试时长,
                        设置人ID = a.设置人ID,
                        设置时间=a.设置时间,
                        试卷内容ID = a.试卷内容ID,
                        是否删除=a.是否删除
                    });
            }
        }


        public static IQueryable<练习设置> 练习设置联合试卷内容查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from exerciseSet in db.练习设置表
                       join content in db.试卷内容表
                       on exerciseSet.试卷内容ID equals content.ID
                       select new 练习设置
                       {
                           及格条件 = exerciseSet.及格条件,
                           考试时长 = exerciseSet.考试时长,
                           试卷内容ID = exerciseSet.试卷内容ID,
                           设置人ID = exerciseSet.设置人ID,
                           设置时间=exerciseSet.设置时间,
                           试卷内容表 = content,
                           是否删除=exerciseSet.是否删除
                       };
            }
        }


        public static IQueryable<练习设置> 练习设置联合考生范围查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from exerciseSet in db.练习设置表
                       join examArea in db.考生范围表
                           on exerciseSet.试卷内容ID equals examArea.相关ID
                       select new 练习设置
                       {
                           及格条件 = exerciseSet.及格条件,
                           考试时长 = exerciseSet.考试时长,
                           试卷内容ID = exerciseSet.试卷内容ID,
                           设置人ID = exerciseSet.设置人ID,
                           设置时间 = exerciseSet.设置时间,
                           考生范围表 = examArea,
                           是否删除 = exerciseSet.是否删除
                       };
            }
        }


        public static IQueryable<练习设置> 练习设置联合考生范围联合试卷内容查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from exerciseSet in db.练习设置表
                       join examArea in db.考生范围表
                       on exerciseSet.试卷内容ID equals examArea.相关ID
                       join content in db.试卷内容表
                       on exerciseSet.试卷内容ID equals content.ID
                       select new 练习设置
                       {
                           及格条件 = exerciseSet.及格条件,
                           考试时长 = exerciseSet.考试时长,
                           试卷内容ID = exerciseSet.试卷内容ID,
                           设置人ID = exerciseSet.设置人ID,
                           设置时间 = exerciseSet.设置时间,
                           试卷内容表 = content,
                           是否删除 = exerciseSet.是否删除,
                           考生范围表 = examArea
                       };
            }
        }


        [JsonIgnore]
        public 试卷内容表 试卷内容表
        {
            get;
            set;
        }


        [JsonIgnore]
        public 考生范围表 考生范围表
        {
            get;
            set;
        }

        #endregion



        #region 方法

        /// <summary>
        /// 返回true能，false不能
        /// </summary>      
        public static bool 判断某试卷是否能设置练习(Guid 试卷内容ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            //若已设置成考试，则在考试结束后才能再设置成练习
            考试设置表 examSet = db.考试设置表.Where(a=>a.试卷内容ID==试卷内容ID&&a.是否删除==false)
                .OrderByDescending(a => a.考试结束时间).FirstOrDefault();
            if (examSet != null)
            {
                if (examSet.考试结束时间 > DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }          
        }



        public static 练习设置 预览练习设置试卷(Guid 练习设置ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            练习设置 exerciseSet = 练习设置查询.Where(a => a.试卷内容ID==练习设置ID).First();
            exerciseSet.试卷内容 = 试卷内容.给试卷内容中试题内容Json赋值(exerciseSet.试卷内容, false);
            return exerciseSet;
        }




        public static List<练习设置> 查询练习(string 试卷名,Guid 考生ID, int 第几页, int 页的大小, out int 返回总条数)
        {
            IQueryable<练习设置> query=练习设置联合考生范围联合试卷内容查询.Where(a => a.是否删除 == false
                && a.考生范围表.考生ID == 考生ID);
            if (String.IsNullOrEmpty(试卷名) == false)
            {
                query = query.Where(a => a.试卷内容表.名称.Contains(试卷名));
            }          
            返回总条数 = query.Count();
            List<练习设置> list = query.OrderByDescending(a => a.设置时间).Skip(第几页 * 页的大小).Take(页的大小).ToList();
            给试卷内容属性赋值(list);
            return list;
        }



        public static List<练习设置> 查询某考官组织的练习(string 试卷名,Guid 考官ID, int 第几页, int 页的大小, out int 返回总条数)
        {
            IQueryable<练习设置> query;
            if (String.IsNullOrEmpty(试卷名) == true)
            {
                query = 练习设置.练习设置查询;
            }
            else
            {
                query = 练习设置.练习设置联合试卷内容查询.Where(a => a.试卷内容表.名称.Contains(试卷名));
            }
            query = query.Where(a => a.设置人ID == 考官ID&&a.是否删除==false);
            返回总条数 = query.Count();
            List<练习设置> list = query.OrderByDescending(a => a.设置时间).Skip(第几页 * 页的大小).Take(页的大小).ToList();
            return list;
        }




        public static void 删除练习设置(Guid 试卷内容ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            练习设置表 exerciseSet = db.练习设置表.FirstOrDefault(a=>a.试卷内容ID==试卷内容ID);
            //只有设置人才能删除
            异常处理.删除修改权限判断(exerciseSet.设置人ID);
            db.练习设置表.DeleteObject(exerciseSet);
            //删除考生范围
            List<考生范围表> listArea = db.考生范围表.Where(a => a.相关ID == 试卷内容ID).ToList();
            foreach (var area in listArea)
            {
                db.考生范围表.DeleteObject(area);
            }
            //删除考生做过的练习记录
            List<考生做过的试卷表> listMemberDoneTest = db.考生做过的试卷表.Where(a => a.类型 == 0 && a.相关ID == 试卷内容ID).ToList();
            foreach (var memberDoneTest in listMemberDoneTest)
            {
                db.考生做过的试卷表.DeleteObject(memberDoneTest);
            }
            db.SaveChanges();
        }



        public static void 给练习设置列表赋值试卷外部信息属性(List<练习设置> listExerciseSet)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            List<Guid> listContentId = listExerciseSet.Select(a => a.试卷内容ID).ToList();
            List<Guid> listOutsideId = db.试卷内容表.Where(a => listContentId.Contains(a.ID))
                .Select(a => a.试卷外部信息ID).ToList();
            List<试卷外部信息> listOutside = 试卷外部信息.试卷外部信息查询.Where(a => listOutsideId.Contains(a.ID)).ToList();
            //给分类列表属性赋值
            List<所属分类> listBelongSort = 所属分类.所属分类查询.Where(a => listOutsideId.Contains(a.相关ID)).ToList();
            foreach (试卷外部信息 outside in listOutside)
            {
                outside.分类列表 = listBelongSort.Where(a => a.相关ID == outside.ID).Select(a=>a.分类名).ToList();
            }
            foreach (练习设置 exerciseSet in listExerciseSet)
            {
                试卷外部信息 outside = listOutside.FirstOrDefault(a => a.试卷内容ID == exerciseSet.试卷内容ID);
                if (outside != null)
                {
                    exerciseSet.试卷外部信息 = outside;
                }
            }
        }



        public static 练习设置 把练习设置表转化成练习设置(练习设置表 exerciseSet)
        {
            练习设置 练习设置 = Mapper.Create<练习设置表, 练习设置>()(exerciseSet);
            练习设置.设置人ID = exerciseSet.设置人ID;
            练习设置.试卷内容ID = exerciseSet.试卷内容ID;
            return 练习设置;
        }




        private static void 给试卷内容属性赋值(List<练习设置> listExerciseSet)
        {
            foreach (练习设置 exerciseSet in listExerciseSet)
            {
                exerciseSet.试卷内容 = 试卷内容.把试卷内容表转化成试卷内容(exerciseSet.试卷内容表);
            }
        }

        #endregion
    }
}
