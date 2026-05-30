using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.UserSale;

public class GetUserSaleByIdUseCase
{
    private readonly IUserSaleRepository _repository;

    public GetUserSaleByIdUseCase(IUserSaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetUserSaleResponseDTO?> GetUserSaleById(Guid id)
    {
        var userSale = await _repository.GetUserSaleById(id);

        if (userSale == null)
            throw new Exception("No User Sale Found");

        return userSale.ToGetUserSaleResponseDTO();
    }
}