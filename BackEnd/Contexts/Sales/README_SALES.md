# Sales Context

This context handles the ticket sale flow. It creates sales, gets sales by id, simulates sale approval or denial, and keeps the event data needed by the sales flow.

## Current Structure

```text
Contexts/Sales
+-- Adapter
|   +-- Controllers
|   |   +-- EventSaleController.cs
|   |   +-- SalesController.cs
|   +-- DTOs
|   |   +-- Mapper
|   |   |   +-- EventSaleMapper.cs
|   |   |   +-- SaleMapper.cs
|   |   +-- Request
|   |   |   +-- CreateSaleRequestDTO.cs
|   |   |   +-- EventSale
|   |   +-- Response
|   |       +-- SaleResponseDTO.cs
|   |       +-- EventSale
|   +-- Interfaces
+-- Domain
|   +-- Entities
|   +-- Events
|   +-- IRepositories
|   +-- UseCases
|       +-- EventSale
|       +-- Sale
|       +-- Ticket
```

## Sale Flow

`SaleEntity` stores `UserId`, `EventId`, optional `TicketId`, selected ticket quantity, total price, creation date, status, and domain events.

Main rules:

* `UserId` and `EventId` cannot be empty.
* `SelectedTicketsUser` must be greater than zero.
* `TotalPrice` cannot be negative.
* `TicketId`, when provided, cannot be `Guid.Empty`.
* Only pending sales can change status.
* Approved sales register `SalePaidEvent`.

## Endpoints

```text
POST /sales
GET /sales/{id}
PATCH /sales/{id}/status
```

Example request:

```json
{
  "userId": "6c6f85f8-2dd2-4e86-9eb8-7d71dd09c111",
  "eventId": "7403c4e1-fd37-4d99-88a0-010f5d4b8f22",
  "selectedTicketsUser": 2,
  "totalPrice": 120.0,
  "ticketId": "6b1ba66b-d66e-4b3f-b857-9c49a2d6b4dd"
}
```

The sale use cases live in `Domain/UseCases/Sale`, ticket use cases live in `Domain/UseCases/Ticket`, and event sale use cases live in `Domain/UseCases/EventSale`.
