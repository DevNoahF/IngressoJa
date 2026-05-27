namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Request;

public sealed record CreateSaleRequestDTO(
    Guid UserId,
    Guid EventId,
    int SelectedTicketsUser,
    double TotalPrice,
    Guid? TicketId = null);
