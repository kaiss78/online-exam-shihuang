<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>手工出卷-石黄高速管理处在线考试系统</title>
    <%=Html.StyleLink("/Content/StyleSheet/Basic.css")%>
    <%=Html.StyleLink("/Library/Plugins/Boxy-0.1.4/StyleSheets/boxy.css")%>
    <%=Html.StyleLink("/Library/Plugins/MsgBox/StyleSheets/msgBox.css")%>
    <%=Html.StyleLink("/Library/Plugins/LKSort/StyleSheets/LKSort.css")%>
    <%=Html.StyleLink("/Content/LKAssembly/Subject/ViewLibSubject.css")%>
    <%=Html.StyleLink("/Content/StyleSheet/themes/redmond/jquery-ui-1.8.9.custom.css")%>
    <%=Html.StyleLink("/Content/LKAssembly/Test/CreateExam.css")%>
    <%=Html.ScriptLink("/Scripts/JQuery/jquery-1.4.2.js")%>
    <%=Html.ScriptLink("/Scripts/Base/GlobalManage.js")%>
    <%=Html.ScriptLink("/Scripts/Base/LKConfig.js")%>
    <%=Html.ScriptLink("/Scripts/Base/Global.js")%>
    <%=Html.ScriptLink("/Library/Plugins/Boxy-0.1.4/JavaScripts/jQuery.boxy.js")%>
    <%=Html.ScriptLink("/Library/Plugins/MsgBox/JavaScripts/jQuery.msgBox.js")%>
    <%=Html.StyleLink("/Library/Plugins/lk_tags/tags.css")%>
    <%=Html.ScriptLink("/Scripts/JQuery/jquery-ui-1.8.9.custom.js")%>
    <%=Html.ScriptLink("/Library/Plugins/jquery_position/jquery-position.js")%>
    <%=Html.ScriptLink("/Library/Plugins/jquery_selection/jquery.selection.js")%>
    <%=Html.ScriptLink("/Library/Plugins/Json2/json2.js")%>
    <%=Html.ScriptLink("/Library/Plugins/lk_tags/lk.tags.js")%>
    <%=Html.ScriptLink("/Library/UI/blockUI/jquery.blockUI.js")%>
    <%=Html.ScriptLink("/Library/Plugins/LKSort/JavaScript/LKSortZJSY_IFRAME.js")%>
    <%=Html.ScriptLink("/Library/Plugins/Json2/json2.js")%>
    <%=Html.ScriptLink("/Library/Plugins/Equals/equals.js")%>
    <%=Html.ScriptLink("/Scripts/Views/LKAssembly/Subject/ViewLibSubject.js")%>
    <%=Html.ScriptLink("/Scripts/Views/LKAssembly/Test/CreateExam.js?TestUse=true")%>
    <style type="text/css">
        .tip_msg{z-index: 1800;}
    </style>
</head>
<body>
    <div class="ajxaLoading" id="loadingID">
    </div>
    <div class="CreateExamLayout">
        <div class="C_E_L_Integer" id="IntegerID" style="display: none;">
            <div class="C_E_L_I_Header">
                <div class="C_E_L_I_H_Content">
                    <div class="TestCenterIco">
                        <a href="/" title="返回石黄管理处考试中心首页" target="_blank" hidefocus="true"></a>
                    </div>
                    <div class="C_E_L_I_H_C_Title" id="outsideInfoId">
                        未设置
                    </div>
                    <div class="C_E_L_I_H_C_Set">
                        <span>考试时间：<label>120</label>分钟</span><span>试卷总分：<label>100</label>分</span> <span>当前总分：<label>0</label>分</span>
                        <span>所属分类：<label>未设置</label></span></div>
                </div>
                <div class="C_E_L_I_H_ToolBar">
                    <div class="C_E_L_I_H_Tool_B_Set">
                        <a href="javascript:void(0)" onclick="TestPageManage.back();" class="C_E_L_I_H_T_A">
                            返回</a> <a href="javascript:void(0)" class=" C_E_L_I_H_T_A C_E_L_I_H_C_T_Set" onclick="TestBoxyManage.showTestSetBox();">
                                试卷设置</a>
                        <div class="C_E_L_I_H_T_Refer">
                            <a class="C_E_L_I_H_T_R_Left" title="提 交" href="javascript:void(0)" onclick="TestReferSever.ajaxIsNew('0');">
                                提 交</a> <a class="C_E_L_I_H_T_R_Right" title="选择保存类型" href="javascript:void(0)" onclick="TestReferSever.showChooseRefer(this);">
                                </a>
                        </div>
                        <div class="SaveType" id="SaveTypeID">
                            <a href="javascript:void(0)" onclick="TestReferSever.ajaxIsNew('0');" style="word-spacing: 7px;">
                                提 交</a> <a href="javascript:void(0)" onclick="TestReferSever.ajaxIsNew('4');">保存草稿</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="C_E_L_I_Content">
                <div class="C_E_L_I_C_Left">
                    <div class="C_E_L_I_C_L_Header">
                        <%--<a href="#" class="C_E_L_I_C_L_H_ArrayIco" title="题型排列方式"></a>试卷导航
                        <div class="C_E_L_I_C_L_H_ArrayFont">
                            <a>简略模式</a> <a>详细模式</a>
                        </div>--%>
                    </div>
                    <div class="C_E_L_I_C_L_Footer">
                        <div class="C_E_L_I_C_L_F_Left">
                            <div class="AddSubject AddSubType">
                                <a href="#" class="C_E_L_I_C_L_F_L_AddIco" onclick="TestBoxyManage.show();">新增题型</a></div>
                            <div class="C_E_L_I_C_L_F_L_AddSubTypeBox" id="AddSubTypeBox">
                            </div>
                        </div>
                        <div class="C_E_L_I_C_L_F_Right">
                            <span class="C_E_L_I_C_L_F_Header" id="AddSubBox"></span>
                            <div class="C_E_L_I_C_L_F_R_SubBox" id="SubContentId">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="C_E_L_I_C_Right">
                    <div class="C_E_L_I_C_R_Header">
                        <a href="javascript:void(0)" class="C_E_L_I_C_R_H_Preview" style="cursor: text;">试题预览</a>
                    </div>
                    <div class="C_E_L_I_C_R_SubView" id="subViewID">
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
