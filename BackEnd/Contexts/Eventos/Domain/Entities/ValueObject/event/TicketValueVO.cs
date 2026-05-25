namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

public class TicketValueVO
{
    public double Value { get; private set; }

    public TicketValueVO(double value)
    {
        {
            if (value < 0)
                throw new Exception("Ticket value can't be negative");
            Value = value;
        }
    }
}
