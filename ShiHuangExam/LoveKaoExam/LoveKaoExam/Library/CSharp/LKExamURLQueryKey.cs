using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoveKaoExam.Library.CSharp
{
    /// <summary>
    /// URLQueryKey键
    /// </summary>
    public class LKExamURLQueryKey
    {
        /// <summary>
        /// 返回URL上默认或指定的参数名对应值
        /// </summary>
        /// <param name="name">参数名称，为空/null时取默认值</param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            if (string.IsNullOrEmpty(key)) return "";

            string queryString = HttpContext.Current.Request[key];
            return string.IsNullOrEmpty(queryString) ? "" : queryString.Trim();
        }

        /// <summary>
        /// 将字符串ID转为等同的Guid
        /// </summary>
        /// <param name="id">字符串ID</param>
        /// <returns></returns>
        public static Guid SToGuid(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || (id.Length != 36))
                {
                    return Guid.Empty;
                }
                return Guid.Parse(id);
            }
            catch (Exception ex)
            {
                return Guid.Empty;

            }

        }

        /// <summary>
        /// 返回URL上默认或指定的参数名对应值 类型自动转为Guid
        /// </summary>
        /// <param name="key">参数名称，为空/null时取默认值</param>
        /// <returns></returns>
        public static Guid GetGuid(string key)
        {
            return SToGuid(GetString(key));
        }

        /// <summary>
        /// 返回URL上默认或指定的参数名对应值 类型自动转为Int32
        /// </summary>
        /// <param name="key">参数名称，为空/null时取默认值</param>
        /// <returns></returns>
        public static int GetInt32(string key)
        {
            try
            {
                return Int32.Parse(GetString(key));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 下拉框 - 部门ID 参数值
        /// </summary>
        /// <returns></returns>
        public static Guid 部门ID()
        {
            return 部门ID((string)null);
        }
        /// <summary>
        /// 下拉框 - 部门ID 参数值
        /// </summary>
        /// <param name="name">参数名称，为空/null时取默认值</param>
        /// <returns></returns>
        public static Guid 部门ID(string key)
        {
            //部门控件name
            key = LKExamURLDefaultName.部门ID(key);
            key = GetString(key);
            return SToGuid(key);
        }

        /// <summary>
        /// 获取url上默认的试卷参数值
        /// </summary>
        /// <returns></returns>
        public static Guid 试卷ID()
        {
            return 试卷ID((string)null);
        }
        /// <summary>
        /// 获取url上自定义/默认的试卷参数值
        /// </summary>
        /// <param name="name">参数名称，为空/null时取默认值</param>
        /// <returns></returns>
        public static Guid 试卷ID(string key)
        {
            //试卷控件name
            key = LKExamURLDefaultName.试卷ID(key);
            key = GetString(key);
            return SToGuid(key);
        }

        /// <summary>
        /// 获取url上默认的文本框参数值
        /// </summary>
        /// <returns></returns>
        public static string 关键字()
        {
            return 关键字((string)null);
        }
        /// <summary>
        /// 获取url上自定义/默认的文本框参数值
        /// </summary>
        /// <param name="name">参数名称，为空/null时取默认值</param>
        /// <returns></returns>
        public static string 关键字(string key)
        {
            //文本框控件默认name
            key = LKExamURLDefaultName.关键字(key);
            return GetString(key);
        }

        /// <summary>
        /// 获取url上自定义/默认的下拉框类型
        /// </summary>
        /// <returns></returns>
        public static int 下拉静态值项()
        {
            return 下拉静态值项((string)null);
        }
        /// <summary>
        /// 获取url上自定义/默认的下拉框类型
        /// </summary>
        /// <param name="key">参数名称，为空/null时取默认值</param>
        /// <returns></returns>
        public static int 下拉静态值项(string key)
        {
            //文本框控件默认name
            key = LKExamURLDefaultName.下拉静态值项(key);
            return GetInt32(key);
        }

        /// <summary>
        /// 判断URL上是否存在部门ID
        /// </summary>
        /// <returns></returns>
        public static bool 包含部门ID()
        {
            return 包含部门ID((string)null);
        }
        /// <summary>
        /// 判断URL上是否存在部门ID
        /// </summary>
        /// <param name="key">参数名称，为空/null时取默认值</param>
        /// <returns></returns>
        public static bool 包含部门ID(string key)
        {
            return 部门ID(key) != Guid.Empty;
        }

        /// <summary>
        /// 判断URL上是否存在试卷ID
        /// </summary>
        /// <returns></returns>
        public static bool 包含试卷ID()
        {
            return 包含试卷ID((string)null);
        }
        /// <summary>
        /// 判断URL上是否存在试卷ID
        /// </summary>
        /// <param name="key">参数名称，为空/null时取默认值</param>
        /// <returns></returns>
        public static bool 包含试卷ID(string key)
        {
            return 试卷ID(key) != Guid.Empty;
        }

        /// <summary>
        /// 判断URL上是否存在关键字
        /// </summary>
        /// <returns></returns>
        public static bool 包含关键字()
        {
            return 包含关键字((string)null);
        }
        /// <summary>
        /// 判断URL上是否存在关键字
        /// </summary>
        /// <param name="key">参数名称，为空/null时取默认值</param>
        /// <returns></returns>
        public static bool 包含关键字(string key)
        {
            return !string.IsNullOrEmpty(关键字(key));
        }

        /// <summary>
        /// 判断URL上是否存在下拉静态值项
        /// </summary>
        /// <returns></returns>
        public static bool 包含下拉静态值项()
        {
            return 包含下拉静态值项((string)null);
        }
        /// <summary>
        /// 判断URL上是否存在下拉静态值项
        /// </summary>
        /// <param name="key">参数名称，为空/null时取默认值</param>
        /// <returns></returns>
        public static bool 包含下拉静态值项(string key)
        {
            return 下拉静态值项(key) != 0;
        }

    }
}