using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;


namespace Canchita.PruebasUnitarias
{
   
    [TestClass]
    public class SedeTest
    {

        TransaccionClient proxy = new TransaccionClient();

        [TestMethod]
        public void AgregarSede()
        {
            Sede sede = new Sede()
            {
                Descripcion= "Sede Lince",
                Direccion= "av. lince 345",
                Estado="Disponible"

            };

            Assert.IsTrue(proxy.AgregarSede(sede));


        }

        [TestMethod]
        public void ActualizarSede()
        {
            Sede sede = new Sede()
            {
                Id=1,
                Descripcion = "Sede chorrillos",
                Direccion = "av. chorrillos 345",
                Estado = "Disponible"

            };
            Assert.IsTrue(proxy.ActualizarSede(sede));
        }

        [TestMethod]
        public void EliminarSede()
        {
            Sede sede = new Sede()
            {
                Id = 2
            };
            Assert.IsTrue(proxy.EliminarSede(sede));
        }

        [TestMethod]
        public void ListarSedes()
        {
            List<Sede> lista = new List<Sede>();
            lista = proxy.ListarSedes();
            Assert.IsNotNull(lista);
        }

    }
}
