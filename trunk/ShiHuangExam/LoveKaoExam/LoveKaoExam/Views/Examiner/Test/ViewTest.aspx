<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<试卷内容>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
    <%= Model.总分%>- 石黄高速管理处</title>
    <%=Html.CssJsBasic() %>
    <%=Html.StyleLink("/Content/LKAssembly/Subject/ViewLibSubject.css")%>
    <%=Html.StyleLink("/Content/LKAssembly/Test/ViewTest.css")%>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/EmbedHandle.js")%>
    <style type="text/css">
        body
        {
            font-size: 14px;
        }
        .ViewTest_I_Header
        {
            text-align: center;
            color: black;
        }
        .ViewTest_I_H_Title
        {
            font-size: 30px;
            font-weight: bold;
        }
        .time
        {
            text-align: center;
            line-height: 30px;
            color: black;
        }
        .time span
        {
            margin: 0px 3px;
        }
        .time span label
        {
            color: #ee8c00;
        }
        .stViewHead table
        {
            color: #000;
        }
        .stViewOption span
        {
            color: #333;
        }
        a.chakanyuanti
        {
            color: #369;
            text-decoration: none;
            margin-left: 10px;
        }
        a.chakanyuanti:hover
        {
            color: #f50;
            text-decoration: underline;
        }
    </style>

    <script type="text/javascript">
        function showTestKey() {

            LK_LoginReg.show({
                是否提示未登录: true,
                预回调函数: function () {
                    window.location.href = '' + LKConfig.URLAssembly.Test.Page.get("ViewTest") + '<%=Request["guid"]%>' + LKConfig.URLAssembly.Test.Page.get("isKeyTestSuffix") + '';
                }
            });
        }
        function showOriginalTest() {
            window.location.href = '' + LKConfig.URLAssembly.Test.Page.get("ViewTest") + '<%=Request["guid"]%>' + LKConfig.URLAssembly.Test.Page.get("isAddTestSuffix") + '';
        }
    </script>

</head>
<body class="body1">
    <div class="ViewTestInteger" style="display: block;">
        <div class="ViewTest_I_Header">
            <div class="ViewTest_I_H_Title">
                <%= Model.名称%>
            </div>
            <div class="ViewTest_I_H_Count">
                <span>总分:
                    <samp id="TestCountID">
                        <%= Model.总分%></samp>分</span> <span>考试时间:<samp id="TestTimeID"></samp>分钟</span>
            </div>
            <div class="ViewTestBox " id="ViewTestButton">
                <% if (LKExamURLQueryKey.GetString("key") == "1")
                   { %>
                    <%=Html.Button("btn1", "原始卷", new { onclick = "showOriginalTest();" })%>
             
                <% }
                   else
                   { %>
              
               <%=Html.Button("btn1", "显示答案", new { onclick = "showTestKey();" })%>
                <% 
                  }
                %>
                <form action="/Test/ViewTest?word=1&guid=<%=LKExamURLQueryKey.GetGuid("guid")%>&key=<%=LKExamURLQueryKey.GetString("key")%>" method="post">
                    <%=Html.Submit("btn1","导出Word")%>
                </form>
                <%=Html.Button("btn1", "打印试卷", new {onclick="window.print();" })%>
               <%=Html.Button("btn1", "返回", new { onclick = "history.back(-1);" })%>
                
            </div>
        </div>
        <div class="ViewTest_Content" style="margin: 0 auto;">
            <div class="ViewTest_C_Header">
                <div class="ViewTest_C_H_Font">
                </div>
                <div class="ViewTest_C_H_Ico">
                </div>
            </div>
            <%string sContent = LoveKaoExam.Controllers.LKAssembly.LKTestController.GetTestViewHTML(Model, LKExamURLQueryKey.GetString("key"), false); %>
            <%=Html.MvcHtmlTag(TagName.DIV, sContent, new { @class = "ViewTest_C_SubContent" })%>
            <div class="ViewTest_C_Footer">
            </div>
                  <%-- <%using (Html.BeginForm("", "", new { word = 1, Guid = LKExamURLQueryKey.GetGuid("guid") }, FormMethod.Get)) {  %>
                <%} %>--%>
        </div>
    </div>
</body>
</html>
