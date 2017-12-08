var fs = require("fs");
var lg = require("../common/index");

var mod = lg.protocol.createProtocol();
mod.setID("action");

module.exports = function (req, resp) {
    if (!req || !resp) {
        return false;
    }

    mod.hand(req, resp);
};
//////////////////////////////////////////////////////////////////
// 数据
var cache = lg.cache.createCache();
cache.set("dir", "E:/CloudMusic/");
cache.set("files", null);

// 菜单
mod.register("menu", function (packet, resp) {
    if (cache.get("files")) {
        var p = lg.packet.createPacket();
        p.setContent(cache.get("files"));
        resp.sendPacket(p);
        return;
    }
    fs.readdir(cache.get("dir"), function (err, files) {
        var p = lg.packet.createPacket();
        if (err) {
            p.setError(err);
            resp.sendPacket(p);
        } else {
            cache.set("files", files);
            p.setContent(cache.get("files"));
            resp.sendPacket(p);
        }
    });
});

// 播放音乐
mod.register("play", function (packet, resp) {
    var name = packet.getValue("name");
    name = String.decodeURI(name);
    if (!name) {
        resp.sendStatus(404);
        return;
    }
    resp.sendFile(cache.get("dir") + name);
});
