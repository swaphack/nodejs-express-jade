(function () {
    var db = require("mysql");
    var pool = null;
    var mysql = {
        // 初始化
        init : function(dbConfig) {
            pool = pool || (function (params) {
                var connect = db.createPool(params);
                if (!connect) {
                    console.log("connect database failure");
                }
                return connect;
            }(dbConfig));
        },
        // 查询
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
                        console.log(qerr.sql);
                        func(qerr, null, null);
                    } else {
                        func(qerr, values, fields);
                    }
                })
            });
        },
        // 拼接查询语句
        format : function (sql, params) {
            if (!sql) {
                return sql;
            }

            var args = arguments;
            var ary = [];
            for (var i = 1; i < args.length; i++) {
                ary.push(pool.escape(args[i]));
            }

            return sql.format(ary);
        },
        // 关闭
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

