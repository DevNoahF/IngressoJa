using IngressoJa.Contexts.Eventos.Domain.Entities;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;//Pode puxar Enum nesse caso?

public record EventCreateRequestDTO(
        string Name,
        string Description,
        string Street,
        string Neighborhood,
        string City,
        int Number,
        StatesEnum State,
        DateTime Date,
        DateTime Hour,
        UserEntity Organizer
    );