<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%=Html.StyleLink("/Content/StyleSheet/Basic.css")%>
    <%=Html.StyleLink("/Library/Plugins/Boxy-0.1.4/StyleSheets/boxy.css")%>
    <%=Html.StyleLink("/Library/Plugins/MsgBox/StyleSheets/msgBox.css")%>
    <%=Html.StyleLink("/Content/LKAssembly/Subject/ViewLibSubject.css")%>
    <%=Html.StyleLink("/Content/LKAssembly/Test/EditExam.css")%>
    <%=Html.ScriptLink("/Scripts/JQuery/jquery-1.4.2.js")%>
    <%=Html.ScriptLink("/Scripts/Base/GlobalManage.js")%>
    <%=Html.ScriptLink("/Scripts/Base/LKConfig.js")%>
    <%=Html.ScriptLink("/Scripts/Base/Global.js")%>
    <%=Html.ScriptLink("/Library/Plugins/Boxy-0.1.4/JavaScripts/jQuery.boxy.js")%>
    <%=Html.ScriptLink("/Library/Plugins/MsgBox/JavaScripts/jQuery.msgBox.js")%>
    <%=Html.ScriptLink("/Library/Plugins/Json2/json2.js")%>
    <%=Html.ScriptLink("/Scripts/Views/LKAssembly/Subject/ViewLibSubject.js")%>
    <%=Html.ScriptLink("/Scripts/Views/LKAssembly/Test/Jquery.Object.js")%>
    <%=Html.ScriptLink("/Scripts/Views/LKAssembly/Test/EditExam.js")%>
    <title>在线考试-石黄高速管理处在线考试系统</title>
</head>
<body>
    <div class="ajxaLoading" id="loadingID">
    </div>
    <div class="EditExamLayout" id="LayoutID" style="display: none;">
        <div class="E_E_L_Integer">
            <a href="javascript:void(0)"></a>
            <div id="ExamHeader">
                <div class="E_E_L_I_H_Content">
                    <div class="ExamCenterIco">
                        <a href="/" title="返回石黄管理处考试中心首页" target="_blank" hidefocus="true"></a>
                    </div>
                    <div class="E_E_L_I_H_C_Title" id="ExamTitleID">
                        石黄管理处考试中心 <a href="javascript:void(0)"></a>
                    </div>
                    <div class="E_E_L_I_H_C_Set">
                        <span>考试时间：<label></label>分钟</span> <span>试卷总分：<label></label>分</span>
                    </div>
                </div>
            </div>
        </div>
        <div id="ExamContent">
            <div class="E_E_L_I_C_Left">
                <div class="E_E_L_I_C_L_Header" id="RatingHeaderID">
                    <%-- <a href="#" class="E_E_L_I_C_L_H_ArrayIco" title="题型排列方式"></a>--%>
                    <%--<div class="E_E_L_I_C_L_H_ArrayFont" style="display:block;">
                            <a>简略模式</a> <a>详细模式</a>
                        </div>--%>试卷导航
                   
                </div>
                <div class="E_E_L_I_C_L_Footer">
                    <div class="E_E_L_I_C_L_F_Left">
                        <div class="E_E_L_I_C_L_F_L_AddSubTypeBox" id="AddSubTypeBox">
                        </div>
                    </div>
                    <div class="E_E_L_I_C_L_F_Right">
                        <span class="E_E_L_I_C_L_F_Header" id="AddSubBox"></span>
                        <div class="E_E_L_I_C_L_F_R_SubBox" id="SubContentId">
                        </div>
                    </div>
                </div>
            </div>
            <div class="E_E_L_I_C_Right">
                <div class="E_E_L_I_C_R_SubView" id="subViewID">
                </div>
            </div>
        </div>
    </div>
</body>
</html>
