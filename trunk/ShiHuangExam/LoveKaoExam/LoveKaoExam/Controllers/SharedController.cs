using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKaoExam.Library.CSharp;
using System.Web.UI;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using LoveKao.Page;

namespace LoveKaoExam.Controllers
{

    public class SharedController : Controller
    {
        /// <summary>
        /// 如果验证码E:\LoveKao项目\LoveKao团队\LoveKaoExam\LoveKaoExam\Controllers\HomeController.cs
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "考官")]
        public ActionResult ImageCode()
        {
            LKPageImageCode lkPageImageCode = new LKPageImageCode();

            string code = lkPageImageCode.CreateVerifyCode();                //取随机码
            lkPageImageCode.CreateImageOnPage(code);        // 输出图片

            Response.Cookies.Add(new HttpCookie("LKExamImageCode", code.ToUpper()));// 使用Cookies取验证码的值

            return View();
        }

        

        static object temp = new object();

        /// <summary>
        /// 空格图片下划线
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "考官")]
        public ActionResult UnderLine()
        {

            string sVirtualPath = @"/Content/File/UnderLine/";

            //web请求
            HttpServerUtility httpServerUtility = System.Web.HttpContext.Current.Server;
            if (!Directory.Exists(httpServerUtility.MapPath(sVirtualPath)))
            {
                //按虚拟路径创建所有目录和子目录
                Directory.CreateDirectory(@httpServerUtility.MapPath(sVirtualPath));
            }
            
            string index = "1";
            string reIndex = Request["index"];
            if (!string.IsNullOrEmpty(reIndex))
            {
                int n;
                index = int.TryParse(reIndex, out n) ? n.ToString() : "1";
            }

            lock (temp)
            {
                string dFilePath = Server.MapPath(sVirtualPath + "UnderLine" + index + ".png");

                //判断图片是否存在
                #region 验证空格图片是否存在
                if (System.IO.File.Exists(dFilePath))
                {
                    Response.ClearContent(); //需要输出图象信息 要修改HTTP头
                    Response.ContentType = "image/Png";
                    Response.BinaryWrite(System.IO.File.ReadAllBytes(dFilePath));
                    Response.End();
                }
                #endregion

                #region 创建新空格图片
                else
                {
                    string filePath = Server.MapPath(sVirtualPath + "UnderLine.gif");
                    Bitmap bgImg = (Bitmap)Bitmap.FromFile(filePath);

                    int width = 36;
                    int height = 18;

                    Bitmap theBitmap = new Bitmap(width, height);
                    Graphics theGraphics = Graphics.FromImage(theBitmap);
                    theGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    theGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    theGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    theGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    //  白色背景
                    //theGraphics.Clear(Color.White);
                    theGraphics.DrawImage(bgImg, new Rectangle(0, 0, width, height), new Rectangle(0, 0, bgImg.Width, bgImg.Height), GraphicsUnit.Pixel);
                    //13pt的字体
                    float fontSize = height * 1.0f / 1.38f;
                    Font theFont = new Font("Arial", fontSize);
                    System.Drawing.Drawing2D.GraphicsPath gp = null;


                    int indLen = index.Length;

                    Point thePos = new Point((int)(fontSize - (indLen > 1 ? indLen * 2.5 : indLen * 2)), 2);
                    gp = new System.Drawing.Drawing2D.GraphicsPath();
                    gp.AddString(index, theFont.FontFamily, 0, fontSize, thePos, new StringFormat());

                    theGraphics.DrawPath(new Pen(Color.White, 2f), gp);
                    theGraphics.FillPath(new SolidBrush(Color.Red), gp);
                    theGraphics.ResetTransform();


                    if (gp != null) gp.Dispose();

                    //  将生成的图片发回客户端
                    MemoryStream ms = new MemoryStream();
                    theBitmap.Save(ms, ImageFormat.Png);

                    //保存文件
                    theBitmap.Save(dFilePath);

                    Response.ClearContent(); //需要输出图象信息 要修改HTTP头
                    Response.ContentType = "image/Png";
                    Response.BinaryWrite(ms.ToArray());
                    theGraphics.Dispose();
                    theBitmap.Dispose();
                    Response.End();
                }
                #endregion
            }

            return View();
        }
    }
}
