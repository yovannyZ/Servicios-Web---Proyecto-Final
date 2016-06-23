using AppClient.CanchitaWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppClient.Controllers
{
  
        
    public class CampoController : Controller
    {

        TransaccionClient proxy = new TransaccionClient();
        // GET: Campo
        public ActionResult Index()
        {
            var listado = proxy.ListarCampos();
            return View(listado);
        }

        public ActionResult ListarCampoCombo()
        {

            int idSede = (int)Session["sedeSelect"];
            var listado = proxy.ObtenerCamposXSede(idSede);
            ViewBag.Id = new SelectList(listado, "Id", "Descripcion");
            return View();
        }

        public ActionResult VerCalendario(int combo)
        {
            Session["idCampo"] = combo;
            return View();
        }

        public ActionResult Disponibilidad(DateTime dia)
        {
            string diaFormat = string.Format("{0:yyyy-MM-dd}", dia);
            ViewBag.dia = diaFormat;
            Session["diaReserva"] = dia;
            int idCampo = (int)Session["idCampo"];
            var listado = proxy.ListarTarifas(dia, idCampo);
            return View(listado);
        }

        [HttpPost]
        public ActionResult Disponibilidad(List<Tarifa> lista)
        {
            DetalleReserva dtReserva;
            List<DetalleReserva> listaDetalles = new List<DetalleReserva>();
            double monto = 0;
            foreach (var tarifa in lista)
            {
                if (tarifa.Checked)
                {
                    monto = tarifa.Precio + monto;
                    dtReserva = new DetalleReserva();
                    dtReserva.Tarifa = tarifa;
                    dtReserva.HoraInicio = tarifa.HoraInicio;
                    dtReserva.HoraFin = tarifa.HoraFin;
                    dtReserva.Precio = tarifa.Precio;
                    listaDetalles.Add(dtReserva);
                }
            }
            Session["listaDetalles"] = listaDetalles;

            return RedirectToAction("DetalleReserva", "Reserva");
        }


        public PartialViewResult Verajax(DateTime dia)
        {
            string diaFormat = string.Format("{0:yyyy-MM-dd}", dia);
            ViewBag.dia = diaFormat;
            Session["diaReserva"] = dia;
            int idCampo = (int)Session["idCampo"];
            var listado = proxy.ListarTarifas(dia, idCampo);
            return PartialView("_Verajax", listado);
        }



        public ActionResult Create()
        {

            var listaSedes = proxy.ListarSedes();
            ViewBag.Id = new SelectList(listaSedes, "Id", "Descripcion");

            return View();
        }


        [HttpPost]
        public ActionResult Create(Campo campo, HttpPostedFileBase fimage, int combo)
        {

            if (fimage != null)
            {
                using (var reader = new BinaryReader(fimage.InputStream))
                {
                    byte[] data = reader.ReadBytes(fimage.ContentLength);
                    campo.Imagen = data;
                }
            }
            else
            {
                campo.Imagen = null;
            }
            Sede sede = new Sede();
            sede.Id = combo;
            campo.Sede = sede;
            proxy.AgregarCampo(campo);
            return RedirectToAction("Index");
        }


    }
}