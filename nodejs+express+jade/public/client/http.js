// http请求
var http = (function (mod) {
    mod.baseURL = function () {
        return "../";
    };
    mod.remoteURL = function () {
        return "http://localhost:8080/";
    };

    mod.getDataURL = function (url) {
        return "../" + url;
    };

    mod.get = function (url, data, callback) {
        $.get(mod.getDataURL(url), data, callback, 'json');
    };

    mod.post = function (url, data, callback) {
        $.post(mod.getDataURL(url), data, callback, 'json');
    };

    return mod;
}({}));