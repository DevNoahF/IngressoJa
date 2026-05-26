using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

public class CityVO
{
    public string Value { get; private set; }

    public CityVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EventFieldNameRequiredException("City");
        
        if(value.Length>55)
            throw new EventMaxLenghtExceededException("City",55);
        Value = value;
    }
}