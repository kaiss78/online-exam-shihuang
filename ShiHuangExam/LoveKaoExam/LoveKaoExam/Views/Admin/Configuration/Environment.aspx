<%@ Page Title="��������" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.StyleLink("/Content/StyleSheet/UseBox.css")%>
    <%=Html.ScriptLink("/Scripts/Views/Admin/Configuration.js")%>
    <div class="usebox admin-configuration-environment">
        <ul class="editor">
            <li>
                <%=Html.LabelW90("��ѡ�����ͣ�")%>
                <%=Html.RadioButton("enType", "0", LKExamEnvironment.�Ƿ�ΪѧУ, new { id = "EnSchool" })%>
                <%=Html.Label("ѧУ", "EnSchool", new { style = LKExamEnvironment.�Ƿ�ΪѧУ ? "color:red;" : "" })%>
                <%=Html.RadioButton("enType", "1", !LKExamEnvironment.�Ƿ�ΪѧУ, new { id = "EnEnterprise" })%>
                <%=Html.Label("��λ", "EnEnterprise", new { style = LKExamEnvironment.�Ƿ�ΪѧУ ? "" : "color:red;" })%>
            </li>
            <li>
                <%=Html.Button("btn1", "���Ļ�������", new { onclick = "AdminConfiguration.Environment.submit();" })%>
            </li>
            <li class="introduction">
                <dl>
                    <dt>���߿���ϵͳΪ���ṩ���ֻ�����</dt>
                    <dd>
                        �����������Ҫѡ��ɣ�</dd>
                </dl>
            </li>
        </ul>
    </div>
    <%=Html.ScriptText("AdminConfiguration.Environment.initHtmlElem();")%>
</asp:Content>
