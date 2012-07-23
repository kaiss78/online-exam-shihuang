<%@ Page Title="已组织练习" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.ScriptLink("/Library/UI/blockUI/jquery.blockUI.js")%>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/EmbedHandle.js")%>
    <%=Html.MvcPagerNormalSearch("练习名称")%>
    <%Html.MvcPagerAjaxRenderPartial("~/Views/Examiner/Organization/UCPractice.ascx");%>
</asp:Content>
