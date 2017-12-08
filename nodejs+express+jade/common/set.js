(function (mod) {
    // 页结合
    var PageSet = function () {
        // 数据
        this._data = [];
        // 页码
        this._pageIndex = 0;
        // 总页数
        this._totalPageCount = 0;
        // 索引
        this._itemIndex = 0;
        // 每页最大数据项
        this._perPageItemCount = 10;

        // 重置数据
        this.resetData = function () {
            this._totalPageCount = Math.ceil(this._data.length / this._perPageItemCount);
            this._pageIndex = 0;
            this._itemIndex = 0;
        };

        // 重置索引
        this.resetIndex = function () {
            this._pageIndex = Math.floor(this._itemIndex / this._perPageItemCount);
        };
    };

    // 设置数据
    PageSet.prototype.setData = function(data) {
        this._data = data || [];
        this.resetData();
    };

    // 获取数据长度
    PageSet.prototype.getDataLength = function () {
        return this._data.length;
    };

    // 获取数据
    PageSet.prototype.getData = function () {
        return this._data;
    };

    // 每页显示的最大数据项
    PageSet.prototype.setPerPageItemCount = function (perPageItemCount) {
        if (!perPageItemCount || perPageItemCount <= 0) {
            return;
        }
        this._perPageItemCount = perPageItemCount;
        this.resetData();
    };

    // 每页显示的最大数据项
    PageSet.prototype.getPerPageItemCount = function () {
        return this._perPageItemCount;
    };

    // 设置当前页面索引
    PageSet.prototype.setPageIndex = function (index) {
        if (index < 0 || index >= this._totalPageCount) {
            return null;
        }

        this._pageIndex = index;
    };

    // 获取页面上的数据
    PageSet.prototype.getPageData = function (page) {
        if (page < 0 || page >= this._totalPageCount) {
            return null;
        }

        var ary = [];
        for (var i = 0; i < this._perPageItemCount; i++) {
            var index = page * this._perPageItemCount + i;
            if (index < this._data.length) {
                ary.push(this._data[index]);
            } else {
                break;
            }
        }

        return ary;
    };

    // 当前页码
    PageSet.prototype.getPageIndex = function () {
        return this._pageIndex;
    };

    // 总页码
    PageSet.prototype.getTotalPageCount = function () {
        return this._totalPageCount;
    };

    // 获取页码数据
    PageSet.prototype.getItemData = function (index) {
        if (index < 0 || index >= this._data.length) {
            return null;
        }

        return this._data[index];
    };

    // 当前数据索引
    PageSet.prototype.getItemIndex = function () {
        return this._itemIndex;
    };

    // 设置当前数据索引
    PageSet.prototype.setItemIndex = function (index) {
        if (index < 0 || index >= this._data.length) {
            return;
        }

        this._itemIndex = index;
        this.resetIndex();
    };

    var set = {
        PageSet : PageSet,
    }


    ////////////////////////////////////////////////////////////////////
    // export

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = set;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function(){ return set });
    } else if (mod) {
        mod.set = set;
    }
})(typeof self !== 'undefined' ? self
    : typeof window !== 'undefined' ? window
    : typeof global !== 'undefined' ? global
    : this
);