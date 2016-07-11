using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;
namespace AppClient.Controllers
{
    public class BancoController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        // GET: Banco
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Index(Pago pago)
        {
            double monto = proxy.retornarMontoAPagar(pago.nroPago);
            List<Pago> lista = new List<Pago>();
            Pago p = new Pago(); p.monto = monto;
            lista.Add(p);
            Session["nroAPagar"] = pago.nroPago;
            Session["listt"] = lista;
            return RedirectToAction("verMonto");

        }
        
        public ActionResult verMonto()
        {
           var lista = Session["listt"];
           
            return View(lista);
        }

        public ActionResult pagar()
        {
            string nroAPagar = (string)Session["nroAPagar"];
            bool resul=proxy.pagarReservaConEfectivo(nroAPagar);
            if (resul==true)
            ViewBag.Mensaje = "Pagado";
            else
            ViewBag.Mensaje = "No se pudo Pagar";
            return View();

        }

        public ActionResult cancelar()
        {
            Session.Remove("listt");
            Session.Remove("nroAPagar");
            return RedirectToAction("Index");
        }
    }
}