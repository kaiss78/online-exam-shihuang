<%@ Page Title="我的试卷" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.ScriptLink("/Library/UI/blockUI/jquery.blockUI.js")%>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/EmbedHandle.js")%>
    <%=Html.MvcPagerNormalSearch(new LKPageMvcPager文本输入框("试卷名称"), new LKPageMvcPager下拉列表框(MvcPager下拉列表适用环境.静态值项, MvcPager下拉列表静态值项.考官试卷))%>
    <%Html.MvcPagerAjaxRenderPartial("~/Views/Examiner/Test/UCMyPapers.ascx");%>
</asp:Content>
