using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Canchita.Service.Data;

namespace Canchita.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IServicioRest
    {

        UsuarioDAO usuDao = new UsuarioDAO();
        public List<Modelo.Usuario> listarUsuario()
        {
            return usuDao.ListarUsuarios();
        }
    }
}
