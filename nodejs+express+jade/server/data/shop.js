var mysql = require("../common/mysqlDB");
var lg = require("../common/index");

var mod = new lg.protocol.Protocol();
mod.setID("action");

module.exports = function (req, resp) {
    if (!req || !resp) {
        return false;
    }

    mod.hand(req, resp);
};
//////////////////////////////////////////////////////////////////
// 数据
mod.register("userdata", function (packet, resp) {
    var id = packet.getValue("id");
});

// 菜单
mod.register("menu", function (packet, resp) {
    var sql = "select * from db_shop_item";
    mysql.query(sql, function (qerr, values, fields) {
    });
});

// 购买
mod.register("buy", function (packet, resp) {

});

// 出售
mod.register("sell", function (packet, resp) {

});

