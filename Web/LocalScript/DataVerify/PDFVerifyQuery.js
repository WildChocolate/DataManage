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
    DateFrom: "",
    DateTo: "",
    Name: "",
    PageSize: 10,
    PageIndex: 1,
    DataTypeKey:$("#DataTypeKey").val(),
    State: false,
};
var search = {
    submit: function () {
        $.ajax({
            url: "SearchDataVerify", // 获取表格数据的url
            type: "post",
            data: pager,
            dataType: "json",
            success: function (data) {
                if (data) {
                    //第一行要加入一个选择框列
                    data.title.splice(0,0, { checkbox: true, align: "center" });
                    console.log(data.title);
                    $("#List").bootstrapTable("refreshOptions", { columns: data.title });//加载标题
                    $("#List").bootstrapTable("load", data);//加载行数据
                }
            }
        });
    },

    update: function () {
        var rows = $("#List").bootstrapTable("getSelections");
        console.log(rows);
        window.location.href = "PDFVerify?Key=" + rows[0].Key;
    }
};
$(function () {
    $("#List").bootstrapTable({ // 对应table标签的id
        //url: "SearchData", // 获取表格数据的url
        //method: "post",
        //dataType: "json",
        cache: false, // 设置为 false 禁用 AJAX 数据缓存， 默认为true
        singleSelect: true,
        toolbar: "#tb",
        striped: true,  //表格显示条纹，默认为false
        pagination: true, // 在表格底部显示分页组件，默认false
        pageList: [10, 20], // 设置页面可以显示的数据条数
        pageSize: 10, // 页面数据条数
        pageNumber: 1, // 首页页码
        sidePagination: 'server', // 设置为服务器端分页
        sortName: 'Key', // 要排序的字段
        sortOrder: 'desc', // 排序规则
        
        onLoadSuccess: function (data) {  //加载成功时执行
            console.info("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            console.info("加载数据失败");
        },
        onPageChange: function (number, size) {
            pager.PageSize = size;
            pager.PageIndex = number;
            search.submit();
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
    $("#search").click(function () {
        var options = $('#List').bootstrapTable('getOptions');
        pager.PageSize = options.pageSize;
        pager.PageIndex = 1;
        pager.DateFrom = $("#DateFrom").val();
        pager.DateTo = $("#DateTo").val();
        pager.Name = $.trim($("#Name").val());
        pager.State = $("#State").prop("checked");
        search.submit();
    });
    $("#update").click(search.update);
    search.submit();//加载表格需要的数据
});