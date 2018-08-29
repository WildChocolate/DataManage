var execute = {

    submit: function () {
        var tbl = $("table.datagrid-btable").eq(1);
        var arr = new Array();
        tbl.find("tr").each(function (i) {
            var item = new rolemenuList();
            $(this).children("td").each(function (j) {
                if (j == 0) {
                    item.Key = $(this).text();
                }
                if (j == 3) {
                    item.CanReaded = $(this).find("input[name='read']").eq(0).prop("checked");
                }
                if (j == 4) {
                    item.CanUpdated = $(this).find("input[name='update']").eq(0).prop("checked");
                }
                if (j == 5) {
                    item.CanCreated = $(this).find("input[name='create']").eq(0).prop("checked");
                }
                if (j == 6) {
                    item.CanDeleted = $(this).find("input[name='delete']").eq(0).prop("checked");
                }
            });
            if (item.CanCreated || item.CanDeleted || item.CanReaded || item.CanUpdated) {
                arr.push(item);
            }
        });
        console.log(arr);
        $.ajax({
            url: "Manage",
            type: "post",
            data: { RoleKey: $("#RoleKey").val(), RoleMenusJsonString: JSON.stringify(arr) },
            datatype: "json",
            traditional: true,//这里设置为true 
            success: function (data) {
                $.messager.alert("提示！！！",data.Message);
            },
            error: function (err) {
                alert(err);
            }
        });
        
    },

    cancel: function () {
        window.location.href = "IndexQuery";
    },

    init: function () {
        $("#submit").click(this.submit);
        $("#cancel").click(this.cancel);
    }
};
function rolemenuList(Key, CanReaded, CanUpdated,CanCreated,CanDeleted){
    this.Key = Key;
    this.Menu = "";
    this.ParentMenu = "";
    this.CanReaded = CanReaded;
    this.CanUpdated = CanUpdated;
    this.CanCreated = CanCreated;
    this.CanDeleted = CanDeleted;
}

$(function () {
    $.ajax({
        url: "GetRoleMenuGrid",
        type: "post",
        data: { RoleKey: $("#RoleKey").val() },
        success: function (data) {
            //console.log(data);
            $("#List").datagrid({
                fit: false, fitColumns: true, singleSelect: true,
                height: 400,
                rownumbers: true, nowrap: true, pagination: false,
                checkOnSelect: false, selectOnCheck: false,
                columns: [[
                { field: 'Key', title: 'Key', width: 200, align: 'center', hidden:true },
                { field: 'Menu', title: '菜单', width: 200, align: 'center' },
                { field: 'ParentMenu', title: ' 父菜单', width: 200, align: 'right', align: 'center' },
                {
                    field: 'CanReaded', title: '浏览<input id=\"readAllCheck\" name="readall" type=\"checkbox\"  >', width: 100, align: 'center',
                    formatter: function (value, rec, rowIndex) {
                        if(value)
                            return "<input type=\"checkbox\"  name=\"read\" checked=\"" + rec.CanReaded + "\" >";
                        else
                            return "<input type=\"checkbox\"  name=\"read\"  >";
                    }
                },

                {
                    field: 'CanUpdated', title: '修改<input id=\"updateAllCheckbox\" name="updateall" type=\"checkbox\"  >', width: 100, align: 'center',
                    formatter: function (value, rec, rowIndex) {
                        if (value)
                            return "<input type=\"checkbox\"  name=\"update\" checked=\"" + rec.CanUpdated + "\" >";
                        else
                            return "<input type=\"checkbox\"  name=\"update\"  >";
                    }
                },
                {
                    field: 'CanCreated', title: '添加<input id=\"createAllCheckbox\" name="createall" type=\"checkbox\"  >', width: 100, align: 'center',
                    formatter: function (value, rec, rowIndex) {
                        if (value)
                            return "<input type=\"checkbox\"  name=\"create\" checked=\"" + rec.CanCreated + "\" >";
                        else
                            return "<input type=\"checkbox\"  name=\"create\"  >";
                    }
                },
                {
                    field: 'CanDeleted', title: '删除<input id=\"deleteAllCheckbox\" name="deleteall" type=\"checkbox\"  >', width: 100, align: 'center',
                    formatter: function (value, rec, rowIndex) {
                        if (value)
                            return "<input type=\"checkbox\"  name=\"delete\" checked=\"" + rec.CanDeleted + "\" >";
                        else
                            return "<input type=\"checkbox\"  name=\"delete\"  >";
                    }
                }
                ]],

                title: "角色菜单编辑",
                onLoadSuccess: function () {
                    $("#readAllCheck").unbind();
                    $("#updateAllCheckbox").unbind();
                    $("#createAllCheckbox").unbind();
                    $("#deleteAllCheckbox").unbind();
                    $("input[name='read']").unbind().bind("change", function () {
                        //总记录数
                        var totolrows = $("input[name='read']").length;
                        //选中的记录数
                        var checkrows = $("input[name='read']:checked").length;
                        //全选
                        if (checkrows == totolrows) {
                            $("#readAllCheck").prop("checked", 'checked');
                        }
                        else {
                            $("#readAllCheck").removeProp("checked");
                        }
                    });
                    $("input[name='update']").unbind().bind("change", function () {
                        //总记录数
                        var totolrows = $("input[name='update']").length;
                        //选中的记录数
                        var checkrows = $("input[name='update']:checked").length;
                        //全选
                        if (checkrows == totolrows) {
                            $("#updateAllCheckbox").prop("checked", 'checked');
                        }
                        else {
                            $("#updateAllCheckbox").removeProp("checked");
                        }
                    });
                    $("input[name='create']").unbind().bind("change", function () {
                        //总记录数
                        var totolrows = $("input[name='create']").length;
                        //选中的记录数
                        var checkrows = $("input[name='create']:checked").length;
                        //全选
                        if (checkrows == totolrows) {
                            $("#createAllCheckbox").prop("checked", 'checked');
                        }
                        else {
                            $("#createAllCheckbox").removeProp("checked");
                        }
                    });
                    $("input[name='delete']").unbind().bind("change", function () {
                        //总记录数
                        var totolrows = $("input[name='delete']").length;
                        //选中的记录数
                        var checkrows = $("input[name='delete']:checked").length;
                        //全选
                        if (checkrows == totolrows) {
                            $("#deleteAllCheckbox").prop("checked", 'checked');
                        }
                        else {
                            $("#deleteAllCheckbox").removeProp("checked");
                        }
                    });


                    //全选
                    $("#readAllCheck").change(function (e) {
                        var checked = $("#readAllCheck").prop("checked");
                        $("input[name='read']").prop("checked", checked);
                        e.stopPropagation();
                        return false;
                    });
                    $("#updateAllCheckbox").click(function (e) {
                        var checked = $("#updateAllCheckbox").prop("checked");
                        $("input[name='update']").prop("checked", checked);
                        e.stopPropagation();
                    });
                    $("#createAllCheckbox").click(function (e) {
                        var checked = $("#createAllCheckbox").prop("checked");
                        $("input[name='create']").prop("checked", checked);
                        e.stopPropagation();
                    });
                    $("#deleteAllCheckbox").click(function (e) {
                        var checked = $("#deleteAllCheckbox").prop("checked");
                        $("input[name='delete']").prop("checked", checked);
                        e.stopPropagation();
                    });
                }
            }).datagrid("loadData", data);
        }
    });

    execute.init();
});