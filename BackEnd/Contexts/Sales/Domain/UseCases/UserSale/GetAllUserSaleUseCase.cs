using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.UserSale;

public class GetAllUserSaleUseCase:IGetAllUserSaleUseCase
{
    private readonly IUserSaleRepository _repository;
    private readonly IUserSaleMapper _mapper;

    public GetAllUserSaleUseCase(IUserSaleRepository repository, IUserSaleMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetUserSaleResponseDTO>> GetUserAllUserSales()
    {
        var userSales = await _repository.GetUserAllUserSales();
        if (!userSales.Any()) 
            throw new Exception("No UserSales Found");

        return userSales.Select(e => _mapper.ToGetUserSaleResponseDTO(e));

    }
}