namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Response;

public sealed record SaleResponseDTO(
    int Id,
    Guid UserId,
    Guid EventId,
    Guid? TicketId,
    int SelectedTicketsUser,
    double TotalPrice,
    string SaleStatus,
    DateTime CreatedAt);
