﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace AppClient.Controllers
{
    public class UsuarioController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        //
        // GET: /Usuario/
    

        public ActionResult listaUsuario()
        {
            if (Session["usuario"] != null)
            {
                var webClient = new WebClient();
                var json = webClient.DownloadString("http://localhost:1557/ServicioRest.svc/Usuarios");
                var js = new JavaScriptSerializer();              
                var lista = js.Deserialize<List<Usuario>>(json);                
                  //var listado = proxy.ListarUsuario();
               return View(lista);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }

        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Usuario usuario, HttpPostedFileBase fimage)
        {
            if (fimage != null)
            {
                using (var reader = new BinaryReader(fimage.InputStream))
                {
                    byte[] data = reader.ReadBytes(fimage.ContentLength);
                    usuario.Imagen = data;
                }
            }
            else
            {
                usuario.Imagen = null;
            }
            bool resul=proxy.AgregarUsuario(usuario);
            if (resul)
                proxy.MensajeBienvenida(usuario);
            return RedirectToAction("listaUsuario");
        }

        public ActionResult Edit(int id = 0)
        {
            var usuario = proxy.ObtenerUsuarioId(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Edit(Usuario usuario, HttpPostedFileBase fimage)
        {
            if (fimage != null)
            {
                using (var reader = new BinaryReader(fimage.InputStream))
                {
                    byte[] data = reader.ReadBytes(fimage.ContentLength);
                    usuario.Imagen = data;
                }
            }
            else
            {
                usuario.Imagen = null;
            }
            proxy.ActualizarUsuario(usuario);
            return RedirectToAction("listaUsuario");
        }

        public ActionResult Eliminar(int id = 0)
        {
            var usuario = proxy.ObtenerUsuarioId(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);

        }

        [HttpPost]
        public ActionResult Eliminar(Usuario usuario)
        {
            proxy.EmininarUsuario(usuario);
            return RedirectToAction("listaUsuario");
        }

	}
}