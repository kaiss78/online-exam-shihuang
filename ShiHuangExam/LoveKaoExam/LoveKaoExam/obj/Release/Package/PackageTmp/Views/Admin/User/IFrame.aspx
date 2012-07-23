<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑账户<%=BasePage.PageTitle %></title>
    <%=Html.CssJsBasic() %>
</head>
<body>
    <div style="margin-top: 20px;">
        <%Html.RenderAction("UCEditor", Request["id"]);%>
    </div>
</body>
</html>
