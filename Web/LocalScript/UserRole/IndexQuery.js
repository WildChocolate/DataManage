var Search = {
    DateFrom: "",
    DateTo: "",
    Name: "",
    PageSize: 10,
    PageIndex: 1,

    submit: function () {
        this.DateFrom = $("#DateFrom").val();
        this.DateTo = $("#DateTo").val();
        this.Name = $("#Name").val();
        var options = $("#List").datagrid("getPager").data("pagination").options;
        this.PageSize = options.pageSize;
        this.PageIndex = options.pageNumber;
        console.log(options);
        $.ajax({
            url: "GetUserGrid",
            type: "post",
            data: { Name: this.Name, DateFrom: this.DateFrom, DateTo: this.DateTo, PageSize: this.PageSize, PageIndex: this.PageIndex },
            datatype: "json",
            success: function (data) {
                $("#List").datagrid({
                    view: detailview,
                    detailFormatter: function (index, row) {
                        return '<div style="padding:2px"><table class="ddv"></table></div>';
                    },
                    onExpandRow: function (index, row) {
                        var ddv = $(this).datagrid('getRowDetail', index).find('table.ddv');
                        ddv.datagrid({
                            url: 'GetUserRoles?userid=' + row.Key,
                            fitColumns: true,
                            singleSelect: true,
                            rownumbers: true,
                            loadMsg: '',
                            height: 'auto',
                            columns: [[
                            { field: 'Role', title: '角色名', width: 100 },
                            { field: 'CreatedDate', title: '添加时间', width: 100 },
                            { field: 'UpdatedDate', title: '修改时间', width: 100 }
                            ]],
                            onResize: function () {
                                $('#List').datagrid('fixDetailRowHeight', index);
                            },
                            onLoadSuccess: function () {
                                setTimeout(function () {
                                    $('#List').datagrid('fixDetailRowHeight', index);
                                }, 0);
                            }
                        });
                        $('#List').datagrid('fixDetailRowHeight', index);
                    }
                }).datagrid("loadData", data);
                //使用detailview后的datagrid 在重新绑定数据后需要重置 分页控件的一些参数，不然会自动设置paegNumber为1，无法正常使用
                $('#List').datagrid("getPager").pagination({
                    total: data.total,
                    pageSize: options.pageSize,
                    pageNumber:options.pageNumber,
                    onSelectPage: function (number, size) {
                        Search.submit();
                    }
                });
            }
        });

    }
};

$(function () {
    $.ajax({
        url: "GetUserGrid",
        type:"post",
        data: {},
        datatype: "json",
        success: function (data) {
            $("#List").datagrid({
                columns: [data.title],
                fitColumns: true,
                view: detailview,
                detailFormatter: function (index, row) {

                    return '<div style="padding:2px"><table class="ddv"></table></div>';
                },
                onExpandRow: function (index, row) {
                   
                    var ddv = $(this).datagrid('getRowDetail',index).find('table.ddv');
                    ddv.datagrid({
                        url: 'GetUserRoles?userid=' + row.Key,
                        fitColumns:true,
                        singleSelect:true,
                        rownumbers:true,
                        loadMsg:'',
                        height:'auto',
                        columns:[[
                            {field:'Role',title:'角色名',width:100},
                            {field:'CreatedDate',title:'添加时间',width:100},
                            {field:'UpdatedDate',title:'修改时间',width:100}
                        ]],
                        onResize:function(){
                            $('#List').datagrid('fixDetailRowHeight', index);
                        },
                        onLoadSuccess:function(){
                            setTimeout(function(){
                                $('#List').datagrid('fixDetailRowHeight', index);
                            },0);
                        }
                    });
                    $('#List').datagrid('fixDetailRowHeight', index);
                }
            }).datagrid("loadData", data);
            var pager = $("#List").datagrid("getPager");
            pager.pagination({
                onSelectPage: function () {
                    Search.submit();
                }
            });
        }
    });
    $("#search").click(Search.submit);
    $("#UpdateBtn").click(function () {
        var row = $('#List').datagrid('getSelected');
        if (row) {
            var key = row.Key;
            window.location.href = "IndexExecute?userid=" + key;
        }
    });
});