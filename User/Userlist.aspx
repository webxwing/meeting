<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Userlist.aspx.cs" Inherits="meeting.User.Userlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户列表</title>
    <link href="../Content/themes/default/easyui.css" rel="stylesheet" />
    <link href="../Content/themes/icon.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.0.min.js"></script>
    <script src="../Scripts/jquery.easyui.min.js"></script>
    <script src="../Scripts/locale/easyui-lang-zh_CN.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_wraper">
            <table id="dg" title="用户列表" 
                data-options="rownumbers:true,singleSelect:true,pagination:true,url:'UserHandler.ashx',method:'get'">
                <thead>
                    <tr>
                        <th data-options="field:'ch',checkbox:true"></th>
                        <th data-options="field:'username',width:100,align:'center'">用户名</th>
                        <th data-options="field:'password',width:100,align:'center'">密码</th>
                        <th data-options="field:'actor',width:100,align:'center'">角色</th>
                    </tr>
                </thead>
            </table>
        </div>
    </form>
</body>
<script>
    $(function () {
        var pager = $('#dg').datagrid().datagrid('getPager');
        pager.pagination({
            buttons: [{
                iconCls: 'icon-add',
                handler: function () {
                    window.location.href = "AddUser.aspx";
                }
            }, {
                iconCls: 'icon-cancel',
                handler: function () {
                    var row = $('#dg').datagrid('getSelected');
                    if (row) {
                        $.messager.alert('Info', row.id);
                    }
                }
            }, {
                iconCls: 'icon-edit',
                handler: function () {
                    alert('edit');
                }
            }]
        });
    })
</script>
</html>
