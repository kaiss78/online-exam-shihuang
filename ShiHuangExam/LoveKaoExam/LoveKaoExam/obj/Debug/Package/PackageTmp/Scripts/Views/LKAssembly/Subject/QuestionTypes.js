$(document).ready(function(){QuestionTypesManage.init()});var QuestionTypesManage={selectMark:0,chsOptions:0,buttonSubmit:0,contentBox:0,chsHeader:0,chsContent:0,init:function(){this.buttonSubmit=$("#ID_ButtonSubmit_Box");this.contentBox=$("#ID_Content_Box");this.chsHeader=$("#ID_Header_Box").children("a");this.chsContent=this.contentBox.children("div")},switchTags:function(a){this.chsHeader.each(function(b){b==a?$(this).addClass("TagsSelect").blur():$(this).removeClass("TagsSelect")});this.chsContent.each(function(b){$(this)[b==a?"show":"hide"]()})},showButton:function(){this.buttonSubmit.show()},hideButton:function(){this.buttonSubmit.hide()},select:function(b){var a=$(b).attr("mark");if(this.checkItem(a))return;this.selectMark=a;this.update();this.showButton()},initOptions:function(){this.chsOptions=this.chsOptions||this.contentBox.find("a")},update:function(){this.initOptions();var b=this.selectMark,a;this.chsOptions.each(function(){a=$(this);a.attr("mark")==b?a.addClass("OptionSelect"):a.removeClass("OptionSelect")})},checkItem:function(a){return a==this.selectMark?true:false},fn:function(){},fn_ok:function(){},fn_cancel:function(){},send:function(){if(this.selectMark==0)return;this.minimize();this.fn_ok&&this.fn_ok(this.selectMark)},receive:function(b,a){this.fn_ok=b||this.fn;this.fn_cancel=a||this.fn;this.hideButton();if(this.selectMark!=0){this.selectMark=0;this.update()}},minimize:function(){this.fn_cancel&&this.fn_cancel()},maximize:function(){}}