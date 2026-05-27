namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

public class BannerImageVO
{
    public string Value { get; private set; }

    public BannerImageVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new Exception("Banner Image can't be empty");

        if (!Uri.TryCreate(value, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new Exception("Banner Image must contain a valid URL.");
        }

        Value = value;
    }
}
