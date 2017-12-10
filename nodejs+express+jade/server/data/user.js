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
// 注册
mod.register("signUp", function (packet, resp) {
    var name = packet.getValue("name");
    var pwd = packet.getValue("pwd");
    if (!name || !pwd) {
        resp.sendStatus(400);
        return;
    }

    var sql = "select * from user where name={0}".formatSQL(name);
    lg.mysql.query(sql, function (serr, svalues, sfields) {
        if (serr) {
            resp.sendStatus(500);
            return;
        }
       if (svalues.length !== 0) { // 有数据
           resp.sendPacket(lg.packet.createErrorPacket("exists name"));
       } else {
           sql = "insert into user(name, pwd) values({0}, {1})".formatSQL(name, pwd);
           lg.mysql.query(sql, function (ierr, ivalues, ifields) {
               if (ierr) {
                   resp.sendStatus(500);
                   return;
               }
               if (ivalues.length !== 0) {
                   resp.sendStatus(500);
               } else {
                   var p = lg.packet.createPacket();
                   p.setContent(ivalues[0]["id"]);
                   resp.sendPacket(p);
               }
           });
       }
    });
});

// 登录
mod.register("signIn", function (packet, resp) {
    var name = packet.getValue("name");
    var pwd = packet.getValue("pwd");
    if (!name || !pwd) {
        resp.sendStatus(400);
        return;
    }
    var sql = "select * from user where name={0} and pwd={1}".formatSQL(name, pwd);
    lg.mysql.query(sql, function (serr, svalues, sfields) {
        if (serr) {
            resp.sendStatus(500);
            return;
        }
        if (svalues.length !== 0) {
            var p = new lg.packet.Packet();
            p.setContent(svalues[0]["id"]);
            resp.sendPacket(p);
        } else {
            resp.sendPacket(lg.packet.createErrorPacket("not exists this role"));
        }
    });
});

// 请求基础信息
mod.register("userInfo", function (packet, resp) {
    var id = packet.getValue("id");
});