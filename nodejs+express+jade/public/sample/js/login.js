(function () {
    function onSignUp() { // 注册
        var name = lg.layout.getValue("#username");
        if (!name) {
            alert("username is null");
            return;
        }
        var pwd = lg.layout.getValue("#password");
        if (!pwd) {
            alert("password is null");
            return;
        }

        var pwd2 = lg.layout.getValue("#confirm_password");
        if (!pwd) {
            alert("confirm password is null");
            return;
        }

        if (pwd !== pwd2) {
            alert("confirm password is not equal with password");
            return;
        }

        lg.userData.set("name", name);
        lg.userData.set("pwd", pwd);

        var p = packet.createPacket();
        p.setValue("action", "signUp");
        p.setValue("name", name);
        p.setValue("pwd", pwd);

        lg.http.postLogic("sample/user", p, function (error, data) {
            if (error) {
                alert(error);
            } else {
                lg.userData.flush(data);
                lg.http.redirect("sample/sign_in");
            }
        });
    }

    function onSignIn() { // 登录
        var name = lg.layout.getValue("#username");
        if (!name) {
            alert("username is null");
            return;
        }
        var pwd = lg.layout.getValue("#password");
        if (!pwd) {
            alert("password is null");
            return;
        }

        lg.userData.set("name", name);
        lg.userData.set("pwd", pwd);

        var p = packet.createPacket();
        p.setValue("action", "signIn");
        p.setValue("name", name);
        p.setValue("pwd", pwd);

        lg.http.postLogic("sample/user", p, function (error, data) {
            if (error) {
                alert(error);
            } else {
                lg.userData.flush(data);
                lg.http.redirect("sample/game");
            }
        });
    }

    $(document).ready(function () {
        lg.layout.setValue("#username", lg.userData.get("name", ""));
        lg.layout.setValue("#password", lg.userData.get("pwd", ""));
        lg.layout.setValue("#confirm_password", lg.userData.get("pwd", ""));

        lg.layout.click("#signIn", function () {
            onSignIn();
        });

        lg.layout.click("#signUp", function () {
            onSignUp();
        });
    });
})();