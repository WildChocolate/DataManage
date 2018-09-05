var execute = {
    submit: function () {
        if (vm.Name == "") {
            $("#myModal").modal("show")
            return;
        }
        var f = $("#Path").get(0).files[0];
        var data = new FormData();
        data.append("Key", vm.Key);
        data.append("Name", vm.Name);
        data.append("Description", vm.Description);
        data.append("DataTypeKey", vm.DataTypeKey);
        data.append("file", f);
        $.ajax({
            url: "DataManage",
            //加入下面两个设置取消Illegal invocation 这个错误
            //ajax2.0可以不用设置请求头，但是jq帮我们自动设置了，这样的话需要我们自己取消掉
            contentType: false,
            //取消帮我们格式化数据，是什么就是什么
            processData: false,
            type: "post",
            data: data,
            datatype: "json",
            success: function (data) {
                var modal = $("#resultModal").modal();
                //此处即为修改modal的标题
                modal.find('.modal-body label').text(data.Message);
                $(model).model("show");
            }
        });
    },
    cancel: function () {
        window.location.href = "PDFQuery";
    }
};
$(function () {
    $("#submit").click(execute.submit);
    $("#cancel").click(execute.cancel);
});