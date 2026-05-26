using IngressoJa.Contexts.Sales.Adapter.DTOs.Request;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Data.Model;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;

public static class SaleMapper
{
    public static SaleEntity ToEntity(this CreateSaleRequestDTO dto)
    {
        return new SaleEntity(
            dto.UserId,
            dto.EventId,
            dto.SelectedTicketsUser,
            dto.TotalPrice,
            dto.TicketId);
    }

    public static SaleResponseDTO ToResponse(this SaleEntity entity)
    {
        return new SaleResponseDTO(
            entity.Id,
            entity.UserId,
            entity.EventId,
            entity.TicketId,
            entity.SelectedTicketsUser,
            entity.TotalPrice,
            entity.SaleStatus.ToString(),
            entity.CreatedAt);
    }

    public static IEnumerable<SaleResponseDTO> ToResponse(this IEnumerable<SaleEntity> entities)
    {
        return entities.Select(entity => entity.ToResponse());
    }

    public static SaleModel ToModel(this SaleEntity entity)
    {
        return new SaleModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            EventId = entity.EventId,
            TicketId = entity.TicketId,
            SelectedTicketsUser = entity.SelectedTicketsUser,
            TotalPrice = entity.TotalPrice,
            CreatedAt = entity.CreatedAt,
            SaleStatus = entity.SaleStatus
        };
    }

    public static SaleEntity ToEntity(this SaleModel model)
    {
        var entity = new SaleEntity(
            model.UserId,
            model.EventId,
            model.SelectedTicketsUser,
            model.TotalPrice,
            model.TicketId);
        return entity;
    }
}
