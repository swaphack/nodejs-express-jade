(function (global) {
    var cache = require("./cache")
    var filePath = require("./filePath");
    var mysql = require("./mysql");
    var protocol = require("./protocol");
    var xml = require("./xml");
    var view = require("./view");
    var packet = require("./packet");

    var lg = {
        cache: cache,
        filePath: filePath,
        mysql: mysql,
        protocol: protocol,
        xml: xml,
        view : view,
        packet : packet
    };

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = lg;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return lg });
    }
}(this));