using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canchita.Service.Modelo
{
    public class Pago
    {
        public string nroPago { get; set; }
        public double monto { get; set; }
        public string estado { get; set; }
        public Reserva idReserva { get; set; }

    }
}