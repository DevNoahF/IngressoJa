using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Domain.Entities;

public sealed class SaleEventEntity
{
    public Guid EventId { get; }
    public NameVO EventName { get; }
    public int TicketsSold { get; }
    public double TotalRevenue { get; }
    public int ApprovedSales { get; }

    public SaleEventEntity(
        Guid eventId,
        NameVO eventName,
        int ticketsSold,
        double totalRevenue,
        int approvedSales)
    {
        EventId = eventId;
        EventName = eventName;
        TicketsSold = ticketsSold;
        TotalRevenue = totalRevenue;
        ApprovedSales = approvedSales;
    }
}