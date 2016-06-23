using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppClient.Controllers
{
    public sealed class ErrorsController : Controller
    {

        public ActionResult NotFound()
        {
            return PartialView("_NotFound");

          
        }
	}
}