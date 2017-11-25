(function () {
    var name = localStorage.name || "swaphack";

    // 编号 名称 价格基准值 价格浮动值 更新时间
    var shopDataAry = [];

    // 更新时间
    function updateTime() {
        scheduler.schedule(function () {
            var date = new Date();
            $("#time").text(date.format("yyyy/MM/dd hh:mm:ss"));
        }, 1000);
    }

    $(document).ready(function () {
        updateTime();

        http.get("data/user", {action:"signIn", name:"root", pwd:"123"}, function (data) {
            console.log(data);
           if (!data) {
               return;
           }
        });
    });
}());