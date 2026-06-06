using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.UserSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface IUpdateUserSaleUseCase
{
    Task<UpdateUserSaleResponseDTO> UpdateUserSale(UpdateUserSaleRequestDTO userSale, Guid userId);
}