(function () {
    // 缓存
    function Cache() {
        // 键值对
        this._values = {};
    }
    // 设置缓存
    Cache.prototype.set = function (key, value) {
        if (!key) {
            return;
        }
        this._values[key] = value;
    }

    // 获取缓存
    Cache.prototype.get = function (key) {
        if (!key) {
            return null;
        }
        return this._values[key];
    }
    
    function createCache() {
        return new Cache();
    }
    /////////////////////////////////////////////////////////////////
    var cache = {
        Cache : Cache,
        createCache : createCache
    }

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = cache;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return cache });
    }
})();



