using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Request.UserSale;

public record CreateUserSaleRequestDTO(
    string FirstName,
    string LastName,
    CpfVO Cpf,
    EmailVO Email
    );
