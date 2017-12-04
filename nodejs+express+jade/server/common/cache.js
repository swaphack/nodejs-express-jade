(function () {
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

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = Cache;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return Cache });
    }
}());



