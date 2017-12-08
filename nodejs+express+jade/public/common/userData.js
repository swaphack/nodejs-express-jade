// 页面布局相关 jquery
(function (mod) {
    var userData = {
        // 取值
        get : function (key, defaultValue) {
            if (!key) {
                return defaultValue;
            }
            return localStorage.getItem(key) || defaultValue;
        },
        // 设置值
        set : function (key, value) {
            if (!key) {
                return;
            }
            localStorage.setItem(key, value);
        }
    };

    if (mod) {
        mod.lg = mod.lg || {};
        mod.lg.userData = userData;
    }

})(typeof self !== 'undefined' ? self
    : typeof window !== 'undefined' ? window
    : typeof global !== 'undefined' ? global
    : this
);