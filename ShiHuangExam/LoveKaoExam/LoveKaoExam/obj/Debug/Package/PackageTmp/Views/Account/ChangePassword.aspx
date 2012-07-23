<%@ Page Title="修改密码" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.StyleLink("/Content/StyleSheet/UseBox.css")%>
    <%=Html.ScriptLink("/Scripts/Views/Account.js")%>
    <div class="usebox changepassword">
        <ul class="editor">
            <li>
                <%=Html.LabelW90("原密码：", "OldPassword")%>
                <%=Html.Password("OldPassword","", new { @class = "textbg textbase" })%>
            </li>
            <li>
                <%=Html.LabelW90("新密码：", "NewPassword")%>
                <%=Html.Password("NewPassword", "", new { @class = "textbg textbase" })%>
            </li>
            <li>
                <%=Html.LabelW90("确认密码：", "ConfirmPassword")%>
                <%=Html.Password("ConfirmPassword", "", new { @class = "textbg textbase" })%>
            </li>
            <li>
                <%=Html.Button("btn1", "修改密码", new { id = "提交按钮", onclick = "Account.ChangePassword.submit();" })%>
            </li>
        </ul>
    </div>
    <%=Html.ScriptText("Account.ChangePassword.initData();")%>
</asp:Content>
