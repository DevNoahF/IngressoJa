using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.EventSale;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Data.Model;
using EventStatusEnum = IngressoJa.Contexts.Eventos.Domain.Entities.Enums.EventStatusEnum;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Mapper;

public static class EventSaleMapper
{
    public static EventSaleAddEventResponseDTO ToCreateResponse(this EventSaleEntity entity)
    {
        return new EventSaleAddEventResponseDTO(
            entity.EventId,
            entity.EventName,
            entity.TicketValue,
            entity.TotalTicketQuantity,
            entity.Status
        );
    }

    public static EventSaleEntity ToEntity(this EventSaleAddEventRequestDTO dto)
    {
        return new EventSaleEntity(
            dto.EventId,
            dto.EventName,
            dto.TicketValue,
            dto.TotalTicketQuantity,
            dto.Status
        );
    }

    public static EventSaleGetResponseDTO ToGetResponse(this EventSaleEntity entity)
    {
        return new EventSaleGetResponseDTO(
            entity.EventId,
            entity.EventName,
            entity.TicketValue,
            entity.TotalTicketQuantity,
            entity.Status
            );
    }

    public static EventSaleGetResponseDTO ToGetByIdResponse(this EventSaleEntity entity)
    {
        return new EventSaleGetResponseDTO(
            entity.EventId,
            entity.EventName,
            entity.TicketValue,
            entity.TotalTicketQuantity,
            entity.Status
            );
    }

    public static EventSaleUpdateResponseDTO ToUpdateResponse(this EventSaleEntity entity)
    {
        return new EventSaleUpdateResponseDTO(
            entity.EventId,
            entity.EventName,
            entity.TicketValue,
            entity.TotalTicketQuantity,
            entity.Status
            );
    }

    public static EventSaleEntity ToEntity(this EventSaleUpdateRequestDTO dto, Guid eventId)
    {
        return new EventSaleEntity(
            eventId,
            dto.EventName,
            dto.TicketValue,
            dto.TotalTicketQuantity,
            dto.Status
            
            );
    }

    public static EventModel UpdateModel(this EventSaleEntity entity, EventModel existingModel)
    {
        existingModel.Name = entity.EventName;
        existingModel.TicketValue = entity.TicketValue;
        existingModel.TotalTicketQuantity = entity.TotalTicketQuantity;
        existingModel.Status = (IngressoJa.Contexts.Eventos.Domain.Entities.Enums.EventStatusEnum)(int)entity.Status;
        return existingModel;
    }
}

