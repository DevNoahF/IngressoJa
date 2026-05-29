using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Response.EventSale;

public record EventSaleGetResponseDTO(
    Guid EventId,
    NameVO Name,
    TicketValueVO TicketValue,
    TotalTicketQuantity TotalTicketQuantity,
    EventStatusEnum EventStatus
    );