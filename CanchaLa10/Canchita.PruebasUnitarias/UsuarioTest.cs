using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;
using System.Collections.Generic;

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
                ApPaterno = "Carbajal",
                ApMaterno = "Carbajal",
                Dni = "70123394",
                Email = "Armandez@hotmail.com",
                Telefono = "943823186",
                TipoUsuario = "Administrador",
                Username = "acarbajal",
                Clave = "123"
            };

           
            Assert.IsTrue(proxy.AgregarUsuario(usuario));
        }

        [TestMethod]
        public void ActulizarUsuario()
        {
            Usuario usuario = new Usuario()
            {
                Id=10,
                Nombres = "Armando",
                ApPaterno = "Carbajal",
                ApMaterno = "Carbajal",
                Dni = "70123394",
                Email = "Armandez@hotmail.com",
                Telefono = "943823186",
                TipoUsuario = "Administrador",
                Username = "acarbajal",
                Clave = "321"
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
         public void ValidarUsuario()
         {
             Usuario usuario = new Usuario()
             {
                 Username = "acarbajal",
                 Clave = "321"
             };

             Assert.IsTrue(proxy.ValidarUsuario(usuario));
         }
    }
}
