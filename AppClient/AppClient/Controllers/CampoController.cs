using AppClient.CanchitaWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppClient.Controllers
{
  
        
    public class CampoController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        // GET: Campo
        public ActionResult Index()
        {
            var listado = proxy.ListarCampos();
            return View(listado);
        }

        public ActionResult Create()
        {
            
            var listaSedes = proxy.ListarSedes();
            ViewBag.Id = new SelectList(listaSedes, "Id", "Descripcion");

            return View();
        }


        [HttpPost]
        public ActionResult Create(Campo campo, HttpPostedFileBase fimage, int combo)
        {

            if (fimage != null)
            {
                using (var reader = new BinaryReader(fimage.InputStream))
                {
                    byte[] data = reader.ReadBytes(fimage.ContentLength);
                    campo.Imagen = data;
                }
            }
            else
            {
                campo.Imagen = null;
            }
            Sede sede = new Sede();
            sede.Id = combo;
            campo.Sede = sede;
            proxy.AgregarCampo(campo);
            return RedirectToAction("Index");
        }
    }
}