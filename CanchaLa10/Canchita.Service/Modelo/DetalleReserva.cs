using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canchita.Service.Modelo
{
    public class DetalleReserva
    {
        public Reserva Reserva { get; set; }
        public Tarifa Tarifa { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public double Precio { get; set; }
    }
}