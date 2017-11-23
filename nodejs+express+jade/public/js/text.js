var text = (function (mod) {
    mod._texts = {};
    mod.get = function (name) {
        if (!name) {
            return "";
        }

        return mod._texts[name];
    };

    mod.set = function (name, value) {
        if (!name) {
            return;
        }
        mod._texts[name] = value;
    };
    return mod;
}({}));