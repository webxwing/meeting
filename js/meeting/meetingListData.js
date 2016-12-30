
$(function () {
    //窗口尺寸
    var bodyWidth = $('#form1').width();    
    $('#dg').datagrid({
        columns: [[
            { field: 'ch', checkbox: true },
            { field: 'm_title', align: 'left', title: '标题' },
            {
                field: 'm_date', align: 'center', title: '时间', formatter: function (value, rec) {
                    return (rec.m_date).replace('T', ' ');
                }
            },
            { field: 'm_place', align: 'center', title: '地点' },
            { field: 'm_state', align: 'center', title: '状态' },
            {
                field: 'm_pass', title: '审核状态', formatter: function (value, rec) {
                    return rec.m_pass == 0 ? "<span class='dg_pass' style='color:red'>未审</span>" : "<span class='dg_pass' style='color:blue'>通过</span>";
                }
            },
        ]]
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
            text: '添加',
            handler: AddLayer
        }, {
            iconCls: 'icon-remove',
            text: '删除',
            handler: function () {
                var row = $('#dg').datagrid('getSelected');
                if (row == null) {
                    infoAlert("请先选择需要操作的会议！");
                    return;
                }
                layer.confirm("议程将会一并删除，确认删除？", {}, function () {
                    getData('MeetingDelete.ashx', { 'm_id': row.id, 'i_state': row.m_state }, function (result) {
                        if (result == "ok") {
                            infoAlert("删除成功！");
                            $('#dg').datagrid('reload');
                        } else {
                            infoAlert(result);
                        }
                    }, function (e) {
                        infoAlert("删除失败：服务器错误！");
                    });
                });
            }
        }, {
            iconCls: 'icon-edit',
            text: '编辑',
            handler: function () {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    //window.location.href = "MeetingEdit.aspx?m_id=" + row.id;
                    if (row.m_state != "未开始") {
                        infoAlert(row.m_state+"的会议不允许再编辑！");
                        return;
                    }
                    EditLayer(row.id);
                } else {
                    infoAlert("请先选择需要操作的会议！");
                }
            }
        }, {
            iconCls: 'icon-tip',
            text: '控制',
            handler: function () {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    ControlLayer(row.id);
                }
                else {
                    infoAlert("请先选择需要操作的会议！");
                }
            }
        }]
    });

    //添加会议窗口
    function AddLayer() {
        var newWidth = (parseInt(bodyWidth) * 0.95) + 'px';
        parent.layer.open({
            type: 2,
            title: ['会议添加窗口', false],
            shadeClose: false,
            shade: 0.5,
            area: [newWidth, '95%'],
            content: './Meeting/MeetingAddS.aspx',
            end: function () {
                $('#dg').datagrid('reload');
            }
        });
    }
    //编辑会议窗口
    function EditLayer(m_id) {
        var newWidth = (parseInt(bodyWidth) * 0.95) + 'px';
        parent.layer.open({
            type: 2,
            title: ['会议编辑窗口', false],
            shadeClose: false,
            shade: 0.5,
            area: [newWidth, '95%'],
            content: './Meeting/MeetingEditS.aspx?m_id='+m_id,
            end: function () {
                $('#dg').datagrid('reload');
            }
        });
    }
    //控制会议窗口
    function ControlLayer(m_id) {
        parent.layer.open({
            type: 2,
            title: ['会议控制窗口', false],
            shadeClose: false,
            shade: 0.5,
            area: ['250px', '200px'],
            content: './Meeting/MeetingControl.aspx?m_id='+m_id,
            end: function () {
                $('#dg').datagrid('reload');
            }
        });
    }
    //封装提示信息
    function infoAlert(info) {
        layer.msg(info, {time:2000});
    }
});
