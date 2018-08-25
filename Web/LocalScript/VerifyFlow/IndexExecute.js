var execute = {
    Key:0,
    Name:"",
    Description: "",
    submit: function () {
        this.Name = $("#Name").val();
        if (this.Name == "") {
            $.messager.alert("请输入流程名称");
            return;
        }
        this.Description = $("#Description").val();
        this.Key = $("#Key").val();
        $.ajax({
            url:"Manage",
            type: "post",
            data: {Key:this.Key, Name:this.Name, Description:this.Description},
            datatype: "json",
            success: function (data) {
                console.log(data);
                $.messager.alert("提示！！！",data.Message);
            },
            error: function (err) {
                $.messager.alert("请求出错",err)
            }
        });
    },
    cancel: function () {

    },
    init: function () {
        $("#submit").click(this.submit);
        $("#cancel").click(this.cancel);
    }
};

$(function () {
    execute.init();

});