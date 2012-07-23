﻿<%@ Page Title="下载试题/试卷" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.StyleLink("/Content/StyleSheet/UseBox.css")%>
    <%=Html.ScriptLink("/Library/UI/blockUI/jquery.blockUI.js")%>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/EmbedHandle.js")%>
    <div class="usebox examiner-share-download" id="Examiner_Share_Download">
        <%Html.RenderPartial("~/Views/Examiner/Share/Download.ascx");%>
    </div>
</asp:Content>
