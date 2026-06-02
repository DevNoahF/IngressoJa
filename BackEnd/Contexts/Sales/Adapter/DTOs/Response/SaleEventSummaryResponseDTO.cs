namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Response;

public sealed record SaleEventSummaryResponseDTO(
    Guid EventId,
    string EventName,
    int TotalTicketsPublished,
    int TicketsSold,
    int TicketsRemaining,
    double TicketValue,
    double TotalRevenue);