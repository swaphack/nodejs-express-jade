(function () {
    // 工程目录
    const fs = require("fs");

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
        if (fullPath.lastIndexOf("/") !== (fullPath.length - 1)) {
            fullPath += "/";
        }
        fullPath += fileName;
        return getUnixPath(fullPath);
    }

    var filePath = {
        // 搜索路径
        searchPath : [],
        // 路径缓存
        pathCache : {},
        // 执行的根目录
        root : function () {
            return process.cwd();
        },
        // 设置搜索路径
        setSearchPath : function (paths) {
            this.searchPath = [];
            if (!paths) {
                return;
            }
            if (!(paths instanceof Array)) {
                return;
            }
            for (var key in paths) {
                console.log("set search path : " + paths[key]);
                this.searchPath.push(getUnixPath(paths[key]));
            }
        },
        // 添加搜索路径
        appendSearchPath : function (path) {
            if (!path) {
                return;
            }
            var p = getUnixPath(path);
            this.searchPath.push(p);
        },
        // 获取完整路径
        getFullPath : function (fileName) {
            if (!fileName) {
                return null;
            }

            if (!this.searchPath) {
                return null;
            }

            var file = fileName;
            if (file.indexOf("/") === 0) {
                file = file.substr(1, file.length - 1);
            }

            if (this.pathCache[file]) {
                return this.pathCache[file];
            }
            for (var dir in this.searchPath) {
                var fullPath = getPath(this.searchPath[dir], file);
                var result = fs.existsSync(fullPath);
                if (result) {
                    this.pathCache[file] = fullPath;
                    return fullPath;
                }
            }
            return null;
        }
    };

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = filePath;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return filePath });
    }
}());
