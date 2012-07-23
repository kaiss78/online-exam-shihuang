using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 考生单选题回答:考生考试回答
    {
        #region 属性
   
        public Guid 回答的选项ID
        {
            get;
            set;
        }


        public static IQueryable<考生单选题回答> 考生单选题回答查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from answer in db.考生考试回答表
                       join singleAnswer in db.考生单选题回答表
                       on answer.ID equals singleAnswer.ID
                       select new 考生单选题回答
                       {
                           ID = answer.ID,
                           该题的得分 = answer.该题的得分,
                           该题的用时 = answer.该题的用时,
                           考生做过的试卷ID = answer.考生做过的试卷ID,
                           评语 = answer.评语,
                           试卷大题中试题ID = answer.试卷大题中试题ID,
                           回答的选项ID = singleAnswer.回答的选项ID
                       };
            }
        }

        #endregion


        #region 方法

        public override void 保存考生考试回答(LoveKaoExamEntities db)
        {
            考生单选题回答表 singleAnswer = new 考生单选题回答表();
            singleAnswer.回答的选项ID = this.回答的选项ID;

            考生考试回答表 memberTestAnswer = new 考生考试回答表();
            memberTestAnswer.ID = Guid.NewGuid();
            memberTestAnswer.该题的得分 = this.该题的得分;
            memberTestAnswer.该题的用时 = this.该题的用时;
            memberTestAnswer.考生做过的试卷ID = this.考生做过的试卷ID;
            memberTestAnswer.评语 = "";
            memberTestAnswer.试卷大题中试题ID = this.试卷大题中试题ID;
            memberTestAnswer.考生单选题回答表 = singleAnswer;
            db.考生考试回答表.AddObject(memberTestAnswer);
        }
        #endregion
    }
}
