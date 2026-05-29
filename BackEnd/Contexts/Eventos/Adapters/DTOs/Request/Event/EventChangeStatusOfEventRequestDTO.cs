using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;

public record EventChangeStatusOfEventRequestDTO(
    EventStatusEnum Status
);

    
