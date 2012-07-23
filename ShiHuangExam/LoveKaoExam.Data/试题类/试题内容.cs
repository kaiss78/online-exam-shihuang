using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LoveKaoExam.Data
{
    public class 试题内容
    {
        #region 变量

        //private 试题外部信息 _试题外部信息;
        //private 会员 _操作人;

        #endregion

        #region 属性

        public int 小题型Enum
        {
            get;
            set;
        }


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

      

        public Guid 操作人ID
        {
            get;
            set;
        }


        [JsonIgnore]
        public DateTime 操作时间
        {
            get;
            set;
        }


        public string 解题思路
        {
            get;
            set;
        }


        public Guid 试题外部信息ID
        {
            get;
            set;
        }



        public string 题干HTML
        {
            get;
            set;
        }


        public string 题干文本
        {
            get;
            set;
        }


        public decimal 难易度
        {
            get;
            set;
        }


        public Guid? 父试题内容ID
        {
            get;
            set;
        }


        [JsonIgnore]
        public byte 本题在复合题中顺序
        {
            get;
            set;
        }



        public string Json字符串
        {
            get;
            set;
        }



        public static IQueryable<试题内容> 试题内容查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试题内容表.Select(a => new 试题内容
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
                        小题型Enum = a.小题型Enum,
                        难易度=a.难易度
                    });
            }
        }

        #endregion



        #region 方法


        public virtual void 保存(LoveKaoExamEntities db, string Json字符串) { }

        public virtual string 生成查询内容() { return ""; }

        public virtual string 生成试题显示列() { return ""; }

        public virtual string 转化成Json不带答案() { return ""; }

        public virtual string 转化成Json带答案() { return ""; }


        public static int 得到试题的小题型(string 试题内容Json)
        {
            JObject bo = JObject.Parse(试题内容Json);
            string typeStr = bo["content"]["小题型Enum"].ToString();
            typeStr = typeStr.Replace("\"", "");
            int type = Convert.ToInt32(typeStr);
            return type;
        }


        public static string 去掉答案(string 试题内容Json)
        {
            JObject bo = JObject.Parse(试题内容Json);
            string typeStr = bo["小题型Enum"].ToString();
            string idea = bo["解题思路"].ToString();
            试题内容Json = 试题内容Json.Replace("\"解题思路\": " + idea + ",", "");
            typeStr = typeStr.Replace("\"", "");
            int type = Convert.ToInt32(typeStr);
            switch (type)
            {
                case 11:
                case 8011:
                    {
                        string answer = bo["答案ID"].ToString();
                        试题内容Json = 试题内容Json.Replace("\"答案ID\": " + answer + ",", "");
                        break;
                    }
                case 12:
                    {
                        string answer = bo["答案列表"].ToString();
                        answer = answer.Replace("\r\n", "\r\n  ");
                        试题内容Json = 试题内容Json.Replace("\"答案列表\": " + answer + ",", "");
                        break;
                    }
                case 13:
                    {
                        for (int i = 0; i < bo["填空空格集合"].Count(); i++)
                        {
                            string answerListString = bo["填空空格集合"][i]["填空空格答案集合"].ToString();
                            answerListString = answerListString.Replace("\r\n", "\r\n      ");
                            //复合题中顺序有可能会变，所以替换2中情况
                            试题内容Json = 试题内容Json.Replace(",\r\n      \"填空空格答案集合\": " + answerListString + "", "");
                            试题内容Json = 试题内容Json.Replace("\"填空空格答案集合\": " + answerListString + ",", "");
                        }
                        break;
                    }
                case 14:
                    {                       
                        for (int i = 0; i < bo["选词空格集合"].Count(); i++)
                        {
                            string answer = bo["选词空格集合"][i]["答案ID"].ToString();
                            //复合题中顺序有可能会变，所以替换2中情况
                            试题内容Json = 试题内容Json.Replace(",\r\n      \"答案ID\": " + answer + "", "");
                            试题内容Json = 试题内容Json.Replace("\"答案ID\": " + answer + ",", "");
                        }
                        break;
                    }
                case 15:
                    {
                        for (int i = 0; i < bo["单选空格集合"].Count(); i++)
                        {
                            string answer = bo["单选空格集合"][i]["答案ID"].ToString();
                            试题内容Json = 试题内容Json.Replace(",\r\n      \"答案ID\": " + answer + "", "");
                        }

                        break;
                    }
                case 20:
                    {
                        string answer = bo["答案"].ToString();
                        if (answer == "true")
                        {
                            试题内容Json = 试题内容Json.Replace("\"答案\": true,", "");
                        }
                        else
                        {
                            试题内容Json = 试题内容Json.Replace("\"答案\": false,", "");
                        }
                        break;
                    }
                case 30:
                    {
                        string answer = bo["连线题答案集合"].ToString();
                        answer = answer.Replace("\r\n", "\r\n  ");
                        试题内容Json = 试题内容Json.Replace("\"连线题答案集合\": " + answer + ",", "");
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
                        string answer = bo["答案"].ToString();
                        试题内容Json = 试题内容Json.Replace("\"答案\": " + answer + ",", "");
                        break;
                    }             
            }
            return 试题内容Json;
        }


        public static string 转化成完整Json字符串(试题内容 试题内容, 试题外部信息 试题外部信息)
        {
            string 试题内容Json字符串 = 试题内容.Json字符串;
            StringBuilder sb = new StringBuilder();
            sb.Append("\"子小题集合\":[");
            if (试题内容.小题型Enum > 39 && 试题内容.小题型Enum < 50 || 试题内容.小题型Enum == 80)
            {
                List<试题内容> listContent = 试题内容查询.Where(a => a.父试题内容ID == 试题内容.ID)
                    .OrderBy(a => a.本题在复合题中顺序).ToList();
                foreach (试题内容 content in listContent)
                {
                    sb.Append("" + content.Json字符串 + ",");
                }
                if (listContent.Count != 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("],");
                JObject bo = JObject.Parse(试题内容Json字符串);
                string contentId = bo["ID"].ToString();
                试题内容Json字符串 = 试题内容Json字符串.Replace("\"ID\": " + contentId + ",", "\"ID\": " + contentId + "," + sb + "");

            }
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            string 试题外部信息Json字符串 = 试题外部信息.转化成Json字符串();
            string json = "{\"content\":" + 试题内容Json字符串 + ",\"outside\":" + 试题外部信息Json字符串 + "}";
            return json;
        }




        public static string 转化成完整Json字符串不带答案(试题内容 试题内容, 试题外部信息 试题外部信息)
        {
            string 试题内容Json字符串 = 试题内容.转化成Json不带答案();
            StringBuilder sb = new StringBuilder();
            sb.Append("\"子小题集合\":[");
            if (试题内容.小题型Enum > 39 && 试题内容.小题型Enum < 50 || 试题内容.小题型Enum == 80)
            {
                List<试题内容> listContent = 试题内容查询.Where(a => a.父试题内容ID == 试题内容.ID)
                    .OrderBy(a => a.本题在复合题中顺序).ToList();
                foreach (试题内容 content in listContent)
                {
                    content.Json字符串 = 去掉答案(content.Json字符串);
                    sb.Append("" + content.Json字符串 + ",");
                }
                if (listContent.Count != 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("],");
                JObject bo = JObject.Parse(试题内容Json字符串);
                string contentId = bo["ID"].ToString();
                试题内容Json字符串 = 试题内容Json字符串.Replace("\"ID\": " + contentId + ",", "\"ID\": " + contentId + "," + sb + "");

            }
            string 试题外部信息Json字符串 = 试题外部信息.转化成Json字符串();
            string json = "{\"content\":" + 试题内容Json字符串 + ",\"outside\":" + 试题外部信息Json字符串 + "}";
            return json;
        }



        public static string 转化成预览试题Json字符串不带答案(试题内容 试题内容, 试题外部信息 试题外部信息, out Dictionary<Guid, string> 子小题Json集合)
        {
            子小题Json集合 = new Dictionary<Guid, string>();
            StringBuilder sb = new StringBuilder();
            if (试题内容.小题型Enum > 39 && 试题内容.小题型Enum < 50 || 试题内容.小题型Enum == 80)
            {
                sb.Append("[");
                if (试题内容.小题型Enum > 39 && 试题内容.小题型Enum < 50)
                {
                    List<试题内容> subContentList = 试题内容查询.Where(a => a.父试题内容ID == 试题内容.ID)
                        .OrderBy(a => a.本题在复合题中顺序).ToList();
                    for (int i = 0; i < subContentList.Count; i++)
                    {
                        string sub试题内容Json字符串 = 试题内容.去掉答案(subContentList[i].Json字符串);
                        if (subContentList[i].小题型Enum == 30)
                        {
                            sub试题内容Json字符串 = "{\"每小题分值\":null,\"该题考试回答\":{\"连线题回答集合\": [{\"回答的左选项ID\":null,\"回答的右选项ID\":null}]},\"试题内容Json\":{\"content\":" + sub试题内容Json字符串 + ",\"outside\":null}}";
                        }
                        else
                        {
                            sub试题内容Json字符串 = "{\"每小题分值\":null,\"该题考试回答\":null,\"试题内容Json\":{\"content\":" + sub试题内容Json字符串 + ",\"outside\":null}}";
                        }
                        子小题Json集合.Add(subContentList[i].ID, sub试题内容Json字符串);
                        if (i == subContentList.Count - 1)
                        {

                            sb.Append(sub试题内容Json字符串);
                        }
                        else
                        {
                            sb.Append(sub试题内容Json字符串 + ",");
                        }
                    }

                }
                else
                {
                    List<题干> subContentList = 题干.题干查询.Where(a => a.父试题内容ID == 试题内容.ID)
                        .OrderBy(a => a.本题在复合题中顺序).ToList();
                    subContentList = 按选项组排序(subContentList);
                    for (int i = 0; i < subContentList.Count; i++)
                    {
                        string sub试题内容Json字符串 = 试题内容.去掉答案(subContentList[i].Json字符串);
                        sub试题内容Json字符串 = "{\"每小题分值\":null,\"该题考试回答\":null,\"试题内容Json\":{\"content\":" + sub试题内容Json字符串 + ",\"outside\":null}}";
                        子小题Json集合.Add(subContentList[i].ID, sub试题内容Json字符串);
                        if (i == subContentList.Count - 1)
                        {

                            sb.Append(sub试题内容Json字符串);
                        }
                        else
                        {
                            sb.Append(sub试题内容Json字符串 + ",");
                        }

                    }
                }
                sb.Append("]");
            }
            string 试题内容Json字符串 = 试题内容.转化成Json不带答案();
            string 试题外部信息Json字符串 = 试题外部信息.转化成Json字符串();
            string json = "{\"试题内容Json\":{\"content\":" + 试题内容Json字符串 + ",\"outside\":" + 试题外部信息Json字符串 + "}}";
            if (sb.Length == 0)
            {
                if (试题内容.小题型Enum == 30)
                {
                    json = json.Replace("{\"试题内容Json\"", "{\"子小题集合\":null,\"每小题分值\":null,\"该题考试回答\":{\"连线题回答集合\": [{\"回答的左选项ID\":null,\"回答的右选项ID\":null}]},\"试题内容Json\"");
                }
                else
                {
                    json = json.Replace("{\"试题内容Json\"", "{\"子小题集合\":null,\"每小题分值\":null,\"该题考试回答\":null,\"试题内容Json\"");
                }
            }
            else
            {
                json = json.Replace("{\"试题内容Json\"", "{\"子小题集合\":" + sb + ",\"每小题分值\":null,\"该题考试回答\":null,\"试题内容Json\"");
            }
            return json;
        }


        public static string 转化成预览试题Json字符串不带答案(试题内容 试题内容, 试题外部信息 试题外部信息)
        {
            StringBuilder sb = new StringBuilder();
            if (试题内容.小题型Enum > 39 && 试题内容.小题型Enum < 50 || 试题内容.小题型Enum == 80)
            {
                sb.Append("[");
                if (试题内容.小题型Enum > 39 && 试题内容.小题型Enum < 50)
                {
                    List<试题内容> subContentList = 试题内容查询.Where(a => a.父试题内容ID == 试题内容.ID)
                        .OrderBy(a => a.本题在复合题中顺序).ToList();
                    for (int i = 0; i < subContentList.Count; i++)
                    {
                        string sub试题内容Json字符串 = 试题内容.去掉答案(subContentList[i].Json字符串);
                        if (subContentList[i].小题型Enum == 30)
                        {
                            sub试题内容Json字符串 = "{\"每小题分值\":null,\"该题考试回答\":{\"连线题回答集合\": [{\"回答的左选项ID\":null,\"回答的右选项ID\":null}]},\"试题内容Json\":{\"content\":" + sub试题内容Json字符串 + ",\"outside\":null}}";
                        }
                        else
                        {
                            sub试题内容Json字符串 = "{\"每小题分值\":null,\"该题考试回答\":null,\"试题内容Json\":{\"content\":" + sub试题内容Json字符串 + ",\"outside\":null}}";
                        }
                        if (i == subContentList.Count - 1)
                        {

                            sb.Append(sub试题内容Json字符串);
                        }
                        else
                        {
                            sb.Append(sub试题内容Json字符串 + ",");
                        }
                    }

                }
                else
                {
                    List<题干> subContentList = 题干.题干查询.Where(a => a.父试题内容ID == 试题内容.ID)
                        .OrderBy(a => a.本题在复合题中顺序).ToList();
                    subContentList = 按选项组排序(subContentList);
                    for (int i = 0; i < subContentList.Count; i++)
                    {
                        string sub试题内容Json字符串 = 试题内容.去掉答案(subContentList[i].Json字符串);
                        sub试题内容Json字符串 = "{\"每小题分值\":null,\"该题考试回答\":null,\"试题内容Json\":{\"content\":" + sub试题内容Json字符串 + ",\"outside\":null}}";
                        if (i == subContentList.Count - 1)
                        {

                            sb.Append(sub试题内容Json字符串);
                        }
                        else
                        {
                            sb.Append(sub试题内容Json字符串 + ",");
                        }

                    }
                }
                sb.Append("]");
            }
            string 试题内容Json字符串 = 试题内容.转化成Json不带答案();
            string 试题外部信息Json字符串 = 试题外部信息.转化成Json字符串();
            string json = "{\"试题内容Json\":{\"content\":" + 试题内容Json字符串 + ",\"outside\":" + 试题外部信息Json字符串 + "}}";
            if (sb.Length == 0)
            {
                if (试题内容.小题型Enum == 30)
                {
                    json = json.Replace("{\"试题内容Json\"", "{\"子小题集合\":null,\"每小题分值\":null,\"该题考试回答\":{\"连线题回答集合\": [{\"回答的左选项ID\":null,\"回答的右选项ID\":null}]},\"试题内容Json\"");
                }
                else
                {
                    json = json.Replace("{\"试题内容Json\"", "{\"子小题集合\":null,\"每小题分值\":null,\"该题考试回答\":null,\"试题内容Json\"");
                }
            }
            else
            {
                json = json.Replace("{\"试题内容Json\"", "{\"子小题集合\":" + sb + ",\"每小题分值\":null,\"该题考试回答\":null,\"试题内容Json\"");
            }
            return json;
        }


        public static string 转化成预览试题Json字符串带答案(试题内容 试题内容, 试题外部信息 试题外部信息)
        {
            StringBuilder sb = new StringBuilder();
            if (试题内容.小题型Enum > 39 && 试题内容.小题型Enum < 50 || 试题内容.小题型Enum == 80)
            {
                sb.Append("[");
                List<string> listJson = new List<string>();
                if (试题内容.小题型Enum > 39 && 试题内容.小题型Enum < 50)
                {
                    listJson = 试题内容查询.Where(a => a.父试题内容ID == 试题内容.ID)
                        .OrderBy(a => a.本题在复合题中顺序).Select(a => a.Json字符串).ToList();
                }
                else
                {
                    List<题干> subContentList =题干.题干查询.Where(a => a.父试题内容ID == 试题内容.ID)
                        .OrderBy(a => a.本题在复合题中顺序).ToList();
                    subContentList = 按选项组排序(subContentList);
                    listJson = subContentList.Select(a => a.Json字符串).ToList();
                }

                for (int i = 0; i < listJson.Count; i++)
                {
                    string sub试题内容Json字符串 = listJson[i];
                    sub试题内容Json字符串 = "{\"每小题分值\":null,\"该题考试回答\":null,\"试题内容Json\":{\"content\":" + sub试题内容Json字符串 + ",\"outside\":null}}";
                    if (i == listJson.Count - 1)
                    {

                        sb.Append(sub试题内容Json字符串);
                    }
                    else
                    {
                        sb.Append(sub试题内容Json字符串 + ",");
                    }

                }
                sb.Append("]");
            }
            string 试题内容Json字符串 = 试题内容.Json字符串;
            string 试题外部信息Json字符串 = 试题外部信息.转化成Json字符串();
            string json = "{\"试题内容Json\":{\"content\":" + 试题内容Json字符串 + ",\"outside\":" + 试题外部信息Json字符串 + "}}";
            if (sb.Length == 0)
            {
                if (试题内容.小题型Enum == 30)
                {
                    json = json.Replace("{\"试题内容Json\"", "{\"子小题集合\":null,\"每小题分值\":null,\"该题考试回答\":{\"连线题回答集合\": [{\"回答的左选项ID\":null,\"回答的右选项ID\":null}]},\"试题内容Json\"");
                }
                else
                {
                    json = json.Replace("{\"试题内容Json\"", "{\"子小题集合\":null,\"每小题分值\":null,\"该题考试回答\":null,\"试题内容Json\"");
                }
            }
            else
            {
                json = json.Replace("{\"试题内容Json\"", "{\"子小题集合\":" + sb + ",\"每小题分值\":null,\"该题考试回答\":null,\"试题内容Json\"");
            }
            return json;
        }


     

        public static 试题内容 根据试题内容ID得到某个试题(Guid 试题内容ID, Guid 会员ID)
        {
            试题内容 content = 试题内容查询.Where(a => a.ID == 试题内容ID).FirstOrDefault();          
            #region
            switch (content.小题型Enum)
            {
                case 11:
                    {
                        单选题 single = 单选题.单选题查询.Where(a => a.ID == 试题内容ID).First();
                        return single;
                    }
                case 12:
                    {
                        多选题 multi = 多选题.多选题查询.Where(a => a.ID == 试题内容ID).First();
                        return multi;
                    }
                case 13:
                    {
                        填空题 fill = 填空题.填空题查询.Where(a => a.ID == 试题内容ID).First();
                        return fill;
                    }
                case 14:
                    {
                        选词填空 diction = 选词填空.选词填空查询.Where(a => a.ID == 试题内容ID).First();
                        return diction;
                    }
                case 15:
                    {
                        完形填空 cloze = 完形填空.完形填空查询.Where(a => a.ID == 试题内容ID).First();
                        return cloze;
                    }
                case 20:
                    {
                        判断题 judge = 判断题.判断题查询.Where(a => a.ID == 试题内容ID).First();
                        return judge;
                    }
                case 30:
                    {
                        连线题 link = 连线题.连线题查询.Where(a => a.ID == 试题内容ID).First();
                        return link;
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
                        复合题 combination =复合题.复合题查询.Where(a => a.ID == 试题内容ID).First();
                        return combination;
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
                        问答题 questionAnswer = 问答题.问答题查询.Where(a => a.ID == 试题内容ID).First();
                        return questionAnswer;
                    }

                case 80:
                    {
                        多题干共选项题 common = 多题干共选项题.多题干共选项题查询.Where(a => a.ID == 试题内容ID).First();
                        return common;
                    }
            }
            #endregion
            return content;
        }




        private static List<题干> 按选项组排序(List<题干> listContent)
        {
            List<题干> reListContent = new List<题干>();
            if (listContent.Count != 0)
            {
                reListContent.Add(listContent[0]);
                for (int i = 1; i < listContent.Count; i++)
                {
                    if (listContent[i].选项组ID == listContent[0].选项组ID)
                    {
                        reListContent.Add(listContent[i]);
                    }
                }
                listContent = listContent.Except(reListContent).ToList();
                if (listContent.Count > 0)
                {
                    List<题干> ll = 按选项组排序(listContent);
                    reListContent.AddRange(ll);
                }
            }
            return reListContent;
        }

        #endregion
    }
}
