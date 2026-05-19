using IngressoJa.Contexts.Eventos.Application.DTOs.Response.EventEntity;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.EventEntity;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;

public static class EventMapper
{
    public static EventDetailResponseDTO ToDetailResponse(this EventEntity eventEntity)//Tela detalhada aonde o usuário poderia ir depois para a tela de pagamento
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
            eventEntity.Date.ToString("dd/MM/yyyy"),
            eventEntity.Hour.ToString("HH:mm"),
            eventEntity.Status
        );
    }

    public static EventSummaryResponseDTO ToSummaryResponse(this EventEntity eventEntity)//Pensado para apresentar uma "tela geral" com todos os eventos
    {
        return new EventSummaryResponseDTO(
            eventEntity.Id,
            eventEntity.Name,
            eventEntity.City,
            eventEntity.Date.ToString("dd/MM/yyyy"),
            eventEntity.Status
        );
    }

    public static EventCreateResponseDTO ToCreateResponse(this EventEntity eventEntity)
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
            eventEntity.Date.ToString("dd/MM/yyyy"),
            eventEntity.Hour.ToString("HH:mm"),
            eventEntity.OrganizerId,
            eventEntity.CreatedAt
        );
    }

    public static EventPutResponseDTO ToPutResponse(this EventEntity eventEntity)
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
            eventEntity.Date.ToString("dd/MM/yyyy"),
            eventEntity.Hour.ToString("HH:mm"),
            eventEntity.OrganizerId,
            eventEntity.Status,
            eventEntity.CreatedAt,
            eventEntity.UpdatedAt
        );
    }

    public static EventEntity ToEntity(this EventCreateRequestDTO dto, Guid organizerId)
    {
        return new EventEntity(
            Guid.NewGuid(),
            dto.Name,
            dto.Description,
            dto.Street,
            dto.Neighborhood,
            dto.City,
            dto.Number,
            dto.State,
            DateOnly.Parse(dto.Date),
            TimeOnly.Parse(dto.Hour),
            organizerId
        );
    }

    public static EventEntity ToEntity(this EventPutRequestDTO dto, EventEntity existingEvent)
    {
        existingEvent.Update(//Precisei criar método Update na entidade, estava dando erro ou pegando outro id diferente, ver como resolver
            dto.Name,
            dto.Description,
            dto.Street,
            dto.Neighborhood,
            dto.City,
            dto.Number,
            dto.State,
            DateOnly.Parse(dto.Date),
            TimeOnly.Parse(dto.Hour)
        );
        return existingEvent;
    }
}