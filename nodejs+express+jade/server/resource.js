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
res.setSearchPath = function (paths) {
    res._searchPath = [];
    if (!paths) {
        return;
    }
    if (!(paths instanceof Array)) {
        return;
    }
    for (var key in paths) {
        console.log("set search path : " + paths[key]);
        res._searchPath.push(res._getUnixPath(paths[key]));
    }
};

// 追加搜索路径
res.appendSearchPath = function (path) {
    if (!path) {
        return;
    }

    var p = res._getUnixPath(path);

    res._searchPath.push(p);
}

res._getUnixPath = function (url) {
    if (!url) {
        return null;
    }
    return url.replace("\\", "/");
}

res._getPath = function (path, fileName) {
    if (!path || !fileName) {
        return null;
    }
    var fullPath = path;
    if (fullPath.lastIndexOf("/") != fullPath.length - 1) {
        fullPath += "/";
    }
    fullPath += fileName;
    return fullPath.replace("//", "/");
}

// 完整路径
res.getFullPath = function (fileName) {
    if (!fileName) {
        return null;
    }

    if (!res._searchPath) {
        return null;
    }

    var file = fileName;
    if (file.indexOf("/") == 0) {
        file = file.substr(1, file.length - 1);
    }

    if (res._pathCache[file]) {
        return res._pathCache[file];
    }
    for (var dir in res._searchPath) {
        var fullPath = res._getPath(res._searchPath[dir], file);
        var result = fs.existsSync(fullPath);
        if (result) {
            res._pathCache[file] = fullPath;
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