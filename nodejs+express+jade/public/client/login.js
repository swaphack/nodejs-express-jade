(function () {
    $(document).ready(function () {
        $("#login_form").submit(function () {
            var username = $("#login_username").val();
            var password = $("#login_password").val();
            if (!username || !password) {
                alert("Username or Password not be null!");
                return;
            }

            http.postLogic("data/user", { action : "signIn", name : username, pwd : password }, function (data) {
                console.log(data);
            });
        });
    });
}());