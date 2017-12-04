var lg = require("../common/index");

var mod = new lg.protocol.Protocol();
mod.setID("action");

module.exports = function (req, resp) {
    if (!req || !resp) {
        return false;
    }

    if (!mod.hand(req, resp)) {
        resp.send([]);
    }
};
//////////////////////////////////////////////////////////////////
// 注册
mod.register("signUp", function (query, resp) {
    var name = query.name;
    var pwd = query.pwd;
    if (!name || !pwd) {
        resp.sendStatus(400);
        return;
    }

    var sql = "select * from user where name='{0}'";
    sql = sql.format(name);
    lg.mysql.query(sql, function (serr, svalues, sfields) {
       if (svalues.length != 0) {
           sql = "insert into user(name, pwd) values('{0}', '{1}')";
           sql = sql.format(name, pwd);
           lg.mysql.query(sql, function (ierr, ivalues, ifields) {
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
    if (!name || !pwd) {
        resp.sendStatus(400);
        return;
    }
    var sql = "select * from user where name='{0}' and pwd='{1}'";
    sql = sql.format(name, pwd);
    lg.mysql.query(sql, function (serr, svalues, sfields) {
        if (svalues.length != 0) {
            resp.send({result : "success", id : svalues[sfields["id"]]});
        } else {
            resp.send({result : "not exists this role"});
        }
    });
});

// 请求基础信息
mod.register("userInfo", function (query, resp) {
    var id = query.id;
});