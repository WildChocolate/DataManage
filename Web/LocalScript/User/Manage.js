var execute = {
    Key:0,
    Name: "",
    Sex:true,
    LoginName: "",
    PassWord: "",

    Execute: function () {
        if (this.Name == "") {
            alert("请输入姓名");
            return;
        }
        if (this.LoginName == "")
        {
            alert("请输入登录帐号");
            return;
        }
        if (this.PassWord == "") {
            alert("请输入密码");
            return;
        }
        if(execute.Sex=="")
        {
            alert("请选择性别");
            return;
        }
        if (execute.Sex == "男") {
            execute.Sex = true;
        }
        else {
            execute.Sex = false;
        }

        var formdata = new FormData();
        //formdata.append('Photo', $('#Photo')[0].files[0]);  有效
        //formdata.append('Photo', document.getElementById("Photo").files[0]);有效

        //formdata.append('Photo', $('#Photo').prop("files").eq（0));  无效

        /* 多个文件上传的方法，要先让 input mutiple
        var files = $('#Photo').prop('files');
        $(files).each(function (i) {
            formdata.append('Photo'+i, files[i]);
        });
        */
        formdata.append('Key', this.Key);
        formdata.append('Photo', $('#Photo')[0].files[0]);
        formdata.append("Name", this.Name);
        formdata.append("LoginName", this.LoginName);
        formdata.append("PassWord", this.PassWord);
        formdata.append("Sex", this.Sex);
        // console.log(formdata);
        //return;
        $.ajax({
            type: "POST",
            //加入下面两个设置取消Illegal invocation 这个错误
            //ajax2.0可以不用设置请求头，但是jq帮我们自动设置了，这样的话需要我们自己取消掉
            contentType: false,
            //取消帮我们格式化数据，是什么就是什么
            processData: false,
            url: "Manage",
            data: formdata,
            dataType:"json",
            success: function (data) {

                    alert(data.Message);
                
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    Cancle: function () {
        $("#Name").val("");
        $("#LoginName").val("");
        $("#PassWord").val("");
    }
};

$(function () {
    console.log(document.getElementById("Photo"));
    $("#Photo").on("change", function () {
        alert("图片改变");
        var file = this.files[0];
        if (this.files && file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#img1").attr("src", e.target.result);
                $("#fn").html(file.name);
                $("#fs").html(file.size + " bytes");
            }
            reader.readAsDataURL(file);
        }
    });

    $.extend($.fn.validatebox.defaults.rules, {
        selectValueRequired: {
            validator: function (value, param) {
                return $(param[0]).find("option:contains('" + value + "')").val() != '';
            },
            message: '请选择性别'
        }
    });

    $('#Sex').combobox({
        onSelect: function (option) {
            if (option.value == "") {
                return;
            }
            if (option.value == "男") {
                $("#img1").css("background-image", "url(../../Images/head_boy.png)");
            }
            else {
                $("#img1").css("background-image", "url(../../Images/head_girl.png)");
            }
        }
    });
    $("#Sex").combobox("setValue", $("#hidSex").val());
    $("#Sex").combobox("setText", $("#hidSex").val());
    $("#submit").click(function () {
        execute.Key=$("#Key").val();
        execute.Name = $("#Name").val();
        execute.LoginName = $("#LoginName").val();
        execute.PassWord = $("#PassWord").val();
        execute.Sex = $("#Sex").val();
        execute.Execute();
    });

    $("#cancle").click(function () {
        execute.Cancle();
    });
});
