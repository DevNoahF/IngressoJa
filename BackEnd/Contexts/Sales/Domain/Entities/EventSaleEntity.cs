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

    public void DeductTickets(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        if (TotalTicketQuantity < quantity)
            throw new InvalidOperationException($"Not enough tickets available. Available: {TotalTicketQuantity}, Requested: {quantity}");

        TotalTicketQuantity -= quantity;
    }
}