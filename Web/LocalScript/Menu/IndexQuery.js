$(function () {
    $.ajax({
        url: "SearchRole",
        type: "post",
        dataType: "json",
        success: function (data) {
            if (data) {
                $("#List").datagrid({
                    columns: [data.title],
                    fitColumns: true,
                    title: "菜单查询"
                }).datagrid("loadData", data);
            }
            else
                $.messager.alert("提示","没有菜单");
        },
        error: function (err) {
            alert(err);
        }
    });
    $("#UpdateBtn").click(function () {
        var row = $('#List').datagrid('getSelected');
        var Key = row.Key;
        if (Key) {
            window.location.href = "IndexExecute?Key=" + Key;
        }
        else {
            $.messager.alert("提示消息","请选择菜单");
        }
    });
});