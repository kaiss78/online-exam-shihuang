using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace LoveKaoExam.Data
{
    
    public class 复合题 : 试题内容
    {

        #region 变量
        private List<试题内容> _子小题集合;
        #endregion

        #region 属性

        public List<试题内容> 子小题集合
        {
            get
            {
                if (_子小题集合 == null)
                {
                    _子小题集合 = new List<试题内容>();
                    List<试题内容> listContent = 试题内容.试题内容查询.Where(a => a.父试题内容ID == this.ID).ToList();
                    //按题型分组
                    var group = listContent.GroupBy(a => a.小题型Enum).ToList();
                    for (int i = 0; i < group.Count; i++)
                    {
                        List<Guid> listContentId = group[i].Select(a => a.ID).ToList();
                        switch (group[i].Key)
                        {
                            case 11:
                                {
                                    List<单选题> listSingle = 单选题.单选题查询.Where(a => listContentId.Contains<Guid>(a.ID))
                                        .ToList();
                                    foreach (单选题 single in listSingle)
                                    {
                                        _子小题集合.Add(single);
                                    }
                                    break;
                                }
                            case 12:
                                {
                                    List<多选题> listMulti = 多选题.多选题查询.Where(a => listContentId.Contains<Guid>(a.ID))
                                        .ToList();
                                    foreach (多选题 multi in listMulti)
                                    {
                                        _子小题集合.Add(multi);
                                    }
                                    break;
                                }
                            case 13:
                                {
                                    List<填空题> listFill =填空题.填空题查询.Where(a => listContentId.Contains<Guid>(a.ID))
                                        .ToList();
                                    foreach (填空题 fill in listFill)
                                    {
                                        _子小题集合.Add(fill);
                                    }
                                    break;
                                }
                            case 20:
                                {
                                    List<判断题> listJudge =判断题.判断题查询.Where(a => listContentId.Contains<Guid>(a.ID))
                                        .ToList();
                                    foreach (判断题 judge in listJudge)
                                    {
                                        _子小题集合.Add(judge);
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
                                    List<问答题> listQuestionAnswer = 问答题.问答题查询.Where(a => listContentId
                                        .Contains<Guid>(a.ID)).ToList();
                                    foreach (问答题 question in listQuestionAnswer)
                                    {
                                        _子小题集合.Add(question);
                                    }
                                    break;
                                }
                        }
                    }
                    _子小题集合 = _子小题集合.OrderBy(a => a.本题在复合题中顺序).ToList();
                    return _子小题集合;
                }
                _子小题集合 = _子小题集合.OrderBy(a => a.本题在复合题中顺序).ToList();
                return _子小题集合;
            }
            set
            {
                _子小题集合 = value;
            }
        }



        public static IQueryable<复合题> 复合题查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试题内容表.Where(a => a.小题型Enum > 39 && a.小题型Enum < 50).Select(a => new 复合题
                    {
                        ID = a.ID,
                        Json字符串 = a.Json字符串,
                        本题在复合题中顺序 = a.本题在复合题中顺序,
                        操作人ID = a.操作人ID,
                        操作时间 = a.操作时间,
                        父试题内容ID = a.父试题内容ID,
                        解题思路 = a.解题思路,
                        试题外部信息ID = a.试题外部信息ID,
                        题干HTML = a.题干HTML,
                        题干文本 = a.题干文本,
                        小题型Enum = a.小题型Enum
                    });
            }
        }

        #endregion


        #region 方法

        public override void 保存(LoveKaoExamEntities db, string Json字符串)
        {
            试题内容表 content = new 试题内容表();
            content.ID = this.ID;
            content.操作人ID = this.操作人ID;
            content.操作时间 = DateTime.Now;
            content.解题思路 = this.解题思路;
            content.试题外部信息ID = this.试题外部信息ID;
            content.题干HTML = this.题干HTML;
            content.题干文本 = this.题干文本;
            content.小题型Enum = this.小题型Enum;
            content.难易度 = this.难易度;
            JObject bo = JObject.Parse(Json字符串);
            for (int i = 0; i < this.子小题集合.Count; i++)
            {
                switch (this.子小题集合[i].小题型Enum)
                {
                    //单选题
                    case 11:
                        {
                            string singleJson = bo["子小题集合"][i].ToString();
                            JavaScriptSerializer jss = new JavaScriptSerializer();
                            单选题 single = jss.Deserialize<单选题>(singleJson);
                            单选题.给单选题选项赋值新ID(single);
                            single.操作人ID = this.操作人ID;
                            single.试题外部信息ID = this.试题外部信息ID;
                            single.父试题内容ID = content.ID;
                            single.爱考网ID = this.子小题集合[i].爱考网ID;
                            single.ID = this.子小题集合[i].ID;
                            试题内容表 singleContent = single.类映射表赋值();
                            singleContent.父试题内容ID = content.ID;
                            singleContent.本题在复合题中顺序 = Convert.ToByte(i);
                            content.试题内容表1.Add(singleContent);
                            break;
                        }
                    //多选题
                    case 12:
                        {
                            string multiJson = bo["子小题集合"][i].ToString();
                            JavaScriptSerializer jss = new JavaScriptSerializer();
                            多选题 multi = jss.Deserialize<多选题>(multiJson);
                            多选题.给多选题选项赋值新ID(multi);
                            multi.操作人ID = this.操作人ID;
                            multi.试题外部信息ID = this.试题外部信息ID;
                            multi.父试题内容ID = content.ID;
                            multi.爱考网ID = this.子小题集合[i].爱考网ID;
                            multi.ID = this.子小题集合[i].ID;
                            试题内容表 multiContent = multi.类映射表赋值();
                            multiContent.父试题内容ID = content.ID;
                            multiContent.本题在复合题中顺序 = Convert.ToByte(i);
                            content.试题内容表1.Add(multiContent);
                            break;
                        }
                    //填空题
                    case 13:
                        {
                            string fillJson = bo["子小题集合"][i].ToString();
                            JavaScriptSerializer jss = new JavaScriptSerializer();
                            填空题 fill = jss.Deserialize<填空题>(fillJson);
                            fill.操作人ID = this.操作人ID;
                            fill.试题外部信息ID = this.试题外部信息ID;
                            fill.父试题内容ID = content.ID;
                            fill.爱考网ID = this.子小题集合[i].爱考网ID;
                            fill.ID = this.子小题集合[i].ID;
                            试题内容表 fillContent = fill.类映射表赋值();
                            fillContent.父试题内容ID = content.ID;
                            fillContent.本题在复合题中顺序 = Convert.ToByte(i);
                            content.试题内容表1.Add(fillContent);
                            break;
                        }
                    case 14:
                        {
                            string dictionJson = bo["子小题集合"][i].ToString();
                            JavaScriptSerializer jss = new JavaScriptSerializer();
                            选词填空 diction = jss.Deserialize<选词填空>(dictionJson);
                            选词填空.给选词填空选项赋值新ID(diction);                        
                            diction.操作人ID = this.操作人ID;
                            diction.试题外部信息ID = this.试题外部信息ID;
                            diction.父试题内容ID = content.ID;
                            diction.爱考网ID = this.子小题集合[i].爱考网ID;
                            diction.ID = this.子小题集合[i].ID;
                            试题内容表 dictionContent = diction.类映射表赋值();
                            dictionContent.父试题内容ID = content.ID;
                            dictionContent.本题在复合题中顺序 = Convert.ToByte(i);
                            content.试题内容表1.Add(dictionContent);
                            break;
                        }
                    case 15:
                        {
                            string clozeJson = bo["子小题集合"][i].ToString();
                            JavaScriptSerializer jss = new JavaScriptSerializer();
                            完形填空 cloze = jss.Deserialize<完形填空>(clozeJson);
                            完形填空.给完形填空选项赋值新ID(cloze);
                            cloze.操作人ID = this.操作人ID;
                            cloze.试题外部信息ID = this.试题外部信息ID;
                            cloze.父试题内容ID = content.ID;
                            cloze.爱考网ID = this.子小题集合[i].爱考网ID;
                            cloze.ID = this.子小题集合[i].ID;
                            试题内容表 clozeContent = cloze.类映射表赋值();
                            clozeContent.父试题内容ID = content.ID;
                            clozeContent.本题在复合题中顺序 = Convert.ToByte(i);
                            content.试题内容表1.Add(clozeContent);
                            break;
                        }
                    //判断题
                    case 20:
                        {
                            string singleJson = bo["子小题集合"][i].ToString();
                            JavaScriptSerializer jss = new JavaScriptSerializer();
                            判断题 judge = jss.Deserialize<判断题>(singleJson);
                            judge.操作人ID = this.操作人ID;
                            judge.试题外部信息ID = this.试题外部信息ID;
                            judge.父试题内容ID = content.ID;
                            judge.爱考网ID = this.子小题集合[i].爱考网ID;
                            judge.ID = this.子小题集合[i].ID;
                            试题内容表 judgeContent = judge.类映射表赋值();
                            judgeContent.父试题内容ID = content.ID;
                            judgeContent.本题在复合题中顺序 = Convert.ToByte(i);
                            content.试题内容表1.Add(judgeContent);
                            break;
                        }
                    case 30:
                        {
                            string linkJson = bo["子小题集合"][i].ToString();
                            JavaScriptSerializer jss = new JavaScriptSerializer();
                            连线题 link = jss.Deserialize<连线题>(linkJson);
                            连线题.给连线题选项赋值新ID(link);
                            link.操作人ID = this.操作人ID;
                            link.试题外部信息ID = this.试题外部信息ID;
                            link.父试题内容ID = content.ID;
                            link.爱考网ID = this.子小题集合[i].爱考网ID;
                            link.ID = this.子小题集合[i].ID;
                            试题内容表 linkContent = link.类映射表赋值();
                            linkContent.父试题内容ID = content.ID;
                            linkContent.本题在复合题中顺序 = Convert.ToByte(i);
                            content.试题内容表1.Add(linkContent);
                            break;
                        }
                    //问答题
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
                            string questionAnswerJson = bo["子小题集合"][i].ToString();
                            JavaScriptSerializer jss = new JavaScriptSerializer();
                            问答题 questionAnswer = jss.Deserialize<问答题>(questionAnswerJson);
                            questionAnswer.操作人ID = this.操作人ID;
                            questionAnswer.试题外部信息ID = this.试题外部信息ID;
                            questionAnswer.父试题内容ID = content.ID;
                            questionAnswer.爱考网ID = this.子小题集合[i].爱考网ID;
                            questionAnswer.ID = this.子小题集合[i].ID;
                            试题内容表 questionAnswerContent = questionAnswer.类映射表赋值();
                            questionAnswerContent.父试题内容ID = content.ID;
                            questionAnswerContent.本题在复合题中顺序 = Convert.ToByte(i);                          
                            content.试题内容表1.Add(questionAnswerContent);
                            break;
                        }

                }
            }
            content.Json字符串 = this.转化成Json带答案();
            content.爱考网ID = this.爱考网ID;          
            db.试题内容表.AddObject(content);
            db.SaveChanges();
        }



       

        public override string 生成查询内容()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.题干文本);
            List<string> choiceList = new List<string>();
            for (int i = 0; i < this.子小题集合.Count; i++)
            {
                choiceList.Add(this.子小题集合[i].题干文本);
            }
            choiceList.Sort();
            for (int j = 0; j < choiceList.Count; j++)
            {
                sb.Append(choiceList[j]);
            }
            return sb.ToString();
        }

        public override string 生成试题显示列()
        {
            return this.题干文本;
        }

        public override string 转化成Json不带答案()
        {
            string json = this.Json字符串;
            JObject bo = JObject.Parse(json);
            string idea = bo["解题思路"].ToString();
            json = json.Replace("\"解题思路\": " + idea + ",", "");

            #region MyRegion
            //for (int i = 0; i < bo["子小题集合"].Count(); i++)
            //{
            //    string subTypeStr = bo["子小题集合"][i]["小题型Enum"].ToString();
            //    subTypeStr = subTypeStr.Replace("\"", "");
            //    int subType = Convert.ToInt32(subTypeStr);
            //    string subIdea = bo["子小题集合"][i]["解题思路"].ToString();
            //    json = json.Replace("\"解题思路\": " + subIdea + ",", "");
            //    switch (subType)
            //    {
            //        case 11:
            //            {
            //                string answer = bo["子小题集合"][i]["答案ID"].ToString();
            //                json = json.Replace("\"答案ID\": " + answer + ",", "");
            //                break;
            //            }
            //        case 12:
            //            {
            //                string answer = bo["子小题集合"][i]["答案列表"].ToString();
            //                answer = answer.Replace("\r\n", "\r\n  ");
            //                json = json.Replace("\"答案列表\": " + answer + ",", "");
            //                break;
            //            }
            //        case 13:
            //            {
            //                //string answer = bo["子小题集合"][i]["填空空格集合"].ToString();
            //                //answer = answer.Replace("\r\n", "\r\n  ");
            //                //json = json.Replace("\"填空空格集合\": " + answer + ",", "");
            //                for (int j = 0; j < bo["子小题集合"][i]["填空空格集合"].Count(); j++)
            //                {          
            //                    string answerListString = bo["子小题集合"][i]["填空空格集合"][j]["填空空格答案集合"].ToString();
            //                    answerListString = answerListString.Replace("\r\n", "\r\n          ");
            //                    json = json.Replace("\"填空空格答案集合\": " + answerListString + ",", "");
            //                    json = json.Replace(",\r\n          \"填空空格答案集合\": " + answerListString + "", "");
            //                }
            //                break;
            //            }
            //        case 20:
            //            {
            //                string answer = bo["子小题集合"][i]["答案"].ToString();
            //                if (answer == "true")
            //                {
            //                    json = json.Replace("\"答案\": true,", "");
            //                }
            //                else
            //                {
            //                    json = json.Replace("\"答案\": false,", "");
            //                }
            //                break;
            //            }
            //        case 60:
            //        case 61:
            //        case 62:
            //        case 63:
            //        case 64:
            //        case 65:
            //        case 66:
            //        case 67:
            //        case 68:
            //        case 69:
            //            {
            //                string answer =bo["子小题集合"][i]["答案"].ToString();
            //                json = json.Replace("\"答案\": " + answer + ",", "");
            //                break;
            //            }
            //    }
            //}
            #endregion

            return json;
        }

        public override string 转化成Json带答案()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, new 复合题不带子小题集合());
            json = json.Replace("\"小题型Enum\": " + 小题型Enum + "", "\"小题型Enum\": \"" + 小题型Enum + "\"");
            return json;
        }


        public static 复合题 把Json转化成试题内容(string 试题Json字符串)
        {          
            JObject bo = JObject.Parse(试题Json字符串);
            string content = bo["content"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            复合题 complex = jss.Deserialize<复合题>(content);
            return complex;
        }




        public static void 给复合题子小题内容赋值新ID(复合题 complex)
        {
            foreach (试题内容 content in complex.子小题集合)
            {
                content.ID = Guid.NewGuid();
            }
        }

        #endregion

    }



    class 复合题不带子小题集合 : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            复合题 complex = (复合题)value;

            JObject o = new JObject();
            o["ID"] = new JValue(complex.ID.ToString());
            o["小题型Enum"] = new JValue(complex.小题型Enum);
            o["题干HTML"] = new JValue(complex.题干HTML);
            o["题干文本"] = new JValue(complex.题干文本);
            o["试题外部信息ID"] = new JValue(complex.试题外部信息ID.ToString());
            o["操作人ID"] = new JValue(complex.操作人ID.ToString());
            o["解题思路"] = new JValue(complex.解题思路);
            o["父试题内容ID"] = new JValue(complex.父试题内容ID.ToString());
            o["难易度"] = new JValue(complex.难易度);
            o.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        {
            // convert back json string into array of picture 
            return null; ;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(复合题);
        }
    }
}
