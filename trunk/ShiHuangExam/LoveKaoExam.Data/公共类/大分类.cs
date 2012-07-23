using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    /// <summary>
    /// 此类是为爱考网统一格式
    /// </summary>
    public class 大分类
    {
        #region 属性

        public List<分类名称和分类类别名称> 学科
        {
            get;
            set;
        }


        public List<分类名称和分类类别名称> 学历
        {
            get;
            set;
        }


        public List<分类名称和分类类别名称> 考试
        {
            get;
            set;
        }


        public List<分类名称和分类类别名称> 年份
        {
            get;
            set;
        }


        public List<分类名称和分类类别名称> 地区
        {
            get;
            set;
        }


        public List<分类名称和分类类别名称> 开放分类
        {
            get;
            set;
        }

        #endregion



        #region 方法

        public 大分类()
        {
            this.学科 = new List<分类名称和分类类别名称>();
            this.学历 = new List<分类名称和分类类别名称>();
            this.考试 = new List<分类名称和分类类别名称>();
            this.年份 = new List<分类名称和分类类别名称>();
            this.地区 = new List<分类名称和分类类别名称>();
            this.开放分类 = new List<分类名称和分类类别名称>();
        }



        public static 大分类 得到会员最近使用分类(Guid 会员ID, int 需返回条数)
        {
            LoveKaoExamDataContext db = new LoveKaoExamDataContext();
            //取最近7天使用过的分类
            List<所属分类表> listDBSort = db.所属分类表.Where(a => a.操作人id == 会员ID
                && a.操作时间 > DateTime.Now.AddDays(-7)).OrderByDescending(a => a.操作时间).ToList();
            大分类 ReturnSort = new 大分类();
            if (listDBSort.Count != 0)
            {              
                List<string> list = listDBSort.Select(a => a.分类名).Distinct().Take(需返回条数).ToList();
                List<系统分类表> listRecentSystemSort = db.系统分类表.Where(a => list.Contains(a.分类名称)).ToList();               
                foreach (string sortName in list)
                {
                    系统分类表 thisSystemSort = listRecentSystemSort.First(a => a.分类名称.ToLower() == sortName.ToLower());
                    分类名称和分类类别名称 newSort = new 分类名称和分类类别名称();
                    newSort.分类名 = sortName;
                    newSort.类型 = thisSystemSort.分类类别名称;
                    switch (thisSystemSort.分类类别名称)
                    {
                        case "学科":
                            {
                                ReturnSort.学科.Add(newSort);
                                break;
                            }
                        case "学历":
                            {
                                ReturnSort.学历.Add(newSort);
                                break;
                            }
                        case "考试":
                            {
                                ReturnSort.考试.Add(newSort);
                                break;
                            }
                        case "年份":
                            {
                                ReturnSort.年份.Add(newSort);
                                break;
                            }
                        case "地区":
                            {
                                ReturnSort.地区.Add(newSort);
                                break;
                            }
                        case "":
                            {
                                newSort.类型 = "开放分类";
                                ReturnSort.开放分类.Add(newSort);
                                break;
                            }
                    }
                }
                return ReturnSort;
            }
            else
            {
                return ReturnSort;
            }
        }

        #endregion

    }
}
