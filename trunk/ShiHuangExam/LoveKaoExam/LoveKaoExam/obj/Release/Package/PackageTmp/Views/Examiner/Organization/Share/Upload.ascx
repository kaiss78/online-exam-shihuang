<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<资源上传下载Models>" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Models" %>
<%@ Import Namespace="LoveKaoExam.Models.Examiner" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%爱考网服务器连接 c爱考网服务器连接 = Model.爱考网服务器连接; %>
<%上传下载信息 c上传下载信息 = Model.上传下载信息; %>
<%if (c爱考网服务器连接.连接是否成功)
  { %>
<ul class="handleInfo">
    <li>
        <h2>
            您已上传试题数量<b><font color="green"><%=c上传下载信息.已上传试题数量%></font></b>道<%if (c上传下载信息.已上传试卷数量 != 0)
                                                                            {%>(包括<b><font color="green"><%=c上传下载信息.已上传试卷数量%></font></b>份试卷中的试题)<% }%>，想要获取更多的试题？</h2>
    </li>
    <li class="itemIcon">我要上传试题到主站爱考网(lovekao.com)
        <%=Html.Button("btn7-input", "上传试题", new { onclick = "ExaminerEmbedHandle.Share.upload(0,\'" + c上传下载信息.是否绑定账号 + "\'," + Model.爱考网账号类型 + ");return false;" })%>
        (每上传<b><font color="green">1</font></b>道试题可下载<b><font color="green">10</font></b>道试题,不包括跟爱考网相似的试题)
    </li>
    <li class="itemIcon">我要上传试卷到主站爱考网(lovekao.com)
        <%=Html.Button("btn7-input", "上传试卷", new { onclick = "ExaminerEmbedHandle.Share.upload(1,\'" + c上传下载信息.是否绑定账号 + "\'," + Model.爱考网账号类型 + ");return false;" })%>
    </li>
    <li class="itemIcon">直接到主站爱考网(lovekao.com)出试题,试卷。请点击 <a target="_blank" style="text-decoration: underline;"
        href="http://www.lovekao.com/All-User/EditSubject.aspx">爱考网(lovekao.com)</a>
    </li>
    <li class="itemIcon">可下载试题数量：<b><font color="green"><%=c上传下载信息.可下载试题数量%></font></b>道
        <%=Html.ActionLink("我要下载试题/试卷", "Download", "Share")%>
    </li>
</ul>
<%}
  else
  { %>
<%=c爱考网服务器连接.连接失败消息%>
<%} %>