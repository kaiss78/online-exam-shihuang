<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px; /*background: #f5f5f5;*/
            background-color: #f5f5f5;
        }
        .fckEdiotr
        {
            width: 630px;
            height: 310px;
        }
        .fckEdiotrBox
        {
            width: 620px;
            height: 305px;
            float: left;
            margin: 5px 4px 0px 6px;
        }
        *html .fckEdiotrBox
        {
            margin: 5px 2px 0px 3px;
        }
    </style>
    <script src="../../../Fckeditor/fckeditor.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        //编辑器FckEditor加载完成
        function FCKeditor_OnComplete(instance) {
            FckEditorManage.initInst();

            instance.Events.AttachEvent('OnAfterSetHTML', FckEditorManage.afterSetHtml);
            //instance.Events.AttachEvent('OnAfterGetData', FckEditorManage.afterGetHtml);
            //instance.Events.AttachEvent('OnBlur', FckEditorManage.afterBlur);
            //instance.Events.AttachEvent('OnFocus', FckEditorManage.afterFocus);

            FckEditorManage.initParent();
            parent.PageManage.complete();
        }

        function getMenuItem() {
            return BaseManage.isPass() && SpaceManage.isselect;
        }

        var BaseManage = {
            settings: { types: "",
                mark: 0
            },
            isPass: function () {
                var _set = this.settings,
                    _mark = _set.mark;
                return (_mark == "13" || _mark == "14" || _mark == "15") && _set.types == "Title";
            }
        };

        //编辑器管理
        var FckEditorManage = {
            fckInstance: 0,
            fckCharacter: 0,
            parentManage: 0,
            isparent: false,
            setFocusDiv: 0,
            isResetDirty: true,

            //初始化编辑器FckEditor实例
            initInst: function () {
                this.fckInstance = FCKeditorAPI.GetInstance("FckEditorTextArea");
                this.setFocusDiv = document.getElementById("setFocus");
            },

            //初始化父窗体
            initParent: function () {
                this.parentManage = parent.IFrameFckEditor;
                this.isparent = true;
            },

            //返回编辑器FckEditor当前HTML
            getHtml: function () {
                return this.fckInstance.GetXHTML(true);
            },

            getInnerHtml: function () {
                return this.fckInstance.EditorDocument.body.innerHTML;
            },

            //返回编辑器FckEditor当前TEXT
            getText: function () {
                return this.fckInstance.EditorDocument.body.innerText;
            },

            //返回编辑器FckEditor当前HTML正则后的TEXT
            getTextRep: function () {
                var _html = this.getHtml();
                return _html.replace(/<.+?>/gim, "");
            },


            //设置编辑器FckEditor当前的HTML
            setHtml: function (html) {
                this.isResetDirty = true;
                this.fckInstance.SetHTML(html);
            },

            renSetHtml: function (html) {
                this.isResetDirty = false;
                this.fckInstance.SetHTML(html);
            },


            //回调设置编辑器FckEditorHTML后的方法
            afterSetHtml: function () {
                var _parentManage = FckEditorManage.parentManage;

                //设置基本信息
                BaseManage.settings = _parentManage.settings;

                //设置焦点
                FckEditorManage.focus();

                //重置Dirty
                FckEditorManage.ResetDirty();

                //内容设置完成
                _parentManage.afterSet = true;
            },

            //回调获取编辑器FckEditorHTML后的方法
            afterGetHtml: function () {

            },

            //插入编辑器FckEditor当前光标位置的HTML
            insertHtml: function (html, fn) {
                if (this.fckInstance.EditMode == FCK_EDITMODE_WYSIWYG) {
                    this.fckInstance.InsertHtml(html);
                    fn && fn(true);
                }
                else {
                    fn && fn(false);
                }

                //!SpaceManage.isselect(2010-06-12 15-00原因:选择空格图片再插入html则覆盖)
                SpaceManage.isselect && BaseManage.isPass() && SpaceManage.exeAfterDelete();
            },

            //返回当前html是否发生变化
            getDirty: function () {
                return this.fckInstance.IsDirty();
            },

            //设置FckEditor的HTML重置Html是否更改值
            ResetDirty: function () {
                this.isResetDirty && this.fckInstance.ResetIsDirty();
            },

            //设置编辑器FckEditor的获取焦点
            focus: function () {
                this.blur();
                this.fckInstance.Focus();
                this.accesskey();
            },

            //设置编辑器FckEditor的失去焦点
            blur: function () {
                this.setFocusDiv && this.setFocusDiv.focus();
            },

            //设置编辑器FckEditor文本后的光标
            focusLast: function () {
                var _editingArea = this.fckInstance.EditingArea;
                _editingArea.Document.body.setActive();
                _editingArea.Window.focus();
                var _range = _editingArea.Document.selection.createRange();
                _range.moveStart("character", 0);
                _range.collapse(true);
                _range.select();
            },


            //键盘按下事件
            bindKeyDown: function (doc, isIE) {
                //isIE ? doc.attachEvent('onkeydown', FckEditorManage.keyDown) : doc.addEventListener("keydown", FckEditorManage.keyDown, false);
                isIE && doc.attachEvent('onkeydown', FckEditorManage.keyDown);
            },

            //编辑器FckEditor软键盘
            accesskey: function () {
                var _documenet = this.fckInstance.EditorDocument;
                var _isIE = document.all ? true : false;

                //键盘按下事件
                this.bindKeyDown(_documenet, _isIE);

                //允许绑定空格事件
                if (BaseManage.isPass()) {
                    SpaceManage.bindKeyUp(_documenet, _isIE);
                    SpaceManage.bindMouseUp(_documenet, _isIE);
                    SpaceManage.bindDrop(_documenet, _isIE);
                }
            },

            //按下键盘后触发keyDown事件
            keyDown: function (event) {
                var _code = event.keyCode || event.which;
                //Ctrl+Enter
                if (event.ctrlKey && _code == 13) {
                    FckEditorManage.blur();
                    parent.IFrameFckEditor.handleKey(13);
                    return true;
                }
                //Tab
                else if (_code == 9) {
                    FckEditorManage.blur();
                    parent.IFrameFckEditor.navKey();
                    return false;
                }
                //空格管理键盘按下事件
                else {
                    BaseManage.isPass() && SpaceManage.exeKeyDown(event, _code);
                }
            }
        };

        var SpaceManage = {

            isselect: false,

            //键盘放开事件
            bindKeyUp: function (doc, isIE) {
                isIE ? doc.attachEvent('onkeyup', SpaceManage.exeKeyUp) : doc.addEventListener("keyup", SpaceManage.exeKeyUp, false);
            },

            //鼠标放开事件
            bindMouseUp: function (doc, isIE) {
                isIE ? doc.attachEvent('onmouseup', SpaceManage.exeMouseUp) : doc.addEventListener("mouseup", SpaceManage.exeMouseUp, false);
            },

            //拖动事件
            bindDrop: function (doc, isIE) {
                isIE ? doc.body.attachEvent('ondrop', SpaceManage.exeBeforeDrop) : doc.body.addEventListener('drop', SpaceManage.exeBeforeDrop, false);
            },

            //返回是否一选中空格
            checkSelect: function () {
                if (document.all) {
                    var _htmlText = FckEditorManage.fckInstance.EditorDocument.selection.createRange().htmlText;
                    if (_htmlText == undefined) {
                        SpaceManage.isselect = true;
                    }
                    else if (_htmlText.indexOf("<IMG class=UnderLine" + BaseManage.settings.mark) != -1) {
                        SpaceManage.isselect = true;
                    }
                    else {
                        SpaceManage.isselect = false;
                    }
                }
                else {
                    //FckEditorManage.fckInstance.EditorWindow.getSelection().focusNode.nextSibling;
                }
            },

            //执行键盘按下事件
            exeKeyDown: function (event, code) {
                SpaceManage.checkSelect();
                //ctrl
                if (event.ctrlKey) {
                    switch (code) {
                        //x                                                            
                        case 88:
                            if (SpaceManage.isselect) { event.returnValue = false; }
                            break;
                        //V                                                            
                        case 86:
                            if (SpaceManage.isselect) { event.returnValue = false; }
                            break;
                        //A                                        
                        case 65:
                            event.returnValue = false;
                            break;
                    }
                }
                //shift
                else if (event.shiftKey) {
                    switch (code) {
                        //insert                                                      
                        case 45:
                            if (SpaceManage.isselect) { event.returnValue = false; }
                            break;
                    }
                }
                else if (SpaceManage.isselect) {
                    var _code = event.keyCode || event.which;
                    //BackSpace/Delete/Enter/Space
                    if (_code != 8 && _code != 46 && _code != 13 && _code != 32) {
                        event.returnValue = false;
                    }
                }
            },

            //执行事件放开事件
            exeKeyUp: function (event) {
                var _code = event.keyCode || event.which;
                //BackSpace/Delete
                if (_code == 8 || _code == 46) {
                    SpaceManage.exeAfterDelete();
                }
                //Enter/Space
                else if (_code == 13 || _code == 32) {
                    SpaceManage.exeAfterDelete(); //删除SpaceManage.isselect(2010-06-12 15-00 原因键盘按下一直删 则isselect为false)
                    //SpaceManage.isselect && SpaceManage.exeAfterDelete();
                }
                //ctrl+Z
                else if (event.ctrlKey && _code == 90) {
                    SpaceManage.exeAfterDelete();
                }
                //shift+上/下/左/右
                else if (event.shiftKey && (_code == 37 || _code == 38 || _code == 39 || _code == 40)) {
                    SpaceManage.checkSelect();
                }

            },

            //执行鼠标放开事件
            exeMouseUp: function () {
                SpaceManage.checkSelect();
            },

            //执行拖动图片之前事件
            exeBeforeDrop: function (event) {
                event.returnValue = false;
            },

            //执行Enter/Delete....删除后的事件
            exeAfterDelete: function () {
                var _set = BaseManage.settings;
                parent.SpaceGroup.finishDel(_set.guidTeam, _set.mark, FckEditorManage.getInnerHtml(), function (html) {
                    FckEditorManage.renSetHtml(html);
                });
                this.isselect = false;
            }
        };
        
    </script>

</head>
<body>
    <div class="fckEdiotr">
        <div class="fckEdiotrBox" id="setFocus">
            <textarea id="FckEditorTextArea"></textarea>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            var sBasePath = "/fckeditor/";
            var oFCKeditor = new FCKeditor("FckEditorTextArea");
            oFCKeditor.Height = "300px";
            oFCKeditor.BasePath = sBasePath;
            oFCKeditor.ReplaceTextarea();
        }
    </script>
</body>
</html>
