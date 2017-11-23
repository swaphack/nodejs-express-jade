var mysql = require("mysql");

var pool = null;

// 查询
module.exports.query = function (sql, func) {
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
                func(qerr, null, null);
            } else {
                func(qerr, values, fields);
            }
        })
    });
}

// 关闭
module.exports.close = function () {
    if (!pool) {
        return;
    }

    pool.end();
}

// 初始化
module.exports.init = function (dbConfig) {
    pool = pool || (function (params) {
        var connect = mysql.createPool(params);
        if (!connect) {
            console.log("connect database failure");
        }
        return connect;
    }(dbConfig));
};