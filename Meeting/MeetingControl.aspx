<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingControl.aspx.cs" Inherits="meeting.Meeting.MeetingControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    会议状态：<select id="controlSel" class="easyui-combobox" panelHeight="auto" style="width:100px;padding:5px 20px;">
            <option value="进行中">开启</option>
            <option value="已结束">结束</option>
            <option value="未开始">重启</option>
        </select><br />
        <br />
        <br />
        <span id="info" style="color:red;" ></span>
        <br />
        <a href="#" class="easyui-linkbutton" iconCls="icon-ok" id="btnOk">确定</a>
        &nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" id="btnCancel">取消</a>
    </div>
    </form>
</body>
    <script>
        $('#btnOk').click(function () {
            var controlVal = $('#controlSel').combobox('getValue');
            var m_id = getQueryString("m_id");
            $('#controlW').dialog('close');
            if (m_id != null) {
                var controlItme = { 'm_id': m_id, 'cState': controlVal };
                $.ajax({
                    type:'post',
                    url: 'MeetingControlHandler.ashx',
                    data: controlItme,
                    success: function (data) {
                        if (data == "ok") {
                            $.messager.alert('提示', '修改成功！', 'info');
                            $('#info').text = "修改成功！";
                        } else {
                            $('#info').text = "修改失败！";
                        }
                    },
                    error: function (msg) {
                        $('#info').text = "修改失败！错误信息：" + msg;
                    }
                });                
            }
            else {
                $('#info').text = "失败！";
            }
        });
    </script>
</html>
