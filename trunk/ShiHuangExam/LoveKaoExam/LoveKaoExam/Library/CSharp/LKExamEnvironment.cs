using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace LoveKaoExam.Library.CSharp
{
    public class LKExamEnvironment
    {

        /// <summary>
        /// 环境变量值
        /// </summary>
        public static string environment;

        /// <summary>
        /// 返回适用于环境值
        /// </summary>
        public static string Environment
        {
            get
            {
                if (environment == null)
                {
                    environment = ConfigurationManager.AppSettings["environment"];
                }
                return environment;
            }
        }

        /// <summary>
        /// 学校返回TRUE 企业返回FALSE
        /// </summary>
        public static bool 是否为学校
        {
            get
            {
                return Environment == "学校";
            }
        }

        /// <summary>
        /// 考生名称
        /// </summary>
        public static string 考生名称
        {
            get
            {
                if (是否为学校)
                {
                    return "学生";
                }
                return "员工";
            }
        }

        /// <summary>
        /// 考生编号名称
        /// </summary>
        public static string 考生编号
        {
            get
            {
                if (是否为学校)
                {
                    return "学号";
                }
                return "工号";
            }
        }

        /// <summary>
        /// 考官名称
        /// </summary>
        public static string 考官名称
        {
            get
            {
                if (是否为学校)
                {
                    return "教师";
                }
                return "主管";
            }
        }

        /// <summary>
        /// 考官编号名称
        /// </summary>
        public static string 考官编号
        {
            get
            {
                if (是否为学校)
                {
                    return "工号";
                }
                return "编号";
            }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public static string 部门名称
        {
            get
            {
                if (是否为学校)
                {
                    return "班级";
                }
                return "部门";
            }
        }

        /// <summary>
        /// 返回角色名称，根据角色值返回相应角色名称
        /// </summary>
        /// <param name="u角色"></param>
        /// <returns></returns>
        public static string 角色名称(int u角色)
        {
            switch (u角色)
            {
                case 0:
                    return 考生名称;
                case 1:
                    return 考官名称;
                default:
                    return "";
            }
        }

        /// <summary>
        /// 返回角色编号，根据角色值返回相应角色编号
        /// </summary>
        /// <param name="u角色"></param>
        /// <returns></returns>
        public static string 角色编号(int u角色)
        {
            switch (u角色)
            {
                case 0:
                    return 考生编号;
                case 1:
                    return 考官编号;
                default:
                    return "";
            }
        }
    }
}