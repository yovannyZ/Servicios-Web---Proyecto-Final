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
            int idCampo = (int)Session["idCampo"];
            var listado = proxy.ListarTarifas(dia, idCampo);
            return View(listado);
        }


    }
}