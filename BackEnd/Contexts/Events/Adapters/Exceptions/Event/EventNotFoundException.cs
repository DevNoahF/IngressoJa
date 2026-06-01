namespace IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

public class EventNotFoundException:Exception
{
    public EventNotFoundException(Guid id)
        : base($"Event with id {id} not found")
    {
        
    }

}