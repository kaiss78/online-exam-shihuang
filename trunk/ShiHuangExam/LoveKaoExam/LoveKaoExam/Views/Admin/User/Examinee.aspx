<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.ScriptLink("/Library/UI/blockUI/jquery.blockUI.js")%>
    <%=Html.ScriptLink("/Scripts/Views/Admin/EmbedHandle.js")%>
    <%=Html.MvcPagerNormalSearch(new LKPageMvcPager文本输入框(LKExamEnvironment.考生编号 + "/姓名"), new LKPageMvcPager下拉列表框())%>
    <%Html.MvcPagerAjaxRenderPartial("~/Views/Admin/User/UCExaminee.ascx");%>
</asp:Content>
