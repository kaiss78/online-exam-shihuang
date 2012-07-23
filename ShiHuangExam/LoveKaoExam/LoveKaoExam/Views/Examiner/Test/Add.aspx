<%@ Page Title="我要组卷" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" runat="server">
  

    <%=Html.StyleLink("/Library/Plugins/LKSort/StyleSheets/LKSort.css")%>
    <%=Html.StyleLink("/Library/Plugins/lk_tags/tags.css")%>
    <%=Html.StyleLink("/Content/StyleSheet/themes/redmond/jquery-ui-1.8.9.custom.css")%>
    <%=Html.ScriptLink("/Scripts/JQuery/jquery-ui-1.8.9.custom.js")%>
    <%=Html.ScriptLink("/Library/Plugins/jquery_position/jquery-position.js")%>
    <%=Html.ScriptLink("/Library/Plugins/jquery_selection/jquery.selection.js")%>
    <%=Html.ScriptLink("/Library/Plugins/Json2/json2.js")%>
    <%=Html.ScriptLink("/Library/Plugins/lk_tags/lk.tags.js")%>
    <%=Html.ScriptLink("/Library/Plugins/LKSort/JavaScript/LKSortZJSY_IFRAME.js")%>
    <%=Html.ScriptLink("/Scripts/Views/LKAssembly/Test/RandomTest.js")%>
    <%=Html.StyleLink("/Content/LKAssembly/Test/RandomTest.css")%>
    
    <div class="membersInsideLayout">
        <!-- begin 内部布局头部-->
        <div class="membersInsideLayoutHeader">
            <!-- begin 横向导航菜单-->
            <%--<div class="membersBarMenu">
                <ul class="membersBarMenuUl">
                    <li class="membersBarMenuUlLi membersBarMenuOnBg">新增试卷 </li>
                    <li class="membersBarMenuUlLi membersBarMenuOffBg">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Members/Test/Finish.aspx"
                            CssClass="membersBarMenuOffBgLink">我的出卷</asp:HyperLink>
                    </li>
                    <li class="membersBarMenuUlLiLine"></li>
                </ul>
            </div>--%>
            <!-- end 横向导航菜单-->
            <!-- begin 横向导航子菜单(可隐藏)-->
            <div class="membersBarMenuSub">
                <!-- begin 会员中心内部布局子横向条-->
                <div class="membersBarMenuSubInside">
                    <div class="membersBarMenuSubInsideLink" style="margin-left: 0px;">
                        <div class="membersBarMenuSubOn">
                            随机出卷
                        </div>
                    </div>
                    <div class="membersBarMenuSubInsideLine">
                        ┊</div>
                    <div class="membersBarMenuSubInsideLink">
                        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="/Test/EditExam" Target="_blank"
                            CssClass="membersBarMenuSubOff">手工出卷</asp:HyperLink>
                    </div>
                </div>
                <!-- end 会员中心内部布局子横向条-->
            </div>
            <!-- end 横向导航子菜单-->
        </div>
        <!-- end 内部布局头部-->
        <!-- begin 内部布局内容区-->
        <div class="RandomTestLayout">
            <div class="R_T_C_Left">
                <div class="TestSetBox" id="TestSetBoxID">
                    <div class="SubTypeChooseBox">
                        <div class="TestSetBoxContent" style="color: #666; font-size: 15px;">
                            <div class="TestSetBoxLeft" style="font-weight: bold;">
                                第一步：</div>
                            <div class="TestSetBoxRight" style="line-height: 58px;">
                                请选择分类范围
                            </div>
                        </div>
                        <div class="TestSetBoxContent">
                            <div class="TestSetBoxLeft" style="line-height: 9px;">
                                分类：</div>
                            <div class="TestSetBoxRight" id="RandomTestSortID" style="position:relative;">
                            </div>
                        </div>
                        <div class="TestSetBoxContent" style="width: 650px; font-size: 12px; padding-top: 50px;">
                            <a class="commonIco changeSubTypeName nextIcon" href="javascript:void(0)" title="点击下一步可以选择题型范围"
                                hidefocus="true" onclick="AjaxRandomManange.ajaxIsSort();">点击下一步</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- end 内部布局内容区-->
    </div>
</asp:Content>
