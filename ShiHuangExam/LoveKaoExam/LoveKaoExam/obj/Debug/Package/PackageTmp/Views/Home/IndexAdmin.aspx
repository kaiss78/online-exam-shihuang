<%@ Page Title="用户中心" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>
<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        dt{font-size: 14px;font-weight: bold;}
        dd{padding: 5px 0px;padding-left: 15px;}
        span{color: #f50;}
    </style>
    <dl>
        <dt>
            <%="欢迎使用" + BasePage.LKExamName + BasePage.Version%></dt>
            <dd /> <dd />
        <dd>
            使用导航：</dd>
        <dd>
            1.添加账户：可以添加员工与主管账户，员工账户只可以参加考试或练习，主管账户能出题和组织考试或练习。</dd>
        <dd>
            2.员工信息：可以查看，删除，修改员工信息。</dd>
        <dd>
            3.主管信息：可以查看，删除，修改主管信息。</dd>
        <dd>4.导入导出：可以批量从Excel表中导入员工信息，或导出为Excel表；Excel表有固定样式。</dd>
        <dd>5.添加部门：可以添加部门。</dd>
        <dd>6.部门信息：可以查看，删除，修改部门信息。</dd>
        <dd>7.环境配置：可以选择系统环境，学校或单位。</dd>
        <dd>8.修改密码：可以修改当前账户的密码。</dd>
    </dl>
</asp:Content>
