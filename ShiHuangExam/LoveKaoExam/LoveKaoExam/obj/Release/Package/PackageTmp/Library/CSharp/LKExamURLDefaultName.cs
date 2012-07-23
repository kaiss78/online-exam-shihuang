using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoveKaoExam.Library.CSharp
{

    /// <summary>
    /// URLDefaultName默认name
    /// </summary>
    public class LKExamURLDefaultName
    {
        /// <summary>
        /// 下拉框 - 部门
        /// </summary>
        /// <returns></returns>
        public static string 部门ID()
        {
            return 部门ID((string)null);
        }
        /// <summary>
        /// 下拉框 - 部门
        /// </summary>
        /// <param name="name">控件name，为空/null时取默认值</param>
        public static string 部门ID(string name)
        {
            return string.IsNullOrEmpty(name) ? "DepartmentID" : name;
        }

        /// <summary>
        /// 试卷
        /// </summary>
        /// <returns></returns>
        public static string 试卷ID()
        {
            return 试卷ID((string)null);
        }
        /// <summary>
        /// 试卷
        /// </summary>
        /// <param name="name">控件name，为空/null时取默认值</param>
        public static string 试卷ID(string name)
        {
            return string.IsNullOrEmpty(name) ? "TestID" : name;
        }

        /// <summary>
        /// 文本框的关键字
        /// </summary>
        /// <returns></returns>
        public static string 关键字()
        {
            return 关键字((string)null);
        }
        /// <summary>
        /// 文本框的关键字
        /// </summary>
        /// <param name="name">控件name，为空/null时取默认值</param>
        /// <returns></returns>
        public static string 关键字(string name)
        {
            return string.IsNullOrEmpty(name) ? "KeyWords" : name;
        }

        /// <summary>
        /// 下拉框的类型
        /// </summary>
        /// <returns></returns>
        public static string 下拉静态值项()
        {
            return 下拉静态值项((string)null);
        }

        /// <summary>
        /// 下拉框的类型
        /// </summary>
        /// <param name="name">控件name，为空/null时取默认值</param>
        /// <returns></returns>
        public static string 下拉静态值项(string name)
        {
            return string.IsNullOrEmpty(name) ? "DDStaticValItem" : name;
        }
    }
}