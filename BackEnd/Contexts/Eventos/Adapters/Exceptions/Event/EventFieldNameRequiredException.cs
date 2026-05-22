namespace IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

public class EventFieldNameRequiredException:Exception
{
    public EventFieldNameRequiredException(string fieldName)
        : base($"Field {fieldName} is required")
    {
    }
}