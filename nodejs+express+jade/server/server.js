var path = require("path");
var ini = require("../ini");

var port = ini.server.port;
var express = require('express');
var app = express();
var res = require("./resource");

// 参数设置
res.set("abs_js_dir", "./server/views/");
res.set("rel_js_dir", "./views/");
res.set("abs_view_dir", "./views/jade/");

res.set("js", ".js");
res.set("jade", ".jade");

function checkJadeExists(url) {
    var fullPath = res.getFullPath(res.get("abs_view_dir") + url + res.get("jade"));
    if (!fullPath) {
        console.info("jade view not exists : " + url);
    }
    return fullPath;
}

function checkJsExists(url) {
    var fullPath = res.getFullPath(res.get("abs_js_dir") + url + res.get("js"));
    if (!fullPath) {
        console.info("javascript view not exists : " + url);
    }
    return fullPath;
}

function runScript(url, query, callback) {
    var script = require(res.get("rel_js_dir") + url);
    if (script) {
        script(url, query, function (params) {
            console.log(url + ":" + params);
            if (callback) {
                callback(params);
            }
        });
    } else {
        if (callback) {
            callback({});
        }
    }
}

function doReq(req, resp) {
    var url = req.params[0];
    var extName = path.extname(url);
    var fullPath = res.getFullPath(url);
    if (extName && fullPath) { // 文件存在
        resp.sendFile(fullPath);
        return;
    }

    if (extName) { // 有扩展名
        console.log("file not exists : " + url);
        resp.sendStatus(204);
        return;
    }

    url = url || ini.url.home_jade;

    fullPath = checkJadeExists(url);
    if (! fullPath) {
        resp.redirect(ini.url.home_url);
        return;
    }

    fullPath = checkJsExists(url);
    if (fullPath) {
        var query = req.query;
        runScript(url, query, function(params) {
            resp.render(url, { params: params });
        });
    } else {
        resp.render(url, { params: {}});
    }
}

module.exports.init = function () {
    // 视图引擎设置
    app.set('views', './views/jade');
    app.set('view engine', 'jade');
    app.engine('jade', require('jade').__express);
    app.get('/*', function (req, resp) {
        doReq(req, resp);
    });
    app.listen(port);
};