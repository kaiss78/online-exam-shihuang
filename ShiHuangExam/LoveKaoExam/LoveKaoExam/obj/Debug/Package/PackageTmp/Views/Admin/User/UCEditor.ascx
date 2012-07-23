<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<用户>" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>

<%=Html.StyleLink("/Content/StyleSheet/UseBox.css")%>
<%=Html.ScriptLink("/Scripts/Views/Admin/User.js")%>
<%=Html.ScriptLink("/Scripts/Views/Admin/EmbedHandle.js")%>
<div class="usebox admin-adduser">
    <ul class="editor">
        <li>
            <%=Html.LabelW90("用户类型：")%>
            <%if (Model.ID == Guid.Empty)
              {%>
            <%=Html.RadioButtonFor(model => model.角色, 0, new { id = "角色考生", onclick = "AdminUser.EditSame.tabIdentity(0);Global.MsgBox.Hide.all();" })%>
            <%=Html.Label(LKExamEnvironment.考生名称, "角色考生")%>
            <%=Html.RadioButtonFor(model => model.角色, 1, new { id = "角色考官", onclick = "AdminUser.EditSame.tabIdentity(1);Global.MsgBox.Hide.all();" })%>
            <%=Html.Label(LKExamEnvironment.考官名称, "角色考官")%>
            <%}
              else
              { %>
            <%=LKExamEnvironment.角色名称(Model.角色)%>
            <%} %>
        </li>
        <li>
            <%=Html.LabelW90(LKExamEnvironment.考生编号 + "：", "编号", new { id = "编号考生", style = (Model.角色 != 0 ? "display:none;" : "") })%>
            <%=Html.LabelW90(LKExamEnvironment.考官编号 + "：", "编号", new { id = "编号考官", style = (Model.角色 != 1 ? "display:none;" : "") })%>
            <%=Html.TextBoxFor(model => model.编号, new { id = "编号", @class = "textbg textbase", autocomplete = "off", maxLength = "16" })%>
        </li>
        <li>
            <%=Html.LabelW90("姓名：", "姓名")%>
            <%=Html.TextBoxFor(model => model.姓名, new { id = "姓名", @class = "textbg textbase", autocomplete = "off", maxLength = "8" })%>
        </li>
        <li class="<%=(Model.角色 != 0 ? "hidden" : "") %>" id="Item_部门ID">
            <%string env部门名称 = LKExamEnvironment.部门名称;%>
            <%=Html.LabelW90("所属" + env部门名称 + "：", "部门ID")%>
            <%=Html.DropDownList("部门ID", (SelectList)null, "--请选择" + env部门名称 + "--", new { mbleft = "290", mbtop = "-5", id = "部门ID" })%>
            <a href="javascript:void(0)" onclick="AdminiEmbedHandle.Department.add();return false;">没有<%=LKExamEnvironment.部门名称 %>？点击添加</a>
        </li>
        <li class="<%=(Model.角色 != 1 ? "hidden" : "") %>" id="Item_邮箱">
            <%=Html.LabelW90("邮箱地址：", "邮箱")%>
            <%=Html.TextBoxFor(model => model.邮箱, new { id = "邮箱", @class = "textbg textbase", autocomplete = "off", maxLength = "32" })%>
            (可填) </li>
        <li>
            <%=Html.LabelW90("性别：")%>
            <%=Html.RadioButtonFor(model => model.性别, 1, new { id = "性别男" })%>
            <%=Html.Label("男", "性别男")%>
            <%=Html.RadioButtonFor(model => model.性别, 2, new { id = "性别女" })%>
            <%=Html.Label("女", "性别女")%>
        </li>
        <li style="color:Red">  <%=Html.Label("所创建用户的默认密码为123456。")%></li>
        <li>
            <%=Html.Button("btn1", (Model.ID == Guid.Empty ? "创建" : "修改") + "账户", new { id = "提交按钮", onclick = "AdminUser.EditSame.submit();" })%>
        </li>
        
    </ul>
</div>
<%=Html.ScriptText("AdminUser.EditSame.initData(" + Model.角色 + ",\'" + Model.ID + "\');")%>