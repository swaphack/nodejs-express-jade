// 定时器
var scheduler = (function (mod) {
    // 执行一次
    mod.scheduleOnce = function (func, delay) {
        if (!func) {
            return;
        }
        delay = delay || 0;
        window.setTimeout(func, delay);
    };
    // 重复执行
    mod.schedule = function (func, interval, delay) {
        if (!func) {
            return;
        }
        interval = interval || 1000;
        if (!delay) {
            func();
        }
        window.setInterval(func, interval);
    };

    return mod;
}({}));