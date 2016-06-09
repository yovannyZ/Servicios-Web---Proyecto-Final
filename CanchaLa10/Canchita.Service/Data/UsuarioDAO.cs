using CanchaLa10.Service.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Canchita.Service.Data
{
    public class UsuarioDAO
    {
        string cadenaConexion = "server=(local);DataBase=reservabd;user=sa;password=Developer2016";

        public bool Agregar(Usuario usuario)
        {

            bool exito = false;
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Usuario  VALUES(@pr1,@pr2,@pr3,@pr4,@pr5,@pr6,@pr7,@pr8,@pr9)", cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@pr1", usuario.Nombres);
                cmd.Parameters.AddWithValue("@pr2", usuario.ApPaterno);
                cmd.Parameters.AddWithValue("@pr3", usuario.ApMaterno);
                cmd.Parameters.AddWithValue("@pr4", usuario.Dni);
                cmd.Parameters.AddWithValue("@pr5", usuario.Email);
                cmd.Parameters.AddWithValue("@pr6", usuario.Telefono);
                cmd.Parameters.AddWithValue("@pr7", usuario.TipoUsuario);
                cmd.Parameters.AddWithValue("@pr8", usuario.Username);
                cmd.Parameters.AddWithValue("@pr9", usuario.Clave);
                exito = cmd.ExecuteNonQuery() > 0;
            }
            return exito;
        }
    }
}