<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingList.aspx.cs" Inherits="meeting.Meeting.MeetingList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会议列表</title>
    <link href="../Content/themes/default/easyui.css" rel="stylesheet" />
    <link href="../Content/themes/icon.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.0.min.js"></script>
    <script src="../Scripts/jquery.easyui.min.js"></script>
    <script src="../Scripts/app.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_wraper">
            <table id="dg" title="会议列表" style="width: 800px;" data-options="rownumbers:true,singleSelect:true,pagination:true,url:'MeetingHandler.ashx',method:'post'">
                <thead>
                    <tr>
                        <th data-options="field:'ch',checkbox:true"></th>
                        <th data-options="field:'m_title',width:280,aligh:'center'">标题</th>
                        <th data-options="field:'m_date',width:140,aligh:'center'">时间</th>
                        <th data-options="field:'m_place',width:100,aligh:'center'">地点</th>
                        <th data-options="field:'m_state',width:80,aligh:'center'">状态</th>
                    </tr>
                </thead>
            </table>
        </div>
    </form>
    <div id="controlW" class="easyui-dialog" title="切换状态" style="width: 240px; height: 160px; padding: 10px;">
        会议状态：<select id="controlSel" class="easyui-combobox" panelheight="auto" style="width: 100px; padding: 5px 20px;">
            <option value="进行中">开启</option>
            <option value="已结束">结束</option>
            <option value="未开始">重启</option>
        </select><br />
        <br />
        <br />
        <br />
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" id="btnOk">确定</a>
        &nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" id="btnCancel">取消</a>

    </div>
    <script>
        $(function () {
            $('#controlW').dialog('close');
            var pager = $('#dg').datagrid().datagrid('getPager');
            pager.pagination({
                pageSize: 10,
                pageList: [5, 10, 15],
                beforePageText: '第',
                afterPageText: '页  共{pages}页',
                displayMsg: '当前 {from} - {to}  ',
                buttons: [{
                    iconCls: 'icon-add',
                    text: '添加',
                    handler: function () {
                        window.location.href = 'MeetingAdd.aspx';
                    }
                }, {
                    iconCls: 'icon-remove',
                    text: '删除',
                    handler: function () {
                        var row = $('#dg').datagrid('getSelected');
                        if (row) {
                            alert("连同该会议的议程将一并删除？");
                        }
                    }
                }, {
                    iconCls: 'icon-edit',
                    text: '编辑',
                    handler: function () {
                        var row = $('#dg').datagrid('getSelected');
                        if (row) {
                            window.location.href = "MeetingEdit.aspx?m_id=" + row.id;
                        }
                    }
                }, {
                    iconCls: 'icon-redo',
                    text: '议程管理',
                    handler: function () {
                        var row = $('#dg').datagrid('getSelected');
                        if (row) {
                            window.location.href = "MeetingItems.aspx?m_id=" + row.id;
                        }
                    }
                }, {
                    iconCls: 'icon-tip',
                    text: '控制',
                    handler: function () {
                        var row = $('#dg').datagrid('getSelected');
                        if (row) {
                            $('#controlW').dialog('open');
                        }
                        else {
                            $.messager.alert('错误', '请先选择需要操作的会议！', 'warning');
                        }
                    }
                }]
            });
            //控制按钮
            $('#btnOk').click(function () {
                var row = $('#dg').datagrid('getSelected');
                var controlVal = $('#controlSel').combobox('getValue');
                if (controlVal != row.m_state) {
                    var controlItme = { 'm_id': row.id, 'cState': controlVal };
                    getData('../Meeting/MeetingControlHandler.ashx', controlItme, function (data) {
                        if (data == "ok") {
                            $.messager.alert('提示', '修改成功！', 'info');
                            $('#dg').datagrid('reload');
                        }
                    }, function (msg) {
                        $.messager.alert('提示', '修改失败！错误代码：' + msg, 'info');
                    });
                } else {
                    $.messager.alert('提示', '当前状态已是' + row.m_state + '！', 'info');
                }
                $('#controlW').dialog('close');
            });
            $('#btnCancel').click(function () {
                $('#controlW').dialog('close');
            });
        });
        
    </script>
</body>
</html>
