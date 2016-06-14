using Canchita.Service.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Canchita.Service.Data
{
    public class DetalleReservaDAO
    {
        public bool Agregar(DetalleReserva dtReserva)
        {
            bool exito = false;
            string query = "INSERT INTO  RESERVA_TARIFA  VALUES(@pr1,@pr2,@pr3,@pr4,@pr5)";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",dtReserva.Reserva.Id),
                 DBHelper.MakeParam("@pr2",dtReserva.Tarifa.Id),
                 DBHelper.MakeParam("@pr3",dtReserva.HoraInicio),
                 DBHelper.MakeParam("@pr4",dtReserva.HoraFin),
                 DBHelper.MakeParam("@pr5",dtReserva.Precio)
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }
    }
}