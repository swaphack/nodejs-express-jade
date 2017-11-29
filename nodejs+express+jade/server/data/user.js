var mysql = require("../common/mysqlDB");
var Protocol = new require("../common/protocol").Protocol;

var User = function () {
    Protocol.call(this);
}

User.prototype = new Protocol();
User.prototype.constructor = User;

var mod = new User();
mod.setID("action");

module.exports = function (req, resp) {
    if (!req || !resp) {
        return false;
    }

    if (!mod.hand(req, resp)) {
        resp.send([]);
    }
};

// 注册
mod.register("signUp", function (query, resp) {
    var name = query.name;
    var pwd = query.pwd;
    var sql = "select * from user where name='{0}'";
    sql = sql.format(name);
    mysql.query(sql, function (serr, svalues, sfields) {
       if (serr) {
           sql = "insert into user(name, pwd) values('{0}', '{1}')";
           sql = sql.format(name, pwd);
           mysql.query(sql, function (ierr, ivalues, ifields) {
               if (ierr) {
                   resp.sendStatus(500);
               } else {
                   resp.send({result : "success"});
               }
           });
       } else {
           resp.send({result : "exists name"});
       }
    });
});

// 登录
mod.register("signIn", function (query, resp) {
    var name = query.name;
    var pwd = query.pwd;
    var sql = "select * from user where name='{0}' and pwd='{1}'";
    sql = sql.format(name, pwd);
    mysql.query(sql, function (serr, svalues, sfields) {
        if (!serr) {
            resp.send({result : "success"});
        } else {
            resp.send({result : "error"});
        }
    });
});

// 请求基础信息
mod.register("userInfo", function (query, resp) {
    var id =
});