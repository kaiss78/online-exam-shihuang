<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="E_S_Layout" style="min-width: 780px;">
    <!--style="margin:30px 0px 0px 45px;"-->
    <div class="E_S_L_Masks" id="ID_Masks_Box">
        数据正在加载中......</div>
    <div style="clear: both;">
    </div>
    <div class="E_S_L_Bar">
        <div class="E_S_L_B_Mark">
            所属题型：</div>
        <div class="E_S_L_B_Auto" id="ID_QuestionTypes_Box">
            <a class="E_S_L_B_QuestionTypes E_S_L_B_Q_S_Link" mark="" title="选择题型" style="display: none;"
                href="javascript:void(0)" onclick="IFrameQuestionTypesManage.show(this);"><span class="E_S_L_B_Q_Value">
                </span></a><a class="E_S_L_B_QuestionTypes"><span class="E_S_L_B_Q_Value"></span>
                    <samp class="E_S_L_B_Q_State">
                        [修改试题]</samp>
                </a>
        </div>
    </div>
    <div class="E_S_L_Hidden" id="ID_Hidden_Box">
        <div class="E_S_L_Bar" id="ID_MoreHtml_Box" style="margin-top: 0px;">
        </div>
        <div class="E_S_L_Bar unxtbj">
            <div class="E_S_L_B_Mark">
                分类：</div>
            <div id="ID_Sort_Box" style="float: left;padding-top: 5px; position: relative;">
            </div>
        </div>
        <div class="E_S_L_Bar unxtbj">
            <div class="E_S_L_B_Mark">
                难易度：</div>
            <div class="E_S_L_B_Difficult" id="ID_Difficult_Box">
            </div>
            <span class="E_S_L_B_Difficult_Amount" id="ID_Difficult_Amount"></span>
        </div>
        <div class="E_S_L_Bar unxtbj hidden">
            <div class="E_S_L_B_Mark">
                是否公开：</div>
            <div class="E_S_L_B_Fix E_S_L_B_Radio" id="ID_Public_Box">
                <input id="RadioPublic1" type="radio" name="Public" onclick="OutsideManage.setPublic('1');"
                    checked="checked" />
                <label for="RadioPublic1">
                    公开</label>
                <input id="RadioPublic2" type="radio" name="Public" onclick="OutsideManage.setPublic('0');"
                    style="margin-left: 6px; *margin-left: 2px;" />
                <label for="RadioPublic2">
                    不公开</label>
            </div>
            <div class="E_S_L_B_Fix E_S_L_B_Radio" style="display: none;">
                <span>是</span><span style="color: #999;">(已公开不能更改)</span>
            </div>
        </div>
        <div class="E_S_L_Bar" style="height: 100px; margin-top: 10px;">
            <div class="E_S_L_B_Mark">
            </div>
            <div class="E_S_L_B_Fix E_S_L_B_Handle">
                <a class="E_S_L_B_H_base" title="智能识别输入试题" href="javascript:void(0)" onclick="Recog试题管理.显示识别输入(this,null,null);">
                    识 别 输 入</a> <a class="E_S_L_B_H_base" title="预览该试题内容" href="javascript:void(0)" onclick="View试题管理.显示试题预览(null);">
                        预 览 试 题</a>
                <div class="E_S_L_B_H_base">
                    <a class="E_S_L_B_H_B_left" href="javascript:void(0)" title="提交" onclick="ReferServer.submit(0);">
                        &nbsp;提&nbsp;&nbsp;&nbsp;交</a> <a class="E_S_L_B_H_B_right0" href="javascript:void(0)"
                            onmouseover="ReferServer.save_sel_mouse(this,'over');" onmouseout="ReferServer.save_sel_mouse(this,'out');"
                            onclick="ReferServer.save_sel(this);" title="选择保存类型"></a>
                </div>
            </div>
        </div>
    </div>
</div>
