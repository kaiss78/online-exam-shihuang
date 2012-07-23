<%@ Page Title="我要出题" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
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

    <style type="text/css">
    .E_S_L_Masks{top:auto;left:auto;height:60px;line-height:62px;}
    </style>
    <div style="padding: 10px 0px 0px 0px;overflow-x:auto">
        <%Html.RenderPartial("~/Views/Examiner/Subject/UCEditor.ascx"); %>
    </div>
</asp:Content>
