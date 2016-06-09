using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;

namespace Canchita.PruebasUnitarias
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void AgregarUsuario()
        {
            Usuario usuario = new Usuario()
            {
                Nombres = "Yovanny Jhon",
                ApPaterno = "Zeballos",
                ApMaterno = "Medina",
                Dni = "70123396",
                Email = "yovanny_jzm@hotmail.com",
                Telefono = "943823186",
                TipoUsuario = "Administrador",
                Username = "yovannyZ",
                Clave = "123"
            };

            TransaccionClient proxy = new TransaccionClient();
            Assert.IsTrue(proxy.Agregar(usuario));
        }
    }
}
