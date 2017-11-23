var mysql = require("../common/mysqlDB");
var Protocol = new require("./protocol").Protocol;

var Game = function () {
    Protocol.call(this);

    this.musicFiles = null;
}

Game.prototype = new Protocol();
Game.prototype.constructor = Game;

var game = new Game();
game.setID("action");

// 数据
game.register("userdata", function (query, resp) {
    var id = query.id;
    var name = query.name;
    var pwd = query.pwd;
});

// 菜单
game.register("menu", function (query, resp) {
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
game.register("buy", function (query, resp) {

});

// 出售
game.register("buy", function (query, resp) {

});

// 游戏文件请求
module.exports = function (req, resp) {
    if (!req || !resp) {
        return false;
    }

    if (!game.hand(req, resp)) {
        resp.send([]);
    }
};