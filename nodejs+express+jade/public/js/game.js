(function () {
    var name = localStorage.name || "swaphack";

    // 编号 名称 价格基准值 价格浮动值 更新时间
    var shopDataAry = {
        1 : ["Food", 10, 5, 1000],
        2 : ["Wood", 20, 10, 2000],
        3 : ["Iron", 50, 15, 4000],
        4 : ["Tea",  100, 20, 10000],
        5 : ["Beer", 100, 30, 10000],
    };

    var tableRow = "<tr class='table_row'><td>{0}</td><td>{1}</td><td><input type='button' shopId='{2}' value='+'></td></tr>";

    // 更新时间
    function updateTime() {
        scheduler.schedule(function () {
            var date = new Date();
            $("#time").text(date.format("yyyy/MM/dd hh:mm:ss"));
        }, 1000);
    }

    $(document).ready(function () {
        $("#name").text(name);

        updateTime();

        var header = $("#table_header");
        for (var i in shopDataAry) {
            var item = shopDataAry[i];
            var child = tableRow.format(item[0], item[1], item[2]);
            header.after(child);
        }
    });
}());