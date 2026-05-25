using IngressoJa.Contexts.Sales.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Request.EventSale;

public record EventSaleUpdateRequestDTO(
    string EventName,
    double TicketValue,
    int TotalTicketQuantity,
    EventStatusEnum Status
    );