using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;

namespace AppClient.Controllers
{
    public class TarifaController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        //
        // GET: /Tarifa/
        public ActionResult Index()
        {
            var listado = proxy.ListarTarifasAdmin();
            return View(listado);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Tarifa tarifa)
        {
            proxy.AgregarTarifa(tarifa);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            var tarifa = proxy.ObtenerTarifaxI(id);

            if (tarifa == null)
            {
                return HttpNotFound();
            }
            return View(tarifa);
        }

        [HttpPost]
        public ActionResult Edit(Tarifa tarifa)
        {
            proxy.ActualizarTarifa(tarifa);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id = 0)
        {
            var tarifa = proxy.ObtenerTarifaxI(id);

            if (tarifa == null)
            {
                return HttpNotFound();
            }
            return View(tarifa);

        }

        [HttpPost]
        public ActionResult Eliminar(Tarifa tarifa)
        {
            proxy.EliminarTarifa(tarifa);
            return RedirectToAction("Index");
        }

	}
}