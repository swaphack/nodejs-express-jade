// 定时器
(function (mod) {
    // 执行一次
    function scheduleOnce(func, delay) {
        if (!func) {
            return;
        }
        delay = delay || 0;
        window.setTimeout(func, delay);
    };

    // 重复执行
    function schedule(func, interval, delay) {
        if (!func) {
            return null;
        }
        interval = interval || 1000;
        if (!delay) {
            func();
            return null;
        }
        return window.setInterval(func, interval);
    };

    // 移除定时器
    function unSchedule(scheduleId) {
        if (!scheduleId) {
            return;
        }
        window.clearInterval(scheduleId);
    };

    var scheduler = {
        scheduleOnce : scheduleOnce,
        schedule : schedule,
        unSchedule : unSchedule,
    };

    if (mod) {
        mod.lg = mod.lg || {};
        mod.lg.scheduler = scheduler;
    }
})(typeof self !== 'undefined' ? self
    : typeof window !== 'undefined' ? window
    : typeof global !== 'undefined' ? global
    : this
);