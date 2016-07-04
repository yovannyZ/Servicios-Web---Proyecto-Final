using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;

namespace Canchita.PruebasUnitarias
{
    [TestClass]
    public class VerificarPagoTest
    {

        TransaccionClient proxy = new TransaccionClient();
        [TestMethod]
        public void verificar()
        {

            int filasAfectadas = proxy.verificar();
            if (filasAfectadas > 0)
                Assert.IsTrue(filasAfectadas >= 1);
            else
                Assert.IsTrue(filasAfectadas == 0);
            
        }
    }
}
