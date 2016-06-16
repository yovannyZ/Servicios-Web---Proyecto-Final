using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;
using System.Collections.Generic;

namespace Canchita.PruebasUnitarias
{
    [TestClass]
    public class TarifaTest
    {
        TransaccionClient proxy = new TransaccionClient();

        [TestMethod]
        public void ListarTarifas()
        {
            DateTime dia =  DateTime.Today;
            List<Tarifa> lista = proxy.ListarTarifas(dia);
            Assert.IsTrue(lista.Count > 0);
        }
    }
}
