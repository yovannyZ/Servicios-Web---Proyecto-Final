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
           
                Usuario usuario= new Usuario { Id = 4 };
                Tarjeta tarjeta  = new Tarjeta();
                tarjeta.idTarjeta = "1941250003";
                tarjeta.fechaCreacion=DateTime.Parse("2016/05/01");
                tarjeta.fechaVencimiento=DateTime.Parse("2020/05/01");
                tarjeta.usuario = usuario;
                Assert.IsTrue(proxy.crearTarjetas(tarjeta));                
        }

        [TestMethod]
        public void abonarTarjeta()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.idTarjeta = "1941250003";
            tarjeta.saldo = 150.0;

            Assert.IsTrue(proxy.abonarSaldo(tarjeta));
         
        }

        [TestMethod]
        public void listarTarjetas()
        {
            var lista = proxy.listarTarjetas();
            Assert.IsNotNull(lista);
          
        }

        [TestMethod]
        public void obtenerTarjetasXUsuario()
        {
            int idUsuario = 3;
            var lista = proxy.ObtenerTarjetasXUsuario(idUsuario);
            Assert.IsNotNull(lista);

        }


        [TestMethod]
        public void obtenerTarjeta()
        {
            string idTarjeta = "1941250003";
            var tarjeta = proxy.obtenerTarjeta(idTarjeta);
            Assert.IsNotNull(tarjeta);

        }

    }
}
