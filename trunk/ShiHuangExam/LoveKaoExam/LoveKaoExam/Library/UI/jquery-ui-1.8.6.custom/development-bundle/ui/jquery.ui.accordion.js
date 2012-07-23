(function(a){a.widget("ui.accordion",{options:{active:0,animated:"slide",autoHeight:true,clearStyle:false,collapsible:false,event:"click",fillSpace:false,header:"> li > :first-child,> :not(li):even",icons:{header:"ui-icon-triangle-1-e",headerSelected:"ui-icon-triangle-1-s"},navigation:false,navigationFilter:function(){return this.href.toLowerCase()===location.href.toLowerCase()}},_create:function(){var b=this,c=b.options;b.running=0;b.element.addClass("ui-accordion ui-widget ui-helper-reset").children("li").addClass("ui-accordion-li-fix");b.headers=b.element.find(c.header).addClass("ui-accordion-header ui-helper-reset ui-state-default ui-corner-all").bind("mouseenter.accordion",function(){if(c.disabled)return;a(this).addClass("ui-state-hover")}).bind("mouseleave.accordion",function(){if(c.disabled)return;a(this).removeClass("ui-state-hover")}).bind("focus.accordion",function(){if(c.disabled)return;a(this).addClass("ui-state-focus")}).bind("blur.accordion",function(){if(c.disabled)return;a(this).removeClass("ui-state-focus")});b.headers.next().addClass("ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom");if(c.navigation){var d=b.element.find("a").filter(c.navigationFilter).eq(0);if(d.length){var e=d.closest(".ui-accordion-header");if(e.length)b.active=e;else b.active=d.closest(".ui-accordion-content").prev()}}b.active=b._findActive(b.active||c.active).addClass("ui-state-default ui-state-active").toggleClass("ui-corner-all").toggleClass("ui-corner-top");b.active.next().addClass("ui-accordion-content-active");b._createIcons();b.resize();b.element.attr("role","tablist");b.headers.attr("role","tab").bind("keydown.accordion",function(a){return b._keydown(a)}).next().attr("role","tabpanel");b.headers.not(b.active||"").attr({"aria-expanded":"false",tabIndex:-1}).next().hide();if(!b.active.length)b.headers.eq(0).attr("tabIndex",0);else b.active.attr({"aria-expanded":"true",tabIndex:0});!a.browser.safari&&b.headers.find("a").attr("tabIndex",-1);c.event&&b.headers.bind(c.event.split(" ").join(".accordion ")+".accordion",function(a){b._clickHandler.call(b,a,this);a.preventDefault()})},_createIcons:function(){var b=this.options;if(b.icons){a("<span></span>").addClass("ui-icon "+b.icons.header).prependTo(this.headers);this.active.children(".ui-icon").toggleClass(b.icons.header).toggleClass(b.icons.headerSelected);this.element.addClass("ui-accordion-icons")}},_destroyIcons:function(){this.headers.children(".ui-icon").remove();this.element.removeClass("ui-accordion-icons")},destroy:function(){var b=this.options;this.element.removeClass("ui-accordion ui-widget ui-helper-reset").removeAttr("role");this.headers.unbind(".accordion").removeClass("ui-accordion-header ui-accordion-disabled ui-helper-reset ui-state-default ui-corner-all ui-state-active ui-state-disabled ui-corner-top").removeAttr("role").removeAttr("aria-expanded").removeAttr("tabIndex");this.headers.find("a").removeAttr("tabIndex");this._destroyIcons();var c=this.headers.next().css("display","").removeAttr("role").removeClass("ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content ui-accordion-content-active ui-accordion-disabled ui-state-disabled");(b.autoHeight||b.fillHeight)&&c.css("height","");return a.Widget.prototype.destroy.call(this)},_setOption:function(c,b){a.Widget.prototype._setOption.apply(this,arguments);c=="active"&&this.activate(b);if(c=="icons"){this._destroyIcons();b&&this._createIcons()}c=="disabled"&&this.headers.add(this.headers.next())[b?"addClass":"removeClass"]("ui-accordion-disabled ui-state-disabled")},_keydown:function(b){if(this.options.disabled||b.altKey||b.ctrlKey)return;var c=a.ui.keyCode,e=this.headers.length,f=this.headers.index(b.target),d=false;switch(b.keyCode){case c.RIGHT:case c.DOWN:d=this.headers[(f+1)%e];break;case c.LEFT:case c.UP:d=this.headers[(f-1+e)%e];break;case c.SPACE:case c.ENTER:this._clickHandler({target:b.target},b.target);b.preventDefault()}if(d){a(b.target).attr("tabIndex",-1);a(d).attr("tabIndex",0);d.focus();return false}return true},resize:function(){var c=this.options,b;if(c.fillSpace){if(a.browser.msie){var d=this.element.parent().css("overflow");this.element.parent().css("overflow","hidden")}b=this.element.parent().height();a.browser.msie&&this.element.parent().css("overflow",d);this.headers.each(function(){b-=a(this).outerHeight(true)});this.headers.next().each(function(){a(this).height(Math.max(0,b-a(this).innerHeight()+a(this).height()))}).css("overflow","auto")}else if(c.autoHeight){b=0;this.headers.next().each(function(){b=Math.max(b,a(this).height("").height())}).height(b)}return this},activate:function(b){this.options.active=b;var a=this._findActive(b)[0];this._clickHandler({target:a},a);return this},_findActive:function(b){return b?typeof b==="number"?this.headers.filter(":eq("+b+")"):this.headers.not(this.headers.not(b)):b===false?a([]):this.headers.filter(":eq(0)")},_clickHandler:function(g,i){var b=this.options;if(b.disabled)return;if(!g.target){if(!b.collapsible)return;this.active.removeClass("ui-state-active ui-corner-top").addClass("ui-state-default ui-corner-all").children(".ui-icon").removeClass(b.icons.headerSelected).addClass(b.icons.header);this.active.next().addClass("ui-accordion-content-active");var e=this.active.next(),h={options:b,newHeader:a([]),oldHeader:b.active,newContent:a([]),oldContent:e},f=this.active=a([]);this._toggle(f,e,h);return}var c=a(g.currentTarget||i),d=c[0]===this.active[0];b.active=b.collapsible&&d?false:this.headers.index(c);if(this.running||!b.collapsible&&d)return;this.active.removeClass("ui-state-active ui-corner-top").addClass("ui-state-default ui-corner-all").children(".ui-icon").removeClass(b.icons.headerSelected).addClass(b.icons.header);if(!d){c.removeClass("ui-state-default ui-corner-all").addClass("ui-state-active ui-corner-top").children(".ui-icon").removeClass(b.icons.header).addClass(b.icons.headerSelected);c.next().addClass("ui-accordion-content-active")}var f=c.next(),e=this.active.next(),h={options:b,newHeader:d&&b.collapsible?a([]):c,oldHeader:this.active,newContent:d&&b.collapsible?a([]):f,oldContent:e},j=this.headers.index(this.active[0])>this.headers.index(c[0]);this.active=d?a([]):c;this._toggle(f,e,h,d,j);return},_toggle:function(f,e,m,j,k){var c=this,b=c.options;c.toShow=f;c.toHide=e;c.data=m;var i=function(){return!c?void 0:c._completed.apply(c,arguments)};c._trigger("changestart",null,c.data);c.running=e.size()===0?f.size():e.size();if(b.animated){var g={};if(b.collapsible&&j)g={toShow:a([]),toHide:e,complete:i,down:k,autoHeight:b.autoHeight||b.fillSpace};else g={toShow:f,toHide:e,complete:i,down:k,autoHeight:b.autoHeight||b.fillSpace};if(!b.proxied)b.proxied=b.animated;if(!b.proxiedDuration)b.proxiedDuration=b.duration;b.animated=a.isFunction(b.proxied)?b.proxied(g):b.proxied;b.duration=a.isFunction(b.proxiedDuration)?b.proxiedDuration(g):b.proxiedDuration;var h=a.ui.accordion.animations,l=b.duration,d=b.animated;if(d&&!h[d]&&!a.easing[d])d="slide";if(!h[d])h[d]=function(a){this.slide(a,{easing:d,duration:l||700})};h[d](g)}else{if(b.collapsible&&j)f.toggle();else{e.hide();f.show()}i(true)}e.prev().attr({"aria-expanded":"false",tabIndex:-1}).blur();f.prev().attr({"aria-expanded":"true",tabIndex:0}).focus()},_completed:function(a){this.running=a?0:--this.running;if(this.running)return;this.options.clearStyle&&this.toShow.add(this.toHide).css({height:"",overflow:""});this.toHide.removeClass("ui-accordion-content-active");this._trigger("change",null,this.data)}});a.extend(a.ui.accordion,{version:"1.8.6",animations:{slide:function(b,h){b=a.extend({easing:"swing",duration:300},b,h);if(!b.toHide.size()){b.toShow.animate({height:"show",paddingTop:"show",paddingBottom:"show"},b);return}if(!b.toShow.size()){b.toHide.animate({height:"hide",paddingTop:"hide",paddingBottom:"hide"},b);return}var i=b.toShow.css("overflow"),f=0,d={},g={},j=["height","paddingTop","paddingBottom"],e,c=b.toShow;e=c[0].style.width;c.width(parseInt(c.parent().width(),10)-parseInt(c.css("paddingLeft"),10)-parseInt(c.css("paddingRight"),10)-(parseInt(c.css("borderLeftWidth"),10)||0)-(parseInt(c.css("borderRightWidth"),10)||0));a.each(j,function(f,c){g[c]="hide";var e=(""+a.css(b.toShow[0],c)).match(/^([\d+-.]+)(.*)$/);d[c]={value:e[1],unit:e[2]||"px"}});b.toShow.css({height:0,overflow:"hidden"}).show();b.toHide.filter(":hidden").each(b.complete).end().filter(":visible").animate(g,{step:function(c,a){if(a.prop=="height")f=a.end-a.start===0?0:(a.now-a.start)/(a.end-a.start);b.toShow[0].style[a.prop]=f*d[a.prop].value+d[a.prop].unit},duration:b.duration,easing:b.easing,complete:function(){!b.autoHeight&&b.toShow.css("height","");b.toShow.css({width:e,overflow:i});b.complete()}})},bounceslide:function(a){this.slide(a,{easing:a.down?"easeOutBounce":"swing",duration:a.down?1e3:200})}}})})(jQuery)