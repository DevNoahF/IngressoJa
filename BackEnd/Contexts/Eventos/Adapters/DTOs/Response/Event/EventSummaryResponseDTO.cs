namespace IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

public record EventSummaryResponseDTO(
    Guid Id,
    string Name,
    string City,
    StatesEnum State,
    string Date,
    EventStatusEnum Status,
    double TicketValue,
    string BannerImage
);