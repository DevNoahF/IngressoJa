using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;

public record EventPutResponseDTO(
    Guid Id,
    string Name,
    string Description,
    string Street,
    string Neighborhood,
    string City,
    int Number,
    StatesEnum State,
    string Date,
    string Hour,
    double TicketValue,
    int TotalTicketQuantity,
    string BannerImage,
    Guid UserId,
    EventStatusEnum Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);