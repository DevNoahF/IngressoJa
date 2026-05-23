using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.EventSale;

public record EventSaleAddEventRequestDTO(
    Guid EventId,
    string EventName,
    double TicketValue,
    int TotalTicketQuantity,
    EventStatusEnum Status
    );