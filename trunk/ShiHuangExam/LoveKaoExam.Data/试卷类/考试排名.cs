using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoveKao.Page;

namespace LoveKaoExam.Data
{
    public class 考试排名
    {
        #region 变量

        private decimal _总得分;
        private decimal _客观题得分;
        #endregion



        #region 属性

        public int 名次
        {
            get;
            set;
        }


        public Guid 考生ID
        {
            get;
            set;
        }


        public 用户 考生
        {
            get;
            set;
        }


        public Guid 考试设置ID
        {
            get;
            set;
        }


        public 考试设置 考试设置
        {
            get;
            set;
        }


        public int 参考人数
        {
            get;
            set;
        }


        public int 我的排名
        {
            get;
            set;
        }


      
        public string 及格情况
        {
            get;
            set;
        }



        public decimal 客观题得分
        {
            get
            {
                string score = _客观题得分.ToString("G0");
                decimal totalScore = Convert.ToDecimal(score);
                return totalScore;
            }
            set
            {
                _客观题得分 = value;
            }
        }


        public decimal 总得分
        {
            get
            {
                string score = _总得分.ToString("G0");
                decimal totalScore = Convert.ToDecimal(score);
                return totalScore;
            }
            set
            {
                _总得分 = value;
            }
        }


        public 考试设置表 考试设置表
        {
            get;
            set;
        }


        public static IQueryable<考试排名> 考试排名查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from examSet in db.考试设置表
                       join memberDoneTest in db.考生做过的试卷表
                       on examSet.ID equals memberDoneTest.相关ID
                       where memberDoneTest.是否是已提交的试卷 == true && memberDoneTest.类型 == 1
                       select new 考试排名
                       {
                           考生ID = memberDoneTest.考生ID,
                           考试设置ID = examSet.ID,
                           考试设置表 = examSet,
                           总得分 = memberDoneTest.客观题得分 + memberDoneTest.主观题得分,
                           客观题得分 = memberDoneTest.客观题得分
                       };
            }
        }

        #endregion



        #region 方法

        public static List<考试排名> 得到考试排名(string 关键字,Guid 考试设置ID, Guid 考生ID, int 第几页, int 页的大小, out int 返回总条数,out LKPageException 异常信息)
        {
            考试设置 examSet = 考试设置.考试设置查询.Where(a => a.ID == 考试设置ID).First();
            if (examSet.是否公布考试结果 == false)
            {
                异常信息 = new LKPageException(异常处理.得到异常信息(10));
                返回总条数 = 0;
                return new List<考试排名>();
            }
            if (DateTime.Now < examSet.考试结束时间)
            {
                异常信息 = new LKPageException(异常处理.得到异常信息(9));
                返回总条数 = 0;
                return new List<考试排名>();
            }
            //先查询出与该考生同班级的所有考生
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            用户表 user = db.用户表.FirstOrDefault(a=>a.ID==考生ID);
            List<Guid> listUserId = db.用户表.Where(a => a.部门ID == user.部门ID).Select(a => a.ID).ToList();
            //查询这些考生的排名
            List<考试排名> listAllRank=考试排名查询.Where(a => listUserId.Contains(a.考生ID) && a.考试设置ID == 考试设置ID).ToList();
            if (listAllRank.Any(a => a.考生ID == 考生ID) == false)
            {
                异常信息 = new LKPageException(异常处理.得到异常信息(11));
                返回总条数 = 0;
                return new List<考试排名>();
            }
            listAllRank = listAllRank.OrderByDescending(a => a.总得分).ToList();
            if (listAllRank.Count > 0)
            {
                //赋值名次，考生，考试设置，及格情况，参考书人数，我的排名属性
                for (int i = 0; i < listAllRank.Count; i++)
                {
                    listAllRank[i].名次 = i + 1;
                }
                List<Guid> listStudentId = listAllRank.Select(a => a.考生ID).ToList();
                List<用户> listStudent = 用户.用户查询.Where(a => listStudentId.Contains(a.ID)).ToList();
                Guid 试卷内容ID=listAllRank[0].考试设置表.试卷内容ID;
                试卷内容 test = 试卷内容.试卷内容查询.Where(a => a.ID == 试卷内容ID).First();
                //查询我的排名
                int myRank = listAllRank.Where(a => a.考生ID == 考生ID).First().名次;
                foreach (考试排名 rank in listAllRank)
                {
                    rank.考试设置 = 考试设置.把考试设置表转化成考试设置(rank.考试设置表);
                    rank.考试设置.试卷内容 = test;
                    rank.考生 = listStudent.First(a => a.ID == rank.考生ID);
                    if (rank.总得分 / test.总分 * 100 < rank.考试设置表.及格条件)
                    {
                        rank.及格情况 = "不及格";
                    }
                    else
                    {
                        rank.及格情况 = "及格";
                    }
                    rank.参考人数 = listAllRank.Count;
                    rank.我的排名 = myRank;
                }
            }
            if (!String.IsNullOrEmpty(关键字))
            {
                listAllRank = listAllRank.Where(a => a.考生.编号.Contains(关键字) || a.考生.姓名.Contains(关键字)).ToList();               
            }
            返回总条数 = listAllRank.Count;
            List<考试排名> listRank = listAllRank.Skip(第几页 * 页的大小).Take(页的大小).ToList();
            异常信息 = new LKPageException();
            return listAllRank;
        }
        #endregion
    }
}
