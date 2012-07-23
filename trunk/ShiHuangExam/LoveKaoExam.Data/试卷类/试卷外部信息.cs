using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Transactions;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using CodeSmith.Data.Linq;
using System.Runtime.Caching;
using ExpressionMapper;
using System.Web;
using System.IO;


namespace LoveKaoExam.Data
{
    public class 试卷外部信息
    {
        #region 变量
        private List<string> _分类列表;
        private 试卷内容 _当前试卷内容;
        private 用户 _创建人;
        private decimal _客观题总分;
        #endregion



        #region 静态属性

        public static CacheItemPolicy cacheRandomTest
        {
            get
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10);
                return policy;
            }
        }
        #endregion


        #region 属性

        public Guid ID
        {
            get;
            set;
        }


        public Guid 爱考网ID
        {
            get;
            set;
        }


        public byte 试卷类型
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


       

        public Guid 创建人ID
        {
            get;
            set;
        }

        

        public 用户 创建人
        {
            get
            {
                if (_创建人 == null)
                {
                    _创建人 = 用户.用户查询.Where(a => a.ID == this.创建人ID).First();
                    return _创建人;
                }
                return _创建人;
            }
            set
            {
                _创建人 = value;
            }
        }



        public DateTime 最新更新时间
        {
            get;
            set;
        }


        [JsonIgnore]
        public DateTime 创建时间
        {
            get;
            set;
        }



        public byte 试卷状态Enum
        {
            get;
            set;
        }


        public int 总分
        {
            get;
            set;
        }


        [JsonIgnore]
        public 试卷内容 当前试卷内容
        {
            get
            {
                if (_当前试卷内容 == null)
                {
                    _当前试卷内容 = 试卷内容.试卷内容查询.Where(a => a.ID == this.试卷内容ID).First();
                    return _当前试卷内容;
                }
                return _当前试卷内容;
            }
            set
            {
                _当前试卷内容 = value;
            }
        }



        public List<string> 分类列表
        {
            get
            {
                if (_分类列表 == null)
                {
                    _分类列表 = 所属分类.所属分类查询.Where(a => a.相关ID == this.ID).Select(a=>a.分类名).ToList();
                }
                return _分类列表;
            }
            set
            {
                _分类列表 = value;
            }
        }



        public List<Guid> 试题外部信息ID集合
        {
            get;
            set;
        }



        public int 试题总数
        {
            get;
            set;
        }



        public int 已上传试题个数
        {
            get;
            set;
        }




        public Guid 试卷内容ID
        {
            get;
            set;
        }



        public int 已组织考试次数
        {
            get;
            set;
        }



        public int 已组织练习次数
        {
            get;
            set;
        }

   


        public static IQueryable<试卷外部信息> 试卷外部信息查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试卷外部信息表.Select(a => new 试卷外部信息
                    {
                        ID = a.ID,
                        创建人ID = a.创建人ID,
                        创建时间 = a.创建时间,
                        名称 = a.名称,
                        试卷内容ID = a.试卷内容ID,
                        试卷状态Enum = a.试卷状态Enum,
                        说明 = a.说明,
                        总分 = a.总分,
                        最新更新时间 = a.最新更新时间,
                        爱考网ID = a.爱考网ID,
                        试卷类型 = a.试卷类型
                    });
            }
        }

        #endregion



        #region 方法

        public static List<试卷外部信息> 得到上传试卷列表(string 关键字, int 第几页, int 页的大小, out int 返回总条数)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            IQueryable<试卷外部信息表> query = db.试卷外部信息表.Where(a => a.创建人ID == 用户信息.CurrentUser.用户ID
                && a.试卷状态Enum == 0&&a.试卷类型<2);
            if (String.IsNullOrEmpty(关键字) == false)
            {
                关键字 = 关键字.TrimStart();
                关键字 = 关键字.TrimEnd();
                //按试卷名查询
                query = query.Where(a => a.名称.Contains(关键字));
                //如果是分类，再按分类查询，并合并结果
                if (db.系统分类表.Any(a => a.分类名称 == 关键字) == true)
                {
                    IQueryable<试卷外部信息表> queryBySort = from outside in db.试卷外部信息表
                                                      join belongSort in db.所属分类表
                                                      on outside.ID equals belongSort.相关信息ID
                                                      where belongSort.分类名 == 关键字
                                                      && outside.创建人ID == 用户信息.CurrentUser.用户ID
                                                      && outside.试卷状态Enum == 0 && outside.试卷类型 < 2
                                                      select outside;
                    query = query.Union(queryBySort);
                }
            }
            返回总条数 = query.Distinct().Count();
            List<试卷外部信息表> list = query.Distinct().OrderByDescending(a => a.创建时间)
                .Skip(第几页 * 页的大小).Take(页的大小).ToList();
            List<试卷外部信息> listOutside = 把试卷外部信息表集合转化为试卷外部信息集合(list);
            List<Guid> listProblemOutsideId = new List<Guid>();
            //计算试题总数
            List<Guid> listContentId = listOutside.Select(a => a.试卷内容ID).ToList();
            List<试卷中大题表> listTestType = db.试卷中大题表.Where(a => listContentId.Contains(a.试卷内容ID)).ToList();
            List<Guid> listTestTypeId = listTestType.Select(a => a.ID).ToList();
            List<试卷大题中试题表> listTestProblem = db.试卷大题中试题表.Where(a => listTestTypeId.Contains(a.试卷中大题ID))
                .ToList();
            List<Guid> listProblemContentId = listTestProblem.Select(a => a.试题内容ID).ToList();
            List<试题内容表> listProblemContent = db.试题内容表.Where(a => listProblemContentId.Contains(a.ID)).ToList();          
            foreach (试卷外部信息 outside in listOutside)
            {
                List<试卷中大题表> listThisTestType = listTestType.Where(a => a.试卷内容ID == outside.试卷内容ID).ToList();
                List<Guid> listThisTestTypeId = listThisTestType.Select(a => a.ID).ToList();
                List<试卷大题中试题表> listThisTestProblem = listTestProblem.Where(a => listThisTestTypeId
                    .Contains(a.试卷中大题ID)).ToList();
                List<Guid> listThisProblemContentId = listThisTestProblem.Select(a => a.试题内容ID).ToList();
                outside.试题外部信息ID集合 = listProblemContent.Where(a => listThisProblemContentId.Contains(a.ID))
                    .Select(a => a.试题外部信息ID).Distinct().ToList();
                outside.试题总数 = outside.试题外部信息ID集合.Count;
                listProblemOutsideId.AddRange(outside.试题外部信息ID集合);
            }
            //计算已上传试题个数
            List<试题外部信息表> listOutsideTable = db.试题外部信息表.Where(a =>a.试题类型>0
                && a.创建人ID == 用户信息.CurrentUser.用户ID).ToList();
            foreach (var outside in listOutside)
            {
                int count = listOutsideTable.Where(a => outside.试题外部信息ID集合.Contains(a.爱考网ID)).Count();
                outside.已上传试题个数 = count;
            }
            return listOutside;
        }



        public static List<Guid> 得到已上传试卷爱考网ID集合()
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            List<Guid> listOutsideId = db.试卷外部信息表.Where(a => a.试卷类型 == 1
                && a.创建人ID == 用户信息.CurrentUser.用户ID).Select(a => a.爱考网ID).ToList();
            return listOutsideId;
        }



        public static List<Guid> 得到已下载试卷爱考网ID集合()
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            List<Guid> listOutsideId = db.试卷外部信息表.Where(a => a.试卷类型 == 2
                && a.创建人ID == 用户信息.CurrentUser.用户ID).Select(a => a.爱考网ID).ToList();
            return listOutsideId;
        }



        /// <summary>
        /// 返回0上传成功，1账号未绑定，2绑定账号被禁用，3该试卷已经上传过
        /// </summary>     
        public static int 上传试卷(Guid 试卷外部信息ID, out List<试题外部信息> 已存在试题集合)
        {
            try
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                //获取绑定账号信息
                绑定账号表 bind = db.绑定账号表.Where(a => a.本地账号ID == 用户信息.CurrentUser.用户ID).FirstOrDefault();
                if (bind == null)
                {
                    异常处理.抛出异常(-1);
                    已存在试题集合 = new List<试题外部信息>();
                    return 1;
                }
                试卷外部信息 试卷外部信息 = 试卷外部信息查询.Where(a => a.ID == 试卷外部信息ID).First();
                试卷内容.给试卷内容中试题内容Json赋值(试卷外部信息.当前试卷内容, false);
                LoveKaoServiceReference.试卷外部信息 outsideWCF = 把试卷外部信息转化成试卷外部信息WCF(试卷外部信息);
                LoveKaoServiceReference.试卷内容 contentWCF = 把试卷内容转化成试卷内容WCF(试卷外部信息.当前试卷内容);
                outsideWCF.当前试卷内容 = contentWCF;
                List<Guid> listNewUploadProblemOutsideId = new List<Guid>();
                LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                int result = client.上传试卷(out listNewUploadProblemOutsideId, outsideWCF, bind.爱考网账号, bind.爱考网密码);
                client.Close();
                if (result == 1)
                {
                    异常处理.抛出异常(-1);
                }
                else if (result == 2)
                {
                    异常处理.抛出异常(1);
                }
                else if (result == 3)
                {
                    throw new Exception("你已上传过该试卷！");
                }
                else if (result == 4)
                {
                    异常处理.抛出异常(2);
                }
                //上传成功，更新试卷类型和试卷中新上传试题类型
                if (result == 0)
                {
                    List<Guid> listAllContentId = new List<Guid>();
                    foreach (试卷中大题 type in 试卷外部信息.当前试卷内容.试卷中大题集合)
                    {
                        listAllContentId.AddRange(type.试卷大题中试题集合.Select(a => a.试题内容ID).ToList());
                    }
                    List<Guid> listAllOutsideId = db.试题内容表.Where(a => listAllContentId.Contains(a.ID))
                        .Select(a => a.试题外部信息ID).Distinct().ToList();
                    List<Guid> listExistOutsideId = listAllOutsideId.Except(listNewUploadProblemOutsideId).ToList();
                    已存在试题集合 = 试题外部信息.试题外部信息查询.Where(a => listExistOutsideId.Contains(a.ID)).ToList();
                    试卷外部信息表 outside = db.试卷外部信息表.FirstOrDefault(a=>a.ID==试卷外部信息ID);
                    outside.试卷类型 = 1;
                    List<试题外部信息表> listOutside = db.试题外部信息表.Where(a => a.创建人ID == 用户信息.CurrentUser.用户ID
                        && listNewUploadProblemOutsideId.Contains(a.ID)).ToList();
                    foreach (试题外部信息表 outsideTable in listOutside)
                    {
                        outsideTable.试题类型 = 1;
                    }
                    db.SaveChanges();
                    return result;
                }
                else
                {
                    已存在试题集合 = new List<试题外部信息>();
                    return result;
                }
              
            }
            catch (Exception ex)
            {
                异常处理.Catch异常处理(ex.Message);
                throw;
            }          
        }



        public static List<LoveKaoServiceReference.试卷外部信息WCF> 得到下载试卷列表(string 关键字, int 第几页, int 页的大小, out int 返回总条数)
        {
            try
            {
                LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                List<LoveKaoServiceReference.试卷外部信息WCF> listOutside = client.得到主站下载试卷列表(out 返回总条数, 关键字, 第几页, 页的大小);
                client.Close();
                //查询已下载过的试题
                List<Guid> listProblemOutsideId = new List<Guid>();
                foreach (var outside in listOutside)
                {
                    listProblemOutsideId.AddRange(outside.试题外部信息ID集合);
                }
                LoveKaoExamEntities db=new LoveKaoExamEntities();
                List<试题外部信息表> listOutsideTable = db.试题外部信息表.Where(a => listProblemOutsideId
                    .Contains(a.爱考网ID)&&a.创建人ID==用户信息.CurrentUser.用户ID) .ToList();
                foreach (var outside in listOutside)
                {
                    int count = listOutsideTable.Where(a => outside.试题外部信息ID集合.Contains(a.爱考网ID)).Count();
                    outside.已下载试题个数 = count;
                }
                return listOutside;
            }
            catch (Exception)
            {
                throw new Exception("连接爱考网服务器出错，请稍后再试！");
            }          
        }



       

        /// <summary>
        /// 返回0下载成功，1账号未绑定，2绑定账号被禁用
        /// </summary>       
        public static int 下载试卷(Guid 试卷外部信息WCFID, int 试题总数, int 已下载试题个数)
        {
            try
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                //获取绑定账号信息
                绑定账号表 bind = db.绑定账号表.Where(a => a.本地账号ID == 用户信息.CurrentUser.用户ID).FirstOrDefault();
                if (bind == null)
                {
                    异常处理.抛出异常(-1);
                    return 1;
                }
                LoveKaoServiceReference.试卷外部信息WCF outsideWCF = new LoveKaoServiceReference.试卷外部信息WCF();
                LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                int result = client.下载试卷(out outsideWCF, 试卷外部信息WCFID, 试题总数, 已下载试题个数, bind.爱考网账号, bind.爱考网密码);
                client.Close();
                if (result == 1)
                {
                    异常处理.抛出异常(-1);
                }
                else if (result == 2)
                {
                    异常处理.抛出异常(1);
                }
                else if (result == 3)
                {
                    异常处理.抛出异常(2);
                }
                if (result == 0)
                {
                    保存下载试卷(outsideWCF);
                    试题外部信息.保存图片(outsideWCF.当前试卷内容WCF.试题图片, 用户信息.CurrentUser.用户名);
                }
                return result;
            }
            catch (Exception ex)
            {
                异常处理.Catch异常处理(ex.Message);
                throw;
            }
           
        }



        public static void 保存下载试卷(LoveKaoServiceReference.试卷外部信息WCF outsideWCF)
        {
            考官.判断是否是考官();
            试卷外部信息 outside = 把试卷外部信息WCF转化成试卷外部信息(outsideWCF);
            试卷内容 content = 把试卷内容WCF转化成试卷内容(outsideWCF.当前试卷内容WCF);
            Guid 爱考网ID = outside.ID;
            outside.爱考网ID = 爱考网ID;
            outside.ID = Guid.NewGuid();
            outside.试卷类型 = 2;
            content.试卷外部信息ID = outside.ID;
            content.ID = Guid.NewGuid();
            outside.试卷内容ID = content.ID;
            保存试卷相关信息(content, outside, 1);
        }



       

        public string 转化成Json字符串()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            return json;
        }



        public string 转化成试卷外部信息Json字符串()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            json = "{\"content\":null,\"outside\":" + json + "}";
            return json;
        }




        public static void 保存试卷相关信息(string 试卷Json字符串)
        {
            考官.判断是否是考官();
            试卷内容 testContent = 试卷内容.把Json转化成试卷内容(试卷Json字符串);
            试卷外部信息 outside = 试卷外部信息.把Json转化成试卷外部信息(试卷Json字符串);
            if (outside.分类列表.Count == 0)
            {
                throw new Exception("请至少填写一个分类！");
            }
            outside.爱考网ID = outside.ID;
            试卷外部信息.保存试卷相关信息(testContent, outside,0);
        }



       
        /// <param name="类型">0保存本站试卷，1保存下载试卷</param>
        public static void 保存试卷相关信息(试卷内容 试卷内容, 试卷外部信息 试卷外部信息,int 类型)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            List<string> listSort = 试卷外部信息.分类列表.ToList();
            if (类型 == 0)
            {
                listSort=所属分类.所属分类处理(listSort, 试卷外部信息.创建人ID, db);
            }
            else
            {
                所属分类.所属分类处理(listSort, 用户信息.CurrentUser.用户ID, db);
            }                       
            试卷外部信息表 testOutside = new 试卷外部信息表();
            testOutside.ID = 试卷内容.试卷外部信息ID;
            if (类型 == 0)
            {
                testOutside.创建人ID = 试卷内容.提交人ID;
            }
            else
            {
                testOutside.创建人ID = 用户信息.CurrentUser.用户ID;
            }
            testOutside.创建时间 = DateTime.Now;
            testOutside.最新更新时间 = DateTime.Now;
            testOutside.说明 = 试卷内容.说明;
            testOutside.试卷内容ID = 试卷内容.ID;
            testOutside.爱考网ID = 试卷外部信息.爱考网ID;
            testOutside.试卷类型 = 试卷外部信息.试卷类型;
            testOutside.名称 = 试卷内容.名称;
            testOutside.试卷状态Enum = 试卷外部信息.试卷状态Enum;
            //保存的是正常试卷，则计算总分，是草稿则不需计算
            if (试卷外部信息.试卷状态Enum == 0)
            {
                //计算试卷总分
                decimal totalScore =  试卷外部信息.计算试卷总分(试卷内容);
                testOutside.总分 = Convert.ToInt32(totalScore);
            }
            using (TransactionScope scope = new TransactionScope())
            {
                if (类型 == 0)
                {
                    所属分类.添加相关信息所属分类(listSort, testOutside.创建人ID, testOutside.ID, 1, db);
                }
                else
                {
                    所属分类.添加相关信息所属分类(listSort, 用户信息.CurrentUser.用户ID, testOutside.ID, 1, db);
                }
                db.试卷外部信息表.AddObject(testOutside);
                if (类型 == 0)
                {
                    试卷内容.保存(db, 试卷内容.ID);
                }
                else
                {
                    试卷内容.保存下载试卷(db, listSort);
                }
                scope.Complete();
            }
        }




        public static void 修改正常试卷(string 试卷Json字符串)
        {           
            试卷外部信息 outside = 试卷外部信息.把Json转化成试卷外部信息(试卷Json字符串);
            List<string> listSort = outside.分类列表;
            试卷内容 content = 试卷内容.把Json转化成试卷内容(试卷Json字符串);
            试卷外部信息.修改正常试卷(listSort, content, outside.ID, outside.试卷状态Enum);
        }


        public static void 修改正常试卷(List<string> 分类列表, 试卷内容 试卷内容, Guid 试卷外部信息ID, int 试卷状态Enum)
        {
            if (试卷状态Enum == 4)
            {
                throw new Exception("正常试卷不能改为草稿！");
            }
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            试卷外部信息表 outside = db.试卷外部信息表.FirstOrDefault(a => a.ID == 试卷外部信息ID);
            异常处理.删除修改权限判断(outside.创建人ID);
            List<string> listBelongSort = 所属分类.所属分类处理(分类列表, outside.创建人ID, db);
            if (outside.试卷状态Enum == 1)
            {
                throw new Exception("该试卷已被删除！");
            }
            outside.最新更新时间 = DateTime.Now;
            if (试卷内容 != null)
            {
                outside.名称 = 试卷内容.名称;
                outside.试卷状态Enum = 0;
                outside.试卷内容ID = 试卷内容.ID;
                //计算试卷总分
                decimal totalScore= 试卷外部信息.计算试卷总分(试卷内容);
                outside.总分 = Convert.ToInt32(totalScore);
            }
            using (TransactionScope scope = new TransactionScope())
            {
                所属分类.修改相关信息更新所属分类(listBelongSort, 1, outside.创建人ID, outside.ID, db);

                if (试卷内容 != null)
                {
                    试卷内容.保存(db, 试卷内容.ID);
                }
                scope.Complete();
            }
        }



        public static void 修改草稿试卷(string 试卷Json字符串)
        {
            试卷外部信息 outside = 试卷外部信息.把Json转化成试卷外部信息(试卷Json字符串);
            List<string> listSort = outside.分类列表;
            试卷内容 content = 试卷内容.把Json转化成试卷内容(试卷Json字符串);
            修改草稿试卷(listSort, content, outside.ID, outside.试卷状态Enum);
        }



        public static void 修改草稿试卷(List<string> 分类列表, 试卷内容 试卷内容, Guid 试卷外部信息ID, byte 试卷状态Enum)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            试卷外部信息表 outsideTable = db.试卷外部信息表.FirstOrDefault(a => a.ID == 试卷外部信息ID);
            List<string> listBelongSort = 所属分类.所属分类处理(分类列表, outsideTable.创建人ID, db);
            if (outsideTable.试卷状态Enum == 1)
            {
                throw new Exception("该试卷已被删除！");
            }
            outsideTable.试卷状态Enum = 试卷状态Enum;
            outsideTable.最新更新时间 = DateTime.Now;
            所属分类.修改相关信息更新所属分类(listBelongSort, 1, outsideTable.创建人ID, outsideTable.ID, db);
            if (试卷内容 != null)
            {
                试卷内容表 dbContent = db.试卷内容表.FirstOrDefault(a => a.ID == 试卷内容.ID);
                db.试卷内容表.DeleteObject(dbContent);
                //db.SaveChanges();
                试卷内容.保存(db, 试卷内容.ID);
                db.SaveChanges();
            }
        }



        public static 试卷外部信息 把Json转化成试卷外部信息(string 试卷Json字符串)
        {
            JObject bo = JObject.Parse(试卷Json字符串);
            string outsideStr = bo["outside"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            试卷外部信息 outside = jss.Deserialize<试卷外部信息>(outsideStr);
            return outside;
        }




        public static void 删除试卷(Guid 试卷外部信息ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            试卷外部信息表 outside = db.试卷外部信息表.FirstOrDefault(a => a.ID == 试卷外部信息ID);
            异常处理.删除修改权限判断(outside.创建人ID);
            outside.试卷状态Enum = 1;
            //若设置了考试或练习，则删除
            List<Guid> listContentId = db.试卷内容表.Where(a=>a.试卷外部信息ID==试卷外部信息ID).Select(a => a.ID).ToList();
            List<Guid> listExamSetId = db.考试设置表.Where(a => listContentId.Contains(a.试卷内容ID)).Select(a => a.ID).ToList();

            foreach (Guid examSetId in listExamSetId)
            {
                考试设置.删除考试设置(examSetId);
            }
            List<Guid> listExerciseSetId = db.练习设置表.Where(a => listContentId.Contains(a.试卷内容ID)).Select(a => a.试卷内容ID).ToList();
            foreach (Guid exerciseSetId in listExerciseSetId)
            {
                练习设置.删除练习设置(exerciseSetId);
            }
            db.SaveChanges();
        }





        public static 试卷外部信息 得到符合ID的试卷(Guid 试卷外部信息ID)
        {
            试卷外部信息 outside = 试卷外部信息.试卷外部信息查询.Where(a => a.ID == 试卷外部信息ID).FirstOrDefault();                   
            return outside;
        }



        public static 试卷外部信息 得到随机试卷根据外部ID(Guid 试卷外部信息ID)
        {
            //从缓存中查找随机试卷  
            试卷外部信息 outside = (试卷外部信息)MemoryCache.Default.Get("" + 试卷外部信息ID + "");           
            return outside;
        }



      
      

        /// <param name="相关ID">考试设置或试卷内容ID</param>
        /// <param name="类型">0练习，1考试</param>          
        public static string 得到在线考试Json(Guid 相关ID,byte 类型, Guid 用户ID)
        {
            int TestDuration = 0;
            试卷内容 content = new 试卷内容();
            if (类型 == 0)
            {
                练习设置 exerciseSet=练习设置.练习设置查询.Where(a => a.试卷内容ID == 相关ID).First();
                if (!exerciseSet.考生ID集合.Contains(用户ID))
                {
                    throw new Exception("你不在该练习的考生范围内，无权参加练习！");
                }
                TestDuration = exerciseSet.考试时长;
                content=exerciseSet.试卷内容;             
            }
            else
            {
                考试设置 examSet = 考试设置.考试设置查询.Where(a => a.ID == 相关ID).First();
                //判断是否是在考试时间内，是才能进入考试
                if (DateTime.Now < examSet.考试开始时间)
                {
                    throw new Exception("考试时间未到，不能进入考试！");
                }
                if (DateTime.Now > examSet.考试结束时间)
                {
                    throw new Exception("考试时间已过，不能进入考试！");
                }
                if (!examSet.考生ID集合.Contains(用户ID))
                {
                    throw new Exception("你不在该考试的考生范围内，无权参加考试！");
                }
                if (考生做过的试卷.考生做过的试卷查询.Any(a => a.相关ID == 相关ID && a.类型 == 1 && a.考生ID == 用户ID
                    && a.是否是已提交的试卷 == true) == true)
                {
                    throw new Exception("你已参加过本场考试，不能再次考试！");
                }
                TestDuration = examSet.考试时长;
                content=examSet.试卷内容;               
            }                     
            试卷外部信息 outside = content.试卷外部信息;
            if (outside.试卷状态Enum == 1 || outside.试卷状态Enum == 3)
            {
                throw new Exception("该试卷已被删除！");
            }
            if (outside.试卷状态Enum == 4)
            {
                throw new Exception("该试卷是草稿试卷，您无法参加考试！");
            }
            考生做过的试卷 memberDoneTest = new 考生做过的试卷();
            memberDoneTest.类型 = 类型;
            memberDoneTest.相关ID = 相关ID;
            if (类型 == 0)
            {
                memberDoneTest.练习设置 = new 练习设置();
                memberDoneTest.练习设置.试卷内容 = content;
                memberDoneTest.练习设置.试卷内容ID = content.ID;
            }
            else
            {
                memberDoneTest.考试设置 = new 考试设置();
                memberDoneTest.考试设置.试卷内容 = content;
                memberDoneTest.考试设置.试卷内容ID = content.ID;
            }          
            content = 试卷内容.去掉试卷中的试题的答案(content);           
            string json = memberDoneTest.转化成Json();
            json = json.Replace("\"考生ID\": null", "\"公共信息\":{\"考生ID\": null");
            if (类型 == 0)
            {
                json = json.Replace(",\r\n  \"练习设置\"", "},\r\n  \"练习设置\"");
            }
            else
            {
                json = json.Replace(",\r\n  \"考试设置\"", "},\r\n  \"考试设置\"");
            }
            //格式化Json          
            JObject bo = JObject.Parse(json);
            json = json.Replace("试卷内容\": {", "试卷内容\": {\"考试时长\":" + TestDuration + ",");
            JObject bo1 = JObject.Parse(json);
            string newJson = "{\"公共信息\":";
            string common = bo1["公共信息"].ToString();
            newJson = newJson + common+",\"试卷内容\":";
            string test = string.Empty;          
            if (类型 == 0)
            {
                test = bo1["练习设置"]["试卷内容"].ToString();               
            }
            else
            {
                test = bo1["考试设置"]["试卷内容"].ToString();              
            }
            newJson = newJson + test+"}";
            return newJson;
        }





        /// <param name="类型">0全部，1自己出的，2草稿,3已上传的，4下载的</param>       
        public static List<试卷外部信息> 得到某考官试卷(string 试卷名, Guid 考官ID,int 类型, int 第几页, int 页的大小, out int 返回总条数)
        {
            IQueryable<试卷外部信息> query =试卷外部信息查询.Where(a => a.创建人ID == 考官ID);
            switch (类型)
            {
                case 0:
                    {
                        query = query.Where(a => a.试卷状态Enum != 1);
                        break;
                    }
                case 1:
                    {
                        query = query.Where(a => a.试卷状态Enum == 0 && a.试卷类型 < 2);                       
                        break;
                    }
                case 2:
                    {
                        query = query.Where(a => a.试卷状态Enum == 4);                       
                        break;
                    }
                case 3:
                    {
                        query = query.Where(a => a.试卷状态Enum == 0 && a.试卷类型 == 1);
                        break;
                    }
                case 4:
                    {
                        query = query.Where(a => a.试卷状态Enum == 0 && a.试卷类型 == 2);
                        break;
                    }
            }
            if (String.IsNullOrEmpty(试卷名) == false)
            {
                query = query.Where(a => a.名称.Contains(试卷名));
            }
            返回总条数 = query.Count();
            List<试卷外部信息> list = query.OrderByDescending(a => a.创建时间).Skip(第几页 * 页的大小).Take(页的大小).ToList();
            //给已组织考试次数，已组织练习次数属性赋值
            LoveKaoExamEntities db=new LoveKaoExamEntities();           
            List<Guid> listOutsideId=list.Select(a=>a.ID).ToList();
            List<试卷内容表> listContent = db.试卷内容表.Where(a => listOutsideId.Contains(a.试卷外部信息ID)).ToList();
            List<Guid> listContentId = listContent.Select(a => a.ID).ToList();
            List<Guid> listExamId = db.考试设置表.Where(a => listContentId.Contains(a.试卷内容ID)
               && a.是否删除 == false).Select(a => a.试卷内容ID).ToList();
            List<Guid> listExerciseId = db.练习设置表.Where(a => listContentId.Contains(a.试卷内容ID)
                && a.是否删除 == false).Select(a => a.试卷内容ID).ToList();
            foreach (试卷外部信息 outside in list)
            {
                List<Guid> listThisContentId = listContent.Where(a => a.试卷外部信息ID == outside.ID).Select(a => a.ID).ToList();
                outside.已组织考试次数 = listExamId.Where(a =>listThisContentId.Contains(a)).Count();
                outside.已组织练习次数 = listExerciseId.Where(a => listThisContentId.Contains(a)).Count();
            }
            return list;
        }
       


    

        private static decimal 计算试卷总分(试卷内容 试卷内容)
        {
            //计算试卷总分
            decimal totalScore = 0;           
            for (int i = 0; i < 试卷内容.试卷中大题集合.Count; i++)
            {
                for (int j = 0; j < 试卷内容.试卷中大题集合[i].试卷大题中试题集合.Count; j++)
                {
                    totalScore += 试卷内容.试卷中大题集合[i].试卷大题中试题集合[j].每小题分值;                   
                }
            }
            return totalScore;
        }




        /// <param name="试题充足情况">1说明生成成功且试题充足，为2说明生成成功且试题不足</param>
        public static Guid 随机生成试卷(string 取题条件Json, out int 试题充足情况)
        {
            取题条件 condition = 取题条件.把Json转化成试题外部信息(取题条件Json);
            Guid outsideId = 随机生成试卷(condition, out 试题充足情况);
            return outsideId;
        }



        public static Guid 随机生成试卷(取题条件 getProblemCondition, out int 试题充足情况)
        {
            试题充足情况 = 0;
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            if (getProblemCondition.取题题型及数量集合.Count > 15)
            {
                throw new Exception("试卷题型不能超过15个！");
            }
            foreach (取题题型及数量 typeNum in getProblemCondition.取题题型及数量集合)
            {
                if (typeNum.数量 > 200)
                {
                    throw new Exception("每个题型下的题目不能超过200道！");
                }
            }
            试卷外部信息 outside = new 试卷外部信息();
            outside.ID = Guid.NewGuid();
            outside.爱考网ID = outside.ID;
            outside.创建人ID = 用户信息.CurrentUser.用户ID;
            outside.名称 = getProblemCondition.试卷名称;
            outside.说明 = "";
            outside.分类列表 = new List<string>();
            //给试卷内容赋值
            试卷内容 test = new 试卷内容();
            test.ID = Guid.NewGuid();
            test.名称 = getProblemCondition.试卷名称;
            test.说明 = "";
            test.提交人ID = outside.创建人ID;
            test.提交备注 = "";
            test.试卷外部信息ID = outside.ID;
            if (getProblemCondition.网站分类集合 == null || getProblemCondition.网站分类集合 == "")
            {
                throw new Exception("您还没选择分类，请先选择！");
            }
            if (getProblemCondition.网站分类集合 != null && getProblemCondition.网站分类集合 != "")
            {
                string[] sortList = getProblemCondition.网站分类集合.Split(",".ToCharArray());
                if (sortList.Length > 10)
                {
                    throw new Exception("分类选择不能超过10个！");
                }
                foreach (string sortName in sortList)
                {
                    outside.分类列表.Add(sortName);
                }
            }          
            test.试卷中大题集合 = new List<试卷中大题>();
            decimal totalScore = 0;
            //遍历题型数量，并赋值
            for (int i = 0; i < getProblemCondition.取题题型及数量集合.Count; i++)
            {
                getProblemCondition.取题题型及数量集合[0] = getProblemCondition.取题题型及数量集合[i];
                List<试题外部信息表> listOutside = 试题外部信息.判断取题条件(getProblemCondition).ToList();
                totalScore += getProblemCondition.取题题型及数量集合[i].每小题分值 * getProblemCondition.取题题型及数量集合[i].数量;
                试卷中大题 type = new 试卷中大题();
                type.ID = Guid.NewGuid();
                type.名称 = getProblemCondition.取题题型及数量集合[i].题型名称;
                type.说明 = "";
                //随机取到的试题内容ID集合
                if (getProblemCondition.取题题型及数量集合[i].数量 != 0)
                {
                    List<Guid> listProblemId = 试题外部信息.随机取题(getProblemCondition.取题题型及数量集合[i].题型Enum,
                        getProblemCondition.取题题型及数量集合[i].数量, listOutside, 用户信息.CurrentUser.用户ID);
                    if (listProblemId.Count < getProblemCondition.取题题型及数量集合[i].数量)
                    {
                        试题充足情况 = 2;
                    }

                    type.试卷大题中试题集合 = new List<试卷大题中试题>();
                    //遍历题目数量，并赋值
                    for (int j = 0; j < listProblemId.Count; j++)
                    {

                        试卷大题中试题 testProblem = new 试卷大题中试题();
                        testProblem.ID = Guid.NewGuid();
                        testProblem.试题内容ID = listProblemId[j];
                        testProblem.每小题分值 = getProblemCondition.取题题型及数量集合[i].每小题分值;
                        testProblem.顺序 = Convert.ToByte(j);
                        type.试卷大题中试题集合.Add(testProblem);
                    }
                    //是复合题和多题干共选项题时给子集赋值
                    if (getProblemCondition.取题题型及数量集合[i].题型Enum > 39 && getProblemCondition.取题题型及数量集合[i].题型Enum < 50 ||
                        getProblemCondition.取题题型及数量集合[i].题型Enum == 80)
                    {
                        List<试题内容表> listSubContent = db.试题内容表.Where(a => listProblemId.Contains(a.父试题内容ID.Value)).ToList();
                        var groupSubContent = listSubContent.GroupBy(a => a.父试题内容ID).ToList();
                        foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                        {
                            testProblem.子小题集合 = new List<试卷大题中试题>();
                            var subContent = groupSubContent.Where(a => a.Key == testProblem.试题内容ID).First();
                            foreach (var sub in subContent)
                            {
                                试卷大题中试题 subTestProblem = new 试卷大题中试题();
                                subTestProblem.ID = Guid.NewGuid();
                                subTestProblem.每小题分值 = getProblemCondition.取题题型及数量集合[i].每小题分值 / subContent.Count();
                                subTestProblem.试题内容ID = sub.ID;
                                testProblem.子小题集合.Add(subTestProblem);
                            }
                        }
                    }
                }
                test.试卷中大题集合.Add(type);
            }
            test.总分 = Convert.ToInt32(totalScore);
            outside.当前试卷内容 = test;
            outside.试卷内容ID = test.ID;
            outside.总分 = Convert.ToInt32(totalScore);
            //加入缓存
            MemoryCache.Default.Add("" + outside.ID + "", outside, cacheRandomTest);
            if (试题充足情况 != 2)
            {
                试题充足情况 = 1;
            }
            return outside.ID;
        }




        private static List<试卷外部信息> 给试卷外部信息赋值创建人和分类列表属性(List<试卷外部信息> listOutside)
        {

            //赋值会员属性
            List<Guid> listMemberId = listOutside.Select(a => a.创建人ID).ToList();
            List<用户> listMember = 用户.用户查询.Where(a => listMemberId.Contains<Guid>(a.ID)).ToList();
            foreach (试卷外部信息 outside in listOutside)
            {
                foreach (用户 member in listMember)
                {
                    if (outside.创建人ID == member.ID)
                    {
                        outside.创建人 = member;
                        break;
                    }
                }
            }

            //赋值分类列表属性
            List<Guid> listOutsideId = listOutside.Select(a => a.ID).ToList();
            List<所属分类> listBelongSort = 所属分类.所属分类查询.Where(a => listOutsideId.Contains<Guid>(a.相关ID)).ToList();
            foreach (试卷外部信息 outside in listOutside)
            {
                outside.分类列表 = listBelongSort.Where(a => a.相关ID == outside.ID).Select(a => a.分类名).ToList();
            }
            return listOutside;
        }


        private static void 给试卷外部信息列表赋值分类列表属性(List<试卷外部信息> listOutside)
        {
            List<Guid> listOutsideId = listOutside.Select(a => a.ID).ToList();
            List<所属分类> listBelongSort = 所属分类.所属分类查询.Where(a => listOutsideId.Contains<Guid>(a.相关ID)).ToList();
            foreach (试卷外部信息 outside in listOutside)
            {
                outside.分类列表 = listBelongSort.Where(a => a.相关ID == outside.ID).Select(a => a.分类名).ToList();
            }
        }


        private static void 给试卷外部信息列表赋值创建人属性(List<试卷外部信息> listOutside)
        {
            List<Guid> listMemberId = listOutside.Select(a => a.创建人ID).ToList();
            List<用户> listMember = 用户.用户查询.Where(a => listMemberId.Contains<Guid>(a.ID)).ToList();
            foreach (试卷外部信息 outside in listOutside)
            {
                foreach (用户 member in listMember)
                {
                    if (outside.创建人ID == member.ID)
                    {
                        outside.创建人 = member;
                        break;
                    }
                }
            }
        }



        /// <summary>
        /// 因试卷大题中试题有无限循环，故无法用Mapper函数转化
        /// </summary>
        private static 试卷外部信息 把试卷外部信息WCF转化成试卷外部信息(LoveKaoServiceReference.试卷外部信息WCF outsideWCF)
        {
            试卷外部信息 outside = new 试卷外部信息();
            outside.ID = outsideWCF.ID;
            outside.创建人ID = outsideWCF.创建人ID;
            outside.分类列表 = new List<string>();
            foreach (LoveKaoServiceReference.所属分类 belongSort in outsideWCF.分类列表)
            {
                outside.分类列表.Add(所属分类.把所属分类WCF转化成所属分类(belongSort).分类名);
            }
            outside.名称 = outsideWCF.名称;
            outside.试卷内容ID = outsideWCF.试卷内容ID;
            outside.说明 = outsideWCF.说明;
            outside.总分 = outsideWCF.总分;          
            return outside;
        }



        private static 试卷内容 把试卷内容WCF转化成试卷内容(LoveKaoServiceReference.试卷内容WCF contentWCF)
        {
            试卷内容 content = Mapper.Create<LoveKaoServiceReference.试卷内容WCF, 试卷内容>()(contentWCF);
            Func<LoveKaoServiceReference.试卷中大题WCF, 试卷中大题> func = Mapper.Create<LoveKaoServiceReference.试卷中大题WCF, 试卷中大题>();          
            content.试卷中大题集合 = new List<试卷中大题>();
            foreach (var testTypeWCF in contentWCF.试卷中大题WCF集合)
            {
                试卷中大题 testType = func(testTypeWCF);
                foreach (var testProblemWCF in testTypeWCF.试卷大题中试题WCF集合)
                {
                    试卷大题中试题 testProblem = 把试卷大题中试题WCF转化成试卷大题中试题(testProblemWCF);
                    if (testProblemWCF.子小题集合 != null && testProblemWCF.子小题集合.Count > 0)
                    {
                        testProblem.子小题集合 = new List<试卷大题中试题>();
                        foreach (var subTestProblemWCF in testProblemWCF.子小题集合)
                        {
                            试卷大题中试题 subTestProblem = 把试卷大题中试题WCF转化成试卷大题中试题(subTestProblemWCF);
                            testProblem.子小题集合.Add(subTestProblem);
                        }
                    }
                    testType.试卷大题中试题集合.Add(testProblem);
                }
                content.试卷中大题集合.Add(testType);
            }
            return content;
        }



        /// <summary>
        /// 因有无限循环，故无法用Mapper函数转化类
        /// </summary>
        private static 试卷大题中试题 把试卷大题中试题WCF转化成试卷大题中试题(LoveKaoServiceReference.试卷大题中试题WCF testProblemWCF)
        {
            试卷大题中试题 testProblem = new 试卷大题中试题();
            testProblem.ID = testProblemWCF.ID;
            testProblem.每小题分值 = testProblemWCF.每小题分值;
            testProblem.试卷中大题ID = testProblemWCF.试卷中大题ID;
            testProblem.试题内容ID = testProblemWCF.试题内容ID;
            testProblem.试题内容Json = testProblemWCF.试题内容Json;
            testProblem.顺序 = testProblemWCF.顺序;
            return testProblem;
        }



        /// <summary>
        ///  因试卷大题中试题有无限循环，故无法用Mapper函数转化
        /// </summary>      
        private static LoveKaoServiceReference.试卷外部信息 把试卷外部信息转化成试卷外部信息WCF(试卷外部信息 outside)
        {
            LoveKaoServiceReference.试卷外部信息 outsideWCF = new LoveKaoServiceReference.试卷外部信息();
            outsideWCF.ID = outside.爱考网ID;
            outsideWCF.创建人ID = outside.创建人ID;
            outsideWCF.分类列表 = new List<LoveKaoServiceReference.所属分类>();
            foreach (string belongSort in outside.分类列表)
            {
                outsideWCF.分类列表.Add(所属分类.把所属分类转化为所属分类WCF(belongSort,outside.ID));
            }
            outsideWCF.名称 = outside.名称;
            outsideWCF.试卷内容ID = outside.试卷内容ID;
            outsideWCF.说明 = outside.说明;
            outsideWCF.总分 = outside.总分;                 
            return outsideWCF;
        }



        private static LoveKaoServiceReference.试卷内容 把试卷内容转化成试卷内容WCF(试卷内容 content)
        {
            LoveKaoServiceReference.试卷内容 contentWCF = new LoveKaoServiceReference.试卷内容();
            contentWCF.ID = content.ID;
            contentWCF.名称 = content.名称;
            contentWCF.试卷外部信息ID = content.试卷外部信息ID;
            contentWCF.说明 = content.说明;
            contentWCF.提交备注 = content.提交备注;
            contentWCF.提交人ID = content.提交人ID;
            contentWCF.提交时间 = content.提交时间;
            contentWCF.试题图片 = content.试题图片;
            contentWCF.试卷中大题集合 = new List<LoveKaoServiceReference.试卷中大题>();
            foreach (var testType in content.试卷中大题集合)
            {
                LoveKaoServiceReference.试卷中大题 testTypeWCF = 把试卷中大题转化成试卷中大题WCF(testType);
                testTypeWCF.试卷大题中试题集合 = new List<LoveKaoServiceReference.试卷大题中试题>();
                foreach (var testProblem in testType.试卷大题中试题集合)
                {
                    LoveKaoServiceReference.试卷大题中试题 testProblemWCF = 把试卷大题中试题转化成试卷大题中试题WCF(testProblem);
                    if (testProblem.子小题集合 != null && testProblem.子小题集合.Count > 0)
                    {
                        testProblemWCF.子小题集合 = new List<LoveKaoServiceReference.试卷大题中试题>();
                        foreach (var subTestProblem in testProblem.子小题集合)
                        {
                            LoveKaoServiceReference.试卷大题中试题 subTestProblemWCF = 把试卷大题中试题转化成试卷大题中试题WCF(subTestProblem);
                            testProblemWCF.子小题集合.Add(subTestProblemWCF);
                        }
                    }
                    testTypeWCF.试卷大题中试题集合.Add(testProblemWCF);
                }
                contentWCF.试卷中大题集合.Add(testTypeWCF);
            }
            return contentWCF;
        }



        private static LoveKaoServiceReference.试卷中大题 把试卷中大题转化成试卷中大题WCF(试卷中大题 testType)
        {
            LoveKaoServiceReference.试卷中大题 testTypeWCF = new LoveKaoServiceReference.试卷中大题();
            testTypeWCF.ID = testType.ID;
            testTypeWCF.多选题评分策略 = testType.多选题评分策略;
            testTypeWCF.名称 = testType.名称;
            testTypeWCF.试卷内容ID = testType.试卷内容ID;
            testTypeWCF.顺序 = testType.顺序;
            testTypeWCF.说明 = testType.说明;
            return testTypeWCF;
        }



        /// <summary>
        /// 因有无限循环，故无法用Mapper函数转化类
        /// </summary>
        private static LoveKaoServiceReference.试卷大题中试题 把试卷大题中试题转化成试卷大题中试题WCF(试卷大题中试题 testProblem)
        {
            LoveKaoServiceReference.试卷大题中试题 testProblemWCF = new LoveKaoServiceReference.试卷大题中试题();
            testProblemWCF.ID = testProblem.ID;
            testProblemWCF.每小题分值 = testProblem.每小题分值;
            testProblemWCF.试卷中大题ID = testProblem.试卷中大题ID;
            testProblemWCF.试题内容ID = testProblem.试题内容ID;
            testProblemWCF.试题内容Json = testProblem.试题内容Json;
            testProblemWCF.顺序 = testProblem.顺序;
            return testProblemWCF;
        }




        private static List<试卷外部信息> 把试卷外部信息表集合转化为试卷外部信息集合(List<试卷外部信息表> listDBOutside)
        {
            List<试卷外部信息> listOutside = new List<试卷外部信息>();
            Func<试卷外部信息表, 试卷外部信息> func = Mapper.Create<试卷外部信息表, 试卷外部信息>();
            for (int i = 0; i < listDBOutside.Count; i++)
            {
                var v = func(listDBOutside[i]);
                v.ID = listDBOutside[i].ID;
                v.创建人ID = listDBOutside[i].创建人ID;
                v.试卷内容ID = listDBOutside[i].试卷内容ID;
                listOutside.Add(v);
            }
            return listOutside;
            
        }
 
             
        #endregion
       
    }
}
