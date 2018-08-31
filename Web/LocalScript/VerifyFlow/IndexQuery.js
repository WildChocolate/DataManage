

var search = {
    DateFrom: "",
    DateTo: "",
    Name: "",
    PageSize: "",
    PageIndex:"",
    submit: function () {
        var options = $("#List").datagrid("getPager").data("pagination").options;
        this.PageSize = options.pageSize;
        this.PageIndex = options.pageNumber;
        this.Name = $.trim($("#Name").val());
        this.DateFrom = $("#DateFrom").val();
        this.DateTo = $("#DateTo").val();
        $.ajax({
            url: "SearchVerifyFlow",
            type:"post",
            data: {DateFrom:this.DateFrom, DateTo:this.DateTo, Name:this.Name, PageSize:this.PageSize, PageIndex:this.PageIndex},
            datatype: "json",
            success: function (data) {
                $("#List").datagrid({
                    fitColumns: true,
                    pagination: true,
                    toolbar: "#tb",
                    singleSelect: true,
                    colums: [data.title]

                }).datagrid("loadData",data);
            }
        });
    },
    cancel: function () {

    }
};

$(function () {
    $.ajax({
        url: "SearchVerifyFlow",
        type: "post",
        data: {PageSize: 10, PageIndex: 1 },
        datatype: "json",
        success: function (data) {
            console.log(data);
            if (data) {
                $("#List").datagrid({
                    pagination: true,
                    singleSelect: true,
                    columns: [data.title],
                    toolbar: "#tb",
                    minHeight: 300,
                    fitColumns: true,
                    title: "资料审核"
                }).datagrid("loadData", data);
            }
            var pager = $("#List").datagrid("getPager");
            pager.pagination({
                onSelectPage: function () {
                    search.submit();
                }
            });
        }
    });

    $("#UpdateBtn").click(function () {
        //获得datagrid 的选中行
        var row = $('#List').datagrid('getSelected');
        
        if (row) {
            var Key = row.Key;
            window.location.href = "IndexExecute?Key=" + Key;
        }
        else {
            alert("请选择须要修改的行");
        }

    });
    $("#search").click(search.submit);
});