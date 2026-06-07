using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.UserSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface ICreateUserSaleUseCase
{
    public record CreateUserSaleRequestDTO(Guid UserId);
}