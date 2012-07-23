<%@ Page Title="修改资料" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<用户>" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%--string GetSex(byte sex)
{
    return sex==1?"男":"女";
}--%>
<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.StyleLink("/Content/StyleSheet/UseBox.css")%>
    <%=Html.ScriptLink("/Scripts/Views/Account.js")%>
    <div class="usebox details">
        <ul class="editor">
            <li class="onlyFontH30">
                <%=Html.LabelW90("学号：")%>
                <span><%=Model.编号 %></span>
            </li>
            <li class="onlyFontH30">
                <%=Html.LabelW90("姓名：")%>
                <span><%=Model.姓名 %></span>
            </li>
            <li class="onlyFontH30">
                <%=Html.LabelW90("性别：")%>
                <span><%=Model.性别名称%></span>
            </li>
            <%if (Model.角色 == 0)
              {%>
            <li class="onlyFontH30">
                <%=Html.LabelW90("部门：")%>
                <span><%=Model.部门.名称%></span>
            </li>
            <%} %>
            <li>
                <%=Html.LabelW90("手机：", "手机")%>
                <%=Html.TextBoxFor(model => model.邮箱, new { id = "邮箱", @class = "textbg textbase" })%>
            </li>
            <li>
                <%=Html.Button("btn1", "保存资料", new { id = "提交按钮", onclick = "Account.Details.submit();" })%>
            </li>
        </ul>
    </div>
    <% string postJson = Newtonsoft.Json.JsonConvert.SerializeObject(Model);%>
    <%=Html.ScriptText("Account.Details.initData(" + postJson + ");")%>
</asp:Content>
