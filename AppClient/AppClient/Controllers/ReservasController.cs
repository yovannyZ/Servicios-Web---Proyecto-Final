using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;
using System.Web.Script.Serialization;

namespace AppClient.Controllers
{
    public class ReservasController : Controller
    {
        //
        // GET: /Prueba/
        public ActionResult Index()
        {
            using (TransaccionClient proxy = new TransaccionClient())
            {
                ViewBag.listadoSedes = proxy.ListarSedes();
                return View();
            }
           
           
        }

        [HttpPost]
        public JsonResult Index(int idSede)
        {
            using (TransaccionClient proxy = new TransaccionClient())
            {
                var resultado = new JsonResult();
                resultado.Data =new {   listadoCampos = proxy.ObtenerCamposXSede(idSede) };
                return resultado;
              
            }


           
        }

      
        public ActionResult CamposPorSedes(string id)
        {
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            var proxy = new TransaccionClient();
            
            var campos =  proxy.ObtenerCamposXSede2(Convert.ToInt32(id));
            return new JsonResult()
            {
                Data = campos,
                JsonRequestBehavior=JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
         
        }

        public ActionResult MostrarImagen(string id)
        {
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            var proxy = new TransaccionClient();
            
            var campos = proxy.ObtenerCamposXId(Convert.ToInt32(id));
            var imagen = Convert.ToBase64String(campos.Imagen);
            
            return new JsonResult()
            {
                Data = imagen,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public ActionResult Informacion(string id)
        {
            Session["idCampoCliente"] = id;

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            var proxy = new TransaccionClient();

            var campos = proxy.ObtenerCamposXId(Convert.ToInt32(id));
          

            return new JsonResult()
            {
                Data = campos,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public ActionResult Tarifa(string dia, int campo)
        {
            Session["idCampoCliente"] = campo;
            DateTime fecha = Convert.ToDateTime(dia);
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            var proxy = new TransaccionClient();


            string diaFormat = string.Format("{0:yyyy-MM-dd}", fecha);

            Session["diaReservaCliente"] = dia;
            

            var listado = proxy.ListarTarifas(fecha, campo);

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
                return new JsonResult()
                {
                    Data = lista,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = Int32.MaxValue
                };
               
            }

            return new JsonResult()
            {
                Data = listado,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

	}
}