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
    public class 完形填空:试题内容
    {
        #region 变量
        private List<单选空格> _单选空格集合;
        #endregion

        #region 属性

        public List<单选空格> 单选空格集合
        {
            get
            {
                if (_单选空格集合 == null)
                {
                    _单选空格集合 = 单选空格.单选空格查询.Where(a => a.试题内容ID == this.ID).OrderBy(a => a.顺序).ToList();
                    return _单选空格集合;
                }
                return _单选空格集合;
            }
            set
            {
                _单选空格集合 = value;
            }
        }


        public static IQueryable<完形填空> 完形填空查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试题内容表.Where(a => a.小题型Enum == 15).Select(a => new 完形填空
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
            试题内容表 problemContent = this.类映射表赋值();
            db.试题内容表.AddObject(problemContent);
            db.SaveChanges();
        }



       
        public override string 生成查询内容()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.题干文本);
            List<string> choiceList = new List<string>();
            for (int i = 0; i < this.单选空格集合.Count; i++)
            {
                for (int m = 0; m < this.单选空格集合[i].选项组.选项集合.Count; m++)
                {
                    choiceList.Add(this.单选空格集合[i].选项组.选项集合[m].选项内容文本);
                }
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
            for (int i = 0; i < bo["单选空格集合"].Count(); i++)
            {
                string answer = bo["单选空格集合"][i]["答案ID"].ToString();
                json = json.Replace(",\r\n      \"答案ID\": " + answer + "", "");
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
            json = json.Replace("\"小题型Enum\": " + 小题型Enum + "", "\"小题型Enum\":\"" + 小题型Enum + "\"");
            return json;
        }


        public static 完形填空 把Json转化成试题内容(string 试题Json字符串)
        {           
            JObject bo = JObject.Parse(试题Json字符串);
            string content = bo["content"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            完形填空 cloze = jss.Deserialize<完形填空>(content);
            return cloze;
        }



        /// <summary>
        /// 界面无需使用此方法
        /// </summary>
        /// <returns></returns>
        public 试题内容表 类映射表赋值()
        {
            试题内容表 problemContent = new 试题内容表();
            problemContent.ID = this.ID;
            problemContent.操作人ID = this.操作人ID;
            problemContent.操作时间 = DateTime.Now;
            problemContent.解题思路 = this.解题思路;
            problemContent.试题外部信息ID = this.试题外部信息ID;
            problemContent.题干HTML = this.题干HTML;
            problemContent.题干文本 = this.题干文本;
            problemContent.小题型Enum = 15;
            problemContent.难易度 = this.难易度;

            for (int i = 0; i < this.单选空格集合.Count; i++)
            {
                this.单选空格集合[i].ID = Guid.NewGuid();
                自由题选项组表 choiceGroup = new 自由题选项组表();
                choiceGroup.ID = Guid.NewGuid();
                for (int j = 0; j < this.单选空格集合[i].选项组.选项集合.Count; j++)
                {
                    自由题选项表 choice = new 自由题选项表();
                    choice.ID = this.单选空格集合[i].选项组.选项集合[j].ID;
                    choice.顺序 = Convert.ToByte(j);
                    choice.选项内容HTML = this.单选空格集合[i].选项组.选项集合[j].选项内容HTML;
                    choice.选项内容文本 = this.单选空格集合[i].选项组.选项集合[j].选项内容文本;
                    choiceGroup.自由题选项表.Add(choice);
                }
                自由题选项空格答案表 answer = new 自由题选项空格答案表();
                answer.ID = Guid.NewGuid();
                answer.顺序 = 0;
                answer.自由题选项ID = this.单选空格集合[i].答案ID;

                自由题空格表 space = new 自由题空格表();
                space.ID = this.单选空格集合[i].ID;
                space.空格类型 = 0;
                space.顺序 = Convert.ToByte(i);
                space.自由题选项组表 = choiceGroup;
                space.自由题选项空格答案表.Add(answer);
                problemContent.自由题空格表.Add(space);
            }
            problemContent.Json字符串 = this.转化成Json带答案();
            problemContent.爱考网ID = this.爱考网ID;
            return problemContent;
        }



        public static void 给完形填空选项赋值新ID(完形填空 cloze)
        {
            foreach (单选空格 space in cloze.单选空格集合)
            {
                foreach (选项 choice in space.选项组.选项集合)
                {
                    if (choice.ID == space.答案ID)
                    {
                        choice.ID = Guid.NewGuid();
                        space.答案ID = choice.ID;
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
