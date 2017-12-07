(function () {
    $(document).ready(function () {
        $("#login_username").val(lg.user.get("name"));
        $("#login_password").val(lg.user.get("pwd"));

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

            lg.user.set("name", name);
            lg.user.set("pwd", pwd);

            lg.http.postLogic("data/user", p, function (error, data) {
                if (error) {
                    alert(error);
                } else {
                    lg.user.set("id", data);
                }
            });

            return false;
        });
    });
})();