using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;

namespace AppClient.Controllers
{
    public class ReservaClientController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        //
        // GET: /ReservaClient/
        public ActionResult DetalleReserva()
        {
          var listadoDetalles = (List<DetalleReserva>)Session["listaDetalles"];

          return View(listadoDetalles);
        }

        public ActionResult EliminarDetalle(int id)
        {
            var listadoDetalles = (List<DetalleReserva>)Session["listaDetalles"];
            listadoDetalles.RemoveAll(x => x.Tarifa.Id==id);
            Session["listaDetalles"] = listadoDetalles;
            return RedirectToAction("DetalleReserva");
        }

        [HttpPost]
        public ActionResult CrearReserva()
        {
            double monto = 0;
            var listadoDetalles = (List<DetalleReserva>)Session["listaDetalles"];

            foreach(var lista in listadoDetalles ){
                monto = monto + lista.Precio;
            }

            Reserva reserva = new Reserva();
            Campo campo = proxy.ObtenerCamposXId((int)Session["idCampo"]);
            Usuario usuario =(Usuario)Session["usuario"];
            reserva.campo = campo;
            reserva.usuario = usuario;
            reserva.FechaReserva = (DateTime)Session["diaReserva"];
            reserva.Estado = "Pendiente";
            reserva.Monto = monto;
            proxy.AgregarReserva(reserva,listadoDetalles);

            return View();
        }
	}
}