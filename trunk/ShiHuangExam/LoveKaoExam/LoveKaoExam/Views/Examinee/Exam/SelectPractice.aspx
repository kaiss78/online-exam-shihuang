<%@ Page Title="我要练习" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.MvcPagerNormalSearch("练习名称")%>
    <%Html.RenderPartial("~/Views/Examinee/Exam/UCSelectPractice.ascx");%>
</asp:Content>
