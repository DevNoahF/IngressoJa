namespace IngressoJa.Contexts.Sales.Domain.Entities;

public class TicketEntity
{
    public Guid Code { get; private set; }
    public Guid UserId { get; private set; }



    public TicketEntity(Guid code, Guid userId)
    {
        
        if (userId == Guid.Empty)
            throw new ArgumentException("The user is required.", nameof(userId));

        Code = code;
        UserId = userId;
    }
}
