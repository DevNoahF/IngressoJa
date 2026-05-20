namespace IngressoJa.Contexts.Vendas.Domain.Entities;
using Enums;
using Events;

public class SaleEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int EventId { get; private set; }
    public int SelectedTicketsUser { get; private set; }
    public double TotalPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public SaleStatusEnum SaleStatus { get; private set; } = SaleStatusEnum.Pending;
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected SaleEntity()
    {
    }

    public SaleEntity(
        int userId,
        int eventId,
        int selectedTicketsUser,
        double totalPrice)
    {
        if (userId <= 0)
            throw new ArgumentException("The user is required.", nameof(userId));

        if (eventId <= 0)
            throw new ArgumentException("The event is required.", nameof(eventId));

        if (selectedTicketsUser <= 0)
            throw new ArgumentException("The quantity must be greater than zero.", nameof(selectedTicketsUser));

        if (totalPrice < 0)
            throw new ArgumentException("The total value cannot be negative.", nameof(totalPrice));

        UserId = userId;
        EventId = eventId;
        SelectedTicketsUser = selectedTicketsUser;
        TotalPrice = totalPrice;
        CreatedAt = DateTime.UtcNow;
        SaleStatus = SaleStatusEnum.Pending;

    }

    public void UpdateStatus(SaleStatusEnum novoStatus)
    {
        if (SaleStatus != SaleStatusEnum.Pending)
            throw new InvalidOperationException("Only pending sales can have their status changed.");

        if (novoStatus is not (SaleStatusEnum.Approved or SaleStatusEnum.Denied))
            throw new ArgumentException("Invalid status.", nameof(novoStatus));

        SaleStatus = novoStatus;

        if (novoStatus == SaleStatusEnum.Approved)
        {
            AdicionarEvento(new SalePaidEvent(
                Id,
                UserId,
                EventId,
                SelectedTicketsUser,
                TotalPrice,
                DateTime.UtcNow));
        }
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    private void AdicionarEvento(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
