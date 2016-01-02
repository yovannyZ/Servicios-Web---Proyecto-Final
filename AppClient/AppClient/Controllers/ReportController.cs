using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppClient.CanchitaWS;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace AppClient.Controllers
{
    public class ReportController : Controller
    {
        TransaccionClient proxy = new TransaccionClient();
        //
        // GET: /Report/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string anio)
        {
            using (TransaccionClient proxy = new TransaccionClient())
            {
                int idSede = (int)Session["sedeSelect"];
                var listado = proxy.ReservasxAnio(anio, idSede);
                return View(listado);
            }
        }

        public ActionResult ExportToExcel()
        {
            string anio = "2016";
            int idSede = (int)Session["sedeSelect"];
            var listado = proxy.ReservasxAnio(anio, idSede);

            var products = new System.Data.DataTable("teste");
            products.Columns.Add("Mes", typeof(string));
            products.Columns.Add("Monto", typeof(double));

            foreach(var item in listado){
                products.Rows.Add(item.Mes, item.Monto);
            }

            var grid = new GridView();
            
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }
	}
}