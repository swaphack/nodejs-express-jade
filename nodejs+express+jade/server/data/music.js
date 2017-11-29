
var fs = require("fs");
var Protocol = new require("../common/protocol").Protocol;

var Music = function () {
    Protocol.call(this);

    this.musicFiles = null;
}

Music.prototype = new Protocol();
Music.prototype.constructor = Music;

var musicDir = "E:/CloudMusic/";

var mod = new Music();
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
    if (this.musicFiles) {
        resp.send(this.musicFiles);
        return;
    }
    fs.readdir(musicDir, function (err, files) {
        if (err) {
            resp.send([]);
        } else {
            this.musicFiles = files;
            resp.send(this.musicFiles);
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
