namespace IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

public class EventMaxLenghtExceededException:Exception
{
    public EventMaxLenghtExceededException(string fieldName, int maxLength)
        :base($"Field {fieldName} cannot exceed {maxLength} characters"){}
    
}