(function () {
    var _pageNum = null;
    var _video = null;
    var _listRoot = null;

    var _musicData = new set.PageSet();
    _musicData.setPerPageItemCount(10);

    var _listView = new lg.ui.ListView();
    _listView.setMark("ol");
    _listView.setItemModel("<li index={0}>{1}</li>");

    // 设置播放的音乐
    function playMusic(index) {
        var name = _musicData.getItemData(index);
        if (name) {
            name = tool.encodeURI(name);
            _video.attr("src", lg.http.getLogicURI("sample/music?action=play&name="+ name));
        }
        var str = "li[index={0}]";
        var lastChild = _listRoot.find(str.format(_musicData.getItemIndex()));
        if (lastChild) {
            lastChild.css("background-color","white");
        }
        var curChild = _listRoot.find(str.format(index));
        if (curChild) {
            curChild.css("background-color", "silver");
        }
        _musicData.setItemIndex(index);
    }

    // 设置音乐页面
    function refreshMusicPage(page) {
        if (page === null) {
            return;
        }

        var dataAry = _musicData.getPageData(page);
        _listView.removeAllItems();
        if (dataAry) {
            for (var i = 0; i < dataAry.length; i++) {
                var index = page * _musicData.getPerPageItemCount() + i;
                _listView.appendItem(index, dataAry[i]);
            }
        }
        _listView.flush();

        $("#nav li").click(function () {
            var index = $(this).attr("index");
            playMusic(parseInt(index));
        });

        _musicData.setPageIndex(page);

        if (_musicData.getTotalPageCount() === 0) {
            _pageNum.text("0/0");
        } else {
            var str = "{0}/{1}";
            str = str.format(_musicData.getPageIndex()+ 1, _musicData.getTotalPageCount());
            _pageNum.text(str);
        }
    }
    // 请求前一页
    function gotoPrePage() {
        var page = _musicData.getPageIndex();
        page = page -1;
        if (page < 0) {
            return;
        }
        refreshMusicPage(page);
    }

    // 请求后一页
    function gotoNextPage() {
        var page = _musicData.getPageIndex();
        page = page + 1;
        if (page >= _musicData.getTotalPageCount()) {
            return;
        }
        refreshMusicPage(page);
    }
    // 播放结束
    function onEndPlayVideo() {
        var nextIndex = _musicData.getItemIndex();
        nextIndex += 1;
        if (nextIndex >= _musicData.getDataLength()) {
            nextIndex = 0;
        }

        _musicData.setItemIndex(nextIndex);
        var page = _musicData.getPageIndex();

        console.log(page, nextIndex);

        refreshMusicPage(page);
        playMusic(nextIndex);
    }
    // 请求音乐数据
    function requestMusicData() {
        var p = packet.createPacket();
        p.setValue("action", "menu");

        lg.http.getLogic("sample/music", p, function (error, data) {
            if (error) {
                return;
            }
            _musicData.setData(data["files"]);
            refreshMusicPage(0);
            playMusic(0);
        });
    }

    $(document).ready(function () {
        _listRoot = $("#nav");
        _pageNum = $("#pageNum");
        _video = $("#left video");

        _listView.setParent(_listRoot);

        $("#pre").click(function () {
            gotoPrePage();
        });

        $("#next").click(function () {
            gotoNextPage();
        });

        $("#left video").bind("ended", function () {
            onEndPlayVideo();
        });

        requestMusicData();
    });
})();