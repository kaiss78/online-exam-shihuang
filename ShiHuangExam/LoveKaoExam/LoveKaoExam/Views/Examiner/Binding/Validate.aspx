<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LoveKaoExam.Models.爱考网绑定账号>" %>

<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>验证绑定爱考网账号</title>
    <%=Html.CssJsBasic() %>
    <%=Html.ScriptLink("/Scripts/Views/Examiner/Binding.js")%>
</head>
<body>
    <!--* 值0表示新账号
        * 值1表示已有账号
        *-->
    <%int 用户绑定类型 = LKExamURLQueryKey.GetInt32("uBindingType"); %>
    <div class="usebox examiner-binding-validate">
        <ul class="editor">
            <li class="onlyFontH30">
                <h2>
                    绑定<%=Model.用户绑定类型 == 0 ? "新" : "爱考网"%>账号</h2>
            </li>
            <li>
                <!-- 如果是已有账号 可输入用户名/邮箱 -->
                <!-- 如果是新账号 只允许输入用户名 -->
                <%=Html.LabelW90("账号：", "账号")%>
                <%=Html.TextBoxFor(model => model.账号, new { id = "账号", @class = "textbg textbase", autocomplete = "off", maxlength = "16" })%>
            </li>
            <!-- 如果是已有账号 则显示密码 否则显示新密码 -->
            <li>
                <%=Html.LabelW90("密码：", "密码")%>
                <input name="密码" id="密码" class="textbg textbase" maxlength="16" type="password" />
            </li>
            <!-- 如果是已有账号 则不显示确认密码和电子邮箱 -->
            <%if (Model.用户绑定类型 == 0)
              {%>
            <li>
                <%=Html.LabelW90("确认密码：", "确认密码")%>
                <input name="确认密码" id="确认密码" class="textbg textbase" maxlength="16" type="password" />
            </li>
            <li>
                <%=Html.LabelW90("电子邮箱：", "邮箱")%>
                <%=Html.TextBoxFor(model => model.邮箱, new { id = "邮箱", @class = "textbg textbase", autocomplete = "off", maxlength = "32" })%>
            </li>
            <%} %>
            <li>
                <%=Html.LabelW90("验证码：", "验证码")%>
                <input name="验证码" id="验证码" class="textbg textbase" autocomplete="off" maxlength="4" type="text" style="width: 65px;" />
                <img id="图片验证码" alt=""/> 看不清?
                <a href="javascript:Global.HTML.ImageCode.reload('图片验证码');" title="看不清?点击换一张" style="color:#3187e3;">换一张</a>
            </li>
            <li style="margin-top:15px;">
                <%=Html.Button("btn1", "绑定" + (Model.用户绑定类型 == 0 ? "新" : "爱考网") + "账号", new { id = "提交按钮", onclick = "ExaminerBinding.EditSame.submit();" })%>
            </li>
        </ul>
    </div>

    <!-- 绑定账号成功/失败 消息内容 -->
    <div class="usebox hidden">
        <ul class="handleInfo">
            <li>
                <h2>
                    账号绑定成功，您现在可以：</h2>
            </li>
            <li class="itemIcon">查看我在主站爱考网(lovekao.com)出的试题，请点击 <a class="unline" href="#">我出的试题</a></li>
            <li class="itemIcon">查看我在主站爱考网(lovekao.com)出的试卷，请点击 <a class="unline" href="#">我出的试卷</a></li>
            <li class="itemIcon">资源共享吧！
                <%=Html.Button("btn7-input", "我要上传", new { onclick = "" })%>
                <%=Html.Button("btn7-input", "我要下载", new { onclick = "" })%>
            </li>
        </ul>
        <ul class="handleInfo">
            <li>
                <h2>
                    账号绑定失败，您现在可以：</h2>
            </li>
            <li class="itemIcon">该爱考网(lovekao.com)账号已被他人使用，我要<a class="unline" href="#">换一个账号</a></li>
            <li class="itemIcon">该爱考网(lovekao.com)账号设置不被任何绑定，我要<a class="unline" href="#">设置该账号允许被绑定</a></li>
            <li class="itemIcon">没有爱考网(lovekao.com)账号？<a class="unline" href="#">直接注册新账号吧</a></li>
        </ul>
    </div>

    <%=Html.ScriptText("ExaminerBinding.EditSame.initData(" + Model.用户操作类型 + "," + Model.用户绑定类型 + ")")%>
</body>
</html>
