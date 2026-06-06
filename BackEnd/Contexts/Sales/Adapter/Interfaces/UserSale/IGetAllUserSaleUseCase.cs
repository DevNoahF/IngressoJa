using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface IGetAllUserSaleUseCase
{
    Task<IEnumerable<GetUserSaleResponseDTO>> GetUserAllUserSales();
}