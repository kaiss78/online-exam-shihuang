<%@ Page Title="我要考试" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.MvcPagerNormalSearch(new LKPageMvcPager文本输入框("试卷名称"), new LKPageMvcPager下拉列表框(MvcPager下拉列表适用环境.静态值项, MvcPager下拉列表静态值项.考生我要考试))%>
    <%Html.RenderPartial("~/Views/Examinee/Exam/UCSelectExam.ascx");%>
</asp:Content>
