// http请求
var http = (function (mod) {
    mod.baseURL = function () {
        return "../";
    };
    mod.remoteURL = function () {
        return "http://localhost:8080/";
    };

    mod.getRootURL = function (url) {
        return mod.remoteURL() + url;
    };

    mod.get = function (url, data, callback) {
        var httpUrl = mod.getRootURL(url);
        console.log(httpUrl);
        $.get(httpUrl, data, callback, 'json');
    };

    mod.post = function (url, data, callback) {
        $.post(mod.getRootURL(url), data, callback, 'json');
    };

    return mod;
}({}));