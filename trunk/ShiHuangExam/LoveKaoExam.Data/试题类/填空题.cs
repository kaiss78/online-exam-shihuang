using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Script.Serialization;

namespace LoveKaoExam.Data
{
    class 填空题不带答案 : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            填空题 fill = (填空题)value;

            JObject o = new JObject();
            o["ID"] = new JValue(fill.ID.ToString());
            o["小题型Enum"] = new JValue(fill.小题型Enum);
            o["题干HTML"] = new JValue(fill.题干HTML);
            o["题干文本"] = new JValue(fill.题干文本);


            o.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        {
            // convert back json string into array of picture 
            return null; ;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(填空题);
        }
    }

    public class 填空题 : 试题内容
    {

        #region 变量
        private List<填空空格> _填空空格集合;
        #endregion

        #region 属性

        public List<填空空格> 填空空格集合
        {
            get
            {
                if (_填空空格集合 == null)
                {
                    _填空空格集合 = 填空空格.填空空格查询.Where(a => a.试题内容ID == this.ID).OrderBy(a => a.顺序).ToList();
                    return _填空空格集合;
                }
                return _填空空格集合;
            }
            set
            {
                _填空空格集合 = value;
            }
        }



        public static IQueryable<填空题> 填空题查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试题内容表.Where(a => a.小题型Enum == 13).Select(a => new 填空题
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
            试题内容表 content = this.类映射表赋值();
            db.试题内容表.AddObject(content);
            db.SaveChanges();
        }






        public override string 生成查询内容()
        {
            return this.题干文本;
        }


        public override string 生成试题显示列()
        {
            return this.题干文本;
        }


        public override string 转化成Json不带答案()
        {          
            string json = this.Json字符串;
            JObject bo = JObject.Parse(json);
            for (int i = 0; i < this.填空空格集合.Count; i++)
            {
                string answerListString = bo["填空空格集合"][i]["填空空格答案集合"].ToString();
                answerListString = answerListString.Replace("\r\n", "\r\n      ");
                //复合题中顺序有可能会变，所以替换2中情况
                json = json.Replace(",\r\n      \"填空空格答案集合\": " + answerListString + "", "");
                json = json.Replace("\"填空空格答案集合\": " + answerListString + ",", "");
            }
            string idea = bo["解题思路"].ToString();
            json = json.Replace("\"解题思路\": " + idea + ",", "");
            return json;
        }


        public override string 转化成Json带答案()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            json = json.Replace("\"小题型Enum\": " + 小题型Enum + "", "\"小题型Enum\": \"" + 小题型Enum + "\"");
            return json;
        }


        public static 填空题 把Json转化成试题内容(string 试题Json字符串)
        {          
            JObject bo = JObject.Parse(试题Json字符串);
            string content = bo["content"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            填空题 fill = jss.Deserialize<填空题>(content);
            return fill;
        }



        /// <summary>
        /// 界面无需使用此方法
        /// </summary>
        /// <returns></returns>
        public 试题内容表 类映射表赋值()
        {
            试题内容表 content = new 试题内容表();
            content.ID = this.ID;
            content.操作人ID = this.操作人ID;
            content.操作时间 = DateTime.Now;
            content.解题思路 = this.解题思路;
            content.试题外部信息ID = this.试题外部信息ID;
            content.题干HTML = this.题干HTML;
            content.题干文本 = this.题干文本;
            content.小题型Enum = 13;
            content.难易度 = this.难易度;

            for (int i = 0; i < this.填空空格集合.Count; i++)
            {
                this.填空空格集合[i].ID = Guid.NewGuid();
                自由题空格表 space = new 自由题空格表();
                space.ID = this.填空空格集合[i].ID;
                space.空格类型 = 2;
                space.顺序 = Convert.ToByte(i);
                for (int j = 0; j < this.填空空格集合[i].填空空格答案集合.Count; j++)
                {
                    自由题填空答案表 answer = new 自由题填空答案表();
                    answer.ID = Guid.NewGuid();
                    answer.该空答案 = this.填空空格集合[i].填空空格答案集合[j].答案内容;
                    answer.顺序 = Convert.ToByte(j);
                    space.自由题填空答案表.Add(answer);
                }
                content.自由题空格表.Add(space);
            }
            content.Json字符串 = this.转化成Json带答案();
            content.爱考网ID = this.爱考网ID;
            return content;
        }

        #endregion

       

    }
}
