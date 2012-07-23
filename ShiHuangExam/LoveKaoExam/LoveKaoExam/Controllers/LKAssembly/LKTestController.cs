using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKaoExam.Library.CSharp;
using LoveKaoExam.Data;
using System.Text;
using LoveKaoExam.Models.Examiner;
using LoveKao.Members.Test;
using LoveKaoExam.Controllers.Examinee;
using LoveKao.Page;

namespace LoveKaoExam.Controllers.LKAssembly
{
    [Authorize(Roles = "考官")]
    public class LKTestController : BaseController 
    {
        /// <summary>
        /// 添加题型
        /// </summary>
        /// <returns></returns>
        public ActionResult SubjectType()
        {

            return View("~/Views/LKAssembly/IFrame/SubjectType.htm");
        }

        /// <summary>
        /// 查找题型
        /// </summary>
        /// <returns></returns>
        public ActionResult FindSubType()
        {

            return View("~/Views/LKAssembly/IFrame/FindSubType.htm");
        }

        /// <summary>
        /// 判断是否是随机生成试卷的分类
        /// </summary>
        /// <returns></returns>   
         [HttpPost]
        public JsonResult IsRandomTestSort()
        {
            try
            {
                string strSort = LKExamURLQueryKey.GetString("randomSort"); //Request["randomSort"];
                List<分类名称和分类类别名称> _ListSort = 分类.判断分类是否存在(strSort);
                if (_ListSort.Count == 0)
                {
                    return LKPageJsonResult.Success();
                }
                else
                {

                    return LKPageJsonResult.Failure(_ListSort);
                }
            }
            catch (Exception ex)
            {

                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 随机生成试卷
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RandomTest()
        {
            try
            {
                //type.题型Enum=11;
                ////itjian.取题题型及数量集合=new List<取题题型及数量>();
                string strRandomJson = LKExamURLQueryKey.GetString("randomDataJson");// Request["randomDataJson"];
                if (!string.IsNullOrEmpty(strRandomJson))
                {
                    int isEnough;
                    Guid guid = 试卷外部信息.随机生成试卷(strRandomJson,out isEnough);
                    return LKPageJsonResult.GetData(new { result = false, info = guid, outInfo = isEnough });

                }
                else
                {
                    return LKPageJsonResult.Failure();
                }

            }
            catch (Exception ex)
            {

                return LKPageJsonResult.Exception(ex);
            }

        }

        /// <summary>
        /// 查找试题
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FindSubject(查找试题 m查找试题)
        {
            try
            {
                string strSubType = LKExamURLQueryKey.GetString("subType[]");
                string strSort = LKExamURLQueryKey.GetString("sortJson[]");
                int pageIndex = LKExamURLQueryKey.GetInt32("pageIndex");
                int pageSize = LKExamURLQueryKey.GetInt32("pageSize");
                Guid guid = LKExamURLQueryKey.GetGuid("userID");

                取题条件 getSubItem = new 取题条件();
                getSubItem.题干关键字 = m查找试题.subContent;
                getSubItem.会员ID = m查找试题.userID;
                getSubItem.网站分类集合 = strSort;
                if (!string.IsNullOrEmpty(strSubType))
                {
                    取题题型及数量 listSubType = new 取题题型及数量();
                    listSubType.题型Enum = Convert.ToByte(strSubType);
                    getSubItem.取题题型及数量集合 = new List<取题题型及数量>();
                    getSubItem.取题题型及数量集合.Add(listSubType);
                }
               
                int count = 0;
                if (!Convert.IsDBNull(getSubItem))
                {

                    List<试题外部信息> subData = 试题外部信息.手工出卷查询题目(getSubItem, pageIndex, pageSize, out count);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("[");
                    foreach (试题外部信息 item in subData)
                    {

                        sb.Append(试题内容.转化成预览试题Json字符串带答案(item.当前试题内容, item) + ",");

                    }

                    int len = sb.Length;

                    string jsonText = len > 1 ? sb.ToString(0, len - 1) + "]" : "[]";
                    return LKPageJsonResult.GetData(new { result = true, subData = jsonText, count = count });

                }
                else
                {
                    return LKPageJsonResult.Failure();
                }
            }
            catch (Exception ex)
            {

                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 保存试卷
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult SaveTest()
        {
            try
            {

                string _testContent = LKExamURLQueryKey.GetString("testContent");
                if (!string.IsNullOrEmpty(_testContent))
                {
                    试卷外部信息.保存试卷相关信息(_testContent);

                    return LKPageJsonResult.Success();
                }
                else
                {
                    return LKPageJsonResult.Failure();

                }
            }
            catch (Exception ex)
            {

                return LKPageJsonResult.Exception(ex);

            }
        }

        /// <summary>
        /// 得到正常试卷
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetNormalTest()
        {
            try
            {
                string strGuid = LKExamURLQueryKey.GetString("guid"); //Request["guid"];
                if (!string.IsNullOrEmpty(strGuid))
                {
                    试卷外部信息 outside = 试卷外部信息.得到符合ID的试卷(new Guid(strGuid));
                    string testText = 试卷内容.转化成完整Json字符串试题内容带答案(outside.当前试卷内容, outside);

                    return LKPageJsonResult.Success(testText);
                }
                else
                {
                    return LKPageJsonResult.Failure();
                }
            }
            catch (Exception ex)
            {

                return LKPageJsonResult.Exception(ex);
            }

        }

        /// <summary>
        /// 得到随机试卷
        /// </summary>
        /// <returns></returns>
       [HttpPost]
        public JsonResult GetRandomTest()
        {
            try
            {
                string strGuid = LKExamURLQueryKey.GetString("guid");
                if (!string.IsNullOrEmpty(strGuid))
                {
                    试卷外部信息 randomTestText = 试卷外部信息.得到随机试卷根据外部ID(new Guid(strGuid));
                    string JsonData = 试卷内容.转化成完整Json字符串试题内容带答案(randomTestText.当前试卷内容, randomTestText);
                    return LKPageJsonResult.Success(JsonData);
                }
                else
                {
                    return LKPageJsonResult.Failure();
                }

            }
            catch (Exception ex)
            {

                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 修改试卷
        /// </summary>
        /// <returns></returns>
       [ValidateInput(false)]
       [HttpPost]
       public JsonResult EditTest(int 试卷状态)
        {
            try
            {
                string _editTestContent = LKExamURLQueryKey.GetString("editTestContent");// Request["editTestContent"];
                if (!string.IsNullOrEmpty(_editTestContent))
                {
                    if (试卷状态==0)
                    {
                        试卷外部信息.修改正常试卷(_editTestContent);
                    }
                    else
                    {
                        试卷外部信息.修改草稿试卷(_editTestContent);
                    }
                    
                    return LKPageJsonResult.Success();

                }
                else
                {

                    return LKPageJsonResult.Failure();
                }
            }
            catch (Exception ex)
            {

                return LKPageJsonResult.Exception(ex);
            }

        }

        /// <summary>
        /// 修改草稿
        /// </summary>
       [ValidateInput(false)]
       [HttpPost]
        public JsonResult EditDraft()
        {
            try
            {

                string _editTestContent = LKExamURLQueryKey.GetString("editDraftContent");// Request["editTestContent"];
                if (!string.IsNullOrEmpty(_editTestContent))
                {
                    试卷外部信息.修改草稿试卷(_editTestContent);
                    return LKPageJsonResult.Success();

                }
                else {
                    return LKPageJsonResult.Failure();
                }

            }
            catch (Exception ex)
            {

                return LKPageJsonResult.Exception(ex);
            }


        }

        /// <summary>
        /// 得到预览试卷
        /// </summary>
        /// <param name="g试卷内容ID"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
       [HttpPost]
        public static string GetTestViewHTML(试卷内容 c试卷内容, string sKey, bool isShowTitle)
        {

            string sHtml = "",
                   sHtml试题 = "";

            序号对象类 序号对象 = new 序号对象类();
            if (isShowTitle)
            {
                sHtml += "<div style=\"font-family:Verdana, Arial,宋体, Sans-Serif, Helvetica;font-size:14px;\">";
                sHtml += "<div style=\"text-align:center;\">" +
                            "<div class=\"ViewTest_I_H_Title\" style=\"font-size:30px;font-weight:bold;\">" + c试卷内容.名称 + "</div>" +
                            "<div class=\"ViewTest_I_H_Count\">总分：" + c试卷内容.总分 + "</div>" +
                        "</div>";
            }
            foreach (试卷中大题 题型 in c试卷内容.试卷中大题集合)
            {
                sHtml试题 = "";
                foreach (试卷大题中试题 试题 in 题型.试卷大题中试题集合)
                {

                    if (sKey != "1")
                    {
                        sHtml试题 += ViewSubject.getMarkItem(试题, 序号对象, 0, false, false);

                    }
                    else
                    {

                        sHtml试题 += ViewSubject.getMarkItem(试题, 序号对象, 0, false, true);
                    }
                }

                sHtml += "<div class=\"ViewTestContent\">";
                sHtml += "<div class=\"C_E_L_I_C_R_C_Title\">" +
                        "<div class=\"C_E_L_I_C_R_C_T_SubType\"><b>" + 题型.名称 + "</b></div>" +
                        "<div class=\"C_E_L_I_C_R_C_T_Explain\">说明:" + 题型.说明 + "</div>" +
                        "</div>";
                sHtml += "<div class=\"C_E_L_I_C_R_C_Content\" >" +
                        "<div class=\"C_E_L_I_C_R_C_Font\">" + sHtml试题 + "</div>" +
                        "</div>";
                sHtml += "</div>";
            }
            if (isShowTitle)
            {
                sHtml += "</div>";


            }
            return sHtml;
        }

        /// <summary>
        /// 老师的分析试卷
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTeaMadeExam()
        {
          return  GetMadeExam();
        }
        

        /// <summary>
        /// 保存批改
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult SaveRarting()
        { 
            try
            {
                string _testJson = LKExamURLQueryKey.GetString("textJson");
                if (!string.IsNullOrEmpty(_testJson))
                {
                    批改试卷.提交保存批改(_testJson);
                    return LKPageJsonResult.Success();

                }
                else
                {
                    return LKPageJsonResult.Failure();
                }
            }
            catch (Exception ex)
            {

                return LKPageJsonResult.Exception(ex);
            }

        }
    }
}