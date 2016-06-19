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

        public ActionResult InsertarHoras(DateTime dia)
        {
            int i = 0;
            string diaFormat = string.Format("{0:yyyy-MM-dd}", dia);
            ViewBag.dia = diaFormat;
            var listado = proxy.ListarTarifas(dia,i);
            return View(listado);
        }

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
            proxy.AgregarReserva(reserva,listaDetalles);
           
            return RedirectToAction("Index");
        }
	}
}