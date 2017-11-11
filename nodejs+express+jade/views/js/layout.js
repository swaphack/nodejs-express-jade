var layout = (function (mod) {
    // 页面布局相关 jquery
    mod.setMaxHeight = function(arg) {
        var args = arguments;
        if (!args) {
            return;
        }
        var maxHeight = 0;
        for (var i in args) {
            if (args[i].outerHeight() > maxHeight) {
                maxHeight = args[i].outerHeight();
            }
        }
        for (var i in args) {
            args[i].height(maxHeight);
        }
    }

    mod.setMaxWidth = function (arg) {
        var args = arguments;
        if (!args) {
            return;
        }
        var maxWidth = 0;
        for (var i in args) {
            if (args[i].outerWidth() > maxWidth) {
                maxWidth = [i].outerWidth();
            }
        }

        for (var i in args) {
            args[i].height(maxWidth);
        }
    }

    return mod;
}({}));
