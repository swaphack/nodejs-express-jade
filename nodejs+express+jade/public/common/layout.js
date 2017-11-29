// 页面布局相关 jquery
var layout = (function (mod) {
    // 设置高度为最大高度
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
    // 设置宽度为最大高度
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

    // 子类处于居中状态
    mod.setCenter = function (child, parent) {
        if (!child) {
            return;
        }
        parent = child.parent();

        console.log(child.outerWidth() + " : " + child.outerHeight());
        console.log(parent.width() + " : " + parent.height());

        child.css({
            "left": (parent.width() - child.outerWidth()) * 0.5,
            "top": (parent.height() - child.outerHeight()) * 0.5
        });
    };

    return mod;
}({}));
