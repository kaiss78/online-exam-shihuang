(function(a){a.extend(a.fn,{validate:function(c){if(!this.length){c&&c.debug&&window.console&&console.warn("nothing selected, can't validate, returning nothing");return}var b=a.data(this[0],"validator");if(b)return b;b=new a.validator(c,this[0]);a.data(this[0],"validator",b);if(b.settings.onsubmit){this.find("input, button").filter(".cancel").click(function(){b.cancelSubmit=true});b.settings.submitHandler&&this.find("input, button").filter(":submit").click(function(){b.submitButton=this});this.submit(function(d){b.settings.debug&&d.preventDefault();function c(){if(b.settings.submitHandler){if(b.submitButton)var c=a("<input type='hidden'/>").attr("name",b.submitButton.name).val(b.submitButton.value).appendTo(b.currentForm);b.settings.submitHandler.call(b,b.currentForm);b.submitButton&&c.remove();return false}return true}if(b.cancelSubmit){b.cancelSubmit=false;return c()}if(b.form()){if(b.pendingRequest){b.formSubmitted=true;return false}return c()}else{b.focusInvalid();return false}})}return b},valid:function(){if(a(this[0]).is("form"))return this.validate().form();else{var b=true,c=a(this[0].form).validate();this.each(function(){b&=c.element(this)});return b}},removeAttrs:function(d){var c={},b=this;a.each(d.split(/\s/),function(d,a){c[a]=b.attr(a);b.removeAttr(a)});return c},rules:function(i,e){var b=this[0];if(i){var f=a.data(b.form,"validator").settings,g=f.rules,d=a.validator.staticRules(b);switch(i){case"add":a.extend(d,a.validator.normalizeRule(e));g[b.name]=d;if(e.messages)f.messages[b.name]=a.extend(f.messages[b.name],e.messages);break;case"remove":if(!e){delete g[b.name];return d}var h={};a.each(e.split(/\s/),function(b,a){h[a]=d[a];delete d[a]});return h}}var c=a.validator.normalizeRules(a.extend({},a.validator.metadataRules(b),a.validator.classRules(b),a.validator.attributeRules(b),a.validator.staticRules(b)),b);if(c.required){var j=c.required;delete c.required;c=a.extend({required:j},c)}return c}});a.extend(a.expr[":"],{blank:function(b){return!a.trim(""+b.value)},filled:function(b){return!!a.trim(""+b.value)},unchecked:function(a){return!a.checked}});a.validator=function(b,c){this.settings=a.extend({},a.validator.defaults,b);this.currentForm=c;this.init()};a.validator.format=function(c,b){if(arguments.length==1)return function(){var b=a.makeArray(arguments);b.unshift(c);return a.validator.format.apply(this,b)};if(arguments.length>2&&b.constructor!=Array)b=a.makeArray(arguments).slice(1);if(b.constructor!=Array)b=[b];a.each(b,function(a,b){c=c.replace(new RegExp("\\{"+a+"\\}","g"),b)});return c};a.extend(a.validator,{defaults:{messages:{},groups:{},rules:{},errorClass:"error",validClass:"valid",errorElement:"label",focusInvalid:true,errorContainer:a([]),errorLabelContainer:a([]),onsubmit:true,ignore:[],ignoreTitle:false,onfocusin:function(a){this.lastActive=a;if(this.settings.focusCleanup&&!this.blockFocusCleanup){this.settings.unhighlight&&this.settings.unhighlight.call(this,a,this.settings.errorClass,this.settings.validClass);this.errorsFor(a).hide()}},onfocusout:function(a){!this.checkable(a)&&(a.name in this.submitted||!this.optional(a))&&this.element(a)},onkeyup:function(a){(a.name in this.submitted||a==this.lastElement)&&this.element(a)},onclick:function(a){if(a.name in this.submitted)this.element(a);else a.parentNode.name in this.submitted&&this.element(a.parentNode)},highlight:function(d,b,c){a(d).addClass(b).removeClass(c)},unhighlight:function(d,b,c){a(d).removeClass(b).addClass(c)}},setDefaults:function(b){a.extend(a.validator.defaults,b)},messages:{required:"This field is required.",remote:"Please fix this field.",email:"Please enter a valid email address.",url:"Please enter a valid URL.",date:"Please enter a valid date.",dateISO:"Please enter a valid date (ISO).",number:"Please enter a valid number.",digits:"Please enter only digits.",creditcard:"Please enter a valid credit card number.",equalTo:"Please enter the same value again.",accept:"Please enter a value with a valid extension.",maxlength:a.validator.format("Please enter no more than {0} characters."),minlength:a.validator.format("Please enter at least {0} characters."),rangelength:a.validator.format("Please enter a value between {0} and {1} characters long."),range:a.validator.format("Please enter a value between {0} and {1}."),max:a.validator.format("Please enter a value less than or equal to {0}."),min:a.validator.format("Please enter a value greater than or equal to {0}.")},autoCreateRanges:false,prototype:{init:function(){this.labelContainer=a(this.settings.errorLabelContainer);this.errorContext=this.labelContainer.length&&this.labelContainer||a(this.currentForm);this.containers=a(this.settings.errorContainer).add(this.settings.errorLabelContainer);this.submitted={};this.valueCache={};this.pendingRequest=0;this.pending={};this.invalid={};this.reset();var d=this.groups={};a.each(this.settings.groups,function(c,b){a.each(b.split(/\s/),function(b,a){d[a]=c})});var c=this.settings.rules;a.each(c,function(d,b){c[d]=a.validator.normalizeRule(b)});function b(c){var b=a.data(this[0].form,"validator");b.settings["on"+c.type]&&b.settings["on"+c.type].call(b,this[0])}a(this.currentForm).delegate("focusin focusout keyup",":text, :password, :file, select, textarea",b).delegate("click",":radio, :checkbox, select, option",b);this.settings.invalidHandler&&a(this.currentForm).bind("invalid-form.validate",this.settings.invalidHandler)},form:function(){this.checkForm();a.extend(this.submitted,this.errorMap);this.invalid=a.extend({},this.errorMap);!this.valid()&&a(this.currentForm).triggerHandler("invalid-form",[this]);this.showErrors();return this.valid()},checkForm:function(){this.prepareForm();for(var a=0,b=this.currentElements=this.elements();b[a];a++)this.check(b[a]);return this.valid()},element:function(b){b=this.clean(b);this.lastElement=b;this.prepareElement(b);this.currentElements=a(b);var c=this.check(b);if(c)delete this.invalid[b.name];else this.invalid[b.name]=true;if(!this.numberOfInvalids())this.toHide=this.toHide.add(this.containers);this.showErrors();return c},showErrors:function(b){if(b){a.extend(this.errorMap,b);this.errorList=[];for(var c in b)this.errorList.push({message:b[c],element:this.findByName(c)[0]});this.successList=a.grep(this.successList,function(a){return!(a.name in b)})}this.settings.showErrors?this.settings.showErrors.call(this,this.errorMap,this.errorList):this.defaultShowErrors()},resetForm:function(){a.fn.resetForm&&a(this.currentForm).resetForm();this.submitted={};this.prepareForm();this.hideErrors();this.elements().removeClass(this.settings.errorClass)},numberOfInvalids:function(){return this.objectLength(this.invalid)},objectLength:function(b){var a=0;for(var c in b)a++;return a},hideErrors:function(){this.addWrapper(this.toHide).hide()},valid:function(){return this.size()==0},size:function(){return this.errorList.length},focusInvalid:function(){if(this.settings.focusInvalid)try{a(this.findLastActive()||this.errorList.length&&this.errorList[0].element||[]).filter(":visible").focus()}catch(b){}},findLastActive:function(){var b=this.lastActive;return b&&a.grep(this.errorList,function(a){return a.element.name==b.name}).length==1&&b},elements:function(){var c=this,b={};return a([]).add(this.currentForm.elements).filter(":input").not(":submit, :reset, :image, [disabled]").not(this.settings.ignore).filter(function(){!this.name&&c.settings.debug&&window.console&&console.error("%o has no name assigned",this);if(this.name in b||!c.objectLength(a(this).rules()))return false;b[this.name]=true;return true})},clean:function(b){return a(b)[0]},errors:function(){return a(this.settings.errorElement+"."+this.settings.errorClass,this.errorContext)},reset:function(){this.successList=[];this.errorList=[];this.errorMap={};this.toShow=a([]);this.toHide=a([]);this.currentElements=a([])},prepareForm:function(){this.reset();this.toHide=this.errors().add(this.containers)},prepareElement:function(a){this.reset();this.toHide=this.errorsFor(a)},check:function(b){b=this.clean(b);if(this.checkable(b))b=this.findByName(b.name)[0];var e=a(b).rules(),c=false;for(method in e){var f={method:method,parameters:e[method]};try{var d=a.validator.methods[method].call(this,b.value.replace(/\r/g,""),b,f.parameters);if(d=="dependency-mismatch"){c=true;continue}c=false;if(d=="pending"){this.toHide=this.toHide.not(this.errorsFor(b));return}if(!d){this.formatAndAdd(b,f);return false}}catch(g){this.settings.debug&&window.console&&console.log("exception occured when checking element "+b.id+", check the '"+f.method+"' method",g);throw g;}}if(c)return;this.objectLength(e)&&this.successList.push(b);return true},customMetaMessage:function(c,d){if(!a.metadata)return;var b=this.settings.meta?a(c).metadata()[this.settings.meta]:a(c).metadata();return b&&b.messages&&b.messages[d]},customMessage:function(c,b){var a=this.settings.messages[c];return a&&(a.constructor==String?a:a[b])},findDefined:function(){for(var a=0;a<arguments.length;a++)if(arguments[a]!==undefined)return arguments[a];return undefined},defaultMessage:function(b,c){return this.findDefined(this.customMessage(b.name,c),this.customMetaMessage(b,c),!this.settings.ignoreTitle&&b.title||undefined,a.validator.messages[c],"<strong>Warning: No message defined for "+b.name+"</strong>")},formatAndAdd:function(b,c){var a=this.defaultMessage(b,c.method),d=/\$?\{(\d+)\}/g;if(typeof a=="function")a=a.call(this,c.parameters,b);else if(d.test(a))a=jQuery.format(a.replace(d,"{$1}"),c.parameters);this.errorList.push({message:a,element:b});this.errorMap[b.name]=a;this.submitted[b.name]=a},addWrapper:function(a){if(this.settings.wrapper)a=a.add(a.parent(this.settings.wrapper));return a},defaultShowErrors:function(){for(var a=0;this.errorList[a];a++){var b=this.errorList[a];this.settings.highlight&&this.settings.highlight.call(this,b.element,this.settings.errorClass,this.settings.validClass);this.showLabel(b.element,b.message)}if(this.errorList.length)this.toShow=this.toShow.add(this.containers);if(this.settings.success)for(var a=0;this.successList[a];a++)this.showLabel(this.successList[a]);if(this.settings.unhighlight)for(var a=0,c=this.validElements();c[a];a++)this.settings.unhighlight.call(this,c[a],this.settings.errorClass,this.settings.validClass);this.toHide=this.toHide.not(this.toShow);this.hideErrors();this.addWrapper(this.toShow).show()},validElements:function(){return this.currentElements.not(this.invalidElements())},invalidElements:function(){return a(this.errorList).map(function(){return this.element})},showLabel:function(c,d){var b=this.errorsFor(c);if(b.length){b.removeClass().addClass(this.settings.errorClass);b.attr("generated")&&b.html(d)}else{b=a("<"+this.settings.errorElement+"/>").attr({"for":this.idOrName(c),generated:true}).addClass(this.settings.errorClass).html(d||"");if(this.settings.wrapper)b=b.hide().show().wrap("<"+this.settings.wrapper+"/>").parent();if(!this.labelContainer.append(b).length)this.settings.errorPlacement?this.settings.errorPlacement(b,a(c)):b.insertAfter(c)}if(!d&&this.settings.success){b.text("");typeof this.settings.success=="string"?b.addClass(this.settings.success):this.settings.success(b)}this.toShow=this.toShow.add(b)},errorsFor:function(b){var c=this.idOrName(b);return this.errors().filter(function(){return a(this).attr("for")==c})},idOrName:function(a){return this.groups[a.name]||(this.checkable(a)?a.name:a.id||a.name)},checkable:function(a){return/radio|checkbox/i.test(a.type)},findByName:function(b){var c=this.currentForm;return a(document.getElementsByName(b)).map(function(d,a){return a.form==c&&a.name==b&&a||null})},getLength:function(c,b){switch(b.nodeName.toLowerCase()){case"select":return a("option:selected",b).length;case"input":if(this.checkable(b))return this.findByName(b.name).filter(":checked").length}return c.length},depend:function(a,b){return this.dependTypes[typeof a]?this.dependTypes[typeof a](a,b):true},dependTypes:{"boolean":function(a){return a},string:function(c,b){return!!a(c,b.form).length},"function":function(b,a){return b(a)}},optional:function(b){return!a.validator.methods.required.call(this,a.trim(b.value),b)&&"dependency-mismatch"},startRequest:function(a){if(!this.pending[a.name]){this.pendingRequest++;this.pending[a.name]=true}},stopRequest:function(c,b){this.pendingRequest--;if(this.pendingRequest<0)this.pendingRequest=0;delete this.pending[c.name];if(b&&this.pendingRequest==0&&this.formSubmitted&&this.form()){a(this.currentForm).submit();this.formSubmitted=false}else if(!b&&this.pendingRequest==0&&this.formSubmitted){a(this.currentForm).triggerHandler("invalid-form",[this]);this.formSubmitted=false}},previousValue:function(b){return a.data(b,"previousValue")||a.data(b,"previousValue",{old:null,valid:true,message:this.defaultMessage(b,"remote")})}},classRuleSettings:{required:{required:true},email:{email:true},url:{url:true},date:{date:true},dateISO:{dateISO:true},dateDE:{dateDE:true},number:{number:true},numberDE:{numberDE:true},digits:{digits:true},creditcard:{creditcard:true}},addClassRules:function(b,c){b.constructor==String?this.classRuleSettings[b]=c:a.extend(this.classRuleSettings,b)},classRules:function(d){var c={},b=a(d).attr("class");b&&a.each(b.split(" "),function(){this in a.validator.classRuleSettings&&a.extend(c,a.validator.classRuleSettings[this])});return c},attributeRules:function(e){var b={},d=a(e);for(method in a.validator.methods){var c=d.attr(method);if(c)b[method]=c}if(b.maxlength&&/-1|2147483647|524288/.test(b.maxlength))delete b.maxlength;return b},metadataRules:function(b){if(!a.metadata)return{};var c=a.data(b.form,"validator").settings.meta;return c?a(b).metadata()[c]:a(b).metadata()},staticRules:function(c){var d={},b=a.data(c.form,"validator");if(b.settings.rules)d=a.validator.normalizeRule(b.settings.rules[c.name])||{};return d},normalizeRules:function(b,c){a.each(b,function(f,d){if(d===false){delete b[f];return}if(d.param||d.depends){var e=true;switch(typeof d.depends){case"string":e=!!a(d.depends,c.form).length;break;case"function":e=d.depends.call(c,c)}if(e)b[f]=d.param!==undefined?d.param:true;else delete b[f]}});a.each(b,function(e,d){b[e]=a.isFunction(d)?d(c):d});a.each(["minlength","maxlength","min","max"],function(){if(b[this])b[this]=Number(b[this])});a.each(["rangelength","range"],function(){if(b[this])b[this]=[Number(b[this][0]),Number(b[this][1])]});if(a.validator.autoCreateRanges){if(b.min&&b.max){b.range=[b.min,b.max];delete b.min;delete b.max}if(b.minlength&&b.maxlength){b.rangelength=[b.minlength,b.maxlength];delete b.minlength;delete b.maxlength}}if(b.messages)delete b.messages;return b},normalizeRule:function(b){if(typeof b=="string"){var c={};a.each(b.split(/\s/),function(){c[this]=true});b=c}return b},addMethod:function(b,d,c){a.validator.methods[b]=d;a.validator.messages[b]=c!=undefined?c:a.validator.messages[b];d.length<3&&a.validator.addClassRules(b,a.validator.normalizeRule(b))},methods:{required:function(c,b,e){if(!this.depend(e,b))return"dependency-mismatch";switch(b.nodeName.toLowerCase()){case"select":var d=a(b).val();return d&&d.length>0;case"input":if(this.checkable(b))return this.getLength(c,b)>0;default:return a.trim(c).length>0}},remote:function(f,b,e){if(this.optional(b))return"dependency-mismatch";var d=this.previousValue(b);if(!this.settings.messages[b.name])this.settings.messages[b.name]={};d.originalMessage=this.settings.messages[b.name].remote;this.settings.messages[b.name].remote=d.message;e=typeof e=="string"&&{url:e}||e;if(d.old!==f){d.old=f;var c=this;this.startRequest(b);var g={};g[b.name]=f;a.ajax(a.extend(true,{url:e,mode:"abort",port:"validate"+b.name,dataType:"json",data:g,success:function(h){c.settings.messages[b.name].remote=d.originalMessage;var g=h===true;if(g){var j=c.formSubmitted;c.prepareElement(b);c.formSubmitted=j;c.successList.push(b);c.showErrors()}else{var i={},e=d.message=h||c.defaultMessage(b,"remote");i[b.name]=a.isFunction(e)?e(f):e;c.showErrors(i)}d.valid=g;c.stopRequest(b,g)}},e));return"pending"}else if(this.pending[b.name])return"pending";return d.valid},minlength:function(d,b,c){return this.optional(b)||this.getLength(a.trim(d),b)>=c},maxlength:function(d,b,c){return this.optional(b)||this.getLength(a.trim(d),b)<=c},rangelength:function(e,b,d){var c=this.getLength(a.trim(e),b);return this.optional(b)||c>=d[0]&&c<=d[1]},min:function(c,a,b){return this.optional(a)||c>=b},max:function(c,a,b){return this.optional(a)||c<=b},range:function(b,c,a){return this.optional(c)||b>=a[0]&&b<=a[1]},email:function(b,a){return this.optional(a)||/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i.test(b)},url:function(b,a){return this.optional(a)||/^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(b)},date:function(b,a){return this.optional(a)||!/Invalid|NaN/.test(new Date(b))},dateISO:function(b,a){return this.optional(a)||/^\d{4}[\/-]\d{1,2}[\/-]\d{1,2}$/.test(b)},number:function(b,a){return this.optional(a)||/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/.test(b)},digits:function(b,a){return this.optional(a)||/^\d+$/.test(b)},creditcard:function(a,f){if(this.optional(f))return"dependency-mismatch";if(/[^0-9-]+/.test(a))return false;var e=0,b=0,c=false;a=a.replace(/\D/g,"");for(var d=a.length-1;d>=0;d--){var g=a.charAt(d),b=parseInt(g,10);if(c)if((b*=2)>9)b-=9;e+=b;c=!c}return e%10==0},accept:function(c,b,a){a=typeof a=="string"?a.replace(/,/g,"|"):"png|jpe?g|gif";return this.optional(b)||c.match(new RegExp(".("+a+")$","i"))},equalTo:function(e,b,d){var c=a(d).unbind(".validate-equalTo").bind("blur.validate-equalTo",function(){a(b).valid()});return e==c.val()}}});a.format=a.validator.format})(jQuery);(function(a){var c=a.ajax,b={};a.ajax=function(d){d=a.extend(d,a.extend({},a.ajaxSettings,d));var e=d.port;if(d.mode=="abort"){b[e]&&b[e].abort();return b[e]=c.apply(this,arguments)}return c.apply(this,arguments)}})(jQuery);(function(a){a.each({focus:"focusin",blur:"focusout"},function(c,b){a.event.special[b]={setup:function(){if(a.browser.msie)return false;this.addEventListener(c,a.event.special[b].handler,true)},teardown:function(){if(a.browser.msie)return false;this.removeEventListener(c,a.event.special[b].handler,true)},handler:function(c){arguments[0]=a.event.fix(c);arguments[0].type=b;return a.event.handle.apply(this,arguments)}}});a.extend(a.fn,{delegate:function(d,b,c){return this.bind(d,function(e){var d=a(e.target);if(d.is(b))return c.apply(d,arguments)})},triggerEvent:function(b,c){return this.triggerHandler(b,[a.event.fix({type:b,target:c})])}})})(jQuery)