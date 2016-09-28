$(function () {
    //for meetingShengHe.aspx
    $('#dg').datagrid({
        columns: [[
            { field: 'ch', checkbox: true },
            { field: 'm_title', title: '标题' },
            {
                field: 'm_date', title: '时间', formatter: function (value, rec) {
                    return (rec.m_date).replace('T', ' ');
                }
            },
            { field: 'm_place', title: '地点' },
            {
                field: 'm_pass', title: '操作', formatter: function (value, rec) {
                    return "<a href='javascript:abc(" + rec.id + ")' class='btn btn-primary btn-xs'>审核通过</a>";
                }
            },
        ]]
    });
    //for meetingListToItems用于显示议程
    $('#dgToItems').datagrid({
        columns: [[
            { field: 'ch', checkbox: true },
            { field: 'm_title',align:'left', title: '标题' },
            {
                field: 'm_date', align: 'center', title: '时间', formatter: function (value, rec) {
                    return (rec.m_date).replace('T', ' ');
                }
            },
            { field: 'm_place', align: 'center', title: '地点' },
            {field:'m_state',title:'会议状态'},
            {
                field: 'm_pass', align: 'center', title: '操作', formatter: function (value, rec) {
                    return "<a href='../../Meeting/MeetingItemsS.aspx?m_id=" + rec.id + "' class='btn btn-primary btn-xs' >查看议程</a>";
                }
            },
        ]]
    });
});
//审核提交服务器
function abc(m_id) {
    
    $.ajax({
        url: "../../Meeting/ashx/ShengHe.ashx",
        data: { 'm_id': m_id },
        type: "post",
        cache: false,
        dataType: "json",
        timeout: 20000,
        success: function (data, textStatus) {
            if (data.isOk) {
                layer.msg("审核通过！可在会议查询中查看~", { time: 2500 });
                $('#dg').datagrid('reload');
            }
            else {
                layer.msg("审核失败：" + data.result, { time: 2000 });
            }
        },
        complete: function (XMLHttpRequest, status) {
            if (status == 'timeout') {
                layer.msg("连接服务器超时！", { time: 2000 });
            }
        }
    });
}
