using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.EventSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Data.Model;
using EventStatusEnum = IngressoJa.Contexts.Eventos.Domain.Entities.Enums.EventStatusEnum;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;

public static class EventSaleMapper
{
    public static EventSaleAddEventResponseDTO ToCreateResponse(this EventSaleEntity entity)
    {
        return new EventSaleAddEventResponseDTO(
            entity.EventId,
            new NameVO(string.Empty),
            new TicketValueVO(0),
            new TotalTicketQuantity(0),
            entity.Status
        );
    }

    public static EventSaleEntity ToEntity(this EventSaleAddEventRequestDTO dto)
    {
        return new EventSaleEntity(
            dto.EventId,
            new NameVO(string.Empty),
            new TicketValueVO(0),
            new TotalTicketQuantity(0),
            dto.Status
        );
    }

    public static EventSaleGetResponseDTO ToGetResponse(this EventSaleEntity entity)
    {
        return new EventSaleGetResponseDTO(
            entity.EventId,
            new NameVO(string.Empty),
            new TicketValueVO(0),
            new TotalTicketQuantity(0),
            entity.Status
            );
    }

    public static EventSaleGetResponseDTO ToGetByIdResponse(this EventSaleEntity entity)
    {
        return new EventSaleGetResponseDTO(
            entity.EventId,
            new NameVO(string.Empty),
            new TicketValueVO(0),
            new TotalTicketQuantity(0),
            entity.Status
            );
    }

    public static EventSaleUpdateResponseDTO ToUpdateResponse(this EventSaleEntity entity)
    {
        return new EventSaleUpdateResponseDTO(
            entity.EventId,
            new NameVO(string.Empty),
            new TicketValueVO(0),
            new TotalTicketQuantity(0),
            entity.Status
            );
    }

    public static EventSaleEntity ToEntity(this EventSaleUpdateRequestDTO dto, Guid eventId)
    {
        return new EventSaleEntity(
            eventId,
            new NameVO(string.Empty),
            new TicketValueVO(0),
            new TotalTicketQuantity(0),
            dto.Status
            
            );
    }

    public static EventModel UpdateModel(this EventSaleEntity entity, EventModel existingModel)
    {
        existingModel.Name = new NameVO(string.Empty);
        existingModel.TicketValue = new TicketValueVO(0);
        existingModel.TotalTicketQuantity = new TotalTicketQuantity(0);
        existingModel.Status = (IngressoJa.Contexts.Eventos.Domain.Entities.Enums.EventStatusEnum)(int)entity.Status;
        return existingModel;
    }
}

