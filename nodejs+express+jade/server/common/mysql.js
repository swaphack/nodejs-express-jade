(function () {
    var cache = require("./cache");
    // 固定mysql缓存
    var FixCache = function (dbName) {
        this._sqlCache = cache.createCache();
        this._callbackCache = cache.createCache();
        this._memCache = cache.createCache();
        this._dbName = dbName;
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

        var mysqlPool = getDB(this._dbName);
        if (!mysqlPool) {
            return;
        }
        var oldSQL = this.getSQL(name);
        if (oldSQL !== undefined && oldSQL !== sql) {
            console.error("Exists Name In Cache.Name:{0}, LastSQL : {1}, NewSQL : {2}".format(name, oldSQL, sql));
            return;
        }
        this.bindCallback(name, callback);
        if (oldSQL !== undefined) {
            this.runCallback(name, this.getMemory(name));
            return;
        }
        this.bindSQL(name, sql);
        var memCache = this;
        mysqlPool.query(sql, function (err, values, fields) {
            if (err) {
                memCache.bindMemory(name, null);
            } else {
                var dataAry = [];
                for (var i = 0; i < values.length; i++) {
                    var data = {};
                    for (var j = 0; j < fields.length; j++) {
                        data[j] = values[i][fields[j].name];
                    }
                    dataAry.push(data);
                }
                memCache.bindMemory(name, dataAry);
            }

            memCache.runCallback(name, memCache.getMemory(name));
        });
    };

    ///////////////////////////////////////////////////////////////////////////
    var db = require("mysql");
    var Pool = function (dbConfig) {
        this._pool = null;

        this.init(dbConfig);
    };

    // 初始化
    Pool.prototype.init = function(dbConfig) {
        this._pool = this._pool || (function (params) {
            var connect = db.createPool(params);
            if (!connect) {
                console.error("connect database failure");
            }
            return connect;
        })(dbConfig);
    };

    // 查询
    Pool.prototype.query = function (sql, func) {
        if (!sql) {
            return;
        }
        if (!this._pool) {
            return;
        }
        this._pool.getConnection(function (err, connection) {
            if (err && func) {
                return func(err, null, null);
            }
            connection.query(sql, function (qerr, values, fields) {
                connection.release();
                if (qerr) {
                    console.error(qerr.message);
                    console.error(qerr.sql);
                    if (func) {
                        func(qerr, null, null);
                    }
                } else {
                    if (func) {
                        func(qerr, values, fields);
                    }
                }
            })
        });
    };

    // 关闭
    Pool.prototype.close = function () {
        if (!this._pool) {
            return;
        }
        this._pool.end();
        this._pool = null;
    };

    ///////////////////////////////////////////////////////////////////////////
    function createFixCache(dbName) {
        return new FixCache(dbName);
    }

    var dbPool = {};
    function init(dbConfigs) {
        for (var key in dbConfigs) {
            dbPool[key] = new Pool(dbConfigs[key]);
        }
    }

    function getDB(dbName) {
        return dbPool[dbName];
    }

    var mysql = {
        createFixCache : createFixCache,
        init : init,
        getDB : getDB,
    };

    // mysql 格式化查询语句
    String.prototype.formatSQL = function (arg) {
        var params = null;
        if (arguments.length === 1 && tool.isDictionary(arguments[0])) {
            params = arguments[0];
        } else {
            params = arguments;
        }

        var ary = {};
        for (var i = 0; i < params.length; i++) {
            ary[i] = db.escape(params[i]);
        }

        return this.format(ary);
    };

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = mysql;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return mysql });
    }
})();

