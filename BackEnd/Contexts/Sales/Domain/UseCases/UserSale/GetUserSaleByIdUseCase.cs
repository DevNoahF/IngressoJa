using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.UserSale;

public class GetUserSaleByIdUseCase:IGetUserSaleByIdUseCase
{
    private readonly IUserSaleRepository _repository;
    private readonly IUserSaleMapper _mapper;

    public GetUserSaleByIdUseCase(IUserSaleRepository repository, IUserSaleMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetUserSaleResponseDTO?> GetUserSaleById(Guid id)
    {
        var userSale = await _repository.GetUserSaleById(id);

        if (userSale == null)
            throw new Exception("No User Sale Found");

        return _mapper.ToGetUserSaleResponseDTO(userSale);
    }
}