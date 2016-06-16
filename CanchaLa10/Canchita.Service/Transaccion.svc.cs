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

        private TarjetaDAO tarjetaDAO;
        private PagoDAO pagoDAO;


        private TarjetaDAO TARJETADAO
        {
            get
            {
                if (tarjetaDAO == null)
                    tarjetaDAO = new TarjetaDAO();
                return tarjetaDAO;
            }
        }

        private PagoDAO PAGODAO
        {
            get
            {
                if (pagoDAO == null)
                    pagoDAO = new PagoDAO();
                return pagoDAO;
            }
        }

        private SedeDAO sedeDAO;
        private CampoDAO campoDAO;

        private CampoDAO CampoDAO
        {
            get
            {
                if(campoDAO== null)
                    campoDAO= new CampoDAO();
                return campoDAO;
            }
        }

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

        public bool crearTarjetas(Tarjeta tarjeta)
        {
            return TARJETADAO.crearTarjeta(tarjeta);
        }


        public bool abonarSaldo(Tarjeta tarjeta)
        {
            return TARJETADAO.abonarSaldo(tarjeta);
        }


        public List<Tarjeta> listarTarjetas()
        {
            return TARJETADAO.listarTarjetas();
        }



        public List<Tarjeta> ObtenerTarjetasXUsuario(int id)
        {
            return TARJETADAO.ObtenerTarjetasXUsuario(id);
        }


        public Tarjeta obtenerTarjeta(string idTarjeta)
        {
            return TARJETADAO.obtenerTarjeta(idTarjeta);
        }


        public double retornarMontoReserva(int idReserva)
        {
            return ReservaDAO.retornarmonto(idReserva);
        }


        public bool pagarReservaCTarjeta(Pago pago, string nroTarjeta, int idReserva)
        {
            return PAGODAO.pagarcTarjeta(pago,nroTarjeta,idReserva);
        }


        public List<Pago> listarPagosPendientes()
        {
            return PAGODAO.listarPagosPendientes();
        }

        public List<Pago> listarPagosCancelados()
        {
            return PAGODAO.listarPagosCancelados();
        }

        public bool AgregarSede(Sede sede)
        {
            return SedeDAO.Agregar(sede);
        }

        public bool ActualizarSede(Sede sede)
        {
            return SedeDAO.Actualizar(sede);
        }

        public List<Sede> ListarSedes()
        {
            return SedeDAO.ListarSedes();
        }

        public bool EliminarSede(Sede sede)
        {
            return SedeDAO.Eliminar(sede);
        }


        public bool AgregarCampo(Campo campo)
        {
            return CampoDAO.Agregar(campo);
        }

        public bool ActualizarCampo(Campo campo)
        {
            return CampoDAO.Actualizar(campo);
        }

        public List<Campo> ListarCampos()
        {
            return CampoDAO.ListarCampos();
        }

        public bool EliminarCampo(Campo campo)
        {
            return CampoDAO.Eliminar(campo);

        }
    }
}
