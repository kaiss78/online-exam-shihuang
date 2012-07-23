using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 考试分析
    {
        #region 属性

        public string 部门名
        {
            get;
            set;
        }


        public List<string> 间隔值列表
        {
            get;
            set;
        }


        public List<int> 人数列表
        {
            get;
            set;
        }

        #endregion



        #region 方法

        public static List<考试分析> 得到某场考试分析数据(Guid 考试设置ID, int 间隔值, Guid 部门ID,out 考试设置 考试设置)
        {
            List<考试分析> listAnalyse = new List<考试分析>();
            考试设置 examSet =考试设置.考试设置联合试卷内容查询.Where(a => a.ID == 考试设置ID).First();
            int totalScore = examSet.试卷内容.总分;
            IQueryable<考生做过的试卷> query = 考生做过的试卷.考生做过的试卷联合查询.Where(a => a.相关ID == 考试设置ID
                && a.是否是已提交的试卷 == true && a.类型 == 1);
            //查询所有部门
            if (部门ID == Guid.Empty)
            {
                var group = query.GroupBy(a => a.考生表.部门ID.Value).ToList();
                foreach (var subGroup in group)
                {
                    string departmentName = 部门.部门查询.Where(a => a.ID == subGroup.Key).First().名称;
                    List<考生做过的试卷> list=subGroup.ToList();
                    if (list.Count > 0)
                    {
                        考试分析 analyse = 得到考试分析(list, totalScore, 间隔值, departmentName);
                        listAnalyse.Add(analyse);
                    }
                }
            }
            //查询单个部门
            else
            {
                string departmentName = 部门.部门查询.Where(a => a.ID == 部门ID).First().名称;
                List<考生做过的试卷> list = query.Where(a => a.考生表.部门ID == 部门ID).ToList();
                if (list.Count > 0)
                {
                    考试分析 analyse = 得到考试分析(list, totalScore, 间隔值, departmentName);
                    listAnalyse.Add(analyse);
                }
            }
            考试设置 = examSet;
            return listAnalyse;
        }



        private static 考试分析 得到考试分析(List<考生做过的试卷> list, int totalScore, int 间隔值, string departmentName)
        {           
            //X轴数据
            List<string> listX = new List<string>();
            //Y轴数据
            List<int> listY = new List<int>();
            int count = 0;
            if (totalScore % 间隔值 == 0)
            {
                count = totalScore / 间隔值;
            }
            else
            {
                count = totalScore / 间隔值 + 1;
            }
            for (int i = 0; i < count; i++)
            {
                string x = string.Empty;
                int y = 0;
                //第一段间隔值
                if (i == 0)
                {
                    x = i * 间隔值 + "-" + (i + 1) * 间隔值;
                    y = list.Where(a => a.总得分 < (i + 1) * 间隔值 + 1).Count();
                }
                //最后一段间隔值
                else if (i == count - 1)
                {
                    x = i * 间隔值 + 1 + "-" + totalScore;
                    y = list.Where(a => a.总得分 > i * 间隔值).Count();
                }
                //中间段间隔值
                else
                {
                    x = i * 间隔值 + 1 + "-" + (i + 1) * 间隔值;
                    y = list.Where(a => a.总得分 > i * 间隔值 && a.总得分 < (i + 1) * 间隔值 + 1).Count();
                }
                listX.Add(x);
                listY.Add(y);
            }
            考试分析 analyse = new 考试分析();
            analyse.部门名 = departmentName;
            analyse.间隔值列表 = listX;
            analyse.人数列表 = listY;
            return analyse;
        }

        #endregion
    }
}
