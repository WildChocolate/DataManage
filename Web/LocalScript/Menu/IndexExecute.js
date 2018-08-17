var execute = {
    Key:0,
    Name: "",
    Description: "",
    ParentKey: 0,
    IsTop: false,
    Controller: "",
    Action: "",
    submit: function () {
        this.Key = $("#Key").val();
        if ($.trim($("#Name").val()) == "") {
            $.messager.alert("提示","菜单名不能为空");
            return;
        }
        this.Name = $.trim($("#Name").val());
        if ($("#IsTop").prop("checked")) {
            this.Controller = "";
            this.Action = "";
            this.ParentMenu = 0;
        }
        else {
            if ($('#ParentKey').combobox('getValue') == "") {
                $.messager.alert("提示", "非厅级菜单请选择父菜单");
            }
            else {
                this.ParentKey = $('#ParentKey').combobox('getValue');
            }
            this.Controller = $.trim($("#Controller").val());
            this.Action = $.trim($("#Action").val());
        }
        $.ajax({
            url: "Manage",
            type: "post",
            data:{Name:this.Name, Description:this.Description, ParentKey:this.ParentKey, Controller:this.Controller, Action:this.Action},
            datatype: "json",
            success: function (data) {
                $.messager.alert("提示！",data.Message);
            },
            error: function (err) {
                alert(err);
            }
        });
    },
    cancel: function () {

    }
};

$(function () {
    $('#ParentKey').combobox({
        url: 'GetParentMenus',
        valueField: 'keyid',
        textField: 'C_Name'
    });
    $("#IsTop").change(function () {
        if ($(this).prop("checked")) {
            $("#Controller").textbox("disable");
            $("#Action").textbox("disable");
            $("#ParentMenu").textbox("disable");
        }
        else {
            $("#Controller").textbox("enable");
            $("#Action").textbox("enable");
            $("#ParentMenu").textbox("enable");
        }
    });
    $("#ParentKey").combobox("setText", $("ParentName").val());
    $("#submit").click(execute.submit);
});