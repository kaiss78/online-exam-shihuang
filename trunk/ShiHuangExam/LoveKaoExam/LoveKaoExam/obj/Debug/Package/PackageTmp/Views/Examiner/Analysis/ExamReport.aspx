<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ExamReportModels>" %>

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Models.Examiner" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试卷报表<%=BasePage.PageTitle %></title>
    <%=Html.CssJsBasic() %>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/Analysis.js")%>
</head>
<body>
    <%考试设置 c考试设置 = Model.考试设置; %>
    <%=Html.ExamReport标题(c考试设置)%>
    <div id="Master_Main_Content">
        <%Guid g试卷ID = LKExamURLQueryKey.试卷ID(); %>
        <div id="MvcPagerSearch" style="border-bottom: #abc0db 1px solid;">
            <form action="/Analysis/ExamReport/1" method="get">
            <input type="hidden" name="TestID" value="<%=g试卷ID %>" />
            <input type="hidden" name="HandleType" value="1" />
            <%=Html.DropDownList(new LKPageMvcPager下拉列表框(MvcPager下拉列表适用环境.考试有关部门, g试卷ID))%>
            <label for="ChartType">
                图形：</label>
            <%=Html.ExamReportSelect图形类型()%>
            <label for="ScoreSection">
                分数段：</label>
            <%=Html.ExamReportSelect分数段()%>
            <%=Html.Submit("btn1","应用") %>
            </form>
            <form action="/Analysis/ExamReport">
            <input type="hidden" name="TestID" value="<%=g试卷ID %>" />
            <input type="hidden" name="HandleType" value="2" />
            <input type="hidden" name="DepartmentID" value="<%=LKExamURLQueryKey.部门ID() %>" />
            <input type="hidden" name="ChartType" value="<%=LKExamURLQueryKey.GetInt32("ChartType")%>" />
            <input type="hidden" name="ScoreSection" value="<%=LKExamURLQueryKey.GetInt32("ScoreSection")%>" />
            <%string s是否公布考试结果 = c考试设置.是否公布考试结果 ? "关闭考试结果" : "公布考试结果"; %>
            <%=Html.Button("btn7-input any-public", s是否公布考试结果, new { onclick = "ExaminerAnalysis.ExamResult.submit();", id = "提交按钮_是否公布考试结果" })%>
            <%=Html.Button("btn7-input any-report", "查看试卷分析", new { onclick = "ExaminerAnalysis.Redirect.toExamResult();" })%>
            <%if (Model.考试分析.Count != 0)
              {%>
            <%=Html.Submit("btn7-input any-excel", "导出Word")%>
            <%  } %>
            </form>
        </div>
        <%Html.MvcPagerAjaxRenderPartial("~/Views/Examiner/Analysis/UCExamReport.ascx");%>
    </div>
    <%=Html.ScriptText("ExaminerAnalysis.ExamResult.initData(\'" + c考试设置.ID + "\'," + (c考试设置.是否公布考试结果 ? 1 : 0) + ");")%>
</body>
</html>
