<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<考试设置>>" %>
<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <td>
                试卷名称
            </td>
            <td class="mvcth-w60 mvc-borlf">
                考试时长
            </td>
            <td class="mvcth-w110 mvc-borlf">
                开始时间
            </td>
            <td class="mvcth-w110 mvc-borlf">
                发布时间
            </td>
            <td class="mvcth-w70 mvc-borlf">
                出卷人
            </td>
            <td class="mvcth-w90">
                我要考试
            </td>
        </tr>
        <%foreach (考试设置 model in Model)
          {
        %>
        <tr>
            <td class="mvctb-lp10" title="<%=LKExamText.网页标签Title(model.试卷内容.名称) %>">
                <%=LKExamText.试卷列表(model.试卷内容.名称, 46)%>
            </td>
            <td class="mvc-borlf">
                <%=model.考试时长 %>
            </td>
            <td class="mvc-borlf">
                <%=model.考试开始时间.ToString("yyyy-MM-dd HH:mm")%>
            </td>
            <td class="mvc-borlf">
                <%=model.考试结束时间.ToString("yyyy-MM-dd HH:mm")%>
            </td>
            <td class="mvc-borlf">
                <%=model.设置人.姓名 %>
            </td>
            <td>
                <%switch (model.考试状态)
                  {%>
                <%case 0:
                              {  %>
                <%=Html.Label("考试时间未到", new { @style = "color:#369;", title = "考试时间还没到,请继续等待！" })%>
                <%break;
                  } %>
                <%case 1:
                      {  %>
                <%=Html.ActionLink("进入考场>>", "OnlineExam", "Exam", new { guid = model.ID, type = 1 }, new { target = ATarget._blank, @class = "examroom", title = "你可以进入考场"})%>
                <%break;
                  } %>
                <%case 2:
                      {  %>
                <%=Html.Label("考试时间已过", new { @style = "color:#369;", title = "你已经错过考试时间,不能进入考场" })%>
                <%break;
                  } %>
                <%default:
                      {  %>
                <%=Html.Label("已经考过", new { @style = "color:#369;", title = "你已经考过该试卷,不能进入考场" })%>
                <%break;
                          } %>
                <%} %>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
<%=Html.MvcPagerNormalPager(Model)%>
