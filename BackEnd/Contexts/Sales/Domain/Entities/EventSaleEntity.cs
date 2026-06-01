using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Sales.Domain.Entities;

public class EventSaleEntity
{

    public Guid EventId { get; set; }
    public NameVO Name { get; set; }
    public TicketValueVO TicketValue { get; set; }
    public TotalTicketQuantity TotalTicketQuantity { get; set; }
    public EventStatusEnum Status { get; set; }

    public EventSaleEntity(Guid eventId, NameVO name, TicketValueVO ticketValue,TotalTicketQuantity totalTicketQuantity,
        EventStatusEnum status)
    {
        if (!Enum.IsDefined(typeof(EventStatusEnum), status))
            throw new Exception("Invalid event sale status");

        EventId = eventId;
        Name = name;
        TicketValue = ticketValue;
        TotalTicketQuantity = totalTicketQuantity;
        Status = status;
        
    }
    
}