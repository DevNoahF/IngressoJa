namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Request;

public sealed record CreateSaleRequestDTO(
    Guid UserId,
    Guid EventId,
    int SelectedTicketsUser,
    double TotalPrice,
    int AvailableTickets,
    Guid? IngressoId = null);
