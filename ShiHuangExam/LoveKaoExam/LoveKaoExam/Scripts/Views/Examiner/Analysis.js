var ExaminerAnalysis={ExamResult:{initData:function(e,d){var b=ExaminerAnalysis.ExamResult,c=b.ElemAssembly,a=b.DataAssembly;Global.LoadIng.ready(function(){a.Post.考试设置ID=e;a.Backup.是否公布考试结果=d==1?true:false;c.Other.提交按钮_是否公布考试结果=$("#提交按钮_是否公布考试结果")})},ElemAssembly:{Post:{},Other:{提交按钮_是否公布考试结果:0}},DataAssembly:{Post:{考试设置ID:null,是否公布考试结果:false},Backup:{是否公布考试结果:false}},Handle:{Type:{},name:function(){},embedBoxyHide:function(){},embedBoxyBtn:function(){},completed:function(){var b=ExaminerAnalysis.ExamResult,e=b.ElemAssembly,a=b.DataAssembly,c=a.Post.是否公布考试结果,d=(c?"关闭":"公布")+"考试结果";a.Backup.是否公布考试结果=c;e.Other.提交按钮_是否公布考试结果.val(d).attr("title",d)}},Format:{是否公布考试结果:function(d){var b=ExaminerAnalysis.ExamResult,a=b.DataAssembly,c=a.Backup.是否公布考试结果;Global.CallBack.true_1(d,!c)}},postAjax:function(h){var b="ExaminerAnalysis-ExamResult",e=ExaminerAnalysis.ExamResult,d=e.DataAssembly,g=d.Backup.是否公布考试结果,c=Global.Boxy.Topics.Examiner.examResult(!g),i=this.Handle,a="设置",f=this.ElemAssembly.Other.提交按钮_是否公布考试结果;Global.Boxy.wait(b,{newTitle:a,dtHtml:a+c,triggerBtn:f});Global.AjaxServer.Post.json("/Analysis/PublicExamResult/",h,function(d){if(d.result)Global.Boxy.success(b,{newTitle:a+"成功",dtHtml:c+"已"+a+"成功.",boxyOptions:{afterHide:function(){i.completed()}}});else Global.Boxy.failure(b,{newTitle:a+"失败",dtHtml:c+a+"失败",newMessage:d.info})})},submit:function(){Global.MsgBox.submit(this,["是否公布考试结果"])}},Redirect:{toExamResult:function(){var a=Global.URL.arg(),b=a.TestID,c=a.DepartmentID||"";location.href="/Analysis/ExamResult?TestID="+b+"&HandleType=1&DepartmentID="+c},toExamReport:function(){var a=Global.URL.arg(),b=a.TestID,c=a.DepartmentID||"";location.href="/Analysis/ExamReport?TestID="+b+"&HandleType=1&DepartmentID="+c+"&ChartType=0&ScoreSection=10"}}}