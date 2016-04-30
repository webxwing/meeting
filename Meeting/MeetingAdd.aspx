<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingAdd.aspx.cs" Inherits="meeting.Metting.AddMeeting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加会议</title>
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
    <form id="MeetingAddForm" class="form1" runat="server">
        <div class="div_wraper">
            <div class="easyui-panel" title="添加" style="width: 700px; height: auto; padding: 10px; background: #fafafa;">
                <div class="div_row">
                    <label for="m_title">标 题：</label>&nbsp;&nbsp;
                    <input type="text" name="m_title" class="easyui-validatebox" data-options="required:true,missingMessage:'标题未填写！'" style="width:420px" />
                </div>
                <div class="div_row">
                    <label for="m_theme">主 题：</label>&nbsp;&nbsp;
                    <textarea rows="" name="m_theme" class="textarea_abstruct"></textarea>
                </div>
                <div class="div_row">
                    <label for="m_abstruct">简 介：</label>&nbsp;&nbsp;
                    <textarea rows="" name="m_abstruct" class="textarea_abstruct"></textarea>
                </div>
                <div class="div_row">
                    <label for="m_place">地 点：</label>&nbsp;&nbsp;
                    <input type="text" name="m_place" />
                </div>
                <div class="div_row">
                    <label for="m_date">时 间：</label>&nbsp;&nbsp;
                    <input type="text" name="m_date" class="easyui-datetimebox easyui-validatebox" data-options="validType:'TimeCheck[\'m_date\']',showSeconds:false" />
                </div>
                <div class="div_row">
                    <label for="m_compere">主持人：</label>
                    <input type="text" name="m_compere" value=" " />
                </div>
                <div class="div_row">
                    <label for="m_code">会议码：</label>
                    <input type="text" name="m_code" value=" " />
                    <input type="button" name="btn_addCode" class="easyui-linkbutton" value="自动生成" onclick="auto_m_code();" />
                </div>
                <div class="div_row">
                    <asp:Button Text="确定" runat="server" class="easyui-linkbutton" ID="btnSubmit"  OnClick="btnSubmit_Click" OnClientClick="return formAvlid();"/>
                    &nbsp;&nbsp;
                    <input type="reset" name="reset" value="重填" class="easyui-linkbutton" />
                </div>
            </div>
        </div>
    </form>
</body>
<script>
</script>
</html>
