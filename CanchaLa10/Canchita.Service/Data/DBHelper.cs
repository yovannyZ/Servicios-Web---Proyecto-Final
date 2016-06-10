using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Canchita.Service.Data
{
    public class DBHelper
    {
        private static string cadenaConexion = "server=(local);DataBase=reservabd;user=sa;password=Developer2016";
        public static SqlParameter MakeParam(string paramName,object objValue)
        {
            SqlParameter param;
            param = new SqlParameter(paramName, objValue);
            param.Value = objValue;
            return param;
        }

        public static int ExecuteNonQuery(string query, SqlParameter[] dbParams)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.Text;

                if (dbParams != null)
                {
                    foreach (SqlParameter dbParam in dbParams)
                    {
                        cmd.Parameters.Add(dbParam);
                    }
                }
                return cmd.ExecuteNonQuery();

            }     
        }

        public static SqlDataReader ExecuteDataReader(string query)
        {
            SqlDataReader dr;

            SqlConnection cn = new SqlConnection(cadenaConexion);
            cn.Open();
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
    }
}