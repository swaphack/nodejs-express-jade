// http请求
var http = (function (mod) {
    // 远程地址
    mod.remoteURL = function () {
        return "http://localhost:8080/";
    };
    // 逻辑地址
    mod.logicURL = function () {
        return "http://localhost:8080/logic/";
    };

    // 访问根目录的数据
    mod.getRootURL = function (url) {
        return mod.remoteURL() + url;
    };

    // 访问逻辑目录的数据
    mod.getLogicURL = function (url) {
        return mod.logicURL() + url;
    };
    // get 请求方式
    mod.getLogic = function (url, data, callback) {
        var httpUrl = mod.getLogicURL(url);
        console.log("get url", httpUrl);
        $.get(httpUrl, data, callback, 'json');
    };

    // post 请求方式
    mod.postLogic = function (url, data, callback) {
        var httpUrl = mod.getLogicURL(url);
        console.log("post url", httpUrl);
        $.post(httpUrl, data, callback, 'json');
    };

    return mod;
}({}));