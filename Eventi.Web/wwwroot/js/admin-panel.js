$(document).ready(function () {

    $("div.ea-navbar").on('click', function (e) {
        $.get("../" + e.target.id + "/" + e.target.id + "List", function (response) {
            $("#Main-Display").html(response);

            SetActionDetails(e.target.id);
            SetActionEdit(e.target.id);
            SetActionCreate(e.target.id);
            SetActionRemove(e.target.id);
        });
    });

    $("div.ea-navbar-home").on('click', function (e) {
        $("#Main-Display").html("");
    });

    function SetActionDetails(type) {
        $("button[for=\"Details\"]").on('click', function (e) {
            $.get("../" + type + "/" + type + "Details?id=" + e.target.id, function (response) {
                $("#Main-Display").html(response);

                SetActionEdit(type);
            });
        });
    }

    function SetActionEdit(type) {
        $("button[for=\"Edit\"]").on('click', function (e) {
            $.get("../" + type + "/" + type + "Edit?id=" + e.target.id, function (response) {
                $("#Main-Display").html(response);
                $.validator.unobtrusive.parse(".ea-form");
            });
        });
    }

    function SetActionCreate(type) {
        $("button[for=\"Create\"]").on('click', function (e) {
            $.get("../" + type + "/" + type + "Create", function (response) {
                $("#Main-Display").html(response);
                $.validator.unobtrusive.parse(".ea-form");
            });
        });
    }

    function SetActionRemove(type) {
        $("button[for=\"Remove\"]").on('click', function (e) {
            $.get("../" + type + "/" + type + "Remove?id=" + e.target.id, function () {
                $("#Main-Display").html("");  
            });
        });
    }
})