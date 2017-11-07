var mysql = require("mysql");
var ini = require("../ini");

var pool = null;

module.exports.query = function (sql, func) {
    if (!sql || !func) {
        return;
    }
    pool.getConnection(function (err, connection) {
        if (err) {
            return func(err, null, null);
        }
        connection.query(sql, function (qerr, values, fields) {
            connection.release();
            if (qerr) {
                func(qerr, values, fields);
            } else {
                func(qerr, null, null);
            }
        })
    });
}

module.exports.init = function () {
    if (pool) {
        pool.end();
        pool = null;
    }
    pool = mysql.createPool(ini.database);
    if (!pool) {
        console.log("connect database failure");
        return;
    }
};