$(document).ready(function(){ChildQueTypesManage.init()});var ChildQueTypesManage={selectMark:0,buttonSubmit:0,contentBox:0,chsHeader:0,chsContent:0,markList:{T40:["11","12","20","13","30","14","15","60"],T41:["11","12","20","13","60"],T42:["11","12","20","13","60"],T43:["11","12","20","13","60"],T44:["11","12","20","13","60"],T45:["11","12","20","13","60"],T46:["11","12","20","13","60"],T47:["11","12","20","13","60"],T80:["8011"]},init:function(){this.buttonSubmit=$("#ID_ChildButton_Box");this.contentBox=$("#ID_ChildContent_Box");this.chsHeader=$("#ID_ChildHeader_Box").children("a");this.chsContent=this.contentBox.children("div")},showButton:function(){this.buttonSubmit.show()},hideButton:function(){this.buttonSubmit.hide()},switchTags:function(a){this.chsHeader.each(function(b){b==a?$(this).addClass("TagsSelect").blur():$(this).removeClass("TagsSelect")});this.chsContent.each(function(b){$(this)[b==a?"show":"hide"]()})},select:function(b){var a=$(b).attr("mark");if(this.checkItem(a))return;this.selectMark=a;this.update();this.showButton()},getOptions:function(){return this.contentBox.find("a")},update:function(){var b=this.selectMark,c=this.getOptions(),a;c.each(function(){a=$(this);a.attr("mark")==b?a.addClass("OptionSelect"):a.removeClass("OptionSelect")})},checkItem:function(a){return a==this.selectMark?true:false},T11:function(){return'<a href="javascript:void(0)" mark="11" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn"  style="width:40px; margin-left:5px;">单选题</a>'},T12:function(){return'<a href="javascript:void(0)" mark="12" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn"  style="width:40px; margin-left:15px;" >多选题</a>'},T13:function(){return'<a href="javascript:void(0)" mark="13" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn"  style="width:40px; margin-left:15px;">填空题</a>'},T14:function(){return'<a href="javascript:void(0)" mark="14" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn"  style="width:50px; margin-left:15px;" >选词填空</a>'},T15:function(){return'<a href="javascript:void(0)" mark="15" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn"  style="width:50px; margin-left:5px;" >完型填空</a>'},T20:function(){return'<a href="javascript:void(0)" mark="20" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn"  style="width:40px; margin-left:15px;">判断题</a>'},T30:function(){return'<a href="javascript:void(0)" mark="30" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn"  style="width:40px; margin-left:5px;" >连线题</a>'},T40:function(){return'<a href="javascript:void(0)" mark="40" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn OptionSelectUnF" style="width:50px; margin-left:5px;">复合题型</a>'},T60:function(){return'<a href="javascript:void(0)" mark="60" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn" style="width:40px; margin-left:5px;">问答题</a><a href="javascript:void(0)" mark="61" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn" style="width:40px; margin-left:15px;">简答题</a><a href="javascript:void(0)" mark="62" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn" style="width:40px; margin-left:15px;">论述题</a><a href="javascript:void(0)" mark="63" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn" style="width:40px; margin-left:15px;">翻译题</a><a href="javascript:void(0)" mark="64" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn" style="width:40px; margin-left:5px;">计算题</a><a href="javascript:void(0)" mark="65" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn" style="width:40px; margin-left:15px;">作文题</a><a href="javascript:void(0)" mark="66" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn" style="width:40px; margin-left:15px;">案例题</a><a href="javascript:void(0)" mark="67" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn" style="width:40px; margin-left:15px;">材料题</a><a href="javascript:void(0)" mark="68" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn" style="width:40px; margin-left:5px;">推理题</a><a href="javascript:void(0)" mark="69" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn" style="width:50px; margin-left:15px;">名词解释</a>'},T8011:function(){return'<a href="javascript:void(0)" mark="8011" onclick="ChildQueTypesManage.select(this);" class="OptionSelectUn"  style="width:40px; margin-left:5px;">单选题</a>'},send:function(){var a=parent.MulitpleSubject,b=this.selectMark;if(b==0)return;a.minimizeQueTypes();a.receiveQueTypes(b)},receive:function(a){this.hideButton();this.setContent(a);if(this.selectMark!=0){this.selectMark=0;this.update()}},getContent:function(e){for(var d=this.markList["T"+e],b,a={div0:{isShow:false,html:""},div1:{isShow:false,html:""}},c=0;c<d.length;c++){b=d[c];if(b=="60"){a.div1.isShow=true;a.div1.html+=this.T60()}else{a.div0.isShow=true;a.div0.html+=this["T"+b]()}}return a},setContent:function(e){var c=this.getContent(e),f=c.div0,b=c.div1,a=this.chsContent.eq(0),d=this.chsContent.eq(1);this.chsContent.hide();a.show();this.chsHeader.removeClass("TagsSelect").eq(0).addClass("TagsSelect").show();b.isShow?this.chsHeader.eq(1).show():this.chsHeader.eq(1).hide();a.html(f.html);d.html(b.html)},minimize:function(){parent.MulitpleSubject.minimizeQueTypes()},maximize:function(){}}