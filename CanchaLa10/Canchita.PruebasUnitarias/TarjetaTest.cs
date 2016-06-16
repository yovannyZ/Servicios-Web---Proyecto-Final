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
                tarjeta.idTarjeta = "1941250004";
                tarjeta.fechaCreacion=DateTime.Parse("2016/05/01");
                tarjeta.fechaVencimiento=DateTime.Parse("2020/05/01");
                tarjeta.usuario = usuario;
                Assert.IsTrue(proxy.crearTarjetas(tarjeta));                
        }

        [TestMethod]
        public void RecargarTarjeta()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.idTarjeta = "1941250004";
            tarjeta.saldo = 530.0;

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
            int idUsuario = 8;
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
