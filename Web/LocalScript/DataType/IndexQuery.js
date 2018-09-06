var search = {
    DateFrom: "",
    DateTo: "",
    Name: "",
    PageSize: 10,
    PageIndex: 1,
    submit: function () {
        this.Name = $("#Name").val();
        this.DateFrom = $("#DateFrom").val();
        this.DateTo = $("#DateTo").val();
        var options = $("#List").datagrid("getPager").data("pagination").options;
        this.PageSize = options.pageSize;
        this.PageIndex = options.pageNumber;
        $.ajax({
            url: "SearchDataType",
            type: "post",
            data: {Name:this.Name, DateFrom:this.DateFrom, DateTo:this.DateTo, PageSize:this.PageSize,PageIndex:this.PageIndex},
            datatype: "json",
            success: function (data) {
                $("#List").datagrid("loadData", data);
            }
        });
    }
};

$(function () {
    $.ajax({
        url: "SearchDataType",
        type: "post",
        data: { PageSize: 10, PageIndex: 1 },
        datatype: "json",
        success: function (data) {
            $("#List").datagrid({
                columns: [data.title],
                singleSelect: true,
                fitColumns: true
            }).datagrid("loadData", data);
            var pager = $("#List").datagrid("getPager");
            pager.pagination({
                onSelectPage: search.submit
            });
        }
    });

    


    $("#search").click(search.submit);
    $("#UpdateBtn").click(function () {
        var row = $("#List").datagrid("getSelected");
        if (row) {
            alert(row.Key);
            window.location.href = "TextExecute?Key=" + row.Key;
        }
        else
            $.messager.alert("提示！！！","请选择需要修改的行");
    });
});