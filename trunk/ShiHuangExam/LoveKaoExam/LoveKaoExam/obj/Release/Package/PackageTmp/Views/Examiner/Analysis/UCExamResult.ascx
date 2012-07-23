<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ExamResultModels>" %>

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Models.Examiner" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>

<%PagedList<考生做过的试卷> pL考生做过的试卷 = Model.考生做过的试卷; %>
<%bool b班级名次 = pL考生做过的试卷.Count > 0 && LKExamURLQueryKey.包含部门ID(); %>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <%=Html.MvcHtmlTag_RetainOrReplace(b班级名次, TagName.TD, "班级名次", new { @class = "mvcth-w50" })%>
            <td class="mvcth-w40">
                总名次
            </td>
            <td class="mvcth-w70 mvc-borlf">
                <%=LKExamEnvironment.考生编号 %>
            </td>
            <td class="mvcth-w78 mvc-borlf">
                姓名
            </td>
            <td class="mvcth-w90 mvc-borlf">
                <%=LKExamEnvironment.部门名称 %>
            </td>
            <td class="mvcth-w78 mvc-borlf">
                主观题得分
            </td>
            <td class="mvcth-w78 mvc-borlf">
                客观题得分
            </td>
            <td class="mvcth-w78 mvc-borlf">
                总得分
            </td>
            <td class="mvcth-w78 mvc-borlf">
                查看答卷
            </td>
            <td class="mvcth-w100 mvc-borlf">
                批改试卷
            </td>
            <td class="mvcth-w100 mvc-borlf">
                批改状态
            </td>
            <td class="mvcth-w120 mvc-borlf">
                批改时间
            </td>
        </tr>
        <%foreach (考生做过的试卷 model in pL考生做过的试卷)
          {
        %>
        <tr>
            <%=Html.MvcHtmlTag_RetainOrReplace(b班级名次, TagName.TD, model.班级名次.ToString())%>
            <td class="mvc-borlf" title="总名次排第<%=model.总名次%>名">
                <%=model.总名次%>
            </td>
            <td class="mvc-borlf" title="<%=model.考生.编号 %>">
                <%=model.考生.编号 %>
            </td>
            <td class="mvc-borlf" title="<%=model.考生.姓名 %>">
                <%=model.考生.姓名 %>
            </td>
            <td class="mvc-borlf" title="<%=model.考生.部门.名称 %>">
                <%=model.考生.部门.名称 %>
            </td>
            <td class="mvc-borlf" title="主观题总得分：<%=model.主观题总得分 %>分">
                <%=model.主观题总得分 %>
            </td>
            <td class="mvc-borlf" title="客观题总得分：<%=model.客观题总得分 %>分">
                <%=model.客观题总得分 %>
            </td>
            <td class="mvc-borlf" title="总得分：<%=model.总得分 %>分">
                <%=model.总得分 %>
            </td>
            <td class="mvc-borlf">
                <%=Html.ActionLink("查看答卷", "Mark", "Test", new { guid = model.ID, type = 2 }, new { title = "查看" + model.考生.编号 + "的答卷", target = "_blank" })%>
            </td>
            <td class="mvc-borlf">
                <%
                    string s批改名称;
                    switch (model.批改状态类型)
                    {
                        case 1:
                            s批改名称 = "继续批改";
                            break;
                        case 2:
                            s批改名称 = "重新批改";
                            break;
                        default:
                            s批改名称 = "批改试卷";
                            break;
                    }%>
                <%=Html.ActionLink(s批改名称, "Mark", "Test", new { guid = model.ID, type = 3 }, new { title = model.考生.编号 +"："+ s批改名称, target = "_blank" })%>
            </td>
            <td class="mvc-borlf" title="批改状态：<%=model.批改状态 %>">
                <%=model.批改状态 %>
            </td>
            <td class="mvc-borlf" title="批改时间：<%=LKPageRetainOrReplace.GetString(model.批改时间, "yyyy-MM-dd HH:mm","无")%>">
                <%=model.批改时间%>
                <%=LKPageRetainOrReplace.GetString(model.批改时间, "yyyy-MM-dd HH:mm","无")%>
            </td>
        </tr>
        <%
            } %>
    </tbody>
</table>
<%=Html.MvcPagerNormalPager(pL考生做过的试卷)%>