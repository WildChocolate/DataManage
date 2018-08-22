var Search = {
    DateFrom: "",
    DateTo: "",
    Name: "",
    PageSize: 10,
    PageIndex: 1,
    Submit: function () {
        this.DateFrom = $("#DateFrom").val().trim();
        this.DateTo = $("#DateTo").val().trim();
        this.Name = $("#Name").val().trim();
        var options = $("#List").datagrid("getPager").data("pagination").options;
        this.PageSize = options.pageSize;
        this.PageIndex = options.pageNumber;
        $.ajax({
            url: "SearchMenu",
            data: { Name: this.Name, DateFrom: this.DateFrom, DateTo: this.DateTo, PageSize: this.PageSize, PageIndex: this.PageIndex },
            dataType: "json",
            type: "post",
            success: function (data) {
                console.log(data);
                $("#List").datagrid("loadData", data);
            },
            error: function (err) {
                $.messager.alert(err.Message);
            }
        });
    },
    Cancle: function () {

    }
};

$(function () {

    $.ajax({
        url: "SearchMenu",
        type: "post",
        dataType: "json",
        success: function (data) {
            if (data) {
                $("#List").datagrid({
                    columns: [data.title],
                    fitColumns: true,
                    title: "菜单查询"
                }).datagrid("loadData", data);
                var pager = $("#List").datagrid("getPager");
                pager.pagination({
                    onSelectPage: function () {
                        $(this).pagination('loading');
                        Search.Submit();
                        $(this).pagination('loaded');
                    }
                });
            }
            else
                $.messager.alert("提示","没有菜单");
        },
        error: function (err) {
            alert(err);
        }
    });

    $("#Search").click(Search.Submit);
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