using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Response;

public sealed record SaleResponseDTO(
    int Id,
    Guid UserId,
    Guid EventId,
    Guid? IngressoId,
    int SelectedTicketsUser,
    double TotalPrice,
    string SaleStatus,
    DateTime CreatedAt)
{
    public static SaleResponseDTO FromEntity(SaleEntity venda)
    {
        return new SaleResponseDTO(
            venda.Id,
            venda.UserId,
            venda.EventId,
            venda.IngressoId,
            venda.SelectedTicketsUser,
            venda.TotalPrice,
            venda.SaleStatus.ToString(),
            venda.CreatedAt);
    }
}
