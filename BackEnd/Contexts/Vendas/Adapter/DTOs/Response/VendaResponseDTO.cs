namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Response;

public sealed record SaleResponseDTO(
    int Id,
    Guid UserId,
    Guid EventId,
    Guid? IngressoId,
    int SelectedTicketsUser,
    double TotalPrice,
    string SaleStatus,
    DateTime CreatedAt);
