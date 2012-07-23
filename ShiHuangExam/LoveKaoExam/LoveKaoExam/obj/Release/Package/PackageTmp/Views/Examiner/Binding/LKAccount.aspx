<%@ Page Title="绑定爱考网账号" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.StyleLink("/Content/StyleSheet/UseBox.css")%>
    <%=Html.ScriptLink("/Library/UI/blockUI/jquery.blockUI.js")%>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/EmbedHandle.js")%>
    <div class="usebox examiner-binding-lKAccount" id="Examiner_Binding_LKAccount">
        <%Html.RenderPartial("~/Views/Examiner/Binding/LKAccount.ascx"); %>
    </div>
</asp:Content>
