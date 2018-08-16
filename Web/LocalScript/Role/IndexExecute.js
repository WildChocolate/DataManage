var execute = {
    Key:"0",
    Name: "",
    Description: "",
    ParentRole: "",
    submit: function () {
        if ($.trim(this.Name) == "") {
            alert("角色名不能为空");
            return;
        }
        if ($("isTop").prop("checked")) {
            this.ParentRole = 0;
        }
        else {
            this.ParentRole = $("#ParentRole").val();
            if (this.ParentRole == "") {
                $.messager.alert("提示","非顶级角色请选择父角色");
                return;
            }
        }
        $.ajax({
            url: "Manage",
            type: "post",
            data:{Key:this.Key, Name:this.Name, Description:this.Description, ParentRole:this.ParentRole},
            datatype: "json",
            success: function (data) {
                if(data)
                    $.messager.alert("提示消息", data.Message);
            },
            error: function (err) {
                $.messager.alert("错误",err);
            }
        });
    },
    cancle: function () {

    }
};

$(function () {
    $.ajax({
        url: "GetRoles?Key="+$("#Key").val(),
        type:"get",
        dataType:"json",
        success: function (data) {
            console.log(data);
            $("#ParentRole").combobox({ editable: false }).combobox("loadData", data);
            $('#cc').combobox('setValue', $("#ParentRole").val());
            $("#ParentRole").combobox("setText", $("#ParentName").val());
        },
        error: function (err) {
            alert(err);
        }
    });
    
    $("#isTop").change(function () {
        if ($(this).prop("checked")) {
            $("#ParentRole").combobox("disable");
        }
        else {
            $("#ParentRole").combobox("enable");
        }
    });
    $("#submit").click(function () {
        execute.Key = $("#Key").val();
        execute.Name = $("#Name").val();
        execute.Description = $("#Description").val();
        execute.ParentRole = $("#ParentRole").val();
        execute.submit();
    });
    $("#cancel").click(execute.cancle);
});
