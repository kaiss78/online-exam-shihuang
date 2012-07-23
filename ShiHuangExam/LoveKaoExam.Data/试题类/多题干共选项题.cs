using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace LoveKaoExam.Data
{
    class 多题干共选项题不带答案 : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            多题干共选项题 common = (多题干共选项题)value;

            JObject o = new JObject();
            o["ID"] = new JValue(common.ID.ToString());
            o["小题型Enum"] = new JValue(common.小题型Enum);
            o["题干HTML"] = new JValue(common.题干HTML);
            o["题干文本"] = new JValue(common.题干文本);
            o["试题外部信息ID"] = new JValue(common.试题外部信息ID.ToString());
            o["选项组集合"] = new JValue(common.选项组集合);
            o.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        {
            // convert back json string into array of picture 
            return null; ;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(多题干共选项题);
        }
    }




    public class 多题干共选项题 : 试题内容
    {
        #region 变量
        private List<题干> _子小题集合;
        private List<选项组> _选项组集合;
        #endregion

        #region 属性

        public List<选项组> 选项组集合
        {
            get
            {
                if (_选项组集合 == null)
                {
                    List<Guid> groupIdList = this.子小题集合.GroupBy(a => a.选项组ID).Select(a => a.Key).ToList();
                    _选项组集合 = 选项组.选项组查询.Where(a => groupIdList.Contains<Guid>(a.ID)).ToList();
                    return _选项组集合;
                }
                return _选项组集合;
            }
            set
            {
                _选项组集合 = value;
            }
        }


        public List<题干> 子小题集合
        {
            get
            {
                if (_子小题集合 == null)
                {
                    _子小题集合 = 题干.题干查询.Where(a => a.父试题内容ID == this.ID).OrderBy(a => a.本题在复合题中顺序).ToList();
                    return _子小题集合;
                }
                return _子小题集合;
            }
            set
            {
                _子小题集合 = value;
            }
        }


        public static IQueryable<多题干共选项题> 多题干共选项题查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试题内容表.Where(a => a.小题型Enum == 80).Select(a => new 多题干共选项题
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
            content.爱考网ID = this.爱考网ID;
            content.难易度 = this.难易度;

            for (int i = 0; i < this.子小题集合.Count; i++)
            {
                this.子小题集合[i].本题在复合题中顺序 = Convert.ToByte(i);
                this.子小题集合[i].ID = Guid.NewGuid();
            }
            var subContentGroup = this.子小题集合.GroupBy(a => a.选项组ID).ToList();
            for (int j = 0; j < subContentGroup.Count; j++)
            {
                自由题选项组表 dbGroup = new 自由题选项组表();
                dbGroup.ID = subContentGroup[j].Key;
                选项组 newGroup = new 选项组();
                foreach (选项组 group in this.选项组集合)
                {
                    if (subContentGroup[j].Key == group.ID)
                    {
                        newGroup = group;
                    }
                }
                for (int k = 0; k < newGroup.选项集合.Count; k++)
                {
                    自由题选项表 choice = new 自由题选项表();
                    choice.ID = newGroup.选项集合[k].ID;
                    choice.顺序 = Convert.ToByte(k);
                    choice.选项内容HTML = newGroup.选项集合[k].选项内容HTML;
                    choice.选项内容文本 = newGroup.选项集合[k].选项内容文本;
                    dbGroup.自由题选项表.Add(choice);
                }

                subContentGroup[j].ElementAt(0).父试题内容ID = content.ID;
                试题内容表 dbSubContent = new 试题内容表();
                dbSubContent.ID = subContentGroup[j].ElementAt(0).ID;
                dbSubContent.操作人ID = this.操作人ID;
                dbSubContent.操作时间 = DateTime.Now;
                dbSubContent.解题思路 = subContentGroup[j].ElementAt(0).解题思路;
                dbSubContent.试题外部信息ID = this.试题外部信息ID;
                dbSubContent.题干HTML = subContentGroup[j].ElementAt(0).题干HTML;
                dbSubContent.题干文本 = subContentGroup[j].ElementAt(0).题干文本;
                dbSubContent.小题型Enum = subContentGroup[j].ElementAt(0).小题型Enum;
                dbSubContent.本题在复合题中顺序 = subContentGroup[j].ElementAt(0).本题在复合题中顺序;
                dbSubContent.父试题内容ID = content.ID;
                dbSubContent.Json字符串 = subContentGroup[j].ElementAt(0).转化成Json带答案();
                dbSubContent.爱考网ID = subContentGroup[j].ElementAt(0).爱考网ID;

                自由题选项空格答案表 answer = new 自由题选项空格答案表();
                answer.ID = Guid.NewGuid();
                answer.自由题选项ID = subContentGroup[j].ElementAt(0).答案ID;

                自由题空格表 space = new 自由题空格表();
                space.ID = Guid.NewGuid();
                space.空格类型 = 0;
                space.顺序 = 0;
                space.自由题选项组表 = dbGroup;
                space.自由题选项空格答案表.Add(answer);
                dbSubContent.自由题空格表.Add(space);
                content.试题内容表1.Add(dbSubContent);


                for (int m = 1; m < subContentGroup[j].Count(); m++)
                {
                    subContentGroup[j].ElementAt(m).父试题内容ID = content.ID;
                    试题内容表 dbSubContent1 = new 试题内容表();
                    dbSubContent1.ID = subContentGroup[j].ElementAt(m).ID;
                    dbSubContent1.操作人ID = this.操作人ID;
                    dbSubContent1.操作时间 = DateTime.Now;
                    dbSubContent1.解题思路 = subContentGroup[j].ElementAt(m).解题思路;
                    dbSubContent1.试题外部信息ID = this.试题外部信息ID;
                    dbSubContent1.题干HTML = subContentGroup[j].ElementAt(m).题干HTML;
                    dbSubContent1.题干文本 = subContentGroup[j].ElementAt(m).题干文本;
                    dbSubContent1.小题型Enum = subContentGroup[j].ElementAt(m).小题型Enum;
                    dbSubContent1.本题在复合题中顺序 = subContentGroup[j].ElementAt(m).本题在复合题中顺序;
                    dbSubContent1.父试题内容ID = content.ID;
                    dbSubContent1.Json字符串 = subContentGroup[j].ElementAt(m).转化成Json带答案();
                    dbSubContent1.爱考网ID = subContentGroup[j].ElementAt(m).爱考网ID;

                    自由题选项空格答案表 answer1 = new 自由题选项空格答案表();
                    answer1.ID = Guid.NewGuid();
                    answer1.自由题选项ID = subContentGroup[j].ElementAt(m).答案ID;

                    自由题空格表 space1 = new 自由题空格表();
                    space1.ID = Guid.NewGuid();
                    space1.空格类型 = 0;
                    space1.顺序 = 0;
                    space1.自由题选项组ID = newGroup.ID;
                    space1.自由题选项空格答案表.Add(answer1);
                    dbSubContent1.自由题空格表.Add(space1);
                    content.试题内容表1.Add(dbSubContent1);
                }
            }
            content.Json字符串 = this.转化成Json带答案();
            db.试题内容表.AddObject(content);
            db.SaveChanges();
        }


        public override string 生成查询内容()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.题干文本);
            List<string> list = new List<string>();
            for (int i = 0; i < this.子小题集合.Count; i++)
            {
                list.Add(this.子小题集合[i].题干文本);
            }
            for (int j = 0; j < this.选项组集合.Count; j++)
            {
                foreach (选项 choice in this.选项组集合[j].选项集合)
                {
                    list.Add(choice.选项内容文本);
                }
            }
            list.Sort();
            for (int k = 0; k < list.Count; k++)
            {
                sb.Append(list[k]);
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
            return json;
        }


        public override string 转化成Json带答案()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            if (this.子小题集合.Count != 0)
            {
                json = json.Replace("\"小题型Enum\": " + this.子小题集合[0].小题型Enum + "", "\"小题型Enum\": \"" + this.子小题集合[0].小题型Enum + "\"");
            }
            json = json.Replace("\"小题型Enum\": " + 小题型Enum + "", "\"小题型Enum\": \"" + 小题型Enum + "\"");
            JObject bo = JObject.Parse(json);
            string 题干集合 = bo["子小题集合"].ToString();
            题干集合 = 题干集合.Replace("\r\n", "\r\n  ");
            json = json.Replace("\"子小题集合\": " + 题干集合 + ",", "");
            return json;
        }


        public static 多题干共选项题 把Json转化成试题内容(string 试题Json字符串)
        {
            JObject bo = JObject.Parse(试题Json字符串);
            string content = bo["content"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            多题干共选项题 common = jss.Deserialize<多题干共选项题>(content);
            return common;
        }



       

        public static void 给多题干共选项题子小题内容和选项赋值新ID(多题干共选项题 common)
        {
            foreach (题干 content in common.子小题集合)
            {
                Guid contentLoveKaoId = content.ID;
                content.ID = Guid.NewGuid();
                content.爱考网ID = contentLoveKaoId;
            }
            foreach (选项组 group in common.选项组集合)
            {
                foreach (选项 choice in group.选项集合)
                {
                    题干 content = common.子小题集合.Where(a => a.答案ID == choice.ID).FirstOrDefault();
                    if (content != null)
                    {
                        choice.ID = Guid.NewGuid();
                        content.答案ID = choice.ID;
                    }
                    else
                    {
                        choice.ID = Guid.NewGuid();
                    }
                }
            }
        }
        #endregion

    }
}
