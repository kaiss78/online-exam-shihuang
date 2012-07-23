<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<考生做过的试卷>>" %>
<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<!--
(0)值0-代表全部考试
(1)值1-代表已完成考试
(2)值2-代表未完成考试
-->

<%int b已完成考试 = LKExamURLQueryKey.下拉静态值项(); %>
<%string s操作答卷 = LKPageRetainOrReplace.GetString(b已完成考试, "操 作", "查看答卷", "继续答卷");%>
<table id="MvcPagerDataList">
    <tbody>
        <tr class="mvcth">
            <td>
                试卷名称
            </td>
            <td class="mvcth-w60 mvc-borlf">
                考试时长
            </td>
            <td class="mvcth-w70 mvc-borlf">
                <%=LKPageRetainOrReplace.GetString(b已完成考试, "得分/总分", "得分/总分", "试卷总分")%>
            </td>
            <%=Html.MvcHtmlTag_RetainOrReplace(b已完成考试, TagName.TD,"是否完成", "是否及格", new { @class = "mvcth-w55 mvc-borlf" })%>
            <td class="mvcth-w120 mvc-borlf">
                <%=LKPageRetainOrReplace.GetString(b已完成考试, "开始答题时间","完成考试时间","开始答题时间")%>
            </td>
            <td class="mvcth-w70 mvc-borlf">
                <%=s操作答卷%>
            </td>
            <%=Html.MvcHtmlTag_RetainOrReplace(b已完成考试, TagName.TD, "查看排名", "查看排名", new { @class = "mvcth-w70 mvc-borlf" })%>
        </tr>
        <%foreach (考生做过的试卷 model in Model)
          {
        %>
        <tr>
            <td class="mvctb-lp10" title="<%=LKExamText.网页标签Title(model.考试设置.试卷内容.名称)%>">
                <%=LKExamText.试卷列表(model.考试设置.试卷内容.名称, 44)%>
            </td>
            <td class="mvc-borlf">
                <%=model.考试设置.考试时长 %>
            </td>
            <td class="mvc-borlf">
                <%=LKPageRetainOrReplace.GetString(true, model.总得分 + "/") + model.考试设置.试卷内容.总分%>
            </td>
            <%
              string 是否完成,是否继续,是否查看排名;
                  
                if (model.是否是已提交的试卷)
               {
                   是否完成 = "已完成";
                   是否继续 = "查看答卷";
                   是否查看排名 = "查看排名";
               }
               else
               {
                   是否完成 = "未完成";
                   是否继续 = "继续答卷";
                   是否查看排名 = "";
               } 
               
               %>
            <%=Html.MvcHtmlTag_RetainOrReplace(b已完成考试, TagName.TD,是否完成, model.及格情况, new { @class = "mvc-borlf" })%>
            <td class="mvc-borlf">
                <%string s答题结束时间 = LKPageRetainOrReplace.GetString(model.答题结束时间, "yyyy-MM-dd HH:mm"); %>
                <%string s答题开始时间 = model.答题开始时间.ToString("yyyy-MM-dd HH:mm"); %>
                <%=LKPageRetainOrReplace.GetString(b已完成考试, s答题开始时间,s答题结束时间, s答题开始时间)%>
            </td>
        
            <td class="mvc-borlf" title="<%=是否继续%>">
                <a href="javascript:void(0)" onclick="ExamineeEmbedHandle.LogExam.QTip.Answer.check('<%=model.是否公布考试结果%>','<%=model.考试是否结束 %>','<%=model.是否是已提交的试卷%>','<%=model.ID%>')">
                    <%=是否继续%></a>
            </td>
            <%if (true)
              { %>
            <td class="mvc-borlf" title="查看排名">
                <a href="javascript:void(0)" onclick="ExamineeEmbedHandle.LogExam.QTip.Rank.check('<%=model.是否公布考试结果%>','<%=model.考试是否结束 %>','<%=model.考试设置.ID %>');">
                    <%=是否查看排名 %></a>
            </td>
            <%} %>
          
        </tr>
        <%
            } %>
    </tbody>
</table>
<%=Html.MvcPagerNormalPager(Model)%>
