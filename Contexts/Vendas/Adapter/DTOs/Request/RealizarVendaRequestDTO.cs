namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Request;

public sealed record CreateSaleRequestDTO(
    int UserId,
    int EventId,
    int SelectedTicketsUser,
    double TotalPrice,
    int AvailableTickets);
