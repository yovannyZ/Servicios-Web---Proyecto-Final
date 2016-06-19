using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canchita.PruebasUnitarias.TransaccionWS;


namespace Canchita.PruebasUnitarias
{
    [TestClass]
    public class TarjetaTest
    {

        TransaccionClient proxy = new TransaccionClient();
        [TestMethod]
        public void crearTarjeta()
        {
           
                Usuario usuario= new Usuario { Id = 6 };
                Tarjeta tarjeta  = new Tarjeta();
                tarjeta.idTarjeta = "1941250005";
                tarjeta.fechaCreacion=DateTime.Parse("2016/06/16");
                tarjeta.fechaVencimiento=DateTime.Parse("2022/06/14");
                tarjeta.usuario = usuario;
                Assert.IsTrue(proxy.crearTarjetas(tarjeta));                
        }

        [TestMethod]
        public void RecargarTarjeta()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.idTarjeta = "1941250005";
            tarjeta.saldo =1200.50;

            Assert.IsTrue(proxy.abonarSaldo(tarjeta));
         
        }

        [TestMethod]
        public void listarTarjetas()
        {
            var lista = proxy.listarTarjetas();
            Assert.IsTrue(lista.Count>0);
          
        }

        [TestMethod]
        public void obtenerTarjetasXUsuario()
        {
            int idUsuario = 6;
            var lista = proxy.ObtenerTarjetasXUsuario(idUsuario);
            Assert.IsNotNull(lista);

        }


        [TestMethod]
        public void obtenerTarjeta()
        {
            string idTarjeta = "1941250005";
            var tarjeta = proxy.obtenerTarjeta(idTarjeta);
            Assert.IsNotNull(tarjeta);

        }

    }
}
