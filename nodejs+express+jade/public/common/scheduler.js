// 定时器
(function (mod) {
    if (mod === undefined) {
        return;
    }

    // 执行一次
    mod.property.scheduleOnce = function (func, delay) {
        if (!func) {
            return;
        }
        delay = delay || 0;
        window.setTimeout(func, delay);
    };

    // 重复执行
    mod.property.schedule = function (func, interval, delay) {
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
    mod.property.unschedule = function (scheduleId) {
        if (!scheduleId) {
            return;
        }
        window.clearInterval(scheduleId);
    };

    return mod;
}(HTMLElement));