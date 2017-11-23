(function () {
    var musicAry = null;
    var pageItemCount = 10;
    var pageNum = 0;
    var totalPageCount = 0;

    function setMusicPage(page) {
        if (!musicAry) {
            return;
        }
        pageNum = page;

        var totalCount = musicAry.length;

        var nav = $("#nav ol");
        nav.empty();
        for (var i = 0; i < pageItemCount; i++) {
            var index = page * pageItemCount + i;
            if (index < totalCount) {
                nav.append("<li index='" + index +"'>" + musicAry[index] + "</li>");
            }
        }

        $("#nav li").click(function () {
            var name = musicAry[$(this).attr("index")];
			console.log(name);
            name = Base64.encode(name);
			console.log(name);
			name = encodeURIComponent(name);
			console.log(name);
            $("#left video").attr("src", http.getDataURL("data/music?action=play&name="+ name));
        });

        $("#pageNum").text(pageNum + 1 + "/" + totalPageCount);
    }

    $(document).ready(function () {
        $("#pre").click(function () {
           pageNum = pageNum -1;
           if (pageNum < 0) {
               pageNum = 0;
               return;
           }
           setMusicPage(pageNum);
        });

        $("#next").click(function () {
            pageNum = pageNum + 1;
            if (pageNum >= totalPageCount) {
                pageNum = totalPageCount - 1;
                return;
            }
            setMusicPage(pageNum);
        });

        http.get("data/music", { action :"menu"}, function (data) {
            if (!data) {
                return;
            }
            musicAry = data;
            totalPageCount = Math.ceil(musicAry.length / pageItemCount);
            setMusicPage(0);
        });
    });
}());