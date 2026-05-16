using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

namespace IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

public class Event
{
    private Event() { }
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
    public EventStatusEnum Status { get; private set; }
    public Guid OrganizerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Event(Guid id, string name, string description, string street, string neighborhood, string city, int number,
        StatesEnum state, DateOnly date, TimeOnly hour, Guid organizerId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new EventFieldNameRequiredException("Name");
        if (name.Length > 55)
            throw new EventMaxLenghtExceededException("Name", 55);

        if (string.IsNullOrWhiteSpace(description))
            throw new EventFieldNameRequiredException("Description");
        if (description.Length > 255)
            throw new EventMaxLenghtExceededException("Description", 255);

        if (string.IsNullOrWhiteSpace(street))
            throw new EventFieldNameRequiredException("Street name");
        if (street.Length > 55)
            throw new EventMaxLenghtExceededException("Street name", 55);

        if (string.IsNullOrWhiteSpace(neighborhood))
            throw new EventFieldNameRequiredException("Neighborhood");
        if (neighborhood.Length > 55)
            throw new EventMaxLenghtExceededException("Neighborhood", 55);

        if (string.IsNullOrWhiteSpace(city))
            throw new EventFieldNameRequiredException("City");
        if (city.Length > 55)
            throw new EventMaxLenghtExceededException("City", 55);

        if (number < 0)
            throw new Exception("Number must be greater than or equal to zero");

        if (date < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new EventDateInPastException(date.ToDateTime(TimeOnly.MinValue));

        if (date == DateOnly.FromDateTime(DateTime.UtcNow) && hour < TimeOnly.FromDateTime(DateTime.UtcNow))
            throw new Exception("Event Date or Hour is in the Past");

        if (!Enum.IsDefined(typeof(StatesEnum), state))
            throw new Exception("Invalid state");

        if (organizerId == Guid.Empty)
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
        Status = EventStatusEnum.Andamento;
        OrganizerId = organizerId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }
    
    public void Update(string name, string description, string street, string neighborhood, string city, int number, StatesEnum state, DateOnly date, TimeOnly hour)
    {
        Name = name;
        Description = description;
        Street = street;
        Neighborhood = neighborhood;
        City = city;
        Number = number;
        State = state;
        Date = date;
        Hour = hour;
        UpdatedAt = DateTime.UtcNow;
    }
}