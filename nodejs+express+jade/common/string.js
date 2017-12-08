// 文本字符串相关
(function () {
    var randLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    // 格式化文本，例如：string.format("a {0} on {1}", "apple", "desk");
    String.prototype.format = function (arg) {
        var params = arguments;
        if (params.length === 0) {
            return String(this);
        }
        var args = null;
        if (params.length === 1 && Array.isArray(params[0])) {
            args = params[0];
        } else {
            for (var i = 0; i < params.length; i++) {
                var type = typeof params[i];
                if (type !== "number" && type !== "boolean" && type !== "string" ) {
                    throw new Error("Params must be one type of [number, boolean, string]");
                }
            }
            args = params;
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
        var val = Base64.encode(value);
        return encodeURIComponent(val);
    };

    // url 解码
    String.decodeURI = function (value) {
        var val = decodeURIComponent(value);
        return Base64.decode(val);
    };
})();



