var execute = {
    Key:0,
    Name: "",
    Description: "",
    Value: "",
    submit: function () {
        this.Key = $("#Key").val();
        this.Name = $.trim($("#Name").val());
        this.Description = $("#Description").val();
        this.Value = $.trim($("#Value").val());
        if (this.Name == "" || this.Value == "") {
            $.messager.alert("提示","关键字和值不能为空");
            return;
        }
        $.ajax({
            url:"NameValueInfoManage",
            type:"post",
            data: { Key: this.Key, Name: this.Name, Value: this.Value, Description: this.Description },
            datatype:"json",
            success:function(data){
                $.messager.alert("提示",data.Message);
            },
            error:function(err){
                $.messager.alert("提示", err);
            }
        });
    },
    cancel: function () {
        window.location.href = "NameValueInfoQuery";
    }

};

$(function () {
    $("#submit").click(execute.submit);
    $("#cancel").click(execute.cancel);
});