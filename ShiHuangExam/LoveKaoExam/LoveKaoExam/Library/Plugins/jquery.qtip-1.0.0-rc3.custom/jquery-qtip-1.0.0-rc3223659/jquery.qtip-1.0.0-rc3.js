/*
 * jquery.qtip. The jQuery tooltip plugin
 *
 * Copyright (c) 2009 Craig Thompson
 * http://craigsworks.com
 *
 * Licensed under MIT
 * http://www.opensource.org/licenses/mit-license.php
 *
 * Launch  : February 2009
 * Version : 1.0.0-rc3
 * Released: Tuesday 12th May, 2009 - 00:00
 * Debug: jquery.qtip.debug.js
 */
(function(a){a.fn.qtip=function(b,n){var e,l,f,i,m,h,c,k;if(typeof b=="string"){typeof a(this).data("qtip")!=="object"&&a.fn.qtip.log.error.call(self,1,a.fn.qtip.constants.NO_TOOLTIP_PRESENT,false);if(b=="api")return a(this).data("qtip").interfaces[a(this).data("qtip").current];else if(b=="interfaces")return a(this).data("qtip").interfaces}else{if(!b)b={};if(typeof b.content!=="object"||b.content.jquery&&b.content.length>0)b.content={text:b.content};if(typeof b.content.title!=="object")b.content.title={text:b.content.title};if(typeof b.position!=="object")b.position={corner:b.position};if(typeof b.position.corner!=="object")b.position.corner={target:b.position.corner,tooltip:b.position.corner};if(typeof b.show!=="object")b.show={when:b.show};if(typeof b.show.when!=="object")b.show.when={event:b.show.when};if(typeof b.show.effect!=="object")b.show.effect={type:b.show.effect};if(typeof b.hide!=="object")b.hide={when:b.hide};if(typeof b.hide.when!=="object")b.hide.when={event:b.hide.when};if(typeof b.hide.effect!=="object")b.hide.effect={type:b.hide.effect};if(typeof b.style!=="object")b.style={name:b.style};b.style=d(b.style);i=a.extend(true,{},a.fn.qtip.defaults,b);i.style=g.call({options:i},i.style);i.user=a.extend(true,{},b)}return a(this).each(function(){if(typeof b=="string"){h=b.toLowerCase();f=a(this).qtip("interfaces");if(typeof f=="object")if(n===true&&h=="destroy")while(f.length>0)f[f.length-1].destroy();else{if(n!==true)f=[a(this).qtip("api")];for(e=0;e<f.length;e++)if(h=="destroy")f[e].destroy();else if(f[e].status.rendered===true)if(h=="show")f[e].show();else if(h=="hide")f[e].hide();else if(h=="focus")f[e].focus();else if(h=="disable")f[e].disable(true);else h=="enable"&&f[e].disable(false)}}else{c=a.extend(true,{},i);c.hide.effect.length=i.hide.effect.length;c.show.effect.length=i.show.effect.length;if(c.position.container===false)c.position.container=a(document.body);if(c.position.target===false)c.position.target=a(this);if(c.show.when.target===false)c.show.when.target=a(this);if(c.hide.when.target===false)c.hide.when.target=a(this);l=a.fn.qtip.interfaces.length;for(e=0;e<l;e++)if(typeof a.fn.qtip.interfaces[e]=="undefined"){l=e;break}m=new r(a(this),c,l);a.fn.qtip.interfaces[l]=m;if(typeof a(this).data("qtip")=="object"){if(typeof a(this).attr("qtip")==="undefined")a(this).data("qtip").current=a(this).data("qtip").interfaces.length;a(this).data("qtip").interfaces.push(m)}else a(this).data("qtip",{current:0,interfaces:[m]});if(c.content.prerender===false&&c.show.when.event!==false&&c.show.ready!==true)c.show.when.target.bind(c.show.when.event+".qtip-"+l+"-create",{qtip:l},function(b){k=a.fn.qtip.interfaces[b.data.qtip];k.options.show.when.target.unbind(k.options.show.when.event+".qtip-"+b.data.qtip+"-create");k.cache.mouse={x:b.pageX,y:b.pageY};j.call(k);k.options.show.when.target.trigger(k.options.show.when.event)});else{m.cache.mouse={x:c.show.when.target.offset().left,y:c.show.when.target.offset().top};j.call(m)}}})};function r(j,i,k){var d=this;d.id=k;d.options=i;d.status={animated:false,rendered:false,disabled:false,focused:false};d.elements={target:j.addClass(d.options.style.classes.target),tooltip:null,wrapper:null,content:null,contentWrapper:null,title:null,button:null,tip:null,bgiframe:null};d.cache={mouse:{},position:{},toggle:0};d.timers={};a.extend(d,d.options.api,{show:function(c){var f,e;if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"show");if(d.elements.tooltip.css("display")!=="none")return d;d.elements.tooltip.stop(true,false);f=d.beforeShow.call(d,c);if(f===false)return d;function b(){d.options.position.type!=="static"&&d.focus();d.onShow.call(d,c);a.browser.msie&&d.elements.tooltip.get(0).style.removeAttribute("filter")}d.cache.toggle=1;d.options.position.type!=="static"&&d.updatePosition(c,d.options.show.effect.length>0);if(typeof d.options.show.solo=="object")e=a(d.options.show.solo);else if(d.options.show.solo===true)e=a("div.qtip").not(d.elements.tooltip);e&&e.each(function(){a(this).qtip("api").status.rendered===true&&a(this).qtip("api").hide()});if(typeof d.options.show.effect.type=="function"){d.options.show.effect.type.call(d.elements.tooltip,d.options.show.effect.length);d.elements.tooltip.queue(function(){b();a(this).dequeue()})}else{switch(d.options.show.effect.type.toLowerCase()){case"fade":d.elements.tooltip.fadeIn(d.options.show.effect.length,b);break;case"slide":d.elements.tooltip.slideDown(d.options.show.effect.length,function(){b();d.options.position.type!=="static"&&d.updatePosition(c,true)});break;case"grow":d.elements.tooltip.show(d.options.show.effect.length,b);break;default:d.elements.tooltip.show(null,b)}d.elements.tooltip.addClass(d.options.style.classes.active)}return a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_SHOWN,"show")},hide:function(e){var c;if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"hide");else if(d.elements.tooltip.css("display")==="none")return d;clearTimeout(d.timers.show);d.elements.tooltip.stop(true,false);c=d.beforeHide.call(d,e);if(c===false)return d;function b(){d.onHide.call(d,e)}d.cache.toggle=0;if(typeof d.options.hide.effect.type=="function"){d.options.hide.effect.type.call(d.elements.tooltip,d.options.hide.effect.length);d.elements.tooltip.queue(function(){b();a(this).dequeue()})}else{switch(d.options.hide.effect.type.toLowerCase()){case"fade":d.elements.tooltip.fadeOut(d.options.hide.effect.length,b);break;case"slide":d.elements.tooltip.slideUp(d.options.hide.effect.length,b);break;case"grow":d.elements.tooltip.hide(d.options.hide.effect.length,b);break;default:d.elements.tooltip.hide(null,b)}d.elements.tooltip.removeClass(d.options.style.classes.active)}return a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_HIDDEN,"hide")},updatePosition:function(k,s){var g,b,f,e,p,h,c,j,r,t,l,i,m,n;if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"updatePosition");else if(d.options.position.type=="static")return a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.CANNOT_POSITION_STATIC,"updatePosition");b={position:{left:0,top:0},dimensions:{height:0,width:0},corner:d.options.position.corner.target};f={position:d.getPosition(),dimensions:d.getDimensions(),corner:d.options.position.corner.tooltip};if(d.options.position.target!=="mouse"){if(d.options.position.target.get(0).nodeName.toLowerCase()=="area"){e=d.options.position.target.attr("coords").split(",");for(g=0;g<e.length;g++)e[g]=parseInt(e[g]);p=d.options.position.target.parent("map").attr("name");h=a('img[usemap="#'+p+'"]:first').offset();b.position={left:Math.floor(h.left+e[0]),top:Math.floor(h.top+e[1])};switch(d.options.position.target.attr("shape").toLowerCase()){case"rect":b.dimensions={width:Math.ceil(Math.abs(e[2]-e[0])),height:Math.ceil(Math.abs(e[3]-e[1]))};break;case"circle":b.dimensions={width:e[2]+1,height:e[2]+1};break;case"poly":b.dimensions={width:e[0],height:e[1]};for(g=0;g<e.length;g++)if(g%2==0){if(e[g]>b.dimensions.width)b.dimensions.width=e[g];if(e[g]<e[0])b.position.left=Math.floor(h.left+e[g])}else{if(e[g]>b.dimensions.height)b.dimensions.height=e[g];if(e[g]<e[1])b.position.top=Math.floor(h.top+e[g])}b.dimensions.width=b.dimensions.width-(b.position.left-h.left);b.dimensions.height=b.dimensions.height-(b.position.top-h.top);break;default:return a.fn.qtip.log.error.call(d,4,a.fn.qtip.constants.INVALID_AREA_SHAPE,"updatePosition")}b.dimensions.width-=2;b.dimensions.height-=2}else if(d.options.position.target.add(document.body).length===1){b.position={left:a(document).scrollLeft(),top:a(document).scrollTop()};b.dimensions={height:a(window).height(),width:a(window).width()}}else{if(typeof d.options.position.target.attr("qtip")!=="undefined")b.position=d.options.position.target.qtip("api").cache.position;else b.position=d.options.position.target.offset();b.dimensions={height:d.options.position.target.outerHeight(),width:d.options.position.target.outerWidth()}}c=a.extend({},b.position);if(b.corner.search(/right/i)!==-1)c.left+=b.dimensions.width;if(b.corner.search(/bottom/i)!==-1)c.top+=b.dimensions.height;if(b.corner.search(/((top|bottom)Middle)|center/)!==-1)c.left+=b.dimensions.width/2;if(b.corner.search(/((left|right)Middle)|center/)!==-1)c.top+=b.dimensions.height/2}else{b.position=c={left:d.cache.mouse.x,top:d.cache.mouse.y};b.dimensions={height:1,width:1}}if(f.corner.search(/right/i)!==-1)c.left-=f.dimensions.width;if(f.corner.search(/bottom/i)!==-1)c.top-=f.dimensions.height;if(f.corner.search(/((top|bottom)Middle)|center/)!==-1)c.left-=f.dimensions.width/2;if(f.corner.search(/((left|right)Middle)|center/)!==-1)c.top-=f.dimensions.height/2;j=a.browser.msie?1:0;r=a.browser.msie&&parseInt(a.browser.version.charAt(0))===6?1:0;if(d.options.style.border.radius>0){if(f.corner.search(/Left/)!==-1)c.left-=d.options.style.border.radius;else if(f.corner.search(/Right/)!==-1)c.left+=d.options.style.border.radius;if(f.corner.search(/Top/)!==-1)c.top-=d.options.style.border.radius;else if(f.corner.search(/Bottom/)!==-1)c.top+=d.options.style.border.radius}if(j){if(f.corner.search(/top/)!==-1)c.top-=j;else if(f.corner.search(/bottom/)!==-1)c.top+=j;if(f.corner.search(/left/)!==-1)c.left-=j;else if(f.corner.search(/right/)!==-1)c.left+=j;if(f.corner.search(/leftMiddle|rightMiddle/)!==-1)c.top-=1}if(d.options.position.adjust.screen===true)c=o.call(d,c,b,f);if(d.options.position.target==="mouse"&&d.options.position.adjust.mouse===true){if(d.options.position.adjust.screen===true&&d.elements.tip)l=d.elements.tip.attr("rel");else l=d.options.position.corner.tooltip;c.left+=l.search(/right/i)!==-1?-6:6;c.top+=l.search(/bottom/i)!==-1?-6:6}!d.elements.bgiframe&&a.browser.msie&&parseInt(a.browser.version.charAt(0))==6&&a("select, object").each(function(){i=a(this).offset();i.bottom=i.top+a(this).height();i.right=i.left+a(this).width();c.top+f.dimensions.height>=i.top&&c.left+f.dimensions.width>=i.left&&q.call(d)});c.left+=d.options.position.adjust.x;c.top+=d.options.position.adjust.y;m=d.getPosition();if(c.left!=m.left||c.top!=m.top){n=d.beforePositionUpdate.call(d,k);if(n===false)return d;d.cache.position=c;if(s===true){d.status.animated=true;d.elements.tooltip.animate(c,200,"swing",function(){d.status.animated=false})}else d.elements.tooltip.css(c);d.onPositionUpdate.call(d,k);typeof k!=="undefined"&&k.type&&k.type!=="mousemove"&&a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_POSITION_UPDATED,"updatePosition")}return d},updateWidth:function(b){var c;if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"updateWidth");else if(b&&typeof b!=="number")return a.fn.qtip.log.error.call(d,2,"newWidth must be of type number","updateWidth");c=d.elements.contentWrapper.siblings().add(d.elements.tip).add(d.elements.button);if(!b)if(typeof d.options.style.width.value=="number")b=d.options.style.width.value;else{d.elements.tooltip.css({width:"auto"});c.hide();a.browser.msie&&d.elements.wrapper.add(d.elements.contentWrapper.children()).css({zoom:"normal"});b=d.getDimensions().width+1;if(!d.options.style.width.value){if(b>d.options.style.width.max)b=d.options.style.width.max;if(b<d.options.style.width.min)b=d.options.style.width.min}}if(b%2!==0)b-=1;d.elements.tooltip.width(b);c.show();d.options.style.border.radius&&d.elements.tooltip.find(".qtip-betweenCorners").each(function(){a(this).width(b-d.options.style.border.radius*2)});if(a.browser.msie){d.elements.wrapper.add(d.elements.contentWrapper.children()).css({zoom:"1"});d.elements.wrapper.width(b);d.elements.bgiframe&&d.elements.bgiframe.width(b).height(d.getDimensions.height)}return a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_WIDTH_UPDATED,"updateWidth")},updateStyle:function(k){var f,n,i,j,m;if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"updateStyle");else if(typeof k!=="string"||!a.fn.qtip.styles[k])return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.STYLE_NOT_DEFINED,"updateStyle");d.options.style=g.call(d,a.fn.qtip.styles[k],d.options.user.style);d.elements.content.css(b(d.options.style));d.options.content.title.text!==false&&d.elements.title.css(b(d.options.style.title,true));d.elements.contentWrapper.css({borderColor:d.options.style.border.color});if(d.options.style.tip.corner!==false)if(a("<canvas>").get(0).getContext){f=d.elements.tooltip.find(".qtip-tip canvas:first");i=f.get(0).getContext("2d");i.clearRect(0,0,300,300);j=f.parent("div[rel]:first").attr("rel");m=e(j,d.options.style.tip.size.width,d.options.style.tip.size.height);l.call(d,f,m,d.options.style.tip.color||d.options.style.border.color)}else if(a.browser.msie){f=d.elements.tooltip.find('.qtip-tip [nodeName="shape"]');f.attr("fillcolor",d.options.style.tip.color||d.options.style.border.color)}if(d.options.style.border.radius>0){d.elements.tooltip.find(".qtip-betweenCorners").css({backgroundColor:d.options.style.border.color});if(a("<canvas>").get(0).getContext){n=c(d.options.style.border.radius);d.elements.tooltip.find(".qtip-wrapper canvas").each(function(){i=a(this).get(0).getContext("2d");i.clearRect(0,0,300,300);j=a(this).parent("div[rel]:first").attr("rel");h.call(d,a(this),n[j],d.options.style.border.radius,d.options.style.border.color)})}else a.browser.msie&&d.elements.tooltip.find('.qtip-wrapper [nodeName="arc"]').each(function(){a(this).attr("fillcolor",d.options.style.border.color)})}return a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_STYLE_UPDATED,"updateStyle")},updateContent:function(b,i){var c,e,g;if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"updateContent");else if(!b)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.NO_CONTENT_PROVIDED,"updateContent");c=d.beforeContentUpdate.call(d,b);if(typeof c=="string")b=c;else if(c===false)return;a.browser.msie&&d.elements.contentWrapper.children().css({zoom:"normal"});if(b.jquery&&b.length>0)b.clone(true).appendTo(d.elements.content).show();else d.elements.content.html(b);e=d.elements.content.find("img[complete=false]");if(e.length>0){g=0;e.each(function(){a('<img src="'+a(this).attr("src")+'" />').load(function(){++g==e.length&&h()})})}else h();function h(){d.updateWidth();if(i!==false){d.options.position.type!=="static"&&d.updatePosition(d.elements.tooltip.is(":visible"),true);d.options.style.tip.corner!==false&&f.call(d)}}d.onContentUpdate.call(d);return a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_CONTENT_UPDATED,"loadContent")},loadContent:function(f,e,g){var c;if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"loadContent");c=d.beforeContentLoad.call(d);if(c===false)return d;if(g=="post")a.post(f,e,b);else a.get(f,e,b);function b(b){d.onContentLoad.call(d);a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_CONTENT_LOADED,"loadContent");d.updateContent(b)}return d},updateTitle:function(b){if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"updateTitle");else if(!b)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.NO_CONTENT_PROVIDED,"updateTitle");returned=d.beforeTitleUpdate.call(d);if(returned===false)return d;if(d.elements.button)d.elements.button=d.elements.button.clone(true);d.elements.title.html(b);d.elements.button&&d.elements.title.prepend(d.elements.button);d.onTitleUpdate.call(d);return a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_TITLE_UPDATED,"updateTitle")},focus:function(g){var e,c,b,f;if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"focus");else if(d.options.position.type=="static")return a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.CANNOT_FOCUS_STATIC,"focus");e=parseInt(d.elements.tooltip.css("z-index"));c=6e3+a("div.qtip[qtip]").length-1;if(!d.status.focused&&e!==c){f=d.beforeFocus.call(d,g);if(f===false)return d;a("div.qtip[qtip]").not(d.elements.tooltip).each(function(){if(a(this).qtip("api").status.rendered===true){b=parseInt(a(this).css("z-index"));typeof b=="number"&&b>-1&&a(this).css({zIndex:parseInt(a(this).css("z-index"))-1});a(this).qtip("api").status.focused=false}});d.elements.tooltip.css({zIndex:c});d.status.focused=true;d.onFocus.call(d,g);a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_FOCUSED,"focus")}return d},disable:function(b){if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"disable");if(b)if(!d.status.disabled){d.status.disabled=true;a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_DISABLED,"disable")}else a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.TOOLTIP_ALREADY_DISABLED,"disable");else if(d.status.disabled){d.status.disabled=false;a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_ENABLED,"disable")}else a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.TOOLTIP_ALREADY_ENABLED,"disable");return d},destroy:function(){var c,e,b;e=d.beforeDestroy.call(d);if(e===false)return d;if(d.status.rendered){d.options.show.when.target.unbind("mousemove.qtip",d.updatePosition);d.options.show.when.target.unbind("mouseout.qtip",d.hide);d.options.show.when.target.unbind(d.options.show.when.event+".qtip");d.options.hide.when.target.unbind(d.options.hide.when.event+".qtip");d.elements.tooltip.unbind(d.options.hide.when.event+".qtip");d.elements.tooltip.unbind("mouseover.qtip",d.focus);d.elements.tooltip.remove()}else d.options.show.when.target.unbind(d.options.show.when.event+".qtip-create");if(typeof d.elements.target.data("qtip")=="object"){b=d.elements.target.data("qtip").interfaces;if(typeof b=="object"&&b.length>0)for(c=0;c<b.length-1;c++)b[c].id==d.id&&b.splice(c,1)}delete a.fn.qtip.interfaces[d.id];if(typeof b=="object"&&b.length>0)d.elements.target.data("qtip").current=b.length-1;else d.elements.target.removeData("qtip");d.onDestroy.call(d);a.fn.qtip.log.error.call(d,1,a.fn.qtip.constants.EVENT_DESTROYED,"destroy");return d.elements.target},getPosition:function(){var b,c;if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"getPosition");b=d.elements.tooltip.css("display")!=="none"?false:true;b&&d.elements.tooltip.css({visiblity:"hidden"}).show();c=d.elements.tooltip.offset();b&&d.elements.tooltip.css({visiblity:"visible"}).hide();return c},getDimensions:function(){var b,c;if(!d.status.rendered)return a.fn.qtip.log.error.call(d,2,a.fn.qtip.constants.TOOLTIP_NOT_RENDERED,"getDimensions");b=!d.elements.tooltip.is(":visible")?true:false;b&&d.elements.tooltip.css({visiblity:"hidden"}).show();c={height:d.elements.tooltip.outerHeight(),width:d.elements.tooltip.outerWidth()};b&&d.elements.tooltip.css({visiblity:"visible"}).hide();return c}})}function j(){var c,i,d,g,f,e,h;c=this;c.beforeRender.call(c);c.status.rendered=true;c.elements.tooltip='<div qtip="'+c.id+'" class="qtip '+(c.options.style.classes.tooltip||c.options.style)+'"style="display:none; -moz-border-radius:0; -webkit-border-radius:0; border-radius:0;position:'+c.options.position.type+';">  <div class="qtip-wrapper" style="position:relative; overflow:hidden; text-align:left;">    <div class="qtip-contentWrapper" style="overflow:hidden;">       <div class="qtip-content '+c.options.style.classes.content+'"></div></div></div></div>';c.elements.tooltip=a(c.elements.tooltip);c.elements.tooltip.appendTo(c.options.position.container);c.elements.tooltip.data("qtip",{current:0,interfaces:[c]});c.elements.wrapper=c.elements.tooltip.children("div:first");c.elements.contentWrapper=c.elements.wrapper.children("div:first").css({background:c.options.style.background});c.elements.content=c.elements.contentWrapper.children("div:first").css(b(c.options.style));a.browser.msie&&c.elements.wrapper.add(c.elements.content).css({zoom:1});c.options.hide.when.event=="unfocus"&&c.elements.tooltip.attr("unfocus",true);typeof c.options.style.width.value=="number"&&c.updateWidth();if(a("<canvas>").get(0).getContext||a.browser.msie){if(c.options.style.border.radius>0)n.call(c);else c.elements.contentWrapper.css({border:c.options.style.border.width+"px solid "+c.options.style.border.color});c.options.style.tip.corner!==false&&k.call(c)}else{c.elements.contentWrapper.css({border:c.options.style.border.width+"px solid "+c.options.style.border.color});c.options.style.border.radius=0;c.options.style.tip.corner=false;a.fn.qtip.log.error.call(c,2,a.fn.qtip.constants.CANVAS_VML_NOT_SUPPORTED,"render")}if(typeof c.options.content.text=="string"&&c.options.content.text.length>0||c.options.content.text.jquery&&c.options.content.text.length>0)d=c.options.content.text;else if(typeof c.elements.target.attr("title")=="string"&&c.elements.target.attr("title").length>0){d=c.elements.target.attr("title").replace("\\n","<br />");c.elements.target.attr("title","")}else if(typeof c.elements.target.attr("alt")=="string"&&c.elements.target.attr("alt").length>0){d=c.elements.target.attr("alt").replace("\\n","<br />");c.elements.target.attr("alt","")}else{d=" ";a.fn.qtip.log.error.call(c,1,a.fn.qtip.constants.NO_VALID_CONTENT,"render")}c.options.content.title.text!==false&&p.call(c);c.updateContent(d);m.call(c);c.options.show.ready===true&&c.show();if(c.options.content.url!==false){g=c.options.content.url;f=c.options.content.data;e=c.options.content.method||"get";c.loadContent(g,f,e)}c.onRender.call(c);a.fn.qtip.log.error.call(c,1,a.fn.qtip.constants.EVENT_RENDERED,"render")}function n(){var d,e,j,b,i,g,f,l,m,k,p,n,o,q,r;d=this;d.elements.wrapper.find(".qtip-borderBottom, .qtip-borderTop").remove();j=d.options.style.border.width;b=d.options.style.border.radius;i=d.options.style.border.color||d.options.style.tip.color;g=c(b);f={};for(e in g){f[e]='<div rel="'+e+'" style="'+(e.search(/Left/)!==-1?"left":"right")+":0; position:absolute; height:"+b+"px; width:"+b+'px; overflow:hidden; line-height:0.1px; font-size:1px">';if(a("<canvas>").get(0).getContext)f[e]+='<canvas height="'+b+'" width="'+b+'" style="vertical-align: top"></canvas>';else if(a.browser.msie){l=b*2+3;f[e]+='<v:arc stroked="false" fillcolor="'+i+'" startangle="'+g[e][0]+'" endangle="'+g[e][1]+'" style="width:'+l+"px; height:"+l+"px; margin-top:"+(e.search(/bottom/)!==-1?-2:-1)+"px; margin-left:"+(e.search(/Right/)!==-1?g[e][2]-3.5:-1)+'px; vertical-align:top; display:inline-block; behavior:url(#default#VML)"></v:arc>'}f[e]+="</div>"}m=d.getDimensions().width-Math.max(j,b)*2;k='<div class="qtip-betweenCorners" style="height:'+b+"px; width:"+m+"px; overflow:hidden; background-color:"+i+'; line-height:0.1px; font-size:1px;">';p='<div class="qtip-borderTop" dir="ltr" style="height:'+b+"px; margin-left:"+b+'px; line-height:0.1px; font-size:1px; padding:0;">'+f.topLeft+f.topRight+k;d.elements.wrapper.prepend(p);n='<div class="qtip-borderBottom" dir="ltr" style="height:'+b+"px; margin-left:"+b+'px; line-height:0.1px; font-size:1px; padding:0;">'+f.bottomLeft+f.bottomRight+k;d.elements.wrapper.append(n);if(a("<canvas>").get(0).getContext)d.elements.wrapper.find("canvas").each(function(){o=g[a(this).parent("[rel]:first").attr("rel")];h.call(d,a(this),o,b,i)});else a.browser.msie&&d.elements.tooltip.append('<v:image style="behavior:url(#default#VML);"></v:image>');q=Math.max(b,b+(j-b));r=Math.max(j-b,0);d.elements.contentWrapper.css({border:"0px solid "+i,borderWidth:r+"px "+q+"px"})}function h(c,b,d,e){var a=c.get(0).getContext("2d");a.fillStyle=e;a.beginPath();a.arc(b[0],b[1],d,0,Math.PI*2,false);a.fill()}function k(d){var b,h,c,i,g;b=this;b.elements.tip!==null&&b.elements.tip.remove();h=b.options.style.tip.color||b.options.style.border.color;if(b.options.style.tip.corner===false)return;else if(!d)d=b.options.style.tip.corner;c=e(d,b.options.style.tip.size.width,b.options.style.tip.size.height);b.elements.tip='<div class="'+b.options.style.classes.tip+'" dir="ltr" rel="'+d+'" style="position:absolute; height:'+b.options.style.tip.size.height+"px; width:"+b.options.style.tip.size.width+'px; margin:0 auto; line-height:0.1px; font-size:1px;">';if(a("<canvas>").get(0).getContext)b.elements.tip+='<canvas height="'+b.options.style.tip.size.height+'" width="'+b.options.style.tip.size.width+'"></canvas>';else if(a.browser.msie){i=b.options.style.tip.size.width+","+b.options.style.tip.size.height;g="m"+c[0][0]+","+c[0][1];g+=" l"+c[1][0]+","+c[1][1];g+=" "+c[2][0]+","+c[2][1];g+=" xe";b.elements.tip+='<v:shape fillcolor="'+h+'" stroked="false" filled="true" path="'+g+'" coordsize="'+i+'" style="width:'+b.options.style.tip.size.width+"px; height:"+b.options.style.tip.size.height+"px; line-height:0.1px; display:inline-block; behavior:url(#default#VML); vertical-align:"+(d.search(/top/)!==-1?"bottom":"top")+'"></v:shape>';b.elements.tip+='<v:image style="behavior:url(#default#VML);"></v:image>';b.elements.contentWrapper.css("position","relative")}b.elements.tooltip.prepend(b.elements.tip+"</div>");b.elements.tip=b.elements.tooltip.find("."+b.options.style.classes.tip).eq(0);a("<canvas>").get(0).getContext&&l.call(b,b.elements.tip.find("canvas:first"),c,h);d.search(/top/)!==-1&&a.browser.msie&&parseInt(a.browser.version.charAt(0))===6&&b.elements.tip.css({marginTop:-4});f.call(b,d)}function l(c,a,d){var b=c.get(0).getContext("2d");b.fillStyle=d;b.beginPath();b.moveTo(a[0][0],a[0][1]);b.lineTo(a[1][0],a[1][1]);b.lineTo(a[2][0],a[2][1]);b.fill()}function f(c){var b,d,e,g,f;b=this;if(b.options.style.tip.corner===false||!b.elements.tip)return;if(!c)c=b.elements.tip.attr("rel");d=positionAdjust=a.browser.msie?1:0;b.elements.tip.css(c.match(/left|right|top|bottom/)[0],0);if(c.search(/top|bottom/)!==-1){if(a.browser.msie)if(parseInt(a.browser.version.charAt(0))===6)positionAdjust=c.search(/top/)!==-1?-3:1;else positionAdjust=c.search(/top/)!==-1?1:2;if(c.search(/Middle/)!==-1)b.elements.tip.css({left:"50%",marginLeft:-(b.options.style.tip.size.width/2)});else if(c.search(/Left/)!==-1)b.elements.tip.css({left:b.options.style.border.radius-d});else c.search(/Right/)!==-1&&b.elements.tip.css({right:b.options.style.border.radius+d});if(c.search(/top/)!==-1)b.elements.tip.css({top:-positionAdjust});else b.elements.tip.css({bottom:positionAdjust})}else if(c.search(/left|right/)!==-1){if(a.browser.msie)positionAdjust=parseInt(a.browser.version.charAt(0))===6?1:c.search(/left/)!==-1?1:2;if(c.search(/Middle/)!==-1)b.elements.tip.css({top:"50%",marginTop:-(b.options.style.tip.size.height/2)});else if(c.search(/Top/)!==-1)b.elements.tip.css({top:b.options.style.border.radius-d});else c.search(/Bottom/)!==-1&&b.elements.tip.css({bottom:b.options.style.border.radius+d});if(c.search(/left/)!==-1)b.elements.tip.css({left:-positionAdjust});else b.elements.tip.css({right:positionAdjust})}e="padding-"+c.match(/left|right|top|bottom/)[0];g=b.options.style.tip.size[e.search(/left|right/)!==-1?"width":"height"];b.elements.tooltip.css("padding",0);b.elements.tooltip.css(e,g);if(a.browser.msie&&parseInt(a.browser.version.charAt(0))==6){f=parseInt(b.elements.tip.css("margin-top"))||0;f+=parseInt(b.elements.content.css("margin-top"))||0;b.elements.tip.css({marginTop:f})}}function p(){var c=this;c.elements.title!==null&&c.elements.title.remove();c.elements.title=a('<div class="'+c.options.style.classes.title+'">').css(b(c.options.style.title,true)).css({zoom:a.browser.msie?1:0}).prependTo(c.elements.contentWrapper);c.options.content.title.text&&c.updateTitle.call(c,c.options.content.title.text);if(c.options.content.title.button!==false&&typeof c.options.content.title.button=="string")c.elements.button=a('<a class="'+c.options.style.classes.button+'" style="float:right; position: relative"></a>').css(b(c.options.style.button,true)).html(c.options.content.title.button).prependTo(c.elements.title).click(function(a){!c.status.disabled&&c.hide(a)})}function m(){var b,d,c,e;b=this;d=b.options.show.when.target;c=b.options.hide.when.target;if(b.options.hide.fixed)c=c.add(b.elements.tooltip);if(b.options.hide.when.event=="inactive"){e=["click","dblclick","mousedown","mouseup","mousemove","mouseout","mouseenter","mouseleave","mouseover"];function f(d){if(b.status.disabled===true)return;clearTimeout(b.timers.inactive);b.timers.inactive=setTimeout(function(){a(e).each(function(){c.unbind(this+".qtip-inactive");b.elements.content.unbind(this+".qtip-inactive")});b.hide(d)},b.options.hide.delay)}}else b.options.hide.fixed===true&&b.elements.tooltip.bind("mouseover.qtip",function(){if(b.status.disabled===true)return;clearTimeout(b.timers.hide)});function h(d){if(b.status.disabled===true)return;if(b.options.hide.when.event=="inactive"){a(e).each(function(){c.bind(this+".qtip-inactive",f);b.elements.content.bind(this+".qtip-inactive",f)});f()}clearTimeout(b.timers.show);clearTimeout(b.timers.hide);b.timers.show=setTimeout(function(){b.show(d)},b.options.show.delay)}function g(c){if(b.status.disabled===true)return;if(b.options.hide.fixed===true&&b.options.hide.when.event.search(/mouse(out|leave)/i)!==-1&&a(c.relatedTarget).parents("div.qtip[qtip]").length>0){c.stopPropagation();c.preventDefault();clearTimeout(b.timers.hide);return false}clearTimeout(b.timers.show);clearTimeout(b.timers.hide);b.elements.tooltip.stop(true,true);b.timers.hide=setTimeout(function(){b.hide(c)},b.options.hide.delay)}if(b.options.show.when.target.add(b.options.hide.when.target).length===1&&b.options.show.when.event==b.options.hide.when.event&&b.options.hide.when.event!=="inactive"||b.options.hide.when.event=="unfocus"){b.cache.toggle=0;d.bind(b.options.show.when.event+".qtip",function(a){if(b.cache.toggle==0)h(a);else g(a)})}else{d.bind(b.options.show.when.event+".qtip",h);b.options.hide.when.event!=="inactive"&&c.bind(b.options.hide.when.event+".qtip",g)}b.options.position.type.search(/(fixed|absolute)/)!==-1&&b.elements.tooltip.bind("mouseover.qtip",b.focus);b.options.position.target==="mouse"&&b.options.position.type!=="static"&&d.bind("mousemove.qtip",function(a){b.cache.mouse={x:a.pageX,y:a.pageY};b.status.disabled===false&&b.options.position.adjust.mouse===true&&b.options.position.type!=="static"&&b.elements.tooltip.css("display")!=="none"&&b.updatePosition(a)})}function o(i,g,c){var d,b,h,e,f,j;d=this;if(c.corner=="center")return g.position;b=a.extend({},i);e={x:false,y:false};f={left:b.left<a.fn.qtip.cache.screen.scroll.left,right:b.left+c.dimensions.width+2>=a.fn.qtip.cache.screen.width+a.fn.qtip.cache.screen.scroll.left,top:b.top<a.fn.qtip.cache.screen.scroll.top,bottom:b.top+c.dimensions.height+2>=a.fn.qtip.cache.screen.height+a.fn.qtip.cache.screen.scroll.top};h={left:f.left&&(c.corner.search(/right/i)!=-1||c.corner.search(/right/i)==-1&&!f.right),right:f.right&&(c.corner.search(/left/i)!=-1||c.corner.search(/left/i)==-1&&!f.left),top:f.top&&c.corner.search(/top/i)==-1,bottom:f.bottom&&c.corner.search(/bottom/i)==-1};if(h.left){if(d.options.position.target!=="mouse")b.left=g.position.left+g.dimensions.width;else b.left=d.cache.mouse.x;e.x="Left"}else if(h.right){if(d.options.position.target!=="mouse")b.left=g.position.left-c.dimensions.width;else b.left=d.cache.mouse.x-c.dimensions.width;e.x="Right"}if(h.top){if(d.options.position.target!=="mouse")b.top=g.position.top+g.dimensions.height;else b.top=d.cache.mouse.y;e.y="top"}else if(h.bottom){if(d.options.position.target!=="mouse")b.top=g.position.top-c.dimensions.height;else b.top=d.cache.mouse.y-c.dimensions.height;e.y="bottom"}if(b.left<0){b.left=i.left;e.x=false}if(b.top<0){b.top=i.top;e.y=false}if(d.options.style.tip.corner!==false){b.corner=new String(c.corner);if(e.x!==false)b.corner=b.corner.replace(/Left|Right|Middle/,e.x);if(e.y!==false)b.corner=b.corner.replace(/top|bottom/,e.y);b.corner!==d.elements.tip.attr("rel")&&k.call(d,b.corner)}return b}function b(e,d){var b,c;b=a.extend(true,{},e);for(c in b)if(d===true&&c.search(/(tip|classes)/i)!==-1)delete b[c];else if(!d&&c.search(/(width|border|tip|title|classes|user)/i)!==-1)delete b[c];return b}function d(a){if(typeof a.tip!=="object")a.tip={corner:a.tip};if(typeof a.tip.size!=="object")a.tip.size={width:a.tip.size,height:a.tip.size};if(typeof a.border!=="object")a.border={width:a.border};if(typeof a.width!=="object")a.width={value:a.width};if(typeof a.width.max=="string")a.width.max=parseInt(a.width.max.replace(/([0-9]+)/i,"$1"));if(typeof a.width.min=="string")a.width.min=parseInt(a.width.min.replace(/([0-9]+)/i,"$1"));if(typeof a.tip.size.x=="number"){a.tip.size.width=a.tip.size.x;delete a.tip.size.x}if(typeof a.tip.size.y=="number"){a.tip.size.height=a.tip.size.y;delete a.tip.size.y}return a}function g(){var h,e,f,c,b,g;h=this;f=[true,{}];for(e=0;e<arguments.length;e++)f.push(arguments[e]);c=[a.extend.apply(a,f)];while(typeof c[0].name=="string")c.unshift(d(a.fn.qtip.styles[c[0].name]));c.unshift(true,{classes:{tooltip:"qtip-"+(arguments[0].name||"defaults")}},a.fn.qtip.styles.defaults);b=a.extend.apply(a,c);g=a.browser.msie?1:0;b.tip.size.width+=g;b.tip.size.height+=g;if(b.tip.size.width%2>0)b.tip.size.width+=1;if(b.tip.size.height%2>0)b.tip.size.height+=1;if(b.tip.corner===true)b.tip.corner=h.options.position.corner.tooltip==="center"?false:h.options.position.corner.tooltip;return b}function e(d,b,a){var c={bottomRight:[[0,0],[b,a],[b,0]],bottomLeft:[[0,0],[b,0],[0,a]],topRight:[[0,a],[b,0],[b,a]],topLeft:[[0,0],[0,a],[b,a]],topMiddle:[[0,a],[b/2,0],[b,a]],bottomMiddle:[[0,0],[b,0],[b/2,a]],rightMiddle:[[0,0],[b,a/2],[0,a]],leftMiddle:[[b,0],[b,a],[0,a/2]]};c.leftTop=c.bottomRight;c.rightTop=c.bottomLeft;c.leftBottom=c.topRight;c.rightBottom=c.topLeft;return c[d]}function c(b){var c;if(a("<canvas>").get(0).getContext)c={topLeft:[b,b],topRight:[0,b],bottomLeft:[b,0],bottomRight:[0,0]};else if(a.browser.msie)c={topLeft:[-90,90,0],topRight:[-90,90,-b],bottomLeft:[90,270,0],bottomRight:[90,270,-b]};return c}function q(){var a,c,b;a=this;b=a.getDimensions();c='<iframe class="qtip-bgiframe" frameborder="0" tabindex="-1" src="javascript:false" style="display:block; position:absolute; z-index:-1; filter:alpha(opacity=\'0\'); border: 1px solid red; height:'+b.height+"px; width:"+b.width+'px" />';a.elements.bgiframe=a.elements.wrapper.prepend(c).children(".qtip-bgiframe:first")}a(document).ready(function(){a.fn.qtip.cache={screen:{scroll:{left:a(window).scrollLeft(),top:a(window).scrollTop()},width:a(window).width(),height:a(window).height()}};var b;a(window).bind("resize scroll",function(c){clearTimeout(b);b=setTimeout(function(){if(c.type==="scroll")a.fn.qtip.cache.screen.scroll={left:a(window).scrollLeft(),top:a(window).scrollTop()};else{a.fn.qtip.cache.screen.width=a(window).width();a.fn.qtip.cache.screen.height=a(window).height()}for(i=0;i<a.fn.qtip.interfaces.length;i++){var b=a.fn.qtip.interfaces[i];b.status.rendered===true&&(b.options.position.type!=="static"||b.options.position.adjust.scroll&&c.type==="scroll"||b.options.position.adjust.resize&&c.type==="resize")&&b.updatePosition(c,true)}},100)});a(document).bind("mousedown.qtip",function(b){a(b.target).parents("div.qtip").length===0&&a(".qtip[unfocus]").each(function(){var c=a(this).qtip("api");a(this).is(":visible")&&!c.status.disabled&&a(b.target).add(c.elements.target).length>1&&c.hide(b)})})});a.fn.qtip.interfaces=[];a.fn.qtip.log={error:function(){return this}};a.fn.qtip.constants={};a.fn.qtip.defaults={content:{prerender:false,text:false,url:false,data:null,title:{text:false,button:false}},position:{target:false,corner:{target:"bottomRight",tooltip:"topLeft"},adjust:{x:0,y:0,mouse:true,screen:false,scroll:true,resize:true},type:"absolute",container:false},show:{when:{target:false,event:"mouseover"},effect:{type:"fade",length:100},delay:140,solo:false,ready:false},hide:{when:{target:false,event:"mouseout"},effect:{type:"fade",length:100},delay:0,fixed:false},api:{beforeRender:function(){},onRender:function(){},beforePositionUpdate:function(){},onPositionUpdate:function(){},beforeShow:function(){},onShow:function(){},beforeHide:function(){},onHide:function(){},beforeContentUpdate:function(){},onContentUpdate:function(){},beforeContentLoad:function(){},onContentLoad:function(){},beforeTitleUpdate:function(){},onTitleUpdate:function(){},beforeDestroy:function(){},onDestroy:function(){},beforeFocus:function(){},onFocus:function(){}}};a.fn.qtip.styles={defaults:{background:"white",color:"#111",overflow:"hidden",textAlign:"left",width:{min:0,max:250},padding:"5px 9px",border:{width:1,radius:0,color:"#d3d3d3"},tip:{corner:false,color:false,size:{width:13,height:13},opacity:1},title:{background:"#e1e1e1",fontWeight:"bold",padding:"7px 12px"},button:{cursor:"pointer"},classes:{target:"",tip:"qtip-tip",title:"qtip-title",button:"qtip-button",content:"qtip-content",active:"qtip-active"}},cream:{border:{width:3,radius:0,color:"#F9E98E"},title:{background:"#F0DE7D",color:"#A27D35"},background:"#FBF7AA",color:"#A27D35",classes:{tooltip:"qtip-cream"}},light:{border:{width:3,radius:0,color:"#E2E2E2"},title:{background:"#f1f1f1",color:"#454545"},background:"white",color:"#454545",classes:{tooltip:"qtip-light"}},dark:{border:{width:3,radius:0,color:"#303030"},title:{background:"#404040",color:"#f3f3f3"},background:"#505050",color:"#f3f3f3",classes:{tooltip:"qtip-dark"}},red:{border:{width:3,radius:0,color:"#CE6F6F"},title:{background:"#f28279",color:"#9C2F2F"},background:"#F79992",color:"#9C2F2F",classes:{tooltip:"qtip-red"}},green:{border:{width:3,radius:0,color:"#A9DB66"},title:{background:"#b9db8c",color:"#58792E"},background:"#CDE6AC",color:"#58792E",classes:{tooltip:"qtip-green"}},blue:{border:{width:3,radius:0,color:"#ADD9ED"},title:{background:"#D0E9F5",color:"#5E99BD"},background:"#E5F6FE",color:"#4D9FBF",classes:{tooltip:"qtip-blue"}}}})(jQuery)