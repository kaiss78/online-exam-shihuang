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
    public class 连线题:试题内容
    {
        #region 变量
        private List<连线题选项> leftChoice;
        private List<连线题选项> rightChoice;
        private List<连线题答案> linkAnswer;
        #endregion

        #region 属性

        public List<连线题选项> 连线题左选项集合
        {
            get
            {
                if (leftChoice == null)
                {
                    leftChoice = 连线题选项.连线题选项查询.Where(a => a.试题内容ID == this.ID && a.连线标记 == 0).OrderBy(a => a.顺序).ToList();
                    return leftChoice;
                }
                return leftChoice;
            }
            set
            {
                leftChoice = value;
            }
        }



        public List<连线题选项> 连线题右选项集合
        {
            get
            {
                if (rightChoice == null)
                {
                    rightChoice =连线题选项.连线题选项查询.Where(a => a.试题内容ID == this.ID && a.连线标记 == 1).OrderBy(a => a.顺序).ToList();
                    return rightChoice;
                }
                return rightChoice;
            }
            set
            {
                rightChoice = value;
            }
        }


  
        public List<连线题答案> 连线题答案集合
        {
            get
            {
                if (linkAnswer == null)
                {
                    linkAnswer = 连线题答案.连线题答案查询.Where(a => a.试题内容ID == this.ID).OrderBy(a => a.顺序).ToList();
                    return linkAnswer;
                }
                return linkAnswer;
            }
            set
            {
                linkAnswer = value;
            }
        }



        public static IQueryable<连线题> 连线题查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试题内容表.Where(a => a.小题型Enum == 30).Select(a => new 连线题
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
            StringBuilder sb = new StringBuilder();
            sb.Append(this.题干文本);
            List<string> choiceList = new List<string>();
            for (int i = 0; i < this.连线题左选项集合.Count; i++)
            {
                choiceList.Add(this.连线题左选项集合[i].选项内容文本);
            }
            for (int k = 0; k < this.连线题右选项集合.Count; k++)
            {
                choiceList.Add(this.连线题右选项集合[k].选项内容文本);
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
            string answer = bo["连线题答案集合"].ToString();
            answer = answer.Replace("\r\n", "\r\n  ");
            json = json.Replace("\"连线题答案集合\": " + answer + ",", "");
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

        public static 连线题 把Json转化成试题内容(string 试题Json字符串)
        {            
            JObject bo = JObject.Parse(试题Json字符串);
            string content = bo["content"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            连线题 link = jss.Deserialize<连线题>(content);
            return link;
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
            content.小题型Enum = 30;
            content.难易度 = this.难易度;

            for (int i = 0; i < this.连线题左选项集合.Count; i++)
            {
                连线题选项表 choice = new 连线题选项表();
                choice.ID = this.连线题左选项集合[i].ID;
                choice.连线标记 = 0;
                choice.顺序 = Convert.ToByte(i);
                choice.选项内容HTML = this.连线题左选项集合[i].选项内容HTML;
                choice.选项内容文本 = this.连线题左选项集合[i].选项内容文本;
                content.连线题选项表.Add(choice);
            }
            for (int j = 0; j < this.连线题右选项集合.Count; j++)
            {
                连线题选项表 rightChoice = new 连线题选项表();
                rightChoice.ID = this.连线题右选项集合[j].ID;
                rightChoice.连线标记 = 1;
                rightChoice.顺序 = Convert.ToByte(j);
                rightChoice.选项内容HTML = this.连线题右选项集合[j].选项内容HTML;
                rightChoice.选项内容文本 = this.连线题右选项集合[j].选项内容文本;
                content.连线题选项表.Add(rightChoice);
            }
            for (int k = 0; k < this.连线题答案集合.Count; k++)
            {
                连线题答案表 answer = new 连线题答案表();
                answer.ID = Guid.NewGuid();
                answer.顺序 = Convert.ToByte(k);
                answer.右边ID = this.连线题答案集合[k].连线题右选项ID;
                answer.左边ID = this.连线题答案集合[k].连线题左选项ID;
                content.连线题答案表.Add(answer);
            }
            content.Json字符串 = this.转化成Json带答案();
            content.爱考网ID = this.爱考网ID;
            return content;
        }



        public static void 给连线题选项赋值新ID(连线题 link)
        {
            foreach (连线题选项 choice in link.连线题左选项集合)
            {
                连线题答案 answer = link.连线题答案集合.Where(a => a.连线题左选项ID == choice.ID).FirstOrDefault();
                if (answer != null)
                {
                    choice.ID = Guid.NewGuid();
                    answer.连线题左选项ID = choice.ID;
                }
                else
                {
                    choice.ID = Guid.NewGuid();
                }
            }
            foreach (连线题选项 choice in link.连线题右选项集合)
            {
                连线题答案 answer = link.连线题答案集合.Where(a => a.连线题右选项ID == choice.ID).FirstOrDefault();
                if (answer != null)
                {
                    choice.ID = Guid.NewGuid();
                    answer.连线题右选项ID = choice.ID;
                }
                else
                {
                    choice.ID = Guid.NewGuid();
                }
            }
        }

        #endregion
    }
}
