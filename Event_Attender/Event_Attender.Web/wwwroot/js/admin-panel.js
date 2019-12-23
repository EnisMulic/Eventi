$(document).ready(function () {

    $("div.ea-navbar").on('click', function (e) {
        $.get("../Home/" + e.target.id + "List", function (response) {
            $("#Main-Display").html(response);

            SetActionsInfo(e.target.id);
            SetActionsUredi(e.target.id);
            SetActionDodaj(e.target.id);
        });
    });

    $("div.ea-navbar-home").on('click', function (e) {
        $("#Main-Display").html("");
    });

    function SetActionsInfo(type) {
        $("button[for=\"Info\"]").on('click', function (e) {
            $.get("../Home/" + type + "Info?id=" + e.target.id, function (response) {
                $("#Main-Display").html(response);

                SetActionsUredi(type);
            });
        });
    }

    function SetActionsUredi(type) {
        $("button[for=\"Uredi\"]").on('click', function (e) {
            $.get("../Home/" + type + "Uredi?id=" + e.target.id, function (response) {
                $("#Main-Display").html(response);
            });
        });
    }

    function SetActionDodaj(type) {
        $("button[for=\"Dodaj\"]").on('click', function (e) {
            $.get("../Home/" + type + "Dodaj", function (response) {
                $("#Main-Display").html(response);
            });
        });
    }
})