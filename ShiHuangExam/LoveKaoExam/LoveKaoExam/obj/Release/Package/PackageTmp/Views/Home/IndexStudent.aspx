<%@ Page Title="用户中心" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        dt{font-size: 14px;font-weight: bold;}
        dd{padding: 5px 0px;padding-left: 15px;}
        span{color: #f50;}
    </style>
    <dl>
        <dt>
            <%="欢迎使用" + BasePage.LKExamName + BasePage.Version%></dt>
        <dd>
            功能导航：</dd>
        <dd>
            我要考试：可以查看当前用户所有的考试，包括已考过和未考过的。
        </dd>
        <dd>
            我要练习：可以查看当前用户所有的可参与的练习，随时可以进入练习。</dd>
        <dd>
            考试记录：可以查看所有已参加的考试，查看试卷中可以查看参考答案，排名中显示本次考试的排名情况。</dd>
        <dd>
            练习记录：可以查看当前已参加的练习，可以查看练习答案以及再次进行练习。</dd>
        <dd>
            修改资料：可以修改自己的个人信息。</dd>
        <dd>
           修改密码：可以修改当前用户的密码，默认密码与账号相同。
<%--            <span>lovekaomaster@gmail.com.</span></dd>--%>
    </dl>
</asp:Content>
