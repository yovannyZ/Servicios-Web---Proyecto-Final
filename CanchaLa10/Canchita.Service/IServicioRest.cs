using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Canchita.Service.Modelo;
using System.ServiceModel.Web;

namespace Canchita.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioRest
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Usuarios", ResponseFormat = WebMessageFormat.Json)]
        List<Usuario> listarUsuario();

    }
}
