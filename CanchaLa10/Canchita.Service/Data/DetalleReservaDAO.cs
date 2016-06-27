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

        public List<DetalleReservaCompletoxUsuario> VerDetalleXReserva(int idReserva)
        {
            List<DetalleReservaCompletoxUsuario> list = new List<DetalleReservaCompletoxUsuario>();
            DetalleReservaCompletoxUsuario detalleReservaFull = null;


            string query = "select RT.HoraInicio,RT.HoraFin,RT.tarifa , C.Descripcion, C.Imagen, S.Descripcion,S.Direccion from Reserva_Tarifa RT,Reserva R, Usuario U, Campo C,Sede S Where U.idUsuario=r.idUsuario AND R.idReserva=RT.idRserva  and C.idCampo=R.idCampo and S.idSede=C.idSede AND R.idReserva=@idReserva";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@idReserva",idReserva)
             };
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    while (lector.Read())
                    {
                        detalleReservaFull = new DetalleReservaCompletoxUsuario();
                        detalleReservaFull.horaInicio = lector[0].ToString();
                        detalleReservaFull.horaFin = lector[1].ToString();
                        detalleReservaFull.tarifa = double.Parse(lector[2].ToString());
                        detalleReservaFull.DescripcionCampo = lector[3].ToString();
                        var img = lector[4];
                        if (img is System.DBNull)
                        {
                            detalleReservaFull.imagencampo = null;
                        }
                        else
                        {
                            detalleReservaFull.imagencampo = (byte[])img;
                        }
                        detalleReservaFull.DescripcionSede = lector[5].ToString();
                        detalleReservaFull.DireccionSede = lector[6].ToString();

                        list.Add(detalleReservaFull);
                    }
                }
            }


            return list;
        }

        public List<DetalleReserva> listarDetalleXReserva(int idReserva)
        {
            ReservaDAO reservaDao = new ReservaDAO();
            TarifaDAO tarifaDao = new TarifaDAO();

            List<DetalleReserva> listadoDetalle = null;
            DetalleReserva detalle = null;

            string query = "Select * from  Reserva_Tarifa where IdRserva=@pr1";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",idReserva)
             };

            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    listadoDetalle = new List<DetalleReserva>();
                    while (lector.Read())
                    {
                        detalle = new DetalleReserva();
                        detalle.Reserva = reservaDao.ObtenerReservaxId(int.Parse(lector["idRserva"].ToString()));
                        detalle.Tarifa = tarifaDao.ObtenerTarifaxId(int.Parse(lector["idTarifa"].ToString()));
                        detalle.HoraInicio = lector["HoraInicio"].ToString();
                        detalle.HoraFin = lector["HoraFin"].ToString();
                        detalle.Precio=double.Parse( lector["Tarifa"].ToString());
                        listadoDetalle.Add(detalle);
                     
                    }
                }
            }

            return listadoDetalle;
        }
    }
}