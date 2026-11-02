# Ecommerce Checkout · C# e xUnit

[![Build e testes](https://github.com/CobkCobinho/ecommerce-checkout-xunit/actions/workflows/ci.yml/badge.svg)](https://github.com/CobkCobinho/ecommerce-checkout-xunit/actions/workflows/ci.yml)

Exercício de testes unitários em C# com três regras de checkout: geração de código de rastreio, cálculo de pontos de fidelidade e elegibilidade para frete grátis.

O projeto inclui uma demonstração no console e **34 casos de teste com xUnit**, cobrindo exemplos válidos, limites e entradas inválidas. Não depende de banco de dados nem de serviços externos.

## Executar

Pré-requisito: **SDK .NET 10**.

```sh
git clone https://github.com/CobkCobinho/ecommerce-checkout-xunit.git
cd ecommerce-checkout-xunit
dotnet restore
dotnet run --project src/EcommerceCheckout.App
```

Saída da demonstração:

```text
Exemplo de checkout
Código de rastreio: SP12345
Pontos de fidelidade: 20
Frete grátis: Não
```

## Regras

| Método | Comportamento | Exemplo |
| --- | --- | --- |
| `GerarCodigoRastreio` | Remove espaços nas extremidades, converte a região para maiúsculas e concatena o número do pedido. | `("sp", 12345)` → `"SP12345"` |
| `CalcularPontosFidelidade` | Concede 2 pontos por bloco **completo** de R$ 10. | `19` → `2`; `100` → `20` |
| `TemDireitoAFreteGratis` | Concede frete grátis para valores a partir de R$ 200 **ou** clientes VIP. | `(200, false)` → `true`; `(199, false)` → `false` |

A região não pode ser nula, vazia ou conter apenas espaços; o número do pedido deve ser positivo. Valores totais negativos são rejeitados, inclusive para clientes VIP. Entradas inválidas geram exceções de argumento com identificação do parâmetro.

O código de rastreio é uma concatenação didática: não verifica UFs, não garante unicidade global e não integra uma transportadora. Os valores são inteiros em reais, conforme a atividade; não há suporte a centavos, carrinho ou processamento de pagamentos.

## Testes

Na raiz do repositório:

```sh
dotnet test --configuration Release
```

Os testes usam `[Fact]` para cenários individuais e `[Theory]` com `[InlineData]` para tabelas de casos:

- Rastreio com letras minúsculas, espaços, região ausente e números inválidos.
- Normalização independente da cultura do computador.
- Fidelidade antes, no limite e depois de cada bloco de dez, incluindo zero e o maior inteiro suportado.
- Frete no limite de R$ 200, abaixo e acima dele, com e sem benefício VIP.
- Rejeição de totais negativos.

Para gerar também os relatórios de execução e cobertura:

```sh
dotnet test --configuration Release --collect:"XPlat Code Coverage" --logger trx
```

Os arquivos `.trx` e `coverage.cobertura.xml` são gerados em `tests/EcommerceCheckout.Tests/TestResults/`, fora do versionamento. Os testes verificam `PedidoService`; a demonstração do console é executada separadamente e também aparece no relatório de cobertura do aplicativo.

## Estrutura

```text
EcommerceCheckout.slnx
global.json
src/
  EcommerceCheckout.App/
    PedidoService.cs                 # Três regras de negócio
    Program.cs                       # Demonstração no console
    EcommerceCheckout.App.csproj
tests/
  EcommerceCheckout.Tests/
    PedidoServiceTests.cs            # Casos parametrizados e validações
    EcommerceCheckout.Tests.csproj
.github/
  workflows/
    ci.yml                          # Build e testes em Linux e Windows
```

`bin/`, `obj/` e relatórios de teste são gerados pelo .NET e ignorados pelo Git. O código-fonte e os arquivos de projeto são suficientes para reconstruir o exercício.

## Integração contínua

O GitHub Actions restaura os pacotes, compila com avisos tratados como erros e executa os testes em Linux e Windows a cada push ou pull request. Os relatórios de testes e cobertura ficam disponíveis nos artefatos da execução por sete dias.
