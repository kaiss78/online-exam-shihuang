<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<试题外部信息>>" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <td>
                试题题干
            </td>
            <td class="mvcth-w90 mvc-borlf">
                所属题型
            </td>
            <td class="mvcth-w60 mvc-borlf">
                难易度
            </td>
            <td class="mvcth-w110 mvc-borlf">
                创建时间
            </td>
            <td class="mvcth-w50 mvc-borlf">
                预览
            </td>
            <td class="mvcth-w50 mvc-borlf">
                编辑
            </td>
            <td class="mvcth-w50">
                删除
            </td>
        </tr>
        <%foreach (试题外部信息 model in Model)
          {
        %>
        <tr>
            <td class="mvctb-lp10" title="<%=LKExamText.网页标签Title(model.试题显示内容)%>">
                <%=LKExamText.试题列表(model.试题显示内容, 54)%>
            </td>
            <td>
                <%=model.题型名称 %>
            </td>
            <td>
                <%=model.难易度 %>
            </td>
            <td class="mvc-borlf">
                <%=model.创建时间.ToString("yyyy-MM-dd HH:mm")%>
            </td>
            <td class="mvc-borlf" title="预览试题">
                <a href="javascript:void(0)" onclick="ExaminerEmbedHandle.Subject.view('<%=model.ID %>');return false;">
                    预览</a>
            </td>
            <td class="mvc-borlf" title="编辑试题">
                <a href="javascript:void(0)" onclick="ExaminerEmbedHandle.Subject.edit('<%=model.ID %>');return false;">
                    编辑</a>
            </td>
            <td title="删除试题">
                <a href="javascript:void(0)" deltitle="<%=LKExamText.删除提示(model.试题显示内容) %>" onclick="ExaminerEmbedHandle.Subject.Delete.confirm('<%=model.ID %>',$(this).attr('deltitle'));return false;">
                    删除</a>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
<%=Html.MvcPagerAjaxPager(Model)%>
