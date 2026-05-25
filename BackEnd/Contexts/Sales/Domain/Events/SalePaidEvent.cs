namespace IngressoJa.Contexts.Sales.Domain.Events;

public sealed record SalePaidEvent(
    int SaleId,
    Guid UserId,
    Guid EventId,
    int SelectedTicketsUser,
    double TotalPrice,
    DateTime PaidAt) : IDomainEvent;
