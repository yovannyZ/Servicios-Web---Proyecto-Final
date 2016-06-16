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

        public double retornarmonto(int idReserva)
        {
            double monto=0;
             string query = "SELECT monto From Reserva WHERE idReserva=@idR";
             SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@idR",idReserva)
             };
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    while (lector.Read())
                    {
                      monto=double.Parse(lector["monto"].ToString());                        
                    }
                }
            }

            return monto ;
        }

        public bool cancelarReserva(int idReserva)
        {
            bool resul = false;
            string queryCancelarReserva = "Update Reserva set estado='Cancelado' Where idReserva=@pIDReserva";
            SqlParameter[] dbPa = new SqlParameter[]
                      {                
                     DBHelper.MakeParam("@pIDReserva",idReserva)
                      };
            resul = DBHelper.ExecuteNonQuery(queryCancelarReserva, dbPa) > 0;
            return resul;
        }


    }
}