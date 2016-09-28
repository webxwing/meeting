<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingEditItems.aspx.cs" Inherits="meeting.Meeting.MeetingEditItems" ValidateRequest="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>议程修改</title>
    <link href="../Content/themes/default/easyui.css" rel="stylesheet" />
    <link href="../Content/themes/icon.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Scripts/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../Scripts/FileUpload.js"></script>
    <script type="text/javascript" src="../Editor/kindeditor-all-min.js"></script>
    <script type="text/javascript" src="../Editor/lang/zh-CN.js"></script>
    <script type="text/javascript" src="../Scripts/app.js"></script>
    
    <style type="text/css">
        .textarea_abstruct {
            width: 420px;
            height: 120px;
        }

        input[type="text"] {
            width: 180px;
        }

        ul {
            list-style-type: none;
            padding-left: 55px;
        }
    </style>
</head>
<body>
    <form id="MeetingEditItemsForm" class="form1" runat="server">
        <asp:HiddenField ID="hideFiles" runat="server" />
        <div class="div_wraper">
            <div class="title_center">
                <h2>
                    <asp:Label Text="text" runat="server" ID="m_title_label" />
                </h2>
            </div>
            <div class="easyui-panel" title="修改议程" style="padding: 10px; background: #fafafa;">
                <div class="div_row">
                    <label for="item_number">序 号：</label>&nbsp;&nbsp;
                    <input class="easyui-numberspinner" data-options="min:1,max:50,required:true" name="item_number" style="width: 80px;" id="item_number" runat="server" />
                </div>
                <div class="div_row">
                    <label for="item_title">标 题：</label>&nbsp;&nbsp;
                    <input type="text" name="item_title" class="easyui-validatebox" data-options="required:true,missingMessage:'标题未填写！'" style="width: 420px" id="item_title" runat="server" />
                </div>
                <div class="div_row">
                    <label for="item_content">内 容：</label>&nbsp;&nbsp;
                    <textarea runat="server" id="item_content" name="item_content" class="textarea_abstruct"></textarea>
                </div>
                <div class="div_row">
                    <label for="item_time">耗 时：</label>&nbsp;&nbsp;
                    <input class="easyui-numberspinner" data-options="min:1,max:999,required:true" name="item_time" style="width: 80px;" id="item_time" runat="server" />
                    分钟
                </div>
                <div class="div_row">
                    已上传附件：
                    <ul id="uploadFiles" runat="server">
                    </ul>
                </div>
                <div class="div_row">
                    <label>附 件：</label>&nbsp;&nbsp;
                    <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/ pub/shockwave/cabs /flash/swflash.cab#version=11,0,0,0" width="420" height="200" id="update" align="middle">
                        <param name="allowFullScreen" value="false" />
                        <param name="allowScriptAccess" value="always" />
                        <param name="movie" value="update.swf" />
                        <param name="quality" value="high" />
                        <param name="bgcolor" value="#ffffff" />
                        <embed src="../Content/update.swf" quality="high" bgcolor="#ffffff" width="420" height="200" name="update_" align="middle" allowscriptaccess="always" allowfullscreen="false"
                            type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
                    </object>
                </div>

                <div id="show" class="div_row">
                </div>
                <div class="div_row">
                    <asp:Button Text="确定" runat="server" class="easyui-linkbutton" ID="btnSubmit" OnClientClick="return formAvlid();" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;
                    <input type="reset" name="reset" value="重填" class="easyui-linkbutton" />
                </div>
            </div>
        </div>
    </form>
</body>
<script>
    $('#uploadFiles li a').click(function () {
        var hideFile = $('#hideFiles').val();
        var a_li = $(this).parents("li")[0];
        var item_value = a_li.getAttribute("item_value");
        a_li.remove();
        hideFile = hideFile.replace(item_value + ",", "");
        $('#hideFiles').val(hideFile);
    });
    KindEditor.ready(function (k) {
        window.editor = k.create('#item_content', {
            resizeType : 1,
            allowPreviewEmoticons : false,
            allowImageUpload : false,
            items : [
                'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                'insertunorderedlist', '|', 'emoticons', 'image', 'link']
        });
    });
</script>
</html>
