﻿using Canchita.Service.Modelo;
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
        [OperationContract]
        bool AgregarUsuario(Usuario usuario);

        [OperationContract]
        bool ActualizarUsuario(Usuario usuario);

        [OperationContract]
        List<Usuario> ListarUsuario();
        
        [OperationContract]
        List<Tarifa> ListarTarifas(DateTime fechaReserva);

        [OperationContract]
        bool AgregarReserva(Reserva reserva);

        [OperationContract]
        bool AgregarDetalleReserva(DetalleReserva dtReserva);

        [OperationContract]
        int ObtenerIdUltimaReserva();

        [OperationContract]
        bool ValidarUsuario(Usuario usuario);
        
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

        [OperationContract]
        double retornarMontoReserva(int idReserva);

        [OperationContract]
        bool pagarReservaCTarjeta(Pago pago,string nroTarjeta,int idReserva);

        [OperationContract]
        List<Pago> listarPagosPendientes();

        [OperationContract]
        List<Pago> listarPagosCancelados();

        [OperationContract]
        bool AgregarSede(Sede sede);

        [OperationContract]
        bool ActualizarSede(Sede sede);

        [OperationContract]
        List<Sede> ListarSedes();

        [OperationContract]
        bool EliminarSede(Sede sede);

        [OperationContract]
        bool AgregarCampo(Campo campo);

        [OperationContract]
        bool ActualizarCampo(Campo campo);

        [OperationContract]
        List<Campo> ListarCampos();

        [OperationContract]
        bool EliminarCampo(Campo campo);

    }
}
