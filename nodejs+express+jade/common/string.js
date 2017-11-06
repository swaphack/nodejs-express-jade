
var string = {};
    /*
    *   格式化文本，
    *   例如：string.format("a {0} on {1}", "apple", "desk");
    */
string.format = function (arg) {
    var args = arguments;
    var pattern = new RegExp("{([0-9]+)}", "g");
    return String(arg).replace(pattern, function (match, index) {
        var currentIndex = parseInt(index);
        if (currentIndex + 1 > args.length || currentIndex < 0) {
            throw new Error("out of index");
        }
        return args[currentIndex + 1];
    });
};

module.exports = string;