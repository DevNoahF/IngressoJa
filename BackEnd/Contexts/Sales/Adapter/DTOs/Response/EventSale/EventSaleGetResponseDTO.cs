using IngressoJa.Contexts.Sales.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Response.EventSale;

public record EventSaleGetResponseDTO(
    Guid EventId,
    string EventName,
    double TicketValue,
    int TotalTicketQuantity,
    EventStatusEnum EventStatus
    );