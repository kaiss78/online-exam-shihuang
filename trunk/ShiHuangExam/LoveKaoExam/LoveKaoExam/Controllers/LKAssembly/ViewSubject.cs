using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using LoveKaoExam.Data;

namespace LoveKao.Members.Test
{
    public class 选项集合对象类
    {
        public 选项 选项;
        public string 大写字母;
    }

    public class 序号对象类
    {

        private int _序号数值 = 1;

        public int 序号数值
        {
            get { return _序号数值; }
            set { _序号数值 = value; }
        }

        private string _序号方式 = "1";
        public string 序号方式
        {
            get { return _序号方式; }
            set { _序号方式 = value; }
        }
    }

    public class ViewLibManage
    {
        public static string 数字转大写字母(int num)
        {
            return Convert.ToChar(num + 65).ToString();
        }

        public static string 数字转小写字母(int num)
        {
            return Convert.ToChar(num + 97).ToString();
        }
    }

    /// <summary>
    /// 头部HTML
    /// </summary>
    public class ViewLibHeader
    {
        public static string getNormal(string 题干HTML, Guid 试题外部信息id, decimal 分值, 序号对象类 序号对象, int 子小题序号, bool 是否为复合题, bool 是否为子小题)
        {
            string 分值HTML, 样式Text, 序号Text = "", 原题HTML;

            if (序号对象.序号方式 == "0")
            {
                if (是否为复合题)
                {
                    序号Text = "";
                }
                else if (是否为子小题)
                {
                    序号Text = (序号对象.序号数值) + ".";
                    序号对象.序号数值 += 1;
                }
                else
                {
                    序号Text = 序号对象.序号数值 + ".";
                    序号对象.序号数值 += 1;
                }
            }
            else
            {
                if (是否为子小题)
                {
                    序号Text = 序号对象.序号数值 - 1 + "." + (子小题序号 + 1) + ")";
                }
                else
                {
                    序号Text = 序号对象.序号数值 + ".";
                    序号对象.序号数值 += 1;
                }
            }

            分值HTML = 分值 == 0 ? "" : "<span class=\"stViewScore\">(" + 分值 + "分)</span>";
            题干HTML = string.IsNullOrEmpty(题干HTML) ? "<span class=\"zf60\">(题干内容为空)</span>" : 题干HTML;
            样式Text = 是否为子小题 ? "stViewOrdNum_" : "stViewOrdNum";
            原题HTML = 是否为子小题 ? "" : "<a class=\"chakanyuanti\" href=\"javascript:void(0)\" onclick=\"ExaminerEmbedHandle.Subject.view('"+试题外部信息id+"');return false;\" title=\"点击查看原题\">[原题]</a>";

            return "<table>" +
                    "    <tbody>" +
                    "        <tr>" +
                    "            <td style=\"vertical-align: top;white-space:nowrap;\">" +
                    "               <label class=\"" + 样式Text + "\" style=\"vertical-align:text-bottom;\">" + 序号Text + "</label>" +
                    "            </td>" +
                    "            <td>" +
                    "               <div style=\"vertical-align:middle;\">" + 题干HTML + 分值HTML + 原题HTML + "</div>" +
                    "            </td>" +
                    "        </tr>" +
                    "    </tbody>" +
                    "</table>";
        }

        public static Regex regex(int num)
        {
            Regex reg = new Regex("<img\\sclass=\"UnderLine" + num + "\"\\salt=\"\"\\ssrc=\"/Shared/UnderLine\\?index=(\\d+)([^>]*)/>", RegexOptions.Multiline & RegexOptions.IgnoreCase);
            return reg;
        }

        public static string getT13(string 题干HTML, Guid 试题外部信息id, decimal 分值, 序号对象类 序号对象, int 子小题序号, bool 是否为复合题, bool 是否为子小题)
        {
            题干HTML = regex(13).Replace(题干HTML, " ______ ");
            return getNormal(题干HTML, 试题外部信息id, 分值, 序号对象, 子小题序号, 是否为复合题, 是否为子小题);
        }

        public static string getT14(string 题干HTML, Guid 试题外部信息id, decimal 分值, 序号对象类 序号对象, int 子小题序号, bool 是否为复合题, bool 是否为子小题)
        {
            题干HTML = regex(14).Replace(题干HTML, " ___<span>$1</span>___ ");
            return getNormal(题干HTML, 试题外部信息id, 分值, 序号对象, 子小题序号, 是否为复合题, 是否为子小题);
        }

        public static string getT15(string 题干HTML, Guid 试题外部信息id, decimal 分值, 序号对象类 序号对象, int 子小题序号, bool 是否为复合题, bool 是否为子小题)
        {
            题干HTML = regex(15).Replace(题干HTML, " ___<span>$1</span>___ ");
            return getNormal(题干HTML, 试题外部信息id, 分值, 序号对象, 子小题序号, 是否为复合题, 是否为子小题);
        }
    }

    /// <summary>
    /// 内容HTML
    /// </summary>
    public class ViewLibContent
    {
        public static string getOptionItem(string html)
        {
            return "<div class=\"stViewOption\">" + html + "</div>";
        }

        public static string getOptionText(string letter, string html)
        {
            html = "<span class=\"stViewOpText\" >" +
                        "<label>(" + letter + ") </label>" +
                        html +
                    "</span>";
            return getOptionItem(html);
        }

        public static string getTable(string trs)
        {
            return "<table  cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tbody>" + trs + "</tbody></table>";
        }

        public static string getT11(List<选项> 选项列表)
        {
            string html = "";

            //遍历选项列表
            for (var i = 0; i < 选项列表.Count; i++)
            {
                html += getOptionText(ViewLibManage.数字转大写字母(i), 选项列表[i].选项内容HTML);
            }
            return html;
        }

        public static string getT12(List<选项> 选项列表)
        {
            string html = "";

            //遍历选项列表
            for (var i = 0; i < 选项列表.Count; i++)
            {
                html += getOptionText(ViewLibManage.数字转大写字母(i), 选项列表[i].选项内容HTML);
            }
            return html;
        }

        public static string getT14Option(int index, string 选项内容HTML)
        {
            return "<td class=\"stViewOpBtnT14\"><label>" + ViewLibManage.数字转大写字母(index) + ").</label>" +
                   "<span>" + 选项内容HTML + "</span></td>";
        }

        public static string getT14(List<选项> 选项集合)
        {

            int 选项长度 = 选项集合.Count,
                余数值;
            string html = "";

            //遍历选项集合
            for (var i = 0; i < 选项长度; i++)
            {
                余数值 = i % 4;
                if (余数值 == 0)
                {
                    html += "<tr>";
                }
                html += getT14Option(i, 选项集合[i].选项内容HTML);
                if (余数值 == 3 || i == 选项长度 - 1)
                {
                    html += "</tr>";
                }
            }
            return getTable(html);
        }

        public static string getT15(List<单选空格> 单选空格集合)
        {
            单选空格 单选空格;
            List<选项> 选项集合;
            选项 选项;
            string html = "";

            //遍历单选空格集合
            for (var i = 0; i < 单选空格集合.Count; i++)
            {
                单选空格 = 单选空格集合[i];
                选项集合 = 单选空格.选项组.选项集合;
                html += "<tr><td>" + (i + 1) + ".</td>";
                for (var k = 0; k < 选项集合.Count; k++)
                {
                    选项 = 选项集合[k];
                    html += "<td class=\"stViewOpBtnT14\">(" + ViewLibManage.数字转大写字母(k) + ")" + 选项.选项内容HTML + "</td>";
                }
                html += "</tr>";
            }
            return getTable(html);
        }

        public static string getT20()
        {
            return "<span class=\"stViewOpText\"><label>正确</label></span>" +
                   "<span class=\"stViewOpText\" style=\"padding-left:10px;\"><label>错误</label></span>"; ;
        }


        public static string getT30RowText(string html)
        {
            return "<a class=\"stViewOpTextT30\" style=\"padding-right:5px;\">" + html + "</a>";
        }

        public static string getT30CellText(int index, string contLeft, string contRight)
        {
            string _number = "<span  class=\"stViewT30OrdNum\">" + (index + 1) + ".</span>",
                   _answerLeft = getT30RowText(contLeft),
                   _answerRight = getT30RowText(contRight),
                   _html = _number + _answerLeft + _answerRight;
            return "<div class=\"stViewT30Cell\">" + _html + "</div>";
        }

        public static string getT30Module选项(string header, string list)
        {
            return "<td><div>" + header + "<div>" + list + "</div></div></td>";
        }

        public static string getT30Module答案(string header, string list)
        {
            return "<td><div>" + header + "<div>" + list + "</div></div></td>";
        }

        public static string getTrT30Option(int index, string content, bool isLeft)
        {
            return "<tr><td class=\"stViewOpTextT30\">" +
                        "<label>" + (isLeft ? ViewLibManage.数字转大写字母(index) : ViewLibManage.数字转小写字母(index)) + ").</label>" +
                        "<span>" + content + "</span>" +
                   "</td></tr>";
        }

        public static string getTableT30(string trs)
        {
            return "<table  cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"display:inline;\" ><tbody>" + trs + "</tbody></table>";
        }

        public static string getT30选项LeftList(List<连线题选项> 连线题左选项集合)
        {
            string trs = "";
            //遍历连线题左选项集合
            for (var i = 0; i < 连线题左选项集合.Count; i++)
            {
                trs += getTrT30Option(i, 连线题左选项集合[i].选项内容文本, true);
            }
            return getTableT30(trs);
        }

        public static string getT30选项RightList(List<连线题选项> 连线题右选项集合)
        {
            string trs = "";
            //遍历连线题右选项集合
            for (var i = 0; i < 连线题右选项集合.Count; i++)
            {
                trs += getTrT30Option(i, 连线题右选项集合[i].选项内容文本, false);
            }
            return getTableT30(trs);
        }

        public static string getT30Head选项(string types)
        {
            return "<div class=\"stViewT30BoxHead\"><span>" + (types == "Left" ? "左选项" : "右选项") + "</span></div>";
        }

        public static string getT30Head答案(string types)
        {
            return "<div class=\"stViewT30BoxHead\"><span>" + (types == "RightAnswer" ? "参考答案" : "您的回答") + "</span></div>";
        }

        public static string getT30选项(连线题 连线题)
        {
            string _leftHead = getT30Head选项("Left"),
                _leftList = getT30选项LeftList(连线题.连线题左选项集合),
                _rightHead = getT30Head选项("Right"),
                _rightList = getT30选项RightList(连线题.连线题右选项集合);

            return getT30Module选项(_leftHead, _leftList) +
                   getT30Module选项(_rightHead, _rightList);
        }

        public static string getT30(连线题 连线题)
        {
            List<连线题选项> 连线题左选项集合 = 连线题.连线题左选项集合;
            List<连线题选项> 连线题右选项集合 = 连线题.连线题右选项集合;
            List<连线题答案> 连线题答案集合 = 连线题.连线题答案集合;
            连线题答案 连线题答案;
            Dictionary<Guid, int> 连线题左选项Data = new Dictionary<Guid, int>();
            Dictionary<Guid, int> 连线题右选项Data = new Dictionary<Guid, int>();
            string 大写字母, 小写字母, html = "";

            //遍历连线题左选项集合
            for (var i = 0; i < 连线题左选项集合.Count; i++)
            {
                连线题左选项Data[连线题左选项集合[i].ID] = i;
            }

            //遍历连线题右选项集合
            for (var i = 0; i < 连线题右选项集合.Count; i++)
            {
                连线题右选项Data[连线题右选项集合[i].ID] = i;
            }

            //遍历连线题答案集合
            for (var i = 0; i < 连线题答案集合.Count; i++)
            {
                连线题答案 = 连线题答案集合[i];

                大写字母 = ViewLibManage.数字转大写字母(连线题左选项Data[连线题答案.连线题左选项ID]);
                小写字母 = ViewLibManage.数字转小写字母(连线题右选项Data[连线题答案.连线题右选项ID]);

                html += getT30CellText(i, 大写字母, 小写字母);
            }

            string tds = getT30选项(连线题) + getT30Module答案(getT30Head答案("RightAnswer"), html);
            return getTable("<tr>" + tds + "</tr>");
        }

        public static string getT40(List<试卷大题中试题> 子小题集合, 序号对象类 序号对象, bool 是否显示答案)
        {
            string _html = "";
            if (子小题集合 != null)
            {
                for (var i = 0; i < 子小题集合.Count; i++)
                {
                    _html += ViewSubject.getMarkItem(子小题集合[i], 序号对象, i, true, 是否显示答案);
                }
            }
            return _html;
        }

        public static string getT80OpList(List<选项> 选项集合)
        {
            var _html = "";
            for (var k = 0; k < 选项集合.Count; k++)
            {
                _html += getOptionText(ViewLibManage.数字转大写字母(k), 选项集合[k].选项内容HTML);
            }
            return "<div class=\"stViewItem\" style=\"padding-top:10px;\" >" + _html + "</div>";
        }

        public static string getT80(多题干共选项题 多题干共选项题, List<试卷大题中试题> 子小题集合, 序号对象类 序号对象, bool 是否显示答案)
        {
            string html = "",
                   htmlHead = "",
                   htmlCont = "",
                   htmlFoot = "";

            试卷大题中试题 试题;
            Guid 选项组ID;
            List<选项组> 选项组集合 = 多题干共选项题.选项组集合;
            选项组 选项组;
            Dictionary<Guid, List<选项>> 选项组集合Data = new Dictionary<Guid, List<选项>>();

            List<Guid> 子集合List = new List<Guid>();

            //遍历选项组
            for (var i = 0; i < 选项组集合.Count; i++)
            {
                选项组 = 选项组集合[i];
                选项组集合Data[选项组.ID] = 选项组.选项集合;
            }

            //遍历子小题集合
            for (var i = 0; i < 子小题集合.Count; i++)
            {
                试题 = 子小题集合[i];
                试题.试题内容 = 题干.把Json转化成试题内容(试题.试题内容Json);
                题干 T8011 = (题干)试题.试题内容;
                选项组ID = T8011.选项组ID;

                if (!子集合List.Contains(选项组ID))
                {
                    子集合List.Add(选项组ID);
                    html += getT80OpList(选项组集合Data[选项组ID]);
                }

                htmlHead = ViewLibHeader.getNormal(T8011.题干HTML, T8011.试题外部信息ID, 试题.每小题分值, 序号对象, i, false, true);
                htmlFoot = ViewLibKeyResolu.getT8011(T8011, 选项组集合, 是否显示答案);
                html += ViewSubject.getItemHtml(htmlHead, htmlCont, htmlFoot, false);
            }

            return html;
        }

    }

    /// <summary>
    /// 答案
    /// </summary>
    public class ViewLibKeyResolu
    {
        public static string getKeyItem(string html)
        {
            return "<div class=\"stViewCell32\">" + html + "</div>";
        }

        public static string getResItem(string html)
        {
            return "<div style=\"padding-top:5px;\">" +
                    "<span class=\"z333\">解题思路：</span>" +
                    "<span class=\"z666\">" + (html == "" ? "无" : html) + "</span>" +
                   "</div>";
        }

        public static string getRightRow(string html)
        {
            return "<span class=\"z333\">参考答案：</span>" +
                   "<span class=\"keyFontRight\">" + (html == "" ? "无" : html) + "</span>";
        }

        public static string getKeyItemDiv(string html)
        {
            return "<div class=\"stViewCell\">" + html + "</div>";
        }

        public static string getT11(单选题 单选题, bool 是否显示答案)
        {
            if (!是否显示答案) { return ""; }

            List<选项> 选项列表 = 单选题.选项列表;
            Guid 答案ID = 单选题.答案ID;
            string 大写字母 = "";

            //遍历选项列表
            for (var i = 0; i < 选项列表.Count; i++)
            {
                if (答案ID == 选项列表[i].ID)
                {
                    大写字母 = ViewLibManage.数字转大写字母(i);
                    break;
                }
            }
            return getKeyItem(getRightRow(大写字母)) +
                   getResItem(单选题.解题思路);
        }

        public static string getT12(多选题 多选题, bool 是否显示答案)
        {
            if (!是否显示答案) { return ""; }

            List<选项> 选项列表 = 多选题.选项列表;
            List<Guid> 答案列表 = 多选题.答案列表;
            string 字母列表 = "";

            //遍历选项列表
            for (var i = 0; i < 选项列表.Count; i++)
            {
                //遍历正确答案列表
                for (var k = 0; k < 答案列表.Count; k++)
                {
                    if (答案列表[k] == 选项列表[i].ID)
                    {
                        字母列表 += ViewLibManage.数字转大写字母(i) + ",";
                    }
                }
            }
            字母列表 = 字母列表.Length > 0 ? 字母列表.Substring(0, 字母列表.Length - 1) : 字母列表;
            return getKeyItem(getRightRow(字母列表)) +
                   getResItem(多选题.解题思路);
        }

        public static string[] getT13BlankData(List<填空空格答案> 填空空格答案集合)
        {
            string[] 填空空格答案集合Data = new string[填空空格答案集合.Count];

            //遍历空格答案
            for (var i = 0; i < 填空空格答案集合.Count; i++)
            {
                填空空格答案集合Data[i] = 填空空格答案集合[i].答案内容;
            }
            return 填空空格答案集合Data;
        }

        public static string getTdT13Blank(int index, string[] 填空空格答案集合Data)
        {
            return "<td style=\"padding-right:5px;\" class=\"keyFontOrdNum\">(" + (index + 1) + ").</td>" +
                   "<td class=\"z333\" style=\"padding-right:5px;\">参考答案:</td>" +
                   "<td><span class=\"keyFontRight\">" +
                        string.Join("</span><samp class=\"keyFontOr\">或</samp><span class=\"keyFontRight\">", 填空空格答案集合Data) + "</span>" +
                   "</td>";
        }

        public static string getT13(填空题 填空题, bool 是否显示答案)
        {
            if (!是否显示答案) { return ""; }
            List<填空空格> 填空空格集合 = 填空题.填空空格集合;
            填空空格 填空空格;
            string[] 填空空格答案集合Data;
            string html = "";

            //遍历空格集合
            for (var i = 0; i < 填空空格集合.Count; i++)
            {
                填空空格 = 填空空格集合[i];
                填空空格答案集合Data = getT13BlankData(填空空格.填空空格答案集合);

                html += "<tr>" + getTdT13Blank(i, 填空空格答案集合Data);
                html += "</tr>";
            }
            html = html != "" ? ViewLibContent.getTable(html) : ViewLibKeyResolu.getRightRow("无");

            return getKeyItemDiv(html) +
                   getResItem(填空题.解题思路);
        }

        public static Dictionary<Guid, 选项集合对象类> get选项集合对象(List<选项> 选项集合)
        {
            选项 选项;
            Dictionary<Guid, 选项集合对象类> 选项集合Data = new Dictionary<Guid, 选项集合对象类>();
            //遍历选项集合
            for (var i = 0; i < 选项集合.Count; i++)
            {
                选项 = 选项集合[i];
                选项集合对象类 选项集合对象 = new 选项集合对象类();
                选项集合对象.选项 = 选项;
                选项集合对象.大写字母 = ViewLibManage.数字转大写字母(i);
                选项集合Data[选项.ID] = 选项集合对象;
            }

            return 选项集合Data;
        }

        public static string getT14(选词填空 选词填空, bool 是否显示答案)
        {
            if (!是否显示答案) { return ""; }

            Dictionary<Guid, 选项集合对象类> 选项集合Data = get选项集合对象(选词填空.选项组.选项集合);

            List<选词空格> 选词空格集合 = 选词填空.选词空格集合;
            Guid 答案ID;
            string html = "<span class=\"z333\">参考答案：</span>";

            //遍历选词空格集合
            for (var i = 0; i < 选词空格集合.Count; i++)
            {
                答案ID = 选词空格集合[i].答案ID;
                html += "<span>(" + (i + 1) + ")</span><span class=\"keyFontRight\" style=\"padding-right:10px;\">" + (选项集合Data[答案ID].大写字母) + "</span>";
            }
            return getKeyItemDiv(html) +
                   getResItem(选词填空.解题思路);
        }

        public static string getT15(完形填空 完形填空, bool 是否显示答案)
        {
            if (!是否显示答案) { return ""; }

            List<单选空格> 单选空格集合 = 完形填空.单选空格集合;
            单选空格 单选空格;
            Guid 答案ID;
            string html = "<span class=\"z333\">参考答案：</span>";

            //遍历单选空格集合
            for (var i = 0; i < 单选空格集合.Count; i++)
            {
                单选空格 = 单选空格集合[i];
                Dictionary<Guid, 选项集合对象类> 选项集合Data = get选项集合对象(单选空格.选项组.选项集合);
                答案ID = 单选空格.答案ID;
                html += "<span>(" + (i + 1) + ")</span><span class=\"keyFontRight\" style=\"padding-right:10px;\">" + (选项集合Data[答案ID].大写字母) + "</span>";
            }
            return getKeyItemDiv(html) +
                   getResItem(完形填空.解题思路);
        }

        public static string getT20(判断题 判断题, bool 是否显示答案)
        {
            if (!是否显示答案) { return ""; }

            return getKeyItem(getRightRow(判断题.答案 ? "正确" : "错误")) +
                   getResItem(判断题.解题思路);
        }

        public static string getT30(连线题 连线题, bool 是否显示答案)
        {
            if (!是否显示答案) { return ""; }
            return getResItem(连线题.解题思路);
        }

        public static string getT40(复合题 复合题, bool 是否显示答案)
        {
            if (!是否显示答案) { return ""; }
            return getResItem(复合题.解题思路);
        }

        public static string getT60(问答题 问答题, bool 是否显示答案)
        {
            if (!是否显示答案) { return ""; }

            return getKeyItemDiv(getRightRow(问答题.答案)) +
                   getResItem(问答题.解题思路);
        }

        public static string getT40(多题干共选项题 多题干共选项题, bool 是否显示答案)
        {
            if (!是否显示答案) { return ""; }
            return getResItem(多题干共选项题.解题思路);
        }

        public static string getT8011(题干 题干, List<选项组> 选项组集合, bool 是否显示答案)
        {
            if (!是否显示答案) { return ""; }
            选项组 选项组;
            List<选项> 选项集合;
            Guid 选项组ID = 题干.选项组ID;
            Guid 答案ID = 题干.选项组ID;
            string html = "";

            for (var i = 0; i < 选项组集合.Count; i++)
            {
                选项组 = 选项组集合[i];
                if (选项组ID == 选项组.ID)
                {
                    选项集合 = 选项组.选项集合;

                    for (var k = 0; k < 选项集合.Count; k++)
                    {
                        if (选项集合[k].ID == 答案ID)
                        {
                            html = ViewLibManage.数字转大写字母(k);
                            break;
                        }
                    }
                    break;
                }
            }
            return getKeyItem(getRightRow(html)) +
                   getResItem(题干.解题思路);
        }
    }

    public class ViewSubject
    {
        //返回标题HTML
        public static string getItemHead(string html, bool isParent)
        {
            return "<div class=\"stViewHead\" " + (isParent ? "style=\"padding-bottom:0px;\"" : "") + ">" + html + "</div>";
        }

        //返回内容HTML
        public static string getItemCont(string html)
        {
            return "<div class=\"stViewCont\" style=\"margin-left:20px;\">" + html + "</div>";
        }

        //返回底部HTML
        public static string getItemFoot(string html)
        {
            return html == "" ? "" : "<div class=\"stViewFoot\">" + html + "</div>";
        }

        public static string getItemHtml(string htmlHead, string htmlCont, string htmlFoot, bool 是否为父级)
        {
            return "<div class=\"stViewItem\">" +
                      getItemHead(htmlHead, 是否为父级) +
                      getItemCont(htmlCont) +
                      getItemFoot(htmlFoot) +
                    "</div>";
        }

        public static string getMarkItem(试卷大题中试题 试题, 序号对象类 序号对象, int 子小题序号, bool 是否为小题, bool 是否显示答案)
        {
            string htmlHead = "", htmlCont = "", htmlFoot = "";
            bool 是否为父级 = false;
            int 小题型Enum = 试题内容.得到试题的小题型(试题.试题内容Json);
            switch (小题型Enum)
            {
                case 11:
                    {
                        试题.试题内容 = 单选题.把Json转化成试题内容(试题.试题内容Json);
                        单选题 T11 = (单选题)试题.试题内容;
                        htmlHead += ViewLibHeader.getNormal(T11.题干HTML, T11.试题外部信息ID, 试题.每小题分值, 序号对象, 子小题序号, false, 是否为小题);
                        htmlCont += ViewLibContent.getT11(T11.选项列表);
                        htmlFoot += ViewLibKeyResolu.getT11(T11, 是否显示答案);
                    }
                    break;
                case 12:
                    {
                        试题.试题内容 = 多选题.把Json转化成试题内容(试题.试题内容Json);
                        多选题 T12 = (多选题)试题.试题内容;
                        htmlHead += ViewLibHeader.getNormal(T12.题干HTML, T12.试题外部信息ID, 试题.每小题分值, 序号对象, 子小题序号, false, 是否为小题);
                        htmlCont += ViewLibContent.getT12(T12.选项列表);
                        htmlFoot += ViewLibKeyResolu.getT12(T12, 是否显示答案);
                    }
                    break;
                case 13:
                    {
                        试题.试题内容 = 填空题.把Json转化成试题内容(试题.试题内容Json);
                        填空题 T13 = (填空题)试题.试题内容;
                        htmlHead += ViewLibHeader.getT13(T13.题干HTML, T13.试题外部信息ID, 试题.每小题分值, 序号对象, 子小题序号, false, 是否为小题);
                        htmlFoot += ViewLibKeyResolu.getT13(T13, 是否显示答案);
                    }
                    break;
                case 14:
                    {
                        试题.试题内容 = 选词填空.把Json转化成试题内容(试题.试题内容Json);
                        选词填空 T14 = (选词填空)试题.试题内容;
                        htmlHead += ViewLibHeader.getT14(T14.题干HTML, T14.试题外部信息ID, 试题.每小题分值, 序号对象, 子小题序号, false, 是否为小题);
                        htmlCont += ViewLibContent.getT14(T14.选项组.选项集合);
                        htmlFoot += ViewLibKeyResolu.getT14(T14, 是否显示答案);
                    }
                    break;

                case 15:
                    {
                        试题.试题内容 = 完形填空.把Json转化成试题内容(试题.试题内容Json);
                        完形填空 T15 = (完形填空)试题.试题内容;
                        htmlHead += ViewLibHeader.getT15(T15.题干HTML, T15.试题外部信息ID, 试题.每小题分值, 序号对象, 子小题序号, false, 是否为小题);
                        htmlCont += ViewLibContent.getT15(T15.单选空格集合);
                        htmlFoot += ViewLibKeyResolu.getT15(T15, 是否显示答案);
                    }
                    break;

                case 20:
                    {
                        试题.试题内容 = 判断题.把Json转化成试题内容(试题.试题内容Json);
                        判断题 T20 = (判断题)试题.试题内容;
                        htmlHead += ViewLibHeader.getNormal(T20.题干HTML, T20.试题外部信息ID, 试题.每小题分值, 序号对象, 子小题序号, false, 是否为小题);
                        htmlCont += ViewLibContent.getT20();
                        htmlFoot += ViewLibKeyResolu.getT20(T20, 是否显示答案);
                    }
                    break;

                case 30:
                    {
                        试题.试题内容 = 连线题.把Json转化成试题内容(试题.试题内容Json);
                        连线题 T30 = (连线题)试题.试题内容;
                        htmlHead += ViewLibHeader.getNormal(T30.题干HTML, T30.试题外部信息ID, 试题.每小题分值, 序号对象, 子小题序号, false, 是否为小题);
                        htmlCont += ViewLibContent.getT30(T30);
                        htmlFoot += ViewLibKeyResolu.getT30(T30, 是否显示答案);
                    }
                    break;

                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                    {
                        是否为父级 = true;
                        试题.试题内容 = 复合题.把Json转化成试题内容(试题.试题内容Json);
                        复合题 T40 = (复合题)试题.试题内容;
                        htmlHead += ViewLibHeader.getNormal(T40.题干HTML, T40.试题外部信息ID, 试题.每小题分值, 序号对象, 子小题序号, true, 是否为小题);
                        htmlCont += ViewLibContent.getT40(试题.子小题集合, 序号对象, 是否显示答案);
                        htmlFoot += ViewLibKeyResolu.getT40(T40, 是否显示答案);
                    }
                    break;

                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                    {
                        试题.试题内容 = 问答题.把Json转化成试题内容(试题.试题内容Json);
                        问答题 T60 = (问答题)试题.试题内容;
                        htmlHead += ViewLibHeader.getNormal(T60.题干HTML, T60.试题外部信息ID, 试题.每小题分值, 序号对象, 子小题序号, false, 是否为小题);
                        htmlFoot += ViewLibKeyResolu.getT60(T60, 是否显示答案);
                    }
                    break;

                case 80:
                    {
                        是否为父级 = true;
                        试题.试题内容 = 多题干共选项题.把Json转化成试题内容(试题.试题内容Json);
                        多题干共选项题 T80 = (多题干共选项题)试题.试题内容;
                        htmlHead += ViewLibHeader.getNormal(T80.题干HTML, T80.试题外部信息ID, 试题.每小题分值, 序号对象, 子小题序号, true, 是否为小题);
                        htmlCont += ViewLibContent.getT80(T80, 试题.子小题集合, 序号对象, 是否显示答案);
                        htmlFoot += ViewLibKeyResolu.getT40(T80, 是否显示答案);
                    }
                    break;
            }

            return getItemHtml(htmlHead, htmlCont, htmlFoot, 是否为父级);
        }
    }
}
