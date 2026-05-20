using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Vendas.Domain.Entities;

public class EventSaleEntity
{
    public Guid EventId { get; set; }
    public string EventName { get; set; }
    public double TicketValue { get; set; }
    public int TotalTicketQuantity { get; set; }
    public EventStatusEnum Status { get; set; }

    public EventSaleEntity(Guid eventId, string eventName, double ticketValue, int totalTicketQuantity,
        EventStatusEnum status)
    {
        EventId = eventId;
        EventName = eventName;
        TicketValue = ticketValue;
        TotalTicketQuantity = totalTicketQuantity;
        Status = status;
        
    }
    
}