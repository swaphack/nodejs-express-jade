(function () {
    var gpacket = require("./packet");
    // 协议
    var Protocol = function () {
        this._protocls = {};
        this._id = "";
        this._req = null;
        this._resp = null;
    };

    Protocol.prototype.setID = function (id) {
        this._id = id;
    };

    Protocol.prototype.getID = function () {
        return this._id;
    };

    Protocol.prototype.getRequest = function () {
        return this._req;
    };

    Protocol.prototype.register = function (id, handler) {
        if (!id || !handler) {
            return;
        }
        this._protocls[id] = handler;
    };

    Protocol.prototype.unregister = function (id) {
        if (!id) {
            return;
        }

        delete this._protocls.key;
    };

    Protocol.prototype.hand = function (req, resp) {
        if (!req || !resp) {
            resp.sendStatus(500);
            return;
        }
        this._req = req;
        this._resp = resp;

        var query = null;
        if (req.method === "GET") {
            query = req.query;
        } else if (req.method === "POST") {
            query = req.body;
        }

        if (!query) {
            resp.sendStatus(400);
            return;
        }

        var id = query[this.getID()];
        if (!id) {
            resp.sendStatus(400);
            return;
        }

        if (!(id in this._protocls)) {
            resp.sendStatus(400);
            return;
        }

        var packet = gpacket.createPacket();
        for (var key in query){
            if (key !== this.getID()) {
                packet.setValue(key, query[key]);
            }
        }

        packet.setGetValueError(function (key) {
            throw new Error("Not Get Param : " + key + "In Packet : " + id);
        });

        try {
            this._protocls[id](packet, this);
        } catch (e) {
            resp.sendStatus(400);
        }
    };

    // 发送包
    Protocol.prototype.sendPacket = function (packet) {
        if (!this._resp) {
            return;
        }
        if (packet === null || packet === undefined) {
            return;
        }

        if (! (packet instanceof gpacket.Packet)) {
            return;
        }

        this._resp.send(packet.data());
    };

    // 发送状态
    Protocol.prototype.sendStatus = function (number) {
        if (!this._resp) {
            return;
        }
        if (number === null || number === undefined) {
            return;
        }

        this._resp.sendStatus(number);
    };

    // 发送数据
    Protocol.prototype.sendData = function (data) {
        if (!this._resp) {
            return;
        }
        if (data === null || data === undefined) {
            return;
        }

        this._resp.send(data);
    };

    // 发送文件
    Protocol.prototype.sendFile = function (fileName) {
        if (!this._resp) {
            return;
        }
        if (fileName === null || fileName === undefined) {
            return;
        }

        this._resp.sendFile(fileName);
    };

    //////////////////////////////////////////////////////////////////////

    function createProtocol() {
        return new Protocol();
    }

    var protocol = {
        createProtocol : createProtocol,
    };

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = protocol;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return protocol });
    }
}());