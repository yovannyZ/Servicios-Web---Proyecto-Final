using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;
namespace AppClient.Controllers
{
    public class AjaxController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        //
        // GET: /Ajax/
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ShowDetails()
        {
            List<Usuario> listado = proxy.ListarUsuario();

            return PartialView("_ShowDetails", listado);
        }
	}
}