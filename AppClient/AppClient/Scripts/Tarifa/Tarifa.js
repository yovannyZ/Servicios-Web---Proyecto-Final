$("#btnNuevo").click(function (eve) {
    $("#modalCrear #modal-content").load("/Tarifa/Create");
});
$(function () {
    $(".btnEdit").click(function () {
        var id = $(this).attr("data-id");
        $("#modalEditar #modal-content").load("/Tarifa/Edit?id=" + id, function () {
            $("#modalEditar").openModal();
        })
    });
})

$(function () {
    $(".btnEliminar").click(function () {
        var id = $(this).attr("data-id");
        $("#modalEliminar #modal-content").load("/Tarifa/Eliminar?id=" + id, function () {
            $("#modalEliminar").openModal();
        })
    });
})


