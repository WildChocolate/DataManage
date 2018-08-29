﻿var execute = {
    Key:0,
    Name: "",
    Description: "",
    submit: function () {
        this.Key = $("#Key").val();
        this.Name = $.trim($("#Name").val());
        this.Description = $("#Description").val();
        var f = $("#filebox_file_id_2").get(0).files[0];

        if (this.Name == "") {
            $.messager.alert("提示","名称不能为空");
            return;
        }
        var data = new FormData();
        data.append("Key",this.Key);
        data.append("Name", this.Name);
        data.append("Description", this.Description);
        data.append("file", f);
        $.ajax({
            url: "TextManage",
            //加入下面两个设置取消Illegal invocation 这个错误
            //ajax2.0可以不用设置请求头，但是jq帮我们自动设置了，这样的话需要我们自己取消掉
            contentType: false,
            //取消帮我们格式化数据，是什么就是什么
            processData: false,
            type: "post",
            data: data,
            datatype: "json",
            success: function (data) {
                $.messager.alert("提示信息",data.Message);
            }
        });
    }
};
$(function () {
    $("#submit").click(execute.submit);

    $('#textfile').filebox({
        accept: 'text/plain',
        validType: ['fileSize[10,"mb"]'],
    });
});