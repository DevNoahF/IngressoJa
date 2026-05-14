using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities;

namespace IngressoJa.Contexts.Eventos.Adapters.DTOs.Mappers;

//Problema usando Mapper no UseCase

public static class EventMapper
{
    public static EventDetailResponseDTO ToDetailResponse(this Event eventEntity)
    {
        return new EventDetailResponseDTO(
            eventEntity.Id,
            eventEntity.Name,
            eventEntity.Description,
            eventEntity.Street,
            eventEntity.Neighborhood,
            eventEntity.City,
            eventEntity.Number,
            eventEntity.State,
            eventEntity.Date,
            eventEntity.Hour,
            eventEntity.Status
        );

    }

    public static EventSummaryResponseDTO ToSummaryResponse(this Event eventEntity)
    {
        return new EventSummaryResponseDTO(
            eventEntity.Id,
            eventEntity.Name,
            eventEntity.City,
            eventEntity.Date,
            eventEntity.Status
            );
    }

    public static EventCreateResponseDTO ToCreateResponse(this Event eventEntity)
    {
        return new EventCreateResponseDTO(
            eventEntity.Id,
            eventEntity.Name,
            eventEntity.Description,
            eventEntity.Street,
            eventEntity.Neighborhood,
            eventEntity.City,
            eventEntity.Number,
            eventEntity.State,
            eventEntity.Date,
            eventEntity.Hour,
            eventEntity.OrganizerId,
            eventEntity.CreatedAt
        );
    }

    public static EventPutResponseDTO ToPutResponse(this Event eventEntity)
    {
        return new EventPutResponseDTO(
            eventEntity.Id,
            eventEntity.Name,
            eventEntity.Description,
            eventEntity.Street,
            eventEntity.Neighborhood,
            eventEntity.City,
            eventEntity.Number,
            eventEntity.State,
            eventEntity.Date,
            eventEntity.Hour,
            eventEntity.OrganizerId,
            eventEntity.Status,
            eventEntity.CreatedAt,
            eventEntity.UpdatedAt
            );
    }
    
    
}