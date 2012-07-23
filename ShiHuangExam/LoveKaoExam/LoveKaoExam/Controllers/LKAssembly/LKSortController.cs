using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKaoExam.Data;

using System.Text.RegularExpressions;
using LoveKaoExam.Library.CSharp;
using LoveKao.Page;

namespace LoveKaoExam.Controllers.LKAssembly
{
    [Authorize(Roles = "考官")]
    public class LKSortController : Controller
    {
        [HttpPost]
        public JsonResult Autocomplete()
        {
            try
            {
                string sName = LKExamURLQueryKey.GetString("name_startsWith");
                int iNum = LKExamURLQueryKey.GetInt32("maxRows");

                List<string> _List分类 = 分类.得到分类智能提示(sName, iNum);

                return LKPageJsonResult.Success(_List分类);
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 自动提示相似分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AutocompleteInXTBJ()
        {
            try
            {
                string sName = LKExamURLQueryKey.GetString("q");
                int iNum = LKExamURLQueryKey.GetInt32("limit");
                List<分类名称和分类类别名称> l自动提示相似分类 = 分类.得到分类输入提示框数据源(sName, iNum);
                return LKPageJsonResult.Success(l自动提示相似分类);
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 最近使用分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ZJSY()
        {
            try
            {
                List<分类名称和分类类别名称> list = 分类.得到某会员最近使用分类(UserInfo.CurrentUser.用户ID, 10);
                return LKPageJsonResult.Success(new { 最近使用分类 = list });
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }
    }
}
