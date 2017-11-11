const fs = require("fs");
const res = require("../resource");

// 获取目录下所有文件
function readDir() {
    var result = {};
    var fullPath = res.get("audio_dir");
    var files = fs.readdirSync(fullPath, "utf-8");
    for (var i in files) {
        console.log(files[i]);
        result[files[i]] = files[i];
    }

    return result;
}

module.exports = function (url, params, callback) {
    var result = {};
    result.nav = readDir();
    if (callback) {
        callback(result);
    }
};