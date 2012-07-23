<%@ Page Title="�޸�����" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<�û�>" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%--string GetSex(byte sex)
{
    return sex==1?"��":"Ů";
}--%>
<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.StyleLink("/Content/StyleSheet/UseBox.css")%>
    <%=Html.ScriptLink("/Scripts/Views/Account.js")%>
    <div class="usebox details">
        <ul class="editor">
            <li class="onlyFontH30">
                <%=Html.LabelW90("ѧ�ţ�")%>
                <span><%=Model.��� %></span>
            </li>
            <li class="onlyFontH30">
                <%=Html.LabelW90("������")%>
                <span><%=Model.���� %></span>
            </li>
            <li class="onlyFontH30">
                <%=Html.LabelW90("�Ա�")%>
                <span><%=Model.�Ա�����%></span>
            </li>
            <%if (Model.��ɫ == 0)
              {%>
            <li class="onlyFontH30">
                <%=Html.LabelW90("���ţ�")%>
                <span><%=Model.����.����%></span>
            </li>
            <%} %>
            <li>
                <%=Html.LabelW90("�ֻ���", "�ֻ�")%>
                <%=Html.TextBoxFor(model => model.����, new { id = "����", @class = "textbg textbase" })%>
            </li>
            <li>
                <%=Html.Button("btn1", "��������", new { id = "�ύ��ť", onclick = "Account.Details.submit();" })%>
            </li>
        </ul>
    </div>
    <% string postJson = Newtonsoft.Json.JsonConvert.SerializeObject(Model);%>
    <%=Html.ScriptText("Account.Details.initData(" + postJson + ");")%>
</asp:Content>
