
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
            string query = "INSERT INTO USUARIO  VALUES(@pr1,@pr2,@pr3,@pr4,@pr5,@pr6,@pr7,@pr8)";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",usuario.Nombres),
                 DBHelper.MakeParam("@pr2",usuario.Apellidos),
                 DBHelper.MakeParam("@pr3",usuario.Email),
                 DBHelper.MakeParam("@pr4",usuario.TipoUsuario),
                 DBHelper.MakeParam("@pr5",usuario.Username),
                 DBHelper.MakeParam("@pr6",usuario.Clave),
                 DBHelper.MakeParam("@pr7","Activo"),
                 DBHelper.MakeParam("@pr8",usuario.Imagen==null?new byte[]{}:usuario.Imagen)

             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams)>0;
            return exito;
        }

        public bool Actualizar(Usuario usuario)
        {
            bool exito = false;
            string query = "UPDATE USUARIO SET Nombres=@pr1,"+
                                                   "Apellidos=@pr2,"+
                                                   "EMail=@pr3,"+
                                                   "TipoUsuario=@pr4,"+
                                                   "username=@pr5,"+
                                                   "clave=@pr6 , Estado=@pr7 , Imagen=@pr8 where idUsuario=@pr9 ";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",usuario.Nombres),
                 DBHelper.MakeParam("@pr2",usuario.Apellidos),
                 DBHelper.MakeParam("@pr3",usuario.Email),
                 DBHelper.MakeParam("@pr4",usuario.TipoUsuario),
                 DBHelper.MakeParam("@pr5",usuario.Username),
                 DBHelper.MakeParam("@pr6",usuario.Clave),
                 DBHelper.MakeParam("@pr7",usuario.Estado),
                  DBHelper.MakeParam("@pr8",usuario.Imagen),
                  DBHelper.MakeParam("@pr9",usuario.Id)

             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public bool ActualizarSinImagen(Usuario usuario)
        {
            bool exito = false;
            string query = "UPDATE USUARIO SET Nombres=@pr1," +
                                                   "Apellidos=@pr2," +
                                                   "EMail=@pr3," +
                                                   "TipoUsuario=@pr4," +
                                                   "username=@pr5," +
                                                   "clave=@pr6 , Estado=@pr7 where idUsuario=@pr8 ";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",usuario.Nombres),
                 DBHelper.MakeParam("@pr2",usuario.Apellidos),
                 DBHelper.MakeParam("@pr3",usuario.Email),
                 DBHelper.MakeParam("@pr4",usuario.TipoUsuario),
                 DBHelper.MakeParam("@pr5",usuario.Username),
                 DBHelper.MakeParam("@pr6",usuario.Clave),
                 DBHelper.MakeParam("@pr7",usuario.Estado),
                 DBHelper.MakeParam("@pr8",usuario.Id)

             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;
            return exito;
        }

        public bool Eliminar(Usuario usuario)
        {
            bool exito = false;
            string query = "UPDATE USUARIO SET Estado=@pr1 where idUsuario=@pr2 ";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1","Inactivo"),
                 DBHelper.MakeParam("@pr2",usuario.Id)

             };
            exito = DBHelper.ExecuteNonQuery(query, dbParams) > 0;

            return exito;

        }
        public List<Usuario> ListarUsuarios()
        {
           
            List<Usuario> lista= new List<Usuario>();
            string query = "SELECT * FROM Usuario where Estado='Activo'";
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query)) { 
            if (lector != null && lector.HasRows)
                {
                    Usuario usuario;
                    while (lector.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(lector["idUsuario"]);
                        usuario.Nombres = Convert.ToString(lector["Nombres"]);
                        usuario.Apellidos = Convert.ToString(lector["Apellidos"]);
                        usuario.Email = Convert.ToString(lector["EMail"]);
                        usuario.TipoUsuario = Convert.ToString(lector["TipoUsuario"]);
                        usuario.Username = Convert.ToString(lector["username"]);
                        usuario.Clave = Convert.ToString(lector["clave"]);
                        usuario.Estado = Convert.ToString(lector["Estado"]);
                        var img = lector["Imagen"];
                        if (img is System.DBNull)
                        {
                            usuario.Imagen = null;
                        }
                        else
                        {
                            usuario.Imagen = (byte[])img;
                        }
                       
                        lista.Add(usuario);
                    }
                }
            }

            return lista;
        }

        public Usuario ObtenerUsuario(string username,string tipoUsuario)
        {
            Usuario usuario = null;
            string query;
            SqlParameter[] dbParams;

            if (tipoUsuario != null)
            {
                query = "SELECT * FROM USUARIO WHERE USERNAME=@pr1 and TipoUsuario=@pr2";

                 dbParams = new SqlParameter[]
                {
                 DBHelper.MakeParam("@pr1",username),
                 DBHelper.MakeParam("@pr2",tipoUsuario)
                 };

            }
            else
            {
                query = "SELECT * FROM USUARIO WHERE USERNAME=@pr1";

                dbParams = new SqlParameter[]
                {
                 DBHelper.MakeParam("@pr1",username)
                
                 };
            }
            
            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query,dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    usuario = new Usuario();
                    while (lector.Read())
                    {
                        usuario.Id = Convert.ToInt32(lector["idUsuario"]);
                        usuario.Nombres = Convert.ToString(lector["Nombres"]);
                        usuario.Apellidos = Convert.ToString(lector["Apellidos"]);
                        usuario.Email = Convert.ToString(lector["EMail"]);
                        usuario.TipoUsuario = Convert.ToString(lector["TipoUsuario"]);
                        usuario.Username = Convert.ToString(lector["username"]);
                        usuario.Clave = Convert.ToString(lector["clave"]);
                        usuario.Estado = Convert.ToString(lector["Estado"]);
                        var img = lector["Imagen"];
                        if (img is System.DBNull)
                        {
                            usuario.Imagen = null;
                        }
                        else
                        {
                            usuario.Imagen = (byte[])img;
                        }
                    }
                }
            }
            return usuario;
        }

        public Usuario ObtenerUsuarioId(int id)
        {
            Usuario usuario = null;
            string query = "SELECT * FROM USUARIO WHERE idUsuario=@pr1";

            SqlParameter[] dbParams = new SqlParameter[]
             {
                 DBHelper.MakeParam("@pr1",id)
             };

            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    usuario = new Usuario();
                    while (lector.Read())
                    {
                        usuario.Id = Convert.ToInt32(lector["idUsuario"]);
                        usuario.Nombres = Convert.ToString(lector["Nombres"]);
                        usuario.Apellidos = Convert.ToString(lector["Apellidos"]);
                        usuario.Email = Convert.ToString(lector["EMail"]);
                        usuario.TipoUsuario = Convert.ToString(lector["TipoUsuario"]);
                        usuario.Username = Convert.ToString(lector["username"]);
                        usuario.Clave = Convert.ToString(lector["clave"]);
                        usuario.Estado = Convert.ToString(lector["Estado"]);
                        var img = lector["Imagen"];
                        if (img is System.DBNull)
                        {
                            usuario.Imagen = null;
                        }
                        else
                        {
                            usuario.Imagen = (byte[])img;
                        }
                    }
                }
            }
            return usuario;
        }

        public Usuario devolverUseryContra(string email)
        {
            Usuario usuario = null;
            string query = "SELECT * FROM USUARIO WHERE  email=@email";

            SqlParameter[] dbParams = new SqlParameter[]
             {                
                 DBHelper.MakeParam("@email",email)
             };

            using (SqlDataReader lector = DBHelper.ExecuteDataReader(query, dbParams))
            {
                if (lector != null && lector.HasRows)
                {
                    usuario = new Usuario();
                    while (lector.Read())
                    {
                        usuario.Id = Convert.ToInt32(lector["idUsuario"]);
                        usuario.Nombres = Convert.ToString(lector["Nombres"]);
                        usuario.Apellidos = Convert.ToString(lector["Apellidos"]);
                        usuario.Email = Convert.ToString(lector["EMail"]);
                        usuario.TipoUsuario = Convert.ToString(lector["TipoUsuario"]);
                        usuario.Username = Convert.ToString(lector["username"]);
                        usuario.Clave = Convert.ToString(lector["clave"]);
                     
                    }
                }
            }
            return usuario;
        }

        public string recuperarCuenta(string email, string apellidos, string nombre, string username, string clave)
        {
            string mensaje = "";
            System.Net.Mail.MailMessage msj = new System.Net.Mail.MailMessage();
            msj.To.Add(email);
            //Asunto
            msj.Subject = "Recuperación de Usuario y Contraseña - Campos Deportivos 'LA 10' ";
            msj.SubjectEncoding = System.Text.Encoding.UTF8;
            //Cuerpo del Mensaje
            /*  msj.Body = "Gracias por user el sistema de recuperación de canchita la 10"+ Environment.NewLine+
            "Estimado:  "+usuClave.Apellidos+" ,   " + usuClave.Nombres +Environment.NewLine+ "Su contraseña olvidada es:  "+
            " " + usuClave.Clave + Environment.NewLine + "Estamos para ayudarte." + Environment.NewLine +"Atte. Soporte LA 10";*/
            msj.Body = "<br /> <br /> <h2 style='color:2D3C6B'>Gracias por utilizar el Servicio de Recuperación de Usuarios y  Contraseñas de Canchita la 10 </h2> <br />  <br />  <br />" +
            "Estimado: <b style='color:blue'>" + apellidos + " </b>,  <b style='color:blue'>" + nombre + " </b> <br />Su Usuario olvidado es:  " +
            "<b style='color:green'> " + username + " </b>  y su Contraseña olvidada es:  <b style='color:green'> " + clave + " </b> </b> <br /> <br /> <br /> <h4 style='color:2D3C6B'>Estamos para ayudarte. <br /> Atte. Soporte LA 10 </h4>";
            msj.BodyEncoding = System.Text.Encoding.UTF8;
            msj.IsBodyHtml = true;
            msj.From = new System.Net.Mail.MailAddress("campoladiez@gmail.com");
            //cliente de correo:
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            cliente.Credentials =
            new System.Net.NetworkCredential("campoladiez@gmail.com", "@mipassword@123");
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com";
            try
            {
                cliente.Send(msj);
                mensaje = "Enviado";
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                mensaje = "Error";
            }

            return mensaje;
        }


    }
}