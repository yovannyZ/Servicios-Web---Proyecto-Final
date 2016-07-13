using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;

namespace Canchita.PruebasUnitarias
{
    [TestClass]
    public class UsuarioTest
    {
        TransaccionClient proxy = new TransaccionClient();

        [TestMethod]
        public void AgregarUsuario()
        {
            Usuario usuario = new Usuario()
            {
                Nombres = "Armando",
                Apellidos = "Carbajal",          
                Email = "Armandez@hotmail.com",
                TipoUsuario = "Cliente",
                Username = "elcliente",
                Clave = "abc"
            };

           
            Assert.IsTrue(proxy.AgregarUsuario(usuario));
        }

        [TestMethod]
        public void ActulizarUsuario()
        {
            Usuario usuario = new Usuario()
            {
                Id=1,
                Nombres = "Armando",
                Apellidos = "Carbajal",
                Email = "Armandez@hotmail.com",
                TipoUsuario = "Cliente",
                Username = "elcliente",
                Clave = "abc"
            };


            Assert.IsTrue(proxy.ActualizarUsuario(usuario));
        }

         [TestMethod]
        public void ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            lista = proxy.ListarUsuario();
            Assert.IsTrue(lista.Count > 0);

        }

         [TestMethod]
         public void ListarUsuariosRest()
         {
             var webClient = new WebClient();
             var json = webClient.DownloadString("http://localhost:1557/ServicioRest.svc/Usuarios");
             var js = new JavaScriptSerializer();
           //  var usuario = js.Deserialize<Usuario>(json);
             var lista = js.Deserialize<List<Usuario>>(json);
             Assert.AreNotEqual(0, lista);

         }

         [TestMethod]
         public void ValidarUsuario()
         {
             Usuario usuario = new Usuario()
             {
                 Username = "acarbajal",
                 Clave = "321"
             };

             Assert.IsNotNull(proxy.ValidarUsuario(usuario));
         }
    }
}
