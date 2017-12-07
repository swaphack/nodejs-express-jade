
var fs = require("fs");
var lg = require("../common/index");

var musicFiles = null;
var musicDir = "E:/CloudMusic/";

var mod = new lg.protocol.Protocol();
mod.setID("action");

module.exports = function (req, resp) {
    if (!req || !resp) {
        return false;
    }

    mod.hand(req, resp);
};
//////////////////////////////////////////////////////////////////
// 菜单
mod.register("menu", function (packet, resp) {
    if (musicFiles) {
        var p = lg.packet.createPacket();
        p.setContent(musicFiles);
        resp.sendPacket(p);
        return;
    }
    fs.readdir(musicDir, function (err, files) {
        var p = lg.packet.createPacket();
        if (err) {
            p.setError(err);
            resp.sendPacket(p);
        } else {
            musicFiles = files;
            p.setContent(musicFiles);
            resp.sendPacket(p);
        }
    });
});

// 播放音乐
mod.register("play", function (packet, resp) {
    var name = packet.getValue("name");
    console.log(name);
    name = String.decodeURL(name);
    console.log(name);
    if (!name) {
        resp.sendStatus(404);
        return;
    }
    resp.sendFile(musicDir + name);
});
