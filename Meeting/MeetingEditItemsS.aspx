<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingEditItemsS.aspx.cs" Inherits="meeting.Meeting.MeetingEditItemsS" ValidateRequest="false" %>

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
    <link href="../css/animate.css" rel="stylesheet" />
    <link href="../css/plugins/simditor/simditor.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../css/plugins/webuploader/webuploader.css" />
    <link href="../css/style.css?v=4.1.0" rel="stylesheet" />

</head>
<body class="gray-bg">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-sm-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5 id="h5" runat="server"></h5>
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
                        <form id="addItemForm" class="form-horizontal" action="MeetingEditItemsS.aspx" method="post" runat="server">
                            <asp:HiddenField ID="hfFilePath" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="hfM_id" runat="server" />
                            <asp:HiddenField ID="hfItem_id" runat="server" />
                            <asp:HiddenField ID="hfItem_num" runat="server" />
                            <asp:HiddenField ID="hfOldFile" runat="server" />
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">标题</label>
                                <div class="col-sm-10">
                                    <input type="text" id="item_title" name="item_title" class="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">内容</label>
                                <div class="col-sm-10">
                                    <textarea id="item_content" rows="5" name="item_content" runat="server" placeholder="这里输入议程内容"></textarea>
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">耗时</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id="item_time" name="item_time" runat="server" />
                                    <span class="help-block m-b-none">预计议程所需要的时间，单位是分钟！</span>
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-sm-2 control-label">附件</label>
                                <div class="col-sm-10">
                                    <div id="uploader">
                                        <div id="thelist" class="uploader-list"></div>
                                        <div class="btns">
                                            <div id="picker">选择文件</div>
                                            <button type="button" id="ctlBtn" class="btn btn-success">开始上传</button>
                                        </div>
                                    </div>
                                    <span class="help-block m-b-none">支持上传的文件类型为：doc,docx,xls,xlsx,ppt,pptx,pdf,gif,jpg,jpeg,png,txt</span>
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>
                            <div class="form-group">
                                <div class="col-sm-4 col-sm-offset-2">
                                    <button class="btn btn-primary disabled" type="submit">提交</button>
                                    <button id="btnClose" class="btn btn-primary" type="button">取消</button>
                                </div>
                            </div>
                            <script src="../js/jquery.min.js?v=2.1.4"></script>
                            <script src="../js/bootstrap.min.js?v=3.3.6"></script>
                            <script src="../js/content.js?v=1.0.0"></script>
                            <script src="../js/plugins/validate/jquery.validate.min.js"></script>
                            <script src="../js/plugins/validate/messages_zh.min.js"></script>
                            <script src="../js/plugins/layer/laydate/laydate.js"></script>
                            <script src="../js/form-validate.js"></script>
                            <script src="../js/plugins/layer/layer.min.js"></script>

                            <script src="../js/plugins/simditor/module.js"></script>
                            <script src="../js/plugins/simditor/uploader.js"></script>
                            <script src="../js/plugins/simditor/hotkeys.js"></script>
                            <script src="../js/plugins/simditor/simditor.js"></script>
                            <script src="../js/plugins/webuploader/webuploader.min.js"></script>
                            <script src="../js/meeting/webuploader-additems.js"></script>
                            <script>
                                $(document).ready(function () {
                                    var tool = ['title', 'bold', 'italic', 'underline', 'fontScale', 'color', '|', 'ol', 'ul', 'code', 'table', '|', 'link', 'image', 'hr', '|', 'indent', 'outdent', 'alignment'];
                                    var editor = new Simditor({
                                        textarea: $('#item_content'),
                                        toolbar: tool
                                    });
                                    $('#btnClose').click(function () {
                                        //重置表单
                                        $('#addItemForm')[0].reset();
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
