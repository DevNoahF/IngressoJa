using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;

public static class EventMapper
{
    public static EventDetailResponseDTO ToDetailResponse(this EventEntity eventEntity)
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
            eventEntity.TicketValue,
            eventEntity.AvailableTickets,
            eventEntity.BannerImage,
            eventEntity.Status
        );
    }

    public static EventSummaryResponseDTO ToSummaryResponse(this EventEntity eventEntity)
    {
        return new EventSummaryResponseDTO(
            eventEntity.Id,
            eventEntity.Name,
            eventEntity.City,
            eventEntity.State,
            eventEntity.Date.ToString("dd/MM/yyyy"),
            eventEntity.Status,
            eventEntity.TicketValue,
            eventEntity.BannerImage
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
            eventEntity.TicketValue,
            eventEntity.TotalTicketQuantity,
            eventEntity.AvailableTickets,
            eventEntity.BannerImage,
            eventEntity.Status,
            eventEntity.UserId,
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
            eventEntity.TicketValue,
            eventEntity.TotalTicketQuantity,
            eventEntity.AvailableTickets,
            eventEntity.BannerImage,
            eventEntity.UserId,
            eventEntity.Status,
            eventEntity.CreatedAt,
            eventEntity.UpdatedAt
        );
    }
    
    public static EventEntity ToEntity(this EventCreateRequestDTO dto)
    {
        return new EventEntity(
            dto.Name,
            dto.Description,
            dto.Street,
            dto.Neighborhood,
            dto.City,
            dto.Number,
            dto.State,
            DateOnly.Parse(dto.Date),
            TimeOnly.Parse(dto.Hour),
            dto.TicketValue,
            dto.TotalTicketQuantity,
            dto.UserId,                  
            dto.TotalTicketQuantity,    
            dto.BannerImage              
        );
    }

    public static EventEntity ToEntity(this EventPutRequestDTO dto, EventEntity existingEvent)
    {
        existingEvent.Update(
            dto.Name,
            dto.Description,
            dto.Street,
            dto.Neighborhood,
            dto.City,
            dto.Number,
            dto.State,
            DateOnly.Parse(dto.Date),
            TimeOnly.Parse(dto.Hour),
            dto.TicketValue,
            dto.TotalTicketQuantity
        );
        return existingEvent;
    }
}