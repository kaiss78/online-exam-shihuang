using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoveKaoExam.Data;
using Webdiyer.WebControls.Mvc;
using LoveKaoExam.Library.CSharp;

namespace LoveKaoExam.Models
{
    /// <summary>
    /// 爱考网资源方式 包含上传, 下载
    /// </summary>
    public enum 爱考网资源方式
    {
        上传, 下载
    }

    /// <summary>
    /// 爱考网资源类型 包含 试题, 试卷
    /// </summary>
    public enum 爱考网资源类型
    {
        试题, 试卷
    }

    /// <summary>
    /// 爱考网资源列表
    /// </summary>
    public class 爱考网资源列表
    {
        /// <summary>
        /// 外部信息ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 试卷内容ID
        /// </summary>
        public Guid ContentID { get; set; }

        /// <summary>
        /// 试题的标题/试卷的名称
        /// </summary>
        public string 标题名称 { get; set; }

        /// <summary>
        /// 试题/试卷的分类
        /// </summary>
        public List<所属分类> 分类列表 { get; set; }

        /// <summary>
        /// 试题/试卷的创建者名称
        /// </summary>
        public string 创建人昵称 { get; set; }

        /// <summary>
        /// 试题/试卷的创建者ID
        /// </summary>
        public Guid 创建人ID { get; set; }

        /// <summary>
        /// 试题/试卷的创建时间
        /// </summary>
        public DateTime 创建时间 { get; set; }

        public int 试卷中所有试题总数 { get; set; }

        public int 试卷中已有试题总数 { get; set; }
    }

    /// <summary>
    /// 爱考网资源共享
    /// </summary>
    public class 爱考网资源共享
    {
        public 爱考网资源共享(){
            爱考网服务器连接 = new 爱考网服务器连接();
        }

        /// <summary>
        /// 爱考网资源列表
        /// </summary>
        public PagedList<爱考网资源列表> 爱考网资源列表 { get; set; }

        /// <summary>
        /// 试题/试卷已选择ID列表
        /// </summary>
        public List<Guid> 爱考网ID列表
        {
            get;
            set;
        }

        /// <summary>
        /// 爱考网资源方式 包含上传, 下载
        /// </summary>
        public 爱考网资源方式 资源方式 { get; set; }

        /// <summary>
        /// 爱考网资源类型 包含 试题, 试卷
        /// </summary>
        public 爱考网资源类型 资源类型 { get; set; }

        public 上传下载信息 资源上传下载信息 { get; set; }

        public 爱考网服务器连接 爱考网服务器连接 { get; set; }

    }
}