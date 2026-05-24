namespace IngressoJa.Contexts.Vendas.Domain.Entities;

public class TicketEntity
{
    public Guid Code { get; private set; }
    public Guid UserId { get; private set; }

    // arrumar iniciar como null 
    //verificar pq acho que tem um problema com o construtor, aqui não está iniciando ele

    protected TicketEntity()
    {
    }

    public TicketEntity(Guid code, Guid userId)
    {
        if (code == Guid.Empty)
            throw new ArgumentException("The ticket code is required.", nameof(code));

        if (userId == Guid.Empty)
            throw new ArgumentException("The user is required.", nameof(userId));

        Code = code;
        UserId = userId;
    }
}
