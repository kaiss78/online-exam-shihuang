using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Transactions;
using System.Text.RegularExpressions;
using ExpressionMapper;
using System.IO;
using System.Drawing;
using System.Web;


namespace LoveKaoExam.Data
{

    public class 试题外部信息
    {
        private delegate void EditProblemEventhandler(试题外部信息 problemOutside, 试题内容 problemContent,
            List<string> 分类列表, string 试题json字符串);

        #region 变量

        private 试题内容 nowContent;
        private List<string> _分类列表;
        private 用户 member;
        private string _难易度;

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


        /// <summary>
        /// 0本地试题，1本地试题且已上传，2下载试题
        /// </summary>
        public byte 试题类型
        {
            get;
            set;
        }

        [JsonIgnore]
        public 试题内容 当前试题内容
        {
            get
            {
                if (nowContent == null)
                {
                    switch (this.小题型Enum)
                    {
                        case 11:
                            {
                                nowContent = 单选题.单选题查询.Where(a => a.ID == this.试题内容ID).First();
                                return nowContent;
                            }
                        case 12:
                            {
                                nowContent = 多选题.多选题查询.Where(a => a.ID == this.试题内容ID).First();
                                return nowContent;
                            }
                        case 13:
                            {
                                nowContent = 填空题.填空题查询.Where(a => a.ID == this.试题内容ID).First();
                                return nowContent;
                            }
                        case 14:
                            {
                                nowContent =选词填空.选词填空查询.Where(a => a.ID == this.试题内容ID).First();
                                return nowContent;
                            }
                        case 15:
                            {
                                nowContent = 完形填空.完形填空查询.Where(a => a.ID == this.试题内容ID).First();
                                return nowContent;
                            }
                        case 20:
                            {
                                nowContent = 判断题.判断题查询.Where(a => a.ID == this.试题内容ID).First();
                                return nowContent;
                            }
                        case 30:
                            {
                                nowContent = 连线题.连线题查询.Where(a => a.ID == this.试题内容ID).First();
                                return nowContent;
                            }
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                            {
                                nowContent = 复合题.复合题查询.Where(a => a.ID == this.试题内容ID).First();
                                return nowContent;
                            }
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                            {
                                nowContent = 问答题.问答题查询.Where(a => a.ID == this.试题内容ID).First();
                                return nowContent;
                            }
                        case 80:
                            {
                                nowContent =多题干共选项题.多题干共选项题查询.Where(a => a.ID == this.试题内容ID).First();
                                return nowContent;
                            }
                    }

                }
                return nowContent;
            }
            set
            {
                nowContent = value;
            }
        }




        public Guid 试题内容ID
        {
            get;
            set;
        }



        public List<string> 分类列表
        {
            get
            {
                if (_分类列表 == null)
                {
                    _分类列表 = 所属分类.所属分类查询.Where(a => a.相关ID == this.ID).Select(a=>a.分类名)
                        .OrderByDescending(a => a).ToList();
                }
                return _分类列表;
            }
            set
            {
                _分类列表 = value;
            }
        }




        public 用户 创建人
        {
            get
            {
                if (member == null)
                {
                    member = 用户.用户查询.Where(a => a.ID == this.创建人ID).First();
                    return member;
                }
                return member;
            }
            set
            {
                member = value;
            }

        }

       


        public Guid 创建人ID
        {
            get;
            set;
        }

       


        public DateTime 创建时间
        {
            get;
            set;
        }

       


        public int 小题型Enum
        {
            get;
            set;
        }



        public string 题型名称
        {
            get
            {
                return this.得到题型名称根据小题型Enum();
            }
        }

       

        public DateTime 最新更新时间
        {
            get;
            set;
        }


       
      
        [JsonIgnore]
        public string 试题查询内容
        {
            get;
            set;
        }


        public string 试题显示内容
        {
            get;
            set;
        }


        /// <summary>
        /// 0正常，1删除，4草稿
        /// </summary>
        public byte 试题状态Enum
        {
            get;
            set;
        }



        
        public decimal 难易度
        {
            get;
            set;
        }




        public static IQueryable<试题外部信息> 试题外部信息查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试题外部信息表.Select(a => new 试题外部信息
                    {
                        ID = a.ID,
                        创建人ID = a.创建人ID,
                        创建时间 = a.创建时间,
                        试题查询内容 = a.试题查询内容,
                        试题内容ID = a.试题内容ID,
                        试题显示内容 = a.试题显示列,
                        试题状态Enum = a.试题状态Enum,
                        小题型Enum = a.小题型Enum,
                        最新更新时间 = a.最新更新时间,
                        爱考网ID = a.爱考网ID,
                        试题类型 = a.试题类型,
                        难易度 = a.难易度
                    });
            }
        }

        #endregion


        #region 方法
       

        public static List<试题外部信息> 得到上传试题列表(string 关键字, int 第几页, int 页的大小, out int 返回总条数)
        {       
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            IQueryable<试题外部信息表> query = db.试题外部信息表.Where(a => a.创建人ID == 用户信息.CurrentUser.用户ID
                && a.试题状态Enum == 0 && a.试题类型 < 2);
            if (String.IsNullOrEmpty(关键字) == false)
            {
                关键字 = 关键字.TrimStart();
                关键字 = 关键字.TrimEnd();
                //按题干查询
                query = query.Where(a => a.试题查询内容.Contains(关键字));
                //如果是分类，再按分类查询，并合并结果
                if (db.系统分类表.Any(a => a.分类名称 == 关键字) == true)
                {
                    IQueryable<试题外部信息表> queryBySort = 按某个分类查询试题(关键字, db);
                    queryBySort = queryBySort.Where(a => a.创建人ID == 用户信息.CurrentUser.用户ID
                        && a.试题状态Enum == 0 && a.试题类型 < 2);
                    query = query.Union(queryBySort);
                }
            }
            返回总条数 = query.Distinct().Count();
            List<试题外部信息表> list = query.Distinct().OrderByDescending(a => a.创建时间).OrderBy(a => a.试题类型)
                .Skip(第几页 * 页的大小).Take(页的大小).ToList();
            List<试题外部信息> listOutside = 把试题外部信息表集合转化为试题外部信息集合(list);
            return listOutside;
        }



        public static List<Guid> 得到已上传试题爱考网ID集合()
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            List<Guid> listOutsideId = db.试题外部信息表.Where(a => a.创建人ID == 用户信息.CurrentUser.用户ID
                && a.试题类型 == 1).Select(a => a.ID).ToList();
            return listOutsideId;
        }



        public static List<Guid> 得到已下载试题爱考网ID集合()
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            List<Guid> listOutsideId = db.试题外部信息表.Where(a => a.创建人ID == 用户信息.CurrentUser.用户ID)
                .Select(a => a.爱考网ID).ToList();
            return listOutsideId;
        }





        /// <summary>
        /// 返回0上传成功，1账号未绑定，2绑定账号被禁用，3禁止绑定任何账号
        /// </summary>       
        public static int 上传试题(List<Guid> 试题外部信息ID集合,out List<试题外部信息> 已存在试题集合)
        {
            try
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                //获取绑定账号信息
                绑定账号表 bind = db.绑定账号表.Where(a => a.本地账号ID == 用户信息.CurrentUser.用户ID).FirstOrDefault();
                if (bind == null)
                {
                    已存在试题集合 = new List<试题外部信息>();
                    return 1;
                }
                List<试题外部信息> 试题外部信息集合 = 试题外部信息查询.Where(a => 试题外部信息ID集合.Contains(a.ID)).ToList();
                //给试题外部信息集合赋值当前试题内容属性
                List<Guid> listContentId = 试题外部信息集合.Select(a => a.试题内容ID).ToList();
                List<试题内容> listContent = 试题内容.试题内容查询.Where(a => listContentId.Contains(a.ID)).ToList();
                foreach (试题外部信息 outside in 试题外部信息集合)
                {
                    outside.当前试题内容 = listContent.Where(a => a.ID == outside.试题内容ID).First();
                }
                Dictionary<string, byte[]> dicPicture = new Dictionary<string, byte[]>();
                List<string> listJson = new List<string>();
                foreach (试题外部信息 outside in 试题外部信息集合)
                {
                    string Json = 试题内容.转化成完整Json字符串(outside.当前试题内容, outside);
                    listJson.Add(Json);
                    Dictionary<string, byte[]> oneDic = 获取试题图片(Json);
                    foreach (var subOneDic in oneDic)
                    {
                        dicPicture.Add(subOneDic.Key, subOneDic.Value);
                    }
                }             
                List<Guid> listExistOutsideId = new List<Guid>();
                LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                int result = client.上传试题(out listExistOutsideId,listJson,dicPicture,bind.爱考网账号, bind.爱考网密码);
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
                    //更新试题类型
                    List<试题外部信息表> listOutside = db.试题外部信息表.Where(a => 试题外部信息ID集合.Contains(a.ID)).ToList();
                    foreach (试题外部信息表 outside in listOutside)
                    {
                        outside.试题类型 = 1;
                    }
                    db.SaveChanges();
                    已存在试题集合 = 试题外部信息.试题外部信息查询.Where(a => listExistOutsideId.Contains(a.ID)).ToList();
                }
                else
                {
                    已存在试题集合 = new List<试题外部信息>();
                }
                return result;
            }
            catch (Exception ex)
            {
                异常处理.Catch异常处理(ex.Message);
                throw;
            }
        }



        public static Dictionary<string, byte[]> 获取试题图片(string 试题Json)
        {
            string matchStr = @"<img[^>]*?src=(?<g1>\\""|""|\\'|'|)(?<g2>/[^>]*[^/].)(?<g3>jpg|bmp|gif|png|jpeg)\1[^>]*?>";
            MatchCollection matchList = Regex.Matches(试题Json, matchStr, RegexOptions.IgnoreCase);
            Dictionary<string, byte[]> dicPicture = new Dictionary<string, byte[]>();
            for (int i = 0; i < matchList.Count; i++)
            {
                string rootDir = HttpContext.Current.Request.PhysicalApplicationPath;
                string imageURL = matchList[i].Groups["g2"].Value + matchList[i].Groups["g3"].Value;  
                try
                {
                    FileStream fs = new FileStream(rootDir + imageURL, FileMode.Open);
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, data.Length);
                    fs.Close();
                    string[] subString = imageURL.Split("/".ToArray());
                    string pictureName = subString[subString.Length - 1];
                    dicPicture.Add(pictureName, data);
                }
                catch (Exception)
                {
                }
            }
            return dicPicture;
        }



        public static List<LoveKaoServiceReference.试题外部信息WCF> 得到下载试题列表(string 关键字, int 第几页, int 页的大小, out int 返回总条数)
        {
            try
            {
                LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                List<LoveKaoServiceReference.试题外部信息WCF> listOutside = client.得到主站下载试题列表(out 返回总条数, 关键字,用户信息.CurrentUser.用户名, 第几页, 页的大小);
                client.Close();
                return listOutside;
            }
            catch (Exception)
            {
                throw new Exception("连接爱考网服务器出错，请稍后再试！");
            }                     
        }



       
        /// <summary>
        /// 返回0下载成功，1账号未绑定，2绑定账号被禁用，3没有足够的下载积分，4禁止绑定任何账号
        /// </summary>
        public static int 下载试题(List<Guid> 试题外部信息WCFID集合)
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
                LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                //判断是否有足够的积分下载
                int type;
                string informationJson = client.得到上传下载信息(out type, bind.爱考网账号, bind.爱考网密码);
                if (type == 1)
                {
                    异常处理.抛出异常(1);
                }
                else if (type == 2)
                {
                    异常处理.抛出异常(2);
                }
                上传下载信息 information = 上传下载信息.把Json转化成上传下载信息(informationJson);
                if (information.可下载试题数量 < 试题外部信息WCFID集合.Count)
                {
                    异常处理.抛出异常(3);
                }
                Dictionary<string, byte[]> dicPicture = new Dictionary<string, byte[]>();
                List<string> ListJson = new List<string>();
                int result = client.下载试题(out ListJson, out dicPicture, 试题外部信息WCFID集合, bind.爱考网账号, bind.爱考网密码);
                client.Close();
                if (result == 0)
                {
                    string userName = 用户信息.CurrentUser.用户名;
                    List<string> listSaveJson = 替换试题图片路径(ListJson, userName);
                    保存主站下载试题(listSaveJson, db);
                    保存图片(dicPicture, userName);
                }
                return result;
            }
            catch (Exception ex)
            {
                异常处理.Catch异常处理(ex.Message);
                throw;
            }        
        }



        public static void 保存图片(Dictionary<string,byte[]> 图片集合,string 用户名)
        {
            for (int i = 0; i < 图片集合.Count; i++)
            {
                string rootDir = HttpContext.Current.Request.PhysicalApplicationPath;
                string imageURL = rootDir + "UploadFiles/" + 用户名 + "/Images/" + 图片集合.ElementAt(i).Key + "";
                //本地存放文件夹
                string tempFolder = System.IO.Path.GetDirectoryName(imageURL);
                //判断文件夹是否已存在
                if (!System.IO.Directory.Exists(tempFolder))
                {
                    System.IO.Directory.CreateDirectory(tempFolder);
                }
                FileStream fs = new FileStream(imageURL, FileMode.Create);
                fs.Write(图片集合.ElementAt(i).Value, 0, 图片集合.ElementAt(i).Value.Length);
                fs.Close();
            }
        }



        public static List<string> 替换试题图片路径(List<string> listJson,string 用户名)
        {
            List<string> listSaveJson = new List<string>();
            foreach (string Json in listJson)
            {
                string saveJson = 替换试题图片路径(Json,用户名);
                listSaveJson.Add(saveJson);
            }
            return listSaveJson;
        }



        public static string 替换试题图片路径(string 试题Json,string 用户名)
        {
            string saveJson = 试题Json.Replace("http://www.lovekao.com", "");
            saveJson = Regex.Replace(saveJson, @"/UploadFiles/Question/(?<g1>[^>]*[^/])/Images/(?<g2>[^>]*[^/].)(?<g3>jpg|bmp|gif|png|jpeg)", "/UploadFiles/" + 用户名 + "/Images/${g2}${g3}", RegexOptions.IgnoreCase);
            saveJson = saveJson.Replace("/Members/EditSubject/UnderLine.aspx", "/Shared/UnderLine");
            return saveJson;
        }




        /// <param name="类型">0全部，1自己出的，2草稿,3已上传的，4下载的</param>      
        public static List<试题外部信息> 得到某考官试题(string 关键字, Guid 考官ID,int 类型, int 第几页, int 页的大小, out int 返回总条数)
        {
            LoveKaoExamEntities db=new LoveKaoExamEntities();
            IQueryable<试题外部信息表> query = db.试题外部信息表.Where(a=>a.创建人ID==考官ID);
            switch (类型)
            {
                case 0:
                    {
                        query = query.Where(a => a.试题状态Enum != 1);
                        break;
                    }
                case 1:
                    {
                        query = query.Where(a => a.试题状态Enum == 0 && a.试题类型 < 2);                      
                        break;
                    }
                case 2:
                    {
                        query = query.Where(a => a.试题状态Enum == 4);                      
                        break;
                    }
                case 3:
                    {
                        query = query.Where(a => a.试题状态Enum == 0 && a.试题类型 == 1);
                        break;
                    }
                case 4:
                    {
                        query = query.Where(a => a.试题状态Enum == 0 && a.试题类型 == 2);
                        break;
                    }
            }
            if (String.IsNullOrEmpty(关键字) == false)
            {
                IQueryable<试题外部信息表> queryByContent = query.Where(a => a.试题查询内容.Contains(关键字));
                //若是分类，再按分类查询，并合并结果
                if (db.系统分类表.Any(a => a.分类名称 == 关键字) == true)
                {
                    IQueryable<试题外部信息表> queryBySort = 按某个分类查询试题(关键字, db);
                    queryBySort = queryBySort.Intersect(query);
                    query = queryByContent.Union(queryBySort);
                }
                else
                {
                    query = queryByContent;
                }
            }
            返回总条数 = query.Count();
            List<试题外部信息表> listOutsideTable = query.OrderByDescending(a => a.创建时间).Skip(第几页 * 页的大小)
                .Take(页的大小).ToList();
            List<试题外部信息> listOutside = 把试题外部信息表集合转化为试题外部信息集合(listOutsideTable);
            return listOutside;
        }



        public string 转化成Json字符串()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            json = json.Replace("\"小题型Enum\": " + 小题型Enum + "", "\"小题型Enum\":\"" + 小题型Enum + "\"");
            return json;
        }


        public string 转化成试题外部信息Json字符串()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            json = json.Replace("\"小题型Enum\": " + 小题型Enum + "", "\"小题型Enum\":\"" + 小题型Enum + "\"");
            json = "{\"content\":null,\"outside\":" + json + "}";
            return json;
        }


        public static 试题外部信息 把Json转化成试题外部信息(string 试题Json字符串)
        {
            JObject bo = JObject.Parse(试题Json字符串);
            string outsideString = bo["outside"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
           
            //试题外部信息 outsideWcf = new 试题外部信息();
            //try
            //{
            //    //outsideWcf = JsonConvert.DeserializeObject<试题外部信息>(outsideString);
            //    //outsideWcf = jss.Deserialize<LoveKaoServiceReference.试题外部信息>(outsideString);
            //}
            //catch (Exception ex)
            //{
            //    string e = ex.ToString();
            //}
            //试题外部信息 outside = 把试题外部信息WCF转化成试题外部信息(outsideWcf);
            试题外部信息 outside = jss.Deserialize<试题外部信息>(outsideString);
            return outside;
        }



        public static 试题外部信息 把试题外部信息WCF转化成试题外部信息(LoveKaoServiceReference.试题外部信息 outsideWcf)
        {
            试题外部信息 outside = new 试题外部信息();
            outside.ID = outsideWcf.ID;
            outside.创建人ID = outsideWcf.创建人ID;
            outside.创建时间 = outsideWcf.创建时间;
            outside.难易度 = outsideWcf.初始难易度;
            outside.试题查询内容 = outsideWcf.试题查询内容;
            outside.试题类型 = 2;
            outside.试题内容ID = outsideWcf.试题内容ID;
            outside.试题显示内容 = outsideWcf.试题显示内容;
            outside.试题状态Enum = 0;
            outside.小题型Enum = outsideWcf.小题型Enum;
            outside.最新更新时间 = outsideWcf.最新更新时间;
            outside.分类列表 = new List<string>();
            foreach (var sort in outsideWcf.分类列表)
            {
                outside.分类列表.Add(sort.分类名);
            }
            return outside;
        }



      


        public static string 得到题干文本根据试题外部信息ID(Guid 试题外部信息ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            试题外部信息表 outside = db.试题外部信息表.FirstOrDefault(a => a.ID == 试题外部信息ID);           
            string text = outside.试题显示列;
            return text;
        }



        /// <param name="类型">0保存本站试题，1保存下载试题，2保存下载试卷中试题</param>     
        private static Dictionary<Guid, Guid> 保存试题(int 题型, 试题外部信息 outside, string 试题Json, int 类型, List<string> 所属分类集合
            , bool 是否只保存试题内容, LoveKaoExamEntities db)
        {
            Dictionary<Guid, Guid> dic = new Dictionary<Guid, Guid>();
            switch (题型)
            {
                case 11:
                    {
                        单选题 single = 单选题.把Json转化成试题内容(试题Json);
                        if (类型 == 0)
                        {
                            保存试题相关信息(0, outside, single, 所属分类集合, 试题Json, db);
                        }
                        else if (类型 == 1)
                        {
                            单选题.给单选题选项赋值新ID(single);
                            保存试题(outside, single, 所属分类集合, 试题Json, 1, db);
                        }
                        else
                        {
                            单选题.给单选题选项赋值新ID(single);
                            dic = 保存下载试卷中试题(outside, single, 所属分类集合, 试题Json, 是否只保存试题内容, db);
                        }
                        break;
                    }
                case 12:
                    {
                        多选题 multi = 多选题.把Json转化成试题内容(试题Json);
                        if (类型 == 0)
                        {
                            保存试题相关信息(0, outside, multi, 所属分类集合, 试题Json, db);
                        }
                        else if (类型 == 1)
                        {
                            多选题.给多选题选项赋值新ID(multi);
                            保存试题(outside, multi, 所属分类集合, 试题Json, 1, db);
                        }
                        else
                        {
                            多选题.给多选题选项赋值新ID(multi);
                            dic = 保存下载试卷中试题(outside, multi, 所属分类集合, 试题Json, 是否只保存试题内容, db);
                        }
                        break;
                    }
                case 13:
                    {
                        填空题 fill = 填空题.把Json转化成试题内容(试题Json);
                        if (类型 == 0)
                        {
                            保存试题相关信息(0, outside, fill, 所属分类集合, 试题Json, db);
                        }
                        else if (类型 == 1)
                        {
                            保存试题(outside, fill, 所属分类集合, 试题Json, 1, db);
                        }
                        else
                        {
                            dic = 保存下载试卷中试题(outside, fill, 所属分类集合, 试题Json, 是否只保存试题内容, db);
                        }
                        break;
                    }
                case 14:
                    {
                        选词填空 diction = 选词填空.把Json转化成试题内容(试题Json);
                        if (类型 == 0)
                        {
                            保存试题相关信息(0, outside, diction, 所属分类集合, 试题Json, db);
                        }
                        else if (类型 == 1)
                        {
                            选词填空.给选词填空选项赋值新ID(diction);
                            保存试题(outside, diction, 所属分类集合, 试题Json, 1, db);
                        }
                        else
                        {
                            选词填空.给选词填空选项赋值新ID(diction);
                            dic = 保存下载试卷中试题(outside, diction, 所属分类集合, 试题Json, 是否只保存试题内容, db);
                        }
                        break;
                    }
                case 15:
                    {
                        完形填空 cloze = 完形填空.把Json转化成试题内容(试题Json);
                        if (类型 == 0)
                        {
                            保存试题相关信息(0, outside, cloze, 所属分类集合, 试题Json, db);
                        }
                        else if (类型 == 1)
                        {
                            完形填空.给完形填空选项赋值新ID(cloze);
                            保存试题(outside, cloze, 所属分类集合, 试题Json, 1, db);
                        }
                        else
                        {
                            完形填空.给完形填空选项赋值新ID(cloze);
                            dic = 保存下载试卷中试题(outside, cloze, 所属分类集合, 试题Json, 是否只保存试题内容, db);
                        }
                        break;
                    }
                case 20:
                    {
                        判断题 judge = 判断题.把Json转化成试题内容(试题Json);
                        if (类型 == 0)
                        {
                            保存试题相关信息(0, outside, judge, 所属分类集合, 试题Json, db);
                        }
                        else if (类型 == 1)
                        {
                            保存试题(outside, judge, 所属分类集合, 试题Json, 1, db);
                        }
                        else
                        {
                            dic = 保存下载试卷中试题(outside, judge, 所属分类集合, 试题Json, 是否只保存试题内容, db);
                        }
                        break;
                    }
                case 30:
                    {
                        连线题 link = 连线题.把Json转化成试题内容(试题Json);
                        if (类型 == 0)
                        {
                            保存试题相关信息(0, outside, link, 所属分类集合, 试题Json, db);
                        }
                        else if (类型 == 1)
                        {
                            连线题.给连线题选项赋值新ID(link);
                            保存试题(outside, link, 所属分类集合, 试题Json, 1, db);
                        }
                        else
                        {
                            连线题.给连线题选项赋值新ID(link);
                            dic = 保存下载试卷中试题(outside, link, 所属分类集合, 试题Json, 是否只保存试题内容, db);
                        }
                        break;
                    }
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                    {
                        复合题 combination = 复合题.把Json转化成试题内容(试题Json);
                        foreach (试题内容 subCombination in combination.子小题集合)
                        {
                            subCombination.爱考网ID = subCombination.ID;
                            subCombination.ID = Guid.NewGuid();
                            dic.Add(subCombination.爱考网ID, subCombination.ID);
                        }
                        if (类型 == 0)
                        {
                            保存试题相关信息(0, outside, combination, 所属分类集合, 试题Json, db);
                        }
                        else if (类型 == 1)
                        {
                            保存试题(outside, combination, 所属分类集合, 试题Json, 1, db);
                        }
                        else
                        {
                            Dictionary<Guid,Guid> reDic = 保存下载试卷中试题(outside, combination, 所属分类集合, 试题Json, 是否只保存试题内容, db);
                            dic.Add(reDic.ElementAt(0).Key, reDic.ElementAt(0).Value);
                        }
                       
                        break;
                    }
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                    {
                        问答题 questionAnswer = 问答题.把Json转化成试题内容(试题Json);
                        if (类型 == 0)
                        {
                            保存试题相关信息(0, outside, questionAnswer, 所属分类集合, 试题Json, db);
                        }
                        else if (类型 == 1)
                        {
                            保存试题(outside, questionAnswer, 所属分类集合, 试题Json, 1, db);
                        }
                        else
                        {
                            dic = 保存下载试卷中试题(outside, questionAnswer, 所属分类集合, 试题Json, 是否只保存试题内容, db);
                        }
                        break;
                    }
                case 80:
                    {
                        多题干共选项题 common = 多题干共选项题.把Json转化成试题内容(试题Json);
                        foreach (试题内容 subcommon in common.子小题集合)
                        {
                            subcommon.爱考网ID = subcommon.ID;
                            subcommon.ID = Guid.NewGuid();
                            dic.Add(subcommon.爱考网ID, subcommon.ID);
                        }
                        if (类型 == 0)
                        {
                            foreach (题干 content in common.子小题集合)
                            {
                                content.爱考网ID = content.ID;
                            }
                            保存试题相关信息(0, outside, common, 所属分类集合, 试题Json, db);
                        }
                        else if (类型 == 1)
                        {
                            多题干共选项题.给多题干共选项题子小题内容和选项赋值新ID(common);
                            保存试题(outside, common, 所属分类集合, 试题Json, 1, db);
                        }
                        else
                        {
                            多题干共选项题.给多题干共选项题子小题内容和选项赋值新ID(common);
                            return 保存下载试卷中试题(outside, common, 所属分类集合, 试题Json, 是否只保存试题内容, db);
                        }
                        break;
                    }
            }
            return dic;
        }



        public static Dictionary<Guid,Guid> 保存下载试卷中试题(试题外部信息 outside,string 试题Json, List<string> 所属分类集合,bool 是否只保存试题内容, LoveKaoExamEntities db)
        {
            int smallType = 试题内容.得到试题的小题型(试题Json);
            return 保存试题(smallType, outside, 试题Json, 2, 所属分类集合, 是否只保存试题内容, db);
        }



        public static Dictionary<Guid,Guid> 保存下载试卷中试题(试题外部信息 problemOutside,试题内容 试题内容, List<string> 所属分类集合, string 分站试题Json
            ,bool 是否只保存试题内容, LoveKaoExamEntities db)
        {
            Guid outsideLoveKaoId = 试题内容.试题外部信息ID;
            Guid contentLoveKaoId = 试题内容.ID;
            试题内容.ID = Guid.NewGuid();
            试题内容.爱考网ID = contentLoveKaoId;
            if (是否只保存试题内容 == false)
            {
                试题外部信息表 outside = new 试题外部信息表();
                outside.ID = Guid.NewGuid();
                outside.爱考网ID = outsideLoveKaoId;
                outside.试题类型 = 2;
                outside.创建人ID = 用户信息.CurrentUser.用户ID;
                outside.创建时间 = problemOutside.创建时间;
                outside.试题查询内容 = 试题内容.生成查询内容();
                outside.试题内容ID = 试题内容.ID;
                outside.试题显示列 = 试题内容.生成试题显示列();
                outside.试题状态Enum = 0;
                outside.小题型Enum = 试题内容.小题型Enum;
                outside.最新更新时间 = problemOutside.最新更新时间;
                outside.难易度 = 0.5m;
                试题内容.试题外部信息ID = outside.ID;
                db.试题外部信息表.AddObject(outside);
                所属分类.添加相关信息所属分类(所属分类集合, outside.创建人ID, outside.ID, 0, db);
            }
            else
            {
                Guid outsideId = db.试题外部信息表.Where(a => a.创建人ID == 用户信息.CurrentUser.用户ID
                    && a.爱考网ID == problemOutside.爱考网ID).First().ID;
                试题内容.试题外部信息ID = outsideId;
            }
            JObject bo = JObject.Parse(分站试题Json);
            string json字符串 = bo["content"].ToString();
            试题内容.操作人ID = 用户信息.CurrentUser.用户ID;
            试题内容.难易度 = 0.5m;
            试题内容.保存(db, json字符串);
            Dictionary<Guid, Guid> dic = new Dictionary<Guid, Guid>();
            dic.Add(contentLoveKaoId, 试题内容.ID);
            return dic;
        }




        private static void 保存主站下载试题(List<string> 试题Json集合,LoveKaoExamEntities db)
        {
            考官.判断是否是考官();
            List<试题外部信息> listOutside = new List<试题外部信息>();
            //所有的所属分类集合
            List<string> listAllBelongSort = new List<string>();
            foreach (string 试题Json in 试题Json集合)
            {
                试题外部信息 outside = 试题外部信息.把Json转化成试题外部信息(试题Json);              
                List<string> listBelongSort = outside.分类列表.ToList();
                listAllBelongSort.AddRange(listBelongSort);
                listOutside.Add(outside);
            }
            所属分类.所属分类处理(listAllBelongSort, 用户信息.CurrentUser.用户ID, db);
            //using (TransactionScope scope = new TransactionScope())
            //{
                //随机一个秒数，使创建时间不在同一秒
                Random random = new Random();
                DateTime time = DateTime.Now;
                //记录随机的秒数，以避免重复
                List<int> listSeconds = new List<int>();
                for (int i = 0; i < listOutside.Count; i++)
                {                  
                    int seconds = random.Next(0, 60);
                    if (listSeconds.Contains(seconds))
                    {
                        seconds = random.Next(0, 60);
                    }
                    listSeconds.Add(seconds);
                    List<string> listBelongSort = listOutside[i].分类列表.ToList();
                    listOutside[i].创建时间 = time.AddSeconds(seconds);
                    listOutside[i].最新更新时间 = time.AddSeconds(seconds);
                    Guid outsideLoveKaoId = listOutside[i].ID;
                    listOutside[i].爱考网ID = outsideLoveKaoId;
                    listOutside[i].ID = Guid.NewGuid();
                    listOutside[i].试题类型 = 2;
                    listOutside[i].难易度 = 0.5m;
                    保存试题(listOutside[i].小题型Enum, listOutside[i], 试题Json集合[i], 1, listBelongSort,false, db);
                }
                //scope.Complete();
            //}
        }




        public static void 保存试题相关信息(string 试题Json字符串)
        {
            考官.判断是否是考官();
            试题外部信息 outside = 试题外部信息.把Json转化成试题外部信息(试题Json字符串);
            if (outside.分类列表.Count == 0)
            {
                throw new Exception("请至少填写一个分类！");
            }
            List<string> listSort =outside.分类列表.ToList();
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            保存试题(outside.小题型Enum, outside, 试题Json字符串, 0, listSort,false, db);
           

        }


        public static void 保存试题相关信息(int 类型,试题外部信息 problemOutside, 试题内容 problemContent, List<string> 分类列表, string 试题json字符串,LoveKaoExamEntities db)
        {
            List<string> listBelongSort = 所属分类.所属分类处理(分类列表, problemOutside.创建人ID, db);
            problemOutside.最新更新时间 = DateTime.Now;
            problemOutside.创建时间 = DateTime.Now;
            problemContent.爱考网ID = problemContent.ID;
            problemOutside.ID = problemContent.试题外部信息ID;
            problemOutside.爱考网ID = problemOutside.ID;
            保存试题(problemOutside, problemContent, 分类列表, 试题json字符串, 类型, db);
        }



        private static void 保存试题(试题外部信息 problemOutside, 试题内容 problemContent, List<string> 分类列表, string 试题json字符串,int 类型, LoveKaoExamEntities db)
        {
            if (类型 == 1)
            {
                problemOutside.创建人ID = 用户信息.CurrentUser.用户ID;
                problemContent.操作人ID = 用户信息.CurrentUser.用户ID;
            }
            试题外部信息表 outside = new 试题外部信息表();
            outside.ID = problemOutside.ID;
            outside.爱考网ID = problemOutside.爱考网ID;
            outside.试题类型 = problemOutside.试题类型;
            outside.创建人ID = problemOutside.创建人ID;
            outside.创建时间 = problemOutside.创建时间;
            outside.最新更新时间 = problemOutside.最新更新时间;
            outside.试题状态Enum = problemOutside.试题状态Enum;
            outside.小题型Enum = problemOutside.小题型Enum;
            if (类型 == 1)
            {
                Guid contentLoveKaoId = problemContent.ID;
                problemContent.ID = Guid.NewGuid();
                problemContent.爱考网ID = contentLoveKaoId;
                problemContent.试题外部信息ID = problemOutside.ID;
            }
            outside.试题内容ID = problemContent.ID;     
            outside.试题查询内容 = problemContent.生成查询内容();
            outside.试题显示列 = problemContent.生成试题显示列();
            outside.难易度 = problemOutside.难易度;
            using (TransactionScope scope = new TransactionScope())
            {
                db.试题外部信息表.AddObject(outside);
                所属分类.添加相关信息所属分类(分类列表, outside.创建人ID, outside.ID, 0, db);
                //db.SaveChanges();
                JObject bo = JObject.Parse(试题json字符串);
                string json字符串 = bo["content"].ToString();
                problemContent.难易度 = outside.难易度;
                problemContent.保存(db, json字符串);
                scope.Complete();
            }

        }



        private static void 修改试题(EditProblemEventhandler editProblem,string 试题Json字符串)
        {
            试题外部信息 outside = 试题外部信息.把Json转化成试题外部信息(试题Json字符串);
            List<string> listSort = outside.分类列表.ToList();        
            switch (outside.小题型Enum)
            {
                case 11:
                    {
                        单选题 single = 单选题.把Json转化成试题内容(试题Json字符串);
                        editProblem(outside, single, listSort, 试题Json字符串);
                        break;
                    }
                case 12:
                    {
                        多选题 multi = 多选题.把Json转化成试题内容(试题Json字符串);
                        editProblem(outside, multi, listSort, 试题Json字符串);
                        break;
                    }
                case 13:
                    {
                        填空题 fill = 填空题.把Json转化成试题内容(试题Json字符串);
                        editProblem(outside, fill, listSort, 试题Json字符串);
                        break;
                    }
                case 14:
                    {
                        选词填空 diction = 选词填空.把Json转化成试题内容(试题Json字符串);
                        editProblem(outside, diction, listSort, 试题Json字符串);
                        break;
                    }
                case 15:
                    {
                        完形填空 cloze = 完形填空.把Json转化成试题内容(试题Json字符串);
                        editProblem(outside, cloze, listSort, 试题Json字符串);
                        break;
                    }
                case 20:
                    {
                        判断题 judge = 判断题.把Json转化成试题内容(试题Json字符串);
                        editProblem(outside, judge, listSort, 试题Json字符串);
                        break;
                    }
                case 30:
                    {
                        连线题 link = 连线题.把Json转化成试题内容(试题Json字符串);
                        editProblem(outside, link, listSort, 试题Json字符串);
                        break;
                    }
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                    {
                        复合题 combination = 复合题.把Json转化成试题内容(试题Json字符串);
                        editProblem(outside, combination, listSort, 试题Json字符串);
                        break;
                    }
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                    {
                        问答题 questionAnswer = 问答题.把Json转化成试题内容(试题Json字符串);
                        editProblem(outside, questionAnswer, listSort, 试题Json字符串);
                        break;
                    }
                case 80:
                    {
                        多题干共选项题 common = 多题干共选项题.把Json转化成试题内容(试题Json字符串);
                        editProblem(outside, common, listSort, 试题Json字符串);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }





        public static void 修改正常试题(string 试题Json字符串)
        {            
            修改试题(修改正常试题, 试题Json字符串);
        }


        public static void 修改正常试题(试题外部信息 problemOutside, 试题内容 problemContent, List<string> 分类列表, string 试题json字符串)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
                JObject bo = JObject.Parse(试题json字符串);
                var typeEnum = bo["outside"]["试题状态Enum"].ToString();
                if (typeEnum == "4")
                {
                    throw new Exception("正常试题不能改为草稿！");
                }
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                试题外部信息表 outsideTable = db.试题外部信息表.FirstOrDefault(a => a.ID == problemOutside.ID);
                异常处理.删除修改权限判断(outsideTable.创建人ID);
                if (outsideTable.试题状态Enum == 1)
                {
                    throw new Exception("该试题已被删除！");
                }
                outsideTable.最新更新时间 = DateTime.Now;
                outsideTable.难易度 = problemOutside.难易度;

                if (problemContent != null)
                {
                    outsideTable.试题查询内容 = problemContent.生成查询内容();
                    outsideTable.试题显示列 = problemContent.生成试题显示列();
                    outsideTable.试题内容ID = problemContent.ID;
                }
                List<string> listBelongSort = 所属分类.所属分类处理(分类列表, outsideTable.创建人ID, db);
                所属分类.修改相关信息更新所属分类(listBelongSort, 0, outsideTable.创建人ID, outsideTable.ID, db);
               
                if (problemContent != null)
                {
                    string json字符串 = bo["content"].ToString();
                    problemContent.保存(db, json字符串);
                }
                if (problemContent == null)
                {
                    db.SaveChanges();
                }
               // scope.Complete();
           // }
        }



        public static void 修改草稿试题(string 试题Json字符串)
        {
            修改试题(修改草稿试题, 试题Json字符串);
        }



        private static void 修改草稿试题(试题外部信息 problemOutside,试题内容 problemContent, List<string> 分类列表, string 试题json字符串)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                试题外部信息表 outsideTable = db.试题外部信息表.FirstOrDefault(a => a.ID == problemOutside.ID);
                List<string> listBelongSort = 所属分类.所属分类处理(分类列表, outsideTable.创建人ID, db);
                if (outsideTable.试题状态Enum == 1)
                {
                    throw new Exception("该试题已被删除！");
                }
                //原试题内容ID
                Guid oldContentId = outsideTable.试题内容ID;
                outsideTable.试题状态Enum = problemOutside.试题状态Enum;
                outsideTable.最新更新时间 = DateTime.Now;

                if (problemContent != null)
                {
                    outsideTable.试题查询内容 = problemContent.生成查询内容();
                    outsideTable.试题显示列 = problemContent.生成试题显示列();
                    outsideTable.试题内容ID = problemContent.ID;
                }

                所属分类.修改相关信息更新所属分类(listBelongSort, 0, outsideTable.创建人ID, outsideTable.ID, db);

                if (problemContent != null)
                {
                    outsideTable.试题内容ID = problemContent.ID;
                    试题内容表 dbContent = db.试题内容表.FirstOrDefault(a => a.ID == oldContentId);
                    List<自由题空格表> listSpace = db.自由题空格表.Where(a=>a.试题内容ID==oldContentId).ToList();
                    if (listSpace.Count != 0)
                    {
                        List<Guid> listGroupId = listSpace.Where(a => a.自由题选项组ID != null).GroupBy(a => a.自由题选项组ID).Select(a => a.Key.Value).ToList();
                        if (listGroupId.Count != 0)
                        {
                            List<自由题选项组表> listGroup = db.自由题选项组表.Where(a => listGroupId.Contains(a.ID)).ToList();
                            foreach (var group in listGroup)
                            {
                                db.自由题选项组表.DeleteObject(group);
                            }
                        }
                    }
                    db.试题内容表.DeleteObject(dbContent);
                    JObject bo = JObject.Parse(试题json字符串);
                    string json字符串 = bo["content"].ToString();
                    problemContent.保存(db, json字符串);
                }
                if (problemContent == null)
                {
                    db.SaveChanges();
                }
                //scope.Complete();
            //}
        }

        public static void 删除试题(Guid 试题外部信息ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            试题外部信息表 problem = db.试题外部信息表.FirstOrDefault(a => a.ID == 试题外部信息ID);
            异常处理.删除修改权限判断(problem.创建人ID);
            problem.试题状态Enum = 1;
            db.SaveChanges();
        }



        public static List<试题外部信息> 手工出卷查询题目(取题条件 getProblemCondition, int 第几页, int 页的大小, out int 返回总条数)
        {
            if (getProblemCondition.网站分类集合 != null && getProblemCondition.网站分类集合 != "")
            {
                string[] sortList = getProblemCondition.网站分类集合.Split(",".ToCharArray());
                if (sortList.Length > 10)
                {
                    throw new Exception("分类选择不能超过10个！");
                }
                //判断分类是否是已存在的分类
                List<string> listSort = sortList.ToList();
                int count = 分类.分类查询.Where(a => listSort.Contains(a.分类名称)).Count();
                if (listSort.Count != count)
                {
                    throw new Exception("您选择的分类不存在，请选择已有分类！");
                }
            }
            IQueryable<试题外部信息表> query = 判断取题条件(getProblemCondition);           
            返回总条数 = query.Distinct().Count();
            if (返回总条数 == 0)
            {
                throw new Exception("没有找到符合条件的试题！");
            }
            List<试题外部信息表> list = query.Distinct().OrderByDescending(a => a.创建时间)
                .Skip(第几页 * 页的大小).Take(页的大小).ToList();
            List<试题外部信息> listOutside = 把试题外部信息表集合转化为试题外部信息集合(list);
            //查找对应的试题内容并赋值
            List<Guid> listContentId = list.Select(a => a.试题内容ID).ToList();          
            List<试题内容> listContent = 试题内容.试题内容查询.Where(a => listContentId.Contains(a.ID)).ToList();
            foreach (试题外部信息 outside in listOutside)
            {
                foreach (试题内容 content in listContent)
                {
                    if (outside.试题内容ID == content.ID)
                    {
                        outside.当前试题内容 = content;
                        break;
                    }
                }
            }

            给试题外部信息列表赋值创建人属性(listOutside);
            给试题外部信息列表赋值分类列表属性(listOutside);
            return listOutside;
        }





        public static IQueryable<试题外部信息表> 判断取题条件(取题条件 getProblemCondition)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            IQueryable<试题外部信息表> query = db.试题外部信息表.Where(a => a.试题状态Enum == 0&&a.创建人ID==用户信息.CurrentUser.用户ID);
            //按题干搜索
            if (getProblemCondition.题干关键字 != null && getProblemCondition.题干关键字 != "")
            {
                //去掉前后空格
                getProblemCondition.题干关键字 = getProblemCondition.题干关键字.TrimStart();
                getProblemCondition.题干关键字 = getProblemCondition.题干关键字.TrimEnd();
                if (getProblemCondition.题干关键字 != "")
                {
                    query = query.Where(a => a.试题查询内容.Contains(getProblemCondition.题干关键字));
                }
            }
            //题型
            if (getProblemCondition.取题题型及数量集合 != null)
            {
                if (getProblemCondition.取题题型及数量集合[0].题型Enum != 0)
                {
                    byte 题型Enum=getProblemCondition.取题题型及数量集合[0].题型Enum;
                    query = query.Where(a => a.小题型Enum == 题型Enum);
                }
            }
            //网站标签分类
            if (!string.IsNullOrEmpty(getProblemCondition.网站分类集合))
            {
                query =取分类下的试题(getProblemCondition.网站分类集合, query, db);
            }
            return query;
        }



        private static IQueryable<试题外部信息表> 取分类下的试题(string 分类集合字符串, IQueryable<试题外部信息表> 查询条件, LoveKaoExamEntities db)
        {
            IQueryable<试题外部信息表> querySort = db.试题外部信息表;
            string[] sortList = 分类集合字符串.Split(",".ToCharArray());
            List<string> listSort = 分类.分类处理(sortList.ToList());
            //分类个数为1
            if (listSort.Count == 1)
            {
                List<string> listAllSort = 分类.得到分类及子分类名称集合(listSort[0]);
                querySort = from outside in db.试题外部信息表
                            join sort in db.所属分类表 on outside.ID equals sort.相关信息ID
                            where listAllSort.Contains(sort.分类名)
                            select outside;              
                querySort = 查询条件.Intersect(querySort);
            }
            //取并集
            else if (listSort.Count > 1)
            {
                List<string> listAllSort = 分类.得到分类及子分类名称集合(listSort[0]);
                querySort = from outside in db.试题外部信息表
                            join sort in db.所属分类表 on outside.ID equals sort.相关信息ID
                            where listAllSort.Contains(sort.分类名)
                            select outside;
                for (int i = 1; i < listSort.Count; i++)
                {
                    List<string> listNextAllSort = 分类.得到分类及子分类名称集合(listSort[i]);
                    IQueryable<试题外部信息表> queryNextSort = from outside in db.试题外部信息表
                                                        join sort in db.所属分类表 on outside.ID equals sort.相关信息ID
                                                        where listNextAllSort.Contains(sort.分类名)
                                                        select outside;
                    querySort = querySort.Union(queryNextSort);
                }
                querySort = 查询条件.Intersect(querySort);
            }
            return querySort;
        }



       

        public static 试题外部信息 得到符合ID的试题(Guid 试题外部信息ID)
        {
            试题外部信息 outside = 试题外部信息查询.Where(a => a.ID == 试题外部信息ID).FirstOrDefault();           
            return outside;
        }



          
        public static List<试题外部信息> 把试题外部信息表集合转化为试题外部信息集合(List<试题外部信息表> listDBOutside)
        {
            List<试题外部信息> listOutside = new List<试题外部信息>();
            Func<试题外部信息表, 试题外部信息> func = Mapper.Create<试题外部信息表, 试题外部信息>();
            for (int i = 0; i < listDBOutside.Count; i++)
            {
                var v = func(listDBOutside[i]);
                v.ID = listDBOutside[i].ID;
                v.创建人ID = listDBOutside[i].创建人ID;
                v.试题显示内容 = listDBOutside[i].试题显示列;
                v.试题内容ID = listDBOutside[i].试题内容ID;
                v.难易度 = listDBOutside[i].难易度;
                listOutside.Add(v);
            }
            return listOutside;
        }


 

        public static List<Guid> 随机取题(byte 题型, int 取题数量, List<试题外部信息表> 按取题条件得到的集合, Guid 取题人ID)
        {           
            List<Guid> listProblem = 按取题条件得到的集合.Where(a => a.创建人ID == 取题人ID).Select(a => a.试题内容ID).ToList();
            //集合的总数量
            int num = listProblem.Count;
            //记录取出的下标值
            List<int> list = new List<int>();
            //返回的题目集合
            List<Guid> reList = new List<Guid>(取题数量);
            //题目的总数量必须大于要取的题目数量
            if (num >= 取题数量)
            {
                Random random = new Random();
                while (list.Count < 取题数量)
                {
                    //随机产生一个集合的下标
                    int iter = random.Next(0, num);
                    if (!list.Contains(iter))
                    {
                        list.Add(iter);
                        reList.Add(listProblem[iter]);
                    }
                }
            }
            //题目不足，直接返回原集合
            else
            {
                reList = listProblem;
            }

            return reList;
        }



        private static IQueryable<试题外部信息表> 按某个分类查询试题(string 分类名, LoveKaoExamEntities db)
        {           
            List<string> listSortName = 分类.得到分类及子分类名称集合(分类名);
            IQueryable<试题外部信息表> querySort = from outside in db.试题外部信息表
                                            join sort in db.所属分类表 on outside.ID equals sort.相关信息ID
                        where listSortName.Contains(sort.分类名)
                        select outside;          
            return querySort;
        }




        private static void 给试题外部信息列表赋值分类列表属性(List<试题外部信息> listOutside)
        {
            List<Guid> listOutsideId = listOutside.Select(a => a.ID).ToList();
            List<所属分类> listSort = 所属分类.所属分类查询.Where(a => listOutsideId.Contains<Guid>(a.相关ID)).ToList();
            foreach (试题外部信息 outside in listOutside)
            {
                outside.分类列表 = listSort.Where(a => a.相关ID == outside.ID).Select(a=>a.分类名).ToList();
            }
        }



        private static void 给试题外部信息列表赋值创建人属性(List<试题外部信息> listOutside)
        {
            List<Guid> listMemberId = listOutside.Select(a => a.创建人ID).ToList();
            List<用户> listMember = 用户.用户查询.Where(a => listMemberId.Contains<Guid>(a.ID)).ToList();
            foreach (试题外部信息 outside in listOutside)
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


 
        
        public static int 根据答案文本得到答案序号(string 答案文本)
        {
            string answerString = 答案文本;
            int answerOrder;
            byte[] array = new byte[1];
            array = System.Text.Encoding.ASCII.GetBytes(answerString);
            int asciicode = (int)(array[0]);
            if (asciicode > 64 && asciicode < 92)
            {
                answerOrder = asciicode - 65;
            }
            else
            {
                answerOrder = asciicode - 97;
            }
            return answerOrder;
        }



        private string 得到题型名称根据小题型Enum()
        {
            string name = string.Empty;
            switch (this.小题型Enum)
            {
                case 11:
                    {
                        name= "单选题";
                        break;
                    }
                case 12:
                    {
                        name= "多选题";
                        break;
                    }
                case 13:
                    {
                        name = "填空题";
                        break;
                    }
                case 14:
                    {
                        name = "选词填空";
                        break;
                    }
                case 15:
                    {
                        name = "完形填空";
                        break;
                    }
                case 20:
                    {
                        name = "判断题";
                        break;
                    }
                case 30:
                    {
                        name = "连线题";
                        break;
                    }
                case 40:
                    {
                        name = "复合题";
                        break;
                    }
                case 41:
                    {
                        name = "诗词鉴赏";
                        break;
                    }
                case 42:
                    {
                        name = "阅读理解";
                        break;
                    }
                case 43:
                    {
                        name = "快速阅读";
                        break;
                    }
                case 44:
                    {
                        name = "文言文";
                        break;
                    }
                case 45:
                    {
                        name = "现代文";
                        break;
                    }
                case 46:
                    {
                        name = "材料分析";
                        break;
                    }
                case 47:
                    {
                        name = "案例分析";
                        break;
                    }
                case 60:
                    {
                        name = "问答题";
                        break;
                    }
                case 61:
                    {
                        name = "简答题";
                        break;
                    }
                case 62:
                    {
                        name = "论述题";
                        break;
                    }
                case 63:
                    {
                        name = "翻译题";
                        break;
                    }
                case 64:
                    {
                        name = "计算题";
                        break;
                    }
                case 65:
                    {
                        name = "作文题";
                        break;
                    }
                case 66:
                    {
                        name = "案例题";
                        break;
                    }
                case 67:
                    {
                        name = "材料题";
                        break;
                    }
                case 68:
                    {
                        name = "推理题";
                        break;
                    }
                case 69:
                    {
                        name = "名词解释";
                        break;
                    }
                case 80:
                    {
                        name = "多题干共选项题";
                        break;
                    }
            }
            return name;
        }
   

        #endregion
    }

}
