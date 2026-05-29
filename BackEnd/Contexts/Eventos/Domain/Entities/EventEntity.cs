using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Domain.Entities;

public class EventEntity
{
    private EventEntity() { }

    public Guid Id { get; private set; }
    public NameVO Name { get; private set; }
    public DescriptionVO Description { get; private set; }
    public StreetNameVo Street { get; private set; }
    public NeighborhoodVO Neighborhood { get; private set; }
    public CityVO City { get; private set; }
    public int Number { get; private set; }
    public StatesEnum State { get; private set; }
    public DateVO Date { get; private set; }
   public TimeOnly Hour { get; private set; }
    public TicketValueVO TicketValue { get; private set; }
    public TotalTicketQuantity TotalTicketQuantity { get; private set; }
    public EventStatusEnum Status { get; private set; } = EventStatusEnum.Andamento;
    public BannerImageVO BannerImage { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public EventEntity(NameVO name, DescriptionVO description, StreetNameVo street, NeighborhoodVO neighborhood, CityVO city, int number,
        StatesEnum state, DateVO date, TimeOnly hour, TicketValueVO ticketValue, TotalTicketQuantity totalTicketQuantity, Guid userId, BannerImageVO bannerImage, EventStatusEnum status)
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
        Status = status;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public void Update(NameVO name, DescriptionVO description, StreetNameVo street, NeighborhoodVO neighborhood, CityVO city, int number,
        StatesEnum state, DateVO date, TimeOnly hour, TicketValueVO ticketValue, TotalTicketQuantity totalTicketQuantity, BannerImageVO bannerImage)
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
        BannerImage = bannerImage;

    }
    
    public void ChangeStatus(EventStatusEnum newStatus)//Necessário realizar verificações-Todo
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}