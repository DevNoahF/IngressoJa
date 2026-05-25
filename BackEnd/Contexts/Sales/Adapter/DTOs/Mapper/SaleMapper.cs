using IngressoJa.Contexts.Sales.Adapter.DTOs.Request;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response;
using IngressoJa.Contexts.Sales.Domain.Entities;

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
}
