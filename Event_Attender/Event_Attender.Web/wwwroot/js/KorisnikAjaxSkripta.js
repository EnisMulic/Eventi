function DodajAjaxEvente() {

    $("a[ajax-poziv='like']").click(function (event) {
        $(this).attr("ajax-poziv", "dodan");
        event.preventDefault();
        var urlPoziv = $(this).attr("href");
        var mjestoRezultata = $(this).attr("ajax-rezultat");
        //ajax - rezultat="likeTd"
        $.get(urlPoziv, function (data, status) {
            $("#likeTd").html(data);
        })
    });
    $("a[ajax-poziv='dislike']").click(function (event) {
        $(this).attr("ajax-poziv", "dodan");
        event.preventDefault();
        var urlPoziv = $(this).attr("href");
        var mjestoRezultata = $(this).attr("ajax-rezultat");
        $.get(urlPoziv, function (data, status) {
            $("#likeTd").html(data);
        })
    })
}


$(document).ready(function () {
    DodajAjaxEvente();
});
$(document).ajaxComplete(function () {
    DodajAjaxEvente();
});



