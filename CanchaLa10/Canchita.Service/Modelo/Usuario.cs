using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanchaLa10.Service.Modelo
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string TipoUsuario { get; set; }
        public string Username { get; set; }
        public string Clave { get; set; }
        
    }
}