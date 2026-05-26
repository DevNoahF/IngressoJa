using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

public class DescriptionVO
{
    public string Value { get; private set; }

    public DescriptionVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EventFieldNameRequiredException("Description");
        
        if(value.Length > 255)
            throw new EventMaxLenghtExceededException("Description", 255);
        
        Value = value;
    }
}