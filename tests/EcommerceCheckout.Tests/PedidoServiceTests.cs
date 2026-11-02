using System.Globalization;
using EcommerceCheckout.App;

namespace EcommerceCheckout.Tests;

public class PedidoServiceTests
{
    [Theory]
    [InlineData("SP", 12345, "SP12345")]
    [InlineData("sp", 12345, "SP12345")]
    [InlineData("  rj  ", 42, "RJ42")]
    [InlineData("MG", 1, "MG1")]
    [InlineData("SP", int.MaxValue, "SP2147483647")]
    public void GerarCodigoRastreio_NormalizaRegiaoEConcatenaPedido(string regiao, int numeroPedido, string esperado)
    {
        Assert.Equal(esperado, PedidoService.GerarCodigoRastreio(regiao, numeroPedido));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GerarCodigoRastreio_RejeitaRegiaoAusente(string? regiao)
    {
        var exception = Assert.ThrowsAny<ArgumentException>(() => PedidoService.GerarCodigoRastreio(regiao!, 1));
        Assert.Equal("regiao", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void GerarCodigoRastreio_RejeitaNumeroNaoPositivo(int numeroPedido)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => PedidoService.GerarCodigoRastreio("SP", numeroPedido));
        Assert.Equal("numeroPedido", exception.ParamName);
    }

    [Fact]
    public void GerarCodigoRastreio_NaoDependeDaCulturaDoComputador()
    {
        var culturaOriginal = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("tr-TR");
            Assert.Equal("PI42", PedidoService.GerarCodigoRastreio("pi", 42));
        }
        finally
        {
            CultureInfo.CurrentCulture = culturaOriginal;
        }
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(9, 0)]
    [InlineData(10, 2)]
    [InlineData(19, 2)]
    [InlineData(20, 4)]
    [InlineData(99, 18)]
    [InlineData(100, 20)]
    [InlineData(int.MaxValue, 429496728)]
    public void CalcularPontosFidelidade_ConsideraApenasBlocosCompletos(int valorTotal, int esperado)
    {
        Assert.Equal(esperado, PedidoService.CalcularPontosFidelidade(valorTotal));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void CalcularPontosFidelidade_RejeitaValorNegativo(int valorTotal)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => PedidoService.CalcularPontosFidelidade(valorTotal));
        Assert.Equal("valorTotal", exception.ParamName);
    }

    [Theory]
    [InlineData(0, false, false)]
    [InlineData(199, false, false)]
    [InlineData(200, false, true)]
    [InlineData(201, false, true)]
    [InlineData(250, false, true)]
    [InlineData(0, true, true)]
    [InlineData(199, true, true)]
    [InlineData(200, true, true)]
    [InlineData(250, true, true)]
    public void TemDireitoAFreteGratis_AplicaLimiteOuBeneficioVip(int valorTotal, bool eClienteVip, bool esperado)
    {
        Assert.Equal(esperado, PedidoService.TemDireitoAFreteGratis(valorTotal, eClienteVip));
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(-1, true)]
    [InlineData(int.MinValue, false)]
    public void TemDireitoAFreteGratis_RejeitaValorNegativoMesmoParaVip(int valorTotal, bool eClienteVip)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => PedidoService.TemDireitoAFreteGratis(valorTotal, eClienteVip));
        Assert.Equal("valorTotal", exception.ParamName);
    }
}
