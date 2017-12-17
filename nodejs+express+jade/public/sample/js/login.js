(function () {
    function onSignUp(name, pwd, pwd2) { // 注册
        if (!name) {
            alert("username is null");
            return;
        }
        if (!pwd) {
            alert("password is null");
            return;
        }
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

    function onSignIn(name, pwd) { // 登录
        if (!name) {
            alert("username is null");
            return;
        }
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
            var name = lg.layout.getValue("#username");
            var pwd = lg.layout.getValue("#password");
            onSignIn(name, pwd);
        });

        lg.layout.click("#signUp", function () {
            var name = lg.layout.getValue("#username");
            var pwd = lg.layout.getValue("#password");
            var pwd2 = lg.layout.getValue("#password");
            onSignUp(name,pwd,pwd2);
        });
    });
})();