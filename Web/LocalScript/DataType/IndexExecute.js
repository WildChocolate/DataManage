var execute = {
    Key: 0,
    Name: "",
    Description: "",

    submit: function () {
        this.Key = $("#Key").val();
        this.Name = $.trim($("#Name").val());
        this.Description = $("#Description").val();
        this.Value = $.trim($("#Value").val());
        if (this.Name == "" ) {
            $.messager.alert("提示", "类型名称不能为空");
            return;
        }
        $.ajax({
            url: "DataTypeManage",
            type: "post",
            data: { Key: this.Key, Name: this.Name, Description: this.Description },
            datatype: "json",
            success: function (data) {
                $.messager.alert("提示！！！", data.Message);
            },
            error: function (err) {
                $.messager.alert("提示！！！", err);
            }
        });
    },
    cancel: function () {
        window.location.href = "IndexQuery";
    }

};

$(function () {
    $("#submit").click(execute.submit);
    $("#cancel").click(execute.cancel);
});