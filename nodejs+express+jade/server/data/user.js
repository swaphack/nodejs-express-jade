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
// 注册
mod.register("signUp", function (packet, resp) {
    var name = packet.getValue("name");
    var pwd = packet.getValue("pwd");
    if (!name || !pwd) {
        resp.sendStatus(400);
        return;
    }

    var sql = "select * from user where name={0}";
    sql = lg.mysql.format(sql, name);
    lg.mysql.query(sql, function (serr, svalues, sfields) {
        if (serr) {
            resp.sendStatus(400);
            return;
        }
       if (svalues.length !== 0) {
           sql = "insert into user(name, pwd) values({0}, {1})";
           sql = lg.mysql.format(sql, name, pwd);
           lg.mysql.query(sql, function (ierr, ivalues, ifields) {
               if (ierr) {
                   resp.sendStatus(400);
                   return;
               }
               if (ierr) {
                   resp.sendStatus(500);
               } else {
                   resp.sendPacket(lg.packet.createPacket());
               }
           });
       } else {
           resp.sendPacket(lg.packet.createErrorPacket("exists name"));
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
    var sql = "select * from user where name={0} and pwd={1}";
    sql = lg.mysql.format(sql, name, pwd);
    lg.mysql.query(sql, function (serr, svalues, sfields) {
        if (serr) {
            resp.sendStatus(400);
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