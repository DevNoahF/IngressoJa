using IngressoJa.Contexts.Sales.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Request.EventSale;

public record EventSaleAddEventRequestDTO(
    Guid EventId,
    string EventName,
    double TicketValue,
    int TotalTicketQuantity,
    EventStatusEnum Status
    );