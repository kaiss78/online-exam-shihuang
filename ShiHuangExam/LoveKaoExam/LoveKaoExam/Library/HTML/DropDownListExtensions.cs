using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using LoveKao.Page;
using LoveKaoExam.Library.CSharp;
using LoveKaoExam.Controllers;
using LoveKao.Page.HTML;
using System.Web.Mvc.Html;

namespace LoveKaoExam.Library.HTML
{
    public static class DropDownListExtensions
    {
        public static RouteValueDictionary DropDownListDictionary(string name, object htmlattributes)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("不能为空或Null", "控件Name");

            RouteValueDictionary routeValueDictionary = new RouteValueDictionary(htmlattributes);
            object id = routeValueDictionary["id"];
            routeValueDictionary["id"] = id == null ? name : id;
            return routeValueDictionary;
        }
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, LKPageMvcPager下拉列表框 c下拉列表框)
        {
            string s控件Name = c下拉列表框.控件Name;
            string s标识Label = c下拉列表框.标识Label;
            string s默认Option = null;
            SelectList sL下拉列表 = null;

            #region 适用集合
            switch (c下拉列表框.下拉列表适用环境)
            {
                /* 系统所有部门 */
                case MvcPager下拉列表适用环境.系统所有部门:
                    s控件Name = LKExamURLDefaultName.部门ID(s控件Name);
                    s标识Label = LKPageRetainOrReplace.GetString(s标识Label, LKExamEnvironment.部门名称);
                    s默认Option = LKPageRetainOrReplace.GetString(c下拉列表框.是否默认Option, "--请选择" + LKExamEnvironment.部门名称 + "--");
                    sL下拉列表 = LKExamSelectList.系统所有部门SelectList(s控件Name);
                    break;

                case MvcPager下拉列表适用环境.考试有关部门:
                    s控件Name = LKExamURLDefaultName.部门ID(s控件Name);
                    s标识Label = LKPageRetainOrReplace.GetString(s标识Label, LKExamEnvironment.部门名称);
                    s默认Option = LKPageRetainOrReplace.GetString(c下拉列表框.是否默认Option, "--请选择" + LKExamEnvironment.部门名称 + "--");
                    sL下拉列表 = LKExamSelectList.考试有关部门SelectList(s控件Name, c下拉列表框.考试设置ID);
                    break;

                /* 用于静态值项 */
                case MvcPager下拉列表适用环境.静态值项:
                    s控件Name = LKExamURLDefaultName.下拉静态值项(s控件Name);

                    #region 选择集合
                    switch (c下拉列表框.下拉列表静态值项)
                    {

                        /* 考官试题 */
                        case MvcPager下拉列表静态值项.考官试题:
                            s标识Label = LKPageRetainOrReplace.GetString(s标识Label, "试题类型");
                            sL下拉列表 = LKExamSelectList.考官试题SelectList(s控件Name);
                            break;

                        /* 考官试卷 */
                        case MvcPager下拉列表静态值项.考官试卷:
                            s标识Label = LKPageRetainOrReplace.GetString(s标识Label, "试卷类型");
                            sL下拉列表 = LKExamSelectList.考官试卷SelectList(s控件Name);
                            break;

                        /* 考生我要考试 */
                        case MvcPager下拉列表静态值项.考生我要考试:
                            s标识Label = LKPageRetainOrReplace.GetString(s标识Label, "考试类型");
                            sL下拉列表 = LKExamSelectList.考生我要考试SelectList(s控件Name);
                            break;

                        /* 考生考试记录 */
                        case MvcPager下拉列表静态值项.考生考试记录:
                            s标识Label = LKPageRetainOrReplace.GetString(s标识Label, "考试记录类型");
                            sL下拉列表 = LKExamSelectList.考生考试记录SelectList(s控件Name);
                            break;

                        /* 考生练习记录 */
                        case MvcPager下拉列表静态值项.考生练习记录:
                            s标识Label = LKPageRetainOrReplace.GetString(s标识Label, "练习记录类型");
                            sL下拉列表 = LKExamSelectList.考生练习记录SelectList(s控件Name);
                            break;
                        default:
                            break;
                    }
                    #endregion

                    break;

                /* 其他 */
                default:
                    break;
            }
            #endregion

            //DropDownList控件属性
            IDictionary<string, object> iDictionary = DropDownListDictionary(s控件Name, c下拉列表框.属性Attrs);

            MvcHtmlString tagLabel = LoveKao.Page.HTML.LabelExtensions.Label(htmlHelper, s标识Label + "：", iDictionary["id"].ToString(), new { @class = "markdepartment" });

            MvcHtmlString tagDropDownList = SelectExtensions.DropDownList(htmlHelper, s控件Name, sL下拉列表, s默认Option, iDictionary);

            return MvcHtmlString.Create(tagLabel.ToString() + tagDropDownList.ToString());

        }

    }
}