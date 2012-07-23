using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 批改试卷
    {
       
        #region 方法

        public static void 提交保存批改(string 批改Json)
        {
            考生做过的试卷 doneTest = 考生做过的试卷.把Json转化成考生做过的试卷(批改Json);
            List<试卷大题中试题> listTestProblem=new List<试卷大题中试题>();
            foreach(试卷中大题 testType in doneTest.考试设置.试卷内容.试卷中大题集合)
            {
                listTestProblem.AddRange(testType.试卷大题中试题集合);
            }

            LoveKaoExamEntities db = new LoveKaoExamEntities();
            考生做过的试卷表 memberDoneTest = db.考生做过的试卷表.FirstOrDefault(a=>a.ID==doneTest.ID);      
            //更新考生得分
            List<考生考试回答表> listMemberAnswer = db.考生考试回答表.Where(a => a.考生做过的试卷ID == doneTest.ID)
                .ToList();
            foreach (考生考试回答表 memberAnswer in listMemberAnswer)
            {
                memberAnswer.该题的得分 = listTestProblem.Where(a => a.ID == memberAnswer.试卷大题中试题ID).First()
                    .该题考试回答.该题的得分;
            }
            //更新批改状态
            memberDoneTest.批改状态 = doneTest.批改状态类型;
            if (doneTest.批改状态类型>0)
            {
                memberDoneTest.批改时间 = DateTime.Now;
            }
            //计算客观题总得分
            //List<Guid> listContentId = listTestProblem.Select(a => a.试题内容ID).ToList();
            //List<Guid> listObjectiveContentId = db.试题内容表.Where(a => listContentId.Contains(a.ID)
            //    && (a.小题型Enum < 31 || a.小题型Enum == 8011)).Select(a => a.ID).ToList();
            //memberDoneTest.客观题得分 = listTestProblem.Where(a => listObjectiveContentId.Contains(a.试题内容ID))
            //    .Sum(a => a.该题考试回答.该题的得分.Value);
            List<Guid> listTestProblemID = listMemberAnswer.Select(a => a.试卷大题中试题ID).ToList();
            List<试卷大题中试题表> listTestProblemTable = db.试卷大题中试题表.Where(a => listTestProblemID.Contains(a.ID))
                .ToList();
            List<Guid> listProblemContentID = listTestProblemTable.Select(a => a.试题内容ID).ToList();
            //是客观题的试题内容ID
            List<Guid> listContentID = db.试题内容表.Where(a => listProblemContentID.Contains(a.ID)
                && (a.小题型Enum < 31 || a.小题型Enum == 8011)).Select(a => a.ID).ToList();
            //是客观题的试卷大题中试题ID
            List<Guid> listTestTypeProblemID = listTestProblemTable.Where(a => listContentID.Contains(a.试题内容ID))
                .Select(a=>a.ID).ToList();
            memberDoneTest.客观题得分 = listMemberAnswer.Where(a => listTestTypeProblemID.Contains(a.试卷大题中试题ID))
                .Sum(a => a.该题的得分.Value);

            //主观题得分
            //memberDoneTest.主观题得分 = listTestProblem.Sum(a => a.该题考试回答.该题的得分.Value) - memberDoneTest.客观题得分;
            memberDoneTest.主观题得分 = listMemberAnswer.Sum(a => a.该题的得分.Value) - memberDoneTest.客观题得分;
            db.SaveChanges();
        }

       


        /// <param name="批改状态">0未批改，1批改未完成，2批改完毕</param>       
        public static List<考生做过的试卷> 查询要批改的试卷(Guid? 部门ID, string 试卷名,int 批改状态
            , int 第几页, int 页的大小, out int 返回总条数)
        {
            IQueryable<考生做过的试卷> query = 考生做过的试卷.考生做过的试卷联合查询;
            if (部门ID != null)
            {
                query = query.Where(a => a.考生表.部门ID == 部门ID);
            }
            if (试卷名 != "" && 试卷名 != null)
            {
                query = query.Where(a => a.考试设置表.试卷内容表.名称.Contains(试卷名));
            }
            if (批改状态 > -1)
            {
                query = query.Where(a => a.批改状态类型 == 批改状态);
            }
            返回总条数 = query.Count();
            List<考生做过的试卷> list = query.OrderByDescending(a => a.答题开始时间).Skip(第几页 * 页的大小).Take(页的大小).ToList();
            return list;
        }
        #endregion
    }
}
