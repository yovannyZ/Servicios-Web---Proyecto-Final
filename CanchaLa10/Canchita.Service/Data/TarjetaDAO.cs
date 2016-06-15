using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Canchita.Service.Modelo;
namespace Canchita.Service.Data
{
    public class TarjetaDAO
    {
        public bool crearTarjeta(Tarjeta tarjeta)
        {
            bool exito = false;
            string query = "INSERT INTO Tarjeta(idTarjeta,fechaCreacion,fechaVencimiento,idUsuario) VALUES(@pr1,@pr3,@pr4,@pr5)";
            SqlParameter[] dbParams = new SqlParameter[]
             {                
                 DBHelper.MakeParam("@pr1",tarjeta.idTarjeta),
                 DBHelper.MakeParam("@pr3",tarjeta.fechaCreacion),
                 DBHelper.MakeParam("@pr4",tarjeta.fechaVencimiento),
                 DBHelper.MakeParam("@pr5",tarjeta.usuario.Id)
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public bool abonarSaldo(Tarjeta tarjeta)
        {
            bool exito = false;
            string query = "Update Tarjeta set saldo=saldo+@psaldo where idTarjeta=@pidT";
            SqlParameter[] dbParams = new SqlParameter[]
             {                
                 DBHelper.MakeParam("@psaldo",tarjeta.saldo),
                 DBHelper.MakeParam("@pidT",tarjeta.idTarjeta),
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public List<Tarjeta> listarTarjetas()
        {

            List<Tarjeta> lista = new List<Tarjeta>();
            string query = "Select  * from Tarjeta where estado='Disponible'";
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query))
            {
                if (lector != null && lector.HasRows)
                {
                    Usuario usu = new Usuario();
                    while (lector.Read())
                    {
                        Tarjeta tarje = new Tarjeta();
                        tarje.idTarjeta = lector["idTarjeta"].ToString();
                        tarje.saldo = double.Parse(lector["saldo"].ToString());
                        tarje.estado = lector["estado"].ToString();
                        tarje.fechaCreacion = DateTime.Parse(lector["fechaCreacion"].ToString());
                        tarje.fechaVencimiento = DateTime.Parse(lector["fechaVencimiento"].ToString());
                        usu.Id = Convert.ToInt32(lector["idUsuario"]);
                        tarje.usuario = usu;
                        lista.Add(tarje);
                    }
                }
            }

            return lista;
        }

        public List<Tarjeta> ObtenerTarjetasXUsuario(int idUsuario)
        {
            Tarjeta tarje = new Tarjeta();
            List<Tarjeta> lista = new List<Tarjeta>();
            Usuario usuario = new Usuario();
            string query = "SELECT * FROM Tarjeta WHERE idUsuario=@idUsu and estado='Disponible'";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@idUsu",idUsuario)
             };

            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {

                    while (lector.Read())
                    {
                        
                        tarje.idTarjeta = lector["idTarjeta"].ToString();
                        tarje.saldo = double.Parse(lector["saldo"].ToString());
                        tarje.estado = lector["estado"].ToString();
                        tarje.fechaCreacion = DateTime.Parse(lector["fechaCreacion"].ToString());
                        tarje.fechaVencimiento = DateTime.Parse(lector["fechaVencimiento"].ToString());
                        usuario.Id = Convert.ToInt32(lector["idUsuario"]);
                        tarje.usuario = usuario;
                        lista.Add(tarje);
                    }
                }
            }
            return lista;
        }



        public Tarjeta obtenerTarjeta(string idTarjeta)
        {
            Tarjeta tarje = new Tarjeta();            
            Usuario usuario = new Usuario();
            string query = "SELECT * FROM Tarjeta WHERE idTarjeta=@idT and estado='Disponible'";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@idT",idTarjeta)
             };

            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    while (lector.Read())
                    {
                        tarje.idTarjeta = lector["idTarjeta"].ToString();
                        tarje.saldo = double.Parse(lector["saldo"].ToString());
                        tarje.estado = lector["estado"].ToString();
                        tarje.fechaCreacion = DateTime.Parse(lector["fechaCreacion"].ToString());
                        tarje.fechaVencimiento = DateTime.Parse(lector["fechaVencimiento"].ToString());
                        usuario.Id = Convert.ToInt32(lector["idUsuario"]);
                        tarje.usuario = usuario;                        
                    }
                }
            }
            return tarje;
        }

        public double retornarSaldoTarjeta(string idTarjeta)
        {
            double saldo = 0;
            string query = "SELECT saldo from Tarjeta where idTarjeta=@idT and estado='Disponible'";
            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@idT",idTarjeta)
             };
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    while (lector.Read())
                    {
                       saldo = double.Parse(lector["saldo"].ToString());
                    }
                }
            }

            return saldo;
        }
    }
}