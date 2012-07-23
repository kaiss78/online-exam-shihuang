using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;


namespace LoveKaoExam.Data
{
    public class 上传下载信息
    {
        #region 属性

        public int 已上传试题数量
        {
            get;
            set;
        }


        public int 已下载试题数量
        {
            get;
            set;
        }


        public int 已上传试卷数量
        {
            get;
            set;
        }


        public int 已下载试卷数量
        {
            get;
            set;
        }


        public int 可下载试题数量
        {
            get;
            set;
        }


        public bool 是否绑定账号
        {
            get;
            set;
        }

        #endregion



        #region 方法

        public static 上传下载信息 得到上传下载信息(Guid 用户ID)
        {
            try
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                上传下载信息 information = new 上传下载信息();
                绑定账号表 bind = db.绑定账号表.Where(a=>a.本地账号ID==用户ID).FirstOrDefault();
                if (bind == null)
                {
                    information.可下载试题数量 = 100;
                    information.是否绑定账号 = false;
                }
                else
                {
                    LoveKaoServiceReference.LoveKaoServiceInterfaceClient client = new LoveKaoServiceReference.LoveKaoServiceInterfaceClient();
                    int result;
                    string Json = client.得到上传下载信息(out result, bind.爱考网账号, bind.爱考网密码);
                    client.Close();
                    if (result == 1)
                    {
                        异常处理.抛出异常(1);
                    }
                    else if (result == 2)
                    {
                        异常处理.抛出异常(2);
                    }

                    if (String.IsNullOrEmpty(Json))
                    {
                        information.可下载试题数量 = 100;
                        information.是否绑定账号 = false;
                    }
                    else
                    {
                        information = 把Json转化成上传下载信息(Json);
                        information.是否绑定账号 = true;
                    }
                }
                return information;
            }
            catch (Exception ex)
            {
                异常处理.Catch异常处理(ex.Message);
                throw;
            }           
        }



        public static 上传下载信息 把Json转化成上传下载信息(string Json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            上传下载信息 information = jss.Deserialize<上传下载信息>(Json);
            return information;
        }
        #endregion
    }
}
