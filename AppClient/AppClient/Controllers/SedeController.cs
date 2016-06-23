using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;
using System.IO;
using System.Net;

namespace AppClient.Controllers
{
    public class SedeController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        
        // GET: Sede
        public ActionResult Index()
        {
            var listado = proxy.ListarSedes();
            return View(listado);
        }

        public ActionResult Create()
        {
            return View();
        }


          [HttpPost]
        public ActionResult Create(Sede sede, HttpPostedFileBase fimage)
        {
            if (fimage != null)
            {
                using (var reader = new BinaryReader(fimage.InputStream))
                {
                    byte[] data = reader.ReadBytes(fimage.ContentLength);
                    sede.Imagen = data;
                }
            }
            else
            {
                sede.Imagen = null;
            }

            sede.Estado = "Disponible";
            proxy.AgregarSede(sede);
            return RedirectToAction("Index");
        }

          public ActionResult Edit(int id=0)
          {
              var sede = proxy.ObtenerSedeId(id);

              if (sede == null)
              {
                  return HttpNotFound();
              }
              return View(sede);
          }



    }
}