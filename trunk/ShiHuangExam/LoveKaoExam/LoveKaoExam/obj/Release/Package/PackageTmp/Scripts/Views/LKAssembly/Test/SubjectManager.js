var IframeSubjectTypeManage={curObj:0,init:function(){},switchTags:function(c,e,d,a){a=a||"TagsSelect";var b=$(c).parent().children();b.each(function(){$(this).removeClass(a)});$(c).addClass(a).blur();var b=$("#"+e).children();b.each(function(a){if(a==d)$(this).show();else $(this).hide()})},data:[],add:function(b){var a=$(b).text();this.send(a)},send:function(a){parent.FatherIframeSubManage.receive(a)}}