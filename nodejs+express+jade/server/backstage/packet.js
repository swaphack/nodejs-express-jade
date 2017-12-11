var lg = require("../common/index");

var mod = new lg.protocol.createProtocol();
mod.setID("action");

module.exports = function (req, resp) {
    if (!req || !resp) {
        return;
    }
    mod.hand(req, resp);
};
//////////////////////////////////////////////////////////////////
// 请求协议号
mod.register("packetIds", function (packet, resp) {

});

