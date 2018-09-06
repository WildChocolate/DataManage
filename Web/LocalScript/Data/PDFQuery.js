$.fn.datetimepicker.defaults = {
    //默认语言
    language: 'zh-CN',
    //默认选择格式
    format: "yyyy-mm-dd hh:ii:ss",
    autoclose: true,
    todayBtn: true,
    //选择板所在输入框位置
    pickerPosition: "bottom-left"
};
var pager = {
    PageSize: 10, // 每页要显示的数据条数
    PageIndex: 1, // 每页显示数据的开始行号
    Name: "",
    DataTypeKey: "",
    DateFrom: "",
    DateTo: "",
};
var search = {

    submit: function () {
        var options = $('#List').bootstrapTable('getOptions');
        pager.PageSize = options.pageSize;
        pager.PageIndex = 1;
        pager.DataTypeKey = $("#DataTypeKey").val();
        pager.DateFrom = $("#DateFrom").val();
        pager.DateTo = $("#DateTo").val();
        pager.Name = $.trim($("#Name").val());
        $.ajax({
            url: "SearchData", // 获取表格数据的url
            type: "post",
            data:pager,
            dataType: "json",
            success: function (data) {
                console.log(data);
                $("#List").bootstrapTable("load", data);
            }
        });
    },
    add: function () {
        window.location.href = "PDFExecute?Key=0";
    },
    update: function () {
        var rows = $("#List").bootstrapTable("getSelections");
        console.log(rows);
        window.location.href = "PDFExecute?Key="+rows[0].Key;
    },
    remove: function () {
        window.location.href = "DataRemove";
    }
};
$(function () {
    $("#List").bootstrapTable({ // 对应table标签的id
        url: "SearchData", // 获取表格数据的url
        method: "post",
        dataType: "json",
        cache: false, // 设置为 false 禁用 AJAX 数据缓存， 默认为true
        singleSelect:true,
        toolbar:"#tb",
        striped: true,  //表格显示条纹，默认为false
        pagination: true, // 在表格底部显示分页组件，默认false
        pageList: [10, 20], // 设置页面可以显示的数据条数
        pageSize: 10, // 页面数据条数
        pageNumber: 1, // 首页页码
        sidePagination: 'server', // 设置为服务器端分页
        queryParams: function (params) { // 请求服务器数据时发送的参数，可以在这里添加额外的查询参数，返回false则终止请求

            var data = {
                PageSize: params.limit, // 每页要显示的数据条数
                PageIndex: params.offset, // 每页显示数据的开始行号
                Name: $("#Name").val(),
                DataTypeKey: $("#DataTypeKey").val(),
                DateFrom: $("#DateFrom").val(),
                DateTo: $("#DateTo").val()
            }
            return data;
        },
        sortName: 'Key', // 要排序的字段
        sortOrder: 'desc', // 排序规则
        columns: [
            {
                checkbox: true, // 显示一个勾选框
                align: 'center' // 居中显示
            }, {
                field: 'Key', // 返回json数据中的name
                title: '编号', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle', // 上下居中
                width: 50 // 定义列的宽度，单位为像素px
            }, {
                field: 'Name',
                title: '名称',
                align: 'center',
                valign: 'middle'
            }, {
                field: 'Description',
                title: '描述',
                align: 'center',
                valign: 'middle'
            }, {
                field: "Path",
                title: "路径",
                align: 'center',
                valign: 'middle',
                width: 160, // 定义列的宽度，单位为像素px
            }, {
                field: "StateText",
                title: "已审核",
                align: 'center',
                valign: 'middle',
                width: 50, // 定义列的宽度，单位为像素px
            }, {
                field: "CreatedDate",
                title: "创建时间",
                align: 'center',
                valign: 'middle',
                width: 160, // 定义列的宽度，单位为像素px
            }, {
                field: "UpdatedDate",
                title: "修改时间",
                align: 'center',
                valign: 'middle',
                width: 160, // 定义列的宽度，单位为像素px
            },{
                field: "Download",
                title: "操作",
                align: 'center',
                valign: 'middle',
                width: 50, // 定义列的宽度，单位为像素px
            }
        ],
        onLoadSuccess: function (data) {  //加载成功时执行
            console.info("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            console.info("加载数据失败");
        },
        onPageChange: function (number, size) {
            var options = $('#List').bootstrapTable('getOptions');
            pager.PageSize = options.pageSize;
            pager.PageIndex = options.pageNumber;
            pager.DataTypeKey = $("#DataTypeKey").val();
            pager.DateFrom = $("#DateFrom").val();
            pager.DateTo = $("#DateTo").val();
            pager.Name = $.trim($("#Name").val());
            $.ajax({
                url: "SearchData", // 获取表格数据的url
                type: "post",
                data: pager,
                dataType: "json",
                success: function (data) {
                    console.log(data);
                    $("#List").bootstrapTable("load", data);
                }
            });
        }
    });
    var picker1 = $('#DateFrom').datetimepicker();
    var picker2 = $("#DateTo").datetimepicker();

    //动态设置最小值(选择前面一个日期后：后面一个日期不能小于前面一个)
    picker1.on('changeDate', function (e) {
        picker2.datetimepicker('setStartDate', e.date);
    });
    //动态设置最大值(选择后面一个日期后：前面一个日期不能大于前面一个）
    picker2.on('changeDate', function (e) {
        picker1.datetimepicker('setEndDate', e.date);
    });
    $("#search").click(search.submit);
    $("#add").click(search.add);
    $("#update").click(search.update);
    $("#remove").click(search.remove);
});