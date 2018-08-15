(function ($) {
    $.login = {

        formMessage: function (msg) {
            $('.login_tips').find('.tips_msg').remove();
            $('.login_tips').append('<div class="tips_msg"><i class=fa fa-question-circle></i>' + msg + '</div>');
        },

        loginClick: function () {
            var $username = $("#username");
            var $password = $("#password");
            var $code = $("#validateCode");
            if ($username.val() == "") {
                $username.focus();
                $.login.formMessage('请输入用户名');

                return false;
            }
            else if ($password.val() == "") {
                $password.focus();
                $.login.formMessage('请输入登录密码');

                return false;
            }
            else if ($code.val() == "") {
                $code.focus();
                $.login.formMessage('请输入验证码');

                return false;
            }
            else {

                $.login.formMessage('');
                $("#loginButton").attr('disabled', 'disabled').find('span').html("验证中...");
                $.ajax({
                    url: "/Login/CheckLogin",
                    data: { username: $.trim($username.val()), password: $.trim($password.val()), code: $.trim($code.val()) },
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.key);
                        if (parseInt(data.key )> 0) {
                            $("#loginButton").find('span').html("登录成功，正在跳转...");
                            window.setTimeout(function () {
                                window.location.href = "/Home/Index";
                            }, 500);
                        }
                        else {
                            $("#loginButton").removeAttr('disabled').find('span').html("登录");
                            $("#switchCode").trigger("click");
                            $code.val('');
                            $.login.formMessage(data.message);
                        }
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            }
        },

        init: function () {
            $("#switchCode").click(function () {
                $("#imgCode").attr("src", "/Login/GetAuthCode?time=" + Math.random());
            });
            $("#loginButton").click(function () {
                $.login.loginClick();
            });
        }

    };
    $(function () {
        $.login.init();
    });
})(jQuery);