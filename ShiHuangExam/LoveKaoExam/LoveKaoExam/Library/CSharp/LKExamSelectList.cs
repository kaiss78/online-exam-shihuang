using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveKao.Page;
using LoveKaoExam.Data;

namespace LoveKaoExam.Library.CSharp
{

    /// <summary>
    /// LKExamSelectList静态值项
    /// </summary>
    public class LKExamSelectList静态值项
    {

        private static Dictionary<string, string> _考官试题;

        /// <summary>
        /// 考官试题的静态值项
        /// </summary>
        public static Dictionary<string, string> 考官试题
        {
            get
            {
                if (_考官试题 == null)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("0", "全部试题");
                    dictionary.Add("1", "我的新增试题");
                    dictionary.Add("2", "我的草稿试题");
                    //dictionary.Add("3", "已上传的试题");
                    //dictionary.Add("4", "已下载的试题");

                    _考官试题 = dictionary;
                }
                return _考官试题;
            }
        }

        private static Dictionary<string, string> _考官试卷;

        /// <summary>
        /// 考官试卷的静态值项
        /// </summary>
        public static Dictionary<string, string> 考官试卷
        {
            get
            {
                if (_考官试卷 == null)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("0", "全部试卷");
                    dictionary.Add("1", "我的正常试卷");
                    dictionary.Add("2", "我的草稿试卷");
                    //dictionary.Add("3", "已上传的试卷");
                    //dictionary.Add("4", "已下载的试卷");

                    _考官试卷 = dictionary;
                }
                return _考官试卷;
            }
        }

        private static Dictionary<string, string> _考生我要考试;

        /// <summary>
        /// 考生我要考试的静态值项
        /// </summary>
        public static Dictionary<string, string> 考生我要考试
        {
            get
            {
                if (_考生我要考试 == null)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("0", "全部试卷");
                    dictionary.Add("1", "未做过试卷");
                    dictionary.Add("2", "已做过试卷");

                    _考生我要考试 = dictionary;
                }
                return _考生我要考试;
            }
        }

        private static Dictionary<string, string> _考生考试记录;

        /// <summary>
        /// 考生考试记录的静态值项
        /// </summary>
        public static Dictionary<string, string> 考生考试记录
        {
            get
            {
                if (_考生考试记录 == null)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("0", "全部考试");
                    dictionary.Add("1", "已完成考试");
                    dictionary.Add("2", "未完成考试");

                    _考生考试记录 = dictionary;
                }
                return _考生考试记录;
            }
        }

        private static Dictionary<string, string> _考生练习记录;

        /// <summary>
        /// 考生练习记录的静态值项
        /// </summary>
        public static Dictionary<string, string> 考生练习记录
        {
            get
            {
                if (_考生练习记录 == null)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("0", "全部练习");
                    dictionary.Add("1", "已完成练习");
                    dictionary.Add("2", "未完成练习");

                    _考生练习记录 = dictionary;
                }
                return _考生练习记录;
            }
        }
    }

    /// <summary>
    /// LKExamSelectList动态值项
    /// </summary>
    public class LKExamSelectList动态值项
    {
        /// <summary>
        /// 系统所有部门的动态值项
        /// </summary>
        /// <returns></returns>
        public static List<部门> 系统所有部门()
        {
            return 部门.查询部门();
        }

        /// <summary>
        /// 考试有关部门的动态值项
        /// </summary>
        /// <param name="考试设置ID"></param>
        /// <returns></returns>
        public static List<部门> 考试有关部门(Guid 考试设置ID)
        {
            return 导出考试分析.得到某场考试部门(考试设置ID);
        }
    }

    public class LKExamSelectList
    {
        #region 部门SelectList
        public static SelectList 系统所有部门SelectList()
        {
            return 系统所有部门SelectList((string)null);
        }
        public static SelectList 系统所有部门SelectList(string 部门控件Name)
        {
            /* 部门ID */
            Guid g部门ID = LKExamURLQueryKey.部门ID(部门控件Name);

            /*  */
            List<部门> list部门 = LKExamSelectList动态值项.系统所有部门();

            return new SelectList(list部门, "ID", "名称", g部门ID);
        }
        public static SelectList 考试有关部门SelectList(string 部门控件Name, Guid 考试设置ID)
        {
            /* 部门ID */
            Guid g部门ID = LKExamURLQueryKey.部门ID(部门控件Name);

            List<部门> list部门 = LKExamSelectList动态值项.考试有关部门(考试设置ID);

            return new SelectList(list部门, "ID", "名称", g部门ID);
        }
        #endregion

        #region 下拉列表SelectList
        public static SelectList 下拉列表SelectList(MvcPager下拉列表静态值项 eMvcPager下拉列表静态值项)
        {
            return 下拉列表SelectList(eMvcPager下拉列表静态值项, (string)null);
        }
        public static SelectList 下拉列表SelectList(MvcPager下拉列表静态值项 eMvcPager下拉列表静态值项, string 下拉静态值项控件Name)
        {

            Dictionary<string, string> dictionary = null;

            /* MvcPager下拉列表静态值项 */
            switch (eMvcPager下拉列表静态值项)
            {
                /* 考官试题 */
                case MvcPager下拉列表静态值项.考官试题:
                    dictionary = LKExamSelectList静态值项.考官试题;
                    break;

                /* 考官试卷 */
                case MvcPager下拉列表静态值项.考官试卷:
                    dictionary = LKExamSelectList静态值项.考官试卷;
                    break;

                /* 考生我要考试 */
                case MvcPager下拉列表静态值项.考生我要考试:
                    dictionary = LKExamSelectList静态值项.考生我要考试;
                    break;

                /* 考生考试记录 */
                case MvcPager下拉列表静态值项.考生考试记录:
                    dictionary = LKExamSelectList静态值项.考生考试记录;
                    break;

                /* 考生练习记录 */
                case MvcPager下拉列表静态值项.考生练习记录:
                    dictionary = LKExamSelectList静态值项.考生练习记录;
                    break;

                /* 其他 */
                default:
                    dictionary = new Dictionary<string, string>();
                    break;
            }
            List<SelectListItem> lSelectListItem = new List<SelectListItem>();
            foreach (var item in dictionary)
            {
                lSelectListItem.Add(new SelectListItem() { Value = item.Key, Text = item.Value });
            }

            int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项(下拉静态值项控件Name);
            return new SelectList(lSelectListItem, "Value", "Text", i下拉静态值项);
        }
        #endregion

        #region 考官试题SelectList
        /// <summary>
        /// 考官试题
        /// </summary>
        /// <returns></returns>
        public static SelectList 考官试题SelectList()
        {
            return 考官试题SelectList((string)null);
        }

        /// <summary>
        /// 考官试题
        /// </summary>
        /// <param name="下拉静态值项控件Name">下拉静态值项控件Name</param>
        /// <returns></returns>
        public static SelectList 考官试题SelectList(string 下拉静态值项控件Name)
        {
            return 下拉列表SelectList(MvcPager下拉列表静态值项.考官试题, 下拉静态值项控件Name);
        }
        #endregion

        #region 考官试卷SelectList
        /// <summary>
        /// 考官试卷
        /// </summary>
        /// <returns></returns>
        public static SelectList 考官试卷SelectList()
        {
            return 考官试卷SelectList((string)null);
        }

        /// <summary>
        /// 考官试卷
        /// </summary>
        /// <param name="下拉静态值项控件Name">下拉静态值项控件Name</param>
        /// <returns></returns>
        public static SelectList 考官试卷SelectList(string 下拉静态值项控件Name)
        {
            return 下拉列表SelectList(MvcPager下拉列表静态值项.考官试卷, 下拉静态值项控件Name);
        }
        #endregion

        #region 考生我要考试SelectList
        /// <summary>
        /// 考生我要考试
        /// </summary>
        /// <returns></returns>
        public static SelectList 考生我要考试SelectList()
        {
            return 考生我要考试SelectList((string)null);
        }

        /// <summary>
        /// 考生我要考试
        /// </summary>
        /// <param name="下拉静态值项控件Name">下拉静态值项控件Name</param>
        /// <returns></returns>
        public static SelectList 考生我要考试SelectList(string 下拉静态值项控件Name)
        {
            return 下拉列表SelectList(MvcPager下拉列表静态值项.考生我要考试, 下拉静态值项控件Name);
        }
        #endregion

        #region 考生考试记录SelectList
        /// <summary>
        /// 考生考试记录
        /// </summary>
        /// <returns></returns>
        public static SelectList 考生考试记录SelectList()
        {
            return 考生考试记录SelectList((string)null);
        }

        /// <summary>
        /// 考生考试记录
        /// </summary>
        /// <param name="下拉静态值项控件Name">下拉静态值项控件Name</param>
        /// <returns></returns>
        public static SelectList 考生考试记录SelectList(string 下拉静态值项控件Name)
        {
            return 下拉列表SelectList(MvcPager下拉列表静态值项.考生考试记录, 下拉静态值项控件Name);
        }
        #endregion

        #region 考生练习记录SelectList
        /// <summary>
        /// 考生练习记录
        /// </summary>
        /// <returns></returns>
        public static SelectList 考生练习记录SelectList()
        {
            return 考生练习记录SelectList((string)null);
        }

        /// <summary>
        /// 考生练习记录
        /// </summary>
        /// <param name="下拉静态值项控件Name">下拉静态值项控件Name</param>
        /// <returns></returns>
        public static SelectList 考生练习记录SelectList(string 下拉静态值项控件Name)
        {
            return 下拉列表SelectList(MvcPager下拉列表静态值项.考生练习记录, 下拉静态值项控件Name);
        }
        #endregion
    }
}