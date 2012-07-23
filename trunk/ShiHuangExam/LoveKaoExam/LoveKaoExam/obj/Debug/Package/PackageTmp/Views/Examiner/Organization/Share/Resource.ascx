<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<爱考网资源共享>" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Models" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>


<%PagedList<爱考网资源列表> 资源列表 = Model.爱考网资源列表; %>
<%bool b资源方式_上传 = Model.资源方式 == 爱考网资源方式.上传; %>
<%bool b资源方式_下载 = Model.资源方式 == 爱考网资源方式.下载; %>
<%bool b资源类型_试题 = Model.资源类型 == 爱考网资源类型.试题; %>
<%bool b资源类型_试卷 = Model.资源类型 == 爱考网资源类型.试卷; %>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <td class="mvcth-w40">
            </td>
            <td class="mvc-borlf">
                <%=Model.资源类型 + (b资源类型_试题 ? "题干" : "名称")%>
            </td>
            <td class="mvcth-w120 mvc-borlf over-hidden">
                所属分类
            </td>
            <%if (b资源方式_下载)
              {%><td class="mvcth-w100 mvc-borlf over-hidden">
                  创建者
              </td>
            <%} %>
            <td class="mvcth-w90 mvc-borlf">
                发布时间
            </td>
            <td class="mvcth-w60 mvc-borlf">
                已<%=Model.资源方式 %>
            </td>
            <td class="mvcth-w70">
                预览<%=Model.资源类型%>
            </td>
        </tr>
        <%bool isall = true;
          foreach (爱考网资源列表 model in 资源列表)
          {
              bool isContains = Model.爱考网ID列表.Contains(model.ID);
              if (isContains == false) isall = false;
        %>
        <tr type="MvcTB" class="<%=isContains?"disabled":"" %>">
            <label for="<%=model.ID %>">
                <td class="mvctb-lp10">
                    <%if (isContains)
                      { %><%=Html.CheckBox(model.ID.ToString(), true, new { disabled = "disabled", id = model.ID, title = "该" + Model.资源类型 + "已" + Model.资源方式 })%><%}
                      else
                      { %><%string strClick = "";
                            strClick += "Global.MvcPager.CheckBox.radioSelect(this);";
                            strClick += "ExaminerShare.Resource.CheckBox.radioSelect(this);"; %>
                    <%=Html.CheckBox(model.ID.ToString(), false, new { testInAllSubjectNum = model.试卷中所有试题总数, testInExistSubjectNum = model.试卷中已有试题总数, id = model.ID, onclick = strClick })%>
                    <%} %>
                </td>
                <td class="mvc-borlf" style="text-align: left;" title="<%=LKExamText.网页标签Title(model.标题名称)%>">
                    <%=LKExamText.爱考网资源共享列表(model.标题名称, Model.资源方式, Model.资源类型)%>
                </td>
                <td class="mvc-borlf over-hidden" style="word-break:keep-all;">
                    <%=Html.资源共享URL重写分类_详细(model.分类列表, Model.资源方式)%>
                </td>
                <%if (b资源方式_下载)
                  {%>
                <td class="mvc-borlf over-hidden">
                    <%=Html.爱考网URL重写会员_详细(model.创建人ID, model.创建人昵称)%>
                </td>
                <%} %>
                <td class="mvc-borlf" title="<%=model.创建时间 %>">
                    <%=model.创建时间.ToString("yyyy-MM-dd")%>
                </td>
                <td class="mvc-borlf">
                    <%=Model.爱考网ID列表.Contains(model.ID) ? "是" : "否"%>
                </td>
                <td title="预览<%=Model.资源类型%>">
                    <%=Html.资源共享URL重写试卷(model.ID, model.ContentID, Model.资源方式, Model.资源类型)%>
                </td>
            </label>
        </tr>
        <%} %>
        <%if (资源列表.Count != 0)
          { %>
        <tr class="mvctf">
            <td class="mvc-borlf" colspan="<%=Model.资源方式 ==爱考网资源方式.下载?7:6 %>">
                <%if (isall)
                  { %>
                <%=Html.CheckBox("全选", true, new { id = "全选", title = "全选", @class = "allsel", disabled = "disabled" })%>
                <%}
                  else
                  { %>
                <%string strClick = "";
                  strClick += "Global.MvcPager.CheckBox.allSelect(this.checked);";
                  strClick += "ExaminerShare.Resource.CheckBox.allSelect(this.checked);"; %>
                <%=Html.CheckBox("全选", false, new { id = "全选", title = "全选", @class = "allsel", onclick = strClick })%>
                <%} %>
                <label for="全选" title="全选">
                    全选</label>
                <span style="color: #666;">
                    <%=Html.爱考网资源共享上传下载信息(Model.资源上传下载信息, Model.资源方式, Model.资源类型)%>
                </span>
            </td>
        </tr>
        <%} %>
    </tbody>
</table>
<%=Html.MvcPagerAjaxPager(资源列表)%>
<%if (资源列表.Count != 0)
  {%>
<div style="margin-top: 20px; padding-left: 40px;">
    <%=Html.Button("btn1", Model.资源方式.ToString() + Model.资源类型.ToString(), new { id = "提交按钮", onclick = "ExaminerShare.Resource.submit();" })%>
</div>
<%} %>
<script type="text/javascript">
    $(function () {
        ExaminerShare.Resource.CheckBox.defaultSelect();
    });
</script>
