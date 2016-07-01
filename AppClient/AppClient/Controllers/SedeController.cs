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
            if (Session["usuario"] != null)
            {
                var listado = proxy.ListarSedes();
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
            proxy.AgregarSede(sede);
            return RedirectToAction("Index");
        }

          public ActionResult Edit(int id=0)
          {
              if (Session["usuario"] == null)
              {
                  return RedirectToAction("Index", "Admin");
              }
              else
              {
                  var sede = proxy.ObtenerSedeId(id);

                  if (sede == null)
                  {
                      return HttpNotFound();
                  }
                  return View(sede);
              }
              
          }

        [HttpPost]
          public ActionResult Edit(Sede sede, HttpPostedFileBase fimage)
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

              proxy.ActualizarSede(sede);
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
                var sede = proxy.ObtenerSedeId(id);

                if (sede == null)
                {
                    return HttpNotFound();
                }
                return View(sede);
            }
           
            
        }

        [HttpPost]
        public ActionResult Eliminar(Sede sede)
        {
            proxy.EliminarSede(sede);
            return RedirectToAction("Index");
        }


    }
}