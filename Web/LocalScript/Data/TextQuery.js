var search = {
    DateFrom: "",
    DateTo:"",
    Name: "",
    PageSize: 10,
    PageIndex: 1,
    Search: function () {
        this.Name = $.trim($("#Name").val());
        this.DateFrom = $("#DateFrom").val();
        this.DateTo = $("#DateTo").val();
        var options = $("#List").datagrid("getPager").data("pagination").options;
        this.PageIndex = options.pageNumber;
        this.PageSize = options.pageSize;
        $.ajax({
            url: "SearchTextData",
            type: "post",
            data: {DateFrom:this.DateFrom, DateTo:this.DateTo, Name:this.Name, PageSize:this.PageSize, PageIndex:this.PageIndex},
            datatype: "json",
            success: function (data) {
                console.log(data);
                $("#List").datagrid({
                    columns: [data.title],
                    singleSelect: true,
                    fitColumns: true
                }).datagrid("loadData", data);
            }
        });
    }
};
$(function () {
    $("#List").datagrid();
    var pager = $("#List").datagrid("getPager");
    pager.pagination({
        onSelectPage: search.submit
    });
    $("#search").click(search.Search);
    search.Search();
    $("#UpdateBtn").click(function () {
        var row = $("#List").datagrid("getSelected");
        if (row) {
            window.location.href = "TextExecute?Key=" + row.Key;
        }
        else
            $.messager.alert("提示！！！", "请选择需要修改的行");
    });
    //$.ajax({
    //    url: "SearchTextData",
    //    type: "post",
    //    data: {},
    //    datatype: "json",
    //    success: function (data) {
    //        $("#List").datagrid({
    //            colums: [data.title],
    //            fitColumns: true,
    //            singleSelect: true,
    //            onLoadSuccess: function () {
    //                alert("加载完成");
    //                var rows = $("#List").datagrid("getRows");
    //                console.log(rows);
    //            }
    //        }).datagrid("loadData", data);
    //    }
    //});
    

});
