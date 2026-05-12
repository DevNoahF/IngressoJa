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
    DateTime Date,
    DateTime Hour,
    EventStatusEnum Status
    );