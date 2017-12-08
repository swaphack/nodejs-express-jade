// 时间
(function (mod) {
    Date.prototype.format = function(strFormat){
        var o = {
            "M+" :  this.getMonth()+1,  //month
            "d+" :  this.getDate(),     //day
            "h+" :  this.getHours(),    //hour
            "m+" :  this.getMinutes(),  //minute
            "s+" :  this.getSeconds(), //second
            "q+" :  Math.floor((this.getMonth()+3)/3),  //quarter
            "S"  :  this.getMilliseconds() //millisecond
        }
        if(/(y+)/.test(strFormat)) {
            strFormat = strFormat.replace(RegExp.$1, (this.getFullYear()+"").substr(4 - RegExp.$1.length));
        }
        for(var k in o) {
            if(new RegExp("("+ k +")").test(strFormat)) {
                strFormat = strFormat.replace(RegExp.$1, RegExp.$1.length==1 ? o[k] : ("00"+ o[k]).substr((""+ o[k]).length));
            }
        }
        return strFormat;
    };

    function UTCToLocalTimeString(d, format) {
        var timeOffsetInHours = (new Date().getTimezoneOffset()/60)  + -10;
        d.setHours(d.getHours() + timeOffsetInHours);
        return d.format(format);
    }

    var dateTime = {
        UTCToLocalTimeString : UTCToLocalTimeString,
    };

    if (mod) {
        mod.lg = mod.lg || {};
        mod.lg.dateTime = dateTime;
    }
}(typeof self !== 'undefined' ? self
    : typeof window !== 'undefined' ? window
    : typeof global !== 'undefined' ? global
    : this
));