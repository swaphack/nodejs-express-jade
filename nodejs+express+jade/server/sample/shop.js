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
        p.setContent(data);
        resp.sendPacket(p);
    });
});

// 购买
mod.register("buy", function (packet, resp) {

});

// 出售
mod.register("sell", function (packet, resp) {

});

