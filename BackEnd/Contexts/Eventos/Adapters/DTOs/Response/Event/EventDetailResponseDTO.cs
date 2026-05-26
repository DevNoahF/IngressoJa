using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

public record EventDetailResponseDTO(
    Guid Id,
    NameVO Name,
    DescriptionVO Description,
    StreetNameVo Street,
    NeighborhoodVO Neighborhood,
    CityVO City,
    int Number,
    StatesEnum State,
    string Date,
    string Hour,
    TicketValueVO TicketValue,
    BannerImageVO BannerImage,
    EventStatusEnum Status
);