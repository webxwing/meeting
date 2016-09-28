
$(function () {
    //窗口尺寸
    var bodyWidth = $('#form1').width();
    $('#dg').datagrid({
        columns: [[
            { field: 'ch', checkbox: true },
            { field: 'username', align: 'left', title: '用户名' },
            {
                field: 'password', align: 'center', title: '密码', formatter: function (value, rec) {
                    return rec.password.substring(1, 0) + "*******";
                }
            },
            { field: 'actor', align: 'center', title: '角色' }
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
                    infoAlert("请先选择需要删除的用户！");
                    return;
                }
                getData('./User/UserDelete.ashx', { 'u_id': row.id}, function (result) {
                    if (result == "ok") {
                        infoAlert("删除成功！");
                        $('#dg').datagrid('reload');
                    } else {
                        infoAlert(result);
                    }
                }, function (e) {
                    infoAlert("删除失败：500服务器错误！");
                });
            }
        }, {
            iconCls: 'icon-edit',
            text: '编辑',
            handler: function () {
                var row = $('#dg').datagrid('getSelected');
                if (row) {                    
                    EditLayer(row.id);
                } else {
                    infoAlert("请先选择需要编辑的用户！");
                }
            }
        }]
    });

    //添加用户窗口
    function AddLayer() {
        var newWidth = (parseInt(bodyWidth) * 0.95) + 'px';
        parent.layer.open({
            type: 2,
            title: ['用户添加窗口', false],
            shadeClose: false,
            shade: 0.5,
            area: [newWidth, '95%'],
            content: './User/AddUserS.aspx',
            end: function () {
                $('#dg').datagrid('reload');
            }
        });
    }
    //编辑用户窗口
    function EditLayer(u_id) {
        var newWidth = (parseInt(bodyWidth) * 0.95) + 'px';
        parent.layer.open({
            type: 2,
            title: ['用户编辑窗口', false],
            shadeClose: false,
            shade: 0.5,
            area: [newWidth, '95%'],
            content: './User/UserEditS.aspx?u_id=' + u_id,
            end: function () {
                $('#dg').datagrid('reload');
            }
        });
    }    
    //封装提示信息
    function infoAlert(info) {
        layer.msg(info, { time: 2000 });
    }
});
