var res = require("./resource");
var server = require("./server");
var database = require("./database");

function init() {
    // 搜索路径设置
    var root = res.root();
    var searchPath = [];
    searchPath.push(root + "/");
    res.setSearchPath(searchPath);
}

module.exports = function () {
    init();
    server.init();
    database.init();
}