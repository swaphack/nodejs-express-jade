// 页面布局相关 jquery
(function (mod) {
    // 设置高度为最大高度
     function setMaxHeight(arg) {
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
    function setMaxWidth(arg) {
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
    function setCenter(child, parent) {
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
    }

    function setValue(id, value) {
        if ($(id)) {
            $(id).val(value);
        }
    }

    function getValue(id) {
        if ($(id)) {
            return $(id).val();
        }
        return null;
    }

    function click(id, func) {
        if ($(id)) {
            $(id).click(func);
        }
    }

    function isVisible(id) {
        if ($(id)) {
            return $(id).is(":visible");
        }
        return false;
    }

    function isHidden() {
        if ($(id)) {
            return $(id).is(":hidden");
        }
        return false;
    }

     var layout = {
         setMaxHeight : setMaxHeight,
         setMaxWidth : setMaxWidth,
         setCenter : setCenter,
         setValue : setValue,
         getValue : getValue,
         isVisible : isVisible,
         isHidden : isHidden,
         click : click
     };

     if (mod) {
         mod.lg = mod.lg || {};
         mod.lg.layout = layout;
     }
})(typeof self !== 'undefined' ? self
    : typeof window !== 'undefined' ? window
    : typeof global !== 'undefined' ? global
    : this
);
