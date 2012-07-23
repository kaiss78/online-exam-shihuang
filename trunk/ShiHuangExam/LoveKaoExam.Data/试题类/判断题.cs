using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace LoveKaoExam.Data
{
    public class 判断题:试题内容
    {
        #region 属性

        /// <summary>
        /// true正确，false错误
        /// </summary>
        public bool 答案
        {
            get;
            set;
        }



        public static IQueryable<判断题> 判断题查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from problemContent in db.试题内容表
                       join judgeAnswer in db.判断题答案表
                       on problemContent.ID equals judgeAnswer.试题内容ID
                       where problemContent.小题型Enum == 20
                       select new 判断题
                       {
                           ID = problemContent.ID,
                           Json字符串 = problemContent.Json字符串,
                           本题在复合题中顺序 = problemContent.本题在复合题中顺序,
                           操作人ID = problemContent.操作人ID,
                           操作时间 = problemContent.操作时间,
                           答案 = judgeAnswer.答案,
                           父试题内容ID = problemContent.父试题内容ID,
                           解题思路 = problemContent.解题思路,
                           试题外部信息ID = problemContent.试题外部信息ID,
                           题干HTML = problemContent.题干HTML,
                           题干文本 = problemContent.题干文本,
                           小题型Enum = problemContent.小题型Enum
                       };
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
            if (this.答案 == true)
            {
                json = json.Replace("\"答案\": true,", "");
            }
            else
            {
                json = json.Replace("\"答案\": false,", "");
            }
            json = json.Replace("\"解题思路\": \"" + this.解题思路 + "\",", "");
            json = json.Replace("\"小题型Enum\": " + 小题型Enum + "", "\"小题型Enum\":\"" + 小题型Enum + "\"");
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


        public static 判断题 把Json转化成试题内容(string 试题Json字符串)
        {           
            JObject bo = JObject.Parse(试题Json字符串);
            string content = bo["content"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            判断题 judge = jss.Deserialize<判断题>(content);
            return judge;
        }



        /// <summary>
        /// 界面无需使用此方法
        /// </summary>
        /// <returns></returns>
        public 试题内容表 类映射表赋值()
        {
            判断题答案表 answer = new 判断题答案表();
            answer.答案 = this.答案;

            试题内容表 content = new 试题内容表();
            content.ID = this.ID;
            content.操作人ID = this.操作人ID;
            content.操作时间 = DateTime.Now;
            content.解题思路 = this.解题思路;
            content.试题外部信息ID = this.试题外部信息ID;
            content.题干HTML = this.题干HTML;
            content.题干文本 = this.题干文本;
            content.小题型Enum = 20;
            content.判断题答案表 = answer;
            content.Json字符串 = this.转化成Json带答案();
            content.爱考网ID = this.爱考网ID;
            content.难易度 = this.难易度;

            return content;
        }

        #endregion

    }
}
