using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;

namespace AppClient.Controllers
{
    public class ReservaController : Controller
    {

        //
        // GET: /Reserva/
        TransaccionClient proxy = new TransaccionClient();
        //
        // GET: /Home/
       

        public ActionResult DetalleReserva()
        {
            var listadoDetalles = (List<DetalleReserva>)Session["listaDetalles"];

           return View(listadoDetalles);
     }

        public ActionResult EliminarDetalle(int id)
        {
            var listadoDetalles = (List<DetalleReserva>)Session["listaDetalles"];
            listadoDetalles.RemoveAll(x => x.Tarifa.Id == id);
            
            Session["listaDetalles"] = listadoDetalles;
            return RedirectToAction("DetalleReserva");
        }


        [HttpPost]
        public ActionResult CrearReserva()
        {
            double monto = 0;
            var listadoDetalles = (List<DetalleReserva>)Session["listaDetalles"];

            foreach (var lista in listadoDetalles)
            {
                monto = monto + lista.Precio;
            }

            Reserva reserva = new Reserva();
            Campo campo = proxy.ObtenerCamposXId((int)Session["idCampo"]);
            Usuario usuario = (Usuario)Session["usuario"];
            reserva.campo = campo;
            reserva.usuario = usuario;
            reserva.FechaReserva = (DateTime)Session["diaReserva"];
            reserva.Estado = "Pendiente";
            reserva.Monto = monto;
            proxy.AgregarReserva(reserva, listadoDetalles);

            //crear pago:
            int idUltimaReserva = proxy.obtenerUltimoIDReseva();
            Pago pago = new Pago();
            pago.nroPago = "P" + idUltimaReserva;
            proxy.reservarPagoEfectivo(pago, idUltimaReserva);
            return RedirectToAction("listaPagoPend");
        }
        public ActionResult listaPagoPend()
        {
            var listaPendiente = proxy.listarPagosPendientes();

            return View(listaPendiente);
        }

        public ActionResult Pagar(string nroPago)
        {
            proxy.pagarReservaConEfectivo(nroPago);
            return RedirectToAction("listaPagoPend");
        }

        public ActionResult NuevaReserva()
        {
            var listaUsuarios= proxy.ListarUsuario();
            ViewBag.IdUsuario = new SelectList(listaUsuarios, "Id", "Nombres");

            var listaCampos = proxy.ListarCampos();
            ViewBag.IdCampo = new SelectList(listaCampos, "Id", "Descripcion");


            return View();
        }


        public PartialViewResult VerHorarios(DateTime dia, int IdCampo, int IdUsuario)
        {
            string diaFormat = string.Format("{0:yyyy-MM-dd}", dia);
            ViewBag.dia = diaFormat;
            Session["diaReserva"] = dia;
            Session["idCampo"] = IdCampo;

            Usuario usuario =  (Usuario)Session["usuario"];

            var listado = proxy.ListarTarifas(dia, IdCampo);
            return PartialView("_VerHorarios", listado);
        }


          [HttpPost]
        public ActionResult DetalleReserva(List<Tarifa> lista)
        {
            DetalleReserva dtReserva;
            List<DetalleReserva> listaDetalles = new List<DetalleReserva>();
            double monto = 0;
            foreach (var tarifa in lista)
            {
                if (tarifa.Checked)
                {
                    monto = tarifa.Precio + monto;
                    dtReserva = new DetalleReserva();
                    dtReserva.Tarifa = tarifa;
                    dtReserva.HoraInicio = tarifa.HoraInicio;
                    dtReserva.HoraFin = tarifa.HoraFin;
                    dtReserva.Precio = tarifa.Precio;
                    listaDetalles.Add(dtReserva);
                }
            }
            Session["monto"] = monto;
            Session["listaDetalles"] = listaDetalles;

            return View(listaDetalles);
        }

          public ActionResult PagarEfectivo(double monto)
          {
              ViewBag.Monto = monto;
              return View();
          }


        [HttpPost]
          public ActionResult PagarEfectivo(string montoRecibido, double monto)
          {
             
              double cambio =double.Parse(montoRecibido) - monto;
              Session["MontoRecibido"] = montoRecibido;
              Session["Cambio"] = cambio;
              return RedirectToAction("DetallePago");
          }


        public ActionResult DetallePago()
        {
            double monto = 0;
            var listadoDetalles = (List<DetalleReserva>)Session["listaDetalles"];

            foreach (var lista in listadoDetalles)
            {
                monto = monto + lista.Precio;
            }

            Reserva reserva = new Reserva();
            Campo campo = proxy.ObtenerCamposXId((int)Session["idCampo"]);
            Usuario usuario = (Usuario)Session["usuario"];
            reserva.campo = campo;
            reserva.usuario = usuario;
            reserva.FechaReserva = (DateTime)Session["diaReserva"];
            reserva.Estado = "Cancelado";
            reserva.Monto = monto;
            proxy.AgregarReserva(reserva, listadoDetalles);

            //crear pago:
            int idUltimaReserva = proxy.obtenerUltimoIDReseva();
            Pago pago = new Pago();
            pago.nroPago = "P" + idUltimaReserva;
            proxy.reservarPagoEfectivo(pago, idUltimaReserva);
            proxy.pagarReservaConEfectivo(pago.nroPago);
            return View();
        }

        public ActionResult ListadoReserva()
        {
            var listado = proxy.ListarReservas();
            return View(listado);
        }

        public ActionResult DetallexReserva(int IdReserva)
        {
            var listado = proxy.listarDetalleXReserva(IdReserva);
            return View(listado);
        }

	}
}