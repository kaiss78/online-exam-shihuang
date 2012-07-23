using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using LoveKaoExam.Data;
using LoveKaoExam.Library.CSharp;
using LoveKao.Page;

namespace LoveKaoExam.Controllers.Examiner
{
    [Authorize(Roles = "考官")]
    public class OrganizationController : BaseController
    {
        /// <summary>
        /// 已组织考试列表
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult Exam(int? id)
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            /* 已组织考试PagedList */
            PagedList<考试设置> 已组织考试PagedList = 考官已组织考试列表(id);

            /* 已组织考试信息视图 */
            LKExamMvcPagerData<考试设置>.数据信息(已组织考试PagedList, MvcPager引用目标.考官_已组织考试, ViewData);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Examiner/Organization/UCExam.ascx", 已组织考试PagedList);
            }

            /* 返回已组织考试视图 */
            return View("~/Views/Examiner/Organization/Exam.aspx", 已组织考试PagedList);
        }

        /// <summary>
        /// 已组织练习列表
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult Practice(int? id)
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            /* 已组织练习PagedList */
            PagedList<练习设置> 已组织练习PagedList = 考官已组织练习列表(id);

            /* 已组织练习信息视图 */
            LKExamMvcPagerData<练习设置>.数据信息(已组织练习PagedList, MvcPager引用目标.考官_已组织练习, ViewData);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Examiner/Organization/UCPractice.ascx", 已组织练习PagedList);
            }

            /* 返回已组织练习视图 */
            return View("~/Views/Examiner/Organization/Practice.aspx", 已组织练习PagedList);
        }

        /// <summary>
        /// 试卷设置 考生范围
        /// </summary>
        /// <param name="id">当前页数</param>
        /// <returns></returns>
        public ActionResult Setup(int? id)
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            /* 考生范围PagedList */
            PagedList<考生> 考生范围PagedList = 考生信息列表(id);

            /* 考生范围信息视图 */
            LKExamMvcPagerData<考生>.数据信息(考生范围PagedList, MvcPager引用目标.考官_组织考试或练习设置考生范围, ViewData);

            /* 用户操作类型 */
            int 用户操作类型 = LKExamURLQueryKey.GetInt32("uHandleType");

            /* 设置类型 */
            int 试卷设置类型 = LKExamURLQueryKey.GetInt32("testSetType");

            试卷设置 view试卷设置 = new 试卷设置();
            view试卷设置.考生ID集合 = new List<Guid>();
            view试卷设置.设置类型 = 试卷设置类型;

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                ViewData["试卷设置"] = view试卷设置;
                return PartialView("~/Views/Examiner/Organization/UCSetup.ascx", 考生范围PagedList);
            }
            else
            {
                /*
                 * 试卷设置ID
                 * 如果是组织试卷 试卷设置ID 为model.试卷内容ID
                 * 如果是修改考试 试卷设置ID 为model.ID
                 * 如果是修改练习 试卷设置ID 为model.试卷内容ID
                 */
                Guid 试卷设置ID = LKExamURLQueryKey.GetGuid("testSetID");

                /* 修改试卷设置 */
                if (用户操作类型 == 1)
                {
                    view试卷设置 = 试卷设置.得到某试卷设置根据ID(试卷设置ID, 试卷设置类型);
                }
                /* 组织试卷是设置 */
                else
                {
                    view试卷设置.试卷内容ID = 试卷设置ID;/* 考试，练习 */
                    view试卷设置.及格条件 = 60;
                    view试卷设置.考试时长 = 90;
                    view试卷设置.考试开始时间 = DateTime.Now;
                    view试卷设置.考试结束时间 = DateTime.Now.AddMinutes(view试卷设置.考试时长);
                    view试卷设置.是否公布考试结果 = true;
                }
                ViewData["试卷设置"] = view试卷设置;
            }
            /* 返回考生范围视图 */
            return View("~/Views/Examiner/Organization/Setup.aspx", 考生范围PagedList);
        }

        /// <summary>
        /// 试卷设置 考生范围
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetupUCEditor(试卷设置 model)
        {
            /* 用户操作类型 */
            int 用户操作类型 = LKExamURLQueryKey.GetInt32("uHandleType");

            /* 考生ID数组 */
            string[] 考生ID数组 = LKExamURLQueryKey.GetString("考生ID集合[]").Split(",".ToArray());

            #region 考生ID集合
            List<Guid> 考生ID集合 = new List<Guid>();
            foreach (var item in 考生ID数组)
            {
                Guid 考生ID = LKExamURLQueryKey.SToGuid(item);
                if (考生ID != Guid.Empty)
                {
                    考生ID集合.Add(考生ID);
                }
            }
            #endregion

            #region try/catch(){}
            try
            {
                model.设置人ID = UserInfo.CurrentUser.用户ID;
                model.考生ID集合 = 考生ID集合;

                if (用户操作类型 == 1)
                {
                    试卷设置.修改试卷设置(model);
                }
                else
                {
                    试卷设置.添加试卷设置(model);
                }

                /* 返回执行成功的Json字符串 */
                return LKPageJsonResult.Success();
            }
            catch (Exception ex)
            {
                /* 返回异常后Json格式字符串 */
                return LKPageJsonResult.Exception(ex);
            }
            #endregion
        }

        [HttpPost]
        public JsonResult SetupAmount()
        {
            try
            {
                /*
                 * 如果是组织考试设置 为model.试卷内容ID
                 * 如果是修改考试设置 为model.ID
                 * 如果是组织练习或修改练习设置 为model.试卷内容ID
                 */
                Guid 试卷设置ID = LKExamURLQueryKey.GetGuid("testSetID");
                int 试卷设置类型 = LKExamURLQueryKey.GetInt32("testSetType");
                int 用户操作类型 = LKExamURLQueryKey.GetInt32("uHandleType");

                /* 修改试卷设置 */
                if (用户操作类型 == 1)
                {
                    /* 修改考试设置 */
                    if (试卷设置类型 == 1)
                    {
                        /*
                         * 如果该考试设置 处于考试开始时间之后 则不能修改设置
                         */
                        int reval = 考试设置.判断是否能修改考试设置(试卷设置ID);
                        if (reval == 1)
                        {
                            return LKPageJsonResult.Failure("该考试现处于考试时间之内，不能修改设置");
                        }
                        else if (reval == 2)
                        {
                            return LKPageJsonResult.Failure("该考试的考试时间已结束，不能修改设置");
                        }
                    }
                }
                else
                {
                    if (试卷设置类型 == 1)
                    {
                        /*  
                         * 如果该试卷存在练习设置 则不能再设置考试
                         */
                        bool reval = 考试设置.判断某试卷是否能设置考试(试卷设置ID);

                        if (!reval)
                        {
                            return LKPageJsonResult.Failure("该试卷已组织过练习，不能组织考试。");
                        }
                    }
                    else
                    {
                        /*
                         * 如果该试卷 处于考试设置的 考试时间之内 则不能组织练习
                         * 如果该试卷 处于考试设置的 考试开始时间 需删除该考试设置
                         */
                        bool reval = 练习设置.判断某试卷是否能设置练习(试卷设置ID);
                        if (!reval)
                        {
                            return LKPageJsonResult.Failure("该试卷组织过的的考试现处于考试时间之内，不能组织练习");
                        }
                    }
                }
                return LKPageJsonResult.Success();

            }
            catch (Exception ex)
            {
                /* 返回异常后Json格式字符串 */
                return LKPageJsonResult.Exception(ex);
            }
        }

        /// <summary>
        /// 删除试卷设置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete()
        {
            #region try/catch(){}
            try
            {
                int 设置类型 = LKExamURLQueryKey.GetInt32("testSetType");
                /*
                 * 如果是删除考试设置 为model.ID
                 * 如果是删除练习设置 为model.试卷内容ID
                 */
                Guid 试卷设置ID = LKExamURLQueryKey.GetGuid("testSetID");
                if (设置类型 == 0)
                {
                    练习设置.删除练习设置(试卷设置ID);
                }
                else
                {
                    考试设置.删除考试设置(试卷设置ID);
                }

                /* 返回执行成功的Json字符串 */
                return LKPageJsonResult.Success();
            }
            catch (Exception ex)
            {
                /* 返回异常后Json格式字符串 */
                return LKPageJsonResult.Exception(ex);
            }
            #endregion
        }
    }
}
