//for meetingItemsS.aspx
//窗口尺寸
var bodyWidth = $('#form1').width();
var m_id = getQueryString('m_id');
$('#dg').datagrid({
    url: 'MeetingItemsHandler.ashx?m_id=' + m_id
});
$('#dg').datagrid({
    columns: [[
        { field: 'ch', checkbox: true },
        {
            field: 'item_title', align: 'left', title: '名称', formatter: function (value, rec) {
                return rec.item_title.length > 40 ? rec.item_title.substring(0, 39) + "..." : rec.item_title;
            }
        },
        { field: 'item_time', align: 'center', title: '用时（分钟）' },
        { field: 'item_state', align: 'center', title: '状态' },
        {
            field: 'item_number', align: 'center', title: '操作', formatter: function (value, rec) {
                return "<a href='javascript:EditLayer(" + rec.id + ");' class='btn btn-primary btn-xs'>编辑</a> | <a href='javascript:DeleteItem(" + rec.id + ",\"" + rec.item_state + "\")' class='btn btn-primary btn-xs'>删除</a> | <a href='javascript:Push(" + rec.id + ",\"up\"," + rec.item_number + ",\"" + rec.item_state + "\");' class='btn btn-primary btn-xs'>上移</a> | <a href='javascript:Push(" + rec.id + ",\"down\"," + rec.item_number + ",\"" + rec.item_state + "\");' class='btn btn-primary btn-xs'>下移</a>";
            }
        }
        //'./Meeting/MeetingEditItemsS.aspx?m_id=' + m_id+'&item_id='+id
    ]],
    toolbar: [{
        id: 'btnBack',
        text: '返回议程管理主页',
        iconCls: 'icon-back',
        handler: function () {
            document.location.href = "../../Meeting/MeetingListToItems.aspx";
        }
    },
        {
            id: 'btnStart',
            text: '开始',
            iconCls: 'icon-application_start',
            handler: function () {

            }
        }, {
            id: 'btnEnd',
            text: '结束',
            iconCls: 'icon-application_stop',
            handler: function () {

            }
        }, {
            id: 'btnReStart',
            text: '重置',
            iconCls: 'icon-application_side_expand',
            handler: function () {

            }
        }]
});
var pager = $('#dg').datagrid().datagrid('getPager');
pager.pagination({
    fit: true,
    pageSize: 10,
    pageList: [5, 10, 15],
    beforePageText: '第',
    afterPageText: '页  共{pages}页',
    displayMsg: '当前 {from} - {to}  ',
    buttons: [{
        iconCls: 'icon-add',
        text: '增加议程',
        handler: AddLayer
    }, {
        iconCls: 'icon-remove',
        text: '删除所选',
        handler: function () {
            var row = $('#dg').datagrid('getSelected');
            if (row == null) {
                infoAlert("请先选择需要操作的会议！");
                return;
            }
            DeleteItem(row.id, row.item_state);
        }
    }, {
        iconCls: 'icon-edit',
        text: '编辑所选',
        handler: function () {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                if (row.item_state != "未开始") {
                    infoAlert(row.item_state + "的会议不允许再编辑！");
                    return;
                }
                EditLayer(row.id);
            } else {
                infoAlert("请先选择需要操作的会议！");
            }
        }
    }]
});
//添加议程窗口    
function AddLayer(title) {
    var newWidth = (parseInt(bodyWidth) * 0.95) + 'px';
    parent.layer.open({
        type: 2,
        title: ['议程添加窗口', false],
        shadeClose: false,
        shade: 0.5,
        area: [newWidth, '95%'],
        content: './Meeting/MeetingAddItemsS.aspx?m_id=' + m_id,
        end: function () {
            $('#dg').datagrid('reload');
        }
    });
}
//编辑议程窗口
function EditLayer(id) {
    var newWidth = (parseInt(bodyWidth) * 0.95) + 'px';
    parent.layer.open({
        type: 2,
        title: ['议程编辑窗口', false],
        shadeClose: false,
        shade: 0.5,
        area: [newWidth, '95%'],
        content: './Meeting/MeetingEditItemsS.aspx?m_id=' + m_id + '&item_id=' + id,
        end: function () {
            $('#dg').datagrid('reload');
        }
    });
}
//删除议程
function DeleteItem(id,state) {
    var row = $('#dg').datagrid('getSelected');
    if (row == null) {
        infoAlert("请先选择需要操作的会议！");
        return;
    }
    layer.confirm("确认删除？", {}, function () {
        getData('MeetingItemsDelete.ashx', { 'm_id': m_id, 'i_id': id,'i_state':state }, function (result) {
            if (result == "ok") {
                infoAlert("删除成功！");
                $('#dg').datagrid('reload');
            } else {
                infoAlert("删除失败:"+result);
            }
        }, function (e) {
            infoAlert("删除失败：服务器错误！");
        });
    });
}
//议程移动
function Push(id, operation, num, state) {
    getData('../Meeting/ashx/MeetingItemsPush.ashx', { 'm_id': m_id, 'i_id': id, 'i_num': num, 'operation': operation, 'i_state': state },
        function (code) {
            switch (code) {
                case "1": infoAlert("已是第一个议程！");break;
                case "2": infoAlert("已是最后一个议程！");break;
                case "3": infoAlert("操作成功！"); $('#dg').datagrid('reload'); break;
                default: infoAlert("操作失败:"+code); break;
            }
        });
}

//封装提示信息
function infoAlert(info) {
    layer.msg(info, { time: 2000 });
}




