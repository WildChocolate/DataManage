

var search = {
    DateFrom: "",
    DateTo: "",
    Name: "",
    PageSize: 10,
    PageIndex: 1,
    DataTypeKey: 0,
    State:false,
    submit: function () {
        var options = $("#List").datagrid("getPager").data("pagination").options;
        this.PageSize = options.pageSize;
        this.PageIndex = options.pageNumber;
        this.Name = $.trim($("#Name").val());
        this.DateFrom = $("#DateFrom").val();
        this.DateTo = $("#DateTo").val();
        this.DataTypeKey = $("#DataTypeKey").val();
        this.State = $("#State").prop("checked");
        var test = 0;
        $.ajax({
            url: "SearchDataVerify",
            type: "post",
            data: { DateFrom: this.DateFrom, DateTo: this.DateTo, Name: this.Name, PageSize: this.PageSize, PageIndex: this.PageIndex, DataTypeKey: this.DataTypeKey, State: this.State },
            datatype: "json",
            success: function (data) {
                $("#List").datagrid({
                    columns: [data.title],
                    singleSelect: true,
                    fitColumns: true

                }).datagrid("loadData", data);
            }
        });
    },
    cancel: function () {

    }
};
$(function () {
    //$.ajax({
    //    url: "SearchDataVerify",
    //    type: "post",
    //    data: { PageSize: 10, PageIndex: 1 },
    //    datatype: "json",
    //    success: function (data) {
    //        console.log(data);
    //        if (data) {
    //            $("#List").datagrid({
    //                pagination: true,
    //                singleSelect: true,
    //                columns: [data.title],
    //                toolbar: "#tb",
    //                minHeight: 300,
    //                fitColumns: true,
    //                title: "流程查询"
    //            }).datagrid("loadData", data);
    //        }
    //        var pager = $("#List").datagrid("getPager");
    //        pager.pagination({
    //            onSelectPage: function () {
    //                search.submit();
    //            }
    //        });
    //    }
    //});
    $("#List").datagrid();
    var pager = $("#List").datagrid("getPager");
    pager.pagination({
        onSelectPage: search.submit
    });
    search.submit();

    $("#search").click(search.submit);
    $("#UpdateBtn").click(function () {
        var row = $("#List").datagrid("getSelected");
        if (row) {
            window.location.href = "TextExecute?Key=" + row.Key;
        }
        else
            $.messager.alert("提示！！！", "请选择需要修改的行");
    });
});