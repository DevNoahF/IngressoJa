using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;

public record EventPutResponseDTO(
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
    TotalTicketQuantity TotalTicketQuantity,
    BannerImageVO BannerImage,
    Guid UserId,
    EventStatusEnum Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);