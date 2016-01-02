using Canchita.Service.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Canchita.Service.Data
{
    public class ReportesDAO
    {
        public List<ReporteReservasAnio> ReservasXAnio(string anio,int idSede)
        {
            List<ReporteReservasAnio> lista = null;

            string query = " SELECT DATENAME(MM, r.fechaOperacion) mes, sum(r.monto) monto,MONTH(r.fechaOperacion) " +
	                       "from reserva r inner join Campo c on r.idCampo =c.idCampo "+
                           "where YEAR(fechaReserva)=@pr1 and r.estado='Cancelado' and c.idSede=@pr2 "+
                           "GROUP BY DATENAME(MM, fechaOperacion),MONTH(r.fechaOperacion) " +
                           "order by MONTH(r.fechaOperacion) ";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",anio),
                 DBHelper.MakeParam("@pr2",idSede)
             };
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    lista = new List<ReporteReservasAnio>();
                    while (lector.Read())
                    {
                        ReporteReservasAnio reporte = new ReporteReservasAnio();
                        reporte.Mes = lector["mes"].ToString();
                        reporte.Monto = double.Parse(lector["monto"].ToString());
                        lista.Add(reporte);
                    }
                }
            }
            return lista;
        }

    }
}