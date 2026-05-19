namespace IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;//Pode puxar Enum nesse caso?

public record EventPutRequestDTO(
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