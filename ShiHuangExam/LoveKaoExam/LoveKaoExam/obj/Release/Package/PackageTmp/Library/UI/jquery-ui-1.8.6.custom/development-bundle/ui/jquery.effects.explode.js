(function(a){a.effects.explode=function(b){return this.queue(function(){var e=b.options.pieces?Math.round(Math.sqrt(b.options.pieces)):3,d=b.options.pieces?Math.round(Math.sqrt(b.options.pieces)):3;b.options.mode=b.options.mode=="toggle"?a(this).is(":visible")?"hide":"show":b.options.mode;var c=a(this).show().css("visibility","hidden"),i=c.offset();i.top-=parseInt(c.css("marginTop"),10)||0;i.left-=parseInt(c.css("marginLeft"),10)||0;for(var j=c.outerWidth(true),h=c.outerHeight(true),f=0;f<e;f++)for(var g=0;g<d;g++)c.clone().appendTo("body").wrap("<div></div>").css({position:"absolute",visibility:"visible",left:-g*(j/d),top:-f*(h/e)}).parent().addClass("ui-effects-explode").css({position:"absolute",overflow:"hidden",width:j/d,height:h/e,left:i.left+g*(j/d)+(b.options.mode=="show"?(g-Math.floor(d/2))*(j/d):0),top:i.top+f*(h/e)+(b.options.mode=="show"?(f-Math.floor(e/2))*(h/e):0),opacity:b.options.mode=="show"?0:1}).animate({left:i.left+g*(j/d)+(b.options.mode=="show"?0:(g-Math.floor(d/2))*(j/d)),top:i.top+f*(h/e)+(b.options.mode=="show"?0:(f-Math.floor(e/2))*(h/e)),opacity:b.options.mode=="show"?1:0},b.duration||500);setTimeout(function(){b.options.mode=="show"?c.css({visibility:"visible"}):c.css({visibility:"visible"}).hide();b.callback&&b.callback.apply(c[0]);c.dequeue();a("div.ui-effects-explode").remove()},b.duration||500)})}})(jQuery)