var ExaminerEmbedHandle={Organization:{Boxy:{key:"ExaminerEmbedHandle-Organization-Edit",init:function(c,b,a){Global.Boxy.iFrame(this.key,{src:"/Organization/Setup/1?testSetID="+c+"&testSetType="+b+"&uHandleType="+a,htmlattributes:{width:"800px",height:"550px"}},{newTitle:(a==0?"组织":"修改")+(b==0?"练习":"考试")+"设置"})},hide:function(){Global.Boxy.hide(this.key)},setCloseVis:function(a){Global.Boxy.setCloseVis(this.key,a)}},add:function(a,b,c){this.QTip.amount(a,b,c,0,function(){ExaminerEmbedHandle.Organization.Boxy.init(a,b,0)})},edit:function(a,b,c){this.QTip.amount(a,b,c,1,function(){ExaminerEmbedHandle.Organization.Boxy.init(a,b,1)})},QTip:{boxyKey:"ExaminerEmbedHandle-Organization-QTip",amount:function(j,c,i,f,g){var a=this.boxyKey,h={testSetID:j,testSetType:c,uHandleType:f},b=f==0?"组织":"修改",d=b+Global.Boxy.Topics.Examiner.organization(c,null);Global.Boxy.wait(a,{newTitle:"处理",dtHtml:"处理"+d});var e=c==0?"练习":"考试";if(i==4){Global.Boxy.failure(a,{newTitle:b+e+"失败",dtHtml:d+"失败",newMessage:"该试卷是草稿试卷，不能组织"+e+""});return}Global.AjaxServer.Post.json("/Organization/SetupAmount/",h,function(c){if(c.result){g&&g();Global.Boxy.hide(a)}else Global.Boxy.failure(a,{newTitle:b+e+"失败",dtHtml:d+"失败",newMessage:c.info})})}},Update:{list:function(a){Global.MvcPager.Ajax.update(a)}},Delete:{boxyKey:"ExaminerEmbedHandle-Organization-Del",confirm:function(d,b,e){var a=this.boxyKey,c=Global.Boxy.Topics.Examiner.organization(b,e);Global.Boxy.confirm(a,{newTitle:"确认删除",dtHtml:"确定删除"+c+"吗？",clickEvent:{oK:function(){ExaminerEmbedHandle.Organization.Delete.wait(d,b,c)},cancel:function(){Global.Boxy.hide(a)}}})},wait:function(c,d,b){var a=this.boxyKey;Global.Boxy.wait(a,{newTitle:"删除",dtHtml:"删除"+b});Global.AjaxServer.Post.json("/Organization/Delete/",{testSetID:c,testSetType:d},function(c){if(c.result)Global.Boxy.success(a,{newTitle:"删除成功",dtHtml:b+"已删除成功.",boxyOptions:{afterHide:function(){ExaminerEmbedHandle.Organization.Update.list({waitType:"正在更新"})}}});else Global.Boxy.failure(a,{newTitle:"删除失败",dtHtml:b+"删除失败",newMessage:c.info})})}}},Binding:{Boxy:{key:"ExaminerEmbedHandle-Binding-Edit",init:function(b,a){Global.Boxy.iFrame(this.key,{src:"/Binding/Validate/1?uBindingType="+b+"&uHandleType="+a,htmlattributes:{width:"550px",height:"350px"}},{newTitle:(a==0?"绑定":"更改绑定")+"账号"})},hide:function(){Global.Boxy.hide(this.key)},setCloseVis:function(a){Global.Boxy.setCloseVis(this.key,a)}},add:function(a){ExaminerEmbedHandle.Binding.Boxy.init(a,0)},edit:function(a){ExaminerEmbedHandle.Binding.Boxy.init(a,1)},Update:{userInfo:function(){var a=$("#Examiner_Binding_LKAccount");Global.BlockUI.Element.wait16({targetElem:a,数据类型:"正在更新"},null);$.ajax({type:"POST",url:"/Binding/LKAccount/",data:null,success:function(b){setTimeout(function(){a.html(b).unblock()},500)}})}},Delete:{boxyKey:"ExaminerEmbedHandle-Binding-Del",confirm:function(c){var a=this.boxyKey,b=Global.Boxy.Topics.Examiner.binding(1,c);Global.Boxy.confirm(a,{newTitle:"确认解除",dtHtml:"确认解除"+b+"吗？",clickEvent:{oK:function(){ExaminerEmbedHandle.Binding.Delete.wait(b)},cancel:function(){Global.Boxy.hide(a)}}})},wait:function(b){var a=this.boxyKey;Global.Boxy.wait(a,{newTitle:"解除绑定",dtHtml:"解除"+b});Global.AjaxServer.Post.json("/Binding/Delete/",null,function(c){if(c.result)Global.Boxy.success(a,{newTitle:"解除成功",dtHtml:b+"已解除成功.",boxyOptions:{afterHide:function(){ExaminerEmbedHandle.Binding.Update.userInfo()}}});else Global.Boxy.failure(a,{newTitle:"解除失败",dtHtml:b+"解除失败",newMessage:c.info})})}}},Share:{Boxy:{key:"ExaminerEmbedHandle-Share-Resource",init:function(a,b){Global.Boxy.iFrame(this.key,{src:"/Share/Resource/1?resourceMode="+a+"&resourceType="+b,htmlattributes:{width:"850px",height:"480px"}},{newTitle:(a==0?"上传":"下载")+(b==0?"试题":"试卷")})},hide:function(){Global.Boxy.hide(this.key)},setCloseVis:function(a){Global.Boxy.setCloseVis(this.key,a)}},upload:function(c,b,a){this.QTip.isBinding(b,a,function(){ExaminerEmbedHandle.Share.Boxy.init(0,c)})},download:function(d,c,a,b){this.QTip.allowDownload(c,a,b,function(){ExaminerEmbedHandle.Share.Boxy.init(1,d)})},QTip:{boxyKey:"ExaminerEmbedHandle-Share-QTip",isBinding:function(e,c,d){var b=this.boxyKey;if(e!="True")Global.Boxy.alert(b,{newTitle:"",dtHtml:"您还没绑定爱考网账号！",newMessage:Global.HTML.a("我要绑定账号",{href:"/Binding/LKAccount"})});else if(c==1)Global.Boxy.alert(b,{newTitle:"",dtHtml:"该账号已被爱考网禁用，请更改绑定账号！",newMessage:Global.HTML.a("我要更改绑定账号",{href:"/Binding/LKAccount"})});else if(c==2){var a=[];a.push("更改绑定账号");a.push("到爱考网设置允许被绑定");a.push(Global.HTML.a("我要绑定账号",{href:"/Binding/LKAccount"}));Global.Boxy.alert(b,{newTitle:"",dtHtml:"该绑定账号已设置不被任何人绑定，您可以",newMessage:a})}else d&&d()},allowDownload:function(e,c,a,b){var d=this.boxyKey;this.isBinding(e,c,function(){if(a<=0)Global.Boxy.alert(d,{newTitle:"",dtHtml:"目前您可下载试题数量为"+Global.HTML.b(a,{style:"color:green"})+"，请先上传试题或试卷后再下载！",newMessage:Global.HTML.a("我要上传试题/试卷",{href:"/Share/Upload"})+"(每上传"+Global.HTML.b(1,{style:"color:green"})+"道试题可下载"+Global.HTML.b(10,{style:"color:green"})+"道试题)"});else b&&b()})}},Update:{上传下载信息:function(b){var a=b=="上传"?$("#Examiner_Share_Upload"):$("#Examiner_Share_Download");Global.BlockUI.Element.wait16({targetElem:a,数据类型:"正在更新"},null);$.ajax({type:"post",url:"/Share/"+(b=="上传"?"Upload":"Download")+"/",data:null,success:function(b){setTimeout(function(){a.html(b).unblock()},500)}})}}},Subject:{Boxy:{key:"ExaminerEmbedHandle-Subject",init:function(c,a,b){Global.Boxy.iFrame(this.key,{src:c,htmlattributes:a},{newTitle:b})},hide:function(){Global.Boxy.hide(this.key)},setCloseVis:function(a){Global.Boxy.setCloseVis(this.key,a)}},view:function(a){this.Boxy.init("/LKSubject/SubjectView?guid="+a+"&state=2",{width:800,height:400},"预览试题")},edit:function(a){Global.Boxy.iFrame(this.key,{src:"/Subject/IFrame?guid="+a,htmlattributes:{width:940,height:500,id:"IFrameEditSubjectID"}},{newTitle:"编辑试题",boxyOptions:{beforeHide:function(){var b=document.getElementById("IFrameEditSubjectID").contentWindow||{},a=b.ReferServer||{};a.isEdited&&ExaminerEmbedHandle.Subject.Update.list({waitType:"正在更新"})}}})},Update:{list:function(a){Global.MvcPager.Ajax.update(a)}},Delete:{boxyKey:"ExaminerEmbedHandle-Subject-Delete",confirm:function(c,d){var a=this.boxyKey,b=Global.Boxy.Topics.Examiner.Subject.del(d);Global.Boxy.confirm(a,{newTitle:"确认删除",dtHtml:"确定删除"+b+"吗？",clickEvent:{oK:function(){ExaminerEmbedHandle.Subject.Delete.wait(c,b)},cancel:function(){Global.Boxy.hide(a)}}})},wait:function(c,b){var a=this.boxyKey;Global.Boxy.wait(a,{newTitle:"删除",dtHtml:"删除"+b});Global.AjaxServer.Post.json("/Subject/Delete/"+c,null,function(c){if(c.result)Global.Boxy.success(a,{newTitle:"删除成功",dtHtml:b+"已删除成功.",boxyOptions:{afterHide:function(){ExaminerEmbedHandle.Subject.Update.list({waitType:"正在更新"})}}});else Global.Boxy.failure(a,{newTitle:"删除失败",dtHtml:b+"删除失败",newMessage:c.info})})}}},Test:{Boxy:{key:"ExaminerEmbedHandle-Test",init:function(){},hide:function(){},setCloseVis:function(){}},Update:{list:function(a){Global.MvcPager.Ajax.update(a)}},Delete:{boxyKey:"ExaminerEmbedHandle-Test-Delete",confirm:function(c,d){var a=this.boxyKey,b=Global.Boxy.Topics.Examiner.Test.del(d);Global.Boxy.confirm(a,{newTitle:"确认删除",dtHtml:"确定删除"+b+"吗？",clickEvent:{oK:function(){ExaminerEmbedHandle.Test.Delete.wait(c,b)},cancel:function(){Global.Boxy.hide(a)}}})},wait:function(c,b){var a=this.boxyKey;Global.Boxy.wait(a,{newTitle:"删除",dtHtml:"删除"+b});Global.AjaxServer.Post.json("/Test/Delete/"+c,null,function(c){if(c.result)Global.Boxy.success(a,{newTitle:"删除成功",dtHtml:b+"已删除成功.",boxyOptions:{afterHide:function(){ExaminerEmbedHandle.Test.Update.list({waitType:"正在更新"})}}});else Global.Boxy.failure(a,{newTitle:"删除失败",dtHtml:b+"删除失败",newMessage:c.info})})}}}}