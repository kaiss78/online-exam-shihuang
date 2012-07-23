using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ExpressionMapper;

namespace LoveKaoExam.Data
{
    public class 试卷设置
    {
        #region 变量

        private List<Guid> _考生ID集合;

        #endregion



        #region 属性

        public Guid ID
        {
            get;
            set;
        }


        public Guid 试卷内容ID
        {
            get;
            set;
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


        public DateTime 设置时间
        {
            get;
            set;
        }


        public bool 是否删除
        {
            get;
            set;
        }


        public DateTime 考试开始时间
        {
            get;
            set;
        }


        public DateTime 考试结束时间
        {
            get;
            set;
        }


        public bool 是否公布考试结果
        {
            get;
            set;
        }


        /// <summary>
        /// 0练习设置，1考试设置
        /// </summary>
        public int 设置类型
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
                    _考生ID集合 = db.考生范围表.Where(a => a.相关ID == this.ID).Select(a => a.考生ID).ToList();
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

        #endregion



        #region 方法

        public static void 添加试卷设置(试卷设置 试卷设置)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            if (试卷设置.设置类型 == 1)
            {
                //未设置成练习才能设置成考试
                Guid outsideId = db.试卷内容表.FirstOrDefault(a=>a.ID==试卷设置.试卷内容ID).试卷外部信息ID;
                List<Guid> listContentId = db.试卷内容表.Where(a=>a.试卷外部信息ID==outsideId).Select(a=>a.ID).ToList();
                if (db.练习设置表.Any(a => listContentId.Contains(a.试卷内容ID) && a.是否删除 == false) == true)
                {
                    throw new Exception("该试卷已设置成练习，若要设置成考试，请先删除练习设置！");
                }
                //只有出卷人才能设置
                试卷内容 testContent = 试卷内容.试卷内容查询.Where(a => a.ID == 试卷设置.试卷内容ID).First();
                if (testContent.提交人ID != 试卷设置.设置人ID)
                {
                    throw new Exception("您无权设置该试卷！");
                }
                //开始时间不能大于结束时间
                if (试卷设置.考试开始时间 >= 试卷设置.考试结束时间)
                {
                    throw new Exception("考试开始时间不能大于考试结束时间！");
                }
                考试设置表 examSet = new 考试设置表();
                examSet.ID = Guid.NewGuid();
                examSet.及格条件 = 试卷设置.及格条件;
                examSet.考试结束时间 = 试卷设置.考试结束时间;
                examSet.考试开始时间 = 试卷设置.考试开始时间;
                examSet.考试时长 = 试卷设置.考试时长;
                examSet.试卷内容ID = 试卷设置.试卷内容ID;
                examSet.是否公布考试结果 = 试卷设置.是否公布考试结果;
                examSet.设置人ID = 试卷设置.设置人ID;
                examSet.设置时间 = DateTime.Now;
                examSet.是否删除 = false;
                List<考生范围表> listArea = new List<考生范围表>();
                foreach (Guid examineeId in 试卷设置.考生ID集合)
                {
                    考生范围表 area = new 考生范围表();
                    area.ID = Guid.NewGuid();
                    area.考生ID = examineeId;
                    area.相关ID = examSet.ID;
                    listArea.Add(area);
                }
                db.考试设置表.AddObject(examSet);
                foreach (var area in listArea)
                {
                    db.考生范围表.AddObject(area);
                }
                db.SaveChanges();
            }
            else
            {
                //若试卷已设置成考试，则要在考试结束后或删除考试设置才能设置成练习
                Guid outsideId = db.试卷内容表.FirstOrDefault(a=>a.ID==试卷设置.试卷内容ID).试卷外部信息ID;
                List<Guid> listContentId = db.试卷内容表.Where(a=>a.试卷外部信息ID==outsideId).Select(a => a.ID).ToList();
                考试设置表 examSet = db.考试设置表.Where(a=>listContentId.Contains(a.试卷内容ID)&& a.是否删除 == false)
                    .OrderByDescending(a => a.考试结束时间).FirstOrDefault();
                if (examSet != null)
                {
                    if (examSet.考试结束时间 > DateTime.Now)
                    {
                        throw new Exception("该试卷已设置成考试，若要设置成练习，需在考试结束时间以后或删除考试设置！");
                    }
                }
                //只有出卷人才能设置
                试卷内容 testContent = 试卷内容.试卷内容查询.Where(a => a.ID == 试卷设置.试卷内容ID).First();
                if (testContent.提交人ID != 试卷设置.设置人ID)
                {
                    throw new Exception("您无权设置该试卷！");
                }
                //若该试卷以前有设置成练习的记录，且被删除了，则更新设置，并恢复删除,若没有，则添加新的设置
                练习设置表 dbExerciseSet = db.练习设置表.FirstOrDefault(a=>a.试卷内容ID==试卷设置.试卷内容ID);
                if (dbExerciseSet == null)
                {
                    练习设置表 exerciseSet = new 练习设置表();
                    exerciseSet.及格条件 = 试卷设置.及格条件;
                    exerciseSet.考试时长 = 试卷设置.考试时长;
                    exerciseSet.设置人ID = 试卷设置.设置人ID;
                    exerciseSet.设置时间 = DateTime.Now;
                    exerciseSet.试卷内容ID = 试卷设置.试卷内容ID;
                    exerciseSet.是否删除 = false;
                    db.练习设置表.AddObject(exerciseSet);
                    List<考生范围表> listArea = new List<考生范围表>();
                    foreach (Guid examineeId in 试卷设置.考生ID集合)
                    {
                        考生范围表 area = new 考生范围表();
                        area.ID = Guid.NewGuid();
                        area.考生ID = examineeId;
                        area.相关ID = exerciseSet.试卷内容ID;
                        listArea.Add(area);
                    }
                    foreach (var area in listArea)
                    {
                        db.考生范围表.AddObject(area);
                    }
                }
                else
                {
                    dbExerciseSet.及格条件 = 试卷设置.及格条件;
                    dbExerciseSet.考试时长 = 试卷设置.考试时长;
                    dbExerciseSet.设置时间 = DateTime.Now;
                    dbExerciseSet.是否删除 = false;
                    考生范围.更新考生范围(试卷设置.试卷内容ID, 试卷设置.考生ID集合, db);
                }
                db.SaveChanges();
            }
        }



       

        public static void 修改试卷设置(试卷设置 试卷设置)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            if (试卷设置.设置类型 == 1)
            {
                考试设置表 examSet = db.考试设置表.FirstOrDefault(a=>a.ID==试卷设置.ID);
                //只有设置人自己才能修改
                异常处理.删除修改权限判断(examSet.设置人ID);
                //考试时间开始以后，就不能修改了
                if (DateTime.Now >= examSet.考试开始时间 && DateTime.Now <= examSet.考试结束时间)
                {
                    throw new Exception("考试时间内不能修改，只有考试开始时间前能修改！");
                }
                if (DateTime.Now > examSet.考试结束时间)
                {
                    throw new Exception("考试时间已过，不能修改，只有考试开始时间前能修改！");
                }
                examSet.及格条件 = 试卷设置.及格条件;
                examSet.考试结束时间 = 试卷设置.考试结束时间;
                examSet.考试开始时间 = 试卷设置.考试开始时间;
                examSet.考试时长 = 试卷设置.考试时长;
                //更新考生范围
                考生范围.更新考生范围(试卷设置.ID, 试卷设置.考生ID集合, db);
                db.SaveChanges();
            }
            else
            {
                练习设置表 exerciseSet = db.练习设置表.FirstOrDefault(a=>a.试卷内容ID==试卷设置.试卷内容ID);
                //只有设置人自己才能修改
                异常处理.删除修改权限判断(exerciseSet.设置人ID);
                exerciseSet.及格条件 = 试卷设置.及格条件;
                exerciseSet.考试时长 = 试卷设置.考试时长;
                exerciseSet.设置时间 = DateTime.Now;
                考生范围.更新考生范围(试卷设置.试卷内容ID, 试卷设置.考生ID集合, db);
                db.SaveChanges();
            }
        }



       
        /// <param name="设置ID">练习设置为试卷内容ID，考试设置为考试设置ID</param>
        /// <param name="设置类型">0练习设置，1考试设置</param>
        /// <returns></returns>
        public static 试卷设置 得到某试卷设置根据ID(Guid 设置ID,int 设置类型)
        {
            if (设置类型 == 1)
            {
                考试设置 examSet = 考试设置.考试设置查询.Where(a => a.ID == 设置ID).First();
                试卷设置 testSet = 把考试设置转化成试卷设置(examSet);
                testSet.设置类型 = 1;
                return testSet;
            }
            else
            {
                练习设置 exerciseSet = 练习设置.练习设置查询.Where(a => a.试卷内容ID == 设置ID).First();
                试卷设置 testSet = 把练习设置转化成试卷设置(exerciseSet);
                return testSet;
            }
        }



        private static 试卷设置 把考试设置转化成试卷设置(考试设置 考试设置)
        {
            试卷设置 testSet = Mapper.Create<考试设置, 试卷设置>()(考试设置);
            return testSet;
        }



        private static 试卷设置 把练习设置转化成试卷设置(练习设置 练习设置)
        {
            试卷设置 testSet = Mapper.Create<练习设置, 试卷设置>()(练习设置);
            return testSet;
        }
        #endregion
    }
}
