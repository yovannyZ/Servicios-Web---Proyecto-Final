using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;
namespace Canchita.PruebasUnitarias
{
    [TestClass]
    public class PagoTest
    {

        //Pruebas
        TransaccionClient proxy = new TransaccionClient();
        [TestMethod]
        public void pagarCTarjeta()
        {

            string idTarjeta = "1941250005";
            int idReserva = 1;
            Pago pago = new Pago();
            pago.nroPago = "P0001";
            
            Assert.IsTrue(proxy.pagarReservaConTarjeta(pago,idTarjeta,idReserva));
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
        [TestMethod]
        public void reservarPago()
        {           
            int idReserva = 2;
            Pago pago = new Pago();
            pago.nroPago = "P0002";
            Assert.IsTrue(proxy.reservarPagoEfectivo(pago,idReserva));
        }
        [TestMethod]
        public void pagarCEfectivo()
        {
            string nroPago = "P0002";
            Assert.IsTrue(proxy.pagarReservaConEfectivo(nroPago));
        }

    }
}
