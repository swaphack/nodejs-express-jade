// 工程资源

const fs = require("fs");

// 搜索路径
var searchPath = [];
// 路径缓存
var pathCache = {};

var res = {};

// 目录
res.root = function () {
        return process.cwd();
    };

// 搜索目录
res.setSearchPath = function (path) {
    for (var key in path) {
        console.log("set search path : " + path[key]);
    }
    searchPath = path;
};

// 完整路径
res.getFullPath = function (fileName) {
    if (!fileName) {
        return null;
    }

    if (!searchPath) {
        return null;
    }

    if (pathCache[fileName]) {
        return pathCache[fileName];
    }
    for (var dir in searchPath) {
        var fullPath = searchPath[dir] + fileName;
        var result = fs.existsSync(fullPath);
        if (result) {
            console.log("search full path : " + fullPath);
            pathCache[fileName] = fullPath;
            return fullPath;
        }
    }
    return null;
};

module.exports = res;