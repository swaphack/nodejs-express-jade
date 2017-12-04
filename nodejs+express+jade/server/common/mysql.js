(function () {
    var db = require("mysql");
    var pool = null;
    var mysql = {
        init : function(dbConfig) {
            pool = pool || (function (params) {
                var connect = db.createPool(params);
                if (!connect) {
                    console.log("connect database failure");
                }
                return connect;
            }(dbConfig));
        },
        query :  function (sql, func) {
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
        },
        close : function () {
            if (!pool) {
                return;
            }
            pool.end();
            pool = null;
        }
    };

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = mysql;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return mysql });
    }
}());

