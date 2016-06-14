
using Canchita.Service.Modelo;
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
       
        public bool Agregar(Usuario usuario)
        {
            bool exito = false;
            string query = "INSERT INTO USUARIO  VALUES(@pr1,@pr2,@pr3,@pr4,@pr5,@pr6,@pr7,@pr8,@pr9)";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",usuario.Nombres),
                 DBHelper.MakeParam("@pr2",usuario.ApPaterno),
                 DBHelper.MakeParam("@pr3",usuario.ApMaterno),
                 DBHelper.MakeParam("@pr4",usuario.Dni),
                 DBHelper.MakeParam("@pr5",usuario.Email),
                 DBHelper.MakeParam("@pr6",usuario.Telefono),
                 DBHelper.MakeParam("@pr7",usuario.TipoUsuario),
                 DBHelper.MakeParam("@pr8",usuario.Username),
                 DBHelper.MakeParam("@pr9",usuario.Clave)

             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams)>0;
            return exito;
        }

        public bool Actualizar(Usuario usuario)
        {
            bool exito = false;
            string query = "UPDATE USUARIO SET Nombres=@pr1,"+
                                                   "apPaterno=@pr2,"+
                                                   "apMaterno=@pr3,"+
                                                   "DNI=@pr4,"+
                                                   "EMail=@pr5,"+
                                                   "Telefono=@pr6,"+
                                                   "TipoUsuario=@pr7,"+
                                                   "username=@pr8,"+
                                                   "clave=@pr9 where idUsuario=@pr10 ";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",usuario.Nombres),
                 DBHelper.MakeParam("@pr2",usuario.ApPaterno),
                 DBHelper.MakeParam("@pr3",usuario.ApMaterno),
                 DBHelper.MakeParam("@pr4",usuario.Dni),
                 DBHelper.MakeParam("@pr5",usuario.Email),
                 DBHelper.MakeParam("@pr6",usuario.Telefono),
                 DBHelper.MakeParam("@pr7",usuario.TipoUsuario),
                 DBHelper.MakeParam("@pr8",usuario.Username),
                 DBHelper.MakeParam("@pr9",usuario.Clave),
                 DBHelper.MakeParam("@pr10",usuario.Id)

             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }
        public List<Usuario> ListarUsuarios()
        {
           
            List<Usuario> lista= new List<Usuario>();
            string query = "SELECT * FROM Usuario";
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query)) { 
            if (lector != null && lector.HasRows)
                {
                    Usuario usuario;
                    while (lector.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(lector["idUsuario"]);
                        usuario.Nombres = Convert.ToString(lector["Nombres"]);
                        usuario.ApPaterno = Convert.ToString(lector["apPaterno"]);
                        usuario.ApMaterno = Convert.ToString(lector["apMaterno"]);
                        usuario.Dni = Convert.ToString(lector["DNI"]);
                        usuario.Email = Convert.ToString(lector["EMail"]);
                        usuario.TipoUsuario = Convert.ToString(lector["TipoUsuario"]);
                        usuario.Username = Convert.ToString(lector["username"]);
                        usuario.Clave = Convert.ToString(lector["clave"]);
                        usuario.Telefono = Convert.ToString(lector["Telefono"]);
                        lista.Add(usuario);
                    }
                }
            }

            return lista;
        }

        public Usuario ObtenerUsuario(string username)
        {
            Usuario usuario = new Usuario();
            string query = "SELECT * FROM USUARIO WHERE USERNAME=@pr1";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",username)
             };

            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query,dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    
                    while (lector.Read())
                    {
                        usuario.Id = Convert.ToInt32(lector["idUsuario"]);
                        usuario.Nombres = Convert.ToString(lector["Nombres"]);
                        usuario.ApPaterno = Convert.ToString(lector["apPaterno"]);
                        usuario.ApMaterno = Convert.ToString(lector["apMaterno"]);
                        usuario.Dni = Convert.ToString(lector["DNI"]);
                        usuario.Email = Convert.ToString(lector["EMail"]);
                        usuario.TipoUsuario = Convert.ToString(lector["TipoUsuario"]);
                        usuario.Username = Convert.ToString(lector["username"]);
                        usuario.Clave = Convert.ToString(lector["clave"]);
                        usuario.Telefono = Convert.ToString(lector["Telefono"]);
                    }
                }
            }
            return usuario;
        }
    }
}