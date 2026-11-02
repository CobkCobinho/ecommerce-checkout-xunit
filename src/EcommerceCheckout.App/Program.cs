using EcommerceCheckout.App;

const int valorTotal = 100;
const bool eClienteVip = false;

string codigoRastreio = PedidoService.GerarCodigoRastreio("SP", 12345);
int pontosFidelidade = PedidoService.CalcularPontosFidelidade(valorTotal);
bool freteGratis = PedidoService.TemDireitoAFreteGratis(valorTotal, eClienteVip);

Console.WriteLine("Exemplo de checkout");
Console.WriteLine($"Código de rastreio: {codigoRastreio}");
Console.WriteLine($"Pontos de fidelidade: {pontosFidelidade}");
Console.WriteLine($"Frete grátis: {(freteGratis ? "Sim" : "Não")}");
