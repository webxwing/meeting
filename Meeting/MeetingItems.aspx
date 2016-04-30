<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingItems.aspx.cs" Inherits="meeting.Meeting.MeetingItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>议程列表</title>
    <link href="../Content/themes/default/easyui.css" rel="stylesheet" />
    <link href="../Content/themes/icon.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.0.min.js"></script>
    <script src="../Scripts/jquery.easyui.min.js"></script>
    <script src="../Scripts/app.js"></script>
    <style>
        .title_center{
            text-align:center;
        }
    </style>
</head>
    
<body>
    <form id="form1" runat="server">
    <div class="div_wraper">
        <div class="title_center">
                <h2>
                    <asp:Label Text="text" runat="server" ID="m_title_label" />
                </h2>
            </div>
        <table id="dg" title="用户列表" style="width: 700px; height: 350px"
                data-options="rownumbers:true,singleSelect:true,pagination:true,method:'get',toolbar:'#tb_tool'">
                <thead>
                    <tr>
                        <th data-options="field:'ch',checkbox:true"></th>
                        <th data-options="field:'item_title',width:260,align:'left'">名称</th>
                        <th data-options="field:'item_time',width:80,align:'center'">用时(分钟)</th>
                        <th data-options="field:'item_state',width:100,align:'center'">状态</th>
                        <th data-options="field:'item_number',width:100,align:'center'">顺序</th>
                    </tr>
                </thead>
            </table>
    </div>
    <div id="tb_tool" style="padding:5px;height:auto">
        <div style="margin-bottom:5px;">
            <a href="#" class="easyui-linkbutton" iconCls="icon-application_start" plain="true" id="start">开启</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-application_stop" plain="true" id="over">结束</a>
            <a href="#" class="easyui-linkbutton" iconCls=" icon-application_side_expand" plain="true" id="restart">重启</a>
        </div>
    </div>
    </form>
</body>
    <script>
        $(function () {
        var m_id = getQueryString('m_id');
        if (m_id != null && m_id != "") {
            $('#dg').datagrid({
                url: 'MeetingItemsHandler.ashx?m_id=' + m_id
            });
        }
        var pager = $('#dg').datagrid().datagrid('getPager');
        pager.pagination({
            pageSize: 10,
            pageList: [5, 10, 15],
            beforePageText: '第',
            afterPageText: '页  共{pages}页',
            displayMsg:'当前{from}-{to}',
            buttons: [{
                iconCls: 'icon-add',
                text:'添加',
                handler: function () {
                    window.location.href = "MeetingAddItems.aspx?m_id=" + m_id;
                }
            }, {
                iconCls: 'icon-remove',
                text:'删除',
                handler: function () {
                    
                }
            }, {
                iconCls: 'icon-edit',
                text:'编辑',
                handler: function () {
                    var row = $('#dg').datagrid('getSelected');
                    window.location.href = "MeetingEditItems.aspx?m_id=" + m_id +"&item_id=" + row.id;
                }
            }, {
                iconCls: 'icon-undo',
                text: '上移',
                handler: function () {

                }
            }, {
                iconCls: 'icon-redo',
                text: '下移',
                handler: function () {

                }
            }]
        });
        $('#start').click(function () {
            var row = $('#dg').datagrid('getSelected');
            if (row && m_id != null && m_id != "") {
                
            }else
            {

            }
        });
        $('#over').click(function () {
            var row = $('#dg').datagrid('getSelected');
            if (row && m_id != null && m_id != "") {

            }
        })
    })
</script>
</html>
