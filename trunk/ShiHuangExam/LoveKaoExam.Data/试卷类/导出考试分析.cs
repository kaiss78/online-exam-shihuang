using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace LoveKaoExam.Data
{
    public class 导出考试分析
    {
        #region 变量

        private decimal _客观题得分;
        private decimal _总得分;

        #endregion



        #region 属性

        public int 班级名次
        {
            get;
            set;
        }


        public int 总名次
        {
            get;
            set;
        }


        public string 学号
        {
            get;
            set;
        }


        public string 姓名
        {
            get;
            set;
        }


        public string 班级
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

        #endregion



        #region 方法

        public static DataSet 得到导出考试分析列表(Guid 考试设置ID, Guid 部门ID, out 考试设置 考试设置,out string 班级名称)
        {
            List<考生做过的试卷> list = 考生做过的试卷.考生做过的试卷联合查询.Where(a => a.是否是已提交的试卷 == true && a.类型 == 1
                   && a.相关ID == 考试设置ID).ToList();
            list = list.OrderByDescending(a => a.客观题总得分 + a.主观题总得分).ToList();
            List<导出考试分析> listOutAnalyse = new List<导出考试分析>();
            //赋值总名次
            for (int i = 0; i < list.Count; i++)
            {
                导出考试分析 outAnalyse = new 导出考试分析();
                outAnalyse.总名次 = i + 1;
                outAnalyse.学号 = list[i].考生.编号;
                outAnalyse.姓名 = list[i].考生.姓名;
                if (部门ID == Guid.Empty)
                {
                    outAnalyse.班级 = list[i].考生.部门.名称;
                }
                outAnalyse.客观题得分 = list[i].客观题总得分;
                outAnalyse.总得分 = list[i].总得分;
                listOutAnalyse.Add(outAnalyse);
            }
            List<考生做过的试卷> listOneClass = new List<考生做过的试卷>();
            List<导出考试分析> listOneClassAnalyse = new List<导出考试分析>();
            //赋值班级名次
            if (部门ID != Guid.Empty)
            {
                listOneClass = list.Where(a => a.考生.部门ID == 部门ID).ToList();
                listOneClass = listOneClass.OrderByDescending(a => a.客观题总得分 + a.主观题总得分).ToList();
                for (int i = 0; i < listOneClass.Count; i++)
                {
                    导出考试分析 oneAnalyse = listOutAnalyse.Where(a => a.学号 == listOneClass[i].考生.编号).First();
                    oneAnalyse.班级名次 = i + 1;
                    listOneClassAnalyse.Add(oneAnalyse);
                }
            }                    
            考试设置 examSet = 考试设置.考试设置联合试卷内容查询.Where(a => a.ID == 考试设置ID).First();
            examSet.试卷内容 = 试卷内容.把试卷内容表转化成试卷内容(examSet.试卷内容表);
            考试设置 = examSet; 
            //无需返回的属性集合
            DataTable dt = new DataTable();
            List<string> listName = new List<string>();
            if (部门ID != Guid.Empty)
            {
                listName.Add("班级");
                dt = 考生.ListConvertToDataTable<导出考试分析>(listOneClassAnalyse, listName);
            }
            else
            {
                listName.Add("班级名次");
                dt = 考生.ListConvertToDataTable<导出考试分析>(listOutAnalyse, listName);
            }           
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            if (部门ID == Guid.Empty)
            {
                班级名称 = "";
            }
            else
            {
                班级名称 = 部门.部门查询.Where(a => a.ID == 部门ID).Select(a => a.名称).First();
            }
            return ds;
        }



        public static DataTable 得到考试分析信息(Guid 考试设置ID, Guid 部门ID)
        {
            IQueryable<考生做过的试卷> query = 考生做过的试卷.考生做过的试卷联合查询.Where(a => a.相关ID == 考试设置ID
                && a.类型 == 1 && a.是否是已提交的试卷 == true);
            int 应参考人数 = 0;
            if (部门ID != Guid.Empty)
            {
                query = query.Where(a => a.考生表.部门ID == 部门ID);
                应参考人数 = 考生范围.考生范围联合查询.Where(a => a.相关ID == 考试设置ID && a.考生表.部门ID == 部门ID).Count();
            }
            else
            {
                应参考人数 = 考生范围.考生范围查询.Where(a => a.相关ID == 考试设置ID).Count();
            }
            List<考生做过的试卷> listMemberDoneTest = query.ToList();
            listMemberDoneTest = listMemberDoneTest.OrderByDescending(a => a.总得分).ToList();
            DataTable table = new DataTable();
            if (listMemberDoneTest.Count > 0)
            {
                table.Columns.Add("考试日期");
                table.Columns.Add("考试时间");
                table.Columns.Add("应参考人数");
                table.Columns.Add("实参考人数");
                table.Columns.Add("最高分");
                table.Columns.Add("最低分");
                table.Columns.Add("平均分");
                table.Columns.Add("平均用时");
                DataRow row = table.NewRow();
                row["考试日期"] = listMemberDoneTest[0].考试设置表.考试开始时间.ToShortDateString();
                string startTime = listMemberDoneTest[0].考试设置表.考试开始时间.ToShortTimeString();
                string endTime = listMemberDoneTest[0].考试设置表.考试结束时间.ToShortTimeString();
                row["考试时间"] = startTime + "-" + endTime;
                row["应参考人数"] = 应参考人数;
                row["实参考人数"] = listMemberDoneTest.Count;
                row["最高分"] = listMemberDoneTest[0].总得分;
                row["最低分"] = listMemberDoneTest[listMemberDoneTest.Count - 1].总得分;
                row["平均分"] = listMemberDoneTest.Sum(a => a.总得分) / listMemberDoneTest.Count;
                TimeSpan totalTimeSpan = new TimeSpan();
                foreach (考生做过的试卷 memberDoneTest in listMemberDoneTest)
                {
                    totalTimeSpan += memberDoneTest.答题结束时间.Value - memberDoneTest.答题开始时间;
                }
                int averageHour = totalTimeSpan.Hours / listMemberDoneTest.Count;
                int averageMinute = totalTimeSpan.Minutes / listMemberDoneTest.Count;
                int averageSecond = totalTimeSpan.Seconds / listMemberDoneTest.Count;
                row["平均用时"] = "" + averageHour + "时" + averageMinute + "分" + averageSecond + "秒";
                table.Rows.Add(row);
            }
            return table;
        }



        public static List<部门> 得到某场考试部门(Guid 考试设置ID)
        {
           List<Guid> listDepartmentId= 考生范围.考生范围联合查询.Where(a => a.相关ID == 考试设置ID).GroupBy(a => a.考生表.部门ID)
               .Select(a => a.Key.Value).ToList();
           List<部门> listDepartment = 部门.部门查询.Where(a => listDepartmentId.Contains(a.ID)).ToList();
           return listDepartment;
            
        }
        #endregion
    }
}
