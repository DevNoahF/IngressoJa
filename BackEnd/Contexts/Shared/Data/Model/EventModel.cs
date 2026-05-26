using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Data.Model;

public class EventModel
{
    public Guid Id { get;  set; }
    public NameVO Name { get;  set; }
    public DescriptionVO Description { get;  set; }
    public StreetNameVo StreetName { get;set; }
    public NeighborhoodVO Neighborhood { get;  set; }
    public CityVO City { get;  set; }
    public int Number { get;  set; }
    public StatesEnum State { get;  set; }
    public DateVO Date { get;  set; }
    public TimeOnly Hour { get;  set; }
    public TicketValueVO TicketValue { get;  set; }
    public TotalTicketQuantity TotalTicketQuantity { get;  set; }
    public Guid UserId { get;  set; }
    public EventStatusEnum Status { get;  set; }
    public BannerImageVO BannerImage {get; set;}
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get;  set; }

}