<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="meeting.User.AddUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>添加用户</title>
    <link href="../Content/themes/default/easyui.css" rel="stylesheet" />
    <link href="../Content/themes/icon.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.0.min.js"></script>
    <script src="../Scripts/jquery.easyui.min.js"></script>
</head>
<body>
    <form id="AddUserForm" runat="server">
        <div class="div_wraper">
            <div class="easyui-panel" title="添加" style="width: 700px; height: auto; padding: 10px; background: #fafafa;">
                <div class="div_row">
                    <label for="username">用户名：</label>
                    <input type="text" name="username" class="easyui-validatebox" data-options="required:true,missingMessage:'用户名未填写！'" />
                </div>
                <div class="div_row">
                    <label for="password">密 码：</label>&nbsp;&nbsp;
            <input type="password" name="password" class="easyui-validatebox" data-options="required:true,missingMessage:'密码未填写！'" />
                </div>
                <div class="div_row">
                    <label for="actor">角 色：</label>&nbsp;&nbsp;
            <asp:DropDownList runat="server" class="easyui-combobox" ID="select_actor">
                <asp:ListItem Text="普通用户" Value="user" />
                <asp:ListItem Text="管理员" Value="admin" />
                <asp:ListItem Text="超级管理员" Value="superAdmin" />
            </asp:DropDownList>
                </div>
                <div class="div_row">
                    <asp:Button Text="确定" runat="server" class="easyui-linkbutton" ID="btnSubmit" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;
            <input type="reset" name="reset" value="重填" class="easyui-linkbutton" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
