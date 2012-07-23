using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 选项组
    {
        #region 变量
        private List<选项> _选项集合;
        #endregion

        #region 属性

        public List<选项> 选项集合
        {
            get
            {
                if (_选项集合 == null)
                {
                    _选项集合 =选项.选项查询.Where(a => a.选项组ID == this.ID).OrderBy(a => a.顺序).ToList();
                    return _选项集合;
                }
                return _选项集合;
            }
            set
            {
                _选项集合 = value;
            }
        }


        public Guid ID
        {
            get;
            set;
        }


        public static IQueryable<选项组> 选项组查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.自由题选项组表.Select(a => new 选项组
                    {
                        ID = a.ID
                    });

            }
        }

        #endregion
    }
}
