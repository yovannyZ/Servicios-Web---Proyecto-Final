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
            Session["sede"] = proxy.ObtenerSedeId(combo);
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
            Session.Remove("usuarioclient");
            Session.Remove("monto");
            Session.Remove("MontoRecibido");
            Session.Remove("Cambio");
            Session.Remove("diaReserva");
            Session.Remove("sede");
            return RedirectToAction("Index");
        }

        public void ListarSedes()
        {
            var listasedes = proxy.ListarSedes();
            ViewBag.Id = new SelectList(listasedes, "Id", "Descripcion");
        }

        public ActionResult RecuperarAccXEmail()
        {

            return View();
        }

        [HttpPost]
        public ActionResult RecuperarAccXEmail(Usuario userRecuperar)
        {
            Usuario usuClave = new Usuario();
            usuClave = proxy.devolverUseryContra(userRecuperar);
            if (usuClave != null)
            {
                string resul = proxy.mandarCorreo(usuClave);
                if (resul.Equals("Error"))
                {
                    ViewBag.Error = "Error al enviar el correo";
                    return View();
                }
                else
                {
                    ViewBag.Info = "En Breve se enviará un correo a la cuenta especificada. Gracias";
                    return View();
                }
            }
            else
            {
                ViewBag.Error = "No se encontró email";
                return View();
            }

        }
	}
}