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
            var listadoDetalles = (List<DetalleReserva>)Session["listaDetallesCliente"];

          return View(listadoDetalles);
        }

        public ActionResult EliminarDetalle(int id)
        {
            var listadoDetalles = (List<DetalleReserva>)Session["listaDetallesCliente"];
            listadoDetalles.RemoveAll(x => x.Tarifa.Id==id);
            Session["listaDetallesCliente"] = listadoDetalles;
            return RedirectToAction("DetalleReserva");
        }

        [HttpPost]
        public ActionResult CrearReserva()
        {
            double monto = 0;
            var listadoDetalles = (List<DetalleReserva>)Session["listaDetallesCliente"];

            foreach(var lista in listadoDetalles ){
                monto = monto + lista.Precio;
            }

            Reserva reserva = new Reserva();
            Campo campo = proxy.ObtenerCamposXId((int)Session["idCampoCliente"]);
            Usuario usuario = (Usuario)Session["usuarioCliente"];
            reserva.campo = campo;
            reserva.usuario = usuario;
            reserva.FechaReserva = (DateTime)Session["diaReservaCliente"];
            reserva.Estado = "Pendiente";
            reserva.Monto = monto;
            proxy.AgregarReserva(reserva,listadoDetalles);

            int idUltimaReserva = proxy.obtenerUltimoIDReseva();
            Pago pago = new Pago();
            pago.nroPago = "P" + idUltimaReserva;
            proxy.reservarPagoEfectivo(pago, idUltimaReserva);

            return RedirectToAction("verlistaReservasxCliente");
        }

        public ActionResult verlistaReservasxCliente()
        {
            Usuario usuario = (Usuario)Session["usuarioCliente"];
            int idUsuario = usuario.Id;
            var listado = proxy.listarReservaXUsuario(idUsuario);
            if (listado.Count <=0)
            {
                ViewBag.Mensaje = "Ud. no cuenta con reservas";
            }
                
            return View(listado);
        }


        public ActionResult verDetalleXReserva(int id)
        {
            var listado = proxy.verDetalleReserva(id);
            
            return View(listado);
        }

	}
}