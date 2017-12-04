(function () {
    var musicAry = null; // 音乐信息
    var pageItemCount = 10; // 每页显示的数量
    var pageNum = 0; // 当前页数
    var totalPageCount = 0; // 总页数
    var indexOfMusic = 0; // 当前播放的音乐索引

    // 设置播放的音乐
    function playMusic(index) {
        if (index === null) {
            return;
        }
        if (index < 0 || index >= musicAry.length) {
            return;
        }
        var name = String.encode(musicAry[index]);
        if (!name) {
            return;
        }

        $("#left video").attr("src", http.getLogicURL("data/music?action=play&name="+ name));
        var lastChild = $("#nav ol").find("li[index=" + indexOfMusic +"]");
        if (lastChild) {
            lastChild.css("background-color","white");
        }

        var curChild = $("#nav ol").find("li[index=" + index +"]");
        if (curChild) {
            curChild.css("background-color","silver");
        }
        indexOfMusic = index;
    }

    // 设置音乐页面
    function refreshMusicPage(page) {
        if (page === null) {
            return;
        }

        if (page < 0 || page > totalPageCount) {
            return;
        }

        var nav = $("#nav ol");
        nav.empty();

        if (!musicAry) {
            $("#pageNum").text(0 + "/" + 0);
            return;
        }

        var totalCount = musicAry.length;
        for (var i = 0; i < pageItemCount; i++) {
            var index = page * pageItemCount + i;
            if (index < totalCount) {
                nav.append("<li index='" + index +"'>" + musicAry[index] + "</li>");
            }
        }

        $("#nav li").click(function () {
            var index = $(this).attr("index");
            playMusic(index);
        });

        $("#pageNum").text(page + 1 + "/" + totalPageCount);

        pageNum = page;
    }

    $(document).ready(function () {
        $("#pre").click(function () {
           pageNum = pageNum -1;
           if (pageNum < 0) {
               pageNum = 0;
               return;
           }
            refreshMusicPage(pageNum);
        });

        $("#next").click(function () {
            pageNum = pageNum + 1;
            if (pageNum >= totalPageCount) {
                pageNum = totalPageCount - 1;
                return;
            }
            refreshMusicPage(pageNum);
        });

        $("#left video").bind("ended", function () {
            console.log("play end");
            var nextIndex = parseInt(indexOfMusic) + 1;
            if (nextIndex >= musicAry.length) {
                nextIndex = 0;
            }
            var page = Math.floor(nextIndex / pageItemCount);

            console.log(page, nextIndex);

            refreshMusicPage(page);
            playMusic(nextIndex);

        });

        http.getLogic("data/music", { action :"menu"}, function (data) {
            if (!data) {
                return;
            }
            musicAry = data;
            totalPageCount = Math.ceil(musicAry.length / pageItemCount);
            refreshMusicPage(0);
            playMusic(0);
        });
    });
}());