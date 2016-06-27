using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canchita.Service.Modelo
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string TipoUsuario { get; set; }
        public string Username { get; set; }
        public string Clave { get; set; }
        public string Estado { get; set; }
        public byte[] Imagen { get; set; }
        
    }
}