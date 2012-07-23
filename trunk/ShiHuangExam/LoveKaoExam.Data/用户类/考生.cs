using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace LoveKaoExam.Data
{
    public class 考生:用户
    {
        #region 变量
    
        private 部门 _部门;

        #endregion




        #region 属性

        public Guid 部门ID
        {
            get;
            set;
        }


        public 部门 部门
        {
            get
            {
                if (_部门 == null)
                {
                    _部门 = 部门.得到某部门根据部门ID(this.部门ID);
                    return _部门;
                }
                else
                {
                    return _部门;
                }
            }
            set
            {
                _部门 = value;
            }
        }


        public static IQueryable<考生> 考生查询
        {
            get
            {

                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.用户表.Where(a => a.角色 == 0).Select(a => new 考生()
                {
                    ID = a.ID,
                    编号 = a.编号,
                    部门ID=a.部门ID.Value,
                    密码 = a.密码,
                    姓名=a.姓名,
                    性别 = a.性别,
                    邮箱=a.邮箱,
                    创建时间=a.添加时间
                }
                    );

            }
        }

        #endregion



        #region 方法
      
        public static 考生 得到考生基本信息根据编号(string 编号)
        {
            考生 examinee = 考生查询.Where(a => a.编号 == 编号).FirstOrDefault();
            return examinee;
        }




        public static List<考生> 查询考生(Guid 部门ID, string 编号或姓名, int 第几页, int 页的大小, out int 返回总条数)
        {
            IQueryable<考生> query = 考生查询;
            if (String.IsNullOrEmpty(编号或姓名) == false)
            {
                query = query.Where(a => a.编号.Contains(编号或姓名) || a.姓名.Contains(编号或姓名));
            }
            //部门为空，则查询所有学生
            if (部门ID == Guid.Empty)
            {
                返回总条数 = query.Count();
                //先按班级分，再按学号排序
                var group = query.ToList().GroupBy(a => a.部门ID).ToList();             
                List<考生> list = new List<考生>();
                foreach (var subGroup in group)
                {
                    List<考生> rankSubGroup = subGroup.OrderBy(a => a.编号).ToList();
                    list.AddRange(rankSubGroup);
                }
                list = list.Skip(第几页 * 页的大小).Take(页的大小).ToList();
                return list;

            }
            else
            {
                query = query.Where(a => a.部门ID == 部门ID);
                返回总条数 = query.Count();
                List<考生> list = query.OrderBy(a => a.编号).Skip(第几页 * 页的大小).Take(页的大小).ToList();
                return list;
            }
        }



        public static List<考生> 查询考生(Guid 部门ID,out string 部门名称)
        {
            IQueryable<考生> query = 考生查询;
            部门 department = new 部门();
            if (部门ID == Guid.Empty)
            {
                //为空则不查询
                部门名称 = string.Empty;
                return new List<考生>();
            }
            department = 部门.部门查询.Where(a => a.ID == 部门ID).First();
            query = query.Where(a => a.部门ID == 部门ID);
            List<考生> listUser = query.ToList().OrderBy(a => a.编号).ToList();
            //给考生列表赋值部门属性
            foreach (考生 user in listUser)
            {
                user.部门 = department;
            }
            部门名称 = department.名称;
            return listUser;
        }



        public static DataSet 查询导出考生(Guid 部门ID, out string 部门名称)
        {
            List<考生> list = 查询考生(部门ID,out 部门名称);
            List<导出考生> listOut = list.Select(a => new 导出考生
            {
                班级 = a.部门.名称,
                姓名 = a.姓名,
                性别名称 = a.性别名称,
                学号 = a.编号
            }).ToList();
            DataTable table = ListConvertToDataTable<导出考生>(listOut,new List<string>());                    
            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            return ds;
        }





        /// <summary>
        /// List集合转化为DataTable
        /// </summary>
        /// <param name="list">泛型集合</param>
        /// <param name="listName">不被转化的属性名集合</param>
        /// <returns></returns>
        public static DataTable ListConvertToDataTable<T>(List<T> list, List<string> listName)
        {
            DataTable table = CreateTable<T>(listName);
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    if (!listName.Contains(prop.Name))
                    {
                        row[prop.Name] = prop.GetValue(item);
                    }
                }

                table.Rows.Add(row);
            }

            return table;
        }

       


       
       
        /// <param name="listName">不被转化的属性名集合</param>
        private static DataTable CreateTable<T>(List<string> listName)
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                if (!listName.Contains(prop.Name))
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            return table;
        }




        public static void 删除考生(Guid 考生ID)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            用户表 user = db.用户表.First(a => a.ID == 考生ID);
            db.用户表.DeleteObject(user);
            db.SaveChanges();
        }



        public static 考生 把考生表转化成考生(用户表 考生表)
        {
            考生 user = new 考生();
            user.ID = 考生表.ID;
            user.编号 = 考生表.编号;
            user.部门ID = 考生表.部门ID.Value;
            user.创建时间 = 考生表.添加时间;
            user.角色 = 考生表.角色;
            user.姓名 = 考生表.姓名;
            user.性别 = 考生表.性别;
            user.邮箱 = 考生表.邮箱;
            return user;
        }

        #endregion
    }
}
