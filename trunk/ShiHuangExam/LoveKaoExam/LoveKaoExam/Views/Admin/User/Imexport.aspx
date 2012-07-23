<%@ Page Title="导入导出Excel" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<PagedList<考生>>" %>

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.CssJs_CUseBox() %>
    <%=Html.CssJs_CMvcPager() %>
    <%=Html.ScriptLink("/Library/Plugins/Form/jquery.form.js")%>
    <%=Html.ScriptLink("/Scripts/Views/Admin/User.js")%>
    <%string 考生编号 = LKExamEnvironment.考生编号;%>
    <%string 考生名称 = LKExamEnvironment.考生名称;%>
    <%bool 状态导入 = (LKExamURLQueryKey.GetString("handleType") == "" ? true : false); %>
    <div class="usebox admin-user-imexport">
        <ul class="editor">
            <li>
                <%=Html.LabelW90("请选择类型：")%>
                <%=Html.RadioButton("utype", "0", 状态导入 == true, new { id = "import", onclick = "AdminUser.ImExport.tabIdentity(0);Global.MsgBox.Hide.all();" })%>
                <%=Html.Label("导入", "import")%>
                <%=Html.RadioButton("utype", "1", 状态导入 == false, new { id = "export", onclick = "AdminUser.ImExport.tabIdentity(1);Global.MsgBox.Hide.all();" })%>
                <%=Html.Label("导出", "export")%>
            </li>
        </ul>
        <div class="<%=状态导入?"":"hidden" %>" id="Admin_User_Import">
            <ul class="editor">
                <li>
                    <form id="FormUpload" method="post" onsubmit="AdminUser.ImExport.submit();return false;" action="/User/ImportExcel/" enctype="multipart/form-data">
                    <%=Html.LabelW90("请选择：", "unumber")%>
                    <input type="file" name="InputFile" id="导入考生XLSPath" class="textbg fileexcel" mbleft="410" mbtop="0" />
                    <%=Html.Submit("btn7-input", "导入" + 考生名称 + "信息", new { id = "提交按钮" })%>
                    </form>
                </li>
                <li class="introduction">
                    <dl>
                        <dt>导入excel格式必须和系统提供的excel模板相同</dt>
                        <dd>
                            点击下载：<a href="/Content/File/Excel/<%=考生名称 %>模板.xls"><%=考生名称 %>Excel模板</a></dd>
                    </dl>
                </li>
            </ul>
        </div>
        <div class="<%=状态导入?"hidden":"" %>" id="Admin_User_Export">
            <div id="MvcPagerSearch">
                <form action="/User/Imexport/1" method="get">
                <input type="hidden" name="handleType" value="2" />
                <%=Html.DropDownList(new LKPageMvcPager下拉列表框())%>
                <%=Html.Submit() %>
                </form>
                <%if (Model.Count != 0)
                  {%>
                <form action="/User/Imexport/">
                <input type="hidden" name="handleType" value="1" />
                <input type="hidden" name="DepartmentID" value="<%=LKExamURLQueryKey.部门ID() %>" />
                <%=Html.Submit("btn7-input", "导出Excel")%>
                </form>
                <%} %>
            </div>
            <%ViewData["引用页面"] = "导入导出考生Excel"; %>
            <%Html.MvcPagerAjaxRenderPartial("~/Views/Admin/User/UCExaminee.ascx");%>
        </div>
    </div>
    <%=Html.ScriptText("AdminUser.ImExport.initData();")%>
</asp:Content>
