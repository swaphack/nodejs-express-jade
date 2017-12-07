(function () {
    var text = {
        _texts : {},

        get : function (name) {
            if (!name) {
                return "";
            }

            return mod._texts[name];
        },

        set : function (name, value) {
            if (!name) {
                return;
            }
            mod._texts[name] = value;
        }
    };
})();