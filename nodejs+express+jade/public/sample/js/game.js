(function () {
    var _userData = null;

    var _hotItemData = new set.PageSet();
    _hotItemData.setPerPageItemCount(10);

    var _newItemData = new set.PageSet();
    _newItemData.setPerPageItemCount(5);

    var _hotItemView = new lg.ui.TableView();
    _hotItemView.setHeader("<tr><th>ID</th><th>Name</th><th>Price</th><th>Add</th></tr>");
    _hotItemView.setItemModel("<tr><td>{0}</td><td><a itemId='{0}' class='item_name'>{1}</a></td><td>{2}</td><td><input type='button' class='item_action' itemId='{0}' value='+'/></td></tr>");

    var _newItemView = new lg.ui.TableView();
    _newItemView.setItemModel("<td><div class='new_item_img'><img src='{3}'></div><div class='new_item_info'><div class='new_item_name'><a>Name : {1}</a></div><div class='new_item_price'><a>Price : {2}</a></div></div></td>");

    var _scheduler = lg.scheduler.createScheduler();

    // 更新时间
    function updateTime() {
        _scheduler.schedule(function () {
            var date = new Date();
            $("#time").text(date.format("yyyy/MM/dd hh:mm:ss"));
        }, 1000, false, "update");
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

    // 获取物品信息
    function getHotShopItems() {
        var p = packet.createPacket();
        p.setValue("action", "hot_item");
        lg.http.getLogic("sample/shop", p, function (error, data) {
            if (error) {
                alert(error);
            } else {
                _hotItemData.setData(data["items"]);
                _hotItemView.flushData(_hotItemData.getPageData(0));
                $("#hot_item .item_action").click(function () {
                    var itemId = $(this).attr("itemId");
                    console.log(itemId);
                });

                $("#hot_item .item_name").click(function () {
                    var itemId = $(this).attr("itemId");
                    console.log(itemId);
                });
            }
        });
    }

    // 获取物品信息
    function getNewShopItems() {
        var p = packet.createPacket();
        p.setValue("action", "new_item");
        lg.http.getLogic("sample/shop", p, function (error, data) {
            if (error) {
                alert(error);
            } else {
                _newItemData.setData(data["items"]);
                _newItemView.removeAllItems();

                if (!data["items"]) {
                    return;
                }
                var itemAry = _newItemData.getPageData(0);
                _newItemView.flushData(itemAry);
            }
        });
    }


    $(document).ready(function () {
        if (!lg.userData.get("id")) {
            alert("Please login");
            return;
        }

        _hotItemView.setParent($("#hot_item"));
        _newItemView.setParent($("#new_item"));

        getUserInfo();

        getHotShopItems();

        getNewShopItems();

        updateTime();
    });
})();