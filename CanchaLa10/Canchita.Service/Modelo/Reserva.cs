using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canchita.Service.Modelo
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }
        public Campo campo { get; set; }
        public Usuario usuario { get; set; }
        public double Monto { get; set; }
        public string Estado { get; set; }
    }
}