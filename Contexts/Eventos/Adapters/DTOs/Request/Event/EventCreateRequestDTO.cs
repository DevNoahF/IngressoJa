using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;

public record EventCreateRequestDTO(
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
    int TotalTicketQuantity
);