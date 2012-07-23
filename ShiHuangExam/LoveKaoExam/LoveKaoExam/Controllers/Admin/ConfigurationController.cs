using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace LoveKaoExam.Controllers.Admin
{
    /// <summary>
    /// 管理员配置控制器
    /// </summary>
    [Authorize(Roles = "管理员")]
    public class ConfigurationController : BaseController
    {
        /// <summary>
        /// 环境配置
        /// </summary>
        /// <returns></returns>
        public ActionResult Environment()
        {
            return View("~/Views/Admin/Configuration/Environment.aspx");
        }

        /// <summary>
        /// 环境配置
        /// </summary>
        /// <param name="enType">环境变量</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Environment(string enType)
        {
            string filename = Server.MapPath("/web.config");
            string KeyName;//键名称

            XmlDocument xmldoc = new XmlDocument();

            #region try/catch(){}
            try
            {
                xmldoc.Load(filename);
            }
            catch
            {
                return new JsonResult
                {
                    Data = new { result = false, info = "读文件时错误,请检查 Web.config 文件是否存在!" }
                };
            }
            #endregion

            XmlNodeList DocdNodeNameArr = xmldoc.DocumentElement.ChildNodes;//文档节点名称数组

            #region foreach
            foreach (XmlElement DocXmlElement in DocdNodeNameArr)
            {
                if (DocXmlElement.Name.ToLower() == "appsettings")//找到名称为 appsettings 的节点
                {
                    XmlNodeList KeyNameArr = DocXmlElement.ChildNodes;//子节点名称数组
                    if (KeyNameArr.Count > 0)
                    {
                        foreach (XmlElement xmlElement in KeyNameArr)
                        {
                            KeyName = xmlElement.Attributes["key"].InnerXml;//键值
                            switch (KeyName)
                            {
                                case "environment":
                                    xmlElement.Attributes["value"].Value = enType == "0" ? "学校" : "企业";
                                    break;
                            }
                        }
                    }
                }
            }
            #endregion

            #region try/catch(){}
            try
            {
                xmldoc.Save(filename);
                return new JsonResult
                {
                    Data = new { result = true, info = "石黄高速管理处在线考试系统环境已配置完成" }
                };
            }
            catch
            {
                List<string> listInfo = new List<string>();
                listInfo.Add("设置 Web.config 文件属性 '只读' 前面的沟去掉");
                listInfo.Add("设置 Web.config 文件权限 '修改' 后面的沟打上");
                return new JsonResult
                {
                    Data = new { result = false, info = listInfo }
                };

            }
            #endregion
        }


    }
}
