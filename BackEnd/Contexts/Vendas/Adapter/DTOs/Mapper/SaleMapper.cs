using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response;
using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Mapper;

public static class SaleMapper
{
    public static SaleEntity ToEntity(this CreateSaleRequestDTO dto)
    {
        return new SaleEntity(
            dto.UserId,
            dto.EventId,
            dto.SelectedTicketsUser,
            dto.TotalPrice,
            dto.IngressoId);
    }

    public static SaleResponseDTO ToResponse(this SaleEntity entity)
    {
        return new SaleResponseDTO(
            entity.Id,
            entity.UserId,
            entity.EventId,
            entity.IngressoId,
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
