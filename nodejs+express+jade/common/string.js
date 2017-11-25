// 文本字符串相关

(function () {
    var randLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

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
}());



