var Search = {
    DateFrom: "",
    DateTo: "",
    Name: "",
    PageSize: 10,
    PageIndex:1,
    Submit: function () {
        this.DateFrom = $("#DateFrom").val().trim();
        this.DateTo = $("#DateTo").val().trim();
        this.Name = $("#Name").val().trim();
        var options = $("#List").datagrid("getPager").data("pagination").options;
        this.PageSize = options.pageSize;
        this.PageIndex = options.pageNumber;
        $.ajax({
            url: "SearchRole",
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
    $("#search").click(Search.Submit);
    $.ajax({
        url: "SearchRole",
        type: "post",
        dataType: "json",
        data: { pageSize: 10, pageIndex: 1 },
        success: function (data) {
            console.log(data);
            if (data) {
                $("#List").datagrid({
                    columns: [data.title],
                    fitColumns: true,
                    title:"角色查询"
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
        }
    });

    $("#UpdateBtn").click(function () {
        //获得datagrid 的选中行
        var row = $('#List').datagrid('getSelected');

        if (row) {
            var Key = row.Key;
            window.location.href = "IndexExecute?Key="+Key;
        }
        else {
            alert("请选择须要修改的行");
        }
        
    });
});