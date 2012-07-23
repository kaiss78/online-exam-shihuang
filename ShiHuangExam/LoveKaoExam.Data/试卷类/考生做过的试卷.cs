using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace LoveKaoExam.Data
{
    class 考生做过的试卷不带试卷内容 : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            考生做过的试卷 memberDoneTest = (考生做过的试卷)value;
            JObject o = new JObject();
            o["ID"] = new JValue(memberDoneTest.ID.ToString());
            o["考生ID"] = new JValue(memberDoneTest.考生ID.ToString());
            o["相关ID"] = new JValue(memberDoneTest.相关ID.ToString());
            o["答题开始时间"] = new JValue(memberDoneTest.答题开始时间.ToString());
            o["答题结束时间"] = new JValue(memberDoneTest.答题结束时间.ToString());
            o["客观题总得分"] = new JValue(memberDoneTest.客观题总得分);
            o["主观题总得分"] = new JValue(memberDoneTest.主观题总得分);
            o["是否是已提交的试卷"] = new JValue(memberDoneTest.是否是已提交的试卷);
            o["批改状态"] = new JValue(memberDoneTest.批改状态);
            o["批改状态类型"] = new JValue(memberDoneTest.批改状态类型);
            o.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        {
            //convert back json string into array of picture 
            return null; 
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(考生做过的试卷);
        }
    }


    public class 考生做过的试卷
    {

        #region 变量

        private 考试设置 _考试设置;
        private 练习设置 _练习设置;
        private 试卷外部信息 _试卷外部信息;
        private 考生 _考生;
        private string _考试用时;
        private DateTime? _答题结束时间;
        private decimal _客观题得分;
        private decimal _主观题总得分;
        private Guid _设置人ID;

        #endregion


        #region 属性

        [JsonIgnore]
        public int 班级名次
        {
            get;
            set;
        }


        [JsonIgnore]
        public int 总名次
        {
            get;
            set;
        }


        public Guid 考生ID
        {
            get;
            set;
        }


        public Guid ID
        {
            get;
            set;
        }



        [JsonIgnore]
        public 考生 考生
        {
            get
            {
                if (_考生 == null)
                {
                    _考生 = 考生.考生查询.Where(a => a.ID == this.考生ID).First();
                    return _考生;
                }
                return _考生;
            }
            set
            {
                _考生 = value;
            }
        }


        /// <summary>
        /// 0代表练习，1代表考试
        /// </summary>
        public byte 类型
        {
            get;
            set;
        }


        /// <summary>
        /// 类型为0时是试卷内容ID，为1时是考试设置ID
        /// </summary>
        public Guid 相关ID
        {
            get;
            set;
        }


       
        /// <summary>
        /// 0未批改，1批改未完成，2批改完成
        /// </summary>
        public byte 批改状态类型
        {
            get;
            set;
        }


        public string 批改状态
        {
            get
            {
                if (批改状态类型 == 0)
                {
                    return "未批改";
                }
                else if (批改状态类型 == 1)
                {
                    return "批改未完成";
                }
                else
                {
                    return "批改完毕";
                }
            }
        }


        public DateTime? 批改时间
        {
            get;
            set;
        }


        public DateTime 答题开始时间
        {
            get;
            set;
        }


        public DateTime? 答题结束时间
        {
            get;
            set;
        }


        public string 考试用时
        {
            get
            {
                if (_考试用时 == null)
                {
                    if (答题结束时间 == null)
                    {
                        return "";
                    }
                    else
                    {
                        int hour = (this.答题结束时间.Value - this.答题开始时间).Hours;
                        int mimute = (this.答题结束时间.Value - this.答题开始时间).Minutes;
                        int seconds = (this.答题结束时间.Value - this.答题开始时间).Seconds;
                        _考试用时 = "" + hour + "时" + mimute + "分" + seconds + "秒";
                        return _考试用时;
                    }
                }
                else
                {
                    return _考试用时;
                }
            }
            set
            {
                _考试用时 = value;
            }
        }


        public decimal 客观题总得分
        {
            get
            {
                string score = _客观题得分.ToString("G0");
                decimal totalScore = Convert.ToDecimal(score);
                return totalScore;
            }
            set
            {
                _客观题得分 = value;
            }
        }


        public decimal 主观题总得分
        {
            get
            {
                string score = _主观题总得分.ToString("G0");
                decimal totalScore = Convert.ToDecimal(score);
                return totalScore;
            }
            set
            {
                _主观题总得分 = value;
            }
        }


        public decimal 总得分
        {
            get
            {
                string score = (客观题总得分 + 主观题总得分).ToString("G0");
                decimal totalScore = Convert.ToDecimal(score);
                return totalScore;
            }                    
        }


        public string 及格情况
        {
            get;
            set;
        }


        public bool 是否是已提交的试卷
        {
            get;
            set;
        }


        public bool 是否公布考试结果
        {
            get;
            set;
        }


        public bool 考试是否结束
        {
            get;
            set;
        }


        public Guid 设置人ID
        {
            get
            {
                if (this.类型 == 1)
                {
                    _设置人ID = this.考试设置.设置人ID;
                    return _设置人ID;
                }
                else
                {
                    return _设置人ID;
                }
            }
            set
            {
                _设置人ID = value;
            }
        }



        public 考试设置 考试设置
        {
            get
            {
                if (_考试设置 == null && this.类型 == 1)
                {
                    _考试设置 = 考试设置.考试设置查询.Where(a => a.ID == this.相关ID).First();
                    return _考试设置;
                }
                else
                {
                    return _考试设置;
                }
            }
            set
            {
                _考试设置 = value;
            }
        }


        public 练习设置 练习设置
        {
            get
            {
                if (_练习设置 == null && this.类型 == 0)
                {
                    _练习设置 = 练习设置.练习设置查询.Where(a => a.试卷内容ID == this.相关ID).First();
                    return _练习设置;
                }
                else
                {
                    return _练习设置;
                }
            }
            set
            {
                _练习设置 = value;
            }
        }


        [JsonIgnore]
        public List<考生考试回答> 考生考试回答集合
        {
            get;
            set;
        }


        public static IQueryable<考生做过的试卷> 考生做过的试卷查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.考生做过的试卷表.Select(a => new 考生做过的试卷
                    {
                        ID = a.ID,
                        答题结束时间 = a.答题结束时间,
                        答题开始时间 = a.答题开始时间,
                        考生ID = a.考生ID,
                        相关ID = a.相关ID,
                        客观题总得分 = a.客观题得分,
                        是否是已提交的试卷 = a.是否是已提交的试卷,
                        类型=a.类型,
                        批改状态类型=a.批改状态,
                        主观题总得分=a.主观题得分,
                        批改时间=a.批改时间
                    });
            }
        }


        [JsonIgnore]
        public 用户表 考生表
        {
            get;
            set;
        }


        [JsonIgnore]
        public 考试设置表 考试设置表
        {
            get;
            set;
        }


        [JsonIgnore]
        public 练习设置表 练习设置表
        {
            get;
            set;
        }


        public static IQueryable<考生做过的试卷> 考生做过的试卷联合查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from memberDoneTest in db.考生做过的试卷表
                       join user in db.用户表
                       on memberDoneTest.考生ID equals user.ID
                       join examSet in db.考试设置表
                       on memberDoneTest.相关ID equals examSet.ID
                       select new 考生做过的试卷
                       {
                           ID = memberDoneTest.ID,
                           答题结束时间 = memberDoneTest.答题结束时间,
                           答题开始时间 = memberDoneTest.答题开始时间,
                           考生ID = memberDoneTest.考生ID,
                           考生表 = user,
                           客观题总得分 = memberDoneTest.客观题得分,
                           类型 = memberDoneTest.类型,
                           批改状态类型 = memberDoneTest.批改状态,
                           是否是已提交的试卷 = memberDoneTest.是否是已提交的试卷,
                           相关ID = memberDoneTest.相关ID,
                           主观题总得分 = memberDoneTest.主观题得分,
                           考试设置表 = examSet,
                           批改时间 = memberDoneTest.批改时间
                       };
            }
        }


        public static IQueryable<考生做过的试卷> 考生做过的试卷联合考试设置查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from memberDoneTest in db.考生做过的试卷表
                       join examSet in db.考试设置表
                       on memberDoneTest.相关ID equals examSet.ID
                       select new 考生做过的试卷
                       {
                           ID = memberDoneTest.ID,
                           答题结束时间 = memberDoneTest.答题结束时间,
                           答题开始时间 = memberDoneTest.答题开始时间,
                           考生ID = memberDoneTest.考生ID,
                           客观题总得分 = memberDoneTest.客观题得分,
                           类型 = memberDoneTest.类型,
                           批改状态类型 = memberDoneTest.批改状态,
                           是否是已提交的试卷 = memberDoneTest.是否是已提交的试卷,
                           相关ID = memberDoneTest.相关ID,
                           主观题总得分 = memberDoneTest.主观题得分,
                           考试设置表 = examSet,
                           批改时间 = memberDoneTest.批改时间
                       };
            }
        }


        public static IQueryable<考生做过的试卷> 考生做过的试卷联合练习设置查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from memberDoneTest in db.考生做过的试卷表
                       join exerciseSet in db.练习设置表
                       on memberDoneTest.相关ID equals exerciseSet.试卷内容ID
                       select new 考生做过的试卷
                       {
                           ID = memberDoneTest.ID,
                           答题结束时间 = memberDoneTest.答题结束时间,
                           答题开始时间 = memberDoneTest.答题开始时间,
                           考生ID = memberDoneTest.考生ID,
                           客观题总得分 = memberDoneTest.客观题得分,
                           类型 = memberDoneTest.类型,
                           批改状态类型 = memberDoneTest.批改状态,
                           是否是已提交的试卷 = memberDoneTest.是否是已提交的试卷,
                           相关ID = memberDoneTest.相关ID,
                           主观题总得分 = memberDoneTest.主观题得分,
                           练习设置表 = exerciseSet,
                           批改时间 = memberDoneTest.批改时间
                       };
            }
        }

        #endregion


        #region 方法
      
        public string 转化成Json带试卷内容()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            试卷内容 testContent;
            if (this.类型 == 0)
            {
                this.是否公布考试结果 = true;
                testContent = 试卷内容.给试卷内容中试题内容Json赋值(this.练习设置.试卷内容, false);
            }
            else
            {
                testContent = 试卷内容.给试卷内容中试题内容Json赋值(this.考试设置.试卷内容, false);
            }
            试卷内容.替换Json的扛(testContent);
            if (this.类型 == 0)
            {
                this.练习设置.试卷内容 = testContent;
            }
            else
            {
                this.考试设置.试卷内容 = testContent;
            }
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            json = 试卷内容.还原Json的扛(json);
            json = json.Replace("\"试题内容Json\": \"", "\"试题内容Json\":");
            json = json.Replace("\\r\\n", "");
            json = json.Replace("\"outside\":null}\"", "\"outside\":null}");
            json = json.Replace("\"00000000-0000-0000-0000-000000000000\"", "null");
            json = json.Replace("\"考生ID\": \"" + this.考生ID + "\"", "\"公共信息\":{\"考生ID\": \"" + this.考生ID + "\"");
            json = json.Replace(",\r\n  \"考试设置\"", "},\r\n  \"考试设置\"");
            //格式化Json          
            JObject bo = JObject.Parse(json);
            if (this.类型 == 0)
            {
                json = json.Replace("试卷内容\": {", "试卷内容\": {\"考试时长\":" + this.练习设置.考试时长 + ",");
            }
            else
            {
                json = json.Replace("试卷内容\": {", "试卷内容\": {\"考试时长\":" + this.考试设置.考试时长 + ",");               
            }
            JObject bo1 = JObject.Parse(json);
            string newJson = "{\"公共信息\":";
            string common = bo1["公共信息"].ToString();
            newJson = newJson + common + ",\"试卷内容\":";
            string test = string.Empty;
            if (类型 == 0)
            {
                test = bo1["练习设置"]["试卷内容"].ToString();
            }
            else
            {
                test = bo1["考试设置"]["试卷内容"].ToString();
            }
            newJson = newJson + test + "}";
            return newJson;
        }


        /// <summary>
        /// 得到在线考试Json用
        /// </summary>
        /// <returns></returns>
        public string 转化成Json()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            试卷内容 testContent;
            if (this.类型 == 0)
            {
                testContent = 试卷内容.给试卷内容中试题内容Json赋值(this.练习设置.试卷内容, false);
                试卷内容.替换Json的扛(testContent);
                this.练习设置.试卷内容 = testContent;
            }
            else
            {
                testContent = 试卷内容.给试卷内容中试题内容Json赋值(this.考试设置.试卷内容, false);
                试卷内容.替换Json的扛(testContent);
                this.考试设置.试卷内容 = testContent;
            }
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            json = 试卷内容.还原Json的扛(json);
            json = json.Replace("\"试题内容Json\": \"", "\"试题内容Json\":");
            json = json.Replace("\\r\\n", "");
            json = json.Replace("\"outside\":null}\"", "\"outside\":null}");
            json = json.Replace("\"00000000-0000-0000-0000-000000000000\"", "null");
            return json;
        }


        public static 考生做过的试卷 把Json转化成考生做过的试卷(string 考生做过的试卷Json)
        {
            考生做过的试卷Json = 考生做过的试卷Json.Replace("'", "\"");
            考生做过的试卷Json = 考生做过的试卷Json.Replace("\"试题内容Json\":", "\"试题内容Json\":'");
            //复合题中的空格数不一样，所以替换2次
            考生做过的试卷Json = 考生做过的试卷Json.Replace("\"outside\":null}", "\"outside\":null}'");
            考生做过的试卷Json = 考生做过的试卷Json.Replace("\"试题内容Json\":' null", "\"试题内容Json\":'null'");
            JavaScriptSerializer js = new JavaScriptSerializer();
            考生做过的试卷 memberDoneTest = js.Deserialize<考生做过的试卷>(考生做过的试卷Json);
            JObject bo = JObject.Parse(考生做过的试卷Json);
            for (int i = 0; i < memberDoneTest.考试设置.试卷内容.试卷中大题集合.Count; i++)
            {
                for (int j = 0; j < memberDoneTest.考试设置.试卷内容.试卷中大题集合[i].试卷大题中试题集合.Count; j++)
                {
                    //复合题和多题干共选项题
                    if (memberDoneTest.考试设置.试卷内容.试卷中大题集合[i].试卷大题中试题集合[j].子小题集合 != null)
                    {
                        for (int k = 0; k < memberDoneTest.考试设置.试卷内容.试卷中大题集合[i].试卷大题中试题集合[j].子小题集合.Count; k++)
                        {
                            if (memberDoneTest.考试设置.试卷内容.试卷中大题集合[i].试卷大题中试题集合[j].子小题集合[k].该题考试回答 != null)
                            {
                                int type = memberDoneTest.考试设置.试卷内容.试卷中大题集合[i].试卷大题中试题集合[j].子小题集合[k]
                                    .该题考试回答.回答的题型;
                                string answer = bo["考试设置"]["试卷内容"]["试卷中大题集合"][i]["试卷大题中试题集合"][j]["子小题集合"][k]["该题考试回答"].ToString();
                                考生考试回答 memberTestAnswer = memberDoneTest.考试设置.试卷内容.试卷中大题集合[i]
                                    .试卷大题中试题集合[j].子小题集合[k].该题考试回答;
                                memberTestAnswer = 把考生考试回答转化成相应的题型回答(answer, memberTestAnswer, type);
                                memberDoneTest.考试设置.试卷内容.试卷中大题集合[i].试卷大题中试题集合[j].子小题集合[k].该题考试回答 = memberTestAnswer;
                            }
                        }
                    }
                    //普通小题
                    else
                    {
                        if (memberDoneTest.考试设置.试卷内容.试卷中大题集合[i].试卷大题中试题集合[j].该题考试回答 != null)
                        {
                            int type = memberDoneTest.考试设置.试卷内容.试卷中大题集合[i].试卷大题中试题集合[j]
                                .该题考试回答.回答的题型;                         
                            string answer = bo["考试设置"]["试卷内容"]["试卷中大题集合"][i]["试卷大题中试题集合"][j]["该题考试回答"].ToString();
                            考生考试回答 memberTestAnswer = memberDoneTest.考试设置.试卷内容.试卷中大题集合[i]
                                .试卷大题中试题集合[j].该题考试回答;
                            memberTestAnswer = 把考生考试回答转化成相应的题型回答(answer, memberTestAnswer, type);
                            memberDoneTest.考试设置.试卷内容.试卷中大题集合[i].试卷大题中试题集合[j].该题考试回答 = memberTestAnswer;
                        }
                    }
                }
            }

            return memberDoneTest;
        }

        public static 考生考试回答 把考生考试回答转化成相应的题型回答(string 考试回答Json, 考生考试回答 memberTestAnswer, int 回答的题型)
        {
            switch (回答的题型)
            {
                case 11:
                    {
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        考生单选题回答 memberSingleAnswer = jss.Deserialize<考生单选题回答>(考试回答Json);
                        memberTestAnswer = memberSingleAnswer;
                        break;
                    }
                case 12:
                    {
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        考生多选题回答 memberMultiAnswer = jss.Deserialize<考生多选题回答>(考试回答Json);
                        memberTestAnswer = memberMultiAnswer;
                        break;
                    }
                case 13:
                    {
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        考生填空题回答 memberFillAnswer = jss.Deserialize<考生填空题回答>(考试回答Json);
                        memberTestAnswer = memberFillAnswer;
                        break;
                    }
                case 14:
                    {
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        考生选词填空回答 memberdictionAnswer = jss.Deserialize<考生选词填空回答>(考试回答Json);
                        memberTestAnswer = memberdictionAnswer;
                        break;
                    }
                case 15:
                    {
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        考生完形填空回答 memberClozeAnswer = jss.Deserialize<考生完形填空回答>(考试回答Json);
                        memberTestAnswer = memberClozeAnswer;
                        break;
                    }
                case 20:
                    {
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        考生判断题回答 memberJudgeAnswer = jss.Deserialize<考生判断题回答>(考试回答Json);
                        memberTestAnswer = memberJudgeAnswer;
                        break;
                    }
                case 30:
                    {
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        考生连线题回答 memberLinkAnswer = jss.Deserialize<考生连线题回答>(考试回答Json);
                        memberTestAnswer = memberLinkAnswer;
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
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        考生问答题回答 memberQuestionAnswer = jss.Deserialize<考生问答题回答>(考试回答Json);
                        memberTestAnswer = memberQuestionAnswer;
                        break;
                    }
            }
            return memberTestAnswer;
        }



       
        /// <param name="类型">0全部,1已完成，2未完成</param>       
        public static List<考生做过的试卷> 得到考生做过的所有考试(string 关键字,int 类型,Guid 考生ID, DateTime? 开始时间, DateTime? 结束时间, int 第几页, int 页的大小, out int 返回总条数)
        {
            IQueryable<考生做过的试卷> query = 考生做过的试卷联合考试设置查询.Where(a => a.考生ID == 考生ID && a.类型 == 1);
            if (类型 == 1)
            {
                query = query.Where(a => a.是否是已提交的试卷 == true);
            }
            else if(类型==2)
            {
                query = query.Where(a => a.是否是已提交的试卷 == false);
            }        
            if (String.IsNullOrEmpty(关键字) == false)
            {
                query = query.Where(a => a.考试设置表.试卷内容表.名称.Contains(关键字));
            }
            
            if (开始时间 != null && 结束时间 != null)
            {
                query = query.Where(a => a.答题结束时间 > 开始时间 && a.答题结束时间 < 结束时间);
            }          
            返回总条数 = query.Count();
            List<考生做过的试卷> listMemberDoneTest = query.OrderByDescending(a => a.答题开始时间).Skip(第几页 * 页的大小)
                .Take(页的大小).ToList();
            if (listMemberDoneTest.Count > 0)
            {
                考生做过的试卷.给考生做过的试卷列表赋值考试设置属性(listMemberDoneTest);
                List<考试设置> listExamSet = listMemberDoneTest.Select(a => a.考试设置).ToList();
                考试设置.给考试设置列表赋值试卷内容属性(listExamSet);
                考试设置.给考试设置列表赋值试卷外部信息属性(listExamSet);
                给考生做过的试卷列表赋值及格情况属性(listMemberDoneTest);
                foreach (考生做过的试卷 memberDoneTest in listMemberDoneTest)
                {
                    memberDoneTest.是否公布考试结果 = memberDoneTest.考试设置.是否公布考试结果;
                    if (DateTime.Now > memberDoneTest.考试设置.考试结束时间)
                    {
                        memberDoneTest.考试是否结束 = true;
                    }
                    else
                    {
                        memberDoneTest.考试是否结束 = false;
                    }
                }
            }
            return listMemberDoneTest;
        }



       
        /// <param name="类型">0全部,1已完成，2未完成</param>   
        public static List<考生做过的试卷> 得到考生做过的所有练习(string 关键字,int 类型,Guid 考生ID,  int 第几页, int 页的大小, out int 返回总条数)
        {
            IQueryable<考生做过的试卷> query = 考生做过的试卷联合练习设置查询.Where(a => a.考生ID == 考生ID && a.类型 == 0);
            if (类型 == 1)
            {
                query = query.Where(a => a.是否是已提交的试卷 == true);
            }
            else if(类型==2)
            {
                query = query.Where(a => a.是否是已提交的试卷 == false);
            }
            if (!String.IsNullOrEmpty(关键字))
            {
                query = query.Where(a => a.练习设置表.试卷内容表.名称.Contains(关键字));
            }          
            返回总条数 = query.Count();
            List<考生做过的试卷> list = query.OrderByDescending(a => a.答题开始时间).Skip(第几页 * 页的大小)
                .Take(页的大小).ToList();
            if (list.Count > 0)
            {
                考生做过的试卷.给考生做过的试卷列表赋值练习设置属性(list);
                List<练习设置> listExerciseSet = list.Select(a => a.练习设置).ToList();
                练习设置.给练习设置列表赋值试卷外部信息属性(listExerciseSet);
                给考生做过的试卷列表赋值及格情况属性(list);
            }
            return list;
        }




        public static void 删除考生未完成的试卷记录(Guid 考生做过的试卷ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            考生做过的试卷表 memberDoneTest = db.考生做过的试卷表.First(a => a.ID == 考生做过的试卷ID);
            db.考生做过的试卷表.DeleteObject(memberDoneTest);
            db.SaveChanges();
        }



        public static List<考生做过的试卷> 得到某场考试参考学生考试结果(string 关键字, Guid 考试设置ID, Guid 部门ID, int 第几页, int 页的大小, out int 返回总条数, out 考试设置 考试设置)
        {
            List<考生做过的试卷> listMemberDoneTest = 考生做过的试卷联合查询.Where(a => a.是否是已提交的试卷 == true && a.类型 == 1
                   && a.相关ID == 考试设置ID).ToList();
            listMemberDoneTest = listMemberDoneTest.OrderByDescending(a => a.客观题总得分 + a.主观题总得分).ToList();
            //赋值总名次
            for (int i = 0; i < listMemberDoneTest.Count; i++)
            {
                listMemberDoneTest[i].总名次 = i + 1;
            }
            //查询一个班级，并赋值班级名次
            List<考生做过的试卷> listOneClassDoneTest = new List<考生做过的试卷>();
            if (部门ID != Guid.Empty)
            {
                listOneClassDoneTest = listMemberDoneTest.Where(a => a.考生表.部门ID == 部门ID).ToList();
                listOneClassDoneTest = listOneClassDoneTest.OrderByDescending(a => a.客观题总得分 + a.主观题总得分).ToList();
                for (int i = 0; i < listOneClassDoneTest.Count; i++)
                {
                    考生做过的试卷 oneClassDoneTest = listMemberDoneTest.Where(a => a.考生ID == listOneClassDoneTest[i].考生ID).First();
                    oneClassDoneTest.班级名次 = i + 1;
                }
            }
            List<考生做过的试卷> listKeyDoneTest = new List<考生做过的试卷>();
            if (!string.IsNullOrEmpty(关键字))
            {
                if (部门ID == Guid.Empty)
                {
                    listKeyDoneTest = listMemberDoneTest.Where(a => a.考生表.编号.Contains(关键字) || a.考生表.姓名.Contains(关键字))
                        .ToList();
                }
                else
                {
                    listKeyDoneTest = listOneClassDoneTest.Where(a => a.考生表.编号.Contains(关键字) || a.考生表.姓名.Contains(关键字))
                        .ToList();
                }
                
            }
            考试设置 examSet = 考试设置.考试设置联合试卷内容查询.Where(a => a.ID == 考试设置ID).First();                       
            examSet.试卷内容 = 试卷内容.把试卷内容表转化成试卷内容(examSet.试卷内容表);
            考试设置 = examSet;
            List<考生做过的试卷> listReturn = new List<考生做过的试卷>();
            if (!string.IsNullOrEmpty(关键字))
            {
                返回总条数 = listKeyDoneTest.Count;
                listReturn = listKeyDoneTest.Skip(第几页 * 页的大小).Take(页的大小).ToList();
            }
            else if (部门ID != Guid.Empty)
            {
                返回总条数 = listOneClassDoneTest.Count;
                listReturn = listOneClassDoneTest.Skip(第几页 * 页的大小).Take(页的大小).ToList();
            }
            else
            {
                返回总条数 = listMemberDoneTest.Count;
                listReturn = listMemberDoneTest.Skip(第几页 * 页的大小).Take(页的大小).ToList();
            }
            foreach (考生做过的试卷 memberDoneTest in listReturn)
            {
                memberDoneTest.考生 = 考生.把考生表转化成考生(memberDoneTest.考生表);
            }
            return listReturn;
        }



       
        public static 考生做过的试卷 得到一份答卷(Guid 考生做过的试卷ID)
        {
            Guid 查看答卷人ID = 用户信息.CurrentUser.用户ID;
            考生做过的试卷 memberDoneTest = 考生做过的试卷查询.Where(a => a.ID == 考生做过的试卷ID).FirstOrDefault();
            if (memberDoneTest == null)
            {
                throw new Exception("该答卷不存在！");
            }
            //设置该考试的人可以查看所有的答卷，其他人只能查看自己的答卷
            if (memberDoneTest.类型 == 0)
            {
                练习设置 exercise = 练习设置.练习设置查询.Where(a => a.试卷内容ID == memberDoneTest.相关ID).First();
                memberDoneTest.是否公布考试结果 = true;
                memberDoneTest.考试是否结束 = true;
                memberDoneTest.设置人ID = exercise.设置人ID;
                if (exercise.设置人ID != 查看答卷人ID)
                {
                    if (memberDoneTest.考生ID != 查看答卷人ID)
                    {
                        throw new Exception("您无权查看他人答卷！");
                    }
                }
            }
            else
            {
                考试设置 examSet = 考试设置.考试设置查询.Where(a => a.ID == memberDoneTest.相关ID).First();
                memberDoneTest.是否公布考试结果 = examSet.是否公布考试结果;
                memberDoneTest.设置人ID = examSet.设置人ID;
                if (DateTime.Now > examSet.考试结束时间)
                {
                    memberDoneTest.考试是否结束 = true;
                }
                if (examSet.设置人ID != 查看答卷人ID)
                {
                    if (memberDoneTest.考生ID != 查看答卷人ID)
                    {
                        throw new Exception("您无权查看他人答卷！");
                    }
                    else
                    {
                        if (memberDoneTest.是否是已提交的试卷==true)
                        {
                            if (examSet.是否公布考试结果 == false)
                            {
                                throw new Exception("该场考试未公布考试结果，您暂无法查看答卷！");
                            }
                            if (DateTime.Now < examSet.考试结束时间)
                            {
                                throw new Exception("考试结束后才能查看答卷！");
                            }
                        }
                    }
                }
            }          
            //该份试卷的回答
            List<考生考试回答> listMemberTestAnswer = 考生考试回答.考生考试回答查询.Where(a => a.考生做过的试卷ID
                == 考生做过的试卷ID).ToList();
            //回答的试卷大题中试题ID集合
            List<Guid> listTestProblemId = listMemberTestAnswer.Select(a => a.试卷大题中试题ID).ToList();
            //回答的试题内容ID集合
            List<Guid> listProblemContentId = 试卷大题中试题.试卷大题中试题查询.Where(a => listTestProblemId.Contains<Guid>(a.ID))
                .Select(a => a.试题内容ID).ToList();
            //回答的试题内容集合按题型分组
            List<试题内容> listContent = 试题内容.试题内容查询.Where(a => listProblemContentId.Contains<Guid>(a.ID))
                .ToList();
            var groupProblemContent = listContent.GroupBy(a => a.小题型Enum).ToList();
            int m = 0;
            for (int i = 0; i < groupProblemContent.Count; i++)
            {
                //该组的试卷大题中试题ID集合
                List<Guid> listGroupTestProblemId = new List<Guid>();
                List<试卷中大题> 试卷中大题集合 = new List<试卷中大题>();
                if (memberDoneTest.类型 == 0)
                {
                    试卷中大题集合 = memberDoneTest.练习设置.试卷内容.试卷中大题集合;
                }
                else
                {
                    试卷中大题集合 = memberDoneTest.考试设置.试卷内容.试卷中大题集合;
                }
                foreach (试题内容 content in groupProblemContent[i])
                {
                    foreach (试卷中大题 type in 试卷中大题集合)
                    {
                        foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                        {
                            if (content.ID == testProblem.试题内容ID)
                            {
                                listGroupTestProblemId.Add(testProblem.ID);
                                m = 1;
                                break;
                            }
                        }
                        if (m == 1)
                        {
                            m = 0;
                            break;
                        }
                    }
                }
                switch (groupProblemContent[i].Key)
                {
                    case 11:
                    case 8011:
                        {
                            List<考生单选题回答> listSingleAnswer = 考生单选题回答.考生单选题回答查询
                                .Where(a => a.考生做过的试卷ID == 考生做过的试卷ID && listGroupTestProblemId
                                    .Contains<Guid>(a.试卷大题中试题ID)).ToList();
                            int aa = 0;
                            //给试卷大题中试题赋值该题考试回答
                            foreach (考生单选题回答 singleAnswer in listSingleAnswer)
                            {
                                foreach (试卷中大题 type in 试卷中大题集合)
                                {
                                    foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                                    {
                                        if (singleAnswer.试卷大题中试题ID == testProblem.ID)
                                        {                                           
                                            singleAnswer.回答的题型 = groupProblemContent[i].Key;
                                            testProblem.该题考试回答 = singleAnswer;
                                            aa = 1;
                                            break;
                                        }
                                    }
                                    if (aa == 1)
                                    {
                                        aa = 0;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case 12:
                        {
                            List<考生多选题回答> listMultiAnswer = 考生多选题回答.考生多选题回答查询
                                .Where(a => a.考生做过的试卷ID == 考生做过的试卷ID && listGroupTestProblemId
                                    .Contains<Guid>(a.试卷大题中试题ID)).ToList();
                            List<Guid> listMultiAnswerId = listMultiAnswer.Select(a => a.ID).ToList();
                            //查询上面这些多选题的回答
                            List<多选题回答> listAnswer = 多选题回答.多选题回答查询.Where(a =>
                                listMultiAnswerId.Contains<Guid>(a.考生考试回答ID)).ToList();
                            //给对应的考生多选题回答赋上回答的值
                            foreach (考生多选题回答 multiAnswer in listMultiAnswer)
                            {
                                List<Guid> listAnswerId = listAnswer.Where(a => a.考生考试回答ID == multiAnswer.ID)
                                    .Select(a => a.回答的选项ID).ToList();
                                multiAnswer.回答的选项ID集合 = listAnswerId;
                            }
                            int bb = 0;
                            //给试卷大题中试题赋值该题考试回答
                            foreach (考生多选题回答 multiAnswer in listMultiAnswer)
                            {
                                foreach (试卷中大题 type in 试卷中大题集合)
                                {
                                    foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                                    {
                                        if (multiAnswer.试卷大题中试题ID == testProblem.ID)
                                        {                                         
                                            multiAnswer.回答的题型 = groupProblemContent[i].Key;
                                            testProblem.该题考试回答 = multiAnswer;
                                            bb = 1;
                                            break;
                                        }
                                    }
                                    if (bb == 1)
                                    {
                                        bb = 0;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case 13:
                        {
                            List<考生填空题回答> listFillAnswer = 考生填空题回答.考生填空题回答查询
                               .Where(a => a.考生做过的试卷ID == 考生做过的试卷ID && listGroupTestProblemId
                                   .Contains<Guid>(a.试卷大题中试题ID)).ToList();
                            //查找这些填空题的回答并赋值
                            List<Guid> listFillAnswerId = listFillAnswer.Select(a => a.ID).ToList();
                            List<填空空格回答> listFillSpaceAnswer = 填空空格回答.填空空格回答查询
                                .Where(a => listFillAnswerId.Contains<Guid>(a.考生考试回答ID)).ToList();
                            foreach (考生填空题回答 fillAnswer in listFillAnswer)
                            {
                                fillAnswer.填空空格回答集合 = listFillSpaceAnswer.Where(a => a.考生考试回答ID == fillAnswer.ID).ToList();
                            }
                            int cc = 0;
                            //给试卷大题中试题赋值该题考试回答
                            foreach (考生填空题回答 fillAnswer in listFillAnswer)
                            {
                                foreach (试卷中大题 type in 试卷中大题集合)
                                {
                                    foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                                    {
                                        if (fillAnswer.试卷大题中试题ID == testProblem.ID)
                                        {
                                            fillAnswer.回答的题型 = groupProblemContent[i].Key;
                                            testProblem.该题考试回答 = fillAnswer;
                                            cc = 1;
                                            break;
                                        }
                                    }
                                    if (cc == 1)
                                    {
                                        cc = 0;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case 14:
                        {
                            List<考生选词填空回答> listDictionAnswer = 考生选词填空回答.考生选词填空回答查询
                              .Where(a => a.考生做过的试卷ID == 考生做过的试卷ID && listGroupTestProblemId
                                  .Contains<Guid>(a.试卷大题中试题ID)).ToList();
                            //查找这些选词填空的回答并赋值
                            List<Guid> listDictionAnswerId = listDictionAnswer.Select(a => a.ID).ToList();
                            List<选词空格回答> listDictionSpaceAnswer = 选词空格回答.选词空格回答查询
                                .Where(a => listDictionAnswerId.Contains<Guid>(a.考生考试回答ID)).ToList();
                            foreach (考生选词填空回答 dictionAnswer in listDictionAnswer)
                            {
                                dictionAnswer.选词空格回答集合 = listDictionSpaceAnswer.Where(a => a.考生考试回答ID == dictionAnswer.ID).ToList();
                            }
                            int dd = 0;

                            //给试卷大题中试题赋值该题考试回答
                            foreach (考生选词填空回答 dictionAnswer in listDictionAnswer)
                            {
                                foreach (试卷中大题 type in 试卷中大题集合)
                                {
                                    foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                                    {
                                        if (dictionAnswer.试卷大题中试题ID == testProblem.ID)
                                        {
                                            dictionAnswer.回答的题型 = groupProblemContent[i].Key;
                                            testProblem.该题考试回答 = dictionAnswer;
                                            dd = 1;
                                            break;
                                        }
                                    }
                                    if (dd == 1)
                                    {
                                        dd = 0;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case 15:
                        {
                            List<考生完形填空回答> listClozeAnswer = 考生完形填空回答.考生完形填空回答查询
                                .Where(a => a.考生做过的试卷ID == 考生做过的试卷ID && listGroupTestProblemId
                                    .Contains<Guid>(a.试卷大题中试题ID)).ToList();
                            //查找这些完形填空的回答并赋值
                            List<Guid> listClozeAnswerId = listClozeAnswer.Select(a => a.ID).ToList();
                            List<完形空格回答> listClozeSpaceAnswer = 完形空格回答.完形空格回答查询
                                .Where(a => listClozeAnswerId.Contains<Guid>(a.考生考试回答ID)).ToList();
                            foreach (考生完形填空回答 clozeAnswer in listClozeAnswer)
                            {
                                clozeAnswer.完形空格回答集合 = listClozeSpaceAnswer.Where(a => a.考生考试回答ID == clozeAnswer.ID).ToList();
                            }
                            int n = 0;
                            //给试卷大题中试题赋值该题考试回答
                            foreach (考生完形填空回答 clozeAnswer in listClozeAnswer)
                            {
                                foreach (试卷中大题 type in 试卷中大题集合)
                                {
                                    foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                                    {
                                        if (clozeAnswer.试卷大题中试题ID == testProblem.ID)
                                        {
                                            clozeAnswer.回答的题型 = groupProblemContent[i].Key;
                                            testProblem.该题考试回答 = clozeAnswer;
                                            n = 1;
                                            break;
                                        }
                                    }
                                    if (n == 1)
                                    {
                                        n = 0;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case 20:
                        {
                            List<考生判断题回答> listJudgeAnswer = 考生判断题回答.考生判断题回答查询
                                .Where(a => a.考生做过的试卷ID == 考生做过的试卷ID && listGroupTestProblemId.Contains<Guid>(a.试卷大题中试题ID))
                                .ToList();
                            int ee = 0;
                            //给试卷大题中试题赋值该题考试回答
                            foreach (考生判断题回答 judgeAnswer in listJudgeAnswer)
                            {
                                foreach (试卷中大题 type in 试卷中大题集合)
                                {
                                    foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                                    {
                                        if (judgeAnswer.试卷大题中试题ID == testProblem.ID)
                                        {
                                            judgeAnswer.回答的题型 = groupProblemContent[i].Key;
                                            testProblem.该题考试回答 = judgeAnswer;
                                            ee = 1;
                                            break;
                                        }
                                    }
                                    if (ee == 1)
                                    {
                                        ee = 0;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case 30:
                        {
                            List<考生连线题回答> listLinkAnswer = 考生连线题回答.考生连线题回答查询
                                .Where(a => a.考生做过的试卷ID == 考生做过的试卷ID && listGroupTestProblemId
                                    .Contains<Guid>(a.试卷大题中试题ID)).ToList();
                            //查找这些连线题的回答并赋值
                            List<Guid> listlinkAnswerId = listLinkAnswer.Select(a => a.ID).ToList();
                            List<连线题回答> listLinkSpaceAnswer = 连线题回答.连线题回答查询
                                .Where(a => listlinkAnswerId.Contains<Guid>(a.考生考试回答ID)).ToList();
                            foreach (考生连线题回答 linkAnswer in listLinkAnswer)
                            {
                                linkAnswer.连线题回答集合 = listLinkSpaceAnswer.Where(a => a.考生考试回答ID == linkAnswer.ID).ToList();
                            }
                            int ff = 0;
                            //给试卷大题中试题赋值该题考试回答
                            foreach (考生连线题回答 linkAnswer in listLinkAnswer)
                            {
                                foreach (试卷中大题 type in 试卷中大题集合)
                                {
                                    foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                                    {
                                        if (linkAnswer.试卷大题中试题ID == testProblem.ID)
                                        {
                                            linkAnswer.回答的题型 = groupProblemContent[i].Key;
                                            testProblem.该题考试回答 = linkAnswer;
                                            ff = 1;
                                            break;
                                        }
                                    }
                                    if (ff == 1)
                                    {
                                        ff = 0;
                                        break;
                                    }
                                }
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
                            List<考生问答题回答> listQuestionAnswer = 考生问答题回答.考生问答题回答查询
                                .Where(a => a.考生做过的试卷ID == 考生做过的试卷ID && listGroupTestProblemId
                                    .Contains<Guid>(a.试卷大题中试题ID)).ToList();
                            int gg = 0;
                            //给试卷大题中试题赋值该题考试回答
                            foreach (考生问答题回答 questionAnswer in listQuestionAnswer)
                            {
                                foreach (试卷中大题 type in 试卷中大题集合)
                                {
                                    foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                                    {
                                        if (questionAnswer.试卷大题中试题ID == testProblem.ID)
                                        {
                                            questionAnswer.回答的题型 = groupProblemContent[i].Key;
                                            testProblem.该题考试回答 = questionAnswer;
                                            gg = 1;
                                            break;
                                        }
                                    }
                                    if (gg == 1)
                                    {
                                        gg = 0;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                }
            }


            return memberDoneTest;
        }



        public static void 给考生做过的试卷列表赋值考试设置属性(List<考生做过的试卷> list)
        {
            foreach (考生做过的试卷 memberDoneTest in list)
            {
                memberDoneTest.考试设置 = 考试设置.把考试设置表转化成考试设置(memberDoneTest.考试设置表);
            }
        }



        public static void 给考生做过的试卷列表赋值练习设置属性(List<考生做过的试卷> list)
        {
            foreach (考生做过的试卷 memberDoneTest in list)
            {
                memberDoneTest.练习设置 = 练习设置.把练习设置表转化成练习设置(memberDoneTest.练习设置表);
            }
        }



        private static void 给考生做过的试卷列表赋值及格情况属性(List<考生做过的试卷> listMemberDoneTest)
        {
            foreach (考生做过的试卷 memberDoneTest in listMemberDoneTest)
            {
                if (memberDoneTest.类型 == 0)
                {
                    if (memberDoneTest.总得分 / memberDoneTest.练习设置.试卷外部信息.总分 * 100 < memberDoneTest.练习设置.及格条件)
                    {
                        memberDoneTest.及格情况 = "不及格";
                    }
                    else
                    {
                        memberDoneTest.及格情况 = "及格";
                    }
                }
                else
                {
                    if (memberDoneTest.总得分 / memberDoneTest.考试设置.试卷外部信息.总分 * 100 < memberDoneTest.考试设置.及格条件)
                    {
                        memberDoneTest.及格情况 = "不及格";
                    }
                    else
                    {
                        memberDoneTest.及格情况 = "及格";
                    }
                }
            }
        }

        #endregion

    }
}
