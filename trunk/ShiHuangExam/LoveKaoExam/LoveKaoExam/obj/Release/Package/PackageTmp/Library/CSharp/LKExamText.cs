using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoveKaoExam.Models;
using System.Text.RegularExpressions;
using LoveKao.Page;

namespace LoveKaoExam.Library.CSharp
{
    public class LKExamText : LKPageText
    {
        /// <summary>
        /// 试题列表，截取试题题干并且优化字符串
        /// </summary>
        /// <param name="s试题标题">试题标题</param>
        /// <returns></returns>
        public static string 试题列表(string s试题标题)
        {
            return 试题列表(s试题标题, 65);
        }

        /// <summary>
        /// 试题列表，截取试题题干并且优化字符串
        /// </summary>
        /// <param name="s试题标题">试题标题</param>
        /// <param name="i截取长度">截取长度</param>
        /// <returns></returns>
        public static string 试题列表(string s试题标题, int i截取长度)
        {
            return 截取纯文本字符串(s试题标题, i截取长度, true, true, true, true);
        }

        /// <summary>
        /// 试卷列表，截取试卷标题并且优化字符串
        /// </summary>
        /// <param name="s试卷标题">试卷标题</param>
        /// <returns></returns>
        public static string 试卷列表(string s试卷标题)
        {
            return 试卷列表(s试卷标题, 30);
        }

        /// <summary>
        /// 试卷列表，截取试卷标题并且优化字符串
        /// </summary>
        /// <param name="s试卷标题">试卷标题</param>
        /// <param name="i截取长度">截取长度</param>
        /// <returns></returns>
        public static string 试卷列表(string s试卷标题, int i截取长度)
        {
            return 截取纯文本字符串(s试卷标题, i截取长度, true, true, true, true);
        }

        /// <summary>
        /// 替换双引号，替换双引号"为单引号'
        /// </summary>
        /// <param name="s标题"></param>
        /// <returns></returns>
        public static string 替换双引号(string s标题)
        {
            return Regex.Replace(s标题, "\"", "\'", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 删除提示，删除数据时截取默认长度字符串
        /// </summary>
        /// <param name="s标题">标题</param>
        /// <returns></returns>
        public static string 删除提示(string s标题)
        {
            return 删除提示(s标题, 20);
        }

        /// <summary>
        /// 删除提示，删除数据时截取指定长度字符串
        /// </summary>
        /// <param name="s标题">标题</param>
        /// <param name="i截取长度">截取长度</param>
        /// <returns></returns>
        public static string 删除提示(string s标题, int i截取长度)
        {
            s标题 = 替换双引号(s标题);
            s标题 = 截取纯文本字符串(s标题, i截取长度, true, true, true, true);
            return s标题;
        }

        /// <summary>
        /// 网页标签Title，用于网页标签的title属性
        /// </summary>
        /// <param name="s标题">标题</param>
        /// <returns></returns>
        public static string 网页标签Title(string s标题)
        {
            s标题 = 替换双引号(s标题);
            return s标题;
        }

        /// <summary>
        /// 爱考网资源共享列表
        /// </summary>
        /// <param name="s标题名称">标题名称</param>
        /// <param name="e爱考网资源方式">爱考网资源方式(1)上传(2)下载</param>
        /// <param name="e爱考网资源类型">爱考网资源类型(1)试题(2)试卷</param>
        /// <returns></returns>
        public static string 爱考网资源共享列表(string s标题名称, 爱考网资源方式 e爱考网资源方式, 爱考网资源类型 e爱考网资源类型)
        {
            int 截取长度;
            if (e爱考网资源方式 == 爱考网资源方式.下载)
            {
                截取长度 = 48;
            }
            else
            {
                截取长度 = 66;
            }
            return 截取纯文本字符串(s标题名称, 截取长度, true, true, true, true);
        }
    
    }
}