var execute = {

    submit: function () {
        var checkBoxs = $("input[type=checkbox]:checked");
        var array = new Array();
        var roleKey = $("#RoleKey").val();
        $.each(checkBoxs, function () {
            var dtKey = $(this).data("id");
            var verifyKey = $(this).parent().next().next("td").children("input").eq(0).val();
            array.push({ RoleKey: roleKey, DataTypeKey: dtKey, VerifyKey: verifyKey });
        });
        $.ajax({
            url: "RoleVerifyManage",
            type: "post",
            data: {RoleKey:$("#RoleKey").val(), RoleVerifyJson:JSON.stringify(array)},
            datatype: "json",
            success: function (data) {
                $.messager.alert("提示！！！", data.Message);
            },
            error: function (err) {
                $.messager.alert("错误！！！", err);
            }
        });
    }
};

$(function () {
    var inputs = $("input[name=verifyFlow]");
    var stepdict = [];
    $.ajax({
        url: "GetAllStepJson",
        data: null,
        async: false,
        timeout:1000,
        type: "post",
        datatype: "json",
        success: function (data) {
            if (data.State) {
                stepdict = data.Data;
                stepdict["0"] = "--无--";
            }
        }
    });
    if (inputs.length > 0) {
        $.ajax({
            url: "GetVerifyFlowCombo",
            data: {},
            type: "post",
            datatype: "json",
            success: function (data) {
                if (data) {
                    var obj = {
                        Key: 0,
                        Name:"--请选择--"
                    };
                    data.splice(0, 0, obj);
                }
                $(inputs).each(function (idx) {
                    $(this).combobox().combobox({
                        data: data,
                        valueField: 'Key',
                        textField: 'Name',
                        onSelect: function (record) {
                            var p = $(this).parent().next("td").children("p[name=Steps]").eq(0);
                            p.html(stepdict[record.Key]);
                        }
                    });
                });
                
            }
        });
    }
    $("#submit").click(execute.submit);
});