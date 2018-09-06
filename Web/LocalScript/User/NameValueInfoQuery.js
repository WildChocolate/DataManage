var search = {
    DateFrom: "",
    DateTo: "",
    Name: "",
    PageSize: 10,
    PageIndex:1,
    submit: function () {
        this.Name = $("#Name").val();
        this.DateFrom = $("#DateFrom").val();
        this.DateTo = $("#DateTo").val();
        var options = $("#List").datagrid("getPager").data("pagination").options;
        this.PageSize = options.pageSize;
        this.PageIndex = options.pageNumber;
        $.ajax({
            url: "SearchNameValueInfo",
            type:"Post",
            data: { Name: this.Name, DateFrom: this.DateFrom, DateTo: this.DateTo, PageSize: this.PageSize, PageIndex: this.PageIndex },
            datatype: "json",
            success: function (data) {
                console.log(data);
                $("#List").datagrid("loadData", data);
                
            }
        });
    },
    cancel: function () {
        window.location.href = "NameValueInfoQuery";
    }
};

$(function () {
    $.ajax({
        url: "SearchNameValueInfo",
        type: "Post",
        data: { Name: this.Name, DateFrom: this.DateFrom, DateTo: this.DateTo, PageSize: this.PageSize, PageIndex: this.PageIndex },
        datatype: "json",
        success: function (data) {
            console.log(data);
            $("#List").datagrid({
                columns: [data.title],
                fitColumns: true,
                singleSelect: true,
            }).datagrid("loadData", data);
            var pager = $("#List").datagrid("getPager");
            pager.pagination({
                onSelectPage: search.submit
            });
        }
    });
    $("#Search").click(search.submit);
    $("#UpdateBtn").click(function () {
        var row = $("#List").datagrid("getSelected");
        if (row) {
            var key = row.Key;
            window.location.href = "NameValueInfoExecute?Key=" + key;
        }
        else
            $.messager.alert("提示！！！", "请选择需要修改的行");
    });
    search.submit();

});