using IngressoJa.Contexts.Sales.Adapter.DTOs.Request;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response;
using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Mapper;

public static class VendaMapper
{
    public static SaleResponseDTO ToDetailResponse(this SaleEntity vendaEntity)
    {
        return new SaleResponseDTO(
            vendaEntity.Id,
            vendaEntity.UserId,
            vendaEntity.EventId,
            vendaEntity.TicketId,
            vendaEntity.SelectedTicketsUser,
            vendaEntity.TotalPrice,
            vendaEntity.SaleStatus.ToString(),
            vendaEntity.CreatedAt
        );
    }

    public static SaleResponseDTO ToCreateResponse(this SaleEntity vendaEntity)
    {
        return new SaleResponseDTO(
            vendaEntity.Id,
            vendaEntity.UserId,
            vendaEntity.EventId,
            vendaEntity.TicketId,
            vendaEntity.SelectedTicketsUser,
            vendaEntity.TotalPrice,
            vendaEntity.SaleStatus.ToString(),
            vendaEntity.CreatedAt
        );
    }

    public static SaleEntity ToEntity(this CreateSaleRequestDTO dto)
    {
        return new SaleEntity(
            dto.UserId,
            dto.EventId,
            dto.SelectedTicketsUser,
            dto.TotalPrice,
            dto.TicketId
        );
    }

    public static IEnumerable<SaleResponseDTO> ToDetailResponse(this IEnumerable<SaleEntity> vendaEntities)
    {
        return vendaEntities.Select(entity => entity.ToDetailResponse());
    }

    public static IEnumerable<SaleResponseDTO> ToSummaryResponse(this IEnumerable<SaleEntity> vendaEntities)
    {
        return vendaEntities.Select(entity => entity.ToDetailResponse());
    }
}