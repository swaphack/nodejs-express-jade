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

module.exports.View = View;

module.exports.direct = direct;