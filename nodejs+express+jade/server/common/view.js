(function () {
    function View() {
        // 处理接收到的请求
        this.do = function(req, resp) {
            direct(req, resp);
        };
    }

    // 直接访问
    function direct(req, resp) {
        var url = req.params[0];
        resp.render(url);
    }

    var view = {
        direct : direct,
        View : View
    };

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = view;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return view });
    }
}(this));
