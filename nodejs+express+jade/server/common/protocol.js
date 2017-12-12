(function () {
    var gpacket = require("./packet");
    // 协议
    var Protocol = function () {
        this._protocls = {};
        this._id = "";
    };

    Protocol.prototype.setID = function (id) {
        this._id = id;
    };

    Protocol.prototype.getID = function () {
        return this._id;
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
            this._protocls[id](packet, resp);
        } catch (e) {
            console.log(e.message);
            resp.sendStatus(400);
        }
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
})();