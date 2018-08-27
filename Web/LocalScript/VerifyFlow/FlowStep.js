var execute = {
    infos:null,
    submit: function () {
        this.infos = [];
        var btns = $("a[name=delete]");
        var len = btns.length;
        var verifykey = $("#VerifyKey").val();
        for (var i = 1 ; i <= len; i++) {
            var info = new flowStepInfo();
            info.VerifyKey = verifykey;
            info.Name = $("#Name-" + i).val();
            info.Description = $("#Description-").val();
            info.Step = $("#Step-" + i).val();
            info.StepRole = $("#role-" + i).val();
            if ($.trim(info.Name) != "" && $.trim(info.Step) != "" && $.trim(info.StepRole)!="")
            {
                this.infos.push(info);
            }
        }
        console.log(this.infos);
        
        $.ajax({
            url: "StepManage",
            type: "post",
            data: { FlowStepsJson: JSON.stringify(this.infos) },
            datatype: "json",
            success: function (data) {
                if (data) {
                    $.messager.alert("提示消息！！！",data.Message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    remove: function () {
        $(this).parent().parent().remove();
    }
};
function flowStepInfo() {
    this.Key = 0;
    this.Name = "";
    this.Description = "";
    this.VerifyKey = "";
    this.RoleKey = "";
    this.Step = "";
    this.StepRole = "";

}
    var rolecache = null;
$(function () {

    $.ajax({
        url: "GetRoleCombo",
        type: "get",
        datatype: "json",
        success: function (data) {
            rolecache = data;
            console.log($("input[name='StepRole']").length);
            $("input[name='StepRole']").each(function (idx) {
                idx++;
                $("#role-"+idx).combobox({
                    data: data, //此为重点，如果后台返回的结果就是combobox要求的格式，那么此处直接写data:json即可
                    valueField: 'Key',
                    textField: 'Name',
                    editable: false,
                    onLoadSuccess: function () {

                    }
            })
            });
            
        }
    });
    $("a[name=delete]").click(execute.remove);
    $("#submit").click(execute.submit);
    $("#add").click(function () {
        if (!rolecache) {
            $.ajax({
                url: "GetRoleCombo",
                type: "get",
                datatype: "json",
                success: function (data) {
                    rolecache = data;
                }
            });
        }
        var newtr = document.createElement("tr");
        var idx = $("a[name=delete]").length+1;
        var td1 = document.createElement("td");
        td1.innerHTML = idx ;
        newtr.appendChild(td1);

        var td2 = document.createElement("td");
        var ipt2 = document.createElement("input");
        ipt2.className = "easyui-textbox";
        ipt2.id = "Name-" + idx;
        ipt2.style.width = "100%";
        ipt2.style.height = "32px ";
        td2.appendChild(ipt2);
        newtr.appendChild(td2);
        
        var td3 = document.createElement("td");
        var ipt3 = document.createElement("input");
        ipt3.className = "easyui-numberbox";
        ipt3.id = "Step-" + idx;
        ipt3.style.width = "100%";
        ipt3.style.height = "32px ";
        td3.appendChild(ipt3);
        newtr.appendChild(td3);

        var td4 = document.createElement("td");
        var ipt4 = document.createElement("input");
        ipt4.className = "easyui-textbox";
        ipt4.id = "Description-" + idx;
        ipt4.style.width = "100%";
        ipt4.style.height = "32px ";
        td4.appendChild(ipt4);
        newtr.appendChild(td4);

        var td5 = document.createElement("td");
        var ipt5 = document.createElement("input");
        ipt5.className = "easyui-textbox";
        ipt5.id = "role-" + idx;
        ipt5.style.width = "100%";
        ipt5.style.height = "32px ";
        td5.appendChild(ipt5);
        newtr.appendChild(td5);

        var td6 = document.createElement("td");
        var linkbtn = document.createElement("a");
        linkbtn.innerHTML = "删除";
        linkbtn.className = "easyui-linkbutton";
        linkbtn.name = "delete";
        $(linkbtn).on("click", execute.remove).attr("data-options", "iconCls:'icon-cancel'");
        td6.appendChild(linkbtn);
        newtr.appendChild(td6);
        
        $(newtr).appendTo("#tblFlowStep>tbody");
        $.parser.parse(newtr);
        console.log(rolecache);
        $(ipt5).combobox({
            data: rolecache, 
            valueField: 'Key',
            textField: 'Name',
            editable: false,
        });
    });
});