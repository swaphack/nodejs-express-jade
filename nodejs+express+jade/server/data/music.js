
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

    if (!mod.hand(req, resp)) {
        resp.send([]);
    }
};
//////////////////////////////////////////////////////////////////
// 菜单
mod.register("menu", function (query, resp) {
    if (musicFiles) {
        resp.send(musicFiles);
        return;
    }
    fs.readdir(musicDir, function (err, files) {
        if (err) {
            resp.send([]);
        } else {
            musicFiles = files;
            resp.send(musicFiles);
        }
    });
});

// 播放音乐
mod.register("play", function (query, resp) {
    console.log(query.name);
    var name = String.decode(query.name);
    console.log(name);
    if (!name) {
        resp.sendStatus(404);
        return;
    }
    resp.sendFile(musicDir + name);
});
