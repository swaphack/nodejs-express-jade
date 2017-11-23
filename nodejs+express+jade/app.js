var server = require("./server/server");
var database = require("./server/database");
var filePath = require("./server/common/filePath");
var ini = require("./ini");

function init() {
    // 搜索路径设置
    var root = filePath.root();
    var searchPath = [];

    searchPath.push(root + "/");
    // 视图
    //searchPath.push(root + "/views/");
    // 页面资源
    //searchPath.push(root + "/public/");
    // 文件资源
    //searchPath.push(root + "/files/");

    filePath.setSearchPath(searchPath);
}

init();
server.init(ini.server);
database.init(ini.database);

