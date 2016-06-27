
$(function () {
    $(".btnPagar").click(function () {
        var monto = $(this).attr("data-id");
        $("#modalPagar #modal-content").load("/Reserva/PagarEfectivo?monto=" + monto, function () {
            $("#modalPagar").openModal();
        })
    });
})

$(function () {
    $(".btnBuscar").click(function () {
        $("#modalListar #modal-content").load("/Usuario/Index", function () {
            $("#modalListar").openModal();
        })
    });
})