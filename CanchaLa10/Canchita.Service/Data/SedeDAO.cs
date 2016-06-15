using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Canchita.Service.Modelo;
using System.Data.SqlClient;


namespace Canchita.Service.Data
{
    public class SedeDAO
    {
        public bool Agregar(Sede sede)
        {
            bool exito = false;

            string query = "INSERT INTO SEDE VALUES(@pr1,@pr2,@pr3)";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",sede.Descripcion),
                 DBHelper.MakeParam("@pr2",sede.Direccion),
                 DBHelper.MakeParam("@pr3",sede.Estado),
                 
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }
        public bool Eliminar(Sede sede)
        {
            bool exito = false;
            string query = "DELETE FROM SEDE WHERE idSede =@pr1";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",sede.Id),
                  
             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;

        }

        public bool Actualizar(Sede sede)
        {
            bool exito = false;
            string query = "UPDATE SEDE SET Descripcion = @pr1, Direccion=@pr2, Estado=@pr3 WHERE idSede= @pr4";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",sede.Descripcion),
                 DBHelper.MakeParam("@pr2",sede.Direccion),
                 DBHelper.MakeParam("@pr3",sede.Estado),
                 DBHelper.MakeParam("@pr4",sede.Id),
                

             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public List<Sede> ListarSedes()
        {

            List<Sede> lista = new List<Sede>();
            string query = "SELECT * FROM Sede";
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query))
            {
                if (lector != null && lector.HasRows)
                {
                    Sede sede;
                    while (lector.Read())
                    {
                        sede = new Sede();
                        sede.Id = Convert.ToInt32(lector["idSede"]);
                        sede.Descripcion = Convert.ToString(lector["Descripcion"]);
                        sede.Direccion = Convert.ToString(lector["Direccion"]);
                        sede.Estado = Convert.ToString(lector["Estado"]);

                        lista.Add(sede);
                    }
                }
            }

            return lista;

        }
    }
}