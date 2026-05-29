using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Request.EventSale;

public record EventSaleAddEventRequestDTO(
    Guid EventId,
    NameVO Name,
    TicketValueVO TicketValue, 
    TotalTicketQuantity TotalTicketQuantity,
    EventStatusEnum Status
    );