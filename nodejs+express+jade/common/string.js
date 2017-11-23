// 文本字符串相关

// 格式化文本，例如：string.format("a {0} on {1}", "apple", "desk");
String.prototype.format = function (arg) {
    var args = arguments;
    var pattern = new RegExp("{([0-9]+)}", "g");
    return String(this).replace(pattern, function (match, index) {
        var currentIndex = parseInt(index);
        if (currentIndex >= args.length || currentIndex < 0) {
            throw new Error("out of index");
        }
        return args[currentIndex];
    });
};