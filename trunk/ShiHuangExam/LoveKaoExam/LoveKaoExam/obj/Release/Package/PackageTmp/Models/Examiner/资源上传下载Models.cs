using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoveKaoExam.Data;

namespace LoveKaoExam.Models.Examiner
{
    public class 资源上传下载Models
    {
        public 资源上传下载Models(Exception ex)
        {
            爱考网服务器连接 = new 爱考网服务器连接(ex);
        }

        public 资源上传下载Models(上传下载信息 c上传下载信息)
        {
            上传下载信息 = c上传下载信息;
            爱考网账号类型 = 0;
            爱考网服务器连接 = new 爱考网服务器连接();
        }

        public 资源上传下载Models(上传下载信息 c上传下载信息, int i爱考网账号类型)
        {
            上传下载信息 = c上传下载信息;
            爱考网账号类型 = i爱考网账号类型;
            爱考网服务器连接 = new 爱考网服务器连接();
        }
        /// <summary>
        /// 上传下载信息
        /// </summary>
        public 上传下载信息 上传下载信息 { get; set; }

        /// <summary>
        /// 爱考网账号类型
        /// <para>(1)0表示该账号正常</para>
        /// <para>(2)1表示该账号已被爱考网禁用</para>
        /// <para>(3)2表示该账号禁止任何分站绑定</para>
        /// </summary>
        public int 爱考网账号类型 { get; set; }

        /// <summary>
        /// 连接爱考网失败消息
        /// </summary>
        public 爱考网服务器连接 爱考网服务器连接 { get; set; }
    }
}