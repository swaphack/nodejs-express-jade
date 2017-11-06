var express = require('express');
var path = require("path");
var app = express();
var ini = require("./ini");
var res = require("./server/resource");
////////////////////////////////////////////////////////
function getParams(query) {
    var params = {};
    for (var k in query) {
        params[k] = query[k];
    }

    return params;
}

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
// 视图引擎设置
app.set('views', './views/jade');
app.set('view engine', 'jade');
app.engine('jade', require('jade').__express);

app.get('/*', function(req, resp) {
    var url = req.params[0];
    var query = getParams(req.query);

    //console.log(url + ":" + query);

    url = url || res.get("home");

    var fullPath = res.getFullPath(url);
    if  (fullPath) { // 文件存在
        resp.sendFile(fullPath);
    } else {
        var extName = path.extname(url);
        if (extName) { // 有扩展名
            console.log("file not exists : " + url);
        } else {
            fullPath = res.getFullPath(res.get("view_dir") + url + res.get("jade"));
            if (!fullPath) {
                console.log("jade view not exists : " + url);
                url = res.get("home");
            }
            fullPath = res.getFullPath(res.get("js_dir") + url + res.get("js"));
            if (fullPath) {
                var script = require(res.get("js_dir") + url);
                if (script) {
                    script(url, query, function (params) {
                        console.log(url + ":" +  params);
                        resp.render(url, {params : params});
                    });
                    return;
                }
            }
            resp.render(url, {params:{}});
        }
    }
});

app.listen(port);