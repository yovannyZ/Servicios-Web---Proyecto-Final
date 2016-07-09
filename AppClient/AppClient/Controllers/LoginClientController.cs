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
                Session["usuarioCliente"] = usuLogeado;
                if (Session["idCampoCliente"] != null)
                {
                    return RedirectToAction("DetalleReserva", "ReservaClient");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña Incorrectas";
                return View();
            }
            
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

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
           
           if ( proxy.ValidarUsuario(usuario) == null)
           {
               usuario.TipoUsuario = "Cliente";
               usuario.Estado = "Activo";
               bool result=proxy.AgregarUsuario(usuario);
               if (result)
               proxy.MensajeBienvenida(usuario);
               Usuario nuevoUsuario = proxy.ValidarUsuario(usuario);
               Session["usuarioCliente"] = nuevoUsuario;
               if (Session["idCampoCliente"] != null)
               {
                   return RedirectToAction("DetalleReserva", "ReservaClient");
               }
               return RedirectToAction("Index","Home");
           }
           else
           {
               ViewBag.Error = "El usuario ya existe";
               return View(usuario);
           }        }

        public ActionResult CerrarSesion()
        {
            Session.Remove("usuarioCliente");
            Session.Remove("idCampoCliente");
            Session.Remove("SedeCliente");
            Session.Remove("montoCliente");
            Session.Remove("listaDetallesCliente");
            Session.Remove("diaReservaCliente");
            return RedirectToAction("Index", "Home");
        }

	}
}