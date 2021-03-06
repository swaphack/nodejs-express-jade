(function (mod) {
    // 包
    var Packet = function () {
        // 数据
        this._data = {};
        // 获取数据失败时的异常处理
        this._onGetValueError = null;
    };

    Packet.prototype.setGetValueError = function (func) {
        this._onGetValueError = func;
    };

    // 设置参数
    Packet.prototype.setValue = function (key, value) {
        if (key === null || key === undefined) {
            return;
        }

        this._data[key] = value;
    };

    // 获取参数值
    Packet.prototype.getValue = function (key) {
        if (key === null || key === undefined) {
            return null;
        }

        if (this._onGetValueError != null) {
            if (! (key in this._data)) {
                this._onGetValueError(key);
                return null;
            }
        }

        return this._data[key];
    };

    // 设置错误时提示文本
    Packet.prototype.setError = function (value) {
        this.setValue("error", value);
    };

    // 获取错误时提示文本
    Packet.prototype.getError = function () {
        return this.getValue("error");
    };

    // 填充数据
    Packet.prototype.flushData = function (data) {
        if (!tool.isDictionary(data)) {
            return;
        }

        for (var key in data) {
            this.setValue(key, data[key]);
        }
    };

    // 数据
    Packet.prototype.data = function () {
        return this._data;
    };

    ////////////////////////////////////////////////////////////////////

    function createPacket(data) {
        var p = new Packet();
        if (data !== null && data !== undefined){
            p.flushData(data);
        }
        return p;
    }

    function createErrorPacket(msg) {
        var p = new Packet();
        if (msg !== null && msg !== undefined){
            p.setError(msg);
        }
        return p;
    }

    var packet = {
        Packet : Packet,
        createPacket : createPacket ,
        createErrorPacket : createErrorPacket
    };

    ////////////////////////////////////////////////////////////////////
    // export

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = packet;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return packet });
    }
    if (mod) {
        mod.packet = packet;
    }
})(typeof self !== 'undefined' ? self
    : typeof window !== 'undefined' ? window
    : typeof global !== 'undefined' ? global
    : this
);