using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Response;

public sealed record SaleResponseDTO(
    int Id,
    int UserId,
    int EventId,
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
            venda.SelectedTicketsUser,
            venda.TotalPrice,
            venda.SaleStatus.ToString(),
            venda.CreatedAt);
    }
}
