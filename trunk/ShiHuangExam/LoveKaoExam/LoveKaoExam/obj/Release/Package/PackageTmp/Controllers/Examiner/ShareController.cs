using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using LoveKaoExam.Data;
using LoveKaoExam.Library.CSharp;
using LoveKaoExam.Models;
using LoveKaoExam.Models.Examiner;
using LoveKao.Page;


namespace LoveKaoExam.Controllers.Examiner
{
    [Authorize(Roles = "考官")]
    public class ShareController : BaseController
    {
        /// <summary>
        /// 上传试题/试卷页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload()
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            资源上传下载Models c资源上传下载Models = null;
            try
            {
                int i爱考网账号类型 = 0;
                上传下载信息 上传下载信息Model = 上传下载信息.得到上传下载信息(UserInfo.CurrentUser.用户ID);
                c资源上传下载Models = new 资源上传下载Models(上传下载信息Model, i爱考网账号类型);
            }
            catch (Exception ex)
            {
                c资源上传下载Models = new 资源上传下载Models(ex);
            }

            /* Ajax请求 */
            if (Request.IsAjaxRequest())
            {
                return View("~/Views/Examiner/Share/Upload.ascx", c资源上传下载Models);
            }

            /* 返回上传试题试卷视图 */
            return View("~/Views/Examiner/Share/Upload.aspx", c资源上传下载Models);
        }

        /// <summary>
        /// 下载试题/试卷页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Download()
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            资源上传下载Models c资源上传下载Models = null;
            try
            {
                int i爱考网账号类型 = 0;
                上传下载信息 上传下载信息Model = 上传下载信息.得到上传下载信息(UserInfo.CurrentUser.用户ID);
                c资源上传下载Models = new 资源上传下载Models(上传下载信息Model, i爱考网账号类型);
            }
            catch (Exception ex)
            {
                c资源上传下载Models = new 资源上传下载Models(ex);
            }

            /* Ajax请求 */
            if (Request.IsAjaxRequest())
            {
                return View("~/Views/Examiner/Share/Download.ascx", c资源上传下载Models);
            }

            /* 返回下载试题试卷视图 */
            return View("~/Views/Examiner/Share/Download.aspx", c资源上传下载Models);
        }

        /// <summary>
        /// 资源共享
        /// <para>(1)上传试题</para>
        /// <para>(2)上传试卷</para>
        /// <para>(3)下载试题</para>
        /// <para>(4)下载试卷</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Resource(int? id)
        {
            /* 禁止页面被缓存 */
            BasePage.PageNoCache();

            /*
             * 资源方式 0表示上传 1表示下载
             */
            int i资源方式 = LKExamURLQueryKey.GetInt32("resourceMode");

            /*
             * 资源类型 0表示试题 1表示试卷
             */
            int i资源类型 = LKExamURLQueryKey.GetInt32("resourceType");

            爱考网资源方式 资源方式 = i资源方式 == 0 ? 爱考网资源方式.上传 : 爱考网资源方式.下载;
            爱考网资源类型 资源类型 = i资源类型 == 0 ? 爱考网资源类型.试题 : 爱考网资源类型.试卷;

            MvcPager引用目标 分页信息类型;

            #region 分页信息类型
            //上传
            if (i资源方式 == 0)
            {
                //试题
                if (i资源类型 == 0)
                {
                    分页信息类型 = MvcPager引用目标.考官_爱考网资源共享上传试题列表;
                }
                //试卷
                else
                {
                    分页信息类型 = MvcPager引用目标.考官_爱考网资源共享上传试卷列表;
                }
            }
            //下载
            else
            {
                //试题
                if (i资源类型 == 0)
                {
                    分页信息类型 = MvcPager引用目标.考官_爱考网资源共享下载试题列表;
                }
                //试卷
                else
                {
                    分页信息类型 = MvcPager引用目标.考官_爱考网资源共享下载试卷列表;
                }
            }
            #endregion

            /* 爱考网资源共享集合 */
            爱考网资源共享 资源共享集合 = 爱考网资源共享集合(id, 资源方式, 资源类型);

            /* 爱考网资源列表 */
            LKExamMvcPagerData<爱考网资源列表>.数据信息(资源共享集合.爱考网资源列表, 分页信息类型, ViewData);

            /* 如果是Ajax异步请求则返回控件视图 */
            if (Request.IsAjaxRequest())
            {
                return View("~/Views/Examiner/Share/Resource.ascx", 资源共享集合);
            }

            /* 返回爱考网资源共享集合 */
            return View("~/Views/Examiner/Share/Resource.aspx", 资源共享集合);
        }

        /// <summary>
        /// 资源共享
        /// <para>(1)上传试题</para>
        /// <para>(2)上传试卷</para>
        /// <para>(3)下载试题</para>
        /// <para>(4)下载试卷</para>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AjaxResource()
        {
            try
            {
                #region 资源共享集合
                /* 资源方式 */
                string i资源方式 = LKExamURLQueryKey.GetString("resourceMode");

                /* 资源类型 */
                string i资源类型 = LKExamURLQueryKey.GetString("resourceType");
                #endregion

                #region 资源类型 试题
                if (i资源类型 == 爱考网资源类型.试题.ToString())
                {

                    #region 资源ID集合
                    /* 资源ID集合 */
                    string[] 资源ID数组 = LKExamURLQueryKey.GetString("resourceID[]").Split(",".ToArray());
                    List<Guid> 资源ID集合 = new List<Guid>();
                    foreach (string item in 资源ID数组)
                    {
                        Guid 资源ID = LKExamURLQueryKey.SToGuid(item);
                        if (资源ID != Guid.Empty)
                        {
                            资源ID集合.Add(资源ID);
                        }
                    }
                    if (资源ID集合.Count == 0)
                    {
                        return LKPageJsonResult.Failure("请选择您要" + i资源方式 + "的" + i资源类型);
                    }
                    #endregion

                    #region 上传试题
                    if (i资源方式 == 爱考网资源方式.上传.ToString())
                    {
                        List<试题外部信息> 已存在试题集合 = null;

                        int reval = 试题外部信息.上传试题(资源ID集合, out 已存在试题集合);

                        /* 如果成功 并且 已存在试题集合长度不为0 */
                        if (reval == 0 && 已存在试题集合 != null && 已存在试题集合.Count != 0)
                        {
                            return new JsonResult { Data = new { result = true, info = new { 上传试题_相似试题数组 = 已存在试题集合 } } };
                        }
                    }
                    #endregion

                    #region 下载试题
                    else
                    {
                        试题外部信息.下载试题(资源ID集合);
                    }
                    #endregion
                }
                #endregion

                #region 资源类型 试卷
                else
                {
                    #region 资源ID
                    Guid 资源ID = LKExamURLQueryKey.GetGuid("resourceID");
                    if (资源ID == Guid.Empty)
                    {
                        return LKPageJsonResult.Failure("请选择您要" + i资源方式 + "的" + i资源类型);
                    }
                    #endregion

                    #region 上传试卷
                    if (i资源方式 == 爱考网资源方式.上传.ToString())
                    {
                        List<试题外部信息> 已存在试题集合 = null;

                        int reval = 试卷外部信息.上传试卷(资源ID, out  已存在试题集合);

                        /* 如果成功 并且 已存在试题集合长度不为0 */
                        if (reval == 0 && 已存在试题集合 != null && 已存在试题集合.Count != 0)
                        {
                            return new JsonResult { Data = new { result = true, info = new { 上传试题_相似试题数组 = 已存在试题集合 } } };
                        }
                    }
                    #endregion

                    #region 下载试卷
                    else
                    {
                        int i试卷中所有试题总数 = LKExamURLQueryKey.GetInt32("testInAllSubjectNum");
                        int i试卷中已有试题总数 = LKExamURLQueryKey.GetInt32("testInExistSubjectNum");

                        试卷外部信息.下载试卷(资源ID, i试卷中所有试题总数, i试卷中已有试题总数);
                    }
                    #endregion
                }
                #endregion

                return LKPageJsonResult.Success();
            }
            catch (Exception ex)
            {
                return LKPageJsonResult.Exception(ex);
            }
        }
    }
}
