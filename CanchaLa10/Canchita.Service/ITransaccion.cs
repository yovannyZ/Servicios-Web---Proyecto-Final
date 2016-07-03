using Canchita.Service.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Canchita.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ITransaccion" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ITransaccion
    {
        #region . USUARIO .
        [OperationContract]
        bool AgregarUsuario(Usuario usuario);

        [OperationContract]
        bool ActualizarUsuario(Usuario usuario);

        [OperationContract]
        List<Usuario> ListarUsuario();

        [OperationContract]
        bool EmininarUsuario(Usuario usuario);

         [OperationContract]
        Usuario ValidarUsuario(Usuario usuario);

        [OperationContract]
        Usuario ObtenerUsuarioId(int id);

        [OperationContract]
        Usuario devolverUseryContra(Usuario usuario);

        [OperationContract]
        string mandarCorreo(Usuario usuario);


        #endregion

        #region . TARIFA .
        [OperationContract]
        List<Tarifa> ListarTarifas(DateTime fechaReserva, int idCampo);

        [OperationContract]
        bool  AgregarTarifa(Tarifa tarifa);

        [OperationContract]
        bool EliminarTarifa(Tarifa tarifa);

        [OperationContract]
        bool ActualizarTarifa(Tarifa tarifa);

        [OperationContract]
        List<Tarifa> ListarTarifasAdmin();

        [OperationContract]
        Tarifa ObtenerTarifaxI( int id);
        #endregion

        #region . RESERVA .
        [OperationContract]
        bool AgregarReserva(Reserva reserva, List<DetalleReserva> listaDetalle);
        [OperationContract]
        double retornarMontoReserva(int idReserva);

        [OperationContract]
        int obtenerUltimoIDReseva();
        [OperationContract]
        List<Reserva> listarReservaXUsuario(int idUsuario);

          [OperationContract]
        List<Reserva> ListarReservas();
        [OperationContract]
        bool eliminarReserva(int idReserva);
        [OperationContract]
        List<Reserva> listaParaEliminar();
        #endregion

        #region . TARJETA .

        [OperationContract]
        bool crearTarjetas(Tarjeta tarjeta);
        [OperationContract]
        bool abonarSaldo(Tarjeta tarjeta);

        [OperationContract]
        List<Tarjeta> listarTarjetas();
        [OperationContract]
        List<Tarjeta> ObtenerTarjetasXUsuario( int id);
        [OperationContract]
        Tarjeta obtenerTarjeta(string  idTarjeta);
        #endregion

        #region . PAGO .
        [OperationContract]
        bool pagarReservaConTarjeta(Pago pago,string nroTarjeta,int idReserva);

        [OperationContract]
        List<Pago> listarPagosPendientes();

        [OperationContract]
        List<Pago> listarPagosCancelados();
        [OperationContract]
        bool reservarPagoEfectivo(Pago pago, int idReserva);
        [OperationContract]
        bool pagarReservaConEfectivo(string nroPago);

        [OperationContract]
        bool eliminarPago(int idReserva);
        #endregion

        #region . SEDE .
        [OperationContract]
        bool AgregarSede(Sede sede);

        [OperationContract]
        bool ActualizarSede(Sede sede);

        [OperationContract]
        List<Sede> ListarSedes();

        [OperationContract]
        bool EliminarSede(Sede sede);

        [OperationContract]
        Sede ObtenerSedeId(int idSede);
        #endregion

        #region . CAMPO .
        [OperationContract]
        bool AgregarCampo(Campo campo);

        [OperationContract]
        bool ActualizarCampo(Campo campo);

        [OperationContract]
        List<Campo> ListarCampos();

        [OperationContract]
        bool EliminarCampo(Campo campo);

        [OperationContract]
        List<Campo> ObtenerCamposXSede(int idSede);
        [OperationContract]
        Campo ObtenerCamposXId(int idCampo);
        #endregion

        #region .DetalleReserva.
        [OperationContract]
        List<DetalleReservaCompletoxUsuario> verDetalleReserva(int idReserva);

         [OperationContract]
        List<DetalleReserva> listarDetalleXReserva(int IdReserva);

         [OperationContract]
         bool eliminarDetalleReserva(int idReserva);
        #endregion
    }
}
