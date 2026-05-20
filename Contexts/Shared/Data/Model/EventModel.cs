using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

namespace IngressoJa.Data.Model;

public class EventModel
{
    public Guid Id { get;  set; }
    public string Name { get;  set; }
    public string Description { get;  set; }
    public string StreetName { get;set; }
    public string Neighborhood { get;  set; }
    public string City { get;  set; }
    public int Number { get;  set; }
    public StatesEnum State { get;  set; }
    public DateOnly Date { get;  set; }
    public TimeOnly Hour { get;  set; }
    public Double TicketValue { get;  set; }
    public int TotalTicketQuantity { get;  set; }
    public Guid UserId { get;  set; }
    public EventStatusEnum Status { get;  set; }
    public string BannerImage {get; set;}
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get;  set; }

}