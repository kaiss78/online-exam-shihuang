<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ExamResultModels>" %>

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Models.Examiner" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试结果详情<%=BasePage.PageTitle %></title>
    <%=Html.CssJsBasic() %>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/Analysis.js")%>
</head>
<body>
    <%考试设置 c考试设置 = Model.考试设置; %>
    <%PagedList<考生做过的试卷> pL考生做过的试卷 = Model.考生做过的试卷; %>
    <%=Html.ExamReport标题(c考试设置)%>
    <div id="Master_Main_Content">
        <%Guid g试卷ID = LKExamURLQueryKey.试卷ID(); %>
        <div id="MvcPagerSearch">
            <form action="/Analysis/ExamResult/1" method="get">
            <input type="hidden" name="TestID" value="<%=g试卷ID %>" />
            <input type="hidden" name="HandleType" value="1" />
            <%=Html.DropDownList(new LKPageMvcPager下拉列表框(MvcPager下拉列表适用环境.考试有关部门, g试卷ID))%>
            <%=Html.KeyWords(LKExamEnvironment.考生编号 + "/姓名")%>
            <%=Html.Submit() %>
            </form>
            <form action="/Analysis/ExamResult/">
            <input type="hidden" name="TestID" value="<%=g试卷ID %>" />
            <input type="hidden" name="HandleType" value="2" />
            <input type="hidden" name="DepartmentID" value="<%=LKExamURLQueryKey.部门ID() %>" />
            <%string s是否公布考试结果 = c考试设置.是否公布考试结果 ? "关闭考试结果" : "公布考试结果"; %>
            <%=Html.Button("btn7-input any-public", s是否公布考试结果, new { onclick = "ExaminerAnalysis.ExamResult.submit();", id = "提交按钮_是否公布考试结果" })%>
            <%=Html.Button("btn7-input any-report", "查看试卷报表", new { onclick = "ExaminerAnalysis.Redirect.toExamReport();" })%>
            <%if (pL考生做过的试卷.Count != 0)
              {%>
            <%=Html.Submit("btn7-input any-excel", "导出Excel")%>
            <%  } %>
            </form>
        </div>
        <%Html.MvcPagerAjaxRenderPartial("~/Views/Examiner/Analysis/UCExamResult.ascx");%>
    </div>
    <%=Html.ScriptText("ExaminerAnalysis.ExamResult.initData(\'" + c考试设置.ID + "\'," + (c考试设置.是否公布考试结果 ? 1 : 0) + ");")%>
</body>
</html>
