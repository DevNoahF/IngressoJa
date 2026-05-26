using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Sales.Domain.Entities;

public class EventSaleEntity
{
    public Guid EventId { get; set; }
    public NameVO EventName { get; set; }
    public TicketValueVO TicketValue { get; set; }
    public TotalTicketQuantity TotalTicketQuantity { get; set; }
    public EventStatusEnum Status { get; set; }

    public EventSaleEntity(Guid eventId, NameVO eventName, TicketValueVO ticketValue,TotalTicketQuantity totalTicketQuantity,
        EventStatusEnum status)
    {
        EventId = eventId;
        EventName = eventName;
        TicketValue = ticketValue;
        TotalTicketQuantity = totalTicketQuantity;
        Status = status;
        
    }
    
}