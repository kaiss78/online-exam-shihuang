<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<考生做过的试卷>>" %>
<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<!--
(0)值0-代表全部练习
(1)值1-代表已完成练习
(2)值2-代表未完成练习
-->
<%int b已完成考试 = LKExamURLQueryKey.下拉静态值项(); %>
<%string s操作练习 = LKPageRetainOrReplace.GetString(b已完成考试, "操 作", "再练一次", "继续练习");%>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <td>
                试卷名称
            </td>
            <td class="mvcth-w60 mvc-borlf">
                考试时长
            </td>
            <td class="mvcth-w70 mvc-borlf">
                得分/总分
            </td>
            <%=Html.MvcHtmlTag_RetainOrReplace(b已完成考试, TagName.TD,"是否完成", "是否及格", new { @class = "mvcth-w55 mvc-borlf" })%>
            <td class="mvcth-w110 mvc-borlf">
                <%=LKPageRetainOrReplace.GetString(b已完成考试, "开始练习时间", "完成练习时间", "开始练习时间")%>
            </td>
            <td class="mvcth-w70 mvc-borlf">
                <%=LKPageRetainOrReplace.GetString(b已完成考试, "查看答卷", "查看答卷", "")%>
            </td>
            <td class="mvcth-w70 mvc-borlf">
                <%=s操作练习 %>
            </td>
        </tr>
        <%foreach (考生做过的试卷 model in Model)
          {
        %>
        <tr>
            <td class="mvctb-lp10" title="<%=LKExamText.网页标签Title(model.练习设置.试卷内容.名称) %>">
                <%=LKExamText.试卷列表(model.练习设置.试卷内容.名称,46)%>
            </td>
            <td class="mvc-borlf">
                <%=model.练习设置.考试时长%>
            </td>
            <td class="mvc-borlf">
                <%=model.总得分 %>/<%=model.练习设置.试卷内容.总分%>
            </td>
            <% 
              string 是否完成, 是否继续, 是否查看答案;
              int examTypea = 0;
              Guid modelID;

if (model.是否是已提交的试卷)
{
    是否完成 = "已完成";
    是否继续 = "再练一次";
    modelID = model.练习设置.试卷内容ID;
    是否查看答案 = "查看答案";
   
}
else
{
    是否完成 = "未完成";
    是否继续 = "继续练习";
    modelID = model.ID;
    是否查看答案 = "";
    examTypea = 3;
}
            %>
           
                <%=Html.MvcHtmlTag_RetainOrReplace(b已完成考试, TagName.TD,是否完成, model.及格情况, new { @class = "mvc-borlf" })%>
          
            <td class="mvc-borlf">
                <%string s答题结束时间 = LKPageRetainOrReplace.GetString(model.答题结束时间, "yyyy-MM-dd HH:mm"); %>
                <%string s答题开始时间 = model.答题开始时间.ToString("yyyy-MM-dd HH:mm"); %>
                <%=LKPageRetainOrReplace.GetString(b已完成考试,s答题开始时间,s答题结束时间, s答题开始时间)%>
            </td>
          <%if (true)
              { %>
            <td class="mvc-borlf" title="<%=是否查看答案 %>">
                <a href="/Exam/OnlineExam/?guid=<%=model.ID%>&type=2" target="_blank">
                    <%=是否查看答案%></a>
            </td>
            <%} %>
         
            <td class="mvc-borlf" title="<%=是否继续 %>">
                <%=Html.ActionLink(是否继续 + ">>", "OnlineExam", "Exam", new { guid = modelID, type = examTypea }, new { @class = "examroom", target = "_blank" })%>
            </td>
        </tr>
        <%
          } %>
    </tbody>
</table>
<%=Html.MvcPagerNormalPager(Model)%>
