// http请求
(function (mod) {
    // 远程地址
    function getRootURL () {
        return "http://localhost:8080/";
    }
    // 逻辑视图地址
    function getViewURL() {
        return "http://localhost:8080/views/";
    }
    // 逻辑地址
    function getLogicURL() {
        return "http://localhost:8080/logic/";
    }

    // 访问根目录的数据
    function getRootURI (url) {
        return getRootURL() + url;
    }

    // 访问逻辑目录的数据
    function getLogicURI (url) {
        return getLogicURL() + url;
    }

    // 跳转界面
    function getViewURI(url) {
        return getViewURL() + url;
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

    // 跳转界面
    function redirect(url) {
        location.href = getViewURI(url);
    }

    var http = {
        getLogicURI : getLogicURI,
        getLogic : getLogic,
        postLogic : postLogic,
        redirect: redirect
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