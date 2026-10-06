using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCheckout.Tests
{
    public class PedidoServiceTests
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("SP12345", PedidoService.PedidoService.GerarCodigoRastreio("SP", 12345));
        }

        [Fact]
        public void Test2()
        {
            Assert.Equal(20, PedidoService.PedidoService.CalcularPontosFidelidade(100));
        }
        public void Test3()
        {
            Assert.True(PedidoService.PedidoService.TemDireitoAFreteGratis(250, true));
            Assert.False(PedidoService.PedidoService.TemDireitoAFreteGratis(250, false));
        }
    }
}