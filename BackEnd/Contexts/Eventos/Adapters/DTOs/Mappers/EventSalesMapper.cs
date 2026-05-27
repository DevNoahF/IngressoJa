using IngressoJa.Contexts.Eventos.Application.DTOs.Response.EventSales;
using IngressoJa.Contexts.Eventos.Domain.Entities;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;

public static class EventSalesMapper
{
    public static EventSalesSummaryResponseDTO ToSummaryResponse(this SaleEventEntity entity)
    {
        return new EventSalesSummaryResponseDTO(
            entity.EventId,
            entity.EventName,
            entity.TicketsSold,
            entity.TotalRevenue,
            entity.ApprovedSales);
    }
}