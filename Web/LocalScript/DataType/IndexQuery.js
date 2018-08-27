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
            data: {},
            datatype: "json",
            success:function(data){
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
    search.submit();

    $("#submit").click(search.submit);
    $("#UpdateBtn").click(function () {
        var row = $("#List").datagrid("getSelected");
        if (row) {
            window.location.href = "IndexExecute?Key=" + row.Key;
        }
        else
            $.messager.alert("提示！！！","请选择需要修改的行");
    });
});