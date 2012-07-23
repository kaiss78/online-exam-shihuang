using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 多选题回答
    {
        #region 属性

        public Guid 考生考试回答ID
        {
            get;
            set;
        }


        public Guid 回答的选项ID
        {
            get;
            set;
        }


        public static IQueryable<多选题回答> 多选题回答查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.考生多选题回答表.Select(a => new 多选题回答
                    {
                        回答的选项ID = a.回答的选项ID,
                        考生考试回答ID = a.考生考试回答ID
                    });
            }
        }

        #endregion
    }
}
