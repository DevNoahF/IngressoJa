using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.UserSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.UserSale;

public class UpdateUserSaleUseCase
{
    private readonly IUserSaleRepository _repository;
    private readonly IUserSaleMapper _mapper;

    public UpdateUserSaleUseCase(IUserSaleRepository repository, IUserSaleMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateUserSaleResponseDTO> UpdateUserSale(UpdateUserSaleRequestDTO userSale, Guid userId)
    {
        var existingUserSale = await _repository.GetUserSaleById(userId);

        if (existingUserSale is null)
            throw new Exception("Cannot Found UserSale");

        var updatedUserSale = _mapper.ToEntity(userSale, existingUserSale.Id);
        var savedUserSale = await _repository.UpdateUserSale(updatedUserSale);

        return _mapper.ToUpdateUserSaleResponseDTO(savedUserSale);
    }
}