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

            string query = "INSERT INTO CAMPO VALUES(@pr1,@pr2,@pr3)";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",campo.Descripcion),
                 DBHelper.MakeParam("@pr2",campo.Estado),
                 DBHelper.MakeParam("@pr3",campo.Sede.Id),
                 
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public bool Eliminar(Campo campo)
        {
            bool exito = false;
            string query = "DELETE FROM CAMPO WHERE idCampo =@pr1";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",campo.Id),
                  
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public bool Actualizar(Campo campo)
        {
            bool exito = false;
            string query = "UPDATE CAMPO SET Descripcion = @pr1, Estado=@pr2, idSede=@pr3 WHERE idCampo= @pr4";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",campo.Descripcion),
                 DBHelper.MakeParam("@pr2",campo.Estado),
                 DBHelper.MakeParam("@pr3",campo.Sede.Id),
                 DBHelper.MakeParam("@pr4",campo.Id),
                

             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public List<Campo> ListarCampos()
        {
            List<Campo> lista = new List<Campo>();

            string query = "SELECT * FROM Campo";
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
                        campo.Sede.Id = Convert.ToInt32(lector["idSede"]);

                        lista.Add(campo);
                    }
                }
            }

            return lista;
        }

    }
}