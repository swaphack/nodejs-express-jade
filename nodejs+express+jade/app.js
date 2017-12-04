var server = require("./server/server");
var database = require("./server/database");
var lg = require("./server/common/index");
var ini = require("./ini");

function init() {
    // 搜索路径设置
    var root = lg.filePath.root();
    var searchPath = [];

    searchPath.push(root + "/");
    // 视图
    //searchPath.push(root + "/views/");
    // 页面资源
    searchPath.push(root + "/public/");
    // 文件资源
    searchPath.push(root + "/files/");
    // 公共代码
    searchPath.push(root + "/common/");

    lg.filePath.setSearchPath(searchPath);
}

init();
server.init(ini.server);
database.initMysql(ini.database);
database.initXml(ini.xmlConfig);

