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
           if (Session["usuario"] != null)
           {
                int idSede = (int)Session["sedeSelect"];
                var listado = proxy.ObtenerCamposXSede(idSede);
                return View(listado);
           }
           else
           {
                return RedirectToAction("Index", "Admin");

           }
            
        }

        public ActionResult Create()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult Create(Campo campo, HttpPostedFileBase fimage)
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

            int idSede=(int)Session["sedeSelect"];
            Sede sede = proxy.ObtenerSedeId(idSede);
            campo.Sede = sede;
            proxy.AgregarCampo(campo);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                var campo = proxy.ObtenerCamposXId(id);

                if (campo == null)
                {
                    return HttpNotFound();
                }
                return View(campo);
            }
           
        }

        [HttpPost]
        public ActionResult Edit(Campo campo, HttpPostedFileBase fimage)
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

            proxy.ActualizarCampo(campo);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id = 0)
        {

            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                var sede = proxy.ObtenerCamposXId(id);

                if (sede == null)
                {
                    return HttpNotFound();
                }
                return View(sede);
            }
            

        }

        [HttpPost]
        public ActionResult Eliminar(Campo campo)
        {
            proxy.EliminarCampo(campo);
            return RedirectToAction("Index");
        }



    }
}