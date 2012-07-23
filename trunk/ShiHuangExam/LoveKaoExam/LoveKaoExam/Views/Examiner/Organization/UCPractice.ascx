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
            <td class="mvcth-w78 mvc-borlf">
                练习时长
            </td>
            <td class="mvcth-w78 mvc-borlf">
                发布时间
            </td>
            <td class="mvcth-w70 mvc-borlf">
                预览练习
            </td>
            <td class="mvcth-w70 mvc-borlf">
                练习设置
            </td>
            <td class="mvcth-w55">
                删除
            </td>
        </tr>
        <%foreach (练习设置 model in Model)
          {
        %>
        <tr>
            <td class="mvctb-lp10" title="<%=LKExamText.网页标签Title(model.试卷外部信息.名称) %>">
                <%=model.试卷外部信息.名称%>
            </td>
            <td class="mvc-borlf">
                <%=model.考试时长 %>
            </td>
            <td class="mvc-borlf" title="<%=model.设置时间 %>">
                <%=model.设置时间.ToString("MM-dd HH:mm") %>
            </td>
            <td class="mvc-borlf" title="预览练习">
                <%=Html.ActionLink("预览", "ViewTest", "Test", new { guid = model.试卷内容ID }, new { target = "_blank" })%>
            </td>
            <td class="mvc-borlf" title="修改练习设置">
                <a href="javascript:void(0)" class="examroom" onclick="ExaminerEmbedHandle.Organization.edit('<%=model.试卷内容ID%>',0);return false;">
                    修改</a>
            </td>
            <td title="删除该练习设置">
                <a href="javascript:void(0)" deltitle="<%=LKExamText.删除提示(model.试卷外部信息.名称,22)%>"
                    onclick="ExaminerEmbedHandle.Organization.Delete.confirm('<%=model.试卷内容ID %>',0,$(this).attr('deltitle'));return false;">
                    删除</a>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
<%=Html.MvcPagerAjaxPager(Model)%>
