var res = require("./resource");
var server = require("./server");
var database = require("./database");

function init() {
    // 音乐文件
    res.set("audio_dir", "E:/CloudMusic/");

    // 搜索路径设置
    var root = res.root();
    var searchPath = [];

    searchPath.push(root + "/");
    // 视图
    searchPath.push(root + "/views/");
    // 公共资源
    searchPath.push(root + "/public/");

    searchPath.push(res.get("audio_dir"));

    res.setSearchPath(searchPath);
}

module.exports = function () {
    init();
    server.init();
    database.init();
}