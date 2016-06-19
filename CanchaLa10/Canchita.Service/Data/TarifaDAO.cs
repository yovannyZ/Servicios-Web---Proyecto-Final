using Canchita.Service.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Canchita.Service.Data
{
    public class TarifaDAO
    {
        public List<Tarifa> Listar(DateTime fechaReserva)
        {
            List<Tarifa> lista = new List<Tarifa>();
            string query="SELECT * FROM tarifa WHERE horaInicio not in(SELECT  rt.horaInicio FROM Reserva_Tarifa rt"+
                         " INNER JOIN Reserva r on rt.idRserva = r.idReserva where r.fechaReserva=@pr1)";

            SqlParameter[] parametros = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",fechaReserva)
             };

            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, parametros))
            {
                if (lector != null && lector.HasRows)
                {
                    Tarifa tarifa;
                    while (lector.Read())
                    {
                        tarifa = new Tarifa();
                        tarifa.Id = Convert.ToInt32(lector["IdTarifa"]);
                        tarifa.HoraInicio = lector["HoraInicio"].ToString();
                        tarifa.HoraFin = lector["HoraFin"].ToString();
                        tarifa.Precio = Convert.ToDouble(lector["Precio"]);
                        lista.Add(tarifa);
                    }
                }
            }
            return lista;
        }
    }
}