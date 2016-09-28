<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingAddItems.aspx.cs" Inherits="meeting.Meeting.MeetingAddItems" ValidateRequest="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>议程添加</title>
    <link href="../Content/themes/default/easyui.css" rel="stylesheet" />
    <link href="../Content/themes/icon.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Scripts/FileUpload.js"></script>
    <script type="text/javascript" src="../Editor/kindeditor-all-min.js"></script>
    <script type="text/javascript" src="../Editor/lang/zh-CN.js"></script>
    <script type="text/javascript" src="../Scripts/app.js"></script>

    <style type="text/css">
        .textarea_abstruct {
            width: 418px;
            height: 120px;
        }

        input[type="text"] {
            width: 180px;
        }

        .title_center {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="MeetingAddItemsForm" class="form1" runat="server">
        <div class="div_wraper">
            <div class="title_center">
                <h2>
                    <asp:Label Text="text" runat="server" ID="m_title_label" />
                </h2>
            </div>

            <div class="easyui-panel" title="添加议程" style="width: 700px; height: auto; padding: 10px; background: #fafafa;">
                <div class="div_row">
                    <label for="item_number">序 号：</label>&nbsp;&nbsp;
                    <input class="easyui-numberspinner" data-options="min:1,max:50,required:true" name="item_number" style="width: 80px;" />
                </div>
                <div class="div_row">
                    <label for="item_title">标 题：</label>&nbsp;&nbsp;
                    <input type="text" name="item_title" class="easyui-validatebox" data-options="required:true,missingMessage:'标题未填写！'" style="width: 420px" />
                </div>
                <div class="div_row">
                    <label for="item_content">内 容：</label>&nbsp;&nbsp;
                    <textarea rows="" id="item_content" name="item_content" class="textarea_abstruct"></textarea>
                </div>
                <div class="div_row">
                    <label for="item_time">耗 时：</label>&nbsp;&nbsp;
                    <input class="easyui-numberspinner" data-options="min:1,max:999,required:true" name="item_time" style="width: 80px;" />
                    分钟
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
                    <asp:Button Text="确定" runat="server" class="easyui-linkbutton" ID="btnSubmit" OnClick="btnSubmit_Click" OnClientClick="return formAvlid();" />
                    &nbsp;&nbsp;
                    <input type="reset" name="reset" value="重填" class="easyui-linkbutton" />
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        KindEditor.ready(function (k) {
            window.editor = k.create('#item_content', {
                resizeType: 1,
                allowPreviewEmoticons: false,
                allowImageUpload: false,
                items: [
                    'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                    'insertunorderedlist', '|', 'emoticons', 'image', 'link']
            });
        });
    </script>
</body>
</html>
