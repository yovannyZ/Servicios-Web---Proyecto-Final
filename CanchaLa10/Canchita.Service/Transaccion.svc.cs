using Canchita.Service.Modelo;
using Canchita.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Canchita.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Transaccion" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Transaccion.svc o Transaccion.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Transaccion : ITransaccion
    {
        private UsuarioDAO usuarioDAO;
        private TarifaDAO tarifaDAO;
        private ReservaDAO reservaDAO;
        private DetalleReservaDAO detalleReservaDAO;
        private SedeDAO sedeDAO;

        private SedeDAO SedeDAO
        {
            get
            {
                if (sedeDAO == null)
                    sedeDAO = new SedeDAO();
                return sedeDAO;
            }
        }
        

        private UsuarioDAO UsuarioDAO
        {
            get
            {
                if (usuarioDAO == null)
                    usuarioDAO = new UsuarioDAO();
                return usuarioDAO;
            }
        }
        private TarifaDAO TarifaDAO
        {
            get
            {
                if (tarifaDAO == null)
                    tarifaDAO = new TarifaDAO();
                return tarifaDAO;
            }
        }
        private ReservaDAO ReservaDAO
        {
            get
            {
                if (reservaDAO == null)
                    reservaDAO = new ReservaDAO();
                return reservaDAO;
            }
        }

        private DetalleReservaDAO DetalleReservaDAO
        {
            get
            {
                if (detalleReservaDAO == null)
                    detalleReservaDAO = new DetalleReservaDAO();
                return detalleReservaDAO;
            }
        }

        public bool AgregarUsuario(Usuario usuario)
        {
            return UsuarioDAO.Agregar(usuario);
        }

        public bool ValidarUsuario(Usuario usuario)
        {
            bool isValid = false;
            var usu = UsuarioDAO.ObtenerUsuario(usuario.Username);
            if (usu != null)
            {
                if (usu.Clave.Equals(usuario.Clave))
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        public List<Usuario> ListarUsuario()
        {
            return UsuarioDAO.ListarUsuarios();
        }


        public bool ActualizarUsuario(Usuario usuario)
        {
            return UsuarioDAO.Actualizar(usuario);
        }


        public List<Tarifa> ListarTarifas(DateTime fechaReserva)
        {
            return TarifaDAO.Listar(fechaReserva);
        }


        public bool AgregarReserva(Reserva reserva)
        {
            return ReservaDAO.Agregar(reserva);
        }


        public bool AgregarDetalleReserva(DetalleReserva dtReserva)
        {
            return DetalleReservaDAO.Agregar(dtReserva);
        }


        public int ObtenerIdUltimaReserva()
        {
            return ReservaDAO.ObtenerIdUltimaReserva();
        }


        public bool AgregarSede(Sede sede)
        {
            throw new NotImplementedException();
        }

        public bool ActualizarSede(Sede sede)
        {
            throw new NotImplementedException();
        }

        public List<Sede> ListarSede()
        {
            throw new NotImplementedException();
        }

        public bool EliminarSede(Sede sede)
        {
            throw new NotImplementedException();
        }
    }
}
