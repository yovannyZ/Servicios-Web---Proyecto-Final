$("#btnNuevo").click(function (eve) {
    $("#modalCrear #modal-content").load("/Campo/Create");
});
$(function () {
    $(".btnEdit").click(function () {
        var id = $(this).attr("data-id");
        $("#modalEditar #modal-content").load("/Campo/Edit?id=" + id, function () {
            $("#modalEditar").openModal();
        })
    });
})

$(function () {
    $(".btnEliminar").click(function () {
        var id = $(this).attr("data-id");
        $("#modalEliminar #modal-content").load("/Campo/Eliminar?id=" + id, function () {
            $("#modalEliminar").openModal();
        })
    });
})