namespace IngressoJa.Contexts.Sales.Domain.Entities;
using Enums;
using ValueObject;

public class SaleEntity
{
    public int Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid EventId { get; private set; }
    public Guid? TicketId { get; private set; }
    public int SelectedTicketsUser { get; private set; }
    public double TotalPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public SaleStatusEnum SaleStatus { get; private set; } = SaleStatusEnum.Pending;

    protected SaleEntity()
    {
    }

    public SaleEntity(
        Guid userId,
        Guid eventId,
        int selectedTicketsUser,
        double totalPrice,
        Guid? ticketId = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("The user is required.", nameof(userId));

        if (eventId == Guid.Empty)
            throw new ArgumentException("The event is required.", nameof(eventId));

        var selectedTicketsUserVO = new SelectedTicketsUserVO(selectedTicketsUser);
        var totalPriceVO = new TotalPriceVO(totalPrice);

        if (ticketId == Guid.Empty)
            throw new ArgumentException("The ticket is invalid.", nameof(ticketId));

        UserId = userId;
        EventId = eventId;
        TicketId = ticketId;
        SelectedTicketsUser = selectedTicketsUserVO.Value;
        TotalPrice = totalPriceVO.Value;
        CreatedAt = DateTime.UtcNow;

    }

    public SaleEntity(
        int id,
        Guid userId,
        Guid eventId,
        Guid? ticketId,
        int selectedTicketsUser,
        double totalPrice,
        DateTime createdAt,
        SaleStatusEnum saleStatus)
    {
        Id = id;
        UserId = userId;
        EventId = eventId;
        TicketId = ticketId;
        SelectedTicketsUser = selectedTicketsUser;
        TotalPrice = totalPrice;
        CreatedAt = createdAt;
        SaleStatus = saleStatus;
    }

    public void UpdateStatus(SaleStatusEnum newStatus)
    {
        if (SaleStatus != SaleStatusEnum.Pending)
            throw new InvalidOperationException("Only pending sales can have their status changed.");

        if (newStatus is not (SaleStatusEnum.Approved or SaleStatusEnum.Denied))
            throw new ArgumentException("Invalid status.", nameof(newStatus));

        SaleStatus = newStatus;
    }

    public void ApproveSale()
    {
        UpdateStatus(SaleStatusEnum.Approved);
    }
}
