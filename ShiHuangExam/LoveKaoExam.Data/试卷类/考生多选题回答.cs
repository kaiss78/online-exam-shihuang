using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 考生多选题回答:考生考试回答
    {
        #region 变量

        private List<Guid> _回答的选项ID集合;

        #endregion




        #region 属性

        public List<Guid> 回答的选项ID集合
        {
            get
            {
                if (_回答的选项ID集合 == null)
                {
                    _回答的选项ID集合 = 多选题回答.多选题回答查询.Where(a => a.考生考试回答ID == this.ID)
                        .Select(a => a.回答的选项ID).ToList();
                    return _回答的选项ID集合;
                }
                return _回答的选项ID集合;
            }
            set
            {
                _回答的选项ID集合 = value;
            }
        }


        public static IQueryable<考生多选题回答> 考生多选题回答查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.考生考试回答表.Select(a => new 考生多选题回答
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
            for (int i = 0; i < this.回答的选项ID集合.Count; i++)
            {
                考生多选题回答表 multiAnswer = new 考生多选题回答表();
                multiAnswer.ID = Guid.NewGuid();
                multiAnswer.回答的选项ID = this.回答的选项ID集合[i];
                memberTestAnswer.考生多选题回答表.Add(multiAnswer);
            }
            db.考生考试回答表.AddObject(memberTestAnswer);
        }
        #endregion
    }
}
