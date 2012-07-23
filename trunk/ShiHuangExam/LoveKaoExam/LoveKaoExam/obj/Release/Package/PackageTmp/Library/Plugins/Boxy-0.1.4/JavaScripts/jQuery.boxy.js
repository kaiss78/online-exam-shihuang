$.fn.boxy=function(a){a=a||{};return this.each(function(){var b=this.nodeName.toLowerCase(),c=this;if(b=="a")jQuery(this).click(function(){var e=Boxy.linkedTo(this),c=this.getAttribute("href"),b=jQuery.extend({actuator:this,title:this.title},a);if(e)e.show();else if(c.indexOf("#")>=0){var d=jQuery(c.substr(c.indexOf("#"))),f=d.clone(true);d.remove();b.unloadOnHide=false;new Boxy(f,b)}else{if(!b.cache)b.unloadOnHide=true;Boxy.load(this.href,b)}return false});else b=="form"&&jQuery(this).bind("submit.boxy",function(){Boxy.confirm(a.message||"Please confirm:",function(){jQuery(c).unbind("submit.boxy").submit()});return false})})};function Boxy(a,b){this.boxy=jQuery(Boxy.WRAPPER);jQuery.data(this.boxy[0],"boxy",this);this.visible=false;this.options=jQuery.extend({},Boxy.DEFAULTS,b||{});if(this.options.modal)this.options=jQuery.extend(this.options,{center:true,draggable:true});this.options.actuator&&jQuery.data(this.options.actuator,"active.boxy",this);this.setContent(a);this._setupTitleBar();this.boxy.css("display","none").appendTo(document.body);this.toTop();if(this.options.fixed)if(jQuery.browser.msie&&jQuery.browser.version<7)this.options.fixed=false;else this.boxy.addClass("fixed");if(this.options.center&&Boxy._u(this.options.x,this.options.y))this.center();else this.moveTo(Boxy._u(this.options.x)?this.options.x:Boxy.DEFAULT_X,Boxy._u(this.options.y)?this.options.y:Boxy.DEFAULT_Y);this.options.show&&this.show()}Boxy.EF=function(){};jQuery.extend(Boxy,{WRAPPER:"<table cellspacing='0' cellpadding='0' border='0' class='boxy-wrapper'><tr><td class='top-left'></td><td class='top'></td><td class='top-right'></td></tr><tr><td class='left'></td><td class='boxy-inner'></td><td class='right'></td></tr><tr><td class='bottom-left'></td><td class='bottom'></td><td class='bottom-right'></td></tr></table>",DEFAULTS:{title:"无标题",closeable:true,draggable:true,clone:false,actuator:null,center:true,show:true,modal:false,fixed:true,closeText:"[close]",unloadOnHide:false,clickToFront:false,behaviours:Boxy.EF,afterDrop:Boxy.EF,afterShow:Boxy.EF,beforeShow:Boxy.EF,beforeHide:Boxy.EF,afterHide:Boxy.EF,beginHide:Boxy.EF,beforeUnload:Boxy.EF,innerStyle:""},DEFAULT_X:50,DEFAULT_Y:50,zIndex:1337,dragConfigured:false,resizeConfigured:false,dragging:null,load:function(c,a){a=a||{};var b={url:c,type:"GET",dataType:"html",cache:false,success:function(b){b=jQuery(b);if(a.filter)b=jQuery(a.filter,b);new Boxy(b,a)}};jQuery.each(["type","cache"],function(){if(this in a){b[this]=a[this];delete a[this]}});jQuery.ajax(b)},"get":function(b){var a=jQuery(b).parents(".boxy-wrapper");return a.length?jQuery.data(a[0],"boxy"):null},linkedTo:function(a){return jQuery.data(a,"active.boxy")},alert:function(b,a,c){return Boxy.ask(b,["确定"],a,c)},confirm:function(b,d,c,a){return Boxy.ask(b,["确定","取消"],function(b){b=="确定"&&d();b=="取消"&&a!=undefined&&a()},c)},ask:function(j,a,h,e){e=jQuery.extend({modal:true,closeable:false},e||{},{show:true,unloadOnHide:true});var i=jQuery("<div></div>").append(jQuery('<div class="question"></div>').html(j)),f={},c=[];if(a instanceof Array)for(var b=0;b<a.length;b++){f[a[b]]=a[b];c.push(a[b])}else for(var g in a){f[a[g]]=g;c.push(a[g])}var d=jQuery('<div class="answers"></div>');d.html(jQuery.map(c,function(a){var b=a=="确定"?"btn1":"btn2";return'<span class="'+b+'">    <span class="bgFFF">        <input   type="button" value=\''+a+"' />    </span></span>"}).join(" "));jQuery("input[type=button]",d).click(function(){var a=this;Boxy.get(this).hide(function(){h&&h(f[a.value])})});i.append(d);new Boxy(i,e)},isModalVisible:function(){return jQuery(".boxy-modal-blackout").length>0},_u:function(){for(var a=0;a<arguments.length;a++)if(typeof arguments[a]!="undefined")return false;return true},_handleResize:function(){jQuery(".boxy-modal-blackout").css("display","none").css(bodyManage.size()).css("display","block")},_handleDrag:function(b){var a;(a=Boxy.dragging)&&a[0].boxy.css({left:b.pageX-a[1],top:b.pageY-a[2]})},_nextZ:function(){return Boxy.zIndex++},_viewport:function(){var a=document.documentElement,c=document.body,b=window;return jQuery.extend(jQuery.browser.msie?{left:c.scrollLeft||a.scrollLeft,top:c.scrollTop||a.scrollTop}:{left:b.pageXOffset,top:b.pageYOffset},!Boxy._u(b.innerWidth)?{width:b.innerWidth,height:b.innerHeight}:!Boxy._u(a)&&!Boxy._u(a.clientWidth)&&a.clientWidth!=0?{width:a.clientWidth,height:a.clientHeight}:{width:c.clientWidth,height:c.clientHeight})}});var bodyManage={browser:$.browser,size:function(){var b,a;if(this.browser.msie&&this.browser.version=="8.0"){b=Math.max(document.body.scrollWidth,document.documentElement.scrollWidth);a=Math.max(document.body.scrollHeight,document.documentElement.scrollHeight)}else{var c=jQuery(document);b=c.width();a=c.height()}b=b>4096?4096:b;a=a>4096?4096:a;return{width:b,height:a}}};Boxy.prototype={estimateSize:function(){this.boxy.css({visibility:"hidden",display:"block"});var a=this.getSize();this.boxy.css("display","none").css("visibility","visible");return a},getSize:function(){return[this.boxy.width(),this.boxy.height()]},getContentSize:function(){var a=this.getContent();return[a.width(),a.height()]},getPosition:function(){var a=this.boxy[0];return[a.offsetLeft,a.offsetTop]},getCenter:function(){var a=this.getPosition(),b=this.getSize();return[Math.floor(a[0]+b[0]/2),Math.floor(a[1]+b[1]/2)]},getInner:function(){return jQuery(".boxy-inner",this.boxy)},getTop:function(){return jQuery("td.top",this.boxy)},getContent:function(){return jQuery(".boxy-content",this.boxy)},setContent:function(c){var b={element:c,innerStyle:this.options.innerStyle},a=Global.URL.Iframe.collect(b).addClass("boxy-content");if(this.options.clone)a=a.clone(true);this.getContent().remove();this.getInner().append(a);this._setupDefaultBehaviours(a);this.options.behaviours.call(this,a);Global.URL.Iframe.release(a);return this},set内部对象属性:function(a,c,b){this.getInner().find(a).attr(c,b);return this},moveTo:function(a,b){this.moveToX(a).moveToY(b);return this},moveToX:function(a){if(typeof a=="number")this.boxy.css({left:a});else this.centerX();return this},moveToY:function(a){if(typeof a=="number")this.boxy.css({top:a});else this.centerY();return this},centerAt:function(b,c){var a=this[this.visible?"getSize":"estimateSize"]();typeof b=="number"&&this.moveToX(b-a[0]/2);typeof c=="number"&&this.moveToY(c-a[1]/2);return this},centerAtX:function(a){return this.centerAt(a,null)},centerAtY:function(a){return this.centerAt(null,a)},center:function(a){var b=Boxy._viewport(),c=this.options.fixed?[0,0]:[b.left,b.top];(!a||a=="x")&&this.centerAt(c[0]+b.width/2,null);(!a||a=="y")&&this.centerAt(null,c[1]+b.height/2);return this},centerX:function(){return this.center("x")},centerY:function(){return this.center("y")},resize:function(d,c,b){if(!this.visible)return;var a=this._getBoundsForResize(d,c);this.boxy.css({left:a[0],top:a[1]});this.getContent().css({width:a[2],height:a[3]});b&&b(this);return this},tween:function(d,c,b){if(!this.visible)return;var a=this._getBoundsForResize(d,c),e=this;this.boxy.stop().animate({left:a[0],top:a[1]});this.getContent().stop().animate({width:a[2],height:a[3]},function(){b&&b(e)});return this},isVisible:function(){return this.visible},show:function(){if(this.visible)return;if(this.options.modal){var c=this;if(!Boxy.resizeConfigured){Boxy.resizeConfigured=true;jQuery(window).resize(function(){Boxy._handleResize()})}var b=bodyManage.size(),a="";if(bodyManage.browser.msie&&bodyManage.browser.version=="6.0")a='<iframe src="about:blank" class="boxy-modal" frameborder="0" scrolling="no"></iframe>';this.modalBlackout=jQuery('<div class="boxy-modal-blackout">'+a+"</div>").css({zIndex:Boxy._nextZ(),opacity:.3,width:b.width,height:b.height}).appendTo(document.body);this.toTop();this.options.closeable&&jQuery(document.body).bind("keypress.boxy",function(a){var b=a.which||a.keyCode;if(b==27&&jQuery("> .title-bar a",c.getTop()).css("display")!="none"){c.hide();jQuery(document.body).unbind("keypress.boxy")}})}this._fire("beforeShow");this.center();this.boxy.stop().css($.browser.msie?{filter:"none"}:{opacity:1}).show();this.visible=true;this._fire("afterShow");return this},hide:function(b){if(!this.visible)return;var a=this;if(this.options.modal){jQuery(document.body).unbind("keypress.boxy");this.modalBlackout.animate({opacity:0},function(){jQuery(this).remove()})}a._fire("beginHide");a._fire("beforeHide");this.boxy.stop().animate({opacity:0},300,function(){a.boxy.css({display:"none"});a.visible=false;a._fire("afterHide");b&&b(a);a.options.unloadOnHide&&a.unload()});return this},toggle:function(){this[this.visible?"hide":"show"]();return this},hideAndUnload:function(a){this.options.unloadOnHide=true;this.hide(a);return this},unload:function(){this._fire("beforeUnload");this.boxy.remove();this.options.actuator&&jQuery.data(this.options.actuator,"active.boxy",false)},toTop:function(){this.boxy.css({zIndex:Boxy._nextZ()});return this},getTitle:function(){return jQuery("> .title-bar h2",this.getTop()).html()},setTitle:function(a){jQuery("> .title-bar h2",this.getTop()).html(a);return this},isCloseAble:function(a){if(a)jQuery("> .title-bar a",this.getTop()).show();else jQuery("> .title-bar a",this.getTop()).hide()},setCloseVis:function(a){this.isCloseAble(a===true)},_getBoundsForResize:function(d,a){var b=this.getContentSize(),c=[d-b[0],a-b[1]],e=this.getPosition();return[Math.max(e[0]-c[0]/2,0),Math.max(e[1]-c[1]/2,0),d,a]},_setupTitleBar:function(){if(this.options.title){var b=this,c=jQuery("<div class='title-bar'></div>").html("<h2>"+this.options.title+"</h2>"),a=this.getTop();this.options.closeable&&c.append(jQuery("<a href='javascript:void(0)' class='close' onmouseover=\"this.className='close close-over';\" onmouseout=\"this.className='close';\"  title=\"关闭\" ></a>"));a.prepend(c);if(this.options.draggable){a[0].onselectstart=function(){return false};a[0].unselectable="on";a[0].style.MozUserSelect="none";DragManage.init(a[0],this.boxy[0])}this._setupDefaultBehaviours(a);return;if(this.options.draggable){a[0].onselectstart=function(){return false};a[0].unselectable="on";a[0].style.MozUserSelect="none";if(!Boxy.dragConfigured){jQuery(document).mousemove(Boxy._handleDrag);Boxy.dragConfigured=true}a.mousedown(function(a){b.toTop();Boxy.dragging=[b,a.pageX-b.boxy[0].offsetLeft,a.pageY-b.boxy[0].offsetTop];jQuery(this).addClass("dragging")}).mouseup(function(){jQuery(this).removeClass("dragging");Boxy.dragging=null;b._fire("afterDrop")})}}},_setupDefaultBehaviours:function(a){var b=this;this.options.clickToFront&&a.click(function(){b.toTop()});jQuery(".close",a).click(function(){b.hide();return false}).mousedown(function(a){a.stopPropagation()})},_fire:function(a){this.options[a].call(this)}};var DragManage={isie:navigator.userAgent.toLowerCase().indexOf("msie")!=-1,obj:null,init:function(a,c,b){if(b==null)a.onmousedown=DragManage.start;a.root=c;if(isNaN(parseInt(a.root.style.left)))a.root.style.left="0px";if(isNaN(parseInt(a.root.style.top)))a.root.style.top="0px";a.root.onDragStart=new Function;a.root.onDragEnd=new Function;a.root.onDrag=new Function;if(b!=null){var a=DragManage.obj=a;b=DragManage.fixe(b);var e=parseInt(a.root.style.top),d=parseInt(a.root.style.left);a.root.onDragStart(d,e,b.pageX,b.pageY);a.lastMouseX=b.pageX;a.lastMouseY=b.pageY;document.onmousemove=DragManage.drag;document.onmouseup=DragManage.end}},start:function(a){var b=DragManage.obj=this;a=DragManage.fixEvent(a);var d=parseInt(b.root.style.top),c=parseInt(b.root.style.left);b.root.onDragStart(c,d,a.pageX,a.pageY);b.lastMouseX=a.pageX;b.lastMouseY=a.pageY;document.onmousemove=DragManage.drag;document.onmouseup=DragManage.end;return false},drag:function(b){b=DragManage.fixEvent(b);var a=DragManage.obj,f=b.pageY,e=b.pageX,h=parseInt(a.root.style.top),g=parseInt(a.root.style.left);if(DragManage.isie)DragManage.obj.setCapture();else b.preventDefault();var c,d;c=g+e-a.lastMouseX;d=h+(f-a.lastMouseY);a.root.style.left=c+"px";a.root.style.top=d+"px";a.lastMouseX=e;a.lastMouseY=f;a.root.onDrag(c,d,b.pageX,b.pageY);return false},end:function(){DragManage.isie&&DragManage.obj.releaseCapture();document.onmousemove=null;document.onmouseup=null;DragManage.obj.root.onDragEnd(parseInt(DragManage.obj.root.style.left),parseInt(DragManage.obj.root.style.top));DragManage.obj=null},fixEvent:function(a){var b=Math.max(document.documentElement.scrollLeft,document.body.scrollLeft),c=Math.max(document.documentElement.scrollTop,document.body.scrollTop);if(typeof a=="undefined")a=window.event;if(typeof a.layerX=="undefined")a.layerX=a.offsetX;if(typeof a.layerY=="undefined")a.layerY=a.offsetY;if(typeof a.pageX=="undefined")a.pageX=a.clientX+b-document.body.clientLeft;if(typeof a.pageY=="undefined")a.pageY=a.clientY+c-document.body.clientTop;return a}}