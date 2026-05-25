namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Request;

public sealed record CreateUserRequestDTO(
    string FirstName,
    string LastName,
    string Cpf,
    string Email);
