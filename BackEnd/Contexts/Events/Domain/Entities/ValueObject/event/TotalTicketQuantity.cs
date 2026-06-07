namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

public class TotalTicketQuantity
{
    public int Value { get; private set; }

    public TotalTicketQuantity(int value)
    {
        if (value < 0.0)
            throw new Exception("Ticket value can't be negative");
        Value = value;
    }
}