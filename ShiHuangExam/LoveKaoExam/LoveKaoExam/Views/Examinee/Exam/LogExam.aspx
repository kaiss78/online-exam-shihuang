<%@ Page Title="考试记录" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.ScriptLink("/Scripts/Views/Examinee/EmbedHandle.js")%>
    <%=Html.MvcPagerNormalSearch(new LKPageMvcPager文本输入框("试卷名称"), new LKPageMvcPager下拉列表框(MvcPager下拉列表适用环境.静态值项, MvcPager下拉列表静态值项.考生考试记录))%>
    <%Html.RenderPartial("~/Views/Examinee/Exam/UCLogExam.ascx");%>
</asp:Content>
