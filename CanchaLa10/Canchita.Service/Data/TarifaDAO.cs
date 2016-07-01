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
        public List<Tarifa> Listar(DateTime fechaReserva, int idCampo)
        {
            List<Tarifa> lista = new List<Tarifa>();
            string query="SELECT * FROM tarifa WHERE Estado='Activo' and horaInicio not in(SELECT  rt.horaInicio FROM Reserva_Tarifa rt"+
                         " INNER JOIN Reserva r on rt.idRserva = r.idReserva where r.fechaReserva=@pr1 and r.idCampo=@pr2)";

            SqlParameter[] parametros = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",fechaReserva),
                 DBHelper.MakeParam("@pr2",idCampo)
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
                        tarifa.Estado = lector["Estado"].ToString();
                        tarifa.Precio = Convert.ToDouble(lector["Precio"]);
                        lista.Add(tarifa);
                    }
                }
            }
            return lista;
        }

        public Tarifa ObtenerTarifaxId( int idTarifa)
        {
            Tarifa tarifa = null;
            string query = "SELECT * FROM tarifa where idTarifa=@pr1";

            SqlParameter[] parametros = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",idTarifa)
             };

            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, parametros))
            {
                if (lector != null && lector.HasRows)
                {
                    
                    while (lector.Read())
                    {
                        tarifa = new Tarifa();
                        tarifa.Id = Convert.ToInt32(lector["IdTarifa"]);
                        tarifa.HoraInicio = lector["HoraInicio"].ToString();
                        tarifa.HoraFin = lector["HoraFin"].ToString();
                        tarifa.Estado = lector["Estado"].ToString();
                        tarifa.Precio = Convert.ToDouble(lector["Precio"]);
                    }
                }
            }
            return tarifa;
        }


        public List<Tarifa> ListarTarifas()
        {
            List<Tarifa> listado = null;
            Tarifa tarifa = null;
            string query = "SELECT * FROM tarifa where Estado='Activo'";

            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query))
            {
                if (lector != null && lector.HasRows)
                {
                    listado = new List<Tarifa>();
                    while (lector.Read())
                    {
                        tarifa = new Tarifa();
                        tarifa.Id = Convert.ToInt32(lector["IdTarifa"]);
                        tarifa.HoraInicio = lector["HoraInicio"].ToString();
                        tarifa.HoraFin = lector["HoraFin"].ToString();
                        tarifa.Estado = lector["Estado"].ToString();
                        tarifa.Precio = Convert.ToDouble(lector["Precio"]);
                        listado.Add(tarifa);
                    }
                }
            }
            return listado;
        }

        public bool Agregar(Tarifa tarifa)
        {
            bool exito = false;
            string query = "INSERT INTO TARIFA VALUES(@pr1,@pr2,@pr3,@pr4)";

            SqlParameter[] parametros = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",tarifa.HoraInicio),
                 DBHelper.MakeParam("@pr2",tarifa.HoraFin),
                  DBHelper.MakeParam("@pr3",tarifa.Precio),
                    DBHelper.MakeParam("@pr4","Activo")

             };
            exito = DBHelper.ExecuteNonQuery(query, parametros) > 0;
            return exito;
        }

        public bool Actulizar(Tarifa tarifa)
        {
            bool exito = false;
            string query = "UPDATE TARIFA SET HoraInicio=@pr1,HoraFin=@pr2,Precio=@pr3,Estado=@pr4 where idTarifa=@pr5";

            SqlParameter[] parametros = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",tarifa.HoraInicio),
                 DBHelper.MakeParam("@pr2",tarifa.HoraFin),
                  DBHelper.MakeParam("@pr3",tarifa.Precio),
                  DBHelper.MakeParam("@pr4",tarifa.Estado),
                   DBHelper.MakeParam("@pr5",tarifa.Id)
             };
            exito = DBHelper.ExecuteNonQuery(query, parametros) > 0;
            return exito;
        }

        public bool Eliminar(Tarifa tarifa)
        {
            bool exito = false;
            string query = "UPDATE TARIFA SET Estado='Inactivo' where idTarifa=@pr1";

            SqlParameter[] parametros = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",tarifa.Id)
                
             };
            exito = DBHelper.ExecuteNonQuery(query, parametros) > 0;
            return exito;
        }

      

    }
}