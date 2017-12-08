(function () {
    var _shopData = new set.PageSet();
    _shopData.setPerPageItemCount(10);

    var _listView = new lg.ui.TableView();
    _listView.setItemModel("<tr><td>{0}</td><td>{1}</td></tr>");

    var id = lg.userData.get("id");

    // 更新时间
    function updateTime() {
        lg.scheduler.schedule(function () {
            var date = new Date();
            $("#time").text(date.format("yyyy/MM/dd hh:mm:ss"));
        }, 1000);
    }

    function getShopItems() {
        var p = packet.createPacket();
        p.setValue("action", "menu");
        lg.http.getLogic("data/shop", p, function (error, data) {
            if (error) {
                alert(error);
            } else {
                _shopData.setData(data);
                _listView.flushData(_shopData.getPageData(0));
            }
        });
    }

    $(document).ready(function () {
        _listView.setParent($("#shop"));

        getShopItems();
    });
})();