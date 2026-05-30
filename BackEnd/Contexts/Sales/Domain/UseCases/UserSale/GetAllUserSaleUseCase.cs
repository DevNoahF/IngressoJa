using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.UserSale;

public class GetAllUserSaleUseCase
{
    private readonly IUserSaleRepository _repository;

    public GetAllUserSaleUseCase(IUserSaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetUserSaleResponseDTO>> GetUserAllUserSales()
    {
        var userSales = await _repository.GetUserAllUserSales();
        if (!userSales.Any()) 
            throw new Exception("No UserSales Found");

        return userSales.Select(e => e.ToGetUserSaleResponseDTO());

    }
}