// 工程资源

const fs = require("fs");

var res = {};

// 搜索路径
res._searchPath = [];
// 路径缓存
res._pathCache = {};
// 键值对
res._keyValueParis = {};

// 根目录
res.root = function () {
        return process.cwd();
    };

// 搜索目录
res.setSearchPath = function (path) {
    for (var key in path) {
        console.log("set search path : " + path[key]);
    }
    res._searchPath = path;
};

// 完整路径
res.getFullPath = function (fileName) {
    if (!fileName) {
        return null;
    }

    if (!res._searchPath) {
        return null;
    }

    if (res._pathCache[fileName]) {
        return res._pathCache[fileName];
    }
    for (var dir in res._searchPath) {
        var fullPath = res._searchPath[dir] + fileName;
        var result = fs.existsSync(fullPath);
        if (result) {
            res._pathCache[fileName] = fullPath;
            return fullPath;
        }
    }
    return null;
};

res.set = function (key, path) {
    if (!key || !path) {
        return;
    }
    res._keyValueParis[key] = path;
}

res.get = function (key) {
    if (!key) {
        return null;
    }
    return res._keyValueParis[key];
}

module.exports = res;