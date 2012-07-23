using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    public class 分类名称和分类类别名称 : JsonConverter
    {
        public string 分类名
        {
            get;
            set;
        }


        public string 类型
        {
            get;
            set;
        }


       

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            系统分类表 sort = (系统分类表)value;

            JObject o = new JObject();
            o["分类名"] = new JValue(sort.分类名称);
            o["类型"] = new JValue(sort.分类类别名称);
            o.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        {
            // convert back json string into array of picture 
            return null; ;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(系统分类表);
        }
    }


    public class 分类
    {
        #region 静态属性
        //private static string cacheGroupName = "系统分类表";


        //public static CacheSettings cacheSetting
        //{
        //    get
        //    {
        //        CacheSettings setting = new CacheSettings();

        //        setting.Group = cacheGroupName;
        //        setting.Duration = TimeSpan.FromHours(4);
        //        // setting.Duration = TimeSpan.FromSeconds(1);

        //        setting.Mode = CodeSmith.Data.Caching.CacheExpirationMode.Sliding;


        //        return setting;
        //    }
        //}

        //private const string cacheRelationGroupName = "总分类关系";
        //public static CacheSettings cacheSettingRelation
        //{
        //    get
        //    {
        //        CacheSettings setting = new CacheSettings();

        //        setting.Group = cacheRelationGroupName;
        //        setting.Duration = TimeSpan.FromHours(4);
        //        // setting.Duration = TimeSpan.FromSeconds(1);
        //        setting.Mode = CodeSmith.Data.Caching.CacheExpirationMode.Duration;
        //        return setting;
        //    }
        //}

        #endregion

        #region 属性



       
        public string 分类名称
        {
            get;
            set;
        }

       
        public Guid 创建人ID
        {
            get;
            set;
        }


        public 用户 创建人
        {
            get;
            set;
        }



      
        public DateTime 创建时间
        {
            get;
            set;
        }

        /// <summary>
        /// 0代表系统分类，1代表扩展分类
        /// </summary>       
        public byte 分类类型
        {
            get;
            set;
        }

       
        public string 同义词分类名称
        {
            get;
            set;
        }

       
        public string 分类类别名称
        {
            get;
            set;
        }

        public int 试题和试卷数量
        {
            get;
            set;
        }


        public List<string> 同义词集合
        {
            get;
            set;
        }

        public int 试题数量
        {
            get;
            set;
        }


        public int 试卷数量
        {
            get;
            set;
        }



        public static IQueryable<分类> 分类查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.系统分类表.Select(a => new 分类
                    {
                        创建人ID = a.操作人ID,
                        创建时间 = a.操作时间,
                        分类类别名称 = a.分类类别名称,
                        分类类型 = a.分类类型,
                        分类名称 = a.分类名称,
                        同义词分类名称 = a.同义词分类名称
                    });
            }
        }

        #endregion



        #region 方法
     

        public static List<分类名称和分类类别名称> 判断分类是否存在(string 分类集合字符串)
        {
            List<string> 分类集合 = 分类集合字符串.Split(",".ToCharArray()).ToList();
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            //去空格和重复
            分类集合 = 分类集合.Distinct().ToList();
            List<string> listNullSort = 分类集合.Where(a => a == "").ToList();
            分类集合 = 分类集合.Except(listNullSort).ToList();
            List<string> listExistSortName = db.系统分类表.Where(a => 分类集合.Contains(a.分类名称))
                .Select(a => a.分类名称).ToList();
            分类集合 = 分类集合.Except(listExistSortName).ToList();
            List<分类名称和分类类别名称> listBelongSort = new List<分类名称和分类类别名称>();
            foreach (string sort in 分类集合)
            {
                分类名称和分类类别名称 newSort = new 分类名称和分类类别名称();
                newSort.分类名 = sort;
                listBelongSort.Add(newSort);
            }
            return listBelongSort;
        }



        /// <summary>
        /// 去掉重复的，空的，和父级分类
        /// </summary>
        public static List<string> 分类处理(List<string> listSort)
        {
            listSort = listSort.Distinct().ToList();
            List<string> listEmptySort = listSort.Where(a => a == "").ToList();
            listSort = listSort.Except(listEmptySort).ToList();
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            listSort=去掉父级分类(listSort, db);
            return listSort;
        }



        public static List<string> 得到下一级分类(string 分类名)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            List<string> listSortName = new List<string>();
            if (分类名 == "开放分类")
            {
                listSortName = db.系统分类表.Where(a => a.分类类型 == 1).Select(a => a.分类名称).ToList();
            }
            else
            {
                listSortName = db.系统分类上下级关系表.Where(a => a.系统分类名称 == 分类名 && a.是否上级 == false)
                    .Select(a => a.分类关系名称).Union(db.系统分类上下级关系表.Where(a => a.分类关系名称 == 分类名 && a.是否上级 == true)
                    .Select(a => a.系统分类名称)).ToList();
            }
            return listSortName;
        }



        public static List<分类名称和分类类别名称> 得到某会员最近使用分类(Guid 会员ID, int 需返回条数)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            DateTime minTime=DateTime.Now.AddDays(-7);
            //取最近7天使用过的分类
            List<所属分类表> listDBSort = db.所属分类表.Where(a => a.操作人ID == 会员ID
                && a.操作时间 > minTime).OrderByDescending(a => a.操作时间).ToList();
            List<string> listBelongSortName=listDBSort.Select(a => a.分类名).Distinct().Take(需返回条数).ToList();
            List<分类名称和分类类别名称> listSort = new List<分类名称和分类类别名称>();
            foreach (string belongSortName in listBelongSortName)
            {
                分类名称和分类类别名称 sort = new 分类名称和分类类别名称();
                sort.分类名 = belongSortName;
                sort.类型 = "";
                listSort.Add(sort);
            }
            return listSort;
        }




        public static List<string> 去掉父级分类(List<string> listSort, LoveKaoExamEntities db)
        {
            List<string> listNoParentSort = listSort;
            List<系统分类上下级关系表> listRelation = db.系统分类上下级关系表.ToList();                       
            foreach (string sort in listSort)
            {
                string 分类名 = sort.ToLower();
                //查找父级分类
                List<string> listParentSort = listRelation.Where(a => a.系统分类名称.ToLower() == 分类名 && a.是否上级 == true)
               .Select(a => a.分类关系名称).Union(listRelation.Where(a => a.分类关系名称.ToLower() == 分类名 && a.是否上级 == false)
               .Select(a => a.系统分类名称)).ToList();
                listNoParentSort = listNoParentSort.Except(listParentSort).ToList();
            }
            return listNoParentSort;

        }


          


        //public static List<分类> 得到所有未设置上下级关系的系统分类(int 第几页, int 页的大小, out int 返回总条数)
        //{
        //    LoveKaoDataContext db = new LoveKaoDataContext();
        //    List<分类> listSystem = SmartEntity.GetQueryList<分类>().Where(a => a.分类类型 == 0 && a.同义词分类名称 == "").ToList();
        //    List<string> listSystemSort = listSystem.Select(a => a.分类名称).ToList();
        //    List<系统分类上下级关系表> listRelation = db.系统分类上下级关系表.ToList();
        //    List<string> listRelationSort = listRelation.Select(a => a.系统分类名称).ToList();
        //    List<string> listRelationSort1 = listRelation.Select(a => a.分类关系名称).ToList();
        //    listRelationSort = listRelationSort.Union(listRelationSort1).ToList();
        //    listSystemSort = listSystemSort.Except(listRelationSort).ToList();
        //    List<分类> list = listSystem.Where(a => listSystemSort.Contains(a.分类名称)).OrderByDescending(a => a.操作时间).ToList();
        //    返回总条数 = list.Count;
        //    list = list.Skip(第几页 * 页的大小).Take(页的大小).ToList();
        //    List<Guid> listMemberId = list.Select(a => a.操作人ID).ToList();
        //    List<会员> listMember = SmartEntity.GetQueryList<会员>().Where(a => listMemberId.Contains<Guid>(a.ID)).ToList();
        //    foreach (分类 sort in list)
        //    {
        //        sort.创建人 = listMember.Where(a => a.ID == sort.操作人ID).First();
        //    }
        //    return list;
        //}



        public static List<string> 得到分类智能提示(string 分类名, int 返回条数)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            List<系统分类表> listSystemSort = db.系统分类表.ToList();
            string 小写分类名称 = 分类名.ToLower();
            List<string> listSort = listSystemSort.Where(a => a.分类名称.ToLower().StartsWith(小写分类名称))
                .Select(a => a.分类名称).Take(返回条数).ToList();
            return listSort;
        }


 

        public static List<分类名称和分类类别名称> 得到分类输入提示框数据源(string 分类名称, int 返回条数)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            //List<系统分类表> listSystemSort = 分类.得到缓存的系统分类(db);
            List<系统分类表> listSystemSort = db.系统分类表.ToList();
            string 小写分类名称 = 分类名称.ToLower();
            List<系统分类表> listSort = listSystemSort.Where(a => a.分类名称.ToLower().StartsWith(小写分类名称)).ToList();
            List<系统分类表> listSimilarSort = listSort.Where(a => a.同义词分类名称 != "").ToList();
            List<系统分类表> listMainSort = listSort.Except(listSimilarSort).ToList();
            if (listSimilarSort.Count != 0)
            {
                var groupSimilarSort = listSimilarSort.GroupBy(a => a.同义词分类名称).ToList();
                foreach (var subGroupSimilarSort in groupSimilarSort)
                {
                    if (listSort.Any(a => a.分类名称 == subGroupSimilarSort.ElementAt(0).同义词分类名称) == false)
                    {
                        listMainSort.Add(subGroupSimilarSort.ElementAt(0));
                    }
                }
            }
            listMainSort = listMainSort.OrderBy(a => a.分类名称).ToList();

            系统分类表 nowSort = listMainSort.Where(a => a.分类名称.ToLower() == 小写分类名称).FirstOrDefault();
            if (nowSort != null)
            {
                listMainSort.Remove(nowSort);
                //放到第一个位置
                listMainSort.Insert(0, nowSort);
            }          
            List<分类名称和分类类别名称> listNewSort = new List<分类名称和分类类别名称>();
            foreach (系统分类表 systemSort in listMainSort)
            {
                分类名称和分类类别名称 newSort = new 分类名称和分类类别名称();
                newSort.分类名 = systemSort.分类名称;
                listNewSort.Add(newSort);
            }
            return listNewSort;
        }



        



        public string 转化成Json字符串()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }




        public static List<string> 得到分类及子分类名称集合(string 分类名称)
        {
            //查找同义词
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            系统分类表 systemSort = db.系统分类表.FirstOrDefault(a=>a.分类名称==分类名称);
            List<string> listSameSort = new List<string>();
            if (systemSort.同义词分类名称 == "")
            {
                listSameSort = db.系统分类表.Where(a => a.同义词分类名称 == 分类名称).Select(a => a.分类名称).ToList();
                listSameSort.Add(分类名称);
            }
            else
            {
                listSameSort = db.系统分类表.Where(a => a.分类名称 == systemSort.同义词分类名称 || a.同义词分类名称 == systemSort.同义词分类名称)
                    .Select(a => a.分类名称).ToList();
            }

            //返回的分类集合
            List<string> listSortName = new List<string>();
            for (int i = 0; i < listSameSort.Count; i++)
            {
                listSortName = 递归得到子分类名称(listSameSort[i], listSortName);
            }
            listSortName.AddRange(listSameSort);
            return listSortName;
        }


        private static List<string> 递归得到子分类名称(string 分类名称, List<string> 子分类名称集合)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            List<string> list = CodeSmith.Data.Linq.QueryResultCache.FromCache(db.系统分类上下级关系表.Where(a => a.系统分类名称 == 分类名称 && a.是否上级 == false).Select(a => a.分类关系名称)
                .Union(db.系统分类上下级关系表.Where(a => a.分类关系名称 == 分类名称 && a.是否上级 == true).Select(a => a.系统分类名称)), 600).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                子分类名称集合.Add(list[i]);
                递归得到子分类名称(list[i], 子分类名称集合);

            }
            return 子分类名称集合;
        }




        public static 分类相关联分类 得到分类相关联的分类(string 分类名, LoveKaoExamEntities db)
        {
            List<string> listParentSort = 分类.得到分类的属于类别的父分类集合(分类名, db);
            int count = listParentSort.Count;
            string mainSimilarSort = 分类.得到分类主同义词(分类名, db);
            List<string> listSubSort = 分类.得到分类的子分类集合(mainSimilarSort, db);
            分类相关联分类 relationSort = new 分类相关联分类();
            relationSort.是类别的父分类集合 = listParentSort;
            relationSort.子分类集合 = listSubSort;
            relationSort.是类别的父分类数量 = count;
            relationSort.分类名 = mainSimilarSort;
            return relationSort;
        }



        public static List<string> 得到分类的属于类别的父分类集合(string 分类名, LoveKaoExamEntities db)
        {
            分类名 = 分类名.ToLower();
            List<string> listParentSort = new List<string>();
            List<系统分类表> listSystemSort = db.系统分类表.ToList();
            List<系统分类上下级关系表> listRelation = db.系统分类上下级关系表.ToList();
            系统分类表 systemSort = listSystemSort.Where(a => a.分类名称.ToLower() == 分类名).FirstOrDefault();
            //为空，说明是新加入的分类
            if (systemSort == null)
            {
                return listParentSort;
            }
            //不为空
            else
            {
                //是同义词中的主分类
                if (systemSort.同义词分类名称 == "")
                {
                    //是系统分类并且不属于类别的，则查询其所有是类别的父分类
                    if (systemSort.分类类型 == 0 && systemSort.分类类别名称 == "")
                    {
                        List<系统分类表> listParent = new List<系统分类表>();
                        listParentSort = 递归得到是类别的父分类集合(分类名, listParent, listSystemSort, listRelation);
                    }
                    return listParentSort;
                }
                //不是主分类，则要找到主分类，并查找主分类的所有是类别的父分类
                else
                {
                    List<系统分类表> listSort = listSystemSort.Where(a => a.分类名称 == systemSort.同义词分类名称
                        || a.同义词分类名称 == systemSort.同义词分类名称).ToList();
                    //去掉自己
                    listSort.Remove(systemSort);
                    //找到主分类
                    系统分类表 mainSort = listSort.Where(a => a.同义词分类名称 == "").First();
                    //是系统分类并且不属于类别的，则查询其所有是类别的父分类
                    if (mainSort.分类类型 == 0 && mainSort.分类类别名称 == "")
                    {
                        List<系统分类表> listParent = new List<系统分类表>();
                        listParentSort = 递归得到是类别的父分类集合(mainSort.分类名称, listParent, listSystemSort, listRelation);
                    }
                    return listParentSort;
                }
            }

        }


        public static string 得到分类主同义词(string 分类名, LoveKaoExamEntities db)
        {
            string sort = 分类名.ToLower();
            List<系统分类表> listSystemSort = db.系统分类表.ToList();
            系统分类表 systemSort = listSystemSort.Where(a => a.分类名称.ToLower() == sort).FirstOrDefault();
            //为空，说明是新加入的分类，即为主同义词
            if (systemSort == null)
            {
                return 分类名;
            }
            else
            {
                //是同义词中的主分类
                if (systemSort.同义词分类名称 == "")
                {
                    return systemSort.分类名称;
                }
                //不是主分类
                else
                {
                    return systemSort.同义词分类名称;
                }
            }
        }

        //public static List<string> 得到分类同义词集合(string 分类名,LoveKaoDataContext db)
        //{
        //    List<string> listSimilarSort = new List<string>();
        //    List<系统分类表> listSystemSort = db.系统分类表.FromCache(30).ToList();
        //    系统分类表 systemSort = listSystemSort.Where(a => a.分类名称 == 分类名).FirstOrDefault();
        //    //为空，说明是新加入的分类
        //    if (systemSort == null)
        //    {
        //        return listSimilarSort;
        //    }
        //    //不为空
        //    else
        //    {
        //        //是同义词中的主分类
        //        if (systemSort.同义词分类名称 == "")
        //        {
        //            listSimilarSort = listSystemSort.Where(a => a.同义词分类名称 == 分类名).Select(a => a.分类名称).ToList();
        //        }
        //        //不是主分类
        //        else
        //        {
        //            listSimilarSort = listSystemSort.Where(a => a.分类名称 == systemSort.同义词分类名称
        //               || a.同义词分类名称 == systemSort.同义词分类名称).Select(a=>a.分类名称).ToList();
        //            //去掉自己
        //            listSimilarSort.Remove(分类名);                  
        //        }
        //        return listSimilarSort;
        //    }
        //}



        public static List<string> 替换同义词(List<string> listSort, LoveKaoExamEntities db)
        {
            List<系统分类表> listSystemSort = db.系统分类表.ToList();
            List<string> listSimilarSort = new List<string>();
            foreach (string sort in listSort)
            {
                string lowSort = sort.ToLower();
                系统分类表 systemSort = listSystemSort.Where(a => a.分类名称.ToLower() == lowSort).FirstOrDefault();
                if (systemSort == null)
                {
                    listSimilarSort.Add(sort);
                }
                else
                {
                    if (systemSort.同义词分类名称 == "")
                    {
                        listSimilarSort.Add(systemSort.分类名称);
                    }
                    else
                    {
                        listSimilarSort.Add(systemSort.同义词分类名称);
                    }
                }
            }
            listSimilarSort = listSimilarSort.Distinct().ToList();
            return listSimilarSort;
        }



        public static List<string> 得到分类的子分类集合(string 分类名, LoveKaoExamEntities db)
        {
            List<系统分类上下级关系表> listRelation = db.系统分类上下级关系表.ToList();
            List<string> list = new List<string>();
            list = 递归得到子分类集合(分类名, list, listRelation);
            return list;
        }



        public static List<string> 得到分类的父分类集合(string 分类名, LoveKaoExamEntities db)
        {
            List<系统分类上下级关系表> listRelation = db.系统分类上下级关系表.ToList();
            List<string> list = new List<string>();
            list = 递归得到父分类集合(分类名, list, listRelation);
            return list;
        }


        public static List<string> 递归得到子分类集合(string 分类名称, List<string> 子分类名称集合, List<系统分类上下级关系表> listRelation)
        {
            分类名称 = 分类名称.ToLower();
            List<string> list = listRelation.Where(a => a.系统分类名称.ToLower() == 分类名称 && a.是否上级 == false).Select(a => a.分类关系名称)
                .Union(listRelation.Where(a => a.分类关系名称.ToLower() == 分类名称 && a.是否上级 == true).Select(a => a.系统分类名称)).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                子分类名称集合.Add(list[i]);
                递归得到子分类集合(list[i], 子分类名称集合, listRelation);

            }
            return 子分类名称集合;
        }


        private static List<string> 递归得到是类别的父分类集合(string 分类名称, List<系统分类表> 父分类集合, List<系统分类表> listSystemSort, List<系统分类上下级关系表> listRelation)
        {
            分类名称 = 分类名称.ToLower();
            List<string> list = listRelation.Where(a => a.系统分类名称.ToLower() == 分类名称 && a.是否上级 == true).Select(a => a.分类关系名称)
                .Union(listRelation.Where(a => a.分类关系名称.ToLower() == 分类名称 && a.是否上级 == false).Select(a => a.系统分类名称)).ToList();
            List<系统分类表> listSort = listSystemSort.Where(a => list.Contains(a.分类名称)).ToList();
            for (int i = 0; i < listSort.Count; i++)
            {
                if (listSort[i].分类类别名称 != "")
                {
                    父分类集合.Add(listSort[i]);
                }
                else
                {
                    递归得到是类别的父分类集合(listSort[i].分类名称, 父分类集合, listSystemSort, listRelation);
                }

            }
            return 父分类集合.Select(a => a.分类名称).ToList();
        }



        private static List<string> 递归得到父分类集合(string 分类名称, List<string> 父分类名称集合, List<系统分类上下级关系表> listRelation)
        {
            分类名称 = 分类名称.ToLower();
            List<string> list = listRelation.Where(a => a.系统分类名称.ToLower() == 分类名称 && a.是否上级 == true).Select(a => a.分类关系名称)
                .Union(listRelation.Where(a => a.分类关系名称.ToLower() == 分类名称 && a.是否上级 == false).Select(a => a.系统分类名称)).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                父分类名称集合.Add(list[i]);
                递归得到父分类集合(list[i], 父分类名称集合, listRelation);
            }
            return 父分类名称集合;
        }




        public static int 得到某用户创建的分类数量(Guid 用户ID)
        {
            int count = 分类.分类查询.Where(a => a.创建人ID == 用户ID).Count();
            return count;
        }


       
        #endregion


    }
}
