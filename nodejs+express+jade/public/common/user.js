// 页面布局相关 jquery
(function (mod) {
    var user = {
        _data : {},

        get : function (key, defaultValue) {
            if (!key) {
                return defaultValue;
            }

            return this._data[key] || defaultValue;
        },

        set : function (key, value) {
            if (!key) {
                return;
            }
            this._data[key] = value;
        }
    }

    if (mod) {
        mod.lg = mod.lg || {};
        mod.lg.user = user;
    }

})(typeof self !== 'undefined' ? self
    : typeof window !== 'undefined' ? window
    : typeof global !== 'undefined' ? global
    : this
);