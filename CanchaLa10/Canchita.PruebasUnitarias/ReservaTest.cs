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
       
            Tarifa tarifa = new Tarifa { Id = 3 };
            DetalleReserva dt = new DetalleReserva
            {
                Tarifa = tarifa,
                HoraInicio = "10:00",
                HoraFin = "11:00",
                Precio = 80
            };

            List<DetalleReserva> detalles = new List<DetalleReserva>();
            detalles.Add(dt);

            Campo campo = new Campo { Id = 1 };
            Usuario usuario = new Usuario { Id = 8 };
            DateTime dia = DateTime.Today;
            Reserva reserva = new Reserva();
            reserva.FechaReserva = dia;
            reserva.campo = campo;
            reserva.usuario = usuario;
            reserva.Estado = "Pendiente";
            reserva.Monto = 80;
            Assert.IsTrue(proxy.AgregarReserva(reserva, detalles));
        }
       
    }
}
