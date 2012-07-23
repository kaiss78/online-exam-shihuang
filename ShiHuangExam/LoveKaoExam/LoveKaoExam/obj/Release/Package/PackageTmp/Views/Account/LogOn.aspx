<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LoveKaoExam.Models.LogOnModel>" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title><%=BasePage.LogonTitle%></title>
    <meta name="Description" content="<%=BasePage.Description %>" />
    <meta name="keywords" content="<%=BasePage.KeyWords %>" /> 
    <%=Html.CssJsBasic() %>
    <%=Html.StyleLink("/Content/StyleSheet/LogOn.css")%>
    <script src="/Library/Plugins/LoginReg/JavaScript/Login.js" type="text/javascript"></script>
</head>
<body>
    <div id="LogOnContainer">
        <div id="LogOnHead">
        </div>
        <div id="LogOnBody">
            <div class="item" id="Introduction">
            <ul>
               
            </ul>
            </div>
            <div class="item login-pawd" id="Entrance">
                <div class="cont" >
                    <div class="left" style="position:relative;">
                        <table class="table-text">
                            <tr>
                                <td class="mark-name">
                                    账 号：
                                </td>
                                <td>
                                    <input type="text" class="text" id="uname" value="" label="" tips="账号" onfocus="this.className='text text-focus';LKLog_Pawd.exeText(this,'focus');"
                                        onblur="this.className='text';LKLog_Pawd.exeText(this,'blur');" maxlength="32"
                                        tabindex="1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="mark-name">
                                    密 码：
                                </td>
                                <td>
                                    <input type="password" class="text" id="upawd" style="color: #333; font-size: 14px;"
                                        onfocus="this.className='text text-focus';" onblur="this.className='text';" maxlength="25"
                                        tabindex="2" />
                                </td>
                            </tr>
                            <tr style="height: 40px;">
                                <td class="mark-name">
                                </td>
                                <td>
                                    <input type="checkbox" id="ustate" checked="checked" tabindex="3" />
                                    <label for="ustate" class="re-state-font" title="">
                                        记住登录状态</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="mark-name">
                                </td>
                                <td>
                                    <button id="smt_login" tabindex="4" type="button" title="登录石黄高速考试系统" onclick="LKLog_Pawd.login_submit(this);">
                                        登 录
                                    </button>
                                </td>
                            </tr>
                        </table>
                       
                    </div>
                </div>
            </div>
        </div>
        <div id="LogOnFoot">
            <div>
<%--                <a target="_blank" href="http://www.lovekao.com/Service/AboutUs.aspx" title="关于爱考网">关于爱考网</a> 
                <a href="#" title="系统声明">系统声明</a> 
                <a target="_blank" href="http://www.lovekao.com/Service/Feedback.aspx" title="意见反馈">意见反馈</a> 
                <a target="_blank" href="http://www.lovekao.com/Service/Customer.aspx" title="客服中心"> 客服中心</a> 
                <a target="_blank" href="http://www.lovekao.com/Service/HelpPage/Help.html" title="帮助中心">帮助中心</a>
                <a href="javascript:void(0)" class="hidden" title="广告服务">广告服务</a> 
                <a href="javascript:void(0)" class="hidden" title="网站地图">网站地图</a>--%>
            </div>
            <div>未经<a href="http://www.hbshgs.com/" title="爱考网(LoveKao.com)" target="_blank">石黄高速</a>授权，不得用于商业用途，违反必究</div>
            <div>
                Copyright 2012, 版权所有 石黄高速 <%--<a href="http://www.miibeian.gov.cn" style="margin-left: 5px;"
                    target="_blank">浙ICP备09109004号</a>--%>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        /*
        *	jQuery加载文档
        */
        $(document).ready(function () {

            /*
            *	jQuery元素对象
            */
            var eOUserName = $("#UserName");
            var eOPassword = $("#Password");

            /*
            *	jQuery元素值
            */
            var valUserName = $.trim(eOUserName.val());
            var valPassword = $.trim(eOPassword.val());

            /*
            *	判断用户名是否已填写
            */
            if (valUserName == "") {
                eOUserName.focus();
            }
            /*
            *	判断密码是否已填写
            */
            else if (valPassword == "") {
                eOPassword.focus();
            }
        });

    </script>
</body>
</html>
