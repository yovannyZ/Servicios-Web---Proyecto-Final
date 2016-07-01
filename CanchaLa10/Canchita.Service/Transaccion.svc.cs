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

        #region . DAO .
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
        #endregion

        #region . USUARIO .
        public bool AgregarUsuario(Usuario usuario)
        {
            return UsuarioDAO.Agregar(usuario);
        }

        public Usuario ValidarUsuario(Usuario usuario)
        {
            Usuario usu = UsuarioDAO.ObtenerUsuario(usuario.Username,usuario.TipoUsuario);

            if (usu != null)
            {
                if (usuario.TipoUsuario != null)
                {
                    if (usu.Clave.Equals(usuario.Clave))
                    {
                        return usu;
                    }
                    else
                    {
                        return usu = null;
                    }
                }
                else
                {
                    return usu;
                }
                
            }
            else
            {
                return usu;
            }
        }

        public List<Usuario> ListarUsuario()
        {
            return UsuarioDAO.ListarUsuarios();
        }


        public bool ActualizarUsuario(Usuario usuario)
        {
            if(usuario.Imagen==null)
                return UsuarioDAO.ActualizarSinImagen(usuario);
            else
                return UsuarioDAO.Actualizar(usuario);
        }

        public Usuario ObtenerUsuarioId(int id)
        {
            return UsuarioDAO.ObtenerUsuarioId(id);
        }

        public bool EmininarUsuario(Usuario usuario)
        {
            return UsuarioDAO.Eliminar(usuario);
        }

        public Usuario devolverUseryContra(Usuario usuario)
        {
            return UsuarioDAO.devolverUseryContra(usuario.Email);
        }

        public string mandarCorreo(Usuario usuario)
        {
            return UsuarioDAO.recuperarCuenta(usuario.Email, usuario.Apellidos, usuario.Nombres, usuario.Username, usuario.Clave);
        }
       
        #endregion

        #region . TARIFA .
        public List<Tarifa> ListarTarifas(DateTime fechaReserva, int idCampo)
        {
            return TarifaDAO.Listar(fechaReserva, idCampo);
        }

        public bool AgregarTarifa(Tarifa tarifa)
        {
            return TarifaDAO.Agregar(tarifa);
        }

        public bool EliminarTarifa(Tarifa tarifa)
        {
            return TarifaDAO.Eliminar(tarifa);
        }

        public bool ActualizarTarifa(Tarifa tarifa)
        {
            return TarifaDAO.Actulizar(tarifa);
        }

        public List<Tarifa> ListarTarifasAdmin()
        {
            return TarifaDAO.ListarTarifas();
        }

        public Tarifa ObtenerTarifaxI(int id)
        {
            return TarifaDAO.ObtenerTarifaxId(id);
        }
        #endregion

        #region . RESERVA .
        public bool AgregarReserva(Reserva reserva, List<DetalleReserva> listaDetalle)
        {
            bool exito = false;
            if (ReservaDAO.Agregar(reserva))
            {
                reserva.Id = ReservaDAO.ObtenerIdUltimaReserva();
                foreach (var detalle in listaDetalle)
                {
                    detalle.Reserva = reserva;
                    DetalleReservaDAO.Agregar(detalle);
                }
                exito = true;
            }
            return exito;
        }
        public double retornarMontoReserva(int idReserva)
        {
            return ReservaDAO.retornarmonto(idReserva);
        }

        public int obtenerUltimoIDReseva()
        {
            return ReservaDAO.ObtenerIdUltimaReserva();
        }

        public List<Reserva> listarReservaXUsuario(int idUsuario)
        {
            return ReservaDAO.ListarReservaXUsuario(idUsuario);
        }
        public List<Reserva> ListarReservas()
        {
            return ReservaDAO.ListadoReservas();
        }
        #endregion

        #region . TARJETA .
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
        #endregion
        
        #region . PAGO .
        public bool pagarReservaConTarjeta(Pago pago, string nroTarjeta, int idReserva)
        {
            return PAGODAO.pagarconTarjeta(pago,nroTarjeta,idReserva);
        }
        public List<Pago> listarPagosPendientes()
        {
            return PAGODAO.listarPagosPendientes();
        }
        public List<Pago> listarPagosCancelados()
        {
            return PAGODAO.listarPagosCancelados();
        }
        public bool reservarPagoEfectivo(Pago pago, int idReserva)
        {
            return PAGODAO.reservarPagoEfectivo(pago,idReserva);
        }
        public bool pagarReservaConEfectivo(string nroPago)
        {
            return PAGODAO.pagarConEfectivo(nroPago);
        }

        #endregion

        #region . SEDE .
        public bool AgregarSede(Sede sede)
        {
            return SedeDAO.Agregar(sede);
        }

        public bool ActualizarSede(Sede sede)
        {
            if (sede.Imagen == null)
                return SedeDAO.ActualizarSinImagen(sede);
            else
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

        public Sede ObtenerSedeId(int idSede)
        {
            return SedeDAO.ObtenerSedeId(idSede);
        }
        #endregion

        #region . CAMPO .
        public bool AgregarCampo(Campo campo)
        {
            return CampoDAO.Agregar(campo);
        }

        public bool ActualizarCampo(Campo campo)
        {
            if (campo.Imagen==null)
                return CampoDAO.ActualizarSinImagen(campo);
            else
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

        public List<Campo> ObtenerCamposXSede(int idSede)
        {
            return CampoDAO.ObtenerCamposXSede(idSede);
        }
        public Campo ObtenerCamposXId(int idCampo)
        {
            return CampoDAO.ObtenerCamposXId(idCampo);
        }

        #endregion

        #region . DETALLERESERVA .

        public List<DetalleReservaCompletoxUsuario> verDetalleReserva(int idReserva)
        {
            return DetalleReservaDAO.VerDetalleXReserva(idReserva);
        }
        public List<DetalleReserva> listarDetalleXReserva(int IdReserva)
        {
            return DetalleReservaDAO.listarDetalleXReserva(IdReserva);
        }
        #endregion



    }
}
