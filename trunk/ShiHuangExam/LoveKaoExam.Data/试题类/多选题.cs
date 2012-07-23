using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Web.Script.Serialization;

namespace LoveKaoExam.Data
{
    public class 多选题:试题内容
    {
        #region 变量
        private List<选项> _选项列表;
        private List<Guid> _答案列表;
        #endregion

        #region 属性


        [JsonIgnore]
        public Guid 选项组ID
        {
            get;
            set;
        }


        public List<Guid> 答案列表
        {
            get
            {
                if (_答案列表 == null)
                {
                    LoveKaoExamEntities db = new LoveKaoExamEntities();
                    Guid spaceId = db.自由题空格表.Where(a => a.试题内容ID == this.ID).First().ID;
                    _答案列表 = db.自由题选项空格答案表.Where(a => a.自由题空格ID == spaceId).Select(a => a.自由题选项ID).ToList();
                    return _答案列表;
                }
                return _答案列表;
            }
            set
            {
                _答案列表 = value;
            }
        }


       

        public List<选项> 选项列表
        {
            get
            {
                if (_选项列表 == null)
                {
                    _选项列表 = 选项.选项查询.Where(a => a.选项组ID == 选项组ID).OrderBy(a => a.顺序).ToList();
                }
                return _选项列表;
            }
            set
            {
                _选项列表 = value;
            }
        }



        public static IQueryable<多选题> 多选题查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from problemContent in db.试题内容表
                       join space in db.自由题空格表 on
                       problemContent.ID equals space.试题内容ID
                       where problemContent.小题型Enum == 12
                       select new 多选题
                {
                    ID = problemContent.ID,
                    Json字符串 = problemContent.Json字符串,
                    本题在复合题中顺序 = problemContent.本题在复合题中顺序,
                    操作人ID = problemContent.操作人ID,
                    操作时间 = problemContent.操作时间,
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
            for (int i = 0; i < this.选项列表.Count; i++)
            {
                choiceList.Add(this.选项列表[i].选项内容文本);
            }
            choiceList.Sort();
            for (int j = 0; j < choiceList.Count; j++)
            {
                sb.Append(choiceList[j]);
            }
            return sb.ToString();
        }




        public override string 转化成Json不带答案()
        {
            string json = this.Json字符串;
            JObject bo = JObject.Parse(json);
            string answer = bo["答案列表"].ToString();
            answer = answer.Replace("\r\n", "\r\n  ");
            json = json.Replace("\"答案列表\": " + answer + ",", "");
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


        public static 多选题 把Json转化成试题内容(string 试题Json字符串)
        {          
            JObject bo = JObject.Parse(试题Json字符串);
            string content = bo["content"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            多选题 multi = jss.Deserialize<多选题>(content);
            return multi;
        }


        public override string 生成试题显示列()
        {
            return this.题干文本;
        }


        /// <summary>
        /// 界面无需使用此方法
        /// </summary>
        /// <returns></returns>
        public 试题内容表 类映射表赋值()
        {
            自由题选项组表 choiceGroup = new 自由题选项组表();
            choiceGroup.ID = Guid.NewGuid();
            for (int i = 0; i < this.选项列表.Count; i++)
            {
                自由题选项表 choice = new 自由题选项表();
                choice.ID = this.选项列表[i].ID;
                choice.选项内容HTML = this.选项列表[i].选项内容HTML;
                if (this.选项列表[i].选项内容文本.Length > 1024)
                {
                    this.选项列表[i].选项内容文本 = this.选项列表[i].选项内容文本.Substring(0, 1024);
                }
                choice.选项内容文本 = this.选项列表[i].选项内容文本;
                choice.顺序 = Convert.ToByte(i);
                choiceGroup.自由题选项表.Add(choice);
            }

            自由题空格表 space = new 自由题空格表();
            space.ID = Guid.NewGuid();
            space.空格类型 = 0;
            space.自由题选项组表 = choiceGroup;

            for (int j = 0; j < 答案列表.Count; j++)
            {
                自由题选项空格答案表 answer = new 自由题选项空格答案表();
                answer.ID = Guid.NewGuid();
                answer.自由题选项ID = 答案列表[j];
                space.自由题选项空格答案表.Add(answer);
            }

            试题内容表 content = new 试题内容表();
            content.ID = this.ID;
            content.操作人ID = this.操作人ID;
            content.操作时间 = DateTime.Now;
            content.解题思路 = this.解题思路;
            content.试题外部信息ID = this.试题外部信息ID;
            content.题干HTML = this.题干HTML;
            content.题干文本 = this.题干文本;
            content.小题型Enum = 12;
            content.Json字符串 = this.转化成Json带答案();
            content.爱考网ID = this.爱考网ID;
            content.难易度 = this.难易度;
            content.自由题空格表.Add(space);

            return content;
        }



        public static void 给多选题选项赋值新ID(多选题 multi)
        {
            List<Guid> listAnswerId = new List<Guid>();
            foreach (选项 choice in multi.选项列表)
            {
                if (multi.答案列表.Contains(choice.ID))
                {
                    choice.ID = Guid.NewGuid();
                    listAnswerId.Add(choice.ID);
                }
                else
                {
                    choice.ID = Guid.NewGuid();
                }
            }
            multi.答案列表 = listAnswerId;
        }
    
        #endregion
    }
}
