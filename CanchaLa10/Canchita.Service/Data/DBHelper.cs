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
        private static string cadenaConexion = "server=(local);DataBase=reservabd;user=sa;password=123";
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

        public static SqlDataReader ExecuteDataReader(string query,SqlParameter[] parametros)
        {
            SqlDataReader dr;

            SqlConnection cn = new SqlConnection(cadenaConexion);
            cn.Open();
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.CommandType = CommandType.Text;
            if (parametros != null)
            {
                foreach (SqlParameter parametro in parametros)
                {
                    cmd.Parameters.Add(parametro);
                }
            }
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        public static object ExecuteScalar(string query)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.Text;
                return cmd.ExecuteScalar();

            }
        }
    }
}