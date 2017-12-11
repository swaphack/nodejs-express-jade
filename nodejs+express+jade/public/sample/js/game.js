(function () {
    var _userData = null;

    var _shopData = new set.PageSet();
    _shopData.setPerPageItemCount(10);

    var _tableView = new lg.ui.TableView();
    _tableView.setItemModel("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>");
    _tableView.setHeader("<tr><th>ID</th><th>Name</th><th>Price</th></tr>");

    var _scheduler = lg.scheduler.createScheduler();

    // 更新时间
    function updateTime() {
        _scheduler.schedule(function () {
            var date = new Date();
            $("#time").text(date.format("yyyy/MM/dd hh:mm:ss"));
        }, 1000, false, "update");
    }

    // 获取物品信息
    function getShopItems() {
        var p = packet.createPacket();
        p.setValue("action", "menu");
        lg.http.getLogic("sample/shop", p, function (error, data) {
            if (error) {
                alert(error);
            } else {
                _shopData.setData(data["items"]);
                _tableView.flushData(_shopData.getPageData(0));
            }
        });
    }
    // 获取个人信息
    function getUserInfo() {
        var p = packet.createPacket();
        p.setValue("action", "userInfo");
        p.setValue("id", lg.userData.get("id"));
        lg.http.postLogic("sample/user", p, function (error, data) {
            _userData = data;
            $("#info").empty();
            var info = "<a>Name:{0} Vip:{1} Level:{2} Gold:{3}</a>".format(data["name"], data["vip"], data["level"], data["gold"]);
            $("#info").append(info);
        });
    }

    $(document).ready(function () {
        if (!lg.userData.get("id")) {
            alert("Please login");
            return;
        }

        _tableView.setParent($("#shop_item"));

        getShopItems();

        getUserInfo();

        updateTime();
    });
})();