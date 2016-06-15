using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;

namespace Canchita.PruebasUnitarias
{
 
    [TestClass]
    public class ReservaTest
    {
        TransaccionClient proxy = new TransaccionClient();
      
        [TestMethod]
        public void Agregar()
        {
       
            Campo campo = new Campo { Id = 1 };
            Usuario usuario = new Usuario { Id = 3};
            DateTime dia = DateTime.Today;
            Reserva reserva = new Reserva();
            reserva.FechaReserva = dia;
            reserva.campo= campo;
            reserva.usuario = usuario;
            reserva.Estado = "Pendiente";
            reserva.Monto = 80;
            Assert.IsTrue(proxy.AgregarReserva(reserva));
        }

        [TestMethod]
        public void ObtenerIdUltimaReserva()
        {
            int idreserva = proxy.ObtenerIdUltimaReserva();
            Assert.IsTrue(idreserva > 0);
        }

        [TestMethod]
        public void retornarMonto()
        {   
            int idRes=10;
            double monto = proxy.retornarMontoReserva(idRes);
            Assert.IsNotNull(monto);
        }
    }
}
