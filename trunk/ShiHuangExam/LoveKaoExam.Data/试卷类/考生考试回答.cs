using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Transactions;

namespace LoveKaoExam.Data
{
    public class 考生考试回答
    {
        #region 属性
    
        [JsonIgnore]
        public Guid ID
        {
            get;
            set;
        }

       
        [JsonIgnore]
        public Guid 考生做过的试卷ID
        {
            get;
            set;
        }

       
        [JsonIgnore]
        public Guid 试卷大题中试题ID
        {
            get;
            set;
        }

       
        [JsonIgnore]
        public short 该题的用时
        {
            get;
            set;
        }

     
        public decimal? 该题的得分
        {
            get;
            set;
        }


       
        public string 评语
        {
            get;
            set;
        }


        public int 回答的题型
        {
            get;
            set;
        }


        [JsonIgnore]
        public 考生做过的试卷 考生做过的试卷
        {
            get;
            set;
        }


        [JsonIgnore]
        public 试卷大题中试题 试卷大题中试题
        {
            get;
            set;
        }



        public static IQueryable<考生考试回答> 考生考试回答查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.考生考试回答表.Select(a => new 考生考试回答
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

        public virtual void 保存考生考试回答(LoveKaoExamEntities db) { }


        public static void 提交保存试卷(string 答卷Json)
        {
            考生做过的试卷 memberDoneTest = 考生做过的试卷.把Json转化成考生做过的试卷(答卷Json);
            List<考生考试回答> listMemberTestAnswer = new List<考生考试回答>();
            List<Guid> listTestProblemId = new List<Guid>();
            List<试卷中大题> listType = new List<试卷中大题>();          
            listType = memberDoneTest.考试设置.试卷内容.试卷中大题集合;
            foreach (试卷中大题 type in listType)
            {
                foreach (试卷大题中试题 testProblem in type.试卷大题中试题集合)
                {
                    //是复合题和多题干共选项题
                    if (testProblem.子小题集合 != null)
                    {
                        foreach (试卷大题中试题 subTestProblem in testProblem.子小题集合)
                        {
                            if (subTestProblem.该题考试回答 != null)
                            {
                                subTestProblem.试卷中大题ID = type.ID;
                                subTestProblem.该题考试回答.试卷大题中试题ID = subTestProblem.ID;
                                listMemberTestAnswer.Add(subTestProblem.该题考试回答);
                                listTestProblemId.Add(subTestProblem.ID);
                            }
                        }
                    }
                    //普通小题
                    else
                    {
                        if (testProblem.该题考试回答 != null)
                        {
                            testProblem.试卷中大题ID = type.ID;
                            testProblem.该题考试回答.试卷大题中试题ID = testProblem.ID;
                            listMemberTestAnswer.Add(testProblem.该题考试回答);
                            listTestProblemId.Add(testProblem.ID);
                        }
                    }
                }
            }
            List<试卷大题中试题> listTestProblem = 试卷大题中试题.试卷大题中试题查询.Where(a => listTestProblemId
                .Contains<Guid>(a.ID)).ToList();
            提交保存试卷(memberDoneTest, listMemberTestAnswer, listTestProblem);

        }


        public static void 提交保存试卷(考生做过的试卷 考生做过的试卷, List<考生考试回答> 考生考试回答集合, List<试卷大题中试题> 试卷大题中试题集合)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                考生做过的试卷表 memberDoneTest = new 考生做过的试卷表();
                考生做过的试卷表 dbMemberDoneTest = db.考生做过的试卷表.Where(a => a.考生ID == 考生做过的试卷.考生ID
                    && a.相关ID == 考生做过的试卷.相关ID && a.是否是已提交的试卷 == false).FirstOrDefault();
                //先删除原来保存的，再加入新的保存
                if (dbMemberDoneTest != null)
                {
                    db.考生做过的试卷表.DeleteObject(dbMemberDoneTest);
                    //db.SaveChanges();
                }
                memberDoneTest.ID = 考生做过的试卷.ID;
                memberDoneTest.答题结束时间 = 考生做过的试卷.答题结束时间;
                memberDoneTest.答题开始时间 = 考生做过的试卷.答题开始时间;
                memberDoneTest.考生ID = 考生做过的试卷.考生ID;
                if (考生做过的试卷.是否是已提交的试卷 == true)
                {
                    memberDoneTest.客观题得分 = 计算客观题总得分(考生考试回答集合, 试卷大题中试题集合, db);
                }
                memberDoneTest.相关ID = 考生做过的试卷.相关ID;
                memberDoneTest.类型 = 考生做过的试卷.类型;
                memberDoneTest.是否是已提交的试卷 = 考生做过的试卷.是否是已提交的试卷;
                memberDoneTest.批改状态 = 考生做过的试卷.批改状态类型;
                memberDoneTest.主观题得分 = 考生做过的试卷.主观题总得分;
                db.考生做过的试卷表.AddObject(memberDoneTest);
                foreach (var memberTestAnswer in 考生考试回答集合)
                {
                    memberTestAnswer.考生做过的试卷ID = memberDoneTest.ID;
                    memberTestAnswer.保存考生考试回答(db);
                }
                db.SaveChanges();
                //scope.Complete();
            //}
        }


        public static decimal 计算客观题总得分(List<考生考试回答> 考生考试回答集合, List<试卷大题中试题> 试卷大题中试题集合, LoveKaoExamEntities db)
        {
            decimal score = 0;
            List<考生多选题回答> listMulti = new List<考生多选题回答>();
            List<考生填空题回答> listFill = new List<考生填空题回答>();
            List<考生选词填空回答> listDiction = new List<考生选词填空回答>();
            List<考生完形填空回答> listCloze = new List<考生完形填空回答>();
            List<考生连线题回答> listLink = new List<考生连线题回答>();
            //按回答的题型分组
            var group = 考生考试回答集合.GroupBy(a => a.回答的题型).ToList();
            for (int j = 0; j < group.Count; j++)
            {
                switch (group[j].Key)
                {
                    case 12:
                        {
                            foreach (考生考试回答 answer in group[j])
                            {
                                考生多选题回答 multi = (考生多选题回答)answer;
                                listMulti.Add(multi);
                            }
                            break;
                        }
                    case 13:
                        {
                            foreach (考生考试回答 answer in group[j])
                            {
                                考生填空题回答 fill = (考生填空题回答)answer;
                                listFill.Add(fill);
                            }
                            break;
                        }
                    case 14:
                        {
                            foreach (考生考试回答 answer in group[j])
                            {
                                考生选词填空回答 diction = (考生选词填空回答)answer;
                                listDiction.Add(diction);
                            }
                            break;
                        }
                    case 15:
                        {
                            foreach (考生考试回答 answer in group[j])
                            {
                                考生完形填空回答 cloze = (考生完形填空回答)answer;
                                listCloze.Add(cloze);
                            }
                            break;
                        }
                    case 30:
                        {
                            foreach (考生考试回答 answer in group[j])
                            {
                                考生连线题回答 link = (考生连线题回答)answer;
                                listLink.Add(link);
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            //试题内容ID集合
            List<Guid> listContentId = new List<Guid>();
            foreach (试卷大题中试题 testProblem in 试卷大题中试题集合)
            {
                listContentId.Add(testProblem.试题内容ID);
            }
            //按题型分组的试题内容集合
            var listContent = db.试题内容表.Where(a => listContentId.Contains(a.ID)
                && a.小题型Enum < 31).GroupBy(a => a.小题型Enum).ToList();
            //按分组得到相应的答案
            for (int i = 0; i < listContent.Count; i++)
            {
                //每个分组的所有试题内容ID集合
                List<Guid> listId = new List<Guid>();
                foreach (试题内容表 content in listContent[i])
                {
                    listId.Add(content.ID);
                }
                switch (listContent[i].Key)
                {
                    //单选题
                    case 11:
                    case 8011:
                        {
                            List<自由题空格表> listSpace = db.自由题空格表.Where(a => listId.Contains(a.试题内容ID)).ToList();
                            //空格ID集合
                            List<Guid> listSpaceId = new List<Guid>();
                            foreach (自由题空格表 space in listSpace)
                            {
                                listSpaceId.Add(space.ID);
                            }
                            List<自由题选项空格答案表> listAnswer = db.自由题选项空格答案表.Where(a => listSpaceId.Contains(a.自由题空格ID)).ToList();
                            //给空格赋答案
                            foreach (自由题空格表 space in listSpace)
                            {
                                foreach (自由题选项空格答案表 answer in listAnswer)
                                {
                                    if (space.ID == answer.自由题空格ID)
                                    {
                                        space.自由题选项空格答案表.Add(answer);
                                        break;
                                    }
                                }
                            }
                            //判断答案是否正确，并判分
                            foreach (自由题空格表 space in listSpace)
                            {
                                foreach (试卷大题中试题 testProblem in 试卷大题中试题集合)
                                {
                                    //找到试题内容ID相等的
                                    if (space.试题内容ID == testProblem.试题内容ID)
                                    {
                                        foreach (考生考试回答 memberTestAnswer in 考生考试回答集合)
                                        {
                                            //找到回答的试题ID相等的
                                            if (testProblem.ID == memberTestAnswer.试卷大题中试题ID)
                                            {
                                                考生单选题回答 singleAnswer = (考生单选题回答)memberTestAnswer;
                                                //回答正确
                                                if (space.自由题选项空格答案表.ElementAt(0).自由题选项ID == singleAnswer.回答的选项ID)
                                                {                                                  
                                                    score += testProblem.每小题分值;
                                                    singleAnswer.该题的得分 = testProblem.每小题分值;
                                                }
                                                //回答错误
                                                else
                                                {
                                                    singleAnswer.该题的得分 = 0;
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    //多选题
                    case 12:
                        {
                            //是多选题的所有试卷大题中试题
                            List<试卷大题中试题> listMultiTestProblem = 试卷大题中试题集合.Where(a => listId.Contains<Guid>(a.试题内容ID))
                                .ToList();
                            //按题型分组，不同题型的多选题评分策略不同
                            var groupMultiType = listMultiTestProblem.GroupBy(a => a.试卷中大题ID).ToList();
                            //得到不同题型的多选题评分策略
                            List<decimal> listMarkScore = new List<decimal>();
                            for (int n = 0; n < groupMultiType.Count; n++)
                            {
                                decimal markScore = 0;// db.试卷中大题表.First(a => a.ID == groupMultiType[n].Key).多选题评分策略;
                                listMarkScore.Add(markScore);
                            }
                            List<自由题空格表> listSpace = db.自由题空格表.Where(a => listId.Contains(a.试题内容ID)).ToList();
                            //空格ID集合
                            List<Guid> listSpaceId = new List<Guid>();
                            foreach (自由题空格表 space in listSpace)
                            {
                                listSpaceId.Add(space.ID);
                            }
                            List<自由题选项空格答案表> listAnswer = db.自由题选项空格答案表.Where(a => listSpaceId.Contains(a.自由题空格ID)).ToList();
                            //给空格赋答案
                            foreach (自由题空格表 space in listSpace)
                            {
                                foreach (自由题选项空格答案表 answer in listAnswer)
                                {
                                    if (space.ID == answer.自由题空格ID)
                                    {
                                        space.自由题选项空格答案表.Add(answer);
                                        break;
                                    }
                                }
                            }

                            //判断答案是否正确，并判分
                            foreach (自由题空格表 space in listSpace)
                            {
                                foreach (试卷大题中试题 testProblem in 试卷大题中试题集合)
                                {
                                    //找到试题内容ID相等的
                                    if (space.试题内容ID == testProblem.试题内容ID)
                                    {
                                        foreach (考生考试回答 memberTestAnswer in 考生考试回答集合)
                                        {
                                            //找到回答的试题ID相等的
                                            if (testProblem.ID == memberTestAnswer.试卷大题中试题ID)
                                            {
                                                考生多选题回答 multiAnswer = (考生多选题回答)memberTestAnswer;
                                                decimal 多选题评分策略 = 0;
                                                //如果答案个数和回答个数相等，判断回答是否正确
                                                if (space.自由题选项空格答案表.Count == multiAnswer.回答的选项ID集合.Count)
                                                {
                                                    //记录回答正确的个数
                                                    int count = 0;
                                                    foreach (var answer in space.自由题选项空格答案表)
                                                    {
                                                        foreach (Guid memberAnswerId in multiAnswer.回答的选项ID集合)
                                                        {
                                                            if (answer.自由题选项ID == memberAnswerId)
                                                            {
                                                                count++;
                                                            }
                                                        }
                                                    }
                                                    //回答全部正确
                                                    if (space.自由题选项空格答案表.Count == count)
                                                    {
                                                        score += testProblem.每小题分值;
                                                        multiAnswer.该题的得分 = testProblem.每小题分值;
                                                    }
                                                    else
                                                    {
                                                        multiAnswer.该题的得分 = 0;
                                                    }
                                                }
                                                //如果答案个数大于回答个数，表示漏选，判断回答是否正确
                                                else if (space.自由题选项空格答案表.Count > multiAnswer.回答的选项ID集合.Count)
                                                {
                                                    //记录回答正确个数
                                                    int count = 0;
                                                    foreach (var answer in space.自由题选项空格答案表)
                                                    {
                                                        foreach (Guid memberAnswerId in multiAnswer.回答的选项ID集合)
                                                        {
                                                            if (answer.自由题选项ID == memberAnswerId)
                                                            {
                                                                count++;
                                                            }
                                                        }
                                                    }
                                                    //回答全部正确
                                                    if (count == multiAnswer.回答的选项ID集合.Count)
                                                    {
                                                        for (int m = 0; m < groupMultiType.Count; m++)
                                                        {
                                                            试卷大题中试题 multiTestProblem = groupMultiType[m].FirstOrDefault(
                                                                a => a.ID == testProblem.ID);
                                                            if (multiTestProblem != null)
                                                            {
                                                                多选题评分策略 = listMarkScore[m];
                                                                break;
                                                            }
                                                        }
                                                        decimal ccc = testProblem.每小题分值;
                                                        score += count * 多选题评分策略;
                                                        multiAnswer.该题的得分 = count * 多选题评分策略;
                                                    }
                                                    //回答有错
                                                    else
                                                    {
                                                        multiAnswer.该题的得分 = 0;
                                                    }
                                                }
                                                //答案个数小于回答个数，表示多选，也错
                                                else
                                                {
                                                    multiAnswer.该题的得分 = 0;
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    //填空题
                    case 13:
                        {
                            List<自由题空格表> listSpace = db.自由题空格表.Where(a => listId.Contains(a.试题内容ID)).ToList();
                            //空格ID集合
                            List<Guid> listSpaceId = new List<Guid>();
                            foreach (自由题空格表 space in listSpace)
                            {
                                listSpaceId.Add(space.ID);
                            }
                            List<自由题填空答案表> listAnswer = db.自由题填空答案表.Where(a => listSpaceId.Contains(a.自由题空格ID)).ToList();
                            //给空格赋答案
                            foreach (自由题空格表 space in listSpace)
                            {
                                foreach (自由题填空答案表 answer in listAnswer)
                                {
                                    if (space.ID == answer.自由题空格ID)
                                    {
                                        space.自由题填空答案表.Add(answer);
                                    }
                                }
                            }
                            //找到空格相等的
                            for (int k = 0; k < listFill.Count; k++)
                            {
                                //一道填空题的得分
                                decimal scoring = 0;
                                试卷大题中试题 testProblem = 试卷大题中试题集合.FirstOrDefault(a => a.ID == listFill[k].试卷大题中试题ID);
                                //本道填空题的分值
                                decimal fillScore = 0;
                                if (testProblem != null)
                                {
                                    fillScore = testProblem.每小题分值;
                                }
                                //每个空格的分值
                                decimal spaceScore = fillScore / listSpace.Where(a => a.试题内容ID == testProblem.试题内容ID).Count();
                                foreach (填空空格回答 spaceAnswer in listFill[k].填空空格回答集合)
                                {
                                    //找到回答的空格ID和题目本身的空格ID相等的
                                    自由题空格表 space = listSpace.Where(a => a.ID == spaceAnswer.空格ID).FirstOrDefault();
                                    if (space != null)
                                    {
                                        //判断回答是否正确
                                        foreach (自由题填空答案表 fillAnswer in space.自由题填空答案表)
                                        {
                                            if (spaceAnswer.回答的内容 == fillAnswer.该空答案)
                                            {
                                                score += spaceScore;
                                                scoring += spaceScore;
                                                break;
                                            }
                                        }
                                    }
                                }
                                listFill[k].该题的得分 = scoring;
                            }
                            break;
                        }
                    //选词填空
                    case 14:
                        {
                            List<自由题空格表> listSpace = db.自由题空格表.Where(a => listId.Contains(a.试题内容ID)).ToList();
                            //空格ID集合
                            List<Guid> listSpaceId = new List<Guid>();
                            foreach (自由题空格表 space in listSpace)
                            {
                                listSpaceId.Add(space.ID);
                            }
                            List<自由题选项空格答案表> listAnswer = db.自由题选项空格答案表.Where(a => listSpaceId.Contains(a.自由题空格ID)).ToList();
                            //给空格赋答案
                            foreach (自由题空格表 space in listSpace)
                            {
                                foreach (自由题选项空格答案表 answer in listAnswer)
                                {
                                    if (space.ID == answer.自由题空格ID)
                                    {
                                        space.自由题选项空格答案表.Add(answer);
                                        break;
                                    }
                                }
                            }
                            //找到空格相等的
                            for (int k = 0; k < listDiction.Count; k++)
                            {
                                //一道选词填空的得分
                                decimal scoring = 0;
                                试卷大题中试题 testProblem = 试卷大题中试题集合.FirstOrDefault(a => a.ID == listDiction[k].试卷大题中试题ID);
                                //本题选词填空的分值
                                decimal dictionScore = 0;
                                if (testProblem != null)
                                {
                                    dictionScore = testProblem.每小题分值;
                                }
                                //每个空格的分值
                                decimal spaceScore = dictionScore / listSpace.Where(a => a.试题内容ID == testProblem.试题内容ID).Count();
                                foreach (选词空格回答 spaceAnswer in listDiction[k].选词空格回答集合)
                                {
                                    //找到回答的空格ID和题目本身的空格ID相等的
                                    自由题空格表 space = listSpace.Where(a => a.ID == spaceAnswer.空格ID).FirstOrDefault();
                                    if (space != null)
                                    {
                                        //判断回答是否正确
                                        if (spaceAnswer.回答的选项ID == space.自由题选项空格答案表.ElementAt(0).自由题选项ID)
                                        {
                                            score += spaceScore;
                                            scoring += spaceScore;
                                        }
                                    }
                                }
                                listDiction[k].该题的得分 = scoring;
                            }
                            break;
                        }
                    //完形填空
                    case 15:
                        {

                            List<自由题空格表> listSpace = db.自由题空格表.Where(a => listId.Contains(a.试题内容ID)).ToList();
                            //空格ID集合
                            List<Guid> listSpaceId = new List<Guid>();
                            foreach (自由题空格表 space in listSpace)
                            {
                                listSpaceId.Add(space.ID);
                            }
                            List<自由题选项空格答案表> listAnswer = db.自由题选项空格答案表.Where(a => listSpaceId.Contains(a.自由题空格ID)).ToList();
                            //给空格赋答案
                            foreach (自由题空格表 space in listSpace)
                            {
                                foreach (自由题选项空格答案表 answer in listAnswer)
                                {
                                    if (space.ID == answer.自由题空格ID)
                                    {
                                        space.自由题选项空格答案表.Add(answer);
                                        break;
                                    }
                                }
                            }
                            //找到空格相等的
                            for (int k = 0; k < listCloze.Count; k++)
                            {
                                //一道完形填空的得分
                                decimal scoring = 0;
                                试卷大题中试题 testProblem = 试卷大题中试题集合.FirstOrDefault(a => a.ID == listCloze[k].试卷大题中试题ID);
                                //本题完形填空的分值
                                decimal clozeScore = 0;
                                if (testProblem != null)
                                {
                                    clozeScore = testProblem.每小题分值;
                                }
                                //每个空格的分值
                                decimal spaceScore = clozeScore / listSpace.Where(a => a.试题内容ID == testProblem.试题内容ID).Count();
                                foreach (完形空格回答 spaceAnswer in listCloze[k].完形空格回答集合)
                                {
                                    //找到回答的空格ID和题目本身的空格ID相等的
                                    自由题空格表 space = listSpace.Where(a => a.ID == spaceAnswer.空格ID).FirstOrDefault();
                                    if (space != null)
                                    {
                                        //判断回答是否正确
                                        if (spaceAnswer.回答的选项ID == space.自由题选项空格答案表.ElementAt(0).自由题选项ID)
                                        {
                                            score += spaceScore;
                                            scoring += spaceScore;
                                        }
                                    }
                                }
                                listCloze[k].该题的得分 = scoring;
                            }
                            break;
                        }
                    //判断题
                    case 20:
                        {
                            List<判断题答案表> listJudgeAnswer = db.判断题答案表.Where(a => listId.Contains(a.试题内容ID)).ToList();
                            //判断答案是否正确，并判分
                            foreach (判断题答案表 judgeAnswer in listJudgeAnswer)
                            {
                                foreach (试卷大题中试题 testProblem in 试卷大题中试题集合)
                                {
                                    //找到试题内容ID相等的
                                    if (judgeAnswer.试题内容ID == testProblem.试题内容ID)
                                    {
                                        foreach (考生考试回答 memberTestAnswer in 考生考试回答集合)
                                        {
                                            //找到回答的试题ID相等的
                                            if (testProblem.ID == memberTestAnswer.试卷大题中试题ID)
                                            {
                                                考生判断题回答 memberJudgeAnswer = (考生判断题回答)memberTestAnswer;
                                                //判断是否正确
                                                if (judgeAnswer.答案 == memberJudgeAnswer.回答答案)
                                                {
                                                    score += testProblem.每小题分值;
                                                    memberJudgeAnswer.该题的得分 = testProblem.每小题分值;
                                                }
                                                else
                                                {
                                                    memberJudgeAnswer.该题的得分 = 0;
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    //连线题
                    case 30:
                        {
                            List<连线题答案表> listLinkAnswer = db.连线题答案表.Where(a => listId.Contains(a.试题内容ID)).ToList();
                            //找到左选项ID相等的
                            for (int k = 0; k < listLink.Count; k++)
                            {
                                //一道连线题的得分
                                decimal scoring = 0;
                                试卷大题中试题 testProblem = 试卷大题中试题集合.FirstOrDefault(a => a.ID == listLink[k].试卷大题中试题ID);
                                //本题连线题的分值
                                decimal linkScore = 0;
                                if (testProblem != null)
                                {
                                    linkScore = testProblem.每小题分值;
                                }
                                //一条连线的分值
                                decimal oneLinkScore = linkScore / listLinkAnswer.Where(a => a.试题内容ID == testProblem.试题内容ID).Count();
                                foreach (连线题回答 linkAnswer in listLink[k].连线题回答集合)
                                {
                                    //找到回答的左选项ID和题目本身的左选项ID相等的
                                    List<连线题答案表> answerList = listLinkAnswer.Where(a => a.左边ID == linkAnswer.回答的左选项ID).ToList();
                                    foreach (连线题答案表 answer in answerList)
                                    {
                                        //判断回答是否正确
                                        if (answer.右边ID == linkAnswer.回答的右选项ID)
                                        {
                                            score += oneLinkScore;
                                            scoring += oneLinkScore;
                                            listLinkAnswer.Remove(answer);
                                        }
                                    }
                                }
                                listLink[k].该题的得分 = scoring;
                            }
                            break;
                        }
                }
            }
            return score;
        }
     
        #endregion
    }
}
