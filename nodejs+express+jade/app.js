var path = require("path");
var ini = require("./ini");
var res = require("./server/resource");

////////////////////////////////////////////////////////
// 端口
var port = ini.port;
if (!port) {
    port = 8080;
}

// 搜索路径设置
var root = res.root();
var searchPath = [];
searchPath.push(root + "/");
res.setSearchPath(searchPath);

res.set("js_dir", "./server/views/");
res.set("view_dir", "./views/jade/");
res.set("js", ".js");
res.set("jade", ".jade");
res.set("home", "index");
////////////////////////////////////////////////////////////
var express = require('express');
var app = express();

// 视图引擎设置
app.set('views', './views/jade');
app.set('view engine', 'jade');
app.engine('jade', require('jade').__express);

////////////////////////////////////////////////////////////
function checkJadeExists(url) {
    var fullPath = res.getFullPath(res.get("view_dir") + url + res.get("jade"));
    if (!fullPath) {
        console.info("jade view not exists : " + url);
    }
    return fullPath;
}

function checkJsExists(url) {
    var fullPath = res.getFullPath(res.get("js_dir") + url + res.get("js"));
    if (!fullPath) {
        console.info("javascript view not exists : " + url);
    }
    return fullPath;
}

function runScript(url, query, callback) {
    var script = require(res.get("js_dir") + url);
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
    var fullPath = res.getFullPath(url);
    if (fullPath) { // 文件存在
        resp.sendFile(fullPath);
        return;
    }

    var extName = path.extname(url);
    if (extName) { // 有扩展名
        console.log("file not exists : " + url);
        resp.sendStatus(204);
        return;
    }

    url = url || res.get("home");

    fullPath = checkJadeExists(url);
    if (! fullPath) {
        resp.redirect(res.get("home"));
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

app.get('/*', function (req, resp) {
    doReq(req, resp);
});
app.listen(port);

////////////////////////////////////////////////////////////
var mysql = require("mysql");