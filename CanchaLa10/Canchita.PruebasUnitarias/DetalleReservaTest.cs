using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;

namespace Canchita.PruebasUnitarias
{
    [TestClass]
    public class DetalleReservaTest
    {
        TransaccionClient proxy = new TransaccionClient();
        [TestMethod]
        public void Agregar()
        {
            Tarifa tarifa = new Tarifa { Id = 3 };
            Reserva reserva = new Reserva { Id = 1 };
            DetalleReserva dt = new DetalleReserva
            { 
                Reserva=reserva,
                Tarifa=tarifa,
                HoraInicio="10:00",
                HoraFin = "11:00",
                Precio=80
            };
            Assert.IsTrue(proxy.AgregarDetalleReserva(dt));

        }
    }
}
