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
    public UserEntity OrganizerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Event(Guid id, string name, string description, string street, string neighborhood, string city, int number,
        StatesEnum state, DateTime date, DateTime hour, UserEntity organizerId)
    {
        // Nome
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name is required");
        if (name.Length > 55)
            throw new Exception("Name cannot exceed 55 characters");

        // Descrição
        if (string.IsNullOrWhiteSpace(description))
            throw new Exception("Description is required");
        if (description.Length > 255)
            throw new Exception("Description cannot exceed 255 characters");

        // Rua
        if (string.IsNullOrWhiteSpace(street))
            throw new Exception("Street is required");
        if (street.Length > 55)
            throw new Exception("Street cannot exceed 55 characters");

        // Bairro
        if (string.IsNullOrWhiteSpace(neighborhood))
            throw new Exception("Neighborhood is required");
        if (neighborhood.Length > 55)
            throw new Exception("Neighborhood cannot exceed 55 characters");

        // Cidade
        if (string.IsNullOrWhiteSpace(city))
            throw new Exception("City is required");
        if (city.Length > 55)
            throw new Exception("City cannot exceed 55 characters");

        // Número
        if (number < 0)
            throw new Exception("Number cannot be negative");

        // Data
        if (date < DateTime.UtcNow)
            throw new Exception("Event date must be in the future");

        // Hora
        if (date.Date == DateTime.UtcNow.Date && hour.TimeOfDay < DateTime.UtcNow.TimeOfDay)
            throw new Exception("Event hour must be in the future");

        // Estado
        if (!Enum.IsDefined(typeof(StatesEnum), state))
            throw new Exception("Invalid state");

        // Organizador
        if (organizerId is null)
            throw new Exception("Organizer is required");

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
        UpdatedAt = DateTime.UtcNow;
    }
}