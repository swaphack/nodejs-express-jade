(function () {
    $(document).ready(function () {
        $("#login_form").submit(function () {
            var username = $("#login_username").val();
            var password = $("#login_password").val();
            if (!username || !password) {
                alert("Username or Password not be null!");
                return;
            }
            var p = packet.createPacket();
            p.setValue("action", "signIn");
            p.setValue("name", username);
            p.setValue("pwd", password);

            lg.http.postLogic("data/user", p, function (error, data) {
                alert(error);
                alert(data);
            });
        });
    });
})();