var Protocol = function () {
    this._protocls = {};
    this._id = "";
}

Protocol.prototype.setID = function (id) {
    this._id = id;
}

Protocol.prototype.getID = function () {
    return this._id;
}

Protocol.prototype.register = function (id, handler) {
    if (!id || !handler) {
        return;
    }
    this._protocls[id] = handler;
}

Protocol.prototype.unregister = function (id) {
    if (!id) {
        return;
    }

    delete this._protocls.key;
}

Protocol.prototype.hand = function (req, resp) {
    if (!req || !resp) {
        return false;
    }

    var query = req.query;
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
}

module.exports.Protocol = Protocol;