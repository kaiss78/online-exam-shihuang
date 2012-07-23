using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using ExpressionMapper;

namespace LoveKaoExam.Data
{
    public class 试卷内容
    {
        #region 变量
        private 试卷外部信息 _试卷外部信息;
        private List<试卷中大题> _试卷中大题集合;
        private int? _总分;
        private 用户 _提交人;
        private decimal? _客观题总分;
        #endregion


        #region 属性

        public Guid ID
        {
            get;
            set;
        }


        public Guid 试卷外部信息ID
        {
            get;
            set;
        }


        [JsonIgnore]
        public 试卷外部信息 试卷外部信息
        {
            get
            {
                if (_试卷外部信息 == null)
                {
                    _试卷外部信息 = 试卷外部信息.试卷外部信息查询.Where(a => a.ID == this.试卷外部信息ID).First();
                    return _试卷外部信息;
                }
                return _试卷外部信息;
            }
            set
            {
                _试卷外部信息 = value;
            }
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

       


        public Guid 提交人ID
        {
            get;
            set;
        }

 
        [JsonIgnore]
        public 用户 提交人
        {
            get
            {
                if (_提交人 == null)
                {
                    _提交人 = 用户.用户查询.Where(a => a.ID == this.提交人ID).First();
                    return _提交人;
                }
                return _提交人;
            }
            set
            {
                _提交人 = value;
            }
        }


        [JsonIgnore]
        public DateTime 提交时间
        {
            get;
            set;
        }

       
        public string 提交备注
        {
            get;
            set;
        }


        public decimal 客观题总分
        {
            get
            {
                if (_客观题总分 == null)
                {
                    LoveKaoExamEntities db = new LoveKaoExamEntities();
                    List<试卷大题中试题表> listTestProblem = db.试卷大题中试题表
                       .Where(a => a.试卷中大题表.试卷内容ID == this.ID).ToList();
                    List<Guid> listContentId = listTestProblem.Select(a => a.试题内容ID).ToList();
                    List<Guid> listObjectiveContentId = db.试题内容表.Where(a => listContentId.Contains(a.ID)
                        && (a.小题型Enum > 10 && a.小题型Enum < 31 || a.小题型Enum == 8011)).Select(a => a.ID).ToList();
                    decimal objectiveScore = listTestProblem.Where(a => listObjectiveContentId.Contains(a.试题内容ID))
                        .Sum(a => a.每小题分值);
                    string score = objectiveScore.ToString("G0");
                    _客观题总分 = Convert.ToDecimal(score);
                    return _客观题总分.Value;
                }
                else
                {
                    return _客观题总分.Value;
                }
            }
            set
            {
                _客观题总分 = value;
            }
        }


        public decimal 主观题总分
        {
            get
            {
                return 总分 - 客观题总分;
            }
        }


        public int 总分
        {
            get
            {
                if (_总分 == null)
                {
                    LoveKaoExamEntities db = new LoveKaoExamEntities();
                    List<试卷大题中试题表> listTestProblem=db.试卷大题中试题表
                        .Where(a => a.试卷中大题表.试卷内容ID== this.ID).ToList();                  
                    List<Guid> listProblemId = listTestProblem.Select(a => a.试题内容ID).ToList();
                    List<Guid> listComplexId = 试题内容.试题内容查询.Where(a => listProblemId.Contains<Guid>(a.ID)
                        && (a.小题型Enum > 39 && a.小题型Enum < 50 || a.小题型Enum == 80)).Select(a => a.ID).ToList();
                    List<试卷大题中试题表> listComplexTestProblem = listTestProblem
                        .Where(a => listComplexId.Contains(a.试题内容ID)).ToList();
                    listTestProblem = listTestProblem.Except(listComplexTestProblem).ToList();
                    decimal totalScore = listTestProblem.Sum(a => a.每小题分值);
                    _总分 = Convert.ToInt32(totalScore);
                    return _总分.Value;
                }
                return _总分.Value;
            }
            set
            {
                _总分 = value;
            }
        }


        public List<试卷中大题> 试卷中大题集合
        {
            get
            {
                if (_试卷中大题集合 == null)
                {
                    _试卷中大题集合 = 试卷中大题.试卷中大题查询.Where(a => a.试卷内容ID == this.ID).OrderBy(a => a.顺序).ToList();
                    return _试卷中大题集合;
                }
                return _试卷中大题集合;
            }
            set
            {
                _试卷中大题集合 = value;
            }
        }



        public static IQueryable<试卷内容> 试卷内容查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试卷内容表.Select(a => new 试卷内容
                    {
                        ID = a.ID,
                        名称 = a.名称,
                        试卷外部信息ID = a.试卷外部信息ID,
                        说明 = a.说明,
                        提交备注 = a.提交备注,
                        提交人ID = a.提交人ID,
                        提交时间 = a.提交时间
                    });
            }
        }



        [JsonIgnore]
        public Dictionary<string, byte[]> 试题图片
        {
            get;
            set;
        }

        #endregion



        #region 方法
       
        public string 转化成试卷内容Json字符串()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            json = "{\"content\":" + json + ",\"outside\":null}";
            return json;
        }


        public string 转化成Json字符串()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            return json;
        }


        public static string 转化成完整Json字符串试题内容带答案(试卷内容 试卷内容, 试卷外部信息 试卷外部信息)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            试卷内容 content = 给试卷内容中试题内容Json赋值(试卷内容, true);
            替换Json的扛(content);
            string 试卷内容Json字符串 = content.转化成Json字符串();
            string 试卷外部信息Json字符串 = 试卷外部信息.转化成Json字符串();
            string json = "{\"content\":" + 试卷内容Json字符串 + ",\"outside\":" + 试卷外部信息Json字符串 + "}";
            json = 还原Json的扛(json);
            json = json.Replace("\"试题内容Json\": \"", "\"试题内容Json\":");
            json = json.Replace("\\r\\n", "");       
            json = json.Replace("\"outside\":null}\"", "\"outside\":null}");
            json = json.Replace("\"00000000-0000-0000-0000-000000000000\"", "null");
            return json;
        }



        public static string 转化成完整Json字符串试题内容不带答案(试卷内容 试卷内容, 试卷外部信息 试卷外部信息)
        {

            试卷内容 = 去掉试卷中的试题的答案(试卷内容);
            替换Json的扛(试卷内容);
            string 试卷内容Json字符串 = 试卷内容.转化成Json字符串();
            string 试卷外部信息Json字符串 = 试卷外部信息.转化成Json字符串();
            string json = "{\"content\":" + 试卷内容Json字符串 + ",\"outside\":" + 试卷外部信息Json字符串 + "}";
            json = 还原Json的扛(json);
            json = json.Replace("\"试题内容Json\": \"", "\"试题内容Json\":");
            json = json.Replace("\\r\\n", "");
            json = json.Replace("\\n", "");
            json = json.Replace("\\", "");
            json = json.Replace("\"outside\":null}\"", "\"outside\":null}");
            json = json.Replace("\"00000000-0000-0000-0000-000000000000\"", "null");
            return json;
        }



        public void 保存(LoveKaoExamEntities db, Guid 试卷内容ID)
        {
            试卷内容表 testContent = new 试卷内容表();
            testContent.ID = 试卷内容ID;
            testContent.名称 = this.名称;
            testContent.试卷外部信息ID = this.试卷外部信息ID;
            testContent.说明 = this.说明;
            testContent.提交备注 = this.提交备注;
            testContent.提交人ID = this.提交人ID;
            testContent.提交时间 = DateTime.Now;

            for (int i = 0; i < this.试卷中大题集合.Count; i++)
            {
                试卷中大题表 type = new 试卷中大题表();
                type.ID = Guid.NewGuid();
                type.多选题评分策略 = this.试卷中大题集合[i].多选题评分策略;
                type.名称 = this.试卷中大题集合[i].名称;
                type.顺序 = Convert.ToByte(i);
                type.说明 = this.试卷中大题集合[i].说明;

                for (int j = 0; j < this.试卷中大题集合[i].试卷大题中试题集合.Count; j++)
                {
                    if (this.试卷中大题集合[i].试卷大题中试题集合[j].子小题集合 != null)
                    {
                        foreach (试卷大题中试题 testProblem in this.试卷中大题集合[i].试卷大题中试题集合[j].子小题集合)
                        {
                            试卷大题中试题表 dbTestProblem = new 试卷大题中试题表();
                            dbTestProblem.ID = Guid.NewGuid();
                            dbTestProblem.每小题分值 = testProblem.每小题分值;
                            dbTestProblem.试题内容ID = testProblem.试题内容ID;
                            type.试卷大题中试题表.Add(dbTestProblem);
                        }
                    }

                    试卷大题中试题表 problem = new 试卷大题中试题表();
                    problem.ID = Guid.NewGuid();
                    problem.每小题分值 = this.试卷中大题集合[i].试卷大题中试题集合[j].每小题分值;
                    problem.试题内容ID = this.试卷中大题集合[i].试卷大题中试题集合[j].试题内容ID;
                    problem.顺序 = Convert.ToByte(j);
                    type.试卷大题中试题表.Add(problem);

                }
                testContent.试卷中大题表.Add(type);
            }
            db.试卷内容表.AddObject(testContent);
            db.SaveChanges();
        }



        /// <summary>
        /// 返回试卷中所有试题外部信息ID，插入试卷引用的试题表有用，全部保存成功才插入
        /// </summary>     
        public List<Guid> 保存下载试卷(LoveKaoExamEntities db, List<string> listBelongSort)
        {
            //全部试题的外部信息ID
            List<Guid> listProblemOutsideId = new List<Guid>();
            //新题的外部信息ID集合
            List<Guid> listNewProblemOutsideId = new List<Guid>();
            //把试卷中所有的试题内容ID,试题外部信息ID取出来
            List<Guid> listProblemContentId = new List<Guid>();
            foreach (试卷中大题 testType in this.试卷中大题集合)
            {
                foreach (试卷大题中试题 testProblem in testType.试卷大题中试题集合)
                {
                    listProblemContentId.Add(testProblem.试题内容ID);
                    JObject bo = JObject.Parse(testProblem.试题内容Json);
                    string problemOutsideIdStr = bo["content"]["试题外部信息ID"].ToString();
                    problemOutsideIdStr = problemOutsideIdStr.Replace("\"", "");
                    Guid problemOutsideId = new Guid(problemOutsideIdStr);
                    listProblemOutsideId.Add(problemOutsideId);
                    string subJsonStr = string.Empty;
                    //若是复合题，则把子小题的Json加入到复合题的Json
                    if (testProblem.子小题集合 != null)
                    {                      
                        foreach (试卷大题中试题 subTestProblem in testProblem.子小题集合)
                        {
                            JObject subBo = JObject.Parse(subTestProblem.试题内容Json);
                            string subJson = subBo["content"].ToString();
                            subJsonStr += subJson+",";
                        }
                        if (subJsonStr.Length > 0)
                        {
                            subJsonStr = subJsonStr.Remove(subJsonStr.Length - 1);
                        }
                    }
                    subJsonStr = "\"子小题集合\":[" + subJsonStr + "]";
                    testProblem.试题内容Json = testProblem.试题内容Json
                        .Replace("" + testProblem.试题内容ID + "\",", "" + testProblem.试题内容ID + "\","+subJsonStr+",");
                }
            }
            //查询已存在的试题，已存在的则不必插入了   
            List<试题内容表> listExistProblemContent = db.试题内容表.Where(a => listProblemContentId.Contains(a.爱考网ID)
                && a.操作人ID == 用户信息.CurrentUser.用户ID).ToList();
            List<Guid> listExistProblemContentLoveKaoId = listExistProblemContent.Select(a => a.爱考网ID).ToList();
            List<Guid> listNotExistProblemContentId = listProblemContentId.Except(listExistProblemContentLoveKaoId).ToList();
             //查询已存在的试题外部信息ID，若试题外部信息存在，而试题内容不存在，则只需添加试题内容即可
            List<Guid> listExistProblemOutsideId = db.试题外部信息表.Where(a => listProblemOutsideId.Contains(a.爱考网ID)
                &&a.创建人ID==用户信息.CurrentUser.用户ID).Select(a => a.爱考网ID).ToList();
            试卷内容表 testContent = new 试卷内容表();
            testContent.ID = this.ID;
            testContent.名称 = this.名称;
            testContent.试卷外部信息ID = this.试卷外部信息ID;
            testContent.说明 = "";
            testContent.提交备注 = "";
            testContent.提交人ID = 用户信息.CurrentUser.用户ID;
            testContent.提交时间 = DateTime.Now;
            //随机一个秒数，使创建时间不在同一秒
            Random random = new Random();
            DateTime time = DateTime.Now;
            //记录随机的秒数，以避免重复
            List<int> listSeconds = new List<int>();
            for (int i = 0; i < this.试卷中大题集合.Count; i++)
            {             
                试卷中大题表 type = new 试卷中大题表();
                type.ID = Guid.NewGuid();
                type.多选题评分策略 = 0;
                type.名称 = this.试卷中大题集合[i].名称;
                type.顺序 = Convert.ToByte(i);
                type.说明 = "";
                for (int j = 0; j < this.试卷中大题集合[i].试卷大题中试题集合.Count; j++)
                {
                    int seconds = random.Next(0, 300);
                    if (listSeconds.Contains(seconds))
                    {
                        seconds = random.Next(0, 300);
                    }
                    listSeconds.Add(seconds);
                    试题外部信息 outside = new 试题外部信息();
                    outside.创建时间=time.AddSeconds(seconds);
                    outside.最新更新时间=time.AddSeconds(seconds);

                    试卷大题中试题表 problem = new 试卷大题中试题表();
                    problem.ID = Guid.NewGuid();
                    problem.每小题分值 = this.试卷中大题集合[i].试卷大题中试题集合[j].每小题分值;

                    string 试题Json = this.试卷中大题集合[i].试卷大题中试题集合[j].试题内容Json;
                    Guid contentId = this.试卷中大题集合[i].试卷大题中试题集合[j].试题内容ID;
                    JObject bo = JObject.Parse(试题Json);
                    string problemOutsideIdStr = bo["content"]["试题外部信息ID"].ToString();
                    problemOutsideIdStr = problemOutsideIdStr.Replace("\"", "");
                    Guid problemOutsideId = new Guid(problemOutsideIdStr);
                    //若是不存在的新题，插入新题
                    if (listNotExistProblemContentId.Contains(contentId))
                    {
                        Dictionary<Guid,Guid> dic=new Dictionary<Guid,Guid>();
                        listNewProblemOutsideId.Add(problemOutsideId);
                        试题Json = 试题外部信息.替换试题图片路径(试题Json,用户信息.CurrentUser.用户名);
                        if (listExistProblemOutsideId.Contains(problemOutsideId))
                        {
                            outside.爱考网ID = problemOutsideId;
                            dic=试题外部信息.保存下载试卷中试题(outside, 试题Json, listBelongSort, true, db);
                            contentId = dic.Where(a => a.Key == contentId).First().Value;
                        }
                        else
                        {
                            dic =试题外部信息.保存下载试卷中试题(outside, 试题Json, listBelongSort, false, db);
                            contentId = dic.Where(a => a.Key == contentId).First().Value;
                        }
                        if (this.试卷中大题集合[i].试卷大题中试题集合[j].子小题集合 != null)
                        {
                            for (int m = 0; m < this.试卷中大题集合[i].试卷大题中试题集合[j].子小题集合.Count; m++)
                            {
                                Guid subContentId = dic.Where(a => a.Key == this.试卷中大题集合[i].试卷大题中试题集合[j]
                                    .子小题集合[m].试题内容ID).First().Value;
                                试卷大题中试题表 subProblem = new 试卷大题中试题表();
                                subProblem.ID = Guid.NewGuid();
                                subProblem.每小题分值 = this.试卷中大题集合[i].试卷大题中试题集合[j].子小题集合[m].每小题分值;
                                subProblem.试题内容ID = subContentId;
                                subProblem.顺序 = Convert.ToByte(m);
                                type.试卷大题中试题表.Add(subProblem);
                            }
                        }

                        listNotExistProblemContentId.Remove(contentId);
                    }
                    else
                    {
                        contentId = listExistProblemContent.First(a => a.爱考网ID == contentId).ID;
                    }
                    problem.试题内容ID = contentId;
                    problem.顺序 = Convert.ToByte(j);
                    type.试卷大题中试题表.Add(problem);
                   
                }
                testContent.试卷中大题表.Add(type);
            }
            db.试卷内容表.AddObject(testContent);
            db.SaveChanges();
            return listProblemOutsideId;
        }




        public static 试卷内容 把Json转化成试卷内容(string 试卷Json字符串)
        {          
            JObject bo = JObject.Parse(试卷Json字符串);
            string content = bo["content"].ToString();
            content = content.Replace("'", "\"");
            content = content.Replace("\"试题内容Json\":", "\"试题内容Json\":'");
            //复合题中的空格数不一样，所以替换2次
            content = content.Replace("\"outside\": null\r\n          }", "\"outside\": null\r\n          }'");
            content = content.Replace("\"outside\": null\r\n              }", "\"outside\": null\r\n              }'");
            content = content.Replace("\"试题内容Json\":' null", "\"试题内容Json\":'null'");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            试卷内容 testContent = jss.Deserialize<试卷内容>(content);
            return testContent;
        }





        public static 试卷内容 根据试卷内容ID得到某份试卷(Guid 试卷内容ID)
        {
            试卷内容 content = 试卷内容.试卷内容查询.Where(a => a.ID == 试卷内容ID).FirstOrDefault();          
            return content;
        }


        public static 试卷内容 得到试卷内容带试题内容Json(Guid 试卷内容ID, Guid 会员ID)
        {
            试卷内容 content =试卷内容.试卷内容查询.Where(a => a.ID == 试卷内容ID).FirstOrDefault();          
            content = 给试卷内容中试题内容Json赋值(content, false);
            return content;
        }


        public static 试卷内容 给试卷内容中试题内容Json赋值(试卷内容 试卷内容, bool 是否加null的连线题回答)
        {
            List<Guid> listProblemId = new List<Guid>();
            foreach (试卷中大题 type in 试卷内容.试卷中大题集合)
            {
                foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                {
                    listProblemId.Add(testProblem.试题内容ID);
                    if (testProblem.子小题集合 != null)
                    {
                        foreach (试卷大题中试题 subTestProblem in testProblem.子小题集合)
                        {
                            listProblemId.Add(subTestProblem.试题内容ID);
                        }
                    }
                }
            }
            List<试题内容> listProblem = 试题内容.试题内容查询.Where(a => listProblemId.Contains<Guid>(a.ID)).ToList();
            试卷内容.试题图片 = new Dictionary<string, byte[]>();
            foreach (试题内容 content in listProblem)
            {
                Dictionary<string, byte[]> oneDic = 试题外部信息.获取试题图片(content.Json字符串);
                foreach (var subOneDic in oneDic)
                {
                    试卷内容.试题图片.Add(subOneDic.Key, subOneDic.Value);
                }
                content.Json字符串 = "{\"content\":" + content.Json字符串 + ",\"outside\":null}";
            }
            //给试卷大题中试题的试题内容属性赋值
            foreach (试卷中大题 type in 试卷内容.试卷中大题集合)
            {
                foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                {
                    foreach (试题内容 content in listProblem)
                    {
                        if (testProblem.试题内容ID == content.ID)
                        {
                            //加一个null的回答，界面有需求
                            if (content.小题型Enum == 30 && 是否加null的连线题回答 == true)
                            {
                                考生连线题回答 memberLinkAnswer = new 考生连线题回答();
                                memberLinkAnswer.连线题回答集合 = new List<连线题回答>();
                                连线题回答 linkAnswer = new 连线题回答();
                                memberLinkAnswer.连线题回答集合.Add(linkAnswer);
                                testProblem.该题考试回答 = memberLinkAnswer;
                            }
                            testProblem.试题内容Json = content.Json字符串;
                            break;
                        }
                        if (testProblem .子小题集合!= null)
                        {
                            试卷大题中试题 subTestProblem = testProblem.子小题集合.FirstOrDefault(a => a.试题内容ID == content.ID);
                            if (subTestProblem != null)
                            {
                                subTestProblem.试题内容Json = content.Json字符串;
                            }
                        }
                    }
                }
            }
            //回答的题中是复合题和多题干共选项题的小题集合
            int hh = 0;
            var listSubContent = listProblem.Where(a => a.父试题内容ID != null).OrderBy(a => a.本题在复合题中顺序)
                .GroupBy(a => a.父试题内容ID).ToList();
            for (int n = 0; n < listSubContent.Count; n++)
            {
                var aa = listSubContent[n].OrderBy(a => a.本题在复合题中顺序).ToList();
                List<试卷大题中试题> listSubTestProblem = new List<试卷大题中试题>();
                foreach (试题内容 content in aa)
                {
                    //找到对应的试卷大题中试题
                    foreach (试卷中大题 type in 试卷内容.试卷中大题集合)
                    {
                        试卷大题中试题 testProblem = type.试卷大题中试题集合.Where(a => a.试题内容ID == content.ID).FirstOrDefault();                       
                        if (testProblem != null)
                        {
                            listSubTestProblem.Add(testProblem);
                            //去掉子小题
                            type.试卷大题中试题集合.Remove(testProblem);
                            break;
                        }
                        foreach (试卷大题中试题 TestProblem in type.试卷大题中试题集合)
                        {
                            if (TestProblem.子小题集合 != null)
                            {
                                试卷大题中试题 subTestProblem = TestProblem.子小题集合.Where(a => a.试题内容ID == content.ID).FirstOrDefault();
                                if (subTestProblem != null)
                                {
                                    listSubTestProblem.Add(subTestProblem);
                                    break;
                                }
                            }
                        }
                    }
                }
                foreach (试卷中大题 type in 试卷内容.试卷中大题集合)
                {
                    foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                    {
                        if (testProblem.试题内容ID == listSubContent[n].Key)
                        {
                            testProblem.子小题集合 = listSubTestProblem;
                            hh = 1;
                            break;
                        }
                    }
                    if (hh == 1)
                    {
                        hh = 0;
                        break;
                    }
                }
            }
            return 试卷内容;
        }


        public static void 替换Json的扛(试卷内容 testContent)
        {
            foreach (试卷中大题 type in testContent.试卷中大题集合)
            {
                foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                {
                    if (testProblem.子小题集合 != null)
                    {
                        foreach (试卷大题中试题 subTestProblem in testProblem.子小题集合)
                        {
                            subTestProblem.试题内容Json = subTestProblem.试题内容Json.Replace("\"", "@2#3$4%5");
                            subTestProblem.试题内容Json = subTestProblem.试题内容Json.Replace("\\", "*8&7%5##");
                        }
                    }
                    testProblem.试题内容Json = testProblem.试题内容Json.Replace("\"", "@2#3$4%5");
                    testProblem.试题内容Json = testProblem.试题内容Json.Replace("\\", "*8&7%5##");
                }
            }
        }


        public static string 还原Json的扛(string json)
        {
            json = json.Replace("@2#3$4%5", "\"");
            json = json.Replace("*8&7%5##", "\\");
            return json;
        }


        public static 试卷内容 去掉试卷中的试题的答案(试卷内容 试卷内容)
        {
            List<Guid> listProblemId = new List<Guid>();
            foreach (试卷中大题 type in 试卷内容.试卷中大题集合)
            {
                foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                {
                    listProblemId.Add(testProblem.试题内容ID);
                }
            }
            List<试题内容> listProblem = 试题内容.试题内容查询.Where(a => listProblemId.Contains<Guid>(a.ID)).ToList();
            foreach (试题内容 content in listProblem)
            {
                content.Json字符串 = 试题内容.去掉答案(content.Json字符串);
                content.Json字符串 = "{\"content\":" + content.Json字符串 + ",\"outside\":null}";
            }
            //给试卷大题中试题的试题内容属性赋值
            foreach (试卷中大题 type in 试卷内容.试卷中大题集合)
            {
                foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                {
                    foreach (试题内容 content in listProblem)
                    {
                        if (testProblem.试题内容ID == content.ID)
                        {
                            //加一个null的回答，界面有需求
                            if (content.小题型Enum == 30)
                            {
                                考生连线题回答 memberLinkAnswer = new 考生连线题回答();
                                memberLinkAnswer.连线题回答集合 = new List<连线题回答>();
                                连线题回答 linkAnswer = new 连线题回答();
                                memberLinkAnswer.连线题回答集合.Add(linkAnswer);
                                testProblem.该题考试回答 = memberLinkAnswer;
                            }
                            testProblem.试题内容Json = content.Json字符串;
                            break;
                        }
                    }
                }
            }
            //回答的题中是复合题和多题干共选项题的小题集合
            int hh = 0;
            var listSubContent = listProblem.Where(a => a.父试题内容ID != null).GroupBy(a => a.父试题内容ID).ToList();
            for (int n = 0; n < listSubContent.Count; n++)
            {
                var aa = listSubContent[n].OrderBy(a => a.本题在复合题中顺序).ToList();
                List<试卷大题中试题> listSubTestProblem = new List<试卷大题中试题>();
                foreach (试题内容 content in aa)
                {
                    //找到对应的试卷大题中试题
                    foreach (试卷中大题 type in 试卷内容.试卷中大题集合)
                    {
                        试卷大题中试题 testProblem = type.试卷大题中试题集合.Where(a => a.试题内容ID == content.ID).FirstOrDefault();
                        if (testProblem != null)
                        {
                            listSubTestProblem.Add(testProblem);
                            //去掉子小题
                            type.试卷大题中试题集合.Remove(testProblem);
                            break;
                        }
                    }
                }
                foreach (试卷中大题 type in 试卷内容.试卷中大题集合)
                {
                    foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                    {
                        if (testProblem.试题内容ID == listSubContent[n].Key)
                        {
                            testProblem.子小题集合 = listSubTestProblem;
                            hh = 1;
                            break;
                        }
                    }
                    if (hh == 1)
                    {
                        hh = 0;
                        break;
                    }
                }
            }
            return 试卷内容;
        }



        public static 试卷内容 把试卷内容表转化成试卷内容(试卷内容表 试卷内容表)
        {         
            试卷内容 试卷内容 = Mapper.Create<试卷内容表, 试卷内容>()(试卷内容表);
            试卷内容.ID = 试卷内容表.ID;
            试卷内容.试卷外部信息ID = 试卷内容表.试卷外部信息ID;
            试卷内容.提交人ID = 试卷内容表.提交人ID;
            return 试卷内容;
        }

        #endregion
    }
}
