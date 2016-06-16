using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;
namespace Canchita.PruebasUnitarias
{
    [TestClass]
    public class PagoTest
    {
        TransaccionClient proxy = new TransaccionClient();
        [TestMethod]
        public void pagarCTarjeta()
        {

            string idTarjeta = "1941250003";
            int idReserva = 33;
            Pago pago = new Pago();
            pago.nroPago = "P0001";

            Assert.IsTrue(proxy.pagarReservaCTarjeta(pago,idTarjeta,idReserva));
        }


        [TestMethod]
        public void listarPendientes()
        {
            var lista = proxy.listarPagosPendientes();
            Assert.IsTrue(lista.Count>0);
        }

        [TestMethod]
        public void listarCancelados()
        {
            var lista = proxy.listarPagosCancelados();
            Assert.IsTrue(lista.Count>0);
        }

    }
}
