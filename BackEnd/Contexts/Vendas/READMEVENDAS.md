# Contexto de Sales

Este contexto representa a venda de ingressos dentro do sistema. Ele foi simplificado para funcionar como um fluxo acadêmico, com foco em regra de negócio clara e pouca complexidade de infraestrutura.

Hoje o contexto está organizado assim:

1. A venda nasce com status `Pending`.
2. A criação valida a quantidade pedida contra os ingressos disponíveis.
3. O pagamento não faz mais parte do contexto como agregado próprio (virou um Enum de status na Venda).
4. O update do status da venda simula o resultado do financeiro com `Approved` ou `Denied`.
5. Quando a venda for aprovada, o domínio dispara o evento `SalePaidEvent`.
6. O agregado de ingressos será responsável por consumir esse evento depois.

## O que foi feito (Refatoração)

O contexto antigo estava muito preso a nomes em português e a uma modelagem com `Guid` no identificador da venda. Ele também carregava uma estrutura de `payment` que não fazia mais sentido para o desenho final.

O desenho atual também deixa dois pontos importantes bem definidos:
- `availableTickets` não é propriedade da venda e não vai para o banco; ele entra só como informação de validação do caso de uso na criação.
- A entidade mantém uma coleção interna de `DomainEvents` para registrar eventos como `SalePaidEvent` antes do despacho por repositório, interceptador ou mediator.

A refatoração atual fez o seguinte:
- `VendasEntidy` virou `SaleEntity`.
- O identificador da venda passou a ser `int`.
- `UserId` e `EventId` também passaram a ser `int`.
- O campo `IngressoId` foi removido da venda.
- O status da venda passou a ser o enum `SaleStatus`.
- Os enums e eventos foram traduzidos para o inglês.
- O controller, request, response, use cases e repository foram alinhados com essa nova linguagem.

## Estrutura Atual de Pastas

```text
Contexts/Vendas
+-- Adapter
|   +-- Controllers
|   |   +-- SalesController.cs
|   +-- DTOs
|       +-- Request
|       |   +-- CreateSaleRequestDTO.cs
|       +-- Response
|           +-- SaleResponseDTO.cs
+-- Domain
|   +-- Entities
|   |   +-- SaleEntity.cs
|   |   +-- Enums
|   |       +-- SaleStatusEnum.cs
|   +-- Events
|   |   +-- IDomainEvent.cs
|   |   +-- SalePaidEvent.cs
|   +-- IRepositories
|   |   +-- ISaleRepository.cs
|   +-- UseCases
|       +-- CreateSaleUseCase.cs
|       +-- GetSaleByIdUseCase.cs
|       +-- UpdateSaleStatusUseCase.cs
+-- Data
  +-- SaleContext
  |   +-- SaleContext.cs
  +-- Persistence
    +-- Repositories
      +-- SaleRepository.cs

```

## Entidade Principal

A entidade principal é `SaleEntity`.

Ela guarda:

* `Id` (`int`)
* `UserId` (`int`)
* `EventId` (`int`)
* `SelectedTicketsUser` (`int`)
* `TotalPrice` (`double`/`decimal`)
* `CreatedAt` (`DateTime`)
* `SaleStatus` (`Enum`)

O parâmetro `availableTickets` é usado apenas no caso de uso de criação para validar estoque do evento antes de instanciar a venda.

O status sempre inicia como `Pending`.

### Regras da entidade (Domain Validations)

* `UserId` deve ser maior que zero.
* `EventId` deve ser maior que zero.
* `SelectedTicketsUser` deve ser maior que zero.
* `TotalPrice` não pode ser negativo.
* `SelectedTicketsUser` não pode ser maior que `AvailableTickets`.
* Apenas vendas em status `Pending` podem mudar de status.
* O status pode virar `Approved` ou `Denied`.
* Se virar `Approved`, a entidade gera e registra o evento `SalePaidEvent`.

## Domain Events

A entidade mantém uma coleção interna de eventos de domínio para acumular o que aconteceu dentro dela.

* `DomainEvents` guarda os eventos disparados pela `SaleEntity`.
* `ClearDomainEvents()` limpa a coleção depois do despacho.
* O mapeamento do EF ignora essa coleção, então ela não vira coluna de banco.

## Fluxo Atual da Venda

### Criação

O controller recebe `CreateSaleRequestDTO` e chama `CreateSaleUseCase`.

1. Recebe `UserId`, `EventId`, quantidade (`SelectedTicketsUser`), valor total e os ingressos disponíveis para validação.
2. Valida se há ingressos suficientes no domínio.
3. Cria a venda com status `Pending`.
4. Persiste no banco via `ISaleRepository`.
5. Retorna a venda criada mapeada para `SaleResponseDTO`.

### Atualização de Status (Simulação)

O endpoint de status simula o resultado do pagamento.

1. Busca a venda pelo `Id`. Se não encontrar, retorna `NotFound`.
2. Sorteia (Random) entre `Approved` ou `Denied`.
3. Executa o método de alteração de status na `SaleEntity`.
4. Se o status mudar para `Approved`, a entidade dispara o `SalePaidEvent`.
5. Salva a alteração no banco.

### Consulta

O endpoint de busca retorna a venda pelo `Id` inteiro.

## DTOs

### Request

```json
{
  "userId": 1,
  "eventId": 10,
  "selectedTicketsUser": 2,
  "totalPrice": 120.0,
  "availableTickets": 50
}

```

### Response

```json
{
  "id": 1,
  "userId": 1,
  "eventId": 10,
  "selectedTicketsUser": 2,
  "totalPrice": 120.0,
  "saleStatus": "Pending",
  "createdAt": "2026-05-20T00:00:00Z"
}

```

## Endpoints

* `POST /sales` -> Criar venda
* `GET /sales/{id}` -> Buscar venda por id
* `PATCH /sales/{id}/status` -> Simular/Atualizar status da venda

---