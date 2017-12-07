// 页面布局相关 jquery
(function (mod) {
    var data = {};

    // 取值
    function get(key, defaultValue) {
        if (!key) {
            return defaultValue;
        }

        return data[key] || defaultValue;
    }

    // 设置值
    function set (key, value) {
        if (!key) {
            return;
        }
        data[key] = value;

        this.save();
    }

    // 保存
    function save() {
        localStorage.user = data;
    }

    // 加载
    function load() {
        data = localStorage.user || {};
    }

    var user = {
        get: get,
        set: set,
        save: save,
        load: load,
    };

    load();

    if (mod) {
        mod.lg = mod.lg || {};
        mod.lg.user = user;
    }

})(typeof self !== 'undefined' ? self
    : typeof window !== 'undefined' ? window
    : typeof global !== 'undefined' ? global
    : this
);