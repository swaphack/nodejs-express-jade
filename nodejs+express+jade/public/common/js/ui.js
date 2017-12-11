// 界面
(function (mod) {
    // 列表
    var ListView = function () {
        this._items = [];
        this._itemModel = "";
        this._parent = null;
        this._beginMark = "";
        this._endMark = "";
    };

    ListView.prototype.setMark = function (mark) {
        if (!mark) {
            return;
        }
        this._beginMark = "<{0}>".format(mark);
        this._endMark = "</{0}>".format(mark);
    };

    ListView.prototype.setParent = function (parent) {
        this._parent = parent;
    };

    // 设置模板
    ListView.prototype.setItemModel = function (itemHTML) {
        this._itemModel = itemHTML;
    };

    // 添加项
    ListView.prototype.appendItem = function (args) {
        if (! this._itemModel) {
            return;
        }

        var params = null;
        if (arguments.length === 1 && tool.isDictionary(arguments[0])) {
            params = arguments[0];
        } else {
            params = arguments;
        }
        var item = this._itemModel.format(params);
        this._items.push(item);
    };

    // 刷新
    ListView.prototype.flush = function () {
        if (!this._parent) {
            return;
        }
        this._parent.empty();
        var lst = this._beginMark;
        lst += this._items.join("");
        lst += this._endMark;
        this._parent.append(lst);
    };

    // 数据填充
    ListView.prototype.flushData = function (itemDataAry) {
        this.removeAllItems();
        if (!itemDataAry || !Array.isArray(itemDataAry)) {
            return;
        }

        for (var i = 0; i < itemDataAry.length; i++) {
            this.appendItem(itemDataAry[i]);
        }

        this.flush();
    };

    ListView.prototype.removeAllItems = function () {
        if (!this._parent) {
            return;
        }

        this._parent.empty();
        this._items = [];
    };

    //////////////////////////////////////////////////////////////
    var TableView = function () {
        ListView.call(this);

        this.setMark("table");

        this._header = "";
    };

    TableView.prototype = new ListView();
    TableView.prototype.constructor = TableView;

    TableView.prototype.setHeader = function (header) {
        this._header = header;
    };

    // 刷新
    TableView.prototype.flush = function () {
        if (!this._parent) {
            return;
        }
        this._parent.empty();
        var lst = this._beginMark;
        lst += this._header;
        lst += this._items.join("");
        lst += this._endMark;
        this._parent.append(lst);
    };

    //////////////////////////////////////////////////////////////
    var ui = {
        ListView : ListView,
        TableView : TableView
    };

    if (mod) {
        mod.lg = mod.lg || {};
        mod.lg.ui = ui;
    }
}(typeof self !== 'undefined' ? self
    : typeof window !== 'undefined' ? window
    : typeof global !== 'undefined' ? global
    : this
));