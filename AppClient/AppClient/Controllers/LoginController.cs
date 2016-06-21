using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;

namespace AppClient.Controllers
{
    public class LoginController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Login()
        {
            var listasedes = proxy.ListarSedes();
            ViewBag.Id = new SelectList(listasedes, "Id", "Descripcion");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario, int combo)
        {
            usuario.TipoUsuario = "Administrador";
            var listasedes = proxy.ListarSedes();
            ViewBag.Id = new SelectList(listasedes, "Id", "Descripcion");
            Session["sedeSelect"] = combo;
            Usuario usuLogeado = proxy.ValidarUsuario(usuario);
            if (usuLogeado != null)
            {
                Session["usuario"] = usuLogeado;

                return RedirectToAction("Index", "Menu");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña Incorrecta";
                return View();
            }

        }
	}
}