using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface IGetUserSaleByIdUseCase
{
    Task<GetUserSaleResponseDTO?> GetUserSaleById(Guid id);
}