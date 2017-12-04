(function () {
    var Protocol = function () {
        this._protocls = {};
        this._id = "";
        this._req = null;
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
            return false;
        }

        this._req = req;



        var query = null;
        if (req.method == "GET") {
            query = req.query;
        } else if (req.method == "POST") {
            query = req.body;
        };
        if (!query) {
            return false;
        }

        var id = query[this.getID()];
        if (!id) {
            return false;
        }

        if (!(id in this._protocls)) {
            return false;
        }

        delete query[this.getID()];

        this._protocls[id](query, resp);
        return true;
    };

    var protocol = {
        Protocol : Protocol,
    };

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = protocol;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return protocol });
    }
}());