var express = require('express');
var session = require('express-session');

require("../common/string");

var path = require("path");
var filePath = require("./common/filePath");
var views = require("./views/_index");

// 请求网址
function baseUrl(url) {
    var spot = url.indexOf("?");
    if (spot != -1) {
        url = url.substr(0, spot);
    }
    
    return url;
}

// 资源处理
function doRes(req, resp) {
    var baseUrl = req.params[0];
    var fullPath = filePath.getFullPath(baseUrl);
    if (!fullPath) {
        fullPath = filePath.getFullPath(baseUrl.substr(baseUrl.indexOf("/") + 1));
    }
    if (fullPath) {
        resp.sendFile(fullPath);
    } else {
        resp.sendStatus(404);
    }
}

// 视图处理
function doView(req, resp) {
    try {
        views.direct(req, resp);
    } catch (err) {
        resp.sendStatus(404);
    }
}

// 数据处理
function doData(req, resp) {
    var url = baseUrl(req.url);
    
    try {
        var script = require("./" + url);
        if (script) {
            script(req, resp);
        }
    } catch (err) {
        try {
            views.direct(req, resp);
        } catch (perr) {
            resp.sendStatus(400);
        }
    }
}

module.exports.init = function (server) {

    var app = express();

    // 视图引擎设置
    app.set('views', './views');
    app.set('view engine', 'jade');
    app.engine('jade', require('jade').__express);

    // session
    app.use(session({
        secret: String.randomWord(20),
        cookie: {maxAge: 60 * 1000 * 30},
        resave: false,
        saveUninitialized: true
    }));

    // 视图
    app.get("/views/*", function (req, resp) {
        var url = baseUrl(req.url);
        if (path.extname(url)) {
            doRes(req, resp);
        } else {
            doView(req, resp);
        }
    });

    // 数据
    app.get("/data/*", function (req, resp) {
        doData(req, resp);
    });

    app.listen(server.port);
};