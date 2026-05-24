# Contexto de Vendas

Este contexto representa o fluxo de venda de ingressos do sistema. A responsabilidade dele e criar vendas, consultar vendas, simular a aprovacao ou negacao da venda e manter uma visao simples dos eventos disponiveis para venda.

## Como funciona hoje

1. A venda e criada com status `Pending`.
2. O caso de uso de criacao valida se a quantidade escolhida pelo usuario nao ultrapassa os ingressos disponiveis.
3. A venda guarda os identificadores do usuario, do evento e, opcionalmente, do ingresso gerado.
4. O endpoint de status simula o resultado financeiro, alternando entre `Approved` e `Denied`.
5. Quando a venda e aprovada, a entidade registra o evento de dominio `SalePaidEvent`.
6. O contexto tambem possui o fluxo `EventSale`, usado para cadastrar, listar, atualizar e remover eventos no recorte de vendas.

## Estrutura atual de pastas

```text
Contexts/Vendas
+-- Adapter
|   +-- Controllers
|   |   +-- EventSaleController.cs
|   |   +-- VendasController.cs
|   +-- DTOs
|   |   +-- Mapper
|   |   |   +-- EventSaleMapper.cs
|   |   |   +-- SaleMapper.cs
|   |   +-- Request
|   |   |   +-- RealizarVendaRequestDTO.cs
|   |   |   +-- EventSale
|   |   +-- Response
|   |       +-- VendaResponseDTO.cs
|   |       +-- EventSale
|   +-- Interfaces
+-- Domain
|   +-- Entities
|   |   +-- EventSaleEntity.cs
|   |   +-- SaleEntity.cs
|   |   +-- TicketEntity.cs
|   |   +-- UserSaleEntity.cs
|   |   +-- Enums
|   +-- Events
|   |   +-- IDomainEvent.cs
|   |   +-- SalePaidEvent.cs
|   +-- IRepositories
|   +-- UseCases
|       +-- ApproveSale
|       +-- CreateSale
|       +-- CreateTicket
|       +-- EventSale
+-- Infrastructure
```

## Entidades principais

### SaleEntity

Representa uma venda criada pelo usuario.

Campos principais:

* `Id` (`int`)
* `UserId` (`Guid`)
* `EventId` (`Guid`)
* `IngressoId` (`Guid?`)
* `SelectedTicketsUser` (`int`)
* `TotalPrice` (`double`)
* `CreatedAt` (`DateTime`)
* `SaleStatus` (`SaleStatusEnum`)
* `DomainEvents` (`IReadOnlyCollection<IDomainEvent>`)

Regras principais:

* `UserId` nao pode ser vazio.
* `EventId` nao pode ser vazio.
* `SelectedTicketsUser` deve ser maior que zero.
* `TotalPrice` nao pode ser negativo.
* `IngressoId`, quando informado, nao pode ser `Guid.Empty`.
* Somente vendas `Pending` podem mudar de status.
* O novo status so pode ser `Approved` ou `Denied`.
* Ao aprovar uma venda, a entidade registra `SalePaidEvent`.

### TicketEntity

Representa um ingresso gerado para um usuario.

Campos principais:

* `Code` (`Guid`)
* `UserId` (`Guid`)

### EventSaleEntity

Representa os dados do evento que o contexto de vendas precisa conhecer.

Campos principais:

* `EventId` (`Guid`)
* `EventName` (`string`)
* `TicketValue` (`double`)
* `TotalTicketQuantity` (`int`)
* `Status` (`EventStatusEnum`)

## DTOs e Mappers

Os mappers ficam em `Adapter/DTOs/Mapper`.

### SaleMapper

Responsavel por converter os objetos do fluxo de venda:

* `CreateSaleRequestDTO` -> `SaleEntity`
* `SaleEntity` -> `SaleResponseDTO`
* `IEnumerable<SaleEntity>` -> `IEnumerable<SaleResponseDTO>`

O controller de vendas usa `SaleMapper` para montar a resposta da API. Assim o DTO nao precisa conhecer a entidade de dominio.

### EventSaleMapper

Responsavel por converter os objetos do fluxo de eventos para venda:

* `EventSaleAddEventRequestDTO` -> `EventSaleEntity`
* `EventSaleUpdateRequestDTO` -> `EventSaleEntity`
* `EventSaleEntity` -> responses de criacao, consulta e atualizacao
* `EventSaleEntity` -> atualizacao de `EventModel`, quando necessario sincronizar dados do evento

## Fluxo de venda

### Criacao

Endpoint:

```text
POST /sales
```

Request:

```json
{
  "userId": "6c6f85f8-2dd2-4e86-9eb8-7d71dd09c111",
  "eventId": "7403c4e1-fd37-4d99-88a0-010f5d4b8f22",
  "selectedTicketsUser": 2,
  "totalPrice": 120.0,
  "availableTickets": 50,
  "ingressoId": "6b1ba66b-d66e-4b3f-b857-9c49a2d6b4dd"
}
```

Passos:

1. O controller recebe `CreateSaleRequestDTO`.
2. `CreateSaleUseCase` valida `SelectedTicketsUser` contra `AvailableTickets`.
3. O caso de uso cria `SaleEntity`.
4. A venda e salva via `ISaleRepository`.
5. O controller converte a entidade com `SaleMapper.ToResponse()`.

Response:

```json
{
  "id": 1,
  "userId": "6c6f85f8-2dd2-4e86-9eb8-7d71dd09c111",
  "eventId": "7403c4e1-fd37-4d99-88a0-010f5d4b8f22",
  "ingressoId": "6b1ba66b-d66e-4b3f-b857-9c49a2d6b4dd",
  "selectedTicketsUser": 2,
  "totalPrice": 120.0,
  "saleStatus": "Pending",
  "createdAt": "2026-05-23T12:00:00Z"
}
```

### Consulta por id

Endpoint:

```text
GET /sales/{id}
```

Busca a venda pelo `Id` inteiro e retorna `404` quando nao encontrar.

### Atualizacao de status

Endpoint:

```text
PATCH /sales/{id}/status
```

Passos:

1. Busca a venda pelo `Id`.
2. Sorteia o novo status entre `Approved` e `Denied`.
3. Executa `SaleEntity.UpdateStatus`.
4. Salva a alteracao via `ISaleRepository`.
5. Retorna a venda atualizada mapeada com `SaleMapper`.

## Fluxo EventSale

Endpoints:

* `POST /event-sales`
* `GET /event-sales`
* `GET /event-sales/{id}`
* `PUT /event-sales/{id}`
* `DELETE /event-sales/{id}`

Esse fluxo usa `EventSaleMapper` para converter requests em `EventSaleEntity` e entidades em DTOs de resposta.

## Observacoes

* O contexto de Vendas usa `Guid` para `UserId`, `EventId` e `IngressoId`.
* O identificador da venda (`SaleEntity.Id`) e `int`.
* O status da venda fica em `SaleStatusEnum`.
* Eventos de dominio sao mantidos somente na entidade e nao devem virar coluna de banco.
* Alteracoes recentes ficaram restritas ao contexto de Vendas.
