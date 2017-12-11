// 文本字符串相关
(function (mod) {
    var randLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    // 格式化文本，例如：string.format("a {0} on {1}", "apple", "desk");
    String.prototype.format = function (arg) {
        if (arguments.length === 0) {
            return String(this);
        }
        var args = null;
        if (arguments.length === 1 && isDictionary(arguments[0])) {
            args = arguments[0];
        } else {
            args = arguments;
        }
        for (var key in args) {
            var index = parseInt(key);
            var type = typeof args[index];
            if (type !== "number" && type !== "boolean" && type !== "string" ) {
                throw new Error("Params must be one type of [number, boolean, string]");
            }
        }
        var pattern = new RegExp("{([0-9]+)}", "g");
        return String(this).replace(pattern, function (match, index) {
            var currentIndex = parseInt(index);
            if (currentIndex >= args.length || currentIndex < 0) {
                throw new Error("Out of index");
            }
            return args[index];
        });
    };

    // 随机文本
    function randomWord(len) {
        var text = "";
        for( var i=0; i < len; i++ )
            text += randLetters.charAt(Math.floor(Math.random() * randLetters.length));
        return text;
    }

    function randomWordByRange(min, max) {
        var str = String.randomWord(min);
        str += String.randomWord(max - min);
        return str;
    }

    // url 编码
    function encodeURI(value) {
        var val = Base64.encode(value);
        return encodeURIComponent(val);
    }

    // url 解码
    function decodeURI(value) {
        var val = decodeURIComponent(value);
        return Base64.decode(val);
    }

    function isUndefined(value) {
        return typeof value === "undefined";
    }

    function isBoolean(value) {
        return typeof value === "boolean" && value.constructor === Boolean;
    }

    function isNumber(value) {
        return typeof value === "number" && value.constructor === Number;
    }

    function isString(value) {
        return typeof value === "string" && value.constructor === String;
    }

    function isFunction(value) {
        return typeof value === "function" && value.constructor === Function;
    }

    function isArray(value) {
        return Array.isArray(value);
    }

    function isNull(value) {
        return value === null;
    }

    function isDictionary(value) {
        if (isNull(value) || isArray(value)) {
            return false;
        }
        return !isUndefined(value) && !isBoolean(value) && !isNumber(value) && !isString(value) && !isFunction(value);
    }

    function isNullOrUndefined(value) {
        return isNull(value) || isUndefined(value);
    }

    ////////////////////////////////////////////////////////////////////
    // export
    var tool = {
        randomWord : randomWord,
        randomWordByRange : randomWordByRange,
        encodeURI : encodeURI,
        decodeURI : decodeURI,
        isUndefined : isUndefined,
        isNull : isNull,
        isBoolean : isBoolean,
        isNumber : isNumber,
        isFunction : isFunction,
        isArray : isArray,
        isDictionary : isDictionary,
        isNullOrUndefined: isNullOrUndefined
    };

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = tool;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return tool });
    }

    if (mod) {
        mod.tool = tool;
    }

})(typeof self !== 'undefined' ? self
    : typeof window !== 'undefined' ? window
    : typeof global !== 'undefined' ? global
    : this
);



