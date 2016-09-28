<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingAddS.aspx.cs" Inherits="meeting.Meeting.MeetingAddS" %>

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
                        <h5>添加会议</h5>
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
                        <form id="addMeetingForm" class="form-horizontal" action="MeetingAdds.aspx" method="post" runat="server">
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">标题</label>
                                <div class="col-sm-10">
                                    <input type="text" id="m_title" name="m_title" class="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">主题</label>
                                <div class="col-sm-10">
                                    <textarea rows="3" type="text" class="form-control" id="m_theme" name="m_theme" runat="server"></textarea>
                                    <span class="help-block m-b-none">会议类别说明！</span>
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">简介</label>
                                <div class="col-sm-10">
                                    <textarea rows="3" type="text" class="form-control" id="m_abstruct" name="m_abstruct" runat="server"></textarea>
                                    <span class="help-block m-b-none">会议的简单情况说明！</span>
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">地点</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id="m_place" name="m_place" runat="server" />
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">日期时间</label>
                                <div class="col-sm-10">
                                    <input class="form-control layer-date" placeholder="YYYY-MM-DD hh:mm:ss" id="m_date" name="m_date" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })" runat="server" />
                                    <label class="laydate-icon"></label>
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">会议主持人</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id="m_compere" name="m_compere" runat="server" />
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">会议验证码</label>
                                <div class="col-sm-10">
                                    <div class="input-group">
                                        <input type="text" name="m_code" id="m_code" class="form-control" runat="server" /><span class="input-group-btn">
                                            <button type="button" class="btn btn-primary" onclick="auto_m_code()">自动生成</button></span>
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
                                        $('#addMeetingForm')[0].reset();
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
