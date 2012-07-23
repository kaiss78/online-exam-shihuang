<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<绑定账号信息Models>" %>
<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Models.Examiner" %>
<%@ Import Namespace="LoveKaoExam.Data" %>

<%绑定账号表 绑定账号表Model = Model.绑定账号表; %>
<%LKPageException lkPageException = Model.LKPageException;%>
<%bool _是否异常 = lkPageException.是否异常; %>
<%if (_是否异常 && lkPageException.返回已知值 == 3)
  { %>
<%=lkPageException.抛出异常数据%>
<%}else{ %>
<%if (绑定账号表Model == null)
  {%>
<ul class="handleInfo">
    <li>
        <h2>
            与主站爱考网(lovekao.com)绑定账号，为您提供题库资源共享。</h2>
    </li>
    <li class="itemIcon">您已拥有爱考网(lovekao.com)账号？请点击这里
        <%=Html.Button("btn7-input", "绑定已有账号", new { onclick = "ExaminerEmbedHandle.Binding.add(1);return false;" })%></li>
    <li class="itemIcon">您还没有爱考网(lovekao.com)账号，请点击这里
        <%=Html.Button("btn7-input", "绑定新账号", new { onclick = "ExaminerEmbedHandle.Binding.add(0);return false;" })%></li>
</ul>
<%}
  else
  { %>
<ul class="detailsInfo">
    <li>
        <h2>
            主站爱考网(lovekao.com)绑定账号信息</h2>
    </li>
    <li>
        <label>
            账号：</label><%=绑定账号表Model.爱考网账号 %><%=lkPageException.抛出异常数据%>
    </li>
    <li>
        <label>
            邮箱：</label><%=绑定账号表Model.爱考网邮箱 %></li>
    <li>
        <label>
            绑定日期：</label><%=绑定账号表Model.绑定时间.ToString("yyyy-MM-dd HH:mm:ss")%></li>
    <li class="itemBtn">
        <%=Html.Button("btn7-input", "更改账号", new { onclick="$('#LKAccountEdit').slideToggle('fast');"})%>
        <%=Html.Button("btn7-input", "解除绑定", new { onclick = "ExaminerEmbedHandle.Binding.Delete.confirm('" + 绑定账号表Model.爱考网账号 + "');return false;" })%></li>
</ul>
<ul class="handleInfo hidden" id="LKAccountEdit">
    <li>
        <h2>
            请选择更改类型</h2>
    </li>
    <li class="itemIcon">您已拥有爱考网(lovekao.com)账号？请点击这里
        <%=Html.Button("btn7-input", "更改绑定已有账号", new { onclick = "ExaminerEmbedHandle.Binding.edit(1);return false;" })%></li>
    <li class="itemIcon">您还没有爱考网(lovekao.com)账号？请点击这里
        <%=Html.Button("btn7-input", "更改绑定新账号", new { onclick = "ExaminerEmbedHandle.Binding.edit(0);return false;" })%></li>
</ul>
<%} %>
<%} %>