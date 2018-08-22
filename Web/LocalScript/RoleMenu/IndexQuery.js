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
        $.ajax({
            url:"GetRoleGrid",
            type: "post",

            data: { Name: this.Name, DateFrom: this.DateFrom, DateTo: this.DateTo, PageSize: this.PageSize, PageIndex: this.PageIndex },
            datatype: "json",
            success: function (data) {
                $("#List").datagrid({
                    pagination: true,
                    view: detailview,
                    detailFormatter: function (index, row) {
                        return '<div style="padding:2px"><table class="ddv"></table></div>';
                    },
                    onExpandRow: function (index, row) {
                        var ddv = $(this).datagrid('getRowDetail',index).find('table.ddv');
                        ddv.datagrid({
                            url: 'GetRoleMenus?roleid=' + row.Key,
                            fitColumns:true,
                            singleSelect:true,
                            rownumbers:true,
                            loadMsg:'',
                            height:'auto',
                            columns:[[
                                { field: 'MenuName', title: '菜单名', width: 100 },
                                { field: 'CanReaded', title: '查看', width: 50 },
                                { field: 'CanCreated', title: '添加', width: 50 },
                                { field: 'CanUpdated', title: '修改', width: 50 },
                                { field: 'CanDeleted', title: '删除', width: 50 },
                                { field:'CreatedDate',title:'添加时间',width:100},
                                { field:'UpdatedDate',title:'修改时间',width:100}
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
    
    $("#search").click(Search.submit);
    $("#UpdateBtn").click(function(){
        var row = $("#List").datagrid("getSelected");
        if (row) {
            window.location.href = "IndexExecute?RoleKey="+row.Key;
        }
        else
        {
            $.messager.alert("提示","请选择需要编辑的角色");
        }
    });
    $.ajax({
        url: "GetRoleGrid",
        type: "post",
        dataType: "json",
        data: { pageSize: 10, pageIndex: 1 },
        success: function (data) {
            if (data) {
                $("#List").datagrid({
                    columns: [data.title],
                    fitColumns: true,
                    title: "角色查询",
                    view: detailview,
                    detailFormatter: function (index, row) {
                        return '<div style="padding:2px"><table class="ddv"></table></div>';
                    },
                    onExpandRow: function (index, row) {
                        var ddv = $(this).datagrid('getRowDetail', index).find('table.ddv');
                        ddv.datagrid({
                            url: 'GetRoleMenus?roleid=' + row.Key,
                            fitColumns: true,
                            singleSelect: true,
                            rownumbers: true,
                            loadMsg: '',
                            height: 'auto',
                            columns: [[
                                { field: 'Menu', title: '菜单名', width: 100 },
                                { field: 'CanReaded', title: '查看', width: 50 },
                                { field: 'CanCreated', title: '添加', width: 50 },
                                { field: 'CanUpdated', title: '修改', width: 50 },
                                { field: 'CanDeleted', title: '删除', width: 50 }
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
            }
            var pager = $("#List").datagrid("getPager");
            pager.pagination({
                onSelectPage: function () {
                    Search.submit();
                }
            });
        }
    });

    
});