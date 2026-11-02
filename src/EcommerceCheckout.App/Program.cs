using System.Text;

Console.WriteLine("Hello, World!");



string codigoRastreio = PedidoService.PedidoService.GerarCodigoRastreio("SP", 12345);
int pontosFidelidade = PedidoService.PedidoService.CalcularPontosFidelidade(100);
bool freteGratis = PedidoService.PedidoService.TemDireitoAFreteGratis(250, true);
Console.WriteLine($"Código de rastreio gerado: {codigoRastreio}");
Console.WriteLine($"Código de rastreio gerado: {pontosFidelidade}");
Console.WriteLine($"Código de rastreio gerado: {freteGratis}");