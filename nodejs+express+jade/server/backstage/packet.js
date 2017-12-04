var Protocol = new require("../common/protocol").Protocol;

var mod = new Protocol();
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
// 请求协议号
mod.register("packetIds", function (query, resp) {
    xmldb
});

