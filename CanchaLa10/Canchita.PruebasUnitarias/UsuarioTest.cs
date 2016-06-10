﻿using System;
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
                Nombres = "Yovanny Jhon",
                ApPaterno = "Zeballos",
                ApMaterno = "Medina",
                Dni = "70123395",
                Email = "yovanny_jzm@hotmail.com",
                Telefono = "943823186",
                TipoUsuario = "Administrador",
                Username = "yovannyZeballos",
                Clave = "123"
            };

           
            Assert.IsTrue(proxy.Agregar(usuario));
        }

        [TestMethod]
        public void ActulizarUsuario()
        {
            Usuario usuario = new Usuario()
            {
                Id=3,
                Nombres = "Yovanny Zeballos",
                ApPaterno = "Zeballos",
                ApMaterno = "Medina",
                Dni = "70123395",
                Email = "yovanny_jzm@hotmail.com",
                Telefono = "943823186",
                TipoUsuario = "Administrador",
                Username = "Zeballos",
                Clave = "123"
            };


            Assert.IsTrue(proxy.Actualizar(usuario));
        }

         [TestMethod]
        public void ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            lista = proxy.Listar();
            Assert.IsNotNull(lista);

        }
    }
}
