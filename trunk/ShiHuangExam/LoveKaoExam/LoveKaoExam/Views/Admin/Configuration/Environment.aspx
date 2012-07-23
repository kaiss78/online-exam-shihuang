<%@ Page Title="环境配置" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.StyleLink("/Content/StyleSheet/UseBox.css")%>
    <%=Html.ScriptLink("/Scripts/Views/Admin/Configuration.js")%>
    <div class="usebox admin-configuration-environment">
        <ul class="editor">
            <li>
                <%=Html.LabelW90("请选择类型：")%>
                <%=Html.RadioButton("enType", "0", LKExamEnvironment.是否为学校, new { id = "EnSchool" })%>
                <%=Html.Label("学校", "EnSchool", new { style = LKExamEnvironment.是否为学校 ? "color:red;" : "" })%>
                <%=Html.RadioButton("enType", "1", !LKExamEnvironment.是否为学校, new { id = "EnEnterprise" })%>
                <%=Html.Label("单位", "EnEnterprise", new { style = LKExamEnvironment.是否为学校 ? "" : "color:red;" })%>
            </li>
            <li>
                <%=Html.Button("btn1", "更改环境配置", new { onclick = "AdminConfiguration.Environment.submit();" })%>
            </li>
            <li class="introduction">
                <dl>
                    <dt>在线考试系统为您提供两种环境，</dt>
                    <dd>
                        请根据您的需要选择吧！</dd>
                </dl>
            </li>
        </ul>
    </div>
    <%=Html.ScriptText("AdminConfiguration.Environment.initHtmlElem();")%>
</asp:Content>
