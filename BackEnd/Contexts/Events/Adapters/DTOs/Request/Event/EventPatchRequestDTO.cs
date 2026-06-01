using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;//Pode puxar Enum nesse caso?

public record EventPatchRequestDTO(
    NameVO? Name, 
    DescriptionVO? Description,
    StreetNameVo? Street,
    NeighborhoodVO? Neighborhood,
    CityVO? City,
    int? Number,
    StatesEnum? State,
    string? Date,
    string? Hour,
    TicketValueVO? TicketValue,
    TotalTicketQuantity? TotalTicketQuantity,
    BannerImageVO? BannerImage,
    EventStatusEnum? EventStatus
);