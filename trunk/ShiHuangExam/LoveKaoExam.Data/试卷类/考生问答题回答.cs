using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 考生问答题回答:考生考试回答
    {
        #region 属性
       
        public string 回答答案
        {
            get;
            set;
        }



        public static IQueryable<考生问答题回答> 考生问答题回答查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from answer in db.考生考试回答表
                       join questionAnswer in db.考生问答题回答表
                       on answer.ID equals questionAnswer.ID
                       select new 考生问答题回答
                       {
                           ID = answer.ID,
                           该题的得分 = answer.该题的得分,
                           该题的用时 = answer.该题的用时,
                           考生做过的试卷ID = answer.考生做过的试卷ID,
                           评语 = answer.评语,
                           试卷大题中试题ID = answer.试卷大题中试题ID,
                           回答答案 = questionAnswer.回答的内容
                       };
            }
        }

        #endregion


        #region 方法

        public override void 保存考生考试回答(LoveKaoExamEntities db)
        {
            考生问答题回答表 questionAnswer = new 考生问答题回答表();
            questionAnswer.回答的内容 = this.回答答案;

            考生考试回答表 memberTestAnswer = new 考生考试回答表();
            memberTestAnswer.ID = Guid.NewGuid();
            memberTestAnswer.该题的得分 = this.该题的得分;
            memberTestAnswer.该题的用时 = this.该题的用时;
            memberTestAnswer.考生做过的试卷ID = this.考生做过的试卷ID;
            memberTestAnswer.评语 = "";
            memberTestAnswer.试卷大题中试题ID = this.试卷大题中试题ID;
            memberTestAnswer.考生问答题回答表 = questionAnswer;
            db.考生考试回答表.AddObject(memberTestAnswer);
        }

        #endregion
    }
}
