# Contexto de Vendas

Este contexto cuida do fluxo principal de uma venda de ingresso.

A ideia atual esta simples:

1. Criar uma venda.
2. A venda nasce com status `Pendente`.
3. Processar o pagamento.
4. Se o pagamento for processado, a venda vira `Aprovado`.
5. Buscar uma venda pelo `Id`.

## Estrutura

```text
Contexts/Vendas
+-- Adapter
|   +-- Controllers
|   |   +-- VendasController.cs
|   +-- DTOs
|       +-- Request
|       |   +-- RealizarVendaRequestDTO.cs
|       +-- Response
|           +-- VendaResponseDTO.cs
+-- Domain
|   +-- Entities
|   |   +-- VendasEntidy.cs
|   +-- IRepositories
|   |   +-- IVendaRepository.cs
|   +-- UseCases
|       +-- ObterVendaUseCase.cs
|       +-- ProcessarPagamentoUseCase.cs
|       +-- RealizarVendaUseCase.cs
+-- Infrastructure
    +-- dbContext
    |   +-- VendasDbContext.cs
    +-- Persistence
        +-- Repositories
            +-- VendaRepository.cs
```

## Entidade

A entidade principal e `VendasEntidy`.

Ela guarda:

- `Id`
- `UserId`
- `EventoId`
- `IngressoId`
- `Quantidade`
- `DataVenda`
- `StatusCompra`

Os status atuais sao:

- `Pendente`
- `Aprovado`

Quando uma venda e criada, ela sempre comeca como `Pendente`.

## Regras de Negocio

As regras ficam dentro da entidade `VendasEntidy`.

Ao criar uma venda:

- `UserId` nao pode ser vazio.
- `EventoId` nao pode ser vazio.
- `IngressoId` nao pode ser vazio.
- `Quantidade` deve ser maior que zero.
- `Quantidade` nao pode ser maior que `IngressosDisponiveis`.

Ao aprovar pagamento:

- Apenas vendas com status `Pendente` podem ser aprovadas.

## DTOs

Os DTOs ficam no `Adapter`, porque eles representam a entrada e saida da API.

### Request

`RealizarVendaRequestDTO` recebe os dados para criar uma venda:

```json
{
  "userId": "guid-do-usuario",
  "eventoId": "guid-do-evento",
  "ingressoId": "guid-do-ingresso",
  "quantidade": 2,
  "ingressosDisponiveis": 50
}
```

Esse DTO nao tem regra de negocio e nao tem mensagem de erro. Ele so transporta dados.

### Response

`VendaResponseDTO` devolve os dados principais da venda:

```json
{
  "id": "guid-da-venda",
  "userId": "guid-do-usuario",
  "eventoId": "guid-do-evento",
  "ingressoId": "guid-do-ingresso",
  "quantidade": 2,
  "statusCompra": "Pendente",
  "dataVenda": "2026-05-14T22:00:00Z"
}
```

## Use Cases

### RealizarVendaUseCase

Cria uma venda nova e salva no banco.

Fluxo:

1. Recebe os dados do controller.
2. Cria a entidade `VendasEntidy`.
3. A entidade valida as regras.
4. O repositorio salva a venda.
5. Retorna a entidade criada.

### ObterVendaUseCase

Busca uma venda pelo `Id`.

Se nao encontrar, retorna `null`.

### ProcessarPagamentoUseCase

Aprova uma venda pendente.

Fluxo:

1. Busca a venda pelo `Id`.
2. Se nao encontrar, retorna `null`.
3. Chama `ConfirmarPagamento()`.
4. Salva a venda atualizada.
5. Retorna a venda aprovada.

Hoje nao existe fluxo de recusa nesse contexto.

## Endpoints

### Criar venda

```http
POST /vendas
```

Body:

```json
{
  "userId": "guid-do-usuario",
  "eventoId": "guid-do-evento",
  "ingressoId": "guid-do-ingresso",
  "quantidade": 2,
  "ingressosDisponiveis": 50
}
```

Resposta esperada:

- `201 Created`
- Retorna a venda criada.

### Buscar venda por Id

```http
GET /vendas/{id}
```

Resposta esperada:

- `200 OK` com a venda.
- `404 NotFound` se nao encontrar.

### Aprovar pagamento

```http
PATCH /vendas/{id}/pagamento
```

Nao precisa enviar body.

Resposta esperada:

- `200 OK` com a venda aprovada.
- `404 NotFound` se nao encontrar.
- `400 BadRequest` se a venda nao puder ser aprovada.

## Tratamento de Erros

As mensagens de erro ficam nas regras de negocio da entidade.

O controller captura `ArgumentException` e `InvalidOperationException`, escreve a mensagem no console com `Console.WriteLine()` e retorna `BadRequest`.

Assim os DTOs continuam simples e sem responsabilidade de validacao.
