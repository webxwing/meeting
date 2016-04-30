<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingEdit.aspx.cs" Inherits="meeting.Meeting.MeetingEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑会议</title>
     <link href="../Content/themes/default/easyui.css" rel="stylesheet" />
    <link href="../Content/themes/icon.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.0.min.js"></script>
    <script src="../Scripts/jquery.easyui.min.js"></script>
    <script src="../Scripts/app.js"></script>
    <style>
        .textarea_abstruct{
            width:420px;
            height:120px;
        }
        input[type="text"]{
            width:180px;
        }
    </style>
</head>
<body>
    <form id="MeetingEditForm" class="form1" runat="server">
    <div class="div_wraper">
        <div class="easyui-panel" title="编辑" style="width: 700px; height: auto; padding: 10px; background: #fafafa;">
            <div class="div_row">
                <label for="m_title">标 题：</label>&nbsp;&nbsp;
                <input type="text" id="m_title" runat="server" name="m_title" class="easyui-validatebox" data-options="required:true,missingMessage:'标题未填写！'" style="width:420px" />
            </div>
            <div class="div_row">
                <label for="m_theme">主 题：</label>&nbsp;&nbsp;
                <textarea id="m_theme" runat="server" name="m_theme" class="textarea_abstruct"></textarea>
            </div>
            <div class="div_row">
                <label for="m_abstruct">简 介：</label>&nbsp;&nbsp;
                <textarea id="m_abstruct" runat="server" name="m_abstruct" class="textarea_abstruct"></textarea>
            </div>
            <div class="div_row">
                <label for="m_place">地 点：</label>&nbsp;&nbsp;
                <input type="text" id="m_place" runat="server" name="m_place" />
            </div>
            <div class="div_row">
                <label for="m_date">时 间：</label>&nbsp;&nbsp;
                <input type="text" id="m_date" runat="server" name="m_date" class="easyui-datetimebox easyui-validatebox" data-options="validType:'TimeCheck[\'m_date\']',showSeconds:false" />
            </div>
            <div class="div_row">
                <label for="m_compere">主持人：</label>
                <input type="text" id="m_compere" runat="server" name="m_compere" value=" " />
            </div>
            <div class="div_row">
                <label for="m_code">会议码：</label>
                <input type="text" id="m_code" runat="server" name="m_code" value=" " />
                <input type="button" name="btn_addCode" class="easyui-linkbutton" value="自动生成" onclick="auto_m_code();" />
            </div>
            <div class="div_row">
                <asp:Button Text="修改" runat="server" class="easyui-linkbutton" ID="btnSubmit"   OnClientClick="return formAvlid();" OnClick="btnSubmit_Click"/>
                &nbsp;&nbsp;
                <input type="reset" name="reset" value="重填" class="easyui-linkbutton" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
