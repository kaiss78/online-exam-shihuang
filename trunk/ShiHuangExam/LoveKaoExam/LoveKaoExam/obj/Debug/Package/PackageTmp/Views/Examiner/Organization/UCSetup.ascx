<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<考生>>" %>

<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>

<%试卷设置 view试卷设置 = (试卷设置)ViewData["试卷设置"]; %>
<%List<Guid> 考生ID集合 = view试卷设置.考生ID集合; %>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <td class="mvcth-w40">
            </td>
            <td class="mvcth-w110 mvc-borlf">
                <%=LKExamEnvironment.考生编号 %>
            </td>
            <td class="mvcth-w120 mvc-borlf">
                姓名
            </td>
            <td class="mvcth-w80 mvc-borlf">
                性别
            </td>
            <td>
                <%=LKExamEnvironment.部门名称 %>
            </td>
        </tr>
        <%
            bool isall = true;
            foreach (考生 model in Model)
            {
                bool isContains = 考生ID集合.Contains(model.ID);
                if (isContains == false) isall = false;
        %>
        <tr type="MvcTB" class="<%=isContains?"selected":"" %>">
            <label for="<%=model.ID %>">
                <td class="mvctb-lp10">
                    <%string strClick = "";
                      strClick += "Global.MvcPager.CheckBox.radioSelect(this);";
                      strClick += "ExaminerOrganization.Setup.CheckBox.radioSelect(this);";
                    %>
                    <%=Html.CheckBox(model.ID.ToString(), isContains, new { id = model.ID, onclick = strClick })%>
                </td>
                <td class="mvc-borlf" title="<%=model.编号 %>">
                    <%=model.编号%>
                </td>
                <td class="mvc-borlf" title="<%=model.姓名 %>">
                    <%=model.姓名 %>
                </td>
                <td class="mvc-borlf">
                    <%=model.性别 %>
                </td>
                <td>
                    <%=model.部门.名称 %>
                </td>
            </label>
        </tr>
        <%} %>
        <%if (Model.Count != 0)
          { %>
        <tr class="mvctf">
            <td colspan="5">
                <%string strClick = "";
                  strClick += "Global.MvcPager.CheckBox.allSelect(this.checked);";
                  strClick += "ExaminerOrganization.Setup.CheckBox.allSelect(this.checked);"; %>
                <%=Html.CheckBox("全选", isall, new { id = "全选", title = "全选", @class = "allsel", onclick = strClick })%>
                <label for="全选" title="全选">
                    全选</label>
                <span style="color: #666;">
                    <%=Html.试卷设置考生范围总数()%>
                </span>
            </td>
        </tr>
        <%} %>
    </tbody>
</table>
<%=Html.MvcPagerAjaxPager(Model)%>
<script type="text/javascript">
    $(function () {
        ExaminerOrganization.Setup.CheckBox.defaultSelect();
    });
</script>
