using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;

namespace AppClient.Controllers
{
    public class LoginClientController : Controller
    {
        TransaccionClient proxy= new TransaccionClient();
        //
        // GET: /LoginClient/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            usuario.TipoUsuario = "Cliente";
            Usuario usuLogeado = proxy.ValidarUsuario(usuario);
            if (usuLogeado!=null)
            {

                Session["usuario"] = usuLogeado;
                return RedirectToAction("DetalleReserva", "ReservaClient");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña Incorrectas";
                return View();
            }
            
        }
	}
}