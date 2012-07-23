using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 部门
    {                   
        #region 属性

        public Guid ID
        {
            get;
            set;
        }


        public string 名称
        {
            get;
            set;
        }


        public Guid? 上级部门ID
        {
            get;
            set;
        }


        public Guid 创建人ID
        {
            get;
            set;
        }


        public DateTime 创建时间
        {
            get;
            set;
        }



        public static IQueryable<部门> 部门查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.部门表.Select(a => new 部门
                    {
                        ID = a.ID,
                        名称 = a.名称,
                        上级部门ID = a.上级部门ID,
                        创建人ID=a.添加人ID,
                        创建时间=a.添加时间
                    });
            }
        }

        #endregion



        #region 方法

        /// <summary>
        /// 返回0成功,1部门名已存在
        /// </summary>
        public static int 添加部门(部门 部门)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            if (db.部门表.Any(a => a.名称 == 部门.名称) == true)
            {
                return 1;
            }
            部门表 department = new 部门表();
            department.ID = 部门.ID;
            department.名称 = 部门.名称;
            department.上级部门ID = 部门.上级部门ID;
            department.添加人ID = 部门.创建人ID;
            department.添加时间 = DateTime.Now;
            db.部门表.AddObject(department);
            db.SaveChanges();
            return 0;
        }



        public static 部门 得到某部门根据部门ID(Guid 部门ID)
        {
            部门 department = 部门查询.Where(a => a.ID == 部门ID).First();
            return department;
        }



        /// <summary>
        /// 返回0修改成功,1部门名已存在，不能修改
        /// </summary>
        public static int 修改部门(部门 部门)
        {
            异常处理.删除修改权限判断();
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            if (db.部门表.Any(a => a.名称 == 部门.名称) == true)
            {
                return 1;
            }
            部门表 department = db.部门表.FirstOrDefault(a=>a.ID==部门.ID);         
            department.名称 = 部门.名称;
            department.上级部门ID = 部门.上级部门ID;
            department.添加时间 = DateTime.Now;
            db.SaveChanges();
            return 0;
        }



        public static List<部门> 查询部门()
        {
            return 部门查询.OrderByDescending(a=>a.创建时间).ToList();
        }



        public static List<部门> 查询部门(string 部门名, int 第几页, int 页的大小, out int 返回总条数)
        {
            IQueryable<部门> query = 部门查询;
            if (部门名 != "" && 部门名 != null)
            {
                query = query.Where(a => a.名称.Contains(部门名));
            }
            返回总条数 = query.Count();
            List<部门> list = query.OrderByDescending(a=>a.创建时间).Skip(第几页 * 页的大小).Take(页的大小).ToList();
            return list;
        }



        public static void 删除部门(Guid 部门ID)
        {
            异常处理.删除修改权限判断();
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            部门表 department = db.部门表.First(a => a.ID == 部门ID);
            db.部门表.DeleteObject(department);
            db.SaveChanges();
        }

        #endregion
    }
}
