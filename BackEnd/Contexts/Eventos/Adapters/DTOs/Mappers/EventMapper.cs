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
            new NameVO(string.Empty),
            new DescriptionVO(string.Empty),
            new StreetNameVo(string.Empty),
            new NeighborhoodVO(string.Empty),
            new CityVO(string.Empty),
            eventEntity.Number,
            eventEntity.State,
            eventEntity.Date.Value.ToString("dd/MM/yyyy"),
            eventEntity.Hour.ToString("HH:mm"),
            new TicketValueVO(0),
            new BannerImageVO(string.Empty),
            eventEntity.Status
        );
    }

    public static EventSummaryResponseDTO ToSummaryResponse(this EventEntity eventEntity)
    {
        return new EventSummaryResponseDTO(
            eventEntity.Id,
            new NameVO(string.Empty),
            new CityVO(string.Empty),
            eventEntity.State,
            eventEntity.Date.Value.ToString("dd/MM/yyyy"),
            eventEntity.Status,
            new TicketValueVO(0),
            new BannerImageVO(string.Empty)
        );
    }

    public static EventCreateResponseDTO ToCreateResponse(this EventEntity eventEntity)
    {
        return new EventCreateResponseDTO(
            eventEntity.Id,
            new NameVO(string.Empty),
            new DescriptionVO(string.Empty),
            new StreetNameVo(string.Empty),
            new NeighborhoodVO(string.Empty),
            new CityVO(string.Empty),
            eventEntity.Number,
            eventEntity.State,
            eventEntity.Date.Value.ToString("dd/MM/yyyy"),
            eventEntity.Hour.ToString("HH:mm"),
            new TicketValueVO(0),
            new TotalTicketQuantity(0),
            new BannerImageVO(string.Empty),
            eventEntity.Status,
            eventEntity.UserId,
            eventEntity.CreatedAt
        );
    }

    public static EventPutResponseDTO ToPutResponse(this EventEntity eventEntity)
    {
        return new EventPutResponseDTO(
            eventEntity.Id,
            new NameVO(string.Empty),
            new DescriptionVO(string.Empty),
            new StreetNameVo(string.Empty),
            new NeighborhoodVO(string.Empty),
            new CityVO(string.Empty),
            eventEntity.Number,
            eventEntity.State,
            eventEntity.Date.Value.ToString("dd/MM/yyyy"),
            eventEntity.Hour.ToString("HH:mm"),
            new TicketValueVO(0),
            new TotalTicketQuantity(0),
            new BannerImageVO(string.Empty),
            eventEntity.UserId,
            eventEntity.Status,
            eventEntity.CreatedAt,
            eventEntity.UpdatedAt
        );
    }

    public static EventEntity ToEntity(this EventCreateRequestDTO dto, Guid userId)
    {
        return new EventEntity(
            new NameVO(string.Empty),
            new DescriptionVO(string.Empty),
            new StreetNameVo(string.Empty),
            new NeighborhoodVO(string.Empty),
            new CityVO(string.Empty),
            dto.Number,
            dto.State,
            new DateVO(DateOnly.MinValue),
            TimeOnly.MinValue,
            new TicketValueVO(0),
            new TotalTicketQuantity(0),
            userId,
            new BannerImageVO(string.Empty),
            dto.Status
        );
    }

    public static EventEntity ToEntity(this EventPutRequestDTO dto, EventEntity existingEvent)
    {
        existingEvent.Update(
            new NameVO(string.Empty),
            new DescriptionVO(string.Empty),
            new StreetNameVo(string.Empty),
            new NeighborhoodVO(string.Empty),
            new CityVO(string.Empty),
            dto.Number,
            dto.State,
            new DateVO(DateOnly.MinValue),
            TimeOnly.MinValue,
            new TicketValueVO(0),
            new TotalTicketQuantity(0),
            new BannerImageVO(string.Empty)
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
            new NameVO(string.Empty),
            new DescriptionVO(string.Empty),
            new StreetNameVo(string.Empty),
            new NeighborhoodVO(string.Empty),
            new CityVO(string.Empty),
            model.Number,
            model.State,
            new DateVO(DateOnly.MinValue),
            TimeOnly.MinValue,
            new TicketValueVO(0),
            new TotalTicketQuantity(0),
            model.UserId,         
            new BannerImageVO(string.Empty),
            model.Status
        );
    }
}