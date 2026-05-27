namespace IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

public class EventNoChangesException : Exception
{
    public EventNoChangesException()
        : base("Updated event data must be different from the current event data")
    {
    }
}