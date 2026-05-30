namespace IngressoJa.Contexts.Sales.Domain.Entities;

public class TicketEntity
{
    public Guid Code { get; private set; }
    public Guid UserId { get; private set; }


    public TicketEntity(Guid code, Guid userId)
    {
        Code = new Guid();
        UserId = userId;
    }
}
