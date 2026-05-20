namespace IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

public record EventDetailResponseDTO(
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
    int AvailableTickets,
    string BannerImage,
    EventStatusEnum Status
);