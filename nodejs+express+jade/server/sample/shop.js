var lg = require("../common/index");

var mod = lg.protocol.createProtocol();
mod.setID("action");

module.exports = function (req, resp) {
    if (!req || !resp) {
        return;
    }

    mod.hand(req, resp);
};
//////////////////////////////////////////////////////////////////
// 数据
var fixCache = lg.mysql.createFixCache();

// 菜单
mod.register("menu", function (packet, resp) {
    var sql = "select * from db_shop_item";
    fixCache.query("shop_items", sql, function (data) {
        var p = lg.packet.createPacket();
        p.setValue("items", data);
        resp.sendPacket(p);
    });
});

// 用户商店信息
mod.register("userShopInfo", function (packet, resp) {

});

// 购买
mod.register("buy", function (packet, resp) {
    var userId = packet.getValue("user_id");
    var itemId = packet.getValue("item_id");
    var itemCount = packet.getValue("item_count");

    var timestamp = Date.now();
    var sql = "insert into bill(user_id, datetime) values({0},{1})".formatSQL(userId, timestamp);
    lg.mysql.query(sql);

    sql = "select id from bill where user_id={0} and datetime={1}".formatSQL(userId, timestamp);
    lg.mysql.query(sql, function (err, values, fields) {
        if (err) {
            resp.sendStatus(500);
            return;
        }

        if (values.length === 0) {
            resp.sendStatus(500);
        } else {
            var billId = values[0]["id"];
            sql = "insert into bill_item(bill_id, user_id, item_id, item_count) values({0}, {1}, {2}, {3}, {4})".formatSQL(billId, userId, itemId, itemCount);
            lg.mysql.query(sql);

            var p = lg.packet.createPacket();
            p.setValue("id", billId);
            resp.sendPacket(p);
        }
    });
});

// 出售
mod.register("sell", function (packet, resp) {

});

