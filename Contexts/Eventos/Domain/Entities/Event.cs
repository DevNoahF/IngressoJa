using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

namespace IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

public class Event
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Street { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public int Number { get; private set; }
    public StatesEnum State { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime Hour { get; private set; }
    public EventStatusEnum Status { get; private set; }
    public Guid OrganizerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Event(Guid id, string name, string description, string street, string neighborhood, string city, int number,
    StatesEnum state, DateTime date, DateTime hour, Guid organizerId)
{
    // Nome
    if (string.IsNullOrWhiteSpace(name))
        throw new EventFieldNameRequiredException("Name");
    if (name.Length > 55)
        throw new EventMaxLenghtExceededException("Name", 55);

    // Descrição
    if (string.IsNullOrWhiteSpace(description))
        throw new EventFieldNameRequiredException("Description");
    if (description.Length > 255)
        throw new EventMaxLenghtExceededException("Description", 255);

    // Rua
    if (string.IsNullOrWhiteSpace(street))
        throw new EventFieldNameRequiredException("Street name");
    if (street.Length > 55)
        throw new EventMaxLenghtExceededException("Street name", 55);

    // Bairro
    if (string.IsNullOrWhiteSpace(neighborhood))
        throw new EventFieldNameRequiredException("Neighborhood");
    if (neighborhood.Length > 55)
        throw new EventMaxLenghtExceededException("Neighborhood", 55);

    // Cidade
    if (string.IsNullOrWhiteSpace(city))
        throw new EventFieldNameRequiredException("City");
    if (city.Length > 55)
        throw new EventMaxLenghtExceededException("City", 55);

    // Número
    if (number < 0)
        throw new Exception("Number must be greater than or equal to zero");

    // Data
    if (date.Date < DateTime.UtcNow.Date)
        throw new EventDateInPastException(date);

    // Hora
    if (date.Date == DateTime.UtcNow.Date && hour.TimeOfDay < DateTime.UtcNow.TimeOfDay)
        throw new EventHourInPastException(hour);

    // Estado
    if (!Enum.IsDefined(typeof(StatesEnum), state))
        throw new Exception("State is not defined");

    // Organizador
    if (organizerId == Guid.Empty)
        throw new EventFieldNameRequiredException("OrganizerId");

    Id = id;
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
}