var mysql = require("../common/mysqlDB");
var Protocol = new require("./protocol").Protocol;

var Game = function () {
    Protocol.call(this);

    this.musicFiles = null;
}

Game.prototype = new Protocol();
Game.prototype.constructor = Game;

var mod = new Game();
mod.setID("action");

module.exports = function (req, resp) {
    if (!req || !resp) {
        return false;
    }

    if (!mod.hand(req, resp)) {
        resp.send([]);
    }
};

// 数据
mod.register("userdata", function (query, resp) {
    var id = query.id;
});

// 菜单
mod.register("menu", function (query, resp) {
    var sql = "select * from db_shop_item";
    mysql.query(sql, function (qerr, values, fields) {
        if (qerr) {
            resp.send([]);
        } else {
            console.log(values);
            console.log(fields);
            resp.send(values);
        }
    });
});

// 购买
mod.register("buy", function (query, resp) {

});

// 出售
mod.register("buy", function (query, resp) {

});

