<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingShengHe.aspx.cs" Inherits="meeting.Meeting.MeetingShengHe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <meta name="renderer" content="webkit" />
    <title>成都工业职业技术学院-会议助手后台管理系统</title>
    <meta name="keywords" content="成都工业职业技术学院,会议系统管理后台,移动APP后台管理" />
    <meta name="description" content="成都市工业职业技术完会议助手管理系统、支持移动智能终端（平板电脑、智能电视）使用的移动客户端应用程序APP" />
    <link href="../Content/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="../Content/themes/icon.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.0.min.js"></script>
    <script src="../Scripts/jquery.easyui.min.js"></script>

    <script src="../Scripts/app.js"></script>
    <script src="../Scripts/locale/easyui-lang-zh_CN.js"></script>
    <!--[if lt IE 9]>
    <meta http-equiv="refresh" content="0;info.html" />
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_wraper">
            <table id="dg" class="easyui-datagrid" title="会议列表"  data-options="rownumbers:true,singleSelect:true,pagination:true,url:'ashx/MeetingListNoPass.ashx',method:'post'">

            </table>
        </div>
    </form>
    <script src="../js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script src="../js/meeting/meetingShengHeData.js" type="text/javascript"></script>
</body>
</html>
