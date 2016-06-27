using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;

namespace AppClient.Controllers
{
    public class CampoClientController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        
        // GET: CampoClient
        public ActionResult Index(int idSede)
        {
            Session["Sede"] = proxy.ObtenerSedeId(idSede);
            var listado = proxy.ObtenerCamposXSede(idSede);
            return View(listado);
        }

        public ActionResult VerCalendario(int idCampo)
        {
            Session["idCampo"] = idCampo;
            return View();
        }

        public ActionResult Disponibilidad(DateTime dia)
        {
            string diaFormat = string.Format("{0:yyyy-MM-dd}", dia);
            ViewBag.dia = diaFormat;
            Session["diaReserva"] = dia;
            int idCampo = (int)Session["idCampo"];
            var listado = proxy.ListarTarifas(dia, idCampo);
            return View(listado);
        }

        [HttpPost]
        public ActionResult Disponibilidad(List<Tarifa> lista)
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
            if (Session["usuario"] != null)
            {
                return RedirectToAction("DetalleReserva", "ReservaClient");
            }
            else {
                return RedirectToAction("Login", "LoginClient");
            }
        }

        public PartialViewResult Verajax(DateTime dia)
        {
            string diaFormat = string.Format("{0:yyyy-MM-dd}", dia);
            ViewBag.dia = diaFormat;
            Session["diaReserva"] = dia;
            int idCampo = (int)Session["idCampo"];
            var listado = proxy.ListarTarifas(dia, idCampo);
            return PartialView("_Verajax", listado);
        }
       
    }
}