using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;

public record CreateUserSaleResponse(
    Guid id,
    string FirstName,
    string LastName,
    CpfVO Cpf,
    EmailVO Email
    );