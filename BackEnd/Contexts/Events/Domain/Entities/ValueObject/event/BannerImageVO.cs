using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

public class BannerImageVO
{
    public string Value { get; private set; }

    public BannerImageVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new Exception("Banner Image can't be empty");

        if (value.Length < 10)
            throw new EventMaxLenghtExceededException("BannerImage", 255);

        Value = value;
    }
}
