
$(function () {
    $(".btnPagar").click(function () {
        var monto = $(this).attr("data-id");
        $("#modalPagar #modal-content").load("/Reserva/PagarEfectivo?monto=" + monto, function () {
            $("#modalPagar").openModal();
        })
    });
})

$(function () {
    $(".btnDetalle").click(function () {
        var idDetalle = $(this).attr("data-id");
        $("#modalDetalle #modal-content").load("/Reserva/DetallexReserva?IdReserva=" + idDetalle, function () {
            $("#modalDetalle").openModal();
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