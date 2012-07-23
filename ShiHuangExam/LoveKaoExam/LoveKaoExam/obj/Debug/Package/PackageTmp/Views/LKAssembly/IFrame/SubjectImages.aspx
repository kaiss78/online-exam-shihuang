<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="LoveKao.Page.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.HTML" %>
<%@ Import Namespace="LoveKaoExam.Library.CSharp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: 12px;
        }
        .qnImagesBox
        {
            width: 800px;
            height: auto;
        }
        .qnImagesBoxContent
        {
            width: 100%;
            height: 340px;
            display: block;
        }
        .qnImagesBoxContentLi
        {
            width: 100%;
            height: 100%;
            display: none;
        }
        .qnImagesBoxContentTab0List
        {
            width: 100%;
            height: 340px;
        }
        .qnImagesBoxContentTab0PagerLine
        {
            width: 100%;
            height: 5px;
            border-top: dotted 1px #ccc;
            margin-top: 5px;
        }
        *html .qnImagesBoxContentTab0PagerLine
        {
            margin-top: -5px;
        }
        .qnImagesBoxContentTab0Pager
        {
            width: 100%;
            height: 30px;
        }
        .qnImagesBoxContentTab0Pager a
        {
            width: 40px;
            height: 20px;
            border: solid 1px #999;
            text-decoration: none;
            text-align: center;
            line-height: 20px;
            margin: 5px 0px 5px 5px;
            float: left;
        }
        .qnImagesPagerCount
        {
            width: 70px;
            height: 20px;
            line-height: 20px;
            margin: 5px 5px 5px 5px;
            float: left;
            text-align: right;
        }
        .qnImagesPagerDis
        {
            color: #999;
        }
        .qnImagesPagerUse
        {
            color: #369;
        }
        .qnImagesBoxContentLiList
        {
            width: 146px;
            height: 160px;
            float: left;
            display: inline;
            margin: 7px 1px 0px 10px;
        }
        .qnImagesBoxContentLiListPic
        {
            width: 146px;
            color: #999;
            text-align: center;
            line-height: 140px;
            height: 140px;
            margin: 0px;
            background: url(/Content/LKAssembly/Images/AllPage/ImagesBg.gif) no-repeat;
        }
        
        .qnImagesBoxContentLiListPic img
        {
            width: 120px;
            border: 0px;
            height: 120px;
            float: left;
            cursor: pointer;
            margin: 6px 0px 0px 13px;
        }
        *html .qnImagesBoxContentLiListPic
        {
            display: inline;
            float: left;
            margin-right: -3px;
        }
        .qnImagesBoxContentLiListFooter
        {
            height: 20px;
            margin: 0px 8px;
            display: block;
            float: left;
            display: inline;
        }
        .qnImagesBoxContentLiListFooter a
        {
            width: 30px;
            height: 16px;
            line-height: 16px;
            margin: 2px 6px;
            float: left;
            display: inline;
            text-align: center;
            color: #369;
            text-decoration: none;
        }
        .qnImagesBoxContentLiListFooter a:hover
        {
            text-decoration: underline;
        }
        .qnImagesBoxContentLiTipMsg
        {
            display: block;
            height: 30px;
            line-height: 30px;
            margin-top: 50px;
            padding-left: 30px;
        }
        .qnImagesBoxContentLiUpload
        {
            display: block;
            height: 30px;
            line-height: 30px;
            padding-left: 30px;
        }
        .inputUpload
        {
            width: 300px;
            height: 25px;
            padding: 4px 5px;
        }
        .inputButton
        {
            width: 85px;
        }
        .qnImagesBoxContentBigImg
        {
            height: 375px;
            margin: 10px 0px 0px 0px;
            overflow: auto;
            text-align: center;
            display: none;
            width: 800px;
            float: left;
        }
        .qnImagesBoxContentBigImg a
        {
            line-height: 100px;
            color: #666;
            text-decoration: none;
            border: 0px;
            outline: none;
            cursor: text;
        }
        .qnImagesBoxContentBigImg img
        {
            border: dotted 1px #369;
            cursor: pointer;
        }
        .qnImagesBoxContentBigImgPage
        {
            height: 25px;
            display: block;
            width: 600px;
            margin-top: 3px;
            border-top: dotted 1px #ccc;
            float: left;
        }
        .qnImgLoading
        {
            height: 40px;
            line-height: 40px;
            line-height: 40px;
            display: none;
        }
        .qnImgLoadingBg
        {
            width: 50px;
            float: left;
            height: 40px;
            background: url(/Content/LKAssembly/Images/AllPage/IconQnSaveLoading.gif) no-repeat center right;
        }
        .qnImgLoadingText
        {
            width: 200px;
            height: 40px;
            color: #333;
            float: left;
            margin-left: 5px;
            display: inline;
        }
    </style>
    <%=Html.CssJsBasic() %>
    <script src="/Scripts/JQuery/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../../../Library/Plugins/Form/jquery.form.js" type="text/javascript"></script>
    <script src="/Scripts/Base/GlobalManage.js" type="text/javascript"></script>
    <script type="text/javascript">

        //图片列表
        var ListImages = {
            objListImg: 0, objListPager: 0, UserName: 0, arrImg: [], objBigImg: 0, IntCurrIndex: 0, intShowBigNum: 0,
            //初始化
            Init: function (pArrImg) {
                this.IntCurrIndex = 0;
                this.intShowBigNum = 0;
                this.arrImg = pArrImg;
                this.objListImg = this.objListImg || $("#QnImagesBoxContentTab0List");
                this.objListPager = this.objListPager || $("#QnImagesBoxContentTab0Pager");
                this.Show(0); //--------------------------------------------------------------页面加载会执行---需处理
            },
            //切换内容
            SwitchTab: function (pNum) {
                $(".qnImagesBoxContentLi").css("display", "none");
                $("#QnImagesBoxContentTab" + pNum).css("display", "block");
            },
            preloadimg: function (url, obj, pUserName, pFileName) {
                var img = new Image();
                obj.innerHTML = "<p>图片加载中...</p>";
                img.onload = function () { obj.innerHTML = ""; obj.appendChild(img); };
                img.onerror = function () { obj.innerHTML = "图片加载失败！" };
                img.src = url;
                img.onclick = function () { ListImages.ShowBigImg(pUserName, pFileName); }
                img.title = "点击预览大图";
            },

            //展示
            Show: function (pIndex) {
                if (pIndex < 0) {
                    pIndex = 0;
                }
                this.objListImg.html("");
                //当前索引值
                var intIndex = parseInt(pIndex);
                //数据的长度
                var intLen = this.arrImg.length;

                //页数
                var intPart = intLen % 10;
                var pageCount = 0;
                if (intPart == 0) {
                    pageCount = parseInt(intLen / 10);
                }
                else {
                    pageCount = parseInt(intLen / 10) + 1;
                }

                //当前页的最后一个
                var intNext = (intIndex + 1) * 10;
                //如果比总数长，设为最大值
                if (intNext > intLen) {
                    intNext = intLen;
                }

                for (var i = intIndex * 10; i < parseInt(intNext); i++) {
                    var html = "<div class=\"qnImagesBoxContentLiList\">"
                    + "<div class=\"qnImagesBoxContentLiListPic\" id=\'" + i + "\' >"

                    + "<\/div>"
                    + "<div class=\"qnImagesBoxContentLiListFooter\">"
                    + "<a href=\"javascript:void(0)\" onclick=\"ListImages.Insert(\'" + this.UserName + "\',\'" + this.arrImg[i] + "\');\" title=\"选择该图片插入到题干\" >选择<\/a>"
                    + "<a href=\"javascript:void(0)\" onclick=\"ListImages.Big(\'" + this.UserName + "\',\'" + this.arrImg[i] + "\');\" title=\"预览大图片\">预览<\/a>"
                    + "<a href=\"javascript:void(0)\" onclick=\"ListImages.Del(\'" + this.UserName + "\',\'" + this.arrImg[i] + "\');\" title=\"删除该图片\">删除<\/a>"
                    + "<\/div>"
                    + "<\/div>";
                    this.objListImg.append(html);
                    var src = "\/UploadFiles\/" + this.UserName + "\/Images\/" + this.arrImg[i];
                    this.preloadimg(src, document.getElementById(i), this.UserName, this.arrImg[i]);
                }
                var strText1 = "", strText2 = "", strText3 = "", strText4 = "";
                var strDis = "class=\"qnImagesPagerDis\"", strUse = "class=\"qnImagesPagerUse\"", strHref = "href=\"javascript:void(0)\"";
                //页数少于等于一页
                if (pageCount <= 1) {
                    strText1 = strText2 = strText3 = strText4 = strDis;
                }
                else if (pIndex == 0) {
                    strText1 = strText2 = strDis;
                    strText3 = strUse + strHref + "onclick=\"ListImages.Show(\'" + parseInt(intIndex + 1) + "\');\"";
                    strText4 = strUse + strHref + "onclick=\"ListImages.Show(\'" + parseInt(pageCount - 1) + "\');\"";
                }
                else if (pIndex == parseInt(pageCount - 1)) {
                    strText1 = strUse + strHref + "onclick=\"ListImages.Show(\'0\');\"";
                    strText2 = strUse + strHref + "onclick=\"ListImages.Show(\'" + parseInt(intIndex - 1) + "');\"";
                    strText3 = strText4 = strDis;
                }
                else {
                    strText1 = strUse + strHref + "onclick=\"ListImages.Show(\'0\');\"";
                    strText2 = strUse + strHref + "onclick=\"ListImages.Show(\'" + parseInt(intIndex - 1) + "');\"";
                    strText3 = strUse + strHref + "onclick=\"ListImages.Show(\'" + parseInt(intIndex + 1) + "\');\"";
                    strText4 = strUse + strHref + "onclick=\"ListImages.Show(\'" + parseInt(pageCount - 1) + "\');\"";
                }
                var intCurrSize = parseInt(intIndex + 1);
                if (pageCount == 0) {
                    intCurrSize = 0;
                }

                var strPager = "<div class=\"qnImagesPagerCount\">第" + intCurrSize + "/" + pageCount + "页<\/div>" +
                               "<a " + strText1 + " title=\"首页\">首页<\/a><a " + strText2 + " title=\"上一页\">上一页<\/a>" +
                               "<a " + strText3 + " title=\"下一页\">下一页<\/a><a " + strText4 + " title=\"尾页\">尾页<\/a>";
                this.objListPager.html(strPager);

                this.IntCurrIndex = pIndex;

            },
            //插入到题干
            Insert: function (pDir, pFileName) {

                parent.IFrameSubjectImages.InsertFckImages(pDir, pFileName);
            },
            //发大
            Big: function (pUserName, pFileName) {
                this.ShowBigImg(pUserName, pFileName);
            },

            //删除
            Del: function (pDir, pFileName) {
                parent.IFrameSubjectImages.DelIamges_验证(pDir, pFileName);
            },
            //删除成功
            DelSuccess: function (pFileName) {
                //更新列表

                for (var i = 0; i < this.arrImg.length; i++) {
                    if (this.arrImg[i] == pFileName) {
                        this.arrImg.splice(i, 1);
                        if (i == this.intShowBigNum) {
                            $(".qnImagesBoxContentBigImg").css("display", "none");
                        }
                    }
                }
                var intSetIndex = this.IntCurrIndex;

                if (parseInt(this.IntCurrIndex) * 10 == this.arrImg.length) {
                    intSetIndex = parseInt(this.IntCurrIndex - 1)
                }
                this.Show(intSetIndex);
            },
            SetSubmit: function (pObj) {
                var objButton = $("#提交按钮");
                if (pObj.val() != "") {
                    objButton.removeAttr("disabled");
                    return;
                }
                objButton.attr("disabled", "disabled");
            },

            ShowBigImg: function (pUserName, pFileName) {

                for (var i = 0; i < this.arrImg.length; i++) {
                    if (pFileName == this.arrImg[i]) {
                        this.intShowBigNum = i;
                    }
                }
                parent.IFrameSubjectImages.AutoShowImage(2);

                var strSrc = "\/UploadFiles\/" + ListImages.UserName + "\/Images\/" + pFileName;
                var img = new Image();
                var obj = document.getElementById("showBigPic");

                obj.innerHTML = "图片加载中......";
                img.onload = function () { obj.innerHTML = ""; obj.appendChild(img); }
                img.onerror = function () { obj.innerHTML = "图片加载失败"; }
                img.src = strSrc;
                img.title = "双击图片插入到题目";
                img.ondblclick = function () { parent.IFrameSubjectImages.InsertFckImages(pUserName, pFileName); };
                $(".qnImagesBoxContentBigImg").fadeIn("fast");
            },
            //上传
            UploadIng: function () {
                $("#QnImgLoading").css("display", "block");
            }
        };

        //后台输出JS
        var CSharpJS = {
            objTipMsg: 0, objTipUpload: 0, objBigImg: 0,
            Init: function () {
                this.objTipMsg = this.objTipMsg || $(".qnImagesBoxContentLiTipMsg");
                this.objTipUpload = this.objTipUpload || $(".qnImagesBoxContentLiUpload");
                this.objBigImg = this.objBigImg || $(".qnImagesBoxContentBigImg");
            },

            verify_login: function () {

                var lk_lr = parent.LK_LoginReg;
                lk_lr && lk_lr.show({
                    是否提示未登录: true,
                    预回调函数: function () {
                        parent.IFrameSubjectImages.GetImagesList();
                    }
                });
            },

            //跳转
            Redirect: function (pUrl) {
                window.location.href = pUrl;
            },
            //提示
            TipMsg: function (pInfor) {
                alert(pInfor);
                return;
            },

            //保存图片成功
            SaveSuccess: function (pDir, pFileName) {
                //获取目录
                ListImages.UserName = pDir;
                //更新列表
                parent.IFrameSubjectImages.GetImagesList();

                //显示大图
                window.setTimeout("ListImages.ShowBigImg(\'" + pDir + "\', \'" + pFileName + "\');", 300);

            }
        };

        //LK程序集
        var LKAssemblySubjectImages = {


            //#region initData：初始化数据
            initData: function () {

                //元素集合
                var elemAssembly = this.ElemAssembly;

                //# 网页已加载完成
                Global.LoadIng.ready(function () {
                    elemAssembly.Post.图片文件 = $("#图片文件");
                    elemAssembly.Other.提交按钮 = $("#提交按钮");
                });
            },
            //#endregion

            //#region ElemAssembly：元素集合
            ElemAssembly: {
                Post: {
                    "图片文件": 0
                },
                Other: {
                    "提交按钮": 0
                }
            },
            //#endregion

            //#region  DataAssembly：数据集合
            DataAssembly: {
                Post: {
                    "图片文件": ""
                },
                Backup: {}
            },
            //#endregion

            //#region Format：格式化
            Format: {

                //#region 登录系统
                登录系统: function (fn) {
                    LK_LoginReg.show({
                        "是否提示未登录": true,
                        "预回调函数": function () {
                            Global.CallBack.true_0(fn);
                        }
                    });
                },
                //#endregion

                //#region 图片文件
                图片文件: function (fn) {
                    var hE图片文件 = LKAssemblySubjectImages.ElemAssembly.Post.图片文件;
                    var hE提交按钮 = LKAssemblySubjectImages.ElemAssembly.Other.提交按钮;
                    var filePath = Global.Extension.fileUpload(hE图片文件[0]);

                    /* 图片文件路径为空 */
                    if (filePath == "") {
                        Global.CallBack.false_3(fn, "请先选择图片文件！", hE图片文件);
                    }
                    /* 是否符合图片文件 */
                    else {

                        Global.Extension.isImage(filePath, function (fData) {

                            //符合图片文件格式
                            if (fData.result) {
                                Global.CallBack.true_1(fn, filePath)
                            }
                            /* 不符合图片文件格式 */
                            else {
                                Global.CallBack.false_3(fn, fData.info, hE图片文件);
                            }
                        });
                    }
                }
                //#endregion

            },
            //#endregion

            //#region Handle：多类型操作
            Handle: {

                //#region Type：操作类型
                Type: {},
                //#endregion

                //#region name：操作名称
                name: function () { },
                //#endregion

                //#region embedBoxyHide：隐藏嵌入式Boxy对话框
                embedBoxyHide: function () { },
                //#endregion

                embedBoxyBtn: function (isVisible) { },

                //#region completed：数据提交完成的回调函数
                completed: function () { }
                //#endregion
            },
            //#endregion

            //#region postAjax：post方式提交数据到服务器
            postAjax: function (postData) {

                /* Boxy唯一键 */
                var boxyKey = "LKAssemblySubjectImages";
                /* 标语 */
                var topics = "图片"

                /* 用户操作类型：创建，修改，删除等 */
                var handle = this.Handle;
                /* 用户操作名称 */
                var handleName = "上传";

                handle.embedBoxyBtn(false);

                //#region 请求等待中
                Global.Boxy.wait(boxyKey, {
                    newTitle: handleName,
                    dtHtml: handleName + topics,
                    triggerBtn: LKAssemblySubjectImages.ElemAssembly.Other.提交按钮
                });
                //#endregion

                $("#FormUpload").ajaxSubmit({
                    target: null,
                    beforeSubmit: null,
                    dataType: 'json',
                    resetForm:true,
                    success: function (fData) {
                        /* 数据修改成功 */
                        if (fData.result) {

                            var oInfo = fData.info || {};
                            var userName = oInfo.userName;
                            var imgName = oInfo.imgName;
                            CSharpJS.SaveSuccess(userName, imgName);
                            Global.Boxy.hide(boxyKey);
                            return;
                            Global.Boxy.success(boxyKey, {
                                newTitle: handleName + "成功",
                                dtHtml: topics + "已" + handleName + "成功.",
                                boxyOptions: {
                                    beforeHide: function () {
                                        handle.embedBoxyHide();
                                    },
                                    afterHide: function () {
                                        handle.completed();
                                    }
                                }
                            });
                        }
                        /* 数据修改失败 */
                        else {

                            handle.embedBoxyBtn(true);

                            Global.Boxy.failure(boxyKey, {
                                newTitle: handleName + "失败",
                                dtHtml: topics + handleName + "失败",
                                newMessage: fData.info
                            });
                        }
                    }
                });
            },
            //#endregion

            //#region submit：提交数据
            submit: function () {
                Global.MsgBox.submit(this, ["登录系统", "图片文件"]);
            }
            //#endregion
        };
    </script>
</head>
<body>
    <div class="qnImagesBox">
        <div class="qnImagesBoxContent">
            <div class="qnImagesBoxContentLi" id="QnImagesBoxContentTab1" style="display: block">
                <div class="qnImagesBoxContentLiTipMsg">
                    请选择新的图片文件，文件需小于3MB。支持格式(jpg，jpeg，bmp，gif, png)</div>
                <div class="qnImagesBoxContentLiUpload">
                    <form id="FormUpload" onsubmit="LKAssemblySubjectImages.submit();return false;" action="/LKImages/AddSubjectImages/" method="post" enctype="multipart/form-data">
                    <input id="图片文件" name="InputFile" type="file" class="textbg inputUpload" onchange="ListImages.SetSubmit($(this));"
                        mbleft="410" mbtop="-10" />
                    <input id="提交按钮" type="submit" value="上 传" disabled="disabled" class="inputButton" />
                    </form>
                    <div id="output1">
                    </div>
                </div>
                <div class="qnImgLoading" id="QnImgLoading">
                    <div class="qnImgLoadingBg">
                    </div>
                    <span class="qnImgLoadingText">正在上传图片，请稍等......</span>
                </div>
            </div>
            <div class="qnImagesBoxContentLi" id="QnImagesBoxContentTab0">
                <div class="qnImagesBoxContentTab0List" id="QnImagesBoxContentTab0List">
                </div>
                <div class="qnImagesBoxContentTab0PagerLine">
                </div>
                <div class="qnImagesBoxContentTab0Pager" id="QnImagesBoxContentTab0Pager">
                </div>
            </div>
            <div class="qnImagesBoxContentLi" id="QnImagesBoxContentTab2">
                <div class="qnImagesBoxContentBigImg">
                    <a href="javascript:void(0)" id="showBigPic"></a>
                </div>
                <div class="qnImagesBoxContentBigImgPage">
                </div>
            </div>
        </div>
    </div>
    <%=Html.ScriptText("LKAssemblySubjectImages.initData();")%>
</body>
</html>
