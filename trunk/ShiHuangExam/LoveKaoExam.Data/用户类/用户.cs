using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Transactions;
using System.Net;
using LoveKao.Page;

namespace LoveKaoExam.Data
{
    public class 用户
    {
        #region 变量

        private 部门 _部门;

        #endregion



        #region 属性

        public Guid ID
        {
            get;
            set;
        }

        public string 编号
        {
            get;
            set;
        }


        public string 密码
        {
            get;
            set;
        }


        public string 姓名
        {
            get;
            set;
        }


        /// <summary>
        ///  0保密，1男，2女
        /// </summary>
        public byte 性别
        {
            get;
            set;
        }


        public string 性别名称
        {
            get
            {
                if (this.性别 == 0)
                {
                    return "保密";
                }
                else if (this.性别 == 1)
                {
                    return "男";
                }
                else
                {
                    return "女";
                }
            }
        }


        public string 邮箱
        {
            get;
            set;
        }


        /// <summary>
        /// 0考生，1考官，2管理员
        /// </summary>
        public byte 角色
        {
            get;
            set;
        }


        public DateTime 创建时间
        {
            get;
            set;
        }


        public Guid? 部门ID
        {
            get;
            set;
        }


        [JsonIgnore]
        public 部门 部门
        {
            get
            {
                if (_部门 == null)
                {
                    if (this.部门ID != null)
                    {
                        _部门 = 部门.部门查询.Where(a => a.ID == this.部门ID.Value).FirstOrDefault();
                    }
                    return _部门;
                }
                else
                {
                    return _部门;
                }
            }
        }


        public static IQueryable<用户> 用户查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.用户表.Select(a => new 用户
                    {
                        ID = a.ID,
                        编号 = a.编号,
                        部门ID = a.部门ID.Value,
                        密码 = a.密码,
                        姓名 = a.姓名,
                        性别 = a.性别,
                        邮箱 = a.邮箱,
                        创建时间=a.添加时间,
                        角色=a.角色
                    });
            }
        }

        #endregion



        #region 方法

        /// <summary>
        /// 返回0成功，1编号不存在,2密码错误
        /// </summary>        
        public static int 登录(string 编号, string 密码, out 用户 用户)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            用户 user  = 用户查询.Where(a => a.编号 == 编号).FirstOrDefault();          
            if (user == null)
            {
                用户 = null;
                return 1;
            }
            else
            {
                用户 = user;
                if (user.密码 == 密码)
                {
                    return 0;
                }
                else
                {
                    return 2;
                }
            }

        }



        /// <summary>
        /// 返回0成功，1用户名已被他人使用，2邮箱已被他人使用，3已经绑定过账号
        /// </summary>      
        public static int 绑定新账号(string 用户名, string 密码, string 邮箱)
        {
            try
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                if (db.绑定账号表.Any(a => a.本地账号ID == 用户信息.CurrentUser.用户ID) == true)
                {
                    异常处理.抛出异常(6);
                }
                IPHostEntry IPHost = Dns.Resolve(Dns.GetHostName());
                string IPAddress = IPHost.AddressList[0].ToString();
                LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                int result = client.绑定新账号(用户名, 密码, 邮箱, IPAddress,用户信息.CurrentUser.用户名);
                client.Close();
                switch (result)
                {
                    case 0:
                        {
                            添加绑定账号(用户名, 密码, 邮箱);
                            break;
                        }
                    case 1:
                        {
                            异常处理.抛出异常(7);
                            break;
                        }
                    case 2:
                        {
                            异常处理.抛出异常(8);
                            break;
                        }
                }  
                return result;
            }
            catch (Exception ex)
            {
                异常处理.Catch异常处理(ex.Message);
                throw;
            }           
        }



        /// <summary>
        /// 返回0成功，1账号不存在，2密码错误，3账号已绑定过，4绑定账号被禁用,5禁止绑定任何账号
        /// </summary>    
        public static int 绑定已有账号(string 用户名, string 密码)
        {
            try
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();                
                IPHostEntry IPHost = Dns.Resolve(Dns.GetHostName());
                string IPAddress = IPHost.AddressList[0].ToString();
                LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                string email = string.Empty;
                string userName = string.Empty;
                int result = client.绑定已有账号(out email,out userName, 用户名, 密码, IPAddress,用户信息.CurrentUser.用户名);
                client.Close();
                switch(result)
                {
                    case 0:
                        {
                             添加绑定账号(userName, 密码, email);
                            break;
                        }
                    case 1:
                        {
                            异常处理.抛出异常(4);
                            break;
                        }
                    case 2:
                        {
                            异常处理.抛出异常(5);
                            break;
                        }
                    case 3:
                        {
                            异常处理.抛出异常(6);
                            break;
                        }
                    case 4:
                        {
                            异常处理.抛出异常(1);
                            break;
                        }
                    case 5:
                        {
                            异常处理.抛出异常(2);
                            break;
                        }
                }                           
                return result;
            }
            catch (Exception ex)
            {
                异常处理.Catch异常处理(ex.Message);
                throw;
            }          
        }



        /// <summary>
        /// 返回0成功，1用户名已被他人使用，2邮箱已被他人使用
        /// </summary>       
        public static int 更改绑定新账号(string 用户名, string 密码, string 邮箱)
        {
            try
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                绑定账号表 bind = db.绑定账号表.Where(a => a.本地账号ID == 用户信息.CurrentUser.用户ID).First();
                IPHostEntry IPHost = Dns.Resolve(Dns.GetHostName());
                string IPAddress = IPHost.AddressList[0].ToString();
                LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                int result = client.更改绑定新账号(用户名, 密码, 邮箱, IPAddress,用户信息.CurrentUser.用户名,bind.爱考网账号);
                client.Close();
                switch (result)
                {
                    case 0:
                        {
                            更新绑定账号(用户名, 密码, 邮箱);
                            break;
                        }
                    case 1:
                        {
                            异常处理.抛出异常(7);
                            break;
                        }
                    case 2:
                        {
                            异常处理.抛出异常(8);
                            break;
                        }
                }                
                return result;
            }
            catch (Exception ex)
            {
                异常处理.Catch异常处理(ex.Message);
                throw;
            }           
        }



        /// <summary>
        /// 返回0成功，1账号不存在，2密码错误，3已绑定过，4绑定账号被禁用,5禁止绑定任何账号
        /// </summary>    
        public static int 更改绑定已有账号(string 用户名, string 密码)
        {
            try
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                绑定账号表 bind = db.绑定账号表.Where(a => a.本地账号ID == 用户信息.CurrentUser.用户ID).First();
                IPHostEntry IPHost = Dns.Resolve(Dns.GetHostName());
                string IPAddress = IPHost.AddressList[0].ToString();
                string email = string.Empty;
                string userName = string.Empty;
                LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                int result = client.更改绑定已有账号(out email,out userName, 用户名, 密码, IPAddress,用户信息.CurrentUser.用户名,bind.爱考网账号);
                client.Close();
                switch (result)
                {
                    case 0:
                        {
                            更新绑定账号(userName, 密码, email);
                            break;
                        }
                    case 1:
                        {
                            异常处理.抛出异常(4);
                            break;
                        }
                    case 2:
                        {
                            异常处理.抛出异常(5);
                            break;
                        }
                    case 3:
                        {
                            异常处理.抛出异常(6);
                            break;
                        }
                    case 4:
                        {
                            异常处理.抛出异常(1);
                            break;
                        }
                    case 5:
                        {
                            异常处理.抛出异常(2);
                            break;
                        }
                }              
                return result;
            }
            catch (Exception ex)
            {
                异常处理.Catch异常处理(ex.Message);
                throw;
            }          
        }



        /// <summary>
        /// 返回0成功，1失败
        /// </summary>
        public static int 解除绑定账号()
        {
            try
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                绑定账号表 bind = db.绑定账号表.Where(a=>a.本地账号ID==用户信息.CurrentUser.用户ID).FirstOrDefault();
                if (bind == null)
                {
                    return 1;
                }
                else
                {
                    LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                    int result = client.解除绑定账号(bind.爱考网账号, bind.爱考网密码,用户信息.CurrentUser.用户名);
                    client.Close();
                    if (result == 0)
                    {
                        //删除本地绑定账号
                        db.绑定账号表.DeleteObject(bind);
                        db.SaveChanges();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                异常处理.Catch异常处理(ex.Message);
                throw;
            }
           
        }



        private static void 添加绑定账号(string 用户名, string 密码, string 邮箱)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            Guid hostUserId=用户信息.CurrentUser.用户ID;
            if (db.绑定账号表.Any(a => a.本地账号ID == hostUserId && a.爱考网账号 == 用户名) == false)
            {
                绑定账号表 bind = new 绑定账号表();
                bind.本地账号ID = hostUserId;
                bind.爱考网账号 = 用户名;
                bind.爱考网密码 = 加密字符串(密码);
                bind.爱考网邮箱 = 邮箱;
                bind.绑定时间 = DateTime.Now;
                db.绑定账号表.AddObject(bind);
                db.SaveChanges();
            }
        }



        private static void 更新绑定账号(string 用户名, string 密码, string 邮箱)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            //删除原来的
            绑定账号表 oldBind = db.绑定账号表.Where(a => a.本地账号ID == 用户信息.CurrentUser.用户ID).FirstOrDefault();
            if (oldBind != null)
            {
                db.绑定账号表.DeleteObject(oldBind);
            }
            //添加新的
            绑定账号表 bind = new 绑定账号表();
            bind.本地账号ID = 用户信息.CurrentUser.用户ID;
            bind.爱考网账号 = 用户名;
            bind.爱考网密码 = 加密字符串(密码);
            bind.爱考网邮箱 = 邮箱;
            bind.绑定时间 = DateTime.Now;
            db.绑定账号表.AddObject(bind);
            db.SaveChanges();
        }


        public static string 加密字符串(string 字符串)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] mdArray = md.ComputeHash(UnicodeEncoding.UTF8.GetBytes(字符串));
            return BitConverter.ToString(mdArray);
        }



        /// <summary>
        /// 返回0成功,1原密码错误
        /// </summary>    
        public static int 修改用户密码(Guid 用户ID, string 原密码, string 新密码)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            用户表 user = db.用户表.FirstOrDefault(a=>a.ID==用户ID);           
            if (user.密码 != 原密码)
            {
                return 1;
            }
            user.密码 = 新密码;
            db.SaveChanges();
            return 0;
        }



        /// <summary>
        /// 返回0成功，1编号重复，2邮箱重复
        /// </summary>      
        public static int 添加用户(用户 用户)
        {
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            //判断编号是否存在
            if (db.用户表.Any(a => a.编号 == 用户.编号) == true)
            {
                return 1;
            }
            if (String.IsNullOrEmpty(用户.邮箱) == false)
            {
                //判断邮箱是否存在
                if (db.用户表.Any(a => a.邮箱 == 用户.邮箱) == true)
                {
                    return 2;
                }
            }
            用户表 user = new 用户表();
            user.ID = Guid.NewGuid();
            user.编号 = 用户.编号;
            if (用户.角色 == 0)
            {
                user.部门ID = 用户.部门ID;
            }
            user.角色 = 用户.角色;
            user.密码 = "123456";
            user.姓名 = 用户.姓名;
            user.性别 = 用户.性别;
            user.邮箱 = 用户.邮箱;
            user.添加时间 = DateTime.Now;
            db.用户表.AddObject(user);
            db.SaveChanges();
            return 0;
        }



        public static 用户 得到用户基本信息根据ID(Guid 用户ID)
        {
            用户 user = 用户查询.Where(a => a.ID==用户ID).FirstOrDefault();
            return user;
        }



        
        /// <summary>
        /// 返回null说明未绑定
        /// </summary>
        /// <param name="result">0正常，1绑定账号被禁用，2禁止绑定任何账号</param>
        public static 绑定账号表 得到用户绑定信息(Guid 用户ID,out LKPageException 异常信息)
        {
            try
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                绑定账号表 bind = db.绑定账号表.Where(a => a.本地账号ID == 用户ID).FirstOrDefault();
                if (bind == null)
                {
                    异常信息 = new LKPageException();
                    return null;
                }
                else
                {
                    LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                    int result = client.得到用户绑定信息(bind.爱考网账号);
                    client.Close();
                    if (result == 1)
                    {
                        异常信息 = new LKPageException(异常处理.得到异常信息(1));
                    }
                    else if (result == 2)
                    {
                        异常信息 = new LKPageException(异常处理.得到异常信息(2));
                    }
                    else
                    {
                        异常信息 = new LKPageException();
                    }
                    return bind;
                }
            }
            catch (Exception)
            {
                异常信息 = new LKPageException(异常处理.得到无法连接爱考网异常信息(),3);
                return new 绑定账号表();
            }          
        }



        /// <summary>
        /// 返回0成功，1编号重复，2邮箱重复
        /// </summary>
        public static int 修改用户个人信息(用户 用户)
        {
            异常处理.删除修改权限判断();
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            用户表 user = db.用户表.FirstOrDefault(a=>a.ID==用户.ID);
            //现在的编号和以前的不一样，说明是修改的，修改的不能重复
            if (user.编号 != 用户.编号)
            {
                if (db.用户表.Any(a => a.编号 == 用户.编号) == true)
                {
                    return 1;
                }
            }
            //邮箱同上
            if (user.邮箱 != 用户.邮箱&&String.IsNullOrEmpty(用户.邮箱)==false)
            {
                if (db.用户表.Any(a => a.邮箱 == 用户.邮箱) == true)
                {
                    return 2;
                }
            }
            //只有管理员能改编号，改编号后密码与新编号相同(密码不变)
            if (用户信息.CurrentUser.用户类型==2&&user.编号!=用户.编号)
            {
                user.编号 = 用户.编号;
                //user.密码 = 用户.编号;
            }
            user.姓名 = 用户.姓名;
            user.性别 = 用户.性别;
            user.邮箱 = 用户.邮箱;
            if (用户.角色 == 0)
            {
                user.部门ID = 用户.部门ID;
            }
            db.SaveChanges();
            return 0;
        }




        /// <param name="用户类型">0考生，1考官，2管理员</param>     
        public static List<用户> 查询用户(int 用户类型,Guid? 部门ID,string 编号或姓名, int 第几页, int 页的大小, out int 返回总条数)
        {
            IQueryable<用户> query = 用户查询.Where(a=>a.角色==用户类型);
            if (部门ID != null)
            {
                query = query.Where(a => a.部门ID == 部门ID);
            }
            if (编号或姓名 != "" && 编号或姓名 != null)
            {
                query = query.Where(a => a.编号.Contains(编号或姓名) || a.姓名.Contains(编号或姓名));
            }
            返回总条数 = query.Count();
            List<用户> list = query.OrderBy(a => a.编号).Skip(第几页 * 页的大小).Take(页的大小).ToList();
            return list;
        }



       


        public static void 删除用户(Guid 用户ID)
        {
            异常处理.删除修改权限判断();
            LoveKaoExamEntities db = new LoveKaoExamEntities();
            List<系统分类表> listSystem = db.系统分类表.Where(a=>a.操作人ID==用户ID).ToList();
            List<string> listSystemSort=listSystem.Select(a=>a.分类名称).ToList();
            List<系统分类上下级关系表> listRelation = db.系统分类上下级关系表.Where(a => listSystemSort.Contains(a.系统分类名称)
                || listSystemSort.Contains(a.分类关系名称)).ToList();
            foreach (var relation in listRelation)
            {
                db.系统分类上下级关系表.DeleteObject(relation);
            }
            foreach (var system in listSystem)
            {
                db.系统分类表.DeleteObject(system);
            }
            List<试卷外部信息表> listTest = db.试卷外部信息表.Where(a => a.创建人ID == 用户ID).ToList();
            foreach (var test in listTest)
            {
                db.试卷外部信息表.DeleteObject(test);
            }
            List<试题外部信息表> listProblem = db.试题外部信息表.Where(a => a.创建人ID == 用户ID).ToList();
            foreach (var problem in listProblem)
            {
                db.试题外部信息表.DeleteObject(problem);
            }
            用户表 user = db.用户表.First(a => a.ID == 用户ID);
            db.SaveChanges();
        }

        #endregion
    }
}
