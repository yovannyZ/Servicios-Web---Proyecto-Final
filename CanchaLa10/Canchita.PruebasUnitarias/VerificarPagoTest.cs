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
            var listado = proxy.listaParaEliminar();
            if (listado.Count > 0)
            {
                foreach (var item in listado)
                {
                    if (item.diferencia >= 2)
                    {
                        proxy.eliminarDetalleReserva(item.Id);
                        proxy.eliminarPago(item.Id);
                        proxy.eliminarReserva(item.Id);
                    }
                }
                Assert.IsTrue(listado.Count > 0);
            }
            else
            {
                Assert.IsTrue(listado.Count <= 0);
            }
           
            
        }
    }
}
