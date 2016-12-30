//定义全局站点信息
var BASE_URL = '../js/plugins/webuploader/Uploader.swf';
//var jsonData = {
//fileList: []
//};
var jsonData = [];
function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}
var $list = $('#thelist'),
    $btn = $('#ctlBtn'),
    state = 'pending';
if (!WebUploader.Uploader.support()) {
    var error = "附件上传不支持您的浏览器！请尝试升级flash版本或者使用Chrome引擎的浏览器。<a target='_blank' href='http://se.360.cn'>下载页面</a>";
    if (window.console) {
        window.console.log(error);
    }
    parent.layer.msg(error, { time: 5000 });
}
var uploader = WebUploader.create({
    swf: BASE_URL,
    server: '../../FileUpload/uploadHandler.ashx',
    accept: { title: 'files', extensions: 'doc,docx,xls,xlsx,ppt,pptx,pdf,gif,jpg,jpeg,png,txt', mimeTypes: '*' },
    fileNumLimit: 10,
    fileSingleSizeLimit: 1 * 1024 * 1024, //10兆
    pick: '#picker'
});
var hdFileData = $('#hfFilePath');
var fileDataStr = hdFileData.val();
if (fileDataStr) {
    jsonData = JSON.parse(fileDataStr);
    $.each(jsonData, function (index, fileData) {
        var newid = S4();
        fileData.queueId = newid;
        $list.append('<div id="' + newid + '" class="item">' +
        '<div class="info">' + fileData.name + '</div>' +
        '<p class="state"><span class=\"text-danger\">已上传</span></p>' +
        '</div><hr/>');
    });
    hdFileData.val(JSON.stringify(jsonData));
}
// 当有文件被添加进队列的时候
uploader.on('fileQueued', function (file) {
    $('#thelist').append('<div id="' + file.id + '" class="item">' +
        '<h4 class="info">' + file.name + '</h4>' +
        '<p class="state">等待上传...<a href="javascript:void(0);" class="btn btn-link deleteLink">移除</a><hr/></p>' +
    '</div>');
});
//移除文件
$(document).on("click", ".deleteLink", function (event) {
    var fileItem = $(this).parent().parent();
    uploader.removeFile($(fileItem).attr("id"), true);
    $(fileItem).fadeOut(function () {
        $(fileItem).remove();
    });
    event.stopPropagation();
});
//删除原有附件
$(document).on("click", ".deleteOldLink", function (event) {
    console.log('deleteOldLinky');
    var fileItem = $(this).parent().parent();
    var hfOldFile = $('#hfOldFile');
    //无原有附件
    if (hfOldFile == null || hfOldFile.val() == "") {
        return;
    }
    var oldFiles = JSON.parse(hfOldFile.val());
    var k = -1;
    for (var i = 0; i < oldFiles.length; i++) {
        if (oldFiles[i].queueId == $(fileItem).attr("id")) {
            k = i;
        }
    }
    if (k != -1) {
        oldFiles.splice(k, 1);
        hfOldFile.val(JSON.stringify(oldFiles));
        $(fileItem).fadeOut(function () {
            $(fileItem).remove();
        });
    }
    event.stopPropagation();
})
// 文件上传过程中创建进度条实时显示。
uploader.on('uploadProgress', function (file, percentage) {
    var $li = $('#' + file.id),
        $percent = $li.find('.progress .progress-bar');

    // 避免重复创建
    if (!$percent.length) {
        $percent = $('<div class="progress progress-striped active">' +
          '<div class="progress-bar" role="progressbar" style="width: 0%">' +
          '</div>' +
        '</div>').appendTo($li).find('.progress-bar');
    }

    $li.find('p.state').text('上传中');

    $percent.css('width', percentage * 100 + '%');
});

uploader.on('uploadSuccess', function (file) {
    $('#' + file.id).find('p.state').text('已上传');
    var fileEvent = {
        queueId: file.id,
        name: file.name,
        size: file.size,
        type: file.type
    };
    jsonData.push(fileEvent);

});

uploader.on('uploadError', function (file) {
    $('#' + file.id).find('p.state').text('上传出错');
});

uploader.on('uploadComplete', function (file) {
    $('#' + file.id).find('.progress').fadeOut();
    //var fp = $('#hfFilePath');
    //fp.val(JSON.stringify(jsonData));
});
uploader.on('all', function (type) {
    if (type === 'startUpload') {
        state = 'uploading';
    } else if (type === 'stopUpload') {
        state = 'paused';
    } else if (type === 'uploadFinished') {
        state = 'done';
    }

    if (state === 'uploading') {
        $btn.text('暂停上传');
    } else {
        $btn.text('开始上传');
    }
});

$btn.on('click', function () {
    if (state === 'uploading') {
        uploader.stop();
    } else {
        uploader.upload();
    }
});
//提交前合并附件
$('button[type=submit]').click(function () {
    var fp = $('#hfFilePath');
    var oldFp = $('#hfOldFile');
    //将原有附件进行合并
    if (oldFp && oldFp.val() != "") {
        Array.prototype.push.apply(jsonData, JSON.parse(oldFp.val()));
    }
    fp.val(JSON.stringify(jsonData));
});