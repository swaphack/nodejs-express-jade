var express = require('express');
var app = express();
var ini = require("./ini");
var res = require("./server/resource");

// 端口
var port = ini.port;
if (!port) {
    port = 8080;
}

// 搜索路径设置
var root = res.root();
var searchPath = [];
searchPath.push(root + "/public/");
searchPath.push(root + "/files/");
res.setSearchPath(searchPath);

// 视图引擎设置
app.set('views', './views');
app.set('view engine', 'jade');
app.engine('jade', require('jade').__express);

var string = require("./common/string");

app.get('/', function(req, resp) {
    string.printObject(req);
    string.printObject(resp);
    resp.render('index', {ip: req.ip, port:req.port});
});

app.listen(port);