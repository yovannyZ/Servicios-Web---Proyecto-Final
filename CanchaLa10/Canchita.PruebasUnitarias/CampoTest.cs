using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;


namespace Canchita.PruebasUnitarias
{
   
    [TestClass]
    public class CampoTest
    {
        TransaccionClient proxy = new TransaccionClient();



        [TestMethod]
        public void AgregarCampo()
        {
            Sede sede = new Sede() { Id = 2 };
            Campo campo = new Campo(){
                Descripcion = "campo 7 jugadores",
                Estado= "Disponible",
                Sede= sede
            };

            Assert.IsTrue(proxy.AgregarCampo(campo));
        }
        [TestMethod]

        public void ActualizarCampo()
        {
            Sede sede = new Sede() { Id = 1 };
            Campo campo = new Campo()
            {
                Id= 1,
                Descripcion = "campo 10 jugadores",
                Estado = "Disponible",
                Sede = sede
            };

            Assert.IsTrue(proxy.ActualizarCampo(campo));

        }

        [TestMethod]
        public void EliminarCampo()
        {
            Campo campo = new Campo()
            {
                Id = 1
            };
            Assert.IsTrue(proxy.EliminarCampo(campo));

        }

        [TestMethod]
        public void ListarCampos()
        {
            List<Campo> lista = new List<Campo>();
            lista = proxy.ListarCampos();
            Assert.IsTrue(lista.Count > 0);
        }

    }
}
