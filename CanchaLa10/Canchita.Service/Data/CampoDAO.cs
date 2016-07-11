using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Canchita.Service.Modelo;
using System.Data.SqlClient;


namespace Canchita.Service.Data
{
    public class CampoDAO
    {
        public bool Agregar(Campo campo)
        {
            bool exito = false;

            string query = "INSERT INTO CAMPO VALUES(@pr1,@pr2,@pr3,@pr4)";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",campo.Descripcion),
                 DBHelper.MakeParam("@pr2","Activo"),
                 DBHelper.MakeParam("@pr3",campo.Sede.Id),
                  DBHelper.MakeParam("@pr4",campo.Imagen==null?new byte[]{}:campo.Imagen)
                 
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public bool Eliminar(Campo campo)
        {
            bool exito = false;
            string query = "UPDATE CAMPO SET Estado=@pr1 WHERE idCampo= @pr2";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                  DBHelper.MakeParam("@pr1","Inactivo"),
                 DBHelper.MakeParam("@pr2",campo.Id)
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public bool Actualizar(Campo campo)
        {
            bool exito = false;
            string query = "UPDATE CAMPO SET Descripcion = @pr1, Estado=@pr2,Imagen=@pr5 WHERE idCampo= @pr4";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",campo.Descripcion),
                 DBHelper.MakeParam("@pr2",campo.Estado),
                 DBHelper.MakeParam("@pr4",campo.Id),
                  DBHelper.MakeParam("@pr5",campo.Imagen)
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public bool ActualizarSinImagen(Campo campo)
        {
            bool exito = false;
            string query = "UPDATE CAMPO SET Descripcion = @pr1, Estado=@pr2 WHERE idCampo= @pr4";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",campo.Descripcion),
                 DBHelper.MakeParam("@pr2",campo.Estado),
                 DBHelper.MakeParam("@pr4",campo.Id)
                
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }
        public List<Campo> ListarCampos()
        {
            List<Campo> lista = new List<Campo>();
            Sede sede = new Sede();

            string query = "SELECT * FROM Campo WHERE Estado='Activo'";
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query))
            {
                if (lector != null && lector.HasRows)
                {
                    Campo campo;
                    while (lector.Read())
                    {
                        campo = new Campo();
                        campo.Id = Convert.ToInt32(lector["idCampo"]);
                        campo.Descripcion = Convert.ToString(lector["Descripcion"]);
                        campo.Estado = Convert.ToString(lector["Estado"]);
                        sede.Id = Convert.ToInt32(lector["idSede"]);
                        var img = lector["Imagen"];
                        if (img is System.DBNull)
                        {
                            campo.Imagen = null;
                        }
                        else
                        {
                            campo.Imagen=(byte[])img;
                        }                        
                        campo.Sede = sede;
                        lista.Add(campo);
                    }
                }
            }

            return lista;
        }


        public List<Campo> ObtenerCamposXSede(int idSede)
        {
            List<Campo> lista = new List<Campo>();
            Sede sede = new Sede();

            string query = "SELECT * FROM Campo where idSede=@pr1 and  Estado='Activo'";
            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",idSede)
             };
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query,dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    Campo campo;
                    while (lector.Read())
                    {
                        campo = new Campo();
                        campo.Id = Convert.ToInt32(lector["idCampo"]);
                        campo.Descripcion = Convert.ToString(lector["Descripcion"]);
                        campo.Estado = Convert.ToString(lector["Estado"]);
                        sede.Id = Convert.ToInt32(lector["idSede"]);
                        var img = lector["Imagen"];
                        if (img is System.DBNull)
                        {
                            campo.Imagen = null;
                        }
                        else
                        {
                            campo.Imagen = (byte[])img;
                        }
                        campo.Sede = sede;
                        lista.Add(campo);
                    }
                }
            }

            return lista;
        }
        public List<Campo> ObtenerCamposXSede2(int idSede)
        {
            List<Campo> lista = new List<Campo>();


            string query = "SELECT idCampo,Descripcion FROM Campo where idSede=@pr1 and  Estado='Activo'";
            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",idSede)
             };
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    Campo campo;
                    while (lector.Read())
                    {
                        campo = new Campo();
                        campo.Id = Convert.ToInt32(lector["idCampo"]);
                        campo.Descripcion = Convert.ToString(lector["Descripcion"]);
                        lista.Add(campo);
                    }
                }
            }

            return lista;
        }
        public Campo ObtenerCamposXId(int idCampo)
        {
            var sedeDao = new SedeDAO();
            Campo campo = null;
            Sede sede = null;

            string query = "SELECT * FROM Campo where idCampo=@pr1 and  Estado='Activo'";
            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",idCampo)
             };
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                     campo= new Campo();
                     sede = new Sede();

                    while (lector.Read())
                    {
                        campo.Id = Convert.ToInt32(lector["idCampo"]);
                        campo.Descripcion = Convert.ToString(lector["Descripcion"]);
                        campo.Estado = Convert.ToString(lector["Estado"]);
                        campo.Sede=sedeDao.ObtenerSedeId(Convert.ToInt32(lector["idSede"]));
                        var img = lector["Imagen"];
                        if (img is System.DBNull)
                        {
                            campo.Imagen = null;
                        }
                        else
                        {
                            campo.Imagen = (byte[])img;
                        }
                       
                    }
                }
            }

            return campo;
        }
       
      

    }
}