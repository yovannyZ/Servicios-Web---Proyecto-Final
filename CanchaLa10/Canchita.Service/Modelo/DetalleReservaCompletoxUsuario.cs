using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canchita.Service.Modelo
{
    public class DetalleReservaCompletoxUsuario
    {
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
        public double tarifa { get; set; }
        public string DescripcionCampo { get; set; }
        public byte[] imagencampo { get; set; }
        public string DescripcionSede { get; set; }
        public string DireccionSede { get; set; }
    }
}