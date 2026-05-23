namespace IngressoJa.Contexts.Vendas.Domain.Entities;
using Enums;
using Events;

public class SaleEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public int Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid EventId { get; private set; }
    public Guid? IngressoId { get; private set; }
    public int SelectedTicketsUser { get; private set; }
    public double TotalPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public SaleStatusEnum SaleStatus { get; private set; } = SaleStatusEnum.Pending;
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected SaleEntity()
    {
    }

    public SaleEntity(
        Guid userId,
        Guid eventId,
        int selectedTicketsUser,
        double totalPrice,
        Guid? ingressoId = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("The user is required.", nameof(userId));

        if (eventId == Guid.Empty)
            throw new ArgumentException("The event is required.", nameof(eventId));

        if (selectedTicketsUser <= 0)
            throw new ArgumentException("The quantity must be greater than zero.", nameof(selectedTicketsUser));

        if (totalPrice < 0)
            throw new ArgumentException("The total value cannot be negative.", nameof(totalPrice));

        if (ingressoId == Guid.Empty)
            throw new ArgumentException("The ticket is invalid.", nameof(ingressoId));

        UserId = userId;
        EventId = eventId;
        IngressoId = ingressoId;
        SelectedTicketsUser = selectedTicketsUser;
        TotalPrice = totalPrice;
        CreatedAt = DateTime.UtcNow;

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

    public void ApproveSale()
    {
        UpdateStatus(SaleStatusEnum.Approved);
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
