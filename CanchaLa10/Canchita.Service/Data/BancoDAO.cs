using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Canchita.Service.Modelo;

namespace Canchita.Service.Data
{
    public class BancoDAO
    {

        public double retornarMontoAPagar(string nropago)
        {
            double monto = 0;
            string query = "SELECT monto FROM PAGO WHERE nroPago=@nroPago and estado='Pendiente'";
            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@nroPago",nropago)
             };
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    while (lector.Read())
                    {
                        monto = double.Parse(lector["monto"].ToString());                        
                    }
                }
            }
            return monto ;
        }

        

    }
}