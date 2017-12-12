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
// 固定数据
var fixCache = lg.mysql.createFixCache("data");

var log = lg.mysql.getDB("log");

// 热销物品
mod.register("hot_item", function (packet, resp) {
    var sql = "select si.id, i.name, si.price  from item i, shop_item si where i.id = si.item_id and si.type = {0}".formatSQL(1);
    fixCache.query("shop_item", sql, function (data) {
        var p = lg.packet.createPacket();
        p.setValue("items", data);
        resp.sendPacket(p);
    });
});

// 新物品
mod.register("new_item", function (packet, resp) {
    var sql = "select si.id, i.name, si.price, i.icon  from item i, shop_item si where i.id = si.item_id and si.type = {0}".formatSQL(2);
    fixCache.query("new_item", sql, function (data) {
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
    log.query(sql);

    sql = "select id from bill where user_id={0} and datetime={1}".formatSQL(userId, timestamp);
    log.query(sql, function (err, values, fields) {
        if (err) {
            resp.sendStatus(500);
            return;
        }

        if (values.length === 0) {
            resp.sendStatus(500);
        } else {
            var billId = values[0]["id"];
            sql = "insert into bill_item(bill_id, user_id, item_id, item_count) values({0}, {1}, {2}, {3}, {4})".formatSQL(billId, userId, itemId, itemCount);
            log.query(sql);

            var p = lg.packet.createPacket();
            p.setValue("id", billId);
            resp.sendPacket(p);
        }
    });
});

// 出售
mod.register("sell", function (packet, resp) {

});

