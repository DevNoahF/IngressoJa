namespace IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

public class EventAlreadyCancelledException: Exception
{
    public EventAlreadyCancelledException(Guid id)
        : base($"Event with id {id} is already cancelled")
    {
    }
}