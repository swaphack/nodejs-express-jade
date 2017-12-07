(function () {
    var _musicAry = null; // 音乐信息
    var _pageItemCount = 10; // 每页显示的数量
    var _pageNum = 0; // 当前页数
    var _totalPageCount = 0; // 总页数
    var _indexOfMusic = 0; // 当前播放的音乐索引

    // 设置播放的音乐
    function playMusic(index) {
        if (index === null) {
            return;
        }
        if (index < 0 || index >= _musicAry.length) {
            return;
        }
        var name = String.encodeURL(_musicAry[index]);
        if (!name) {
            return;
        }

        $("#left video").attr("src", lg.http.getLogicURI("data/music?action=play&name="+ name));
        var lastChild = $("#nav ol").find("li[index=" + _indexOfMusic +"]");
        if (lastChild) {
            lastChild.css("background-color","white");
        }

        var curChild = $("#nav ol").find("li[index=" + index +"]");
        if (curChild) {
            curChild.css("background-color","silver");
        }
        _indexOfMusic = index;
    }

    // 设置音乐页面
    function refreshMusicPage(page) {
        if (page === null) {
            return;
        }

        if (page < 0 || page > _totalPageCount) {
            return;
        }

        var nav = $("#nav ol");
        nav.empty();

        if (!_musicAry) {
            $("#pageNum").text(0 + "/" + 0);
            return;
        }

        var totalCount = _musicAry.length;
        for (var i = 0; i < _pageItemCount; i++) {
            var index = page * _pageItemCount + i;
            if (index < totalCount) {
                nav.append("<li index='" + index +"'>" + _musicAry[index] + "</li>");
            }
        }

        $("#nav li").click(function () {
            var index = $(this).attr("index");
            playMusic(index);
        });

        var indexTxt = page + 1 + "/" + _totalPageCount;
        $("#pageNum").text(indexTxt);

        _pageNum = page;
    }

    $(document).ready(function () {
        $("#pre").click(function () {
           _pageNum = _pageNum -1;
           if (_pageNum < 0) {
               _pageNum = 0;
               return;
           }
            refreshMusicPage(_pageNum);
        });

        $("#next").click(function () {
            _pageNum = _pageNum + 1;
            if (_pageNum >= _totalPageCount) {
                _pageNum = _totalPageCount - 1;
                return;
            }
            refreshMusicPage(_pageNum);
        });

        $("#left video").bind("ended", function () {
            console.log("play end");
            var nextIndex = parseInt(_indexOfMusic) + 1;
            if (nextIndex >= _musicAry.length) {
                nextIndex = 0;
            }
            var page = Math.floor(nextIndex / _pageItemCount);

            console.log(page, nextIndex);

            refreshMusicPage(page);
            playMusic(nextIndex);

        });

        lg.http.getLogic("data/music", { action :"menu"}, function (error, data) {
            if (!data) {
                return;
            }
            _musicAry = data;
            _totalPageCount = Math.ceil(_musicAry.length / _pageItemCount);
            refreshMusicPage(0);
            playMusic(0);
        });
    });
})();