using System.Globalization;

namespace EcommerceCheckout.App;

/// <summary>Regras de rastreio, fidelidade e frete do exercício de checkout.</summary>
public static class PedidoService
{
    public static string GerarCodigoRastreio(string regiao, int numeroPedido)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(regiao);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numeroPedido);

        return regiao.Trim().ToUpperInvariant() + numeroPedido.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>Concede dois pontos por bloco completo de dez reais.</summary>
    public static int CalcularPontosFidelidade(int valorTotal)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(valorTotal);

        return (valorTotal / 10) * 2;
    }

    public static bool TemDireitoAFreteGratis(int valorTotal, bool eClienteVip)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(valorTotal);

        return valorTotal >= 200 || eClienteVip;
    }
}
