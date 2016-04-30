<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="meeting.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>成都工业职业技术学院-会议助手后台管理系统</title>
    <link type="text/css" href="Content/themes/metro/easyui.css" rel="stylesheet" id="swicth-style"/>
    <link href="Content/app.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.0.min.js"></script>
    <script src="Scripts/jquery.easyui.min.js"></script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div region="north" border="true" class="cs-north">
            <div class="cs-north-bg">
                <div class="cs-north-logo">会议助手-管理系统</div>
                    <ul class="ui-skin-nav">
                        <li class="li-skinitem" title="gray"><span class="gray" rel="gray"></span></li>
                        <li class="li-skinitem" title="default"><span class="default" rel="default"></span></li>
                        <li class="li-skinitem" title="bootstrap"><span class="bootstrap" rel="bootstrap"></span></li>
                        <li class="li-skinitem" title="black"><span class="black" rel="black"></span></li>
                        <li class="li-skinitem" title="metro"><span class="metro" rel="metro"></span></li>
                    </ul>   
            </div>
        </div>
        <div region="west" border="true" split="true" title="导航" class="cs-west">
            <div class="easyui-accordion" fit="true" border="false">
                <div title="用户管理">
                    <p>
                        <a href="javascript:void(0)" src="User/Userlist" class="cs-navi-tab">用户列表</a>
                    </p>
                    <p>
                        <a href="javascript:void(0)" src="User/AddUser" class="cs-navi-tab">添加用户</a>
                    </p>
                    <p>
                        <a href="javascript:void(0)" src="User/Actor" class="cs-navi-tab">角色管理</a>
                    </p>
                    <p>
                        <a href="javascript:void(0)" src="User/Power" class="cs-navi-tab">权限设置</a>
                    </p>
                </div>
                <div title="会议管理">
                    <p>
                        <a href="javascript:void(0)" src="Meeting/MeetingList" class="cs-navi-tab">会议列表</a>
                    </p>
                    <p>
                        <a href="javascript:void(0)" src="Meeting/MeetingAdd" class="cs-navi-tab">会议添加</a>
                    </p>
                    <p>
                        <a href="javascript:void(0)" src="Meeting/MeetingItemList" class="cs-navi-tab">议程列表</a>
                    </p>
                    <p>
                        <a href="javascript:void(0)" src="Meeting/MeetingFiles" class="cs-navi-tab">附件管理</a>
                    </p>
                </div>
                <div title="系统">
                    <p>
                        <a href="javascript:void(0)" src="System/Setting" class="cs-navi-tab">系统设置</a>
                    </p>
                    <p>
                        <a href="javascript:void(0)" src="System/Client" class="cs-navi-tab">客户端设置</a>
                    </p>
                    <p>
                        <a href="javascript:void(0)" src="System/About" class="cs-navi-tab">关于</a>
                    </p>
                    <p>
                        <a href="javascript:window.location.href='index.aspx'" class="cs-navi-tab">退出</a>
                    </p>
                </div>
            </div>

        </div>
        <div id="mainPanle" region="center" border="true" border="false">
            <div id="tabs" class="easyui-tabs" fit="true" border="false">
                <div title="主页">
                    <div class="cs-home-remark">
                        欢迎界面
                    </div>
                </div>
            </div>
        </div>
        <div region="south" border="false" id="south">
            <center>成都工业职业技术学院-信息中心  版权所有 @ 2015-2016</center>
        </div>
        <div id="mm" class="easyui-menu">
            <div id="mm-tabupdate">刷新</div>
            <div class="menu-sep"></div>
            <div id="mm-tabclose">关闭</div>
            <div id="mm-tabcloseother">关闭其它</div>
            <div id="mm-tabcloseall">关闭全部</div>
        </div>
    </form>
</body>
<script src="Scripts/app.js"></script>
<script>
    $(function () {
        tabCloseEven();

        $('.cs-navi-tab').click(function () {
            var $this = $(this);
            var href = $this.attr('src');
            var title = $this.text();
            addTab(title, href);
        });

        var themes = {
            'gray': 'Content/themes/gray/easyui.css',
            'black': 'Content/themes/black/easyui.css',
            'bootstrap': 'Content/themes/bootstrap/easyui.css',
            'default': 'Content/themes/default/easyui.css',
            'metro': 'Content/themes/metro/easyui.css'
        };

        var skins = $('.li-skinitem span').click(function () {
            var $this = $(this);
            if ($this.hasClass('cs-skin-on')) return;
            skins.removeClass('cs-skin-on');
            $this.addClass('cs-skin-on');
            var skin = $this.attr('rel');
            $('#swicth-style').attr('href', themes[skin]);
            setCookie('cs-skin', skin);
            skin == 'dark-hive' ? $('.cs-north-logo').css('color', '#FFFFFF') : $('.cs-north-logo').css('color', '#000000');
        });

        if (getCookie('cs-skin')) {
            var skin = getCookie('cs-skin');
            $('#swicth-style').attr('href', themes[skin]);
            $this = $('.li-skinitem span[rel=' + skin + ']');
            $this.addClass('cs-skin-on');
            skin == 'dark-hive' ? $('.cs-north-logo').css('color', '#FFFFFF') : $('.cs-north-logo').css('color', '#000000');
        }

    });
</script>
</html>
