using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using CodeSmith.Data.Linq;
using ExpressionMapper;

namespace LoveKaoExam.Data
{
    public class 考试设置
    {
        #region 变量

        private 试卷内容 _试卷内容;
        private 试卷外部信息 _试卷外部信息;
        private 用户 _设置人;
        private List<Guid> _考生ID集合;

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


        [JsonIgnore]
        public bool 是否公布考试结果
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


        [JsonIgnore]
        public bool 是否删除
        {
            get;
            set;
        }


        public bool 是否已考
        {
            get;
            set;
        }



        /// <summary>
        /// 0考试时间未到，1可以考试，2考试时间已过，3已考过
        /// </summary>
        public int 考试状态
        {
            get
            {
                if (考生做过的试卷.考生做过的试卷查询.Any(a => a.考生ID == 用户信息.CurrentUser.用户ID && a.类型 == 1
                     && a.相关ID == this.ID) == true)
                {
                    return 3;
                }
                else if (DateTime.Now < this.考试开始时间)
                {
                    return 0;
                }
                else if (DateTime.Now > this.考试结束时间)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
        }


        public string 考试状态名称
        {
            get
            {
                string state = string.Empty;
                switch (this.考试状态)
                {
                    case 0:
                        {
                            state= "时间未到";
                            break;
                        }
                    case 1:
                        {
                            state= "正在进行";
                            break;
                        }
                    case 2:
                        {
                            state= "时间已过";
                            break;
                        }
                }
                return state;
            }
        }


        public static IQueryable<考试设置> 考试设置查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.考试设置表.Select(a => new 考试设置
                    {
                        ID = a.ID,
                        及格条件 = a.及格条件,
                        考试结束时间 = a.考试结束时间,
                        考试开始时间 = a.考试开始时间,
                        考试时长 = a.考试时长,
                        试卷内容ID = a.试卷内容ID,
                        是否公布考试结果 = a.是否公布考试结果,
                        设置人ID = a.设置人ID,
                        设置时间=a.设置时间,
                        是否删除=a.是否删除
                    });               
            }
        }


        public static IQueryable<考试设置> 考试设置联合试卷内容查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from examSet in db.考试设置表
                       join content in db.试卷内容表
                       on examSet.试卷内容ID equals content.ID
                       select new 考试设置
                       {
                           ID = examSet.ID,
                           及格条件 = examSet.及格条件,
                           考试结束时间 = examSet.考试结束时间,
                           考试开始时间 = examSet.考试开始时间,
                           考试时长 = examSet.考试时长,
                           试卷内容ID = examSet.试卷内容ID,
                           是否公布考试结果 = examSet.是否公布考试结果,
                           设置人ID = examSet.设置人ID,
                           设置时间=examSet.设置时间,
                           试卷内容表 = content,
                           是否删除=examSet.是否删除
                       };
            }
        }


        public static IQueryable<考试设置> 考试设置联合考生范围联合试卷内容查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from examSet in db.考试设置表
                       join examArea in db.考生范围表
                       on examSet.ID equals examArea.相关ID
                       join content in db.试卷内容表
                       on examSet.试卷内容ID equals content.ID
                       select new 考试设置
                       {
                           ID = examSet.ID,
                           及格条件 = examSet.及格条件,
                           考试结束时间 = examSet.考试结束时间,
                           考试开始时间 = examSet.考试开始时间,
                           考试时长 = examSet.考试时长,
                           试卷内容ID = examSet.试卷内容ID,
                           是否公布考试结果 = examSet.是否公布考试结果,
                           设置人ID = examSet.设置人ID,
                           设置时间 = examSet.设置时间,
                           试卷内容表 = content,
                           是否删除 = examSet.是否删除,
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


        public static IQueryable<考试设置> 考试设置联合考生范围查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from examSet in db.考试设置表
                       join examScope in db.考生范围表
                       on examSet.ID equals examScope.相关ID
                       select new 考试设置
                       {
                           ID = examSet.ID,
                           及格条件 = examSet.及格条件,
                           考试结束时间 = examSet.考试结束时间,
                           考试开始时间 = examSet.考试开始时间,
                           考试时长 = examSet.考试时长,
                           设置人ID = examSet.设置人ID,
                           设置时间=examSet.设置时间,
                           试卷内容ID = examSet.试卷内容ID,
                           是否公布考试结果 = examSet.是否公布考试结果,
                           是否删除 = examSet.是否删除,
                           考生范围表 = examScope
                       };
            }
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
        public static bool 判断某试卷是否能设置考试(Guid 试卷内容ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            //是草稿试卷，不能设置成考试
            试卷内容表 testContent = db.试卷内容表.First(a => a.ID == 试卷内容ID);
            试卷外部信息表 testOutside = db.试卷外部信息表.First(a => a.ID == testContent.试卷外部信息ID);
            if (testOutside.试卷状态Enum == 4)
            {
                return false;
            }
            //试卷已设置成练习的话则不能再设置成考试
            if (db.练习设置表.Any(a => a.试卷内容ID == 试卷内容ID && a.是否删除 == false) == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        /// <summary>
        /// 返回0能修改，1考试时间内不能修改，只有考试开始时间前能修改
        /// 2考试时间已结束，不能修改，只有考试开始时间前能修改
        /// </summary>
        public static int 判断是否能修改考试设置(Guid 考试设置ID)
        {
            LoveKaoExamEntities db=new LoveKaoExamEntities();
            考试设置表 examSet=db.考试设置表.FirstOrDefault(a=>a.ID==考试设置ID);
            if (DateTime.Now >= examSet.考试开始时间 && DateTime.Now <= examSet.考试结束时间)
            {
                return 1;
            }
            else if (DateTime.Now > examSet.考试结束时间)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }




        public static 考试设置 预览考试设置试卷(Guid 考试设置ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            考试设置 examSet = 考试设置查询.Where(a => a.ID == 考试设置ID).First();
            examSet.试卷内容 = 试卷内容.给试卷内容中试题内容Json赋值(examSet.试卷内容, false);
            return examSet;
        }




        public static void 修改公布考试结果(Guid 考试设置ID, bool 是否公布考试结果)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            考试设置表 examSet = db.考试设置表.FirstOrDefault(a=>a.ID==考试设置ID);
            if (examSet.设置人ID != 用户信息.CurrentUser.用户ID)
            {
                throw new Exception("您无权修改！");
            }
            examSet.是否公布考试结果 = 是否公布考试结果;
            db.SaveChanges();
        }



        
        public static DateTime 得到某试卷最后一次组织考试结束时间(Guid 试卷内容ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            return db.考试设置表.Where(a => a.试卷内容ID == 试卷内容ID).OrderByDescending(a => a.考试结束时间)
                .Select(a => a.考试结束时间).FirstOrDefault();
        }



  
       
        /// <param name="类型">0全部，1未考过，2已考过</param>      
        public static List<考试设置> 查询考试(string 试卷名,Guid 考生ID,int 类型, int 第几页, int 页的大小,out int 返回总条数)
        {
            IQueryable<考试设置> query = 考试设置联合考生范围联合试卷内容查询.Where(a => a.是否删除 == false
                && a.考生范围表.考生ID == 考生ID);
            if (String.IsNullOrEmpty(试卷名) == false)
            {
                query = query.Where(a => a.试卷内容表.名称.Contains(试卷名));
            }
            if (类型 == 0)
            {
                返回总条数 = query.Count();
                List<考试设置> list = query.OrderByDescending(a => a.考试开始时间).Skip(第几页 * 页的大小).Take(页的大小).ToList();
                给试卷内容属性赋值(list);
                给考试设置列表赋值是否已考属性(list, 考生ID);
                return list;
            }
            else
            {
                List<考试设置> listAllExamSet = query.ToList();
                给考试设置列表赋值是否已考属性(listAllExamSet, 考生ID);
                List<考试设置> listExamSet;
                if (类型 == 1)
                {
                    listExamSet = listAllExamSet.Where(a => a.是否已考 == false).ToList();
                }
                else
                {
                    listExamSet = listAllExamSet.Where(a => a.是否已考 == true).ToList();
                }
                返回总条数 = listExamSet.Count;
                listExamSet = listExamSet.OrderByDescending(a => a.考试开始时间).Skip(第几页 * 页的大小).Take(页的大小).ToList();
                return listExamSet;
            }          
        }



        private static void 给考试设置列表赋值是否已考属性(List<考试设置> listExamSet, Guid 考生ID)
        {
            List<Guid> listExamSetId = listExamSet.Select(a => a.ID).ToList();
            List<Guid> listDoneExamSetId = 考生做过的试卷.考生做过的试卷查询.Where(a => a.考生ID == 考生ID && a.类型 == 1
                && listExamSetId.Contains(a.相关ID)).Select(a => a.相关ID).ToList();
            foreach (考试设置 examSet in listExamSet)
            {
                if (listDoneExamSetId.Contains(examSet.ID))
                {
                    examSet.是否已考 = true;
                }
                else
                {
                    examSet.是否已考 = false;
                }
            }
        }



        public static List<考试设置> 查询某考官组织的考试(string 试卷名,Guid 考官ID, int 第几页, int 页的大小, out int 返回总条数)
        {
            IQueryable<考试设置> query;
            if (String.IsNullOrEmpty(试卷名) == true)
            {
                query = 考试设置.考试设置查询;
            }
            else
            {
                query = 考试设置.考试设置联合试卷内容查询.Where(a => a.试卷内容表.名称.Contains(试卷名));
            }
            query = query.Where(a => a.设置人ID == 考官ID&&a.是否删除==false);
            返回总条数 = query.Count();
            List<考试设置> list = query.OrderByDescending(a => a.考试开始时间).Skip(第几页 * 页的大小).Take(页的大小).ToList();
            return list;
        }



       
        public static void 删除考试设置(Guid 考试设置ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            考试设置表 examSet = db.考试设置表.FirstOrDefault(a=>a.ID==考试设置ID);
            //只有设置人才能删除
            异常处理.删除修改权限判断(examSet.设置人ID);
            //考试时间内不能删除
            if (DateTime.Now >= examSet.考试开始时间 && DateTime.Now <= examSet.考试结束时间)
            {
                throw new Exception("考试时间内不能删除！");
            }
            db.考试设置表.DeleteObject(examSet);
            //删除考生范围
            List<考生范围表> listArea = db.考生范围表.Where(a => a.相关ID == 考试设置ID).ToList();
            foreach (var area in listArea)
            {
                db.考生范围表.DeleteObject(area);
            }
            //删除考生做过的练习记录
            List<考生做过的试卷表> listMemberDoneTest = db.考生做过的试卷表.Where(a => a.类型 == 1 && a.相关ID == 考试设置ID).ToList();
            foreach (var memberDoneTest in listMemberDoneTest)
            {
                db.考生做过的试卷表.DeleteObject(memberDoneTest);
            }
            db.SaveChanges();
        }



        public static void 给考试设置列表赋值试卷外部信息属性(List<考试设置> listExamSet)
        {
            LoveKaoExamEntities db=new LoveKaoExamEntities();
            List<Guid> listContentId = listExamSet.Select(a => a.试卷内容ID).ToList();
            List<Guid> listOutsideId = db.试卷内容表.Where(a => listContentId.Contains(a.ID))
                .Select(a => a.试卷外部信息ID).ToList();
            List<试卷外部信息> listOutside = 试卷外部信息.试卷外部信息查询.Where(a => listOutsideId.Contains(a.ID)).ToList();
            //给分类列表属性赋值
            List<所属分类> listBelongSort = 所属分类.所属分类查询.Where(a => listOutsideId.Contains(a.相关ID)).ToList();
            foreach (试卷外部信息 outside in listOutside)
            {
                outside.分类列表 = listBelongSort.Where(a => a.相关ID == outside.ID).Select(a=>a.分类名).ToList();
            }
            //给试卷外部信息属性赋值
            foreach (考试设置 examSet in listExamSet)
            {
                试卷外部信息 outside=listOutside.FirstOrDefault(a => a.试卷内容ID == examSet.试卷内容ID);
                if (outside != null)
                {
                    examSet.试卷外部信息 = outside;
                }
            }
        }



        public static void 给考试设置列表赋值试卷内容属性(List<考试设置> listExamSet)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            List<Guid> listContentId = listExamSet.Select(a => a.试卷内容ID).ToList();
            List<试卷内容> listContent = 试卷内容.试卷内容查询.Where(a => listContentId.Contains(a.ID)).ToList();
            foreach (考试设置 examSet in listExamSet)
            {
                examSet.试卷内容 = listContent.First(a => a.ID == examSet.试卷内容ID);
            }
        }



        public static 考试设置 把考试设置表转化成考试设置(考试设置表 examSet)
        {
            考试设置 考试设置 = Mapper.Create<考试设置表, 考试设置>()(examSet);
            考试设置.ID = examSet.ID;
            考试设置.设置人ID = examSet.设置人ID;
            考试设置.试卷内容ID = examSet.试卷内容ID;
            return 考试设置;
        }




        private static void 给试卷内容属性赋值(List<考试设置> listExamSet)
        {
            foreach (考试设置 examSet in listExamSet)
            {
                examSet.试卷内容 = 试卷内容.把试卷内容表转化成试卷内容(examSet.试卷内容表);
            }
        }

        #endregion
    }
}
