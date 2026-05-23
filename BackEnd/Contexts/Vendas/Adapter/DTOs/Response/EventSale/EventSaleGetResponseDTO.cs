using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.EventSale;

public record EventSaleGetResponseDTO(
    Guid EventId,
    string EventName,
    double TicketValue,
    int TotalTicketQuantity,
    EventStatusEnum EventStatus
    );