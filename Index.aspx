<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="meeting.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>成都工业职业技术学院-会议助手后台管理系统</title>   
    <link href="../Content/themes/default/easyui.css" rel="stylesheet" />
    <link href="../Content/themes/icon.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.0.min.js"></script>
    <script src="../Scripts/jquery.easyui.min.js"></script>
    <style>
        body{
            margin:0;
            background-color:#eaeef1;
            font-size:14px;
            display:block;
        }
        .div_login{
            background-image:url("images/login_bg.png");
            margin:50px auto;
            width:706px;
            height:444px;
        }
        .login_up{
            height:300px;
            margin-bottom:25px;
        }
        .login_bootom_row{
            padding-top:10px;
            padding-left:145px;
            font-size:14px;
            color:black;
            line-height:35px;
        }
        .div_float_right{
            float:right;
            padding-right:68px;
        }
        #txt_count,#txt_pass{
            width:160px;
            line-height:20px;
        }
        #btn_login{
            line-height:23px;
        }
        .question{
            font-size:12px;            
            line-height:14px;
            padding-right:14px;
        }
        .question a,.question a:hover{
            color:#808080;
            text-decoration:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_login">
        <div class="login_up">

        </div>
        <div class="login_bootom">
            <div class="login_bootom_row">
                <asp:Label Text="账号：" runat="server" />
                <asp:TextBox runat="server" ID="txt_count" />&nbsp;&nbsp;
                <asp:Label Text="密码:" runat="server" />
                <asp:TextBox runat="server" type="password" ID="txt_pass"/>
            </div>
        </div>
        <div class="login_bootom_row div_float_right">
            <span class="question"><a href="#">忘记密码？</a></span>
            <asp:Button Text="登陆" runat="server" ID="btn_login" OnClick="btn_login_Click" CssClass="easyui-button" />
        </div>
        
    </div>
    </form>
</body>
</html>
