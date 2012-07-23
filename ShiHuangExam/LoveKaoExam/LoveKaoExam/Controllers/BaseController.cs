using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKaoExam.Library.CSharp;
using LoveKaoExam.Data;
using Webdiyer.WebControls.Mvc;
using LoveKaoExam.Models;
using LoveKaoExam.Models.Examiner;
using LoveKao.Page;
using LoveKaoExam.Models.Examinee;

namespace LoveKaoExam.Controllers
{
    public class BaseController : Controller
    {

        #region 管理员
        /// <summary>
        /// 考生信息列表
        /// </summary>
        /// <param name="id">当前分页页数</param>
        /// <returns></returns>
        public static PagedList<考生> 考生信息列表(int? id)
        {
            /* 关键字 */
            string 关键词 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            /* 部门ID */
            Guid 部门ID = LKExamURLQueryKey.部门ID();

            /* 考生信息List */
            List<考生> 考生信息List = 考生.查询考生(部门ID, 关键词, pageIndex - 1, pageSize, out totalItemCount);

            /* 返回考生信息PagedList */
            return new PagedList<考生>(考生信息List, pageIndex, pageSize, totalItemCount);
        }
        /// <summary>
        /// 考生Excel列表
        /// </summary>
        /// <param name="id">当前分页页数</param>
        /// <param name="IsExport">是否为导出Excel</param>
        /// <returns></returns>
        public static PagedList<考生> 考生Excel列表()
        {
            /* 部门ID */
            Guid 部门ID = LKExamURLQueryKey.部门ID();
            string 部门名称 = null;
            /* 考生ExcelList */
            List<考生> 考生Excel = 考生.查询考生(部门ID, out 部门名称);

            /* 返回考生ExcelPagedList */
            return new PagedList<考生>(考生Excel, 1, 考生Excel.Count, 考生Excel.Count);

        }
        /// <summary>
        /// 考官信息列表
        /// </summary>
        /// <param name="id">当前分页页数</param>
        /// <returns></returns>
        public static PagedList<考官> 考官信息列表(int? id)
        {
            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            /* 考官信息List */
            List<考官> 考官信息List = 考官.查询考官(关键字, pageIndex - 1, pageSize, out totalItemCount);

            /* 返回考官信息PagedList */
            return new PagedList<考官>(考官信息List, pageIndex, pageSize, totalItemCount);


        }
        /// <summary>
        /// 部门信息列表
        /// </summary>
        /// <param name="id">当前分页页数</param>
        /// <returns></returns>
        public static PagedList<部门> 部门信息列表(int? id)
        {
            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            /* 部门信息List */
            List<部门> 部门信息List = 部门.查询部门(关键字, pageIndex - 1, pageSize, out totalItemCount);

            /* 返回部门信息PagedList */
            return new PagedList<部门>(部门信息List, pageIndex, pageSize, totalItemCount);
        }
        #endregion

        #region 考官
        public static PagedList<试卷外部信息> 考官试卷列表(int? id)
        {
            /* 下拉静态值项 */
            int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项();

            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            /* 用户ID */
            Guid 用户ID = UserInfo.CurrentUser.用户ID;

            /* 考官试卷List */
            List<试卷外部信息> 考官试卷List = 试卷外部信息.得到某考官试卷(关键字, 用户ID, i下拉静态值项, pageIndex - 1, pageSize, out totalItemCount);

            /* 返回考官试卷PagedList */
            return new PagedList<试卷外部信息>(考官试卷List, pageIndex, pageSize, totalItemCount);
        }
        public static PagedList<试题外部信息> 考官试题列表(int? id)
        {
            /* 下拉静态值项 */
            int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项();

            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            /* 用户ID */
            Guid 用户ID = UserInfo.CurrentUser.用户ID;

            /* 考官试题List */
            List<试题外部信息> 考官试题List = 试题外部信息.得到某考官试题(关键字, 用户ID, i下拉静态值项, pageIndex - 1, pageSize, out totalItemCount);

            /* 返回考官试题PagedList */
            return new PagedList<试题外部信息>(考官试题List, pageIndex, pageSize, totalItemCount);
        }
        public static PagedList<考试设置> 考官已组织考试列表(int? id)
        {
            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            /* 用户ID */
            Guid 用户ID = UserInfo.CurrentUser.用户ID;

            /* 考官试卷List */
            List<考试设置> 考官试卷List = 考试设置.查询某考官组织的考试(关键字, 用户ID, pageIndex - 1, pageSize, out totalItemCount);

            /* 返回考官试卷PagedList */
            return new PagedList<考试设置>(考官试卷List, pageIndex, pageSize, totalItemCount);
        }
        public static PagedList<练习设置> 考官已组织练习列表(int? id)
        {
            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            /* 用户ID */
            Guid 用户ID = UserInfo.CurrentUser.用户ID;

            /* 考官试卷List */
            List<练习设置> 考官试卷List = 练习设置.查询某考官组织的练习(关键字, 用户ID, pageIndex - 1, pageSize, out totalItemCount);

            /* 返回考官试卷PagedList */
            return new PagedList<练习设置>(考官试卷List, pageIndex, pageSize, totalItemCount);
        }
        public static ExamResultModels 考官考试结果列表(int? id)
        {
            /* 考试设置ID */
            Guid g考试设置ID = LKExamURLQueryKey.试卷ID();

            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 部门ID */
            Guid g部门ID = LKExamURLQueryKey.部门ID();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            考试设置 c考试设置 = null;

            /* 考生做过的试卷List */
            List<考生做过的试卷> 考生做过的试卷List = 考生做过的试卷.得到某场考试参考学生考试结果(关键字, g考试设置ID, g部门ID, pageIndex - 1, pageSize, out totalItemCount, out  c考试设置);

            /* 考官考试结果 */
            ExamResultModels examResultModels = new ExamResultModels();

            /* 返回考生做过的试卷PagedList */
            examResultModels.考生做过的试卷 = new PagedList<考生做过的试卷>(考生做过的试卷List, pageIndex, pageSize, totalItemCount);
            examResultModels.考试设置 = c考试设置;
            return examResultModels;
        }
        #endregion

        #region 爱考网资源共享
        public static List<所属分类> 本地系统所属分类(List<Data.LoveKaoServiceReference.所属分类> 爱考网所属分类)
        {
            List<所属分类> 本地系统所属分类 = new List<所属分类>();
            if (爱考网所属分类 != null && 爱考网所属分类.Count != 0)
            {
                foreach (Data.LoveKaoServiceReference.所属分类 item in 爱考网所属分类)
                {
                    本地系统所属分类.Add(new 所属分类
                    {
                        分类名 = item.分类名,
                        ID = item.ID
                    });
                }
            }
            return 本地系统所属分类;
        }

        public static List<所属分类> 本地系统所属分类(List<string> list分类列表)
        {
            List<所属分类> _List所属分类 = new List<所属分类>();
            foreach (string item in list分类列表)
            {
                _List所属分类.Add(new 所属分类() { 分类名 = item });
            }
            return _List所属分类;
        }

        public static PagedList<爱考网资源列表> 爱考网资源列表下载试题列表(int? id)
        {
            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            List<Data.LoveKaoServiceReference.试题外部信息WCF> 下载试题列表List = 试题外部信息.得到下载试题列表(关键字, pageIndex - 1, pageSize, out totalItemCount);

            List<爱考网资源列表> 资源列表 = new List<爱考网资源列表>();

            foreach (Data.LoveKaoServiceReference.试题外部信息WCF item in 下载试题列表List)
            {
                资源列表.Add(new 爱考网资源列表
                {
                    ID = item.ID,
                    ContentID = item.试题内容ID,
                    标题名称 = item.试题显示内容,
                    创建时间 = item.公开时间,
                    创建人昵称 = item.创建人.昵称,
                    创建人ID = item.创建人ID,
                    分类列表 = 本地系统所属分类(item.分类列表)
                });
            }
            return new PagedList<爱考网资源列表>(资源列表, pageIndex, pageSize, totalItemCount);
        }
        public static PagedList<爱考网资源列表> 爱考网资源列表下载试卷列表(int? id)
        {
            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            List<Data.LoveKaoServiceReference.试卷外部信息WCF> 下载试卷列表List = 试卷外部信息.得到下载试卷列表(关键字, pageIndex - 1, pageSize, out totalItemCount);

            List<爱考网资源列表> 资源列表 = new List<爱考网资源列表>();

            foreach (Data.LoveKaoServiceReference.试卷外部信息WCF item in 下载试卷列表List)
            {
                资源列表.Add(new 爱考网资源列表
                {
                    ID = item.ID,
                    ContentID = item.试卷内容ID,
                    标题名称 = item.名称,
                    创建时间 = item.公开时间,
                    创建人昵称 = item.创建人.昵称,
                    创建人ID = item.创建人ID,
                    分类列表 = 本地系统所属分类(item.分类列表),
                    试卷中所有试题总数 = item.试题总数,
                    试卷中已有试题总数 = item.已下载试题个数
                });
            }
            return new PagedList<爱考网资源列表>(资源列表, pageIndex, pageSize, totalItemCount);
        }
        public static PagedList<爱考网资源列表> 爱考网资源列表上传试题列表(int? id)
        {

            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            List<试题外部信息> 上传试题列表List = 试题外部信息.得到上传试题列表(关键字, pageIndex - 1, pageSize, out totalItemCount);

            List<爱考网资源列表> 资源列表 = new List<爱考网资源列表>();

            foreach (试题外部信息 item in 上传试题列表List)
            {
                资源列表.Add(new 爱考网资源列表
                {
                    ID = item.ID,
                    ContentID = item.试题内容ID,
                    标题名称 = item.试题显示内容,
                    创建时间 = item.创建时间,
                    创建人昵称 = item.创建人.姓名,
                    创建人ID = item.创建人ID,
                    分类列表 = 本地系统所属分类(item.分类列表)
                });
            }
            return new PagedList<爱考网资源列表>(资源列表, pageIndex, pageSize, totalItemCount);
        }
        public static PagedList<爱考网资源列表> 爱考网资源列表上传试卷列表(int? id)
        {

            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            List<试卷外部信息> 上传试卷列表List = 试卷外部信息.得到上传试卷列表(关键字, pageIndex - 1, pageSize, out totalItemCount);

            List<爱考网资源列表> 资源列表 = new List<爱考网资源列表>();

            foreach (试卷外部信息 item in 上传试卷列表List)
            {
                资源列表.Add(new 爱考网资源列表
                {
                    ID = item.ID,
                    ContentID = item.试卷内容ID,
                    标题名称 = item.名称,
                    创建时间 = item.创建时间,
                    创建人昵称 = item.创建人.姓名,
                    创建人ID = item.创建人ID,
                    分类列表 = 本地系统所属分类(item.分类列表),
                    试卷中所有试题总数 = item.试题总数,
                    试卷中已有试题总数 = item.已上传试题个数
                });
            }
            return new PagedList<爱考网资源列表>(资源列表, pageIndex, pageSize, totalItemCount);
        }
        public static 爱考网资源共享 爱考网资源共享集合(int? id, 爱考网资源方式 资源方式, 爱考网资源类型 资源类型)
        {
            爱考网资源共享 资源共享 = new 爱考网资源共享();
            try
            {
                资源共享.资源类型 = 资源类型;
                资源共享.资源方式 = 资源方式;
                资源共享.资源上传下载信息 = 上传下载信息.得到上传下载信息(UserInfo.CurrentUser.用户ID);

                #region 资源方式
                switch (资源方式)
                {
                    //上传
                    case 爱考网资源方式.上传:
                        #region 资源类型
                        switch (资源类型)
                        {
                            /* 试题 */
                            case 爱考网资源类型.试题:
                                资源共享.爱考网资源列表 = 爱考网资源列表上传试题列表(id);
                                资源共享.爱考网ID列表 = 试题外部信息.得到已上传试题爱考网ID集合();
                                break;
                            /* 试卷 */
                            case 爱考网资源类型.试卷:
                                资源共享.爱考网资源列表 = 爱考网资源列表上传试卷列表(id);
                                资源共享.爱考网ID列表 = 试卷外部信息.得到已上传试卷爱考网ID集合();
                                break;

                            /* 其他 */
                            default:
                                break;
                        }
                        #endregion
                        break;

                    //下载
                    case 爱考网资源方式.下载:
                        #region 资源类型
                        switch (资源类型)
                        {
                            //试题
                            case 爱考网资源类型.试题:
                                资源共享.爱考网资源列表 = 爱考网资源列表下载试题列表(id);
                                资源共享.爱考网ID列表 = 试题外部信息.得到已下载试题爱考网ID集合();
                                break;
                            //试卷
                            case 爱考网资源类型.试卷:
                                资源共享.爱考网资源列表 = 爱考网资源列表下载试卷列表(id);
                                资源共享.爱考网ID列表 = 试卷外部信息.得到已下载试卷爱考网ID集合();
                                break;
                            default:
                                break;
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
                #endregion
            }
            catch (Exception ex)
            {
                资源共享.爱考网服务器连接 = new 爱考网服务器连接(ex);
            }
            return 资源共享;
        }
        #endregion

        #region 考生
        public static PagedList<考试设置> 考生选择考试列表(int? id)
        {
            /* 下拉静态值项 */
            int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项();

            /* 关键字 */
            string 关键词 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            /* 考生信息List */
            List<考试设置> 考生信息List = 考试设置.查询考试(关键词, UserInfo.CurrentUser.用户ID, i下拉静态值项, pageIndex - 1, pageSize, out totalItemCount);

            /* 返回考生信息PagedList */
            return new PagedList<考试设置>(考生信息List, pageIndex, pageSize, totalItemCount);
        }
        public static PagedList<练习设置> 考生选择练习列表(int? id)
        {
            /* 关键字 */
            string 关键词 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            /* 考生信息List */
            List<练习设置> 考生信息List = 练习设置.查询练习(关键词, UserInfo.CurrentUser.用户ID, pageIndex - 1, pageSize, out totalItemCount);

            /* 返回考生信息PagedList */
            return new PagedList<练习设置>(考生信息List, pageIndex, pageSize, totalItemCount);
        }
        public static PagedList<考生做过的试卷> 考生考试记录列表(int? id)
        {
            /* 下拉静态值项 */
            int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项();

            /* 关键字 */
            string 关键词 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            /* 考生信息List */
            List<考生做过的试卷> 考生信息List = 考生做过的试卷.得到考生做过的所有考试(关键词, i下拉静态值项, UserInfo.CurrentUser.用户ID, null, null, pageIndex - 1, pageSize, out totalItemCount);

            /* 返回考生信息PagedList */
            return new PagedList<考生做过的试卷>(考生信息List, pageIndex, pageSize, totalItemCount);
        }
        public static PagedList<考生做过的试卷> 考生练习记录列表(int? id)
        {
            /* 下拉静态值项 */
            int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项();

            /* 关键字 */
            string 关键词 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            /* 考生信息List */
            List<考生做过的试卷> 考生信息List = 考生做过的试卷.得到考生做过的所有练习(关键词, i下拉静态值项, UserInfo.CurrentUser.用户ID, pageIndex - 1, pageSize, out totalItemCount);

            /* 返回考生信息PagedList */
            return new PagedList<考生做过的试卷>(考生信息List, pageIndex, pageSize, totalItemCount);
        }
        public static 考试排行Models 考生考试排名(int? id, Guid 考试设置ID)
        {
            /* 关键字 */
            string 关键字 = LKExamURLQueryKey.关键字();

            /* 分页页数选项 */
            int pageIndex = LKPageMvcPager分页配置.Get当前页(id),
                pageSize = 10,
                totalItemCount;

            LKPageException lkPageException = null;

            /* 考生信息List */
            List<考试排名> 考生信息List = 考试排名.得到考试排名(关键字, 考试设置ID, UserInfo.CurrentUser.用户ID, pageIndex - 1, pageSize, out totalItemCount, out lkPageException);

            PagedList<考试排名> pagedList考试排名 = new PagedList<考试排名>(考生信息List, pageIndex, pageSize, totalItemCount);

            return new 考试排行Models(pagedList考试排名, lkPageException);
        }
        #endregion

        /// <summary>
        /// 得到考试分析
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMadeExam()
        {
            try
            {
                string strGuid = LKExamURLQueryKey.GetString("guid");// Request["guid"];
                if (!string.IsNullOrEmpty(strGuid))
                {

                    考生做过的试卷 testText = 考生做过的试卷.得到一份答卷(new Guid(strGuid));
                    string JsonData = testText.转化成Json带试卷内容();
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
    }
}
