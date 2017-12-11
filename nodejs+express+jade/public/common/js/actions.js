// 动作
(function () {
    function Action() {
        this.running = true;

        ActionManager.

        this.update = function (dt) {

        };
    }

    Action.prototype.run = function () {

    };

    Action.prototype.pause = function () {
        this.running = false;
    };

    Action.prototype.resume = function () {
        this.running = true;
    };

    Action.prototype.stop = function () {
    };
    //////////////////////////////////////////////////////////
    function ActionManager() {
        this.actions = {};
    }

    ActionManager.addAction = function(action) {
    }

    //////////////////////////////////////////////////////////

    // 移动偏移位置
    var moveBy = function (offset, interval) {

    };

    var scaleTo = function (scale, interval) {

    };

    var scaleBy = function (offset, interval) {

    };

    var rotateTo = function (rotation, interval) {

    };

    var rotateBy = function (offset, interval) {

    };

    //////////////////////////////////////////////////////////
}());