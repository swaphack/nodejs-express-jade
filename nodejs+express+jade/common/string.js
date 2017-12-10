// 文本字符串相关
(function () {
    var base64 = Base64;

    var randLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    // 格式化文本，例如：string.format("a {0} on {1}", "apple", "desk");
    String.prototype.format = function (arg) {
        if (arguments.length === 0) {
            return String(this);
        }
        var args = null;
        if (arguments.length === 1 && Array.isArray(arguments[0])) {
            args = arguments[0];
        } else {
            for (var i = 0; i < arguments.length; i++) {
                var type = typeof arguments[i];
                if (type !== "number" && type !== "boolean" && type !== "string" ) {
                    throw new Error("Params must be one type of [number, boolean, string]");
                }
            }
            args = arguments;
        }
        var pattern = new RegExp("{([0-9]+)}", "g");
        return String(this).replace(pattern, function (match, index) {
            var currentIndex = parseInt(index);
            if (currentIndex >= args.length || currentIndex < 0) {
                throw new Error("Out of index");
            }
            return args[currentIndex];
        });
    };

    // 随机文本
    String.randomWord = function (len) {
        var text = "";
        for( var i=0; i < len; i++ )
            text += randLetters.charAt(Math.floor(Math.random() * randLetters.length));
        return text;
    };

    String.randomWordByRange = function (min, max) {
        var str = String.randomWord(min);
        str += String.randomWord(max - min);
        return str;
    };

    // url 编码
    String.encodeURI = function (value) {
        var val = base64.encode(value);
        return encodeURIComponent(val);
    };

    // url 解码
    String.decodeURI = function (value) {
        var val = decodeURIComponent(value);
        return base64.decode(val);
    };
})();



