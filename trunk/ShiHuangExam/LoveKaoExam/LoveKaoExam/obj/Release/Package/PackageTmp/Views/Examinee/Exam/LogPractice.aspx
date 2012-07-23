<%@ Page Title="练习记录" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.MvcPagerNormalSearch(new LKPageMvcPager文本输入框("练习名称"), new LKPageMvcPager下拉列表框(MvcPager下拉列表适用环境.静态值项, MvcPager下拉列表静态值项.考生练习记录))%>
    <%Html.RenderPartial("~/Views/Examinee/Exam/UCLogPractice.ascx");%>
</asp:Content>