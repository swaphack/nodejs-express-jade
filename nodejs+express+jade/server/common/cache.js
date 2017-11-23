// 缓存
function Cache() {
    // 键值对
    this._values = {};
}
// 设置缓存
Cache.prototype.set = function (key, path) {
    if (!key || !path) {
        return;
    }
    this._values[key] = path;
}

// 获取缓存
Cache.prototype.get = function (key) {
    if (!key) {
        return null;
    }
    return this._values[key];
}

// 全局
var cache = new Cache();
// 类
module.exports.Cache = Cache;
// 全局
module.exports.cache = function () {
    return cache;
}


