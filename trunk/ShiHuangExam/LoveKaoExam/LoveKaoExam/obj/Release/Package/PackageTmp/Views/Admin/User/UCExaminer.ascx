<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<考官>>" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<% string 考官编号 = LKExamEnvironment.考官编号; %>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <td class="mvcth-w80">
                <%=考官编号%>
            </td>
            <td class="mvcth-w90 mvc-borlf">
                姓名
            </td>
            <td class="mvcth-w60 mvc-borlf">
                性别
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
        <%foreach (考官 model in Model)
          {
        %>
        <tr>
            <td title="<%=考官编号 + "：" + model.编号 %>">
                <%=model.编号 %>
            </td>
            <td class="mvc-borlf" title="<%=model.姓名%>">
                <%=model.姓名%>
            </td>
            <td class="mvc-borlf">
                <%=model.性别名称%>
            </td>
            <td class="mvc-borlf">
                <%=model.创建时间.ToString("yyyy-MM-dd")%>
            </td>
            <td class="mvc-borlf" title="编辑该资料">
                <a href="javascript:void(0)" onclick="AdminiEmbedHandle.User.edit(1,'<%=model.ID %>');return false;">
                    编辑</a>
            </td>
            <td title="删除该账号">
                <a href="javascript:void(0)" onclick="AdminiEmbedHandle.User.Delete.confirm(1,'<%=model.ID %>','<%=model.编号 %>');return false;">
                    删除</a>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
<%=Html.MvcPagerAjaxPager(Model)%>
