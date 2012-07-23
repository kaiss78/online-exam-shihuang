using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace LoveKaoExam.Data
{
    public class 所属分类
    {
        #region 属性


        [JsonIgnore]
        public Guid ID
        {
            get;
            set;
        }


        /// <summary>
        /// 试题或试卷ID
        /// </summary>
        [JsonIgnore]
        public Guid 相关ID
        {
            get;
            set;
        }


      
        public string 分类名
        {
            get;
            set;
        }


       


        public static IQueryable<所属分类> 所属分类查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.所属分类表.Select(a => new 所属分类
                    {
                        ID = a.ID,
                        分类名 = a.分类名,
                        相关ID = a.相关信息ID
                    });
            }
        }


        #endregion

        #region 方法

        public static string 转化成Json字符串(List<所属分类> listSort)
        {
            string json = JsonConvert.SerializeObject(listSort);
            return json;
        }


        public static string 得到分类Json根据外部ID(Guid 外部ID)
        {
            List<所属分类> list = 所属分类查询.Where(a => a.相关ID == 外部ID).ToList();          
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            return json;
        }


        public static List<所属分类> 把分类Json转化成所属分类集合(string 分类Json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<所属分类> listSort = jss.Deserialize<List<所属分类>>(分类Json);
            return listSort;
        }



        public static List<string> 所属分类处理(List<string> listSort, Guid memberId, LoveKaoExamEntities db)
        {
            listSort = listSort.Distinct().ToList();
            //如有空分类，则去掉
            List<string> listNullSort = listSort.Where(a => a == "").ToList();
            listSort = listSort.Except(listNullSort).ToList();
            //替换同义词
            if (listSort.Count > 0)
            {
                listSort = 分类.替换同义词(listSort, db);
            }
            //如果同时选了父级和子级，则去掉父级
            if (listSort.Count > 1)
            {
                listSort = 分类.去掉父级分类(listSort, db);
            }
            List<string> listBelongSort = listSort;
            //如果现有分类不存在，则添加
            if (listSort.Count > 0)
            {
                List<string> listSystemSort = (from systemSort in db.系统分类表
                                               where listSort.Contains(systemSort.分类名称)
                                               select systemSort.分类名称).ToList();
                listSort = listSort.Select(a => a.ToLower()).ToList();
                listSystemSort = listSystemSort.Select(a => a.ToLower()).ToList();
                List<string> listSortString = listSort.Except(listSystemSort).ToList();

                foreach (string sort in listSortString)
                {
                    系统分类表 systemSort = new 系统分类表();
                    systemSort.分类名称 = sort;
                    systemSort.操作人ID = memberId;
                    systemSort.操作时间 = DateTime.Now;
                    systemSort.分类类型 = 1;
                    systemSort.分类类别名称 = "";
                    systemSort.同义词分类名称 = "";
                    db.系统分类表.AddObject(systemSort);
                }
                db.SaveChanges();
            }
            return listBelongSort;
        }



        public static void 添加相关信息所属分类(List<string> listSort, Guid memberId, Guid 相关信息ID, byte 相关类型, LoveKaoExamEntities db)
        {
            if (listSort.Count > 0)
            {
                List<所属分类表> listNewSort = new List<所属分类表>();
                for (int i = 0; i < listSort.Count; i++)
                {
                    所属分类表 sort = new 所属分类表();
                    sort.ID = Guid.NewGuid();
                    sort.操作人ID = memberId;
                    sort.操作时间 = DateTime.Now;
                    sort.相关类型 = 相关类型;
                    sort.相关信息ID = 相关信息ID;
                    sort.分类名 = listSort[i];
                    listNewSort.Add(sort);
                }
                foreach (var newSort in listNewSort)
                {
                    db.所属分类表.AddObject(newSort);
                }
            }
        }



        public static void 修改相关信息更新所属分类(List<string> 分类列表, byte 相关类型, Guid memberId, Guid 相关ID, LoveKaoExamEntities db)
        {
            if (分类列表.Count > 0)
            {
                //把原来的删除，再插入新的
                List<所属分类表> listSort = db.所属分类表.Where(a=>a.相关信息ID==相关ID).ToList();
                foreach (var sort in listSort)
                {
                    db.所属分类表.DeleteObject(sort);
                }
                db.SaveChanges();
                List<所属分类表> listNewSort = new List<所属分类表>();
                foreach (string sort in 分类列表)
                {
                    所属分类表 sortTable = new 所属分类表();
                    sortTable.ID = Guid.NewGuid();
                    sortTable.操作人ID = memberId;
                    sortTable.操作时间 = DateTime.Now;
                    sortTable.分类名 = sort;
                    sortTable.相关类型 = 相关类型;
                    sortTable.相关信息ID = 相关ID;
                    listNewSort.Add(sortTable);
                }
                foreach (var newSort in listNewSort)
                {
                    db.所属分类表.DeleteObject(newSort);
                }          
                db.SaveChanges();
            }
        }



        public static 所属分类 把所属分类WCF转化成所属分类(LoveKaoServiceReference.所属分类 belongSortWCF)
        {
            所属分类 belongSort = new 所属分类();
            belongSort.ID = belongSortWCF.ID;
            belongSort.分类名 = belongSortWCF.分类名;
            belongSort.相关ID = belongSortWCF.相关信息ID;
            return belongSort;
        }





        public static LoveKaoServiceReference.所属分类 把所属分类转化为所属分类WCF(所属分类 所属分类)
        {
            LoveKaoServiceReference.所属分类 belongSortWCF = new LoveKaoServiceReference.所属分类();
            belongSortWCF.ID = 所属分类.ID;
            belongSortWCF.分类名 = 所属分类.分类名;
            belongSortWCF.相关信息ID = 所属分类.相关ID;
            return belongSortWCF;
        }



        public static LoveKaoServiceReference.所属分类 把所属分类转化为所属分类WCF(string sortName,Guid 相关ID)
        {
            LoveKaoServiceReference.所属分类 belongSortWCF = new LoveKaoServiceReference.所属分类();
            belongSortWCF.ID = Guid.NewGuid();
            belongSortWCF.分类名 = sortName;
            belongSortWCF.相关信息ID = 相关ID;
            return belongSortWCF;
        }

        #endregion

    }
}
