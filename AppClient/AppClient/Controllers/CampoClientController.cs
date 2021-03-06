﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;

namespace AppClient.Controllers
{
    public class CampoClientController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        
        // GET: CampoClient
        public ActionResult Index(int idSede)
        {
            Session["SedeCliente"] = proxy.ObtenerSedeId(idSede);
            var listado = proxy.ObtenerCamposXSede(idSede);
            return View(listado);
        }

        public ActionResult VerCalendario(int idCampo)
        {
            Session["idCampoCliente"] = idCampo;
            return View();
        }

        [HttpPost]
        public ActionResult Disponibilidad(List<Tarifa> lista , DateTime dia)
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
            Session["diaReservaCliente"] = dia;
            Session["montoCliente"] = monto;
            Session["listaDetallesCliente"] = listaDetalles;
            if (Session["usuarioCliente"] != null)
            {
                return RedirectToAction("DetalleReserva", "ReservaClient");
            }
            else {
                return RedirectToAction("Login", "LoginClient");
            }
        }

        public PartialViewResult Verajax(DateTime dia)
        {
            string diaFormat = string.Format("{0:yyyy-MM-dd}", dia);
            ViewBag.dia = diaFormat;
            Session["diaReservaCliente"] = dia;
            int idCampo = (int)Session["idCampoCliente"];
            var listado = proxy.ListarTarifas(dia, idCampo);

            int horaActual = DateTime.Now.Hour;

            if (diaFormat == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                List<Tarifa> lista = new List<Tarifa>();
                foreach (var tarifa in listado)
                {
                    int hora = int.Parse(tarifa.HoraInicio.Substring(0, 2));
                    if (hora >= horaActual)
                    {
                        lista.Add(tarifa);
                    }

                }
                return PartialView("_Verajax", lista);
            }
           

            return PartialView("_Verajax", listado);
        }
       
    }
}