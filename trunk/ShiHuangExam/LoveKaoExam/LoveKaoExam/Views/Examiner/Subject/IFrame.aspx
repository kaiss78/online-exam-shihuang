<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑试题</title>
    <%=Html.CssJsBasic() %>
    <%=Html.StyleLink("/Content/LKAssembly/Subject/EditHtml.css")%>
    <%=Html.StyleLink("/Content/LKAssembly/Subject/EditSubject.css")%>
    <%=Html.StyleLink("/Library/Plugins/LKSort/StyleSheets/LKSort.css")%>
    <%=Html.StyleLink("/Library/Plugins/lk_tags/tags.css")%>
    <%=Html.StyleLink("/Content/StyleSheet/themes/redmond/jquery-ui-1.8.9.custom.css")%>
    <%=Html.ScriptLink("/Scripts/JQuery/jquery-ui-1.8.9.custom.js")%>
    <%=Html.ScriptLink("/Library/Plugins/jquery_position/jquery-position.js")%>
    <%=Html.ScriptLink("/Library/Plugins/jquery_selection/jquery.selection.js")%>
    <%=Html.ScriptLink("/Library/Plugins/Json2/json2.js")%>
    <%=Html.ScriptLink("/Library/Plugins/lk_tags/lk.tags.js")%>
    <%=Html.ScriptLink("/Library/Plugins/LKSort/JavaScript/LKSortZJSY_IFRAME.js")%>
    <%=Html.ScriptLink("/Scripts/Views/LKAssembly/Subject/EditSubjects.js")%>
</head>
<body>
    <div style="padding: 10px 0px 0px 40px; overflow:auto;height:500px; position:relative;">
        <%Html.RenderPartial("~/Views/Examiner/Subject/UCEditor.ascx"); %>
    </div>
</body>
</html>
