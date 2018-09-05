var submit = {
    Key: 0,
    Advice: "",
    State: false,
    Submit: function () {
        this.Key = $("#Key").val();
        this.Advice = $("#Advice").val();
        this.State = $("#pass").prop("checked");
        if (this.Key < 1) {
            $.messager.alert("提示", "提交失败，dataKey不能小于0 ！！！");
            return;
        }
        var send = $.messager.confirm('提示', '此操作为不可逆行为，确认提交吗？', function (r) {
            if (!r) {
                return true;
            }
        });

        if (send) {
            var location = "";

            $.ajax({
                url: "",
                type: "post",
                data: { Key: this.Key, Advice: this.Advice, State: this.State },
                datatype: "json",
                success: function (data) {

                    alert(data.Message);
                    //如果成功，2秒后跳转到查询页
                    if (data.State) {
                        setTimeout(function () {
                            window.location.href = document.referrer;
                        }, 2000);
                    }
                }
            });
        }
    },
    Cancel: function () {
        window.location.href = document.referrer;
    }
};


$(function () {
    $("#submit").click(submit.Submit);
    $("#cancel").click(submit.Cancel);
});