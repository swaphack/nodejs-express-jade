const fs = require("fs");
const res = require("../resource");

// 获取目录下所有文件
function readDir() {
    var fullPath = res.root() + "/files";

    return fs.readdirSync(fullPath, "utf-8");
}

module.exports = function (url, params, callback) {
    var result = {};
    result.nav = readDir();
    if (callback) {
        callback(result);
    }
};