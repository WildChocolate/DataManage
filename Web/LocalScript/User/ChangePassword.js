var change = {
    oldpassword: "",
    newpassword: "",
    confirmpassword:"",
    Submit :function(){
        if (this.oldpassword == "") {
            alert("请输入密码");
            return false;
        }
        if (this.newpassword == "") {
            alert("请输入新密码");
            return false;
        }
        if (this.confirmpassword == "" || this.confirmpassword!=this.newpassword) {
            alert("两次输入的密码不一致！");
            return false;
        }
        if (this.oldpassword == this.newpassword) {
            alert("新旧密码不能一样！");
            return false;
        }
        $.ajax({
            url: "/User/ChangePassword",
            data: { oldpassword: this.oldpassword, newpassword: this.newpassword },
            type: "post",
            dataType: "json",
            success: function (data) {
                if (data.Status == true) {
                    alert("修改成功");
                }
                else {
                    alert("修改失败");
                    if (data.Url != "")
                    {
                        window.location.href = data.Url;
                    }
                }
            },
            error: function (err) {
                alert(err);
            }
        });
    },
    Cancle: function () {
        if (confirm("确定取消吗！")) {
            var inputs = $("input");
            inputs.each(function (i) {
                $(this).val("");
            });
            this.oldpassword = this.newpassword = this.confirmpassword = "";
        }
    }
};
$(function () {
    (function () {
        $("#btn_ok").click(function () {
            
            if (!confirm("确认修改密码吗？")) {
                return;
            }
            change.oldpassword = $("#oldPassword").val();
            change.newpassword = $("#newPassword").val();
            change.confirmpassword = $("#confirmPassword").val();
            change.Submit();
        });
        $("#btn_cancle").click(function () {
            change.Cancle();
        });
    })();
});
