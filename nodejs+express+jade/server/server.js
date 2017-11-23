var express = require('express');
var app = express();

var path = require("path");
var filePath = require("./common/filePath");
var views = require("./views/_index");

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

function doView(req, resp) {
    try {
        views.direct(req, resp);
    } catch (err) {
        resp.sendStatus(404);
    }
}

function doData(req, resp) {
    var baseUrl = req.url;
    var spot = baseUrl.indexOf("?");
    if (spot != -1) {
        baseUrl = baseUrl.substr(0, spot);
    }
    try {
        var script = require("./" + baseUrl);
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
    // 视图引擎设置
    app.set('views', './views');
    app.set('view engine', 'jade');
    app.engine('jade', require('jade').__express);

    // 视图
    app.get("/views/*", function (req, resp) {
        var baseUrl = req.url;
        var spot = baseUrl.indexOf("?");
        if (spot != -1) {
            baseUrl = baseUrl.substr(0, spot);
        }
        if (path.extname(baseUrl)) {
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