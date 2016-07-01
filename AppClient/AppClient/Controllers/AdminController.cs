using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;

namespace AppClient.Controllers
{
    public class AdminController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Index()
        {
            ListarSedes();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario, int combo)
        {
            usuario.TipoUsuario = "Administrador";
            ListarSedes();
            Session["sedeSelect"] = combo;
            Usuario usuLogeado = proxy.ValidarUsuario(usuario);
            if (usuLogeado != null)
            {
                Session["usuario"] = usuLogeado;

                return RedirectToAction("ListadoReserva", "Reserva");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña Incorrecta";
                return View();
            }

        }
        public ActionResult CerrarSesion()
        {
            Session.Remove("sedeSelect");
            Session.Remove("usuario");
            Session.Remove("listaDetalles");
            Session.Remove("DetalleReserva");
            Session.Remove("idCampo");
            Session.Remove("usuariocliente");
            Session.Remove("monto");
            Session.Remove("MontoRecibido");
            Session.Remove("Cambio");
            Session.Remove("diaReserva");
            return View("Index");
        }

        public void ListarSedes()
        {
            var listasedes = proxy.ListarSedes();
            ViewBag.Id = new SelectList(listasedes, "Id", "Descripcion");
        }
	}
}