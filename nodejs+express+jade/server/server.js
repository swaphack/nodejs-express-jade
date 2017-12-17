// 加载通用资源
require("../common/base64");
require("../common/packet");
require("../common/tool");

var express = require('express');
var session = require('express-session');
var bodyParser = require('body-parser');

var path = require("path");
var lg = require("./common/index");

// 发送包
express.response.sendPacket = function (packet) {
    if (packet === null || packet === undefined) {
        return;
    }

    if (! (packet instanceof lg.packet.Packet)) {
        return;
    }

    this.send(packet.data());
};

// 请求网址
function baseUrl(url) {
    var spot = url.indexOf("?");
    if (spot !== -1) {
        url = url.substr(0, spot);
    }
    
    return url;
}

// 资源处理
function doRes(req, resp) {
    var reqUrl = req.params[0];
    var fullPath = lg.filePath.getFullPath(reqUrl);
    if (!fullPath) {
        fullPath = lg.filePath.getFullPath(reqUrl.substr(reqUrl.indexOf("/") + 1));
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
        lg.view.direct(req, resp);
    } catch (err) {
        resp.sendStatus(404);
    }
}

// 数据处理
function doData(req, resp) {
    var url = baseUrl(req.params[0]);
    try {
        var script = require("./" +  url);
        if (script) {
            script(req, resp);
        }
    } catch (err) {
        console.log("err : " + err);
        try {
            lg.view.direct(req, resp);
        } catch (perr) {
            console.log("perr : " + perr);
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
        secret: tool.randomWord(20),
        cookie: {maxAge: 60 * 1000 * 30},
        resave: false,
        saveUninitialized: true
    }));

    app.use(bodyParser.urlencoded({ extended: true }));

    // 跨域访问
    app.get("*", function (req, resp, next) {
        console.log("get : " + req.url);
        resp.header("Access-Control-Allow-Origin", "*");
        resp.header("Access-Control-Allow-Headers", "X-Requested-With");
        resp.header("Access-Control-Allow-Methods","PUT,POST,GET,DELETE,OPTIONS");
        resp.header("X-Powered-By",' 3.2.1');
        //resp.header("Content-Type", "application/json;charset=utf-8");
        next();
    });

    app.post("*", function (req, resp, next) {
        console.log("post : " + req.url);
        resp.header("Access-Control-Allow-Origin", "*");
        resp.header("Access-Control-Allow-Headers", "X-Requested-With");
        resp.header("Access-Control-Allow-Methods","PUT,POST,GET,DELETE,OPTIONS");
        resp.header("X-Powered-By",' 3.2.1');
        //resp.header("Content-Type", "application/json;charset=utf-8");
        next();
    });

    app.get("/", function (req, resp) {
        var url = baseUrl(req.url);
        if (path.extname(url)) {
            doRes(req, resp);
        } else {
            url = "welcome";
            resp.render(url);
        }
    });

    // 公共文件
    app.get("/files/*", function (req, resp) {
        doRes(req, resp);
    });

    // 公共代码
    app.get("/common/*", function (req, resp) {
        doRes(req, resp);
    });

    // 公共界面用到的资源
    app.get("/public/*", function (req, resp) {
        doRes(req, resp);
    });

    /////////////////////////////////////////////////////////////////////
    // 视图
    app.get("/views/*", function (req, resp) {
        var url = baseUrl(req.url);
        if (path.extname(url)) {
            doRes(req, resp);
        } else {
            doView(req, resp);
        }
    });

    // 逻辑数据
    app.get("/logic/*", function (req, resp) {
        doData(req, resp);
    });

    app.post("/logic/*", function (req, resp) {
        doData(req, resp);
    });

    app.listen(server.port);
};