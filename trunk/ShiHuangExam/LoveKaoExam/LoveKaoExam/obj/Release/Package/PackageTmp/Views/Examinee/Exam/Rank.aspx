<%@ Page Title="考试排行榜" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<考试排行Models>" %>

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Models.Examinee" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.CssJs_CUseBox() %>
    <%=Html.ScriptLink("/Library/UI/blockUI/jquery.blockUI.js")%>
    <%PagedList<考试排名> pagedList考试排名 = Model.PagedList考试排名; %>
    <%LKPageException lkPageException = Model.LKPageException; %>
    <%if (lkPageException.是否异常)
      { %>
    <script type="text/javascript">
        $(function () {
            Global.Boxy.alert("ExamineeEmbedHandle-Rank", {
                newTitle: "查看排行",
                dtHtml: "操作失败，请查看以下原因",
                newMessage: '<%=lkPageException.抛出异常数据%>'
            });
        });
    </script>
    <%}
      else
      { %>
    <%if (pagedList考试排名 != null && pagedList考试排名.Count != 0)
      {%>
    <div class="usebox examinee-exam-rank">
        <h1>
            <%=pagedList考试排名[0].考试设置.试卷内容.名称%></h1>
        <div class="detailsInfo">
            <label>
                时长:</label><span><font color="red"><%=pagedList考试排名[0].考试设置.考试时长%></font>分钟</span>
            <label>
                总分:</label><span><font color="red"><%=pagedList考试排名[0].考试设置.试卷内容.总分%></font>分</span>
            <label>
                参考人数:</label><span><font color="red"><%=pagedList考试排名[0].参考人数%></font>位</span>
            <label>
                本人位于:</label><span>第<font color="red"><%=pagedList考试排名[0].我的排名%></font>名</span>
        </div>
    </div>
    <%} %>
    <%=Html.MvcPagerAjaxSearch(LKExamEnvironment.考生编号 + "/姓名")%>
    <%Html.MvcPagerAjaxRenderPartial("~/Views/Examinee/Exam/UCRank.ascx");%>
    <%} %>
</asp:Content>
