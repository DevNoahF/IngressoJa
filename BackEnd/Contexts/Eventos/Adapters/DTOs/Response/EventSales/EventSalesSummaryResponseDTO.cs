using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Response.EventSales;

public sealed record EventSalesSummaryResponseDTO(
    Guid EventId,
    NameVO EventName,
    int TicketsSold,
    double TotalRevenue,
    int ApprovedSales);