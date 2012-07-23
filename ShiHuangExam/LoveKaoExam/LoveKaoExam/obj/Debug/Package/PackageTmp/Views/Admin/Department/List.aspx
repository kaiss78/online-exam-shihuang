<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.ScriptLink("/Library/UI/blockUI/jquery.blockUI.js")%>
    <%=Html.ScriptLink("/Scripts/Views/Admin/EmbedHandle.js")%>
    <%=Html.MvcPagerNormalSearch(LKExamEnvironment.部门名称 + "名称")%>
    <%Html.MvcPagerAjaxRenderPartial("~/Views/Admin/Department/UCList.ascx");%>
</asp:Content>
