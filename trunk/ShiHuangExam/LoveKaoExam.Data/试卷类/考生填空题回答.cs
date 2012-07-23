using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 考生填空题回答:考生考试回答
    {
        #region 属性

        public List<填空空格回答> 填空空格回答集合
        {
            get;
            set;
        }


        public static IQueryable<考生填空题回答> 考生填空题回答查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.考生考试回答表.Select(a => new 考生填空题回答
                    {
                        ID = a.ID,
                        该题的得分 = a.该题的得分,
                        该题的用时 = a.该题的用时,
                        考生做过的试卷ID = a.考生做过的试卷ID,
                        评语 = a.评语,
                        试卷大题中试题ID = a.试卷大题中试题ID
                    });
            }
        }

        #endregion



        #region 方法

        public override void 保存考生考试回答(LoveKaoExamEntities db)
        {
            考生考试回答表 memberTestAnswer = new 考生考试回答表();
            memberTestAnswer.ID = Guid.NewGuid();
            memberTestAnswer.该题的得分 = this.该题的得分;
            memberTestAnswer.该题的用时 = this.该题的用时;
            memberTestAnswer.考生做过的试卷ID = this.考生做过的试卷ID;
            memberTestAnswer.评语 = "";
            memberTestAnswer.试卷大题中试题ID = this.试卷大题中试题ID;
            for (int i = 0; i < this.填空空格回答集合.Count; i++)
            {
                考生填空题回答表 spaceAnswer = new 考生填空题回答表();
                spaceAnswer.ID = Guid.NewGuid();
                spaceAnswer.该空ID = this.填空空格回答集合[i].空格ID;
                spaceAnswer.回答的内容 = this.填空空格回答集合[i].回答的内容;
                memberTestAnswer.考生填空题回答表.Add(spaceAnswer);
            }
            db.考生考试回答表.AddObject(memberTestAnswer);
        }

        #endregion
    }
}
