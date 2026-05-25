using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Data.Model;

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
            eventEntity.Date.Value.ToString("dd/MM/yyyy"),
            eventEntity.Hour.ToString("HH:mm"),
            eventEntity.TicketValue,
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
            eventEntity.Date.Value.ToString("dd/MM/yyyy"),
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
            eventEntity.Date.Value.ToString("dd/MM/yyyy"),
            eventEntity.Hour.ToString("HH:mm"),
            eventEntity.TicketValue,
            eventEntity.TotalTicketQuantity,
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
            eventEntity.Date.Value.ToString("dd/MM/yyyy"),
            eventEntity.Hour.ToString("HH:mm"),
            eventEntity.TicketValue,
            eventEntity.TotalTicketQuantity,
            eventEntity.BannerImage,
            eventEntity.UserId,
            eventEntity.Status,
            eventEntity.CreatedAt,
            eventEntity.UpdatedAt
        );
    }

    public static EventEntity ToEntity(this EventCreateRequestDTO dto, Guid userId)
    {
        return new EventEntity(
            dto.Name,
            dto.Description,
            dto.Street,
            dto.Neighborhood,
            dto.City,
            dto.Number,
            dto.State,
            new DateVO(DateOnly.Parse(dto.Date)),
            TimeOnly.Parse(dto.Hour),
            dto.TicketValue,
            dto.TotalTicketQuantity,
            userId,
            dto.BannerImage,
            dto.Status
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
            new DateVO(DateOnly.Parse(dto.Date)),
            TimeOnly.Parse(dto.Hour),
            dto.TicketValue,
            dto.TotalTicketQuantity,
            dto.BannerImage
        );
        return existingEvent;
    }

    public static EventModel ToModel(this EventEntity entity)
    {
        return new EventModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            StreetName = entity.Street,
            Neighborhood = entity.Neighborhood,
            City = entity.City,
            Number = entity.Number,
            State = entity.State,
            Date = entity.Date,
            Hour = entity.Hour,
            TicketValue = entity.TicketValue,
            TotalTicketQuantity = entity.TotalTicketQuantity,
            BannerImage = entity.BannerImage,
            Status = entity.Status,
            UserId = entity.UserId,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static EventEntity ModelToEntity(this EventModel model)
    {
        return new EventEntity(
            model.Name,     
            model.Description,
            model.StreetName,
            model.Neighborhood,
            model.City,
            model.Number,
            model.State,
            model.Date,
            model.Hour,
            model.TicketValue,
            model.TotalTicketQuantity,
            model.UserId,         
            model.BannerImage,
            model.Status
        );
    }
}