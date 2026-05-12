namespace IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;//Pode puxar Enum nesse caso?

public record EventCreateResponseDTO(        
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
    User Organizer,
    DateTime CreatedAt
);