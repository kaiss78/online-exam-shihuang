<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<部门>>" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<%string 部门名称 = LKExamEnvironment.部门名称;%>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <td class="mvcth-w90 mvc-borlf">
                <%=部门名称%>名称
            </td>
            <td class="mvcth-w90 mvc-borlf">
                创建时间
            </td>
            <td class="mvcth-w60 mvc-borlf">
                编辑
            </td>
            <td class="mvcth-w60">
                删除
            </td>
        </tr>
        <%foreach (部门 model in Model)
          {
        %>
        <tr>
            <td class="mvc-borlf" title="<%=model.名称 %>">
                <%=model.名称 %>
            </td>
            <td class="mvc-borlf">
                <%=model.创建时间.ToString("yyyy-MM-dd")%>
            </td>
            <td class="mvc-borlf" title="编辑该<%=部门名称 %>">
                <a href="javascript:void(0)" onclick="AdminiEmbedHandle.Department.edit('<%=model.ID %>');return false;">
                    编辑</a>
            </td>
            <td title="删除该<%=部门名称 %>">
                <a href="javascript:void(0)" onclick="AdminiEmbedHandle.Department.Delete.confirm('<%=model.ID %>','<%=model.名称 %>');return false;">
                    删除</a>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
<%=Html.MvcPagerAjaxPager(Model)%>
