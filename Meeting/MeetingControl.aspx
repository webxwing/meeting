<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingControl.aspx.cs" Inherits="meeting.Meeting.MeetingControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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

    <link href="../css/style.css?v=4.1.0" rel="stylesheet" />
</head>
<body class="gray-bg">
    <div class="wrapper wrapper-content">
        <form method="post" class="form-horizontal">
            <div class="row">
                <div class="col-sm-10 col-sm-offset-2">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">改变会议状态</label>
                        <div class="col-sm-10">
                            <div class="radio">
                                <label>
                                    <input type="radio" checked="checked" value="进行中" id="optionIng" name="optionsControl" />开启       
                                </label>
                                <label>
                                    <input type="radio" checked="" value="已结束" id="optionIsOver" name="optionsControl" />结束       
                                </label>
                                <label>
                                    <input type="radio" checked="" value="未开始" id="optionIsBefore" name="optionsControl" />重启       
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-4 col-sm-offset-2">
                            <button class="btn btn-primary" id="btnOk" type="button">提交</button>
                            <button id="btnClose" class="btn btn-primary" type="button">取消</button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
<script src="../js/jquery.min.js?v=2.1.4" type="text/javascript"></script>
<script src="../js/bootstrap.min.js?v=3.3.6" type="text/javascript"></script>

<script src="../js/content.js?v=1.0.0" type="text/javascript"></script>
<script src="../js/plugins/layer/laydate/laydate.js" type="text/javascript"></script>

<script src="../js/plugins/layer/layer.min.js"></script>
<script>
    $('document').ready(function () {
        $('#btnClose').click(function () { closeWindow(); })
        $('#btnOk').click(function () {
            var controlVal = $("input[name='optionsControl']:checked").val();
            var m_id = getQueryString("m_id");
            if (m_id != null) {
                var controlItme = { 'm_id': m_id, 'cState': controlVal };
                //console.log(controlItme);
                $.ajax({
                    type: 'post',
                    url: 'MeetingControlHandler.ashx',
                    data: controlItme,
                    success: function (data) {
                        if (data == "ok") {
                            parent.layer.msg("修改成功！", { time: 2000 });
                            closeWindow();
                        } else {
                            parent.layer.msg("修改失败，请重试！", { time: 2000 });
                        }
                    },
                    error: function (msg) {
                        parent.layer.msg("服务器发生错误！", { time: 2000 });
                    }
                });
            }
            else {
                parent.layer.msg("修改失败，请重试！", { time: 2000 });
            }

        });
    });
    function closeWindow() {
        var index = parent.layer.getFrameIndex(window.name);
        parent.layer.close(index);
    }
</script>
</html>
