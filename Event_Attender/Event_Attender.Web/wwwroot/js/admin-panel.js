$(document).ready(function () {
    $.get("/Administrator/Administrator/Chat", function (response) {
        $("#Main-Display").html(response);
    });

    $("div.ea-navbar").on('click', function (e) {
        $.get("../Home/" + e.target.id + "List", function (response) {
            $("#Main-Display").html(response);

            SetActionsInfo(e.target.id);
            SetActionsUredi(e.target.id);
            SetActionDodaj(e.target.id);
            SetActionUkloni(e.target.id, response);
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
                $.validator.unobtrusive.parse(".ea-form");
            });
        });
    }

    function SetActionDodaj(type) {
        $("button[for=\"Dodaj\"]").on('click', function (e) {
            $.get("../Home/" + type + "Dodaj", function (response) {
                $("#Main-Display").html(response);
                $.validator.unobtrusive.parse(".ea-form");
            });
        });
    }

    function SetActionUkloni(type) {
        $("button[for=\"Ukloni\"]").on('click', function (e) {
            $.get("../Home/" + type + "Ukloni?id=" + e.target.id, function () {
                $("#Main-Display").html("");  
            });
        });
    }
})