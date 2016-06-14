using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canchita.Service.Modelo
{
    public class Tarifa 
    {
        public int Id { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public double Precio { get; set; }
        public bool Checked { get; set; }
     
    }
}