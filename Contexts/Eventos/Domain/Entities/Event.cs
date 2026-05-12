namespace IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
public class Event
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Street { get; private set; }
    public string Neighborhood { get; private set; }//bairro
    public string City { get; private set; }
    public int Number { get; private set; }
    public StatesEnum State { get; private set; }
    public DateTime Date { get; private set; } //dd/mm/yyyy
    public DateTime Hour { get; private set; } //hh:mm
    public EventStatusEnum Status { get; private set; }
    public User Organizer { get; set; } //devido ao erro oq eu poderia fazer?
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Event(Guid id, string name, string description, string street, string neighborhood, string city, int number,
        StatesEnum state, DateTime
            date, DateTime hour, EventStatusEnum status, User organizer)
    {
        Id = id;
        Name = name;
        Description = description;
        Street = street;
        Neighborhood = neighborhood;
        City = city;
        Number = number;
        State = state;
        Date =DateTime.Parse(date.ToString("dd-MM-yyyy")) ;
        Hour = DateTime.Parse(hour.ToString("HH:mm"));
        Status = EventStatusEnum.Andamento;//Evento já seria criado de padrão em andamento ou organizador precisaria colocar manualmente?
        Organizer = organizer;
        CreatedAt = DateTime.UtcNow;
        

    }
    
}