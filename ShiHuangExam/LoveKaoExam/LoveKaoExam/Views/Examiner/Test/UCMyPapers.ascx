<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<试卷外部信息>>" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<!--
(1)值0-代表全部试卷
(2)值1-代表我的新增试卷
(3)值2-代表我的草稿试卷
(4)值3-代表已上传的试卷
(5)值4-代表已下载的试卷
-->
<%int i下拉静态值项 = LKExamURLQueryKey.下拉静态值项(); %>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <td>
                试卷名称
            </td>
            <td class="mvcth-w50 mvc-borlf">
                总分
            </td>
            <td class="mvcth-w110 mvc-borlf">
                创建时间
            </td>
            <!-- 非草稿试卷 -->
            <%if (i下拉静态值项 != 2)
              { %>
            <td class="mvcth-w70 mvc-borlf">
                组织考试
            </td>
            <td class="mvcth-w70 mvc-borlf">
                组织练习
            </td>
            <%} %>
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
        <%foreach (试卷外部信息 model in Model)
          {              
        %>
        <tr>
            <td class="mvctb-lp10" title="<%=LKExamText.网页标签Title(model.名称)%>">
                <%=LKExamText.试卷列表(model.名称, 44)%>
            </td>
            <td class="mvc-borlf">
                <%=model.总分 %>
            </td>
            <td class="mvc-borlf">
                <%=model.创建时间.ToString("yyyy-MM-dd HH:mm")%>
            </td>
            <!-- 非草稿试卷 -->
            <%if (i下拉静态值项 != 2)
              { %>
            <%int i已组织考试次数 = model.已组织考试次数; %>
            <td class="mvc-borlf" title="组织考试(已设置<%=i已组织考试次数 %>次)">
                <a href="javascript:void(0)" class="examroom" onclick="ExaminerEmbedHandle.Organization.add('<%=model.试卷内容ID%>',1,<%=model.试卷状态Enum%>);return false;">
                    设置(<%=i已组织考试次数%>)</a>
            </td>
            <%int i已组织练习次数 = model.已组织练习次数; %>
            <td class="mvc-borlf" title="组织练习(已设置<%=i已组织练习次数 %>次)">
                <a href="javascript:void(0)" class="examroom" onclick="ExaminerEmbedHandle.Organization.add('<%=model.试卷内容ID%>',0,<%=model.试卷状态Enum%>);return false;">
                    设置(<%=i已组织练习次数%>)</a>
            </td>
            <%} %>
            <td class="mvc-borlf" title="预览试卷">
                <%=Html.ActionLink("预览","ViewTest","Test",new{guid=model.试卷内容ID},new{target="_blank"})%>
            </td>
            <td class="mvc-borlf" title="编辑试卷">
                <%=Html.ActionLink("编辑", "EditExam", "Test", new { guid = model.当前试卷内容.试卷外部信息ID }, new { target = "_blank" })%>
            </td>
            <td title="删除试卷">
                <a href="javascript:void(0)" deltitle="<%=LKExamText.删除提示(model.名称)%>" onclick="ExaminerEmbedHandle.Test.Delete.confirm('<%=model.ID %>',$(this).attr('deltitle'));return false;">
                    删除</a>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
<%=Html.MvcPagerAjaxPager(Model)%>
