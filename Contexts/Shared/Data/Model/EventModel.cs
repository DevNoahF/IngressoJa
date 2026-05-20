using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

namespace IngressoJa.Data.Model;

public class EventModel
{
    public Guid Id { get; private set; }
    public string Description { get; private set; }
    public string StreetName { get;private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public StatesEnum State { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly Hour { get; private set; }
    public Double TicketValue { get; private set; }
    public int TotalTicketQuantity { get; private set; }
    public Guid UserId { get; private set; }
    public EventStatusEnum Status { get; private set; }
    public string BannerImage {get; private set;}
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

}