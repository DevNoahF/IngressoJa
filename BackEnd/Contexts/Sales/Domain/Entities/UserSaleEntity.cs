namespace IngressoJa.Contexts.Sales.Domain.Entities;

public class UserSaleEntity
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    protected UserSaleEntity()
    {

    }

    public UserSaleEntity(
        Guid id,
        string firstName,
        string lastName,
        string cpf,
        string email)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("The user is required.", nameof(id));

        ValidateRequired(firstName, nameof(firstName), 55);
        ValidateRequired(lastName, nameof(lastName), 55);
        ValidateRequired(cpf, nameof(cpf), 11);
        ValidateRequired(email, nameof(email), 55);

        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Cpf = cpf;
        Email = email;
    }

    private static void ValidateRequired(string value, string propertyName, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The value is required.", propertyName);

        if (value.Length > maxLength)
            throw new ArgumentException($"The value must have at most {maxLength} characters.", propertyName);
    }
}
