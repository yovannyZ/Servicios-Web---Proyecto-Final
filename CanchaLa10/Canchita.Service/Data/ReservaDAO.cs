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

        public List<Reserva> ListarReservaXUsuario(int idUsuario)
        {
            Reserva reserva = null;
            Campo campo = new Campo();
            Usuario usuario = new Usuario();
            List<Reserva> lista = new List<Reserva>();
            string query = "Select  R.* from Usuario as U, Reserva as R Where U.idUsuario=R.idUsuario And U.idUsuario=@idUsuario";
            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@idUsuario",idUsuario)
             };
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {

                if (lector != null && lector.HasRows)
                {

                    while (lector.Read())
                    {
                        reserva = new Reserva();
                        reserva.Id = int.Parse(lector["idReserva"].ToString());
                        reserva.FechaReserva = DateTime.Parse(lector["fechaReserva"].ToString());
                        reserva.Monto = double.Parse(lector["monto"].ToString());
                        reserva.Estado = lector["estado"].ToString();
                        campo.Id = int.Parse(lector["idCampo"].ToString());
                        usuario.Id = int.Parse(lector["idUsuario"].ToString());
                        reserva.campo = campo;
                        reserva.usuario = usuario;
                        lista.Add(reserva);
                    }
                }
            }
            return lista;
        }

    }
}