// 定时器
(function (mod) {
    // 执行一次
    function scheduleOnce(func, delay) {
        if (!func) {
            return;
        }
        delay = delay || 0;
        window.setTimeout(func, delay);
    }

    // 重复执行
    function schedule(func, interval, delay) {
        if (!func) {
            return null;
        }

        if (tool.isNullOrUndefined(delay)) {
            delay = false;
        }

        interval = interval || 1000;
        if (!delay) {
            func();
        }
        return window.setInterval(func, interval);
    }

    // 移除定时器
    function unSchedule(scheduleId) {
        if (!scheduleId) {
            return;
        }
        window.clearInterval(scheduleId);
    }

    //////////////////////////////////////////////////////////////////////////
    var Scheduler = function () {
        this._schedulers = {};
    };

    // 添加定时器
    Scheduler.prototype.schedule = function (func, interval, delay, name) {
        if (tool.isNullOrUndefined(name)) {
            return;
        }
        var id = schedule(func, interval, delay);
        if (id === null) {
            return;
        }
        this._schedulers[name] = id;
    };

    // 移除定时器
    Scheduler.prototype.unSchedule = function(name) {
        if (tool.isNullOrUndefined(name)) {
            return;
        }

        if (!(name in this._schedulers)) {
            return;
        }

        unSchedule(this._schedulers[name]);
        delete this._schedulers[name];
    };

    function createScheduler() {
        return new Scheduler();
    }


    var scheduler = {
        scheduleOnce : scheduleOnce,
        schedule : schedule,
        unSchedule : unSchedule,
        createScheduler : createScheduler
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