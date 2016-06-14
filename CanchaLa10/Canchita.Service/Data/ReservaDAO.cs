using Canchita.Service.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Canchita.Service.Data
{
    public class ReservaDAO
    {
        public bool Agregar(Reserva reserva)
        {
            bool exito = false;
            string query = "INSERT INTO RESERVA  VALUES(@pr1,@pr2,@pr3,@pr4,@pr5)";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",reserva.FechaReserva),
                 DBHelper.MakeParam("@pr2",reserva.campo.Id),
                 DBHelper.MakeParam("@pr3",reserva.usuario.Id),
                 DBHelper.MakeParam("@pr4",reserva.Estado),
                 DBHelper.MakeParam("@pr5",reserva.Monto)
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public int ObtenerIdUltimaReserva()
        {
            int idReserva = 0;
            string query = " SELECT MAX(IDRESERVA) FROM  RESERVA";
            idReserva = Convert.ToInt32(DBHelper.ExecuteScalar(query));
            return idReserva;
        }
    }
}