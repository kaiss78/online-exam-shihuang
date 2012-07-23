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
                                    �� �ţ�
                                </td>
                                <td>
                                    <input type="text" class="text" id="uname" value="" label="" tips="�˺�" onfocus="this.className='text text-focus';LKLog_Pawd.exeText(this,'focus');"
                                        onblur="this.className='text';LKLog_Pawd.exeText(this,'blur');" maxlength="32"
                                        tabindex="1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="mark-name">
                                    �� �룺
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
                                        ��ס��¼״̬</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="mark-name">
                                </td>
                                <td>
                                    <button id="smt_login" tabindex="4" type="button" title="��¼ʯ�Ƹ��ٿ���ϵͳ" onclick="LKLog_Pawd.login_submit(this);">
                                        �� ¼
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
<%--                <a target="_blank" href="http://www.lovekao.com/Service/AboutUs.aspx" title="���ڰ�����">���ڰ�����</a> 
                <a href="#" title="ϵͳ����">ϵͳ����</a> 
                <a target="_blank" href="http://www.lovekao.com/Service/Feedback.aspx" title="�������">�������</a> 
                <a target="_blank" href="http://www.lovekao.com/Service/Customer.aspx" title="�ͷ�����"> �ͷ�����</a> 
                <a target="_blank" href="http://www.lovekao.com/Service/HelpPage/Help.html" title="��������">��������</a>
                <a href="javascript:void(0)" class="hidden" title="������">������</a> 
                <a href="javascript:void(0)" class="hidden" title="��վ��ͼ">��վ��ͼ</a>--%>
            </div>
            <div>δ��<a href="http://www.hbshgs.com/" title="������(LoveKao.com)" target="_blank">ʯ�Ƹ���</a>��Ȩ������������ҵ��;��Υ���ؾ�</div>
            <div>
                Copyright 2012, ��Ȩ���� ʯ�Ƹ��� <%--<a href="http://www.miibeian.gov.cn" style="margin-left: 5px;"
                    target="_blank">��ICP��09109004��</a>--%>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        /*
        *	jQuery�����ĵ�
        */
        $(document).ready(function () {

            /*
            *	jQueryԪ�ض���
            */
            var eOUserName = $("#UserName");
            var eOPassword = $("#Password");

            /*
            *	jQueryԪ��ֵ
            */
            var valUserName = $.trim(eOUserName.val());
            var valPassword = $.trim(eOPassword.val());

            /*
            *	�ж��û����Ƿ�����д
            */
            if (valUserName == "") {
                eOUserName.focus();
            }
            /*
            *	�ж������Ƿ�����д
            */
            else if (valPassword == "") {
                eOPassword.focus();
            }
        });

    </script>
</body>
</html>
