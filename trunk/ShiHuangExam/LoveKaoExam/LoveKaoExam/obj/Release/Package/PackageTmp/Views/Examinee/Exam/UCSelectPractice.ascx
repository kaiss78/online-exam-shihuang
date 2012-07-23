<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<练习设置>>" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <td>
                练习名称
            </td>
            <td class="mvcth-w60 mvc-borlf">
                练习时长
            </td>
            <td class="mvcth-w110 mvc-borlf">
                发布时间
            </td>
            <td class="mvcth-w70 mvc-borlf">
                出卷人
            </td>
            <td class="mvcth-w90">
                我要练习
            </td>
        </tr>
        <%foreach (练习设置 model in Model)
          {
        %>
        <tr>
            <td class="mvctb-lp10" title="<%=LKExamText.网页标签Title(model.试卷内容.名称) %>">
                <%=model.试卷内容.名称%>
            </td>
            <td class="mvc-borlf">
                <%=model.考试时长%>
            </td>
            <td class="mvc-borlf">
                <%=model.设置时间.ToString("yyyy-MM-dd HH:mm")%>
            </td>
            <td class="mvc-borlf">
                <%=model.设置人.姓名%>
            </td>
            <td title="进入考场练习">
                <%=Html.ActionLink("进入练习>>", "OnlineExam", "Exam", new { guid = model.试卷内容ID, type = 0 }, new { @class = "examroom" ,target="_blank"})%>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
<%=Html.MvcPagerNormalPager(Model)%>
