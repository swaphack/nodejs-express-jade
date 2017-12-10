var server = require("./server/server");
var database = require("./server/database");
var lg = require("./server/common/index");
var ini = require("./ini");

function init() {
    // 搜索路径设置
    var root = lg.filePath.root();
    var searchPath = [];
    for (var i in ini.res) {
        searchPath.push(root + ini.res[i]);
    }
    lg.filePath.setSearchPath(searchPath);
}

init();
server.init(ini.server);
database.initMysql(ini.database);
database.initXml(ini.xmlConfig);

