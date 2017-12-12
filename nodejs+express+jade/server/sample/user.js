var lg = require("../common/index");

var mod = lg.protocol.createProtocol();
mod.setID("action");

module.exports = function (req, resp) {
    if (!req || !resp) {
        return;
    }

    mod.hand(req, resp);
};

var mysql = lg.mysql.getDB("user");

//////////////////////////////////////////////////////////////////
// 注册
mod.register("signUp", function (packet, resp) {
    var name = packet.getValue("name");
    var pwd = packet.getValue("pwd");

    var sql = "select * from user where name={0}".formatSQL(name);
    mysql.query(sql, function (serr, svalues) {
        if (serr) {
            resp.sendStatus(500);
            return;
        }
        if (svalues.length !== 0) { // 有数据
            resp.sendPacket(lg.packet.createErrorPacket("exists name"));
        } else {
            sql = "insert into user(name, pwd) values({0}, {1})".formatSQL(name, pwd);
            mysql.query(sql, function (ierr, ivalues, ifields) {
                if (ierr) {
                    resp.sendStatus(500);
                    return;
                }
                sql = "select * from user where name={0}".formatSQL(name);
                mysql.query(sql, function (err, values) {
                    if (ierr) {
                        resp.sendStatus(500);
                        return;
                    }

                    if (values.length === 0) { // 有数据
                        resp.sendStatus(500);
                        return;
                    }

                    sql = "insert into user_info(id) values({0})".formatSQL(values[0]["id"]);
                    mysql.query(sql);

                    var t = {"id" : values[0]["id"]};
                    var p = lg.packet.createPacket(t);
                    resp.sendPacket(p);
                });
            });
        }
    });
});

// 登录
mod.register("signIn", function (packet, resp) {
    var name = packet.getValue("name");
    var pwd = packet.getValue("pwd");

    var sql = "select * from user where name={0} and pwd={1}".formatSQL(name, pwd);
    mysql.query(sql, function (err, values) {
        if (err) {
            resp.sendStatus(500);
            return;
        }
        if (values.length !== 0) {
            var t = {"id" : values[0]["id"]};
            var p = lg.packet.createPacket(t);
            resp.sendPacket(p);
        } else {
            resp.sendPacket(lg.packet.createErrorPacket("not exists this role"));
        }
    });
});

// 请求基础信息
mod.register("userInfo", function (packet, resp) {
    var id = packet.getValue("id");

    var sql = "select u.name, ui.vip, ui.level, ui.exp, ui.gold from user u, user_info ui where u.id = {0} and ui.id = {0}".formatSQL(id);
    mysql.query(sql, function (err, values) {
        if (err) {
            resp.sendStatus(500);
            return;
        }
        if (values.length !== 0) {
            var p = lg.packet.createPacket(values[0]);
            resp.sendPacket(p);
        } else {
            resp.sendPacket(lg.packet.createErrorPacket("not exists this role"));
        }
    });
});