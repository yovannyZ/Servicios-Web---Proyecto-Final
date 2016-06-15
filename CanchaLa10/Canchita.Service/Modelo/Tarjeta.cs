using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canchita.Service.Modelo
{
    public class Tarjeta
    {
        public string idTarjeta { get; set; }
        public double saldo { get; set; }
        public string estado { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public Usuario usuario { get; set; }

    }
}