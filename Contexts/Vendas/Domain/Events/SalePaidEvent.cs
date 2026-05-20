namespace IngressoJa.Contexts.Vendas.Domain.Events;

public sealed record SalePaidEvent(
    int SaleId,
    int UserId,
    int EventId,
    int SelectedTicketsUser,
    double TotalPrice,
    DateTime PaidAt) : IDomainEvent;