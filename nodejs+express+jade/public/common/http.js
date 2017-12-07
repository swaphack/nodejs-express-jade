// http请求
(function (mod) {
    // 远程地址
    function getRemoteURL () {
        return "http://localhost:8080/";
    }
    // 逻辑地址
    function getLogicURL() {
        return "http://localhost:8080/logic/";
    }

    // 访问根目录的数据
    function getRootURL (url) {
        return getRemoteURL() + url;
    }

    // 访问逻辑目录的数据
    function getLogicURI (url) {
        return getLogicURL() + url;
    }

    // get 请求方式
    function getLogic(url, data, callback) {
        var httpUrl = getLogicURI(url);
        console.log("get url", httpUrl);
        var value = null;
        if (data instanceof packet.Packet) {
            value = data.data();
        } else {
            value = data;
        }
        $.get(httpUrl, value, function (result) {
            console.log(result);
            if (callback) {
                callback(result.error, result.content);
            }
        }, 'json');
    }

    // post 请求方式
    function postLogic(url, data, callback) {
        var httpUrl = getLogicURI(url);
        console.log("post url", httpUrl);
        var value = null;
        if (data instanceof packet.Packet) {
            value = data.data();
        } else {
            value = data;
        }
        $.post(httpUrl, value, function (result) {
            console.log(result);
            if (callback) {
                callback(result.error, result.content);
            }
        }, 'json');
    }

    var http = {
        getLogicURI : getLogicURI,
        getLogic : getLogic,
        postLogic : postLogic,
    };

    if (mod) {
        mod.lg = mod.lg || {};
        mod.lg.http = http;
    }
}(typeof self !== 'undefined' ? self
    : typeof window !== 'undefined' ? window
    : typeof global !== 'undefined' ? global
    : this
));