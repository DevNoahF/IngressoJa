using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.UserSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.UserSale;

public class CreateUserSaleUseCase
{
    private readonly IUserSaleRepository _repository;
    private readonly IUserSaleMapper _mapper;
    
    public CreateUserSaleUseCase(IUserSaleRepository repository, IUserSaleMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateUserSaleResponse> CreateUserSale(CreateUserSaleRequestDTO userSaleDto)
    {
        try
        {
            var userSale = _mapper.ToEntity(userSaleDto);
            var createdUserSale = await _repository.CreateUserSale(userSale);
            return _mapper.ToCreateUserSaleResponse(createdUserSale);
        }
        catch (Exception ex)
        {
            throw new Exception("Cannot create user sale.", ex);
        }
        
    }
}