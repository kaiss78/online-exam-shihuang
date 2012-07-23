using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 考官:用户
    {
       

        #region 属性

        public static IQueryable<考官> 考官查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.用户表.Where(a => a.角色 == 1).Select(a => new 考官
                    {
                        ID = a.ID,
                        编号 = a.编号,
                        密码 = a.密码,
                        姓名 = a.姓名,
                        性别 = a.性别,
                        邮箱 = a.邮箱,
                        创建时间=a.添加时间
                    });
            }
        }

        #endregion



        #region 方法

        public static 考官 得到考官基本信息根据编号(string 编号)
        {
            考官 invigilate = 考官查询.Where(a => a.编号 == 编号).FirstOrDefault();
            return invigilate;
        }



        public static void 判断是否是考官()
        {
            if (用户信息.CurrentUser.用户类型 != 1)
            {
                throw new Exception("只有教师能添加试题！");
            }
        }
       

        public static List<考官> 查询考官(string 编号或姓名, int 第几页, int 页的大小, out int 返回总条数)
        {
            IQueryable<考官> query = 考官查询;
            if (String.IsNullOrEmpty(编号或姓名)==false)
            {
                query = query.Where(a => a.编号.Contains(编号或姓名) || a.姓名.Contains(编号或姓名));
            }
            返回总条数 = query.Count();
            List<考官> list = query.OrderBy(a => a.编号).Skip(第几页 * 页的大小).Take(页的大小).ToList();
            return list;
        }



        public static List<考官> 查询考官()
        {
            return 考官查询.ToList();
        }




        public static void 删除考官(Guid 考官ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            用户表 user = db.用户表.First(a => a.ID == 考官ID);
            db.用户表.DeleteObject(user);
            db.SaveChanges();
        }


        #endregion
    }
}
