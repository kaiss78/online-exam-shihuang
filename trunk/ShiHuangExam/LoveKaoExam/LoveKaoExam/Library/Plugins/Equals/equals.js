var LK_Equals={obj:function(b,a){if(b===a)return true;if(!(a instanceof Object)||a===null)return false;var c=0;for(var e in b){c++;var d=b[e],f=a[e];if(d!=null&&!this.json(d,f))return false}for(var e in a)c--;return c===0},arr:function(c,a){if(!(a instanceof Array))return false;if(c===a)return true;if(a===null)return false;var f=c.length;if(f!=a.length)return false;for(var b=0;b<f;b++){var d=c[b],e=a[b];if(!(d===null?e===null:this.json(d,e)))return false}return true},str:function(b,a){return(a instanceof String||typeof a==="string")&&b.valueOf()===a.valueOf()},num:function(b,a){return(a instanceof Number||typeof a==="number")&&b.valueOf()===a.valueOf()},bool:function(b,a){return(a instanceof Boolean||typeof a==="boolean")&&b.valueOf()===a.valueOf()},date:function(b,a){return a instanceof Date&&b.valueOf()===a.valueOf()},fun:function(b,a){return a instanceof Function&&b.valueOf()===a.valueOf()},reg:function(b,a){return a instanceof RegExp&&b.source===a.source&&b.global===a.global&&b.ignoreCase===a.ignoreCase&&b.multiline===a.multiline},json:function(a,b){var c=a.constructor.toString(),d=(c||"").replace(/function|native code|\(|\)|\{|\}|\[|\]|\n| /g,"");switch(d){case"Object":return this.obj(a,b);break;case"Array":return this.arr(a,b);break;case"String":return this.str(a,b);break;case"Number":return this.num(a,b);break;case"Boolean":return this.bool(a,b);break;case"Date":return this.date(a,b);break;case"Function":return this.fun(a,b);break;case"RegExp":return this.reg(a,b);break;default:return true}}}