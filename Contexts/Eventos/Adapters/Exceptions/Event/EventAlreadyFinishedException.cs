namespace IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

public class EventAlreadyFinishedException: Exception
{
    public EventAlreadyFinishedException(Guid id)
        :base($"Event with id {id} is already finished")
    {}
}