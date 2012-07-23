<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<考试设置>>" %>
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
            <td class="mvcth-w60 mvc-borlf">
                考试状态
            </td>
            <td class="mvcth-w78 mvc-borlf">
                开始时间
            </td>
            <td class="mvcth-w78 mvc-borlf">
                发布时间
            </td>
            <td class="mvcth-w40 mvc-borlf">
                分析
            </td>
            <td class="mvcth-w40 mvc-borlf">
                报表
            </td>
            <td class="mvcth-w55 mvc-borlf">
                考试设置
            </td>
            <td class="mvcth-w40">
                删除
            </td>
        </tr>
        <%foreach (考试设置 model in Model)
          {
        %>
        <tr>
            <td class="mvctb-lp10" title="<%=LKExamText.网页标签Title(model.试卷外部信息.名称)%>">
                <%=LKExamText.试卷列表(model.试卷外部信息.名称,48)%>
            </td>
            <td class="mvc-borlf">
                <%=model.考试时长 %>
            </td>
            <td class="mvc-borlf" title="考试状态">
                <%=model.考试状态名称 %>
            </td>
            <td class="mvc-borlf" title="<%=model.考试开始时间 %>">
                <%=model.考试开始时间.ToString("MM-dd HH:mm") %>
            </td>
            <td class="mvc-borlf" title="<%=model.设置时间 %>">
                <%=model.设置时间.ToString("MM-dd HH:mm")%>
            </td>
            <td class="mvc-borlf" title="查看考试分析">
                <%=Html.ActionLink("分析", "ExamResult", "Analysis", new { TestID = model.ID, HandleType = 1 }, new { target = "_blank" })%>
            </td>
            <td class="mvc-borlf" title="查看考试报表">
                <%=Html.ActionLink("报表", "ExamReport", "Analysis", new { TestID = model.ID, HandleType = 1, ChartType = 0, ScoreSection = 10 }, new { target = "_blank" })%>
            </td>
            <td class="mvc-borlf" title="修改考试设置">
                <a href="javascript:void(0)" class="examroom" onclick="ExaminerEmbedHandle.Organization.edit('<%=model.ID%>',1);return false;">
                    修改</a>
            </td>
            <td title="删除该考试设置">
                <a href="javascript:void(0)" deltitle="<%=LKExamText.删除提示(model.试卷外部信息.名称,22)%>"
                    onclick="ExaminerEmbedHandle.Organization.Delete.confirm('<%=model.ID %>',1,$(this).attr('deltitle'));return false;">
                    删除</a>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
<%=Html.MvcPagerAjaxPager(Model)%>
