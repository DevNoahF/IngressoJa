using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Eventos.Domain.Entities;

public class EventEntity
{
    private EventEntity() { }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Street { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public int Number { get; private set; }
    public StatesEnum State { get; private set; }
    public DateOnly Date { get; private set; }
   public TimeOnly Hour { get; private set; }
    public double TicketValue { get; private set; }
    public int TotalTicketQuantity { get; private set; }
    public EventStatusEnum Status { get; private set; } = EventStatusEnum.Andamento;
    public string BannerImage { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public EventEntity(string name, string description, string street, string neighborhood, string city, int number,
        StatesEnum state, DateOnly date, TimeOnly hour, double ticketValue, int totalTicketQuantity, Guid userId, int availableTickets, string bannerImage)
    {
        
        if (number < 0)
            throw new Exception("Number must be greater than or equal to zero");

        if (!Enum.IsDefined(typeof(StatesEnum), state))
            throw new Exception("Invalid state");
        
        if (userId == Guid.Empty)
            throw new EventFieldNameRequiredException("OrganizerId");
        

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Street = street;
        Neighborhood = neighborhood;
        City = city;
        Number = number;
        State = state;
        Date = date;
        Hour = hour;
        TicketValue = ticketValue;
        TotalTicketQuantity = totalTicketQuantity;
        BannerImage = bannerImage;
        Status = EventStatusEnum.Andamento;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public void Update(string name, string description, string street, string neighborhood, string city, int number,
        StatesEnum state, DateOnly date, TimeOnly hour, double ticketValue, int totalTicketQuantity)
    {
        if (Status == EventStatusEnum.Cancelado)
            throw new Exception("Cannot update a cancelled event");

        if (Status == EventStatusEnum.Encerrado)
            throw new Exception("Cannot update a finished event");

        if (number < 0)
            throw new Exception("Number must be greater than or equal to zero");

        if (!Enum.IsDefined(typeof(StatesEnum), state))
            throw new Exception("Invalid state");



        Name = name;
        Description = description;
        Street = street;
        Neighborhood = neighborhood;
        City = city;
        Number = number;
        State = state;
        Date = date;
        Hour = hour;
        TicketValue = ticketValue;
        TotalTicketQuantity = totalTicketQuantity;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void ChangeStatus(EventStatusEnum newStatus)//Necessário realizar verificações-Todo
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}