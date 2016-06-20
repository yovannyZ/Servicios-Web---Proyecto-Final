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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            
                return View();
            
            
        }
	}
}