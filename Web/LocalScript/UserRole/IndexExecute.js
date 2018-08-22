var execute = {
    Roles:null,
    UserId: "",
    submit: function () {
        var roles = $("#selectedRole").children();
        var arr = new Array();
        $(roles).each(function () {
            arr.push($(this).attr("data-Key"));
        });

        this.UserId = $("#Key").val();
        this.Roles = arr;
        $.ajax({
            url: "Manage",
            type: "post",
            data:{UserId:this.UserId, Roles:this.Roles.join(",")},
            datatype: "json",
            success: function (data) {
                $.messager.alert("提示！",data.Message);
            }
        });
    },
    cancel: function () {
        window.location.href = "IndexQuery";
    }
};

$(function () {
    var liclick = function () {
        $(this).siblings('li').removeClass('active');
        $(this).addClass('active');
        if ($(this).parent().attr("id") == "selectedRole") {
            $("#selectedKey").val($(this).attr("data-Key"));
        }
        else {
            $("#unselectedKey").val($(this).attr("data-Key"));
        }
    }
    
    $.ajax({
        url: "GetExecuteRolesByUserId",
        data: { userid: $("#Key").val() },
        type: "post",
        datatype: "json",
        success: function (data) {
            if (data.selected) {
                var ul = $("#selectedRole");
                for (var i = 0 ; i < data.selected.length;i++)
                {
                    var item = data.selected[i];
                    var li = document.createElement("li");
                    li.setAttribute("data-Key", item.Key);
                    li.innerHTML = item.Name;
                    $(li).appendTo(ul);
                }
            }
            if (data.unselected) {
                var ul = $("#unselectedRole");
                for (var i = 0 ; i < data.unselected.length; i++) {
                    var item = data.unselected[i];
                    var li = document.createElement("li");
                    li.setAttribute("data-Key", item.Key);
                    li.innerHTML = item.Name;
                    $(li).appendTo(ul);
                }
            }
            $("body").on("click", "li", liclick);
        }
    });
    $("#toleft").click(function (e) {
        var unselecedtkey = $("#unselectedKey").val();
        var target = $("#unselectedRole").children(".active").eq(0);
        $(target).appendTo("#selectedRole")
    });
    $("#toright").click(function (e) {
        var key1 = $("#selectedKey").val();
        var target = $("#selectedRole").children(".active").eq(0);
        $(target).appendTo("#unselectedRole");
    });
    $("#submit").click(execute.submit);
    $("#cancel").click(execute.cancel);
});