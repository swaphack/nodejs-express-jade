(function () {
    $(document).ready(function () {
        $("#login_username").val(lg.userData.get("name", ""));
        $("#login_password").val(lg.userData.get("pwd", ""));

        $("#login_form").submit(function () {
            var name = $("#login_username").val();
            var pwd = $("#login_password").val();
            if (!name || !pwd) {
                alert("Username or Password not be null!");
                return false;
            }

            var p = packet.createPacket();
            p.setValue("action", "signIn");
            p.setValue("name", name);
            p.setValue("pwd", pwd);

            lg.userData.set("name", name);
            lg.userData.set("pwd", pwd);

            lg.http.postLogic("sample/user", p, function (error, data) {
                if (error) {
                    alert(error);
                } else {
                    lg.userData.flush(data);
                    lg.http.redirect("sample/game");
                }
            });

            return false;
        });
    });
})();