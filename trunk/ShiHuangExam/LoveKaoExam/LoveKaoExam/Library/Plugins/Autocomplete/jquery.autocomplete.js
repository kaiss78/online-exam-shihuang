(function(a){a.fn.extend({autocomplete:function(c,b){var d=typeof c=="string";b=a.extend({},a.Autocompleter.defaults,{url:d?c:null,data:d?null:c,delay:d?a.Autocompleter.defaults.delay:10,max:b&&!b.scroll?10:150},b);b.highlight=b.highlight||function(a){return a};b.formatMatch=b.formatMatch||b.formatItem;return this.each(function(){new a.Autocompleter(this,b)})},result:function(a){return this.bind("result",a)},search:function(a){return this.trigger("search",[a])},flushCache:function(){return this.trigger("flushCache")},setOptions:function(a){return this.trigger("setOptions",[a])},unautocomplete:function(){return this.trigger("unautocomplete")}});a.Autocompleter=function(f,b){var e={UP:38,DOWN:40,DEL:46,TAB:9,RETURN:13,ESC:27,COMMA:188,PAGEUP:33,PAGEDOWN:34,BACKSPACE:8},d=a(f).attr("autocomplete","off"),k,g="",n=a.Autocompleter.Cache(b),j=0,o,t={mouseDownOnSelect:false},c=a.Autocompleter.Select(b,f,r,t),p;a.browser.opera&&a(f.form).bind("submit.autocomplete",function(){if(p){p=false;return false}});d.bind((a.browser.opera?"keypress":"keydown")+".autocomplete",function(d){j=1;o=d.keyCode;switch(d.keyCode){case e.UP:d.preventDefault();if(c.visible())c.prev();else i(0,true);break;case e.DOWN:d.preventDefault();if(c.visible())c.next();else i(0,true);break;case e.PAGEUP:d.preventDefault();if(c.visible())c.pageUp();else i(0,true);break;case e.PAGEDOWN:d.preventDefault();if(c.visible())c.pageDown();else i(0,true);break;case b.multiple&&a.trim(b.multipleSeparator)==","&&e.COMMA:case e.TAB:case e.RETURN:if(r()){d.preventDefault();p=true;return false}break;case e.ESC:c.hide();break;default:clearTimeout(k);k=setTimeout(i,b.delay)}}).focus(function(){j++}).blur(function(){j=0;!t.mouseDownOnSelect&&u()}).click(function(){j++>1&&!c.visible()&&i(0,true)}).bind("search",function(){var c=arguments.length>1?arguments[1]:null;function b(f,b){var a;if(b&&b.length)for(var e=0;e<b.length;e++)if(b[e].result.toLowerCase()==f.toLowerCase()){a=b[e];break}if(typeof c=="function")c(a);else d.trigger("result",a&&[a.data,a.value])}a.each(h(d.val()),function(c,a){s(a,b,b)})}).bind("flushCache",function(){n.flush()}).bind("setOptions",function(){a.extend(b,arguments[1]);"data"in arguments[1]&&n.populate()}).bind("unautocomplete",function(){c.unbind();d.unbind();a(f.form).unbind(".autocomplete")});function r(){var i=c.selected();if(!i)return false;var e=i.result;g=e;if(b.multiple){var j=h(d.val());if(j.length>1){var n=b.multipleSeparator.length,o=a(f).selection().start,m,k=0;a.each(j,function(b,a){k+=a.length;if(o<=k){m=b;return false}k+=n});j[m]=e;e=j.join(b.multipleSeparator)}e+=b.multipleSeparator}b.afterChange(e);d.val(e);l();d.trigger("result",[i.data,i.value]);return true}function i(h,f){if(o==e.DEL){c.hide();return}var a=d.val();if(!f&&a==g)return;g=a;a=m(a);if(a.length>=b.minChars){if(!b.matchCase)a=a.toLowerCase();s(a,v,l)}else{b.afterSTData("",[],true);q();c.hide()}}function h(c){return!c?[""]:!b.multiple?[a.trim(c)]:a.map(c.split(b.multipleSeparator),function(b){return a.trim(c).length?a.trim(b):null})}function m(c){if(!b.multiple)return c;var d=h(c);if(d.length==1)return d[0];var e=a(f).selection().start;if(e==c.length)d=h(c);else d=h(c.replace(c.substring(e),""));return d[d.length-1]}function w(h,c){if(b.autoFill&&m(d.val()).toLowerCase()==h.toLowerCase()&&o!=e.BACKSPACE){d.val(d.val()+c.substring(m(g).length));a(f).selection(g.length,g.length+c.length)}}function u(){clearTimeout(k);k=setTimeout(l,200)}function l(){var a=c.visible();c.hide();clearTimeout(k);q();b.mustMatch&&d.search(function(c){if(!c)if(b.multiple){var a=h(d.val()).slice(0,-1);d.val(a.join(b.multipleSeparator)+(a.length?b.multipleSeparator:""))}else{d.val("");d.trigger("result",null)}})}function v(d,a,f,e){b.afterSTData(d,f||[],e);if(a&&a.length&&j){q();c.display(a,d);w(d,a[0].value);c.show()}else l()}function s(d,h,j){if(!b.matchCase)d=d.toLowerCase();var i=[],e=n.load(d,function(a){i=a});if(e&&e.length)h(d,e,i,true);else if(typeof b.url=="string"&&b.url.length>0){var g={timestamp:+new Date};a.each(b.extraParams,function(b,a){g[b]=typeof a=="function"?a():a});a.ajax({mode:"abort",port:"autocomplete"+f.name,type:"POST",dataType:"json",url:b.url,data:a.extend({q:m(d),limit:b.searchMax,action:"分类管理_分类名查询相似分类"},g),cache:true,success:function(g){if(g.result){for(var a=g.info||[],e=[],c=0;c<a.length;c++)if(c==0)a[c].类型!=""&&e.push(a[c].分类名);else e.push(a[c].分类名);var f=b.parse&&b.parse(e)||x(e);n.add(d,f,a);h(d,f,a,false)}},error:function(c,b,a){var d=a}})}else{c.emptyList();j(d)}}function x(g){for(var d=[],f=g,e=0;e<f.length;e++){var c=a.trim(f[e]);if(c){c=c.split("|");d[d.length]={data:c,value:c[0],result:b.formatResult&&b.formatResult(c,c[0])||c[0]}}}return d}function q(){}};a.Autocompleter.defaults={inputClass:"ac_input",resultsClass:"ac_results",loadingClass:"ac_loading",minChars:1,delay:250,matchCase:false,matchSubset:true,matchContains:false,cacheLength:10,max:20,searchMax:20,mustMatch:false,extraParams:{},selectFirst:false,formatItem:function(a){return a[0]},formatMatch:null,autoFill:false,width:0,multiple:false,multipleSeparator:", ",highlight:function(a){return a},scroll:true,scrollHeight:180,afterSTData:function(){},afterChange:function(){}};a.Autocompleter.Cache=function(b){var c={},g=[],d=0;function e(a,d){if(!b.matchCase)a=a.toLowerCase();var c=a.indexOf(d);if(b.matchContains=="word")c=a.toLowerCase().search("\\b"+d.toLowerCase());return c==-1?false:c==0||b.matchContains}function i(a,f,e){d>b.cacheLength&&h();if(!c[a])d++;c[a]=f;g=e||[]}function f(){if(!b.data)return false;var d={},j=0;if(!b.url)b.cacheLength=1;d[""]=[];for(var f=0,k=b.data.length;f<k;f++){var c=b.data[f];c=typeof c=="string"?[c]:c;var e=b.formatMatch(c,f+1,b.data.length);if(e===false)continue;var g=e.charAt(0).toLowerCase();if(!d[g])d[g]=[];var h={value:e,data:c,result:b.formatResult&&b.formatResult(c)||e};d[g].push(h);j++<b.max&&d[""].push(h)}a.each(d,function(c,a){b.cacheLength++;i(c,a)})}setTimeout(f,25);function h(){c={};d=0}return{flush:h,add:i,populate:f,load:function(f,k){k&&k(g);if(!b.cacheLength||!d)return null;if(!b.url&&b.matchContains){var h=[];for(var l in c)if(l.length>0){var i=c[l];a.each(i,function(b,a){e(a.value,f)&&h.push(a)})}return h}else if(c[f])return c[f];else if(b.matchSubset)for(var j=f.length-1;j>=b.minChars;j--){var i=c[f.substr(0,j)];if(i){var h=[];a.each(i,function(b,a){if(e(a.value,f))h[h.length]=a});return h}}return null}}};a.Autocompleter.Select=function(d,k,r,n){var f={ACTIVE:"ac_over"},b,c=-1,i,l="",m=true,g,e;function s(){if(!m)return;g=a("<div/>").hide().addClass(d.resultsClass).css("position","absolute").appendTo(document.body);e=a("<ul/>").appendTo(g).mouseover(function(b){if(j(b).nodeName&&j(b).nodeName.toUpperCase()=="LI"){c=a("li",e).removeClass(f.ACTIVE).index(j(b));a(j(b)).addClass(f.ACTIVE)}}).click(function(b){a(j(b)).addClass(f.ACTIVE);r();k.focus();return false}).mousedown(function(){n.mouseDownOnSelect=true}).mouseup(function(){n.mouseDownOnSelect=false});d.width>0&&g.css("width",d.width);m=false}function j(b){var a=b.target;while(a&&a.tagName!="LI")a=a.parentNode;return!a?[]:a}function h(h){b.slice(c,c+1).removeClass(f.ACTIVE);p(h);var g=b.slice(c,c+1).addClass(f.ACTIVE);if(d.scroll){var a=0;b.slice(0,c).each(function(){a+=this.offsetHeight});if(a+g[0].offsetHeight-e.scrollTop()>e[0].clientHeight)e.scrollTop(a+g[0].offsetHeight-e.innerHeight());else a<e.scrollTop()&&e.scrollTop(a)}}function p(a){c+=a;if(c<0)c=b.size()-1;else if(c>=b.size())c=0}function o(a){return d.max&&d.max<a?d.max:a}function q(){e.empty();for(var j=o(i.length),g=0;g<j;g++){if(!i[g])continue;var h=d.formatItem(i[g].data,g+1,j,i[g].value,l);if(h===false)continue;var k=a("<li/>").html(d.highlight(h,l)).addClass(g%2==0?"ac_even":"ac_odd").appendTo(e)[0];a.data(k,"ac_data",i[g])}b=e.find("li");if(d.selectFirst){b.slice(0,1).addClass(f.ACTIVE);c=0}a.fn.bgiframe&&e.bgiframe()}return{display:function(a,b){s();i=a;l=b;q()},next:function(){h(1)},prev:function(){h(-1)},pageUp:function(){if(c!=0&&c-8<0)h(-c);else h(-8)},pageDown:function(){if(c!=b.size()-1&&c+8>b.size())h(b.size()-1-c);else h(8)},hide:function(){g&&g.hide();b&&b.removeClass(f.ACTIVE);c=-1},visible:function(){return g&&g.is(":visible")},current:function(){return this.visible()&&(b.filter("."+f.ACTIVE)[0]||d.selectFirst&&b[0])},show:function(){var h=a(k).offset();g.css({width:typeof d.width=="string"||d.width>0?d.width+1:a(k).width()+1,top:h.top+k.offsetHeight,left:h.left}).show();if(d.scroll){e.scrollTop(0);e.css({maxHeight:d.scrollHeight,overflow:"auto"});if(a.browser.msie&&typeof document.body.style.maxHeight==="undefined"){var c=0;b.each(function(){c+=this.offsetHeight});var f=c>d.scrollHeight;e.css("height",f?d.scrollHeight:c);!f&&b.width(e.width()-parseInt(b.css("padding-left"))-parseInt(b.css("padding-right")))}}},selected:function(){var c=b&&b.filter("."+f.ACTIVE).removeClass(f.ACTIVE);return c&&c.length&&a.data(c[0],"ac_data")},emptyList:function(){e&&e.empty()},unbind:function(){g&&g.remove()}}};a.fn.selection=function(b,c){if(b!==undefined)return this.each(function(){if(this.createTextRange){var a=this.createTextRange();if(c===undefined||b==c){a.move("character",b);a.select()}else{a.collapse(true);a.moveStart("character",b);a.moveEnd("character",c);a.select()}}else if(this.setSelectionRange)this.setSelectionRange(b,c);else if(this.selectionStart){this.selectionStart=b;this.selectionEnd=c}});var a=this[0];if(a.createTextRange){var g=document.selection.createRange(),h=a.value,e="<->",f=g.text.length;g.text=e;var d=a.value.indexOf(e);a.value=h;this.selection(d,d+f);return{start:d,end:d+f}}else if(a.selectionStart!==undefined)return{start:a.selectionStart,end:a.selectionEnd}}})(jQuery)