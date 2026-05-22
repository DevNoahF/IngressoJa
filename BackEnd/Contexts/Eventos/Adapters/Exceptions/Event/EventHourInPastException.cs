namespace IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

public class EventHourInPastException: Exception
{
    public EventHourInPastException(DateTime hour)
        : base($"Event hour {hour:HH:mm} is in the past")
    {
        
    }
}