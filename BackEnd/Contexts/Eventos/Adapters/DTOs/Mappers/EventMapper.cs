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
            eventEntity.TotalTicketQuantity,
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
            eventEntity.TotalTicketQuantity,
            eventEntity.BannerImage
        );
    }

    public static EventCreateResponseDTO ToCreateResponse(this EventEntity eventEntity)
    {
        return new EventCreateResponseDTO(
            Guid.NewGuid(),
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
            Guid.NewGuid(),
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

    public static EventEntity ToEntity(this EventPatchRequestDTO dto, EventEntity existingEvent)
    {
        var name = dto.Name ?? existingEvent.Name;
        var description = dto.Description ?? existingEvent.Description;
        var street = dto.Street ?? existingEvent.Street;
        var neighborhood = dto.Neighborhood ?? existingEvent.Neighborhood;
        var city = dto.City ?? existingEvent.City;
        var number = dto.Number ?? existingEvent.Number;
        var state = dto.State ?? existingEvent.State;
        var date = dto.Date is not null ? new DateVO(DateOnly.Parse(dto.Date)) : existingEvent.Date;
        var hour = dto.Hour is not null ? TimeOnly.Parse(dto.Hour) : existingEvent.Hour;
        var ticketValue = dto.TicketValue ?? existingEvent.TicketValue;
        var totalTicketQuantity = dto.TotalTicketQuantity ?? existingEvent.TotalTicketQuantity;
        var bannerImage = dto.BannerImage ?? existingEvent.BannerImage;

        existingEvent.Update(
            name,
            description,
            street,
            neighborhood,
            city,
            number,
            state,
            date,
            hour,
            ticketValue,
            totalTicketQuantity,
            bannerImage
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
            model.Id,
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