$("#btnNuevo").click(function (eve) {
    $("#modalCrear #modal-content").load("/Usuario/Create");
});
$(function () {
    $(".btnEdit").click(function () {
        var id = $(this).attr("data-id");
        $("#modalEditar #modal-content").load("/Usuario/Edit?id=" + id, function () {
            $("#modalEditar").openModal();
        })
    });
})

$(function () {
    $(".btnEliminar").click(function () {
        var id = $(this).attr("data-id");
        $("#modalEliminar #modal-content").load("/Usuario/Eliminar?id=" + id, function () {
            $("#modalEliminar").openModal();
        })
    });
})


