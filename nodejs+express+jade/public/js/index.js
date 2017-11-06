(function () {
    $(document).ready(function () {
        $("#nav li a").click(function () {
            if ($("#right #content video").attr("src") == $(this).attr("src")) {
                return;
            }
            $("#right #content video").attr("src", $(this).attr("src"));
        });
    });
}());