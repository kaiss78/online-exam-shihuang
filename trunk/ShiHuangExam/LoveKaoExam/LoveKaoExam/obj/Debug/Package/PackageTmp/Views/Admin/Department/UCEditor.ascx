<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<部门>" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>

<%=Html.StyleLink("/Content/StyleSheet/UseBox.css")%>
<%=Html.ScriptLink("/Scripts/Views/Admin/Department.js")%>
<div class="usebox admin-add-department">
    <ul class="editor">
        <li>
            <%=Html.LabelW90(LKExamEnvironment.部门名称 + "名称：", "名称")%>
            <%=Html.TextBoxFor(model => model.名称, new { id = "名称", @class = "textbg textbase", autocomplete = "off" })%>
        </li>
        <li>
            <%=Html.Button("btn1", (Model.ID == Guid.Empty ? "创建" : "修改") + LKExamEnvironment.部门名称, new { id = "提交按钮", onclick = "AdminiDepartment.EditSame.submit();" })%>
        </li>
    </ul>
</div>
<%=Html.ScriptText("AdminiDepartment.EditSame.initData(\'" + Model.ID + "\',\'" + Model.名称 + "\');")%>
