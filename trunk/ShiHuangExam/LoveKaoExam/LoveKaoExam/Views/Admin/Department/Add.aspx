<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%Html.RenderAction("UCEditor");%>
</asp:Content>
