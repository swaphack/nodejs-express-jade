var mysql = require("mysql");

var pool = null;

// 初始化
function init(dbConfig) {
    pool = pool || (function (params) {
        var connect = mysql.createPool(params);
        if (!connect) {
            console.log("connect database failure");
        }
        return connect;
    }(dbConfig));
}
// 查询
function query(sql, func) {
    if (!sql || !func) {
        return;
    }
    if (!pool) {
        return;
    }
    pool.getConnection(function (err, connection) {
        if (err) {
            return func(err, null, null);
        }
        connection.query(sql, function (qerr, values, fields) {
            connection.release();
            if (qerr) {
                console.log(qerr.message);
                func(qerr, null, null);
            } else {
                func(qerr, values, fields);
            }
        })
    });
}
// 关闭
function close() {
    if (!pool) {
        return;
    }
    pool.end();
    pool = null;
}

module.exports.init = init;
module.exports.close = close;
module.exports.query = query;

