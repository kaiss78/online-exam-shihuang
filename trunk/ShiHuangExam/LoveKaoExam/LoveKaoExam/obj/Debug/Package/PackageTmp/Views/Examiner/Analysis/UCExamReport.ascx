<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ExamReportModels>" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Models.Examiner" %>
<%@ Import Namespace="LoveKaoExam.Data" %>

<%bool b是否存在考试分析 = Model.考试分析 != null && Model.考试分析.Count != 0; %>
<div class="usebox examiner-analysis-examReport" style="<%=b是否存在考试分析?"":"width:auto;"%>">
    <%=Html.ExamReport图形HTML版(Model)%>
    <%=Html.ExamReport表格(Model.考试分析)%>
    <%=Html.ExamReport情况(Model.DataTable)%>
    <%=Html.ExamReport管理(Model.考试分析)%>
</div>
