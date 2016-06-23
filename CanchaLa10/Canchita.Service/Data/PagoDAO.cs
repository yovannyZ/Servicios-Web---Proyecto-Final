using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Canchita.Service.Modelo;

namespace Canchita.Service.Data
{
    public class PagoDAO
    {
        public bool pagarconTarjeta(Pago pago,string nroTarjeta,int idReserva)
        {
            bool reserva = false;
            bool exito = false;
            ReservaDAO reservadao = new ReservaDAO();
            TarjetaDAO tarjetadao = new TarjetaDAO();
            double montoAPagar = reservadao.retornarmonto(idReserva);
            double saldoDisponible = tarjetadao.retornarSaldoTarjeta(nroTarjeta);
            if (saldoDisponible > montoAPagar)
            {
                //Quitar saldo :
                bool resul = false;                
                resul=  tarjetadao.quitarSaldo(montoAPagar, nroTarjeta);
                if (resul) { 
                //Insertar Pago :
                    string query = "INSERT INTO Pago values(@p1,@p2,@p3,@p4)";                    
                    SqlParameter[] dbParams = new SqlParameter[]
                      {                
                     DBHelper.MakeParam("@p1",pago.nroPago),
                     DBHelper.MakeParam("@p2",montoAPagar),
                     DBHelper.MakeParam("@p3","Cancelado"),
                     DBHelper.MakeParam("@p4",idReserva)
                      };
                    exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
                    if (exito)
                    {
                     //Cancelar Reservar                   :
                      reserva = reservadao.cancelarReserva(idReserva);
                    }
                }
            }
            return reserva;
        }

        public bool reservarPagoEfectivo(Pago pago, int idReserva)
        {
            ReservaDAO reservadao = new ReservaDAO();
            double montoAPagar = reservadao.retornarmonto(idReserva);
            bool exito = false;
            string query = "INSERT INTO Pago values(@p1,@p2,@p3,@p4)";            
            SqlParameter[] dbParams = new SqlParameter[]
                      {                
                     DBHelper.MakeParam("@p1",pago.nroPago),
                     DBHelper.MakeParam("@p2",montoAPagar),
                     DBHelper.MakeParam("@p3","Pendiente"),
                     DBHelper.MakeParam("@p4",idReserva)
                      };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public List<Pago> listarPagosPendientes()
        {
            List<Pago> lista = new List<Pago>();
            Reserva reserva = new Reserva();
            Pago pago = null;
            string query = "Select * from Pago where estado='Pendiente'";
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query))
            {

                if (lector != null && lector.HasRows)
                {

                    while (lector.Read())
                    {
                        pago = new Pago();
                        pago.nroPago = lector["nroPago"].ToString();
                        pago.monto = double.Parse(lector["monto"].ToString());
                        pago.estado = lector["estado"].ToString();
                        reserva.Id = Convert.ToInt32(lector["idReserva"].ToString());
                        pago.idReserva = reserva;
                        lista.Add(pago);
                    }
                }
            }
            return lista;
        }

        public List<Pago> listarPagosCancelados()
        {
            List<Pago> lista = new List<Pago>();
            string query = "Select * from Pago where estado='Cancelado'";
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query))
            {
                if (lector != null && lector.HasRows)
                {
                    Pago pago = new Pago();
                    Reserva reserva = new Reserva();
                    while (lector.Read())
                    {
                        pago.nroPago = lector[0].ToString();
                        pago.monto = double.Parse(lector[1].ToString());
                        pago.estado = lector[2].ToString();
                        reserva.Id = Convert.ToInt32(lector[3].ToString());
                        pago.idReserva = reserva;
                        lista.Add(pago);
                    }
                }
            }
            return lista;
        }

        public int retornaidReserva(string nropago)
        {
            int idReserva = 0;
            string query = "SELECT idReserva From Pago WHERE nroPago=@nroPa";
            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@nroPa",nropago)
             };
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    while (lector.Read())
                    {
                        idReserva = int.Parse(lector["idReserva"].ToString());
                    }
                }
            }

            return idReserva;
        }

        public bool pagarConEfectivo(string nropago)
        {
            ReservaDAO reservadao = new ReservaDAO();
            bool resul = false;
            int idReservaUp = retornaidReserva(nropago);
            bool exito = false;
            string query = "update Pago set estado='Cancelado' where nroPago=@pnroPago";
            SqlParameter[] dbParam = new SqlParameter[]
                      {                                     
                        DBHelper.MakeParam("@pnroPago",nropago)
                      };
            exito = DBHelper.ExecuteNonQuery(query, dbParam) > 0;
            if (exito)
            {
                //Cancelar-Reserva.
                resul = reservadao.cancelarReserva(idReservaUp);
            }

            return resul;
        }
    }
}