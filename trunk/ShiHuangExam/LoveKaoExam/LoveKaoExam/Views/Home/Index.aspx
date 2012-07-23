<%@ Page Title="用户中心" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        dt{font-size: 14px;font-weight: bold;}
        dd{padding: 5px 0px;padding-left: 15px;}
        span{color: #f50;}
    </style>
    <dl style="background-image:url('bk.jpg'); background-repeat:repeat; height:315px">
        <dt>
            <%="欢迎使用" + BasePage.LKExamName + BasePage.Version%></dt>
    </dl>
</asp:Content>
