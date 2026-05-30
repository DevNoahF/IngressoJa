using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.UserSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.UserSale;

public class CreateUserSaleUseCase
{
    private readonly IUserSaleRepository _repository;
    
    public CreateUserSaleUseCase(IUserSaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateUserSaleResponse> CreateUserSale(CreateUserSaleRequestDTO userSaleDto)
    {
        try
        {
            var userSale = userSaleDto.ToEntity();
            var createdUserSale = await _repository.CreateUserSale(userSale);
            return createdUserSale.ToCreateUserSaleResponse();
        }
        catch (Exception ex)
        {
            throw new Exception("Cannot create user sale.", ex);
        }
        
    }
}