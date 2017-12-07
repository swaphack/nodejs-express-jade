(function () {
    var packet = require("../../common/packet");

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = packet;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return packet });
    }
})();