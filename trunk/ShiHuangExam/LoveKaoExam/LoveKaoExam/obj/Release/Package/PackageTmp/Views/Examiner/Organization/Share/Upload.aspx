<%@ Page Title="上传试题/试卷" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.StyleLink("/Content/StyleSheet/UseBox.css")%>
    <%=Html.ScriptLink("/Library/UI/blockUI/jquery.blockUI.js")%>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/EmbedHandle.js")%>
    <div class="usebox examiner-share-upload" id="Examiner_Share_Upload">
       <%Html.RenderPartial("~/Views/Examiner/Share/Upload.ascx");%>
    </div>
</asp:Content>
