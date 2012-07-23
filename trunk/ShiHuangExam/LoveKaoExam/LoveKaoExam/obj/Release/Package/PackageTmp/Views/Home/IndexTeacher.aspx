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
            功能导航：</dd>
        <dd>1.我要组卷：包括随机出题和手工出题，使用前应先使用我要出题来将题库完成，可在我的试题中查看是否有试题。</dd>
        <dd>2.我的试卷：可以预览，修改或删除已出的试卷，组织考试或练习；在预览试卷中可以导出为Word文档及打印试卷。</dd>
        <dd>3.我要出题：可以输入一道试题，包括很多题型，选择题可用识别输入。</dd>
        <dd>4.我的试题：可以对当前用户所出试题进行预览，修改或删除操作。</dd>
        <dd>5.已组织考试：可以查看当前用户所组织的考试，在分析试卷中对主观题部分进行批改操作，在报表中可查看当前参加考试的情况。</dd>
        <dd>6.已组织练习：对已组织的练习进行预览，修改设置或删除操作。</dd>
        <dd>7.修改资料：对当前用户的信息进行修改。</dd>
        <dd>8.修改密码：修改当前用户的密码，默认密码与账号相同。</dd>
        <%--<dd>
            爱考在线反馈问题和功能建议：<a href="http://bbs.lovekao.com/" target="_blank" title="爱考论坛(bbs.lovekao.com)-安装使用,功能建议,Bug反馈 ">http://bbs.lovekao.com</a>.</dd>
        <dd>
            爱考技术咨询：<samp>QQ:</samp><span>1528141287</span>,<samp>Email:</samp>
            <span>lovekaomaster@gmail.com.</span></dd>--%>
    </dl>
</asp:Content>
