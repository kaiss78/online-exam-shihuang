<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<%@ Import Namespace="LoveKaoExam.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组织考试/练习设置<%=BasePage.PageTitle%></title>
    <%=Html.CssJsBasic() %>
    <%=Html.StyleLink("/Content/StyleSheet/tabSwitch.css") %>
    <%=Html.StyleLink("/Library/UI/jquery-ui-1.8.6.custom/development-bundle/themes/redmond/jquery.ui.all.css")%>
    <%=Html.StyleLink("/Library/UI/jquery-ui-1.8.6.custom/development-bundle/themes/redmond/jquery.ui.datepicker.css")%>
    <%=Html.ScriptLink("/Library/UI/jquery-ui-1.8.6.custom/js/jquery-ui-1.8.6.custom.min.js")%>
    <%=Html.ScriptLink("/Library/UI/jquery-ui-1.8.6.custom/development-bundle/ui/jquery-ui-timepicker-addon.js")%>
    <%=Html.ScriptLink("/Library/UI/jquery-ui-1.8.6.custom/development-bundle/ui/i18n/jquery.ui.datepicker-zh-CN.js")%>
    <%=Html.ScriptLink("/Library/UI/jquery-ui-1.8.6.custom/development-bundle/ui/i18n/jquery-ui-timepicker-addon-zh-CN.js")%>
    <%=Html.ScriptLink("/Library/UI/blockUI/jquery.blockUI.js")%>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/Organization.js")%>
    <style type="text/css">
        .ui-datepicker
        {
            font-size: 1em;
        }
        /* css for timepicker */
        .ui-timepicker-div .ui-widget-header
        {
            margin-bottom: 8px;
        }
        .ui-timepicker-div dl
        {
            text-align: left;
        }
        .ui-timepicker-div dl dt
        {
            height: 25px;
        }
        .ui-timepicker-div dl dd
        {
            margin: -25px 0 10px 65px;
        }
        .ui-timepicker-div td
        {
            font-size: 90%;
        }
    </style>
</head>
<body>
    <%试卷设置 view试卷设置 = (试卷设置)ViewData["试卷设置"]; %>
    <ul class="tabSwitch">
        <li class="tabMenu first first-on" onclick="Global.TabSwitch.target(this);Global.MsgBox.Hide.all();">
            <%=view试卷设置.设置类型 == 0 ? "练习" : "考试"%>设置</li>
        <li class="tabMenu end" mbleft="160" onclick="Global.TabSwitch.target(this);Global.MsgBox.Hide.all();Global.MvcPager.Search.Input.tab(1);">
            考生范围</li>
    </ul>
    <div class="tagCollection" style="height: 400px;">
        <div class="tabMsg usebox examiner-organization-setup">
            <ul class="handleInfo">
                <li>
                    <h2>
                        及格条件：</h2>
                </li>
                <li class="itemIcon">
                    <label>
                        百分比值：</label>
                    <%=Html.TextBox("及格条件", view试卷设置.及格条件, new { id = "及格条件", @class = "textbg percen", maxlength = "2", onkeyup = "ExaminerOrganization.Setup.ValidationData.及格条件(this);", onblur = "ExaminerOrganization.Setup.ValidationData.及格条件(this);" })%><span>%</span></li>
                <li>
                    <h2>
                        时间范围：</h2>
                </li>
                <li class="itemIcon">
                    <label>
                        考试时长：</label>
                    <%=Html.TextBox("考试时长", view试卷设置.考试时长, new { id = "考试时长", @class = "textbg", maxlength = "3", onkeyup = "ExaminerOrganization.Setup.ValidationData.考试时长(this);", onblur = "ExaminerOrganization.Setup.ValidationData.考试时长(this);" })%><span>分钟</span>
                </li>
                <%if (view试卷设置.设置类型 == 1)
                  { %>
                <li class="itemIcon" style="margin-top: 10px;">
                    <label>
                        开始时间：</label>
                    <%=Html.TextBox("考试开始时间", view试卷设置.考试开始时间.ToString("yyyy-MM-dd HH:mm"), new { id = "考试开始时间", @class = "textbg starttime", @readonly = "readonly" })%>
                    <label>
                        结束时间：</label>
                    <%=Html.TextBox("考试结束时间", view试卷设置.考试结束时间.ToString("yyyy-MM-dd HH:mm"), new { id = "考试结束时间", @class = "textbg endtime", @readonly = "readonly", onfocus = "this.blur();" })%>
                </li>
                <li>
                    <h2>
                        公布成绩：</h2>
                </li>
                <li class="itemIcon">
                    <label>
                        是否公布考试结果：</label>
                    <%=Html.RadioButton("是否公布考试结果", true, view试卷设置.是否公布考试结果 == true, new { id = "公布" })%>
                    <%=Html.Label("是", "公布")%>
                    <%=Html.RadioButton("是否公布考试结果", false, view试卷设置.是否公布考试结果 == false, new { id = "不公布" })%>
                    <%=Html.Label("否", "不公布")%>
                </li>
                <%} %>
            </ul>
        </div>
        <div class="tabMsg hidden" style="margin: 0 20px; border-left: #abc0db 1px solid;
            border-right: #abc0db 1px solid;">
            <div id="Master_Main_Content">
                <%=Html.MvcPagerAjaxSearch(new LKPageMvcPager文本输入框(LKExamEnvironment.考生编号 + "/姓名"), new LKPageMvcPager下拉列表框())%>
                <%Html.MvcPagerAjaxRenderPartial("~/Views/Examiner/Organization/UCSetup.ascx");%>
            </div>
        </div>
    </div>
    <div style="margin-top: 20px; padding-left: 40px;">
        <%=Html.Button("btn1", "保存设置", new { id = "提交按钮", onclick = "ExaminerOrganization.Setup.submit();" })%>
    </div>
    <!-- 考生ID集合 -->
    <%List<Guid> 考生ID集合 = view试卷设置.考生ID集合;
      string 考生ID数组 = 考生ID集合.Count == 0 ? "[]" : "[\'" + string.Join("\',\'", 考生ID集合.ToArray()) + "\']"; %>
    <!-- 如果是修改考试 取ID，如果是修改练习 取试卷内容ID -->
    <%Guid 考试设置ID = view试卷设置.ID; %>
    <%Guid 试卷内容ID = view试卷设置.试卷内容ID; %>
    <!-- 用户操作类型 -->
    <%int 用户操作类型 = LKExamURLQueryKey.GetInt32("uHandleType");%>
    <!-- ExaminerOrganization.Setup.initData(用户操作类型,设置类型,试卷内容ID,考试设置ID,考生ID数组) -->
    <%=Html.ScriptText("ExaminerOrganization.Setup.initData(" + 用户操作类型 + "," + view试卷设置.设置类型 + ",\'" + 试卷内容ID + "\',\'" + 考试设置ID + "\'," + 考生ID数组 + ");")%>
</body>
</html>
