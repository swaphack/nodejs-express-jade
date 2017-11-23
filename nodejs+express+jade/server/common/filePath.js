// 工程目录
const fs = require("fs");

// 搜索路径
var searchPath = [];
// 路径缓存
var pathCache = {};

var filePath = {};
module.exports = filePath;

function getUnixPath(url) {
    if (!url) {
        return null;
    }
    return url.replace("\\", "/");
}

// 获取路径
function getPath(path, fileName) {
    if (!path || !fileName) {
        return null;
    }
    var fullPath = path;
    if (fullPath.lastIndexOf("/") != (fullPath.length - 1)) {
        fullPath += "/";
    }
    fullPath += fileName;
    return getUnixPath(fullPath);
}

// 根目录
filePath.root = function () {
    return process.cwd();
};

// 搜索目录
filePath.setSearchPath = function (paths) {
    searchPath = [];
    if (!paths) {
        return;
    }
    if (!(paths instanceof Array)) {
        return;
    }
    for (var key in paths) {
        console.log("set search path : " + paths[key]);
        searchPath.push(getUnixPath(paths[key]));
    }
};

// 追加搜索路径
filePath.appendSearchPath = function (path) {
    if (!path) {
        return;
    }
    var p = getUnixPath(path);
    searchPath.push(p);
}

// 完整路径
filePath.getFullPath = function (fileName) {
    if (!fileName) {
        return null;
    }

    if (!searchPath) {
        return null;
    }

    var file = fileName;
    if (file.indexOf("/") == 0) {
        file = file.substr(1, file.length - 1);
    }

    if (pathCache[file]) {
        return pathCache[file];
    }
    for (var dir in searchPath) {
        var fullPath = getPath(searchPath[dir], file);
        var result = fs.existsSync(fullPath);
        if (result) {
            pathCache[file] = fullPath;
            return fullPath;
        }
    }
    return null;
};