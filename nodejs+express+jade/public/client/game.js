(function () {
    var name = localStorage.name || "swaphack";

    // 编号 名称 价格基准值 价格浮动值 更新时间
    var shopDataAry = [];

    // 更新时间
    function updateTime() {
        lg.scheduler.schedule(function () {
            var date = new Date();
            $("#time").text(date.format("yyyy/MM/dd hh:mm:ss"));
        }, 1000);
    }

    function autoLogin() {
        var p = packet.createPacket();
        p.setValue("action", "signIn");
        p.setValue("name", "root");
        p.setValue("pwd", "123");
        lg.http.getLogic("data/user", p, function (error, data) {
            if (error) {
                return;
            }
        });
    }

    $(document).ready(function () {
        updateTime();

        autoLogin();

    });
})();