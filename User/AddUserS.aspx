<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUserS.aspx.cs" Inherits="meeting.User.AddUserS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <meta name="renderer" content="webkit" />
    <title>成都工业职业技术学院-会议助手后台管理系统</title>
    <meta name="keywords" content="成都工业职业技术学院,会议系统管理后台,移动APP后台管理" />
    <meta name="description" content="成都市工业职业技术完会议助手管理系统、支持移动智能终端（平板电脑、智能电视）使用的移动客户端应用程序APP" />
    <!--[if lt IE 9]>
    <meta http-equiv="refresh" content="0;info.html" />
    <![endif]-->
    <link rel="shortcut icon" href="../favicon.ico" />
    <link href="../css/bootstrap.min.css?v=3.3.6" rel="stylesheet" />
    <link href="../css/font-awesome.css?v=4.4.0" rel="stylesheet" />
    <link href="../css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="../css/animate.css" rel="stylesheet" />
    <link href="../css/style.css?v=4.1.0" rel="stylesheet" />

</head>
<body class="gray-bg">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-sm-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>添加用户</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                            <a class="dropdown-toggle" data-toggle="dropdown" href="MeetingAdds.aspx#">
                                <i class="fa fa-wrench"></i>
                            </a>
                            <ul class="dropdown-menu dropdown-user">
                                <li><a href="MeetingAdds.aspx#">选项1</a>
                                </li>
                                <li><a href="MeetingAdds.aspx#">选项2</a>
                                </li>
                            </ul>
                            <a class="close-link">
                                <i class="fa fa-times"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <form id="addUserForm" class="form-horizontal" action="AddUserS.aspx" method="post" runat="server">
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">用户名</label>
                                <div class="col-sm-10">
                                    <input type="text" id="u_username" name="u_username" class="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">密码</label>
                                <div class="col-sm-10">
                                    <input type="password"  class="form-control" name="u_password" id="u_password" runat="server" />
                                    <span class="help-block m-b-none">密码需要8位以上数字或字母！</span>
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">确认密码</label>
                                <div class="col-sm-10">
                                    <input type="password" class="form-control" name="u_re_password" id="u_re_password" runat="server" />
                                    
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">角色</label>
                                <div class="col-sm-10">
                                    <div class="radio i-checks">
                                        <label>
                                            <input type="radio" value="user" name="actor" checked="" runat="server"/> <i></i> 普通用户</label>
                                    </div>
                                    <div class="radio i-checks">
                                        <label>
                                            <input type="radio" value="admin" name="actor" runat="server"/> <i></i> 管理员</label>
                                    </div>
                                    <div class="radio i-checks">
                                        <label>
                                            <input type="radio" value="superAdmin" name="actor" runat="server"/> <i></i> 超级管理员</label>
                                    </div>
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>
                            <div class="form-group">
                                <div class="col-sm-4 col-sm-offset-2">
                                    <button class="btn btn-primary" type="submit">提交</button>
                                    <button id="btnClose" class="btn btn-primary" type="button">取消</button>
                                </div>
                            </div>
                            <script src="../js/jquery.min.js?v=2.1.4"></script>
                            <script src="../js/bootstrap.min.js?v=3.3.6"></script>
                            <script src="../js/plugins/validate/jquery.validate.min.js"></script>
                            <script src="../js/plugins/validate/messages_zh.min.js"></script>
                            <script src="../js/content.js?v=1.0.0"></script>

                            <script src="../js/plugins/layer/laydate/laydate.js"></script>
                            <script src="../js/form-validate.js"></script>
                            <script src="../js/plugins/layer/layer.min.js"></script>
                            <script type="text/javascript">
                                $(document).ready(function () {
                                    $('#btnClose').click(function () {
                                        //重置表单
                                        $('#addUserForm')[0].reset();
                                        closeLayer();
                                    });
                                });
                                //关闭layer
                                function closeLayer() {
                                    var index = parent.layer.getFrameIndex(window.name);
                                    parent.layer.close(index);
                                }
                            </script>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
