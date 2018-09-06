var search = {
    DateFrom: "",
    DateTo:"",
    Name: "",
    DataTypeKey:0,
    PageSize: 10,
    PageIndex: 1,
    Submit: function () {
        this.Name = $.trim($("#Name").val());
        this.DateFrom = $("#DateFrom").val();
        this.DateTo = $("#DateTo").val();
        var options = $("#List").datagrid("getPager").data("pagination").options;
        this.PageIndex = options.pageNumber;
        this.PageSize = options.pageSize;
        this.DataTypeKey = $("#DataTypeKey").val();
        $.ajax({
            url: "SearchData",
            type: "post",
            data: { DateFrom: this.DateFrom, DateTo: this.DateTo, Name: this.Name, PageSize: this.PageSize, PageIndex: this.PageIndex, DataTypeKey: this.DataTypeKey },
            datatype: "json",
            success: function (data) {
                $("#List").datagrid("loadData", data);
            }
        });
    }
};
$(function () {
    //开局先加载一次数据，并设置分页事件
    $.ajax({
        url: "SearchData",
        type: "post",
        data: { PageSize: 10, PageIndex: 1, DataTypeKey: $("#DataTypeKey").val() },
        datatype: "json",
        success: function (data) {
            $("#List").datagrid({
                columns: [data.title],
                singleSelect: true,
                fitColumns: true
            }).datagrid("loadData", data);
            var pager = $("#List").datagrid("getPager");
            pager.pagination({
                onSelectPage: search.Submit
            });
        }
    });
    
    $("#search").click(search.Submit);
    
    $("#UpdateBtn").click(function () {
        var row = $("#List").datagrid("getSelected");
        if (row) {
            window.location.href = "DataExecute?Key=" + row.Key + "&DataTypeKey=" + $("#DataTypeKey").val();
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
