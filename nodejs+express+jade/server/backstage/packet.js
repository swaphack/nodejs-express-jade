var Protocol = new require("../common/protocol").Protocol;

var Packet = function () {
    Protocol.call(this);

    this.musicFiles = null;
}

Packet.prototype = new Protocol();
Packet.prototype.constructor = Packet;

var mod = new Packet();
mod.setID("action");

module.exports = function (req, resp) {
    if (!req || !resp) {
        return false;
    }

    if (!mod.hand(req, resp)) {
        resp.send([]);
    }
};
//////////////////////////////////////////////////////////////////

