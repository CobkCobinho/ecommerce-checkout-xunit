namespace PedidoService;

    public class PedidoService {
        public static string GerarCodigoRastreio(string regiao, int numeroPedido) {
            regiao = regiao.ToUpper();
            string codigoRastreio = regiao + numeroPedido;

            return codigoRastreio;
        }

        public static int CalcularPontosFidelidade(int valorTotal) {
            valorTotal = (valorTotal / 10) * 2;

            return valorTotal;
        }

        public static bool TemDireitoAFreteGratis(int valorTotal, bool eClienteVip)
    {
        if (valorTotal >= 200 || eClienteVip)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    }
