using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

public class NameVO
{
        public string Value { get; private set; }

        public NameVO(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EventFieldNameRequiredException("Event Name");

            if (value.Length > 55)
                throw new EventMaxLenghtExceededException("Event Name", 55);
            Value = value;
        }
}