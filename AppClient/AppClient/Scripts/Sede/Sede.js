$("#btnNuevo").click(function (eve) {
    $("#modalCrear #modal-content").load("/Sede/Create");
});
$(function () {
    $(".btnEdit").click(function () {
        var id = $(this).attr("data-id");
        $("#modalEditar #modal-content").load("/Sede/Edit?id=" + id, function () {
            $("#modalEditar").openModal();
        })
    });
})

$(function () {
    $(".btnEliminar").click(function () {
        var id = $(this).attr("data-id");
        $("#modalEliminar #modal-content").load("/Sede/Eliminar?id=" + id, function () {
            $("#modalEliminar").openModal();
        })
    });
})




