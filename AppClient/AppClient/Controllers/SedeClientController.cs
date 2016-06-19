using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;

namespace AppClient.Controllers
{
    public class SedeClientController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        // GET: SedeClient
        public ActionResult Index()
        {
            var listado = proxy.ListarSedes();

            return View(listado);
        }
    }
}