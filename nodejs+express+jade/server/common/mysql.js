(function () {
    var cache = require("./cache");
    // 固定mysql缓存
    var FixCache = function () {
        this._sqlCache = cache.createCache();
        this._callbackCache = cache.createCache();
        this._memCache = cache.createCache();
    };

    // 添加sql绑定
    FixCache.prototype.bindSQL = function (name, sql) {
        this._sqlCache.set(name, sql);
    };

    // 获取sql
    FixCache.prototype.getSQL = function (name) {
        return this._sqlCache.get(name);
    };

    // 添加内存绑定
    FixCache.prototype.bindMemory = function (name, memory) {
        this._memCache.set(name, memory);
    };

    // 获取内存
    FixCache.prototype.getMemory = function (name) {
        return this._memCache.get(name);
    };

    // 添加sql执行回调
    FixCache.prototype.bindCallback = function (name, callback) {
        if (!this._callbackCache.get(name)) {
            this._callbackCache.set(name, []);
        }

        var handlers = this._callbackCache.get(name);
        handlers.push(callback);
    };

    // 执行回调
    FixCache.prototype.runCallback = function (name, data) {
        if (!this._callbackCache.get(name)) {
            return;
        }
        var handlers = this._callbackCache.get(name);
        for (var i = 0; i < handlers.length; i++) {
            handlers[i](data);
        }

        this._callbackCache.set(name, null);
    };

    // 查询数据
    FixCache.prototype.query = function (name, sql, callback) {
        if (!name || !sql) {
            return;
        }
        var oldSQL = this.getSQL(name);
        if (oldSQL !== undefined && oldSQL !== sql) {
            console.log("Exists Name In Cache.Name:{0}, LastSQL : {1}, NewSQL : {2}".format(name, oldSQL, sql));
            return;
        }
        this.bindCallback(name, callback);
        if (oldSQL !== undefined) {
            this.runCallback(name, this.getMemory(name));
            return;
        }
        this.bindSQL(name, sql);
        var memCache = this;
        mysql.query(sql, function (err, values, fields) {
            if (err) {
                memCache.bindMemory(name, null);
            } else {
                var dataAry = [];
                for (var i = 0; i < values.length; i++) {
                    var data = [];
                    for (var j = 0; j < fields.length; j++) {
                        data.push(values[i][fields[j].name]);
                    }
                    dataAry.push(data);
                }
                memCache.bindMemory(name, dataAry);
            }

            memCache.runCallback(name, memCache.getMemory(name));
        });
    };

    ///////////////////////////////////////////////////////////////////////////
    function createFixCache() {
        return new FixCache();
    }


    var db = require("mysql");
    var pool = null;
    var mysql = {
        createFixCache : createFixCache,

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
})();

