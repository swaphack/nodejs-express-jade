(function () {
    $(document).ready(function () {
        $("#nav li a").click(function () {
            if ($("video#media").attr("src") == $(this).attr("src")) {
                return;
            }
            $("video#media").attr("src", $(this).attr("src"));
        });

        $("video#media").volume = 0.2;

        layout.setMaxHeight($("#body"),  $("#left"), $("#body"));
    });
}());