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
        public ActionResult Index()
        {
            return View();
        }

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



     /*   public ActionResult InsertarHoras(DateTime dia)
        {
            int i = (int)Session["idCampo"];
            string diaFormat = string.Format("{0:yyyy-MM-dd}", dia);
            ViewBag.dia = diaFormat;
            var listado = proxy.ListarTarifas(dia, i);
            return View(listado);
        }*/

        

/*
        [HttpPost]
        public ActionResult InsertarHoras(List<Tarifa> lista, DateTime dia)
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
            Reserva reserva = new Reserva();
            Campo campo = new Campo { Id = 1 };
            Usuario usuario = new Usuario { Id = 3 };
            reserva.campo = campo;
            reserva.usuario = usuario;
            reserva.FechaReserva = dia;
            reserva.Estado = "Pendiente";
            reserva.Monto = monto;
            proxy.AgregarReserva(reserva, listaDetalles);

            return RedirectToAction("Index");
        }
        */

	}
}