<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<考试设置>" %>
<%@ Import Namespace="LoveKaoExam.Data" %>

<%if (Model != null)
  {%>
<div class="usebox examiner-analysis-head">
    <h1>
        <%=Model.试卷内容.名称%></h1>
    <div class="detailsInfo">
        <label>
            考试时长:</label><span><font color="red"><%=Model.考试时长%></font>分钟</span>
        <label>
            总分:</label><span><font color="red"><%=Model.试卷内容.总分%></font>分</span>
    </div>
</div>
<%} %>