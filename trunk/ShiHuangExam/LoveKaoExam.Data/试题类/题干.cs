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
    class 题干不带答案 : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            题干 common = (题干)value;

            JObject o = new JObject();
            o["ID"] = new JValue(common.ID.ToString());
            o["小题型Enum"] = new JValue(common.小题型Enum);
            o["题干HTML"] = new JValue(common.题干HTML);
            o["题干文本"] = new JValue(common.题干文本);
            o["试题外部信息ID"] = new JValue(common.试题外部信息ID.ToString());
            o["父试题内容ID"] = new JValue(common.父试题内容ID.ToString());
            o["本题在复合题中顺序"] = new JValue(common.本题在复合题中顺序);
            o["选项组ID"] = new JValue(common.选项组ID);
            o.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        {
            // convert back json string into array of picture 
            return null; ;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(题干);
        }
    }


    public class 题干 : 试题内容
    {
        #region 属性

        public Guid 答案ID
        {
            get;
            set;
        }


        public Guid 选项组ID
        {
            get;
            set;
        }


        public static IQueryable<题干> 题干查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from problemContent in db.试题内容表
                       join space in db.自由题空格表 on
                       problemContent.ID equals space.试题内容ID
                       join answer in db.自由题选项空格答案表
                       on space.ID equals answer.自由题空格ID
                       where problemContent.小题型Enum == 8011
                       select new 题干
                       {
                           ID = problemContent.ID,
                           Json字符串 = problemContent.Json字符串,
                           本题在复合题中顺序 = problemContent.本题在复合题中顺序,
                           操作人ID = problemContent.操作人ID,
                           操作时间 = problemContent.操作时间,
                           答案ID = answer.自由题选项ID,
                           父试题内容ID = problemContent.父试题内容ID,
                           解题思路 = problemContent.解题思路,
                           试题外部信息ID = problemContent.试题外部信息ID,
                           题干HTML = problemContent.题干HTML,
                           题干文本 = problemContent.题干文本,
                           小题型Enum = problemContent.小题型Enum,
                           选项组ID = space.自由题选项组ID.Value
                       };
            }
        }




        #endregion


        #region 方法

        public string 转化成Json带答案()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            json = json.Replace("\"小题型Enum\": " + 小题型Enum + "", "\"小题型Enum\":\"" + 小题型Enum + "\"");
            return json;
        }


        public string 转化成Json不带答案()
        {
            string json = JsonConvert.SerializeObject(this, new 题干不带答案());
            json = json.Replace("\"小题型Enum\":" + 小题型Enum + "", "\"小题型Enum\":\"" + 小题型Enum + "\"");
            return json;
        }


        public static 题干 把Json转化成试题内容(string 试题Json字符串)
        {           
            JObject bo = JObject.Parse(试题Json字符串);
            string content = bo["content"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            题干 single = jss.Deserialize<题干>(content);
            return single;
        }
        #endregion


       
    }
}
