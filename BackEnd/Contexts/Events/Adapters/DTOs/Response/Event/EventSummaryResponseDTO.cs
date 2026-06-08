using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;

public record EventSummaryResponseDTO(
    Guid Id,
    NameVO Name,
    CityVO City,
    StatesEnum State,
    string Date,
    EventStatusEnum Status,
    TicketValueVO TicketValue,
    TotalTicketQuantity TotalTicketQuantity,
    BannerImageVO BannerImage
);

//objeto simples que serve apenas para transportar dados entre camadas da aplicação, sem lógica de negócio.