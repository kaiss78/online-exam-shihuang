using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using LoveKaoExam.Library.CSharp;
using LoveKaoExam.Models;
using System.Web.UI.WebControls;
using System.Net;
using LoveKao.Page;

namespace LoveKaoExam.Controllers.LKAssembly
{
    [Authorize(Roles = "考官")]
    public class LKImagesController : Controller
    {
        /// <summary>
        /// 添加试题图片
        /// </summary>
        /// <returns></returns>
        public void AddSubjectImages()
        {
            JsonResult jsonResult = null;
            try
            {
                /* 客户端上载文件 */
                HttpPostedFileBase postFile = Request.Files["InputFile"];

                /* 当前用户信息 */
                UserInfo cUserInfo = UserInfo.CurrentUser;

                /* 当前用户名 */
                string sUserName = cUserInfo.用户名;

                #region 如果没有登录，则抛出异常
                if (cUserInfo.用户ID == Guid.Empty)
                {
                    throw new Exception("您还没登录");
                }
                #endregion

                #region 如果文件长度超过3M，则提示不能超过
                if (postFile.ContentLength > 1024 * 1024 * 3)
                {
                    throw new Exception("上传的图片不能超过3M");
                }
                #endregion

                #region 文件长度符合要求
                else
                {
                    //指定路径字符串的扩展名
                    string ext = System.IO.Path.GetExtension(postFile.FileName).ToLower();

                    //图片文件格式不符合要求
                    if (ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif" && ext != ".png")
                    {
                        throw new Exception("图片格式不正确，请上传jpg,jpeg,bmp,gif格式的图片");
                    }
                    else
                    {
                        //新图片文件命名
                        string sImgFileName = "LoveKaoQn" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;

                        //新图片文件虚拟路径
                        string sVirtualPath = "~/UploadFiles/" + sUserName + "/Images/";

                        //web请求
                        HttpServerUtility httpServerUtility = System.Web.HttpContext.Current.Server;

                        #region 如果图片文件所在的文件夹不存在，则创建目录
                        if (!Directory.Exists(httpServerUtility.MapPath(sVirtualPath)))
                        {
                            //按虚拟路径创建所有目录和子目录
                            Directory.CreateDirectory(@httpServerUtility.MapPath(sVirtualPath));
                        }
                        #endregion

                        //新图片文件完整路径
                        string sFullPath = httpServerUtility.MapPath(sVirtualPath + sImgFileName);

                        #region 如果原有图片不存在，则另存为
                        if (!System.IO.File.Exists(sFullPath))
                        {
                            postFile.SaveAs(sFullPath);
                        }

                        /* 释放流占的资源 */
                        postFile.InputStream.Dispose();
                        postFile.InputStream.Close();
                        postFile = null;
                        #endregion

                        #region jsonResult
                        jsonResult = LKPageJsonResult.GetData(new
                        {
                            result = true,
                            info = new
                            {
                                userName = sUserName,
                                imgName = sImgFileName
                            }
                        });
                        #endregion
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                jsonResult = LKPageJsonResult.Exception(ex);   
            }

            /* 对象转化为Json格式字符串 */
            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(jsonResult.Data);

            /* 将Json格式字符串输出到页面 */
            Response.Write(jsonText);
        }

        /// <summary>
        /// 返回试题图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSubjectImages()
        {
            try
            {
                List<string> l图片文件集合 = new List<string>();

                string sUserName = UserInfo.CurrentUser.用户名;
                string mapPath = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/" + sUserName + "/Images");
                //如果没有则创建目录
                if (!Directory.Exists(mapPath))
                {
                    Directory.CreateDirectory(@mapPath);
                }
                //获取目录下的文件
                else
                {
                    DirectoryInfo userDir = new DirectoryInfo(mapPath);
                    FileInfo[] fileInfo = userDir.GetFiles();

                    //文件数存在一个或一个以上
                    if (fileInfo.Length > 0)
                    {
                        foreach (FileInfo fileItem in fileInfo)
                        {
                            string sFileName = fileItem.Name;
                            string ext = Path.GetExtension(sFileName).ToLower();
                            if (ext == ".jpg" || ext == ".jepg" || ext == ".bmp" || ext == ".gif" || ext == ".png")
                            {
                                l图片文件集合.Add(sFileName);
                            }
                        }
                    }
                }
                return LKPageJsonResult.Success(l图片文件集合);
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 删除试题图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DelSubjectImages()
        {
            try
            {
                string sUserName = UserInfo.CurrentUser.用户名;
                string fileName = LKExamURLQueryKey.GetString("FileName");
                string mapPath = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/" + sUserName + "/Images");
                string fileNamePath = mapPath + "/" + fileName;

                System.IO.File.Delete(fileNamePath);
                return LKPageJsonResult.Success(fileName);
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }

    }
}
