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

    }
}