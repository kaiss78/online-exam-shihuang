<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<爱考网资源共享>" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Models" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查询上传下载试题试卷<%=BasePage.PageTitle %></title>
    <%=Html.CssJsBasic()%>
    <%=Html.ScriptLink("/Library/UI/blockUI/jquery.blockUI.js")%>
    <%=Html.ScriptLink("/Library/Plugins/jquery.progressbar/js/jquery.progressbar.js")%>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/Share.js")%>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/EmbedHandle.js")%>
</head>
<body>
    <%爱考网服务器连接 c爱考网服务器连接 = Model.爱考网服务器连接; %>
    <%if (c爱考网服务器连接.连接是否成功)
      { %>
    <div id="Master_Main_Content">
        <%=Html.MvcPagerAjaxSearch(Model.资源类型 == 爱考网资源类型.试题 ? "试题题干" : "试卷名称")%>
        <%Html.MvcPagerAjaxRenderPartial("~/Views/Examiner/Share/Resource.ascx");%>
    </div>
    <%string 上传下载信息Json = Newtonsoft.Json.JsonConvert.SerializeObject(Model.资源上传下载信息);%>
    <%=Html.ScriptText("ExaminerShare.Resource.initData(\'" + Model.资源方式 + "\',\'" + Model.资源类型 + "\'," + 上传下载信息Json + ");")%>
    <%}
      else
      { %>
    <%=c爱考网服务器连接.连接失败消息%>
    <%} %>
</body>
</html>
