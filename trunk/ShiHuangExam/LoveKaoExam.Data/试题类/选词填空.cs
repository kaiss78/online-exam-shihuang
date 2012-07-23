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
    public class 选词填空:试题内容
    {
        #region 变量
        private List<选词空格> _选词空格集合;
        private 选项组 _选项组;
        #endregion


        #region 属性

        public List<选词空格> 选词空格集合
        {
            get
            {
                if (_选词空格集合 == null)
                {
                    _选词空格集合 = 选词空格.选词空格查询.Where(a => a.试题内容ID == this.ID).OrderBy(a => a.顺序).ToList();
                    return _选词空格集合;
                }
                return _选词空格集合;
            }
            set
            {
                _选词空格集合 = value;
            }
        }

        public 选项组 选项组
        {
            get
            {
                if (_选项组 == null)
                {
                    Guid groupId = 单选空格.单选空格查询.Where(a => a.试题内容ID == this.ID).First().选项组ID;
                    _选项组 = 选项组.选项组查询.Where(a => a.ID == groupId).First();
                    return _选项组;
                }
                return _选项组;
            }
            set
            {
                _选项组 = value;
            }
        }


        public static IQueryable<选词填空> 选词填空查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.试题内容表.Where(a => a.小题型Enum == 14).Select(a => new 选词填空
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

        public override string 转化成Json带答案()
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, iso);
            json = json.Replace("\"小题型Enum\": " + 小题型Enum + "", "\"小题型Enum\":\"" + 小题型Enum + "\"");
            return json;
        }


        public override string 转化成Json不带答案()
        {
            string json = this.Json字符串;
            JObject bo = JObject.Parse(json);
            for (int i = 0; i < this.选词空格集合.Count; i++)
            {
                string answer = bo["选词空格集合"][i]["答案ID"].ToString();
                //复合题中顺序有可能会变，所以替换2中情况
                json = json.Replace(",\r\n      \"答案ID\": " + answer + "", "");
                json = json.Replace("\"答案ID\": " + answer + ",", "");
            }
            string idea = bo["解题思路"].ToString();
            json = json.Replace("\"解题思路\": " + idea + ",", "");
            return json;
        }





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
            for (int i = 0; i < this.选项组.选项集合.Count; i++)
            {
                choiceList.Add(this.选项组.选项集合[i].选项内容文本);
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


        public static 选词填空 把Json转化成试题内容(string 试题Json字符串)
        {          
            JObject bo = JObject.Parse(试题Json字符串);
            string content = bo["content"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            选词填空 diction = jss.Deserialize<选词填空>(content);
            return diction;
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
            problemContent.小题型Enum = 14;
            problemContent.难易度 = this.难易度;

            自由题选项组表 group = new 自由题选项组表();
            group.ID = Guid.NewGuid();
            for (int i = 0; i < this.选项组.选项集合.Count; i++)
            {
                自由题选项表 choice = new 自由题选项表();
                choice.ID = this.选项组.选项集合[i].ID;
                choice.顺序 = Convert.ToByte(i);
                choice.选项内容HTML = this.选项组.选项集合[i].选项内容HTML;
                choice.选项内容文本 = this.选项组.选项集合[i].选项内容文本;
                group.自由题选项表.Add(choice);
            }
            自由题选项空格答案表 answer = new 自由题选项空格答案表();
            answer.ID = Guid.NewGuid();
            answer.自由题选项ID = this.选词空格集合[0].答案ID;

            this.选词空格集合[0].ID = Guid.NewGuid();
            自由题空格表 space = new 自由题空格表();
            space.ID = this.选词空格集合[0].ID;
            space.空格类型 = 0;
            space.顺序 = 0;
            space.自由题选项组表 = group;
            space.自由题选项空格答案表.Add(answer);
            problemContent.自由题空格表.Add(space);

            for (int j = 1; j < this.选词空格集合.Count; j++)
            {
                this.选词空格集合[j].ID = Guid.NewGuid();
                自由题选项空格答案表 answer1 = new 自由题选项空格答案表();
                answer1.ID = Guid.NewGuid();
                answer1.自由题选项ID = this.选词空格集合[j].答案ID;

                自由题空格表 space1 = new 自由题空格表();
                space1.ID = this.选词空格集合[j].ID;
                space1.空格类型 = 0;
                space1.顺序 = Convert.ToByte(j);
                space1.自由题选项组ID = group.ID;
                space1.自由题选项空格答案表.Add(answer1);
                problemContent.自由题空格表.Add(space1);
            }
            problemContent.Json字符串 = this.转化成Json带答案();
            problemContent.爱考网ID = this.爱考网ID;
            return problemContent;
        }



        public static void 给选词填空选项赋值新ID(选词填空 diction)
        {
            foreach (选项 choice in diction.选项组.选项集合)
            {
                选词空格 space = diction.选词空格集合.Where(a => a.答案ID == choice.ID).FirstOrDefault();
                if (space != null)
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

        #endregion
    }
}
