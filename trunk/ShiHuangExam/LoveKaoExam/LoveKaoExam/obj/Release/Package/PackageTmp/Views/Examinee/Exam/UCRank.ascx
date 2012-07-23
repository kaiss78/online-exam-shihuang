<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<考试排行Models>" %>

<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Models.Examinee" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>

<%PagedList<考试排名> pagedList考试排名 = Model.PagedList考试排名; %>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <td class="mvcth-w60">
                名次
            </td>
            <td class="mvcth-w80 mvc-borlf">
                <%=LKExamEnvironment.考生编号 %>
            </td>
            <td class="mvcth-w100 mvc-borlf">
                姓名
            </td>
            <td class="mvcth-w90 mvc-borlf">
                总得分
            </td>
            <td class="mvcth-w55">
                是否及格
            </td>
        </tr>
        
        <%foreach (考试排名 model in pagedList考试排名)
          {
        %>
        <tr>
            <td title="第<%=model.名次%>名">
                <%=model.名次%>
            </td>
            <td class="mvc-borlf" title="<%=model.考生.编号 %>">
                <%=model.考生.编号 %>
            </td>
            <td class="mvc-borlf" title="<%=model.考生.姓名 %>">
                <%=model.考生.姓名 %>
            </td>
            <td class="mvc-borlf" title="<%=model.总得分 %>">
                <%=model.总得分 %>
            </td>
            <td title="<%=model.及格情况%>">
                <%=model.及格情况%>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
<%=Html.MvcPagerAjaxPager(pagedList考试排名)%>
