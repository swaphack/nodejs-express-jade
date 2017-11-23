var fs = require("fs");
var Protocol = new require("./protocol").Protocol;

var Music = function () {
    Protocol.call(this);

    this.musicFiles = null;
}

Music.prototype = new Protocol();
Music.prototype.constructor = Music;

var musicDir = "E:/CloudMusic/";

var music = new Music();
music.setID("action");

// 菜单
music.register("menu", function (query, resp) {
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
music.register("play", function (query, resp) {
    var name = new Buffer(query.name, 'base64').toString();
    if (!name) {
        resp.sendStatus(404);
        return;
    }

    resp.sendFile(musicDir + name);
});

// 音乐文件请求
module.exports = function (req, resp) {
    if (!req || !resp) {
        return false;
    }

    if (!music.hand(req, resp)) {
        resp.send([]);
    }
};