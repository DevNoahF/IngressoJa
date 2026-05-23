namespace IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

public class EventDateInPastException : Exception
{
    public EventDateInPastException(DateTime date)
        : base($"Event date {date:dd/MM/yyyy} is in the past")
    {
    }
}