namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Response;

public sealed record UserResponseDTO(
    Guid Id,
    string FirstName,
    string LastName,
    string Cpf,
    string Email);
